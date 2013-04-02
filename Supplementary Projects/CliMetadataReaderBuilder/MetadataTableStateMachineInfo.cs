using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.OldCodeGen;
using AllenCopeland.Abstraction.OldCodeGen.Expression;
using AllenCopeland.Abstraction.OldCodeGen.Statements;
using AllenCopeland.Abstraction.OldCodeGen.Translation;
using AllenCopeland.Abstraction.OldCodeGen.Types;
using AllenCopeland.Abstraction.OldCodeGen.Types.Members;
using AllenCopeland.Abstraction.OldCodeGen.Utilities.Arrays;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables;
using System.Threading;

namespace CliMetadataReader
{
    public class MetadataTableStateMachineInfo :
        ControlledDictionary<IMetadataTableFieldDataType, MetadataTableStateMachineDataTypeInfo>
    {
        private int tableIndex;
        private MetadataTable[] tables;
        public MetadataTableStateMachineInfo(MetadataTable[] tables, int index)
        {
            this.tables = tables;
            this.tableIndex = index;
        }

        public MetadataTableStateMachineDataTypeInfo Add(IMetadataTableFieldDataType dataType)
        {
            var result = new MetadataTableStateMachineDataTypeInfo(dataType, tables[tableIndex], this.Count);
            base._Add(dataType, result);
            return result;
        }
        public int MetadataStateCount
        {
            get
            {
                return 1 << this.Count;
            }
        }

        private IFieldMember CreateSizeFor(int state, Dictionary<int, IFieldMember> stateSizes, MetadataTable targetTable)
        {
            var nonSpecialFields = from f in targetTable.Values
                                   where f.DataType is MetadataTableTypeDataType
                                   let typeDataType = (MetadataTableTypeDataType) f.DataType
                                   select typeDataType.GetSize();

            var specialFieldSizes = from f in targetTable.Values
                                    where this.Keys.Contains(f.DataType)
                                    select ((state & (1 << f.DataGroupIndex)) != 0) ? 4 : 2;
            var calculatedSize = specialFieldSizes.Sum() + nonSpecialFields.Sum();
            IFieldMember result;
            if (!stateSizes.TryGetValue(calculatedSize, out result))
            {
                var field = targetTable.DeclaredTableClass.Fields.AddNew(new TypedName(string.Format("__COR_{0}_CALC_SIZE_{1}__", targetTable.ShortName.ToUpper(), stateSizes.Count + 1), typeof(int)));
                field.AccessLevel = DeclarationAccessLevel.Internal;
                field.InitializationExpression = new PrimitiveExpression(calculatedSize);
                field.Summary = string.Format("Size constant used when the total size of the @s:{0}; is {1} bytes long.", targetTable.DeclaredTableRowClass.Name, calculatedSize);
                field.IsConstant = true;
                stateSizes.Add(calculatedSize, field);
                result = field;
            }
            return result;
        }

        public void CreateStateMachine(List<IMetadataTableFieldEncodingDataType> encodings, IType metadataRoot)
        {
            var referencesTo = (from t in tables
                                from t2 in tables
                                from f in t.Values
                                where f.DataType == t2
                                group new { Table = t, Field = f } by t2).ToDictionary(k => k.Key, v => v.ToArray());


            var table = tables[tableIndex];

            Dictionary<int, IFieldMember> stateSizeFields = new Dictionary<int, IFieldMember>();
            Dictionary<int, IFieldMember> stateSizes = new Dictionary<int, IFieldMember>();

            var fieldsTargetingThis = (from t in tables
                                       from f in t.Values
                                       where f.DataType == table
                                       where f.ImportType == MetadataTableFieldImportKind.ManyToOneImport
                                       select new { Table = t, Field = f, ListName = f.ImportName, SourceType = f.SourceKind });
            var fieldsCreatingOneToManySet =
                (from t in tables
                 from f in t.Values
                 where f.DataType == table
                 where f.ImportType == MetadataTableFieldImportKind.OneToSequentialManyImported
                 select new { Table = t, Field = f, ListName = f.ImportName, SourceType = f.SourceKind }).ToArray();

            var encodedFieldsTargetingThis =
                (from t in tables
                 from f in t.Values
                 where f.DataType.DataKind == FieldDataKind.Encoding &&
                       (from v in ((IMetadataTableFieldEncodingDataType) f.DataType).Values
                        where v.Item2 == table
                        select v).Count() > 0 &&
                       f.ImportType == MetadataTableFieldImportKind.ManyToOneImport
                 select new { Table = t, Field = f, ListName = f.ImportName, SourceType = f.SourceKind }).ToArray();

            var fieldOneToSequentialManyImported =
                (from t in tables
                 from f in t.Values
                 where f.DataType == table &&
                       f.ImportType == MetadataTableFieldImportKind.OneToSequentialManyImported
                 select new { Table = t, Field = f, ListName = f.ImportName, SourceType = f.SourceKind });

            var fieldsOneToSequentialMany = (from f in table.Values
                                             where f.DataType is MetadataTable &&
                                                   f.ImportType == MetadataTableFieldImportKind.OneToSequentialMany
                                             select new { Field = f, ListName = f.ImportName, TargetTable = f.DataType as MetadataTable });
            var importReferences = (from f in table.Values
                                    where f.ImportType == MetadataTableFieldImportKind.TableReference
                                    select f);

            for (int i = 0; i < this.MetadataStateCount; i++)
                stateSizeFields.Add(i, CreateSizeFor(i, stateSizes, table));
            IFieldMember rowSize = null;
            IFieldMember stateField = null;
            IFieldMember rowStateField = null;
            if (this.Count > 0)
            {
                rowSize = table.DeclaredTableClass.Fields.AddNew(new TypedName("__size", typeof(int)));
                stateField = table.DeclaredTableClass.Fields.AddNew(new TypedName("state", typeof(byte)));

                stateField.AccessLevel = DeclarationAccessLevel.Private;
                rowStateField = table.DeclaredTableRowClass.Fields.AddNew(new TypedName("state", typeof(byte)));
                rowStateField.Summary = string.Format("Data member which denotes the state of the row, used to calculate the size of the\r\n@s:{0};", table.DeclaredTableClass);
            }
            IMethodMember initializeMethod = table.DeclaredTableClass.Methods.AddNew(new TypedName("Initialize", typeof(void)));
            table.InitializeMethod = initializeMethod;
            initializeMethod.AccessLevel = DeclarationAccessLevel.Internal;
            var offsetParameter = initializeMethod.Parameters.AddNew(new TypedName("streamOffset", typeof(long)));

            var offsetField = table.DeclaredTableClass.Fields.AddNew(new TypedName("streamOffset", typeof(long)));
            offsetField.Summary = string.Format("Data member which denotes where in the original stream the @s:{0}; is.", table.DeclaredTableClass.Name);
            offsetParameter.DocumentationComment = string.Format("The @s:Int64; value which denotes where in the stream of @s:{0}; the @s:{1}; is.", table.ReaderField.Name, table.DeclaredTableClass.Name);
            IFieldMember totalSize = table.DeclaredTableClass.Fields.AddNew(new TypedName("length", typeof(long)));
            IPropertyMember lengthProperty = table.DeclaredTableClass.Properties.AddNew(new TypedName("Length", typeof(long)), true, false);
            table.LengthProperty = lengthProperty;
            lengthProperty.GetPart.Return(totalSize.GetReference());
            lengthProperty.AccessLevel = DeclarationAccessLevel.Public;
            initializeMethod.Assign(offsetField.GetReference(), offsetParameter.GetReference());


            LinkedList<MetadataTableStateMachineDataTypeInfo> dataTypeList = new LinkedList<MetadataTableStateMachineDataTypeInfo>();
            Dictionary<MetadataTableStateMachineDataTypeInfo, IMethodParameterMember> parameterLookup = new Dictionary<MetadataTableStateMachineDataTypeInfo, IMethodParameterMember>();
            StringBuilder sizes = new StringBuilder();
            if (this.Count == 0)
                initializeMethod.Assign(totalSize.GetReference(), new BinaryOperationExpression(stateSizeFields[0].GetReference(), CodeBinaryOperatorType.Multiply, table.RowCountField.GetReference()));
            else
            {
                bool first = true;
                foreach (var dataType in this.Values)
                {
                    if (first)
                        first = false;
                    else
                        sizes.Append(", ");
                    dataTypeList.AddLast(dataType);
                    string documentationComment;
                    var currentParameter = initializeMethod.Parameters.AddNew(CreateParameter(dataType, out documentationComment));
                    currentParameter.DocumentationComment = documentationComment;
                    if (dataType.Index > 0 && dataType.Index == this.Count - 1)
                        sizes.Append("and ");
                    sizes.AppendFormat("@p:{0};", currentParameter.Name);
                    parameterLookup.Add(dataType, currentParameter);

                }
            }



            IMethodMember readElementAtMethod = table.DeclaredTableClass.Methods.AddNew(new TypedName("ReadElementAt", table.DeclaredTableRowInterface));
            var readElementAtGotLock = readElementAtMethod.Locals.AddNew(new TypedName("gotLock", typeof(bool)));
            readElementAtGotLock.InitializationExpression = PrimitiveExpression.FalseValue;
            var monitorRef = typeof(Monitor).GetTypeReferenceExpression();
            readElementAtMethod.CallMethod(monitorRef.GetMethod("Enter").Invoke(table.SyncField.GetReference(), new DirectionExpression(FieldDirection.Ref, readElementAtGotLock.GetReference())));
            readElementAtMethod.AccessLevel = DeclarationAccessLevel.Protected;
            readElementAtMethod.Overrides = true;
            var readMethodIndex = readElementAtMethod.Parameters.AddNew(new TypedName("index", typeof(uint)));
            if (this.Count > 0)
                CreateSetStateAlternations(dataTypeList.First, initializeMethod.Statements, parameterLookup, stateField, rowSize, stateSizeFields);

            var stateSizeQuery = (from state in 0.RangeTo(this.MetadataStateCount)
                                  group state by stateSizeFields[state]).ToDictionary(k => k.Key, v => v.ToArray());


            /* *
             * Reads are sequential, thus when the datatype changes
             * the state setups need to be injected accordingly.
             * */
            List<List<MetadataTableField>> readSequence = new List<List<MetadataTableField>>();

            IMetadataTableFieldDataType lastDataType = null;
            foreach (var field in table.Values)
            {
                if (field.DataType != lastDataType)
                    readSequence.Add(new List<MetadataTableField>(new MetadataTableField[] { field }));
                else
                    readSequence[readSequence.Count - 1].Add(field);
                lastDataType = field.DataType;
            }
            if (this.Count > 0)
                initializeMethod.Assign(totalSize.GetReference(), new BinaryOperationExpression(rowSize.GetReference(), CodeBinaryOperatorType.Multiply, table.RowCountField.GetReference()));
            var zeroCondition = readElementAtMethod.IfThen(new BinaryOperationExpression(readMethodIndex.GetReference(), CodeBinaryOperatorType.IdentityEquality, PrimitiveExpression.NumberZero));
            zeroCondition.Return(PrimitiveExpression.NullValue);
            IExpression offsetExpression;
            IExpression offsetLeft = new BinaryOperationExpression(readMethodIndex.GetReference(), CodeBinaryOperatorType.Subtract, new PrimitiveExpression(1));
            if (Count > 0)
                offsetExpression = new BinaryOperationExpression(offsetLeft, CodeBinaryOperatorType.Multiply, rowSize.GetReference());
            else
                offsetExpression = new BinaryOperationExpression(offsetLeft, CodeBinaryOperatorType.Multiply, stateSizeFields[0].GetReference());
            readElementAtMethod.CallMethod(table.StreamField.GetReference().GetMethod("Seek").Invoke(new BinaryOperationExpression(offsetField.GetReference(), CodeBinaryOperatorType.Add, offsetExpression), typeof(SeekOrigin).GetTypeReferenceExpression().GetField("Begin")));
            ICreateNewObjectExpression createNewRow = new CreateNewObjectExpression(table.DeclaredTableRowClass.GetTypeReference());
            var rowSizeProperty = table.DeclaredTableRowClass.Properties.AddNew(new TypedName("Size", typeof(int)), true, false);
            rowSizeProperty.AccessLevel = DeclarationAccessLevel.Public;
            var staticReference = (from r in referencesTo
                                   where r.Key == table
                                   where r.Value.Length > 0
                                   select r).Count() > 0;
            var encodingReference = (from e in encodings
                                     where e.Contains(table)
                                     select e).Count() > 0;
            if (encodingReference || staticReference || table.NeedsIndex)
                createNewRow.Arguments.Add(readMethodIndex.GetReference());

            if (this.Count > 0)
            {
                createNewRow.Arguments.Add(stateField.GetReference());
                createNewRow.Arguments.Add(table.MetadataRootField.GetReference());
                var ctorStateParameter = table.DeclaredTableRowCtor.Parameters.AddNew(new TypedName("state", typeof(byte)));
                var ctorMetadataRootParameter = table.DeclaredTableRowCtor.Parameters.AddNew(new TypedName("metadataRoot", metadataRoot.GetTypeReference()));
                table.DeclaredTableRowCtor.Statements.Assign(table.RowMetadataRootField.GetReference(), ctorMetadataRootParameter.GetReference());
                table.DeclaredTableRowCtor.Statements.Assign(rowStateField.GetReference(), ctorStateParameter.GetReference());
                var sizeSwitch = rowSizeProperty.GetPart.SelectCase(rowStateField.GetReference());
                foreach (var state in stateSizeQuery)
                {
                    var stateList = (from s in state.Value
                                     where s > 0
                                     select new PrimitiveExpression(s)).ToArray();
                    if (stateList.Length == 0)
                        continue;
                    var currentCase = sizeSwitch.Cases.AddNew(false, new ExpressionCollection(stateList));
                    currentCase.Return(state.Key.GetReference());
                }
            }
            else
            {
                table.RowMetadataRootProperty.ParentTarget.Properties.Remove(table.RowMetadataRootProperty);
                table.RowMetadataRootField.ParentTarget.Fields.Remove(table.RowMetadataRootField);
            }

            foreach (var list in fieldsTargetingThis)
            {
                var field = list.Field;

                MetadataTable dictionarySourceTable = null;
                if (list.SourceType == MetadataTableFieldListSource.FieldRef)
                    dictionarySourceTable = field.TargetListTable ?? list.Table;
                else if (list.SourceType == MetadataTableFieldListSource.SourceTableRow)
                    dictionarySourceTable = list.Table;
                var dictionaryContainerTable = table;
                ITypeReference altType = null;
                string listName = field.ImportName;
                string targetListElementName = null;
                string indexName = null;
                if (list.SourceType == MetadataTableFieldListSource.FieldRef)
                {
                    targetListElementName = field.TargetField.PropertyReference.Name;
                    indexName = field.TargetField.PropertyIndexReference.Name;

                }
                else if (list.SourceType == MetadataTableFieldListSource.SourceTableRow)
                {
                    targetListElementName = list.Table.Name;
                    indexName = "Index";
                }

                string containerName = field.PropertyReference.Name;

                var listTable = list.Table;
                //var groupDictionary = CreateGroupDictionary(dictionarySourceTable, dictionaryContainerTable, listName, targetListElementName, containerName, indexName, field.ImportType);
                CreateOneToManyProperty(field, dictionarySourceTable, dictionaryContainerTable, listName, targetListElementName, indexName, listTable);
            }

            foreach (var list in fieldOneToSequentialManyImported)
            {
                var field = list.Field;
                MetadataTable dictionarySourceTable = null;
                string indexName = null;
                string targetListElementName = null;
                if (list.SourceType == MetadataTableFieldListSource.FieldRef)
                {
                    indexName = field.PropertyIndexReference.Name;
                    dictionarySourceTable = field.TargetListTable ?? list.Table;
                    targetListElementName = field.TargetField.PropertyReference.Name;
                }
                else if (list.SourceType == MetadataTableFieldListSource.SourceTableRow)
                {
                    dictionarySourceTable = list.Table;
                    targetListElementName = list.Table.Name;
                    indexName = "Index";
                }
                var dictionaryContainerTable = table;
                var containerName = table.Name;
                var listTable = list.Table;
                string listName = field.ImportName;
                //var groupDictionary = CreateGroupDictionary(dictionarySourceTable, dictionaryContainerTable, listName, targetListElementName, containerName, indexName, field.ImportType);
                CreateOneToManyProperty(table, field, list.Table, dictionarySourceTable);
            }

            if (encodedFieldsTargetingThis.Length > 0)
            {
                foreach (var list in encodedFieldsTargetingThis)
                {
                    var field = list.Field;

                    MetadataTable dictionarySourceTable = null;
                    if (list.SourceType == MetadataTableFieldListSource.FieldRef)
                        dictionarySourceTable = field.TargetListTable ?? list.Table;
                    else if (list.SourceType == MetadataTableFieldListSource.SourceTableRow)
                        dictionarySourceTable = list.Table;
                    var dictionaryContainerTable = table;

                    string listName = field.ImportName;
                    string targetListElementName = null;
                    string indexName = null;
                    if (list.SourceType == MetadataTableFieldListSource.FieldRef)
                    {
                        targetListElementName = field.TargetField.PropertyReference.Name;
                        indexName = field.TargetField.PropertyIndexReference.Name;

                    }
                    else if (list.SourceType == MetadataTableFieldListSource.SourceTableRow)
                    {
                        targetListElementName = list.Table.Name;
                        indexName = "Index";
                    }

                    string containerName = field.PropertyReference.Name;

                    var listTable = list.Table;

                    //var groupDictionary = CreateGroupDictionary(dictionarySourceTable, dictionaryContainerTable, listName, targetListElementName, containerName, indexName, field.ImportType);
                    CreateOneToManyProperty(field, dictionarySourceTable, dictionaryContainerTable, listName, targetListElementName, indexName, listTable);
                }
            }

            foreach (var sequentialSeriesData in fieldsOneToSequentialMany)
            {
                var field = sequentialSeriesData.Field;
                var dictionarySourceTable = sequentialSeriesData.TargetTable;
                var dictionaryContainerTable = table;
                string indexName = "Index";

                //var groupDictionary = CreateGroupDictionary(dictionarySourceTable, dictionaryContainerTable, field.ImportName, sequentialSeriesData.TargetTable.ShortName, table.ShortName, indexName, field.ImportType);

                CreateOneToManyProperty(table, field, table, dictionarySourceTable);
            }

            foreach (var import in importReferences)
            {
                var importTable = import.DataType as MetadataTable;
                if (importTable != null)
                {
                    var importTableLookedUp = importTable.DeclaredTableRowClass.Fields.AddNew(new TypedName(string.Format("checked{0}", import.ImportName), typeof(bool)));
                    var importAltTarget = import.TargetField;
                    MetadataTable targetType = null;
                    if (importAltTarget != null)
                        targetType = import.TargetField.DataType as MetadataTable;
                    else
                        targetType = table;
                    var importTableField = importTable.DeclaredTableRowClass.Fields.AddNew(new TypedName(Program.lowerFirst(import.ImportName), targetType.DeclaredTableRowInterface.GetTypeReference()));
                    var importTableProperty = importTable.DeclaredTableRowClass.Properties.AddNew(new TypedName(import.ImportName, targetType.DeclaredTableRowInterface.GetTypeReference()), true, false);
                    importTableField.Summary = string.Format("Data member for @s:{0};", importTableProperty.Name);
                    importTableProperty.Summary = import.ImportSummary;
                    importTableProperty.Remarks = import.ImportRemarks;

                    importTableProperty.AccessLevel = DeclarationAccessLevel.Public;
                    var importCheck = importTableProperty.GetPart.IfThen(new BinaryOperationExpression(importTableLookedUp.GetReference().LogicalNot(), CodeBinaryOperatorType.BooleanAnd, new BinaryOperationExpression(importTableField.GetReference(), CodeBinaryOperatorType.IdentityEquality, PrimitiveExpression.NullValue)));

                    var importCheckTable = importCheck.Locals.AddNew(new TypedName(string.Format("{0}Table", Program.lowerFirst(table.Name)), table.DeclaredTableInterface.GetTypeReference()));
                    importCheckTable.AutoDeclare = false;
                    importCheck.Add(importCheckTable.GetDeclarationStatement());
                    //importCheckTable.InitializationExpression = ;
                    importCheckTable.InitializationExpression = table.MetadataProperty.GetReference(table.RowMetadataRootField.GetReference().GetProperty("TableStream"));
                    var importTableNullCheck = importCheck.IfThen(new BinaryOperationExpression(importCheckTable.GetReference(), CodeBinaryOperatorType.IdentityInequality, PrimitiveExpression.NullValue));
                    var iteration = importTableNullCheck.Enumerate(importCheckTable.GetReference(), table.DeclaredTableRowInterface.GetTypeReference());
                    iteration.CurrentMember.Name = Program.lowerFirst(import.ImportName);
                    var iterationIndexCheck = iteration.IfThen(new BinaryOperationExpression(import.PropertyIndexReference.GetReference(iteration.CurrentMember.GetReference()), CodeBinaryOperatorType.IdentityEquality, new ThisReferenceExpression().GetProperty("Index")));
                    if (importAltTarget != null)
                        iterationIndexCheck.Assign(importTableField.GetReference(), importAltTarget.PropertyReference.GetReference(iteration.CurrentMember.GetReference()));
                    else
                        iterationIndexCheck.Assign(importTableField.GetReference(), iteration.CurrentMember.GetReference());

                    iterationIndexCheck.Break();
                    iteration.BreakLocal.AutoDeclare = false;
                    iteration.ExitLabel.Skip = true;
                    importCheck.Assign(importTableLookedUp.GetReference(), PrimitiveExpression.TrueValue);
                    table.DeclaredTableRowClass.Properties.Remove(import.PropertyReference);
                    //import.PropertyIndexReference.AccessLevel = DeclarationAccessLevel.Internal;
                    importTableProperty.GetPart.Return(importTableField.GetReference());
                }
            }

            rowSizeProperty.GetPart.Return(stateSizeFields[0].GetReference());
            if (this.Count > 0)
                initializeMethod.Summary = string.Format("Initializes the @s:{0}; with the @p:streamOffset;,{1} provided.", table.DeclaredTableClass.Name, sizes.ToString());
            foreach (var set in readSequence)
            {
                var dataType = set[0].DataType;
                switch (dataType.DataKind)
                {
                    case FieldDataKind.Encoding:
                        {
                            var encoding = dataType as IMetadataTableFieldEncodingDataType;
                            var stateWordSizeQuery =
                                (from state in 0.RangeTo(this.MetadataStateCount)
                                 let info = (from i in this
                                             where i.Key == dataType
                                             select i.Value).First()
                                 where (state & (1 << info.Index)) != 0
                                 select state).ToArray();
                            Dictionary<MetadataTableField, IStatementBlockLocalMember> currentLocals = new Dictionary<MetadataTableField, IStatementBlockLocalMember>();
                            Dictionary<MetadataTableField, IStatementBlockLocalMember> currentTagLocals = new Dictionary<MetadataTableField, IStatementBlockLocalMember>();
                            foreach (var item in set)
                            {
                                var encodedField = item as IMetadataTableEncodedField;
                                var currentLocal = readElementAtMethod.Locals.AddNew(new TypedName(string.Format("{0}Index", Program.lowerFirst(item.FieldName)), typeof(uint)));
                                currentLocal.AutoDeclare = false;
                                readElementAtMethod.Statements.Add(currentLocal.GetDeclarationStatement());
                                currentLocals.Add(item, currentLocal);
                                var currentEncodedLocal = readElementAtMethod.Locals.AddNew(new TypedName(Program.lowerFirst(encodedField.EncodingIdName), encoding.EncodingType));
                                currentEncodedLocal.AutoDeclare = false;
                                readElementAtMethod.Statements.Add(currentEncodedLocal.GetDeclarationStatement());
                                currentTagLocals.Add(item, currentEncodedLocal);
                            }
                            var stateSwitch = readElementAtMethod.SelectCase(stateField.GetReference());
                            IExpression[] currentCases = new IExpression[stateWordSizeQuery.Length];
                            for (int i = 0; i < stateWordSizeQuery.Length; i++)
                                currentCases[i] = new PrimitiveExpression(stateWordSizeQuery[i]);
                            var dwordCase = stateSwitch.Cases.AddNew(false, new ExpressionCollection(currentCases));
                            var wordCase = stateSwitch.Cases.AddNew(true, new ExpressionCollection());
                            var mask = (1 << encoding.BitEncodingSize) - 1;
                            foreach (var item in set)
                            {
                                var encodedField = item as IMetadataTableEncodedField;
                                var currentLocal = currentLocals[item];
                                var currentEncodedLocal = currentTagLocals[item];
                                dwordCase.Assign(currentLocal.GetReference(), table.ReaderField.GetReference().GetMethod("ReadUInt32").Invoke());
                                wordCase.Assign(currentLocal.GetReference(), table.ReaderField.GetReference().GetMethod("ReadUInt16").Invoke());
                                readElementAtMethod.Assign(currentEncodedLocal.GetReference(), new BinaryOperationExpression(currentLocal.GetReference(), CodeBinaryOperatorType.BitwiseAnd, new PrimitiveExpression(mask)).Cast(currentEncodedLocal.LocalType));
                                readElementAtMethod.Assign(currentLocal.GetReference(), Program.MakeRightShiftCall(currentLocal.GetReference(), new PrimitiveExpression((int) encoding.BitEncodingSize)));
                                createNewRow.Arguments.Add(currentEncodedLocal.GetReference());
                                createNewRow.Arguments.Add(currentLocal.GetReference());
                                var currentEncodedParameter = table.DeclaredTableRowCtor.Parameters.AddNew(new TypedName(currentEncodedLocal.Name, currentEncodedLocal.LocalType));
                                var currentParameter = table.DeclaredTableRowCtor.Parameters.AddNew(new TypedName(currentLocal.Name, currentLocal.LocalType));
                                table.DeclaredTableRowCtor.Statements.Assign(encodedField.EncodedField.GetReference(), currentEncodedParameter.GetReference());
                                table.DeclaredTableRowCtor.Statements.Assign(item.FieldReference.GetReference(), currentParameter.GetReference());
                            }
                            dwordCase.Break();
                            wordCase.Break();
                        }
                        break;
                    case FieldDataKind.HeapIndex:
                    case FieldDataKind.TableReference:
                        {
                            var tableRef = dataType as MetadataTable;
                            var stateWordSizeQuery =
                                (from state in 0.RangeTo(this.MetadataStateCount)
                                 let info = (from i in this
                                             where i.Key == dataType
                                             select i.Value).First()
                                 where (state & (1 << info.Index)) != 0
                                 select state).ToArray();
                            Dictionary<MetadataTableField, IStatementBlockLocalMember> currentLocals = new Dictionary<MetadataTableField, IStatementBlockLocalMember>();
                            foreach (var item in set)
                            {
                                var currentLocal = readElementAtMethod.Locals.AddNew(new TypedName(string.Format("{0}Index", Program.lowerFirst(item.FieldName)), typeof(uint)));
                                currentLocal.AutoDeclare = false;
                                readElementAtMethod.Statements.Add(currentLocal.GetDeclarationStatement());
                                currentLocals.Add(item, currentLocal);
                            }
                            var stateSwitch = readElementAtMethod.SelectCase(stateField.GetReference());
                            IExpression[] currentCases = new IExpression[stateWordSizeQuery.Length];
                            for (int i = 0; i < stateWordSizeQuery.Length; i++)
                                currentCases[i] = new PrimitiveExpression(stateWordSizeQuery[i]);
                            var dwordCase = stateSwitch.Cases.AddNew(false, new ExpressionCollection(currentCases));
                            var wordCase = stateSwitch.Cases.AddNew(true, new ExpressionCollection());
                            foreach (var item in set)
                            {
                                var currentLocal = currentLocals[item];
                                dwordCase.Assign(currentLocal.GetReference(), table.ReaderField.GetReference().GetMethod("ReadUInt32").Invoke());
                                wordCase.Assign(currentLocal.GetReference(), table.ReaderField.GetReference().GetMethod("ReadUInt16").Invoke());
                                createNewRow.Arguments.Add(currentLocal.GetReference());
                                var currentParameter = table.DeclaredTableRowCtor.Parameters.AddNew(new TypedName(currentLocal.Name, currentLocal.LocalType));
                                table.DeclaredTableRowCtor.Statements.Assign(item.FieldReference.GetReference(), currentParameter.GetReference());
                            }
                            dwordCase.Break();
                            wordCase.Break();
                        }
                        break;
                    case FieldDataKind.DataType:
                        var regularDataType = dataType as MetadataTableTypeDataType;
                        foreach (var item in set)
                        {
                            var currentLocal = readElementAtMethod.Locals.AddNew(new TypedName(Program.lowerFirst(item.FieldName), regularDataType.DataType));
                            currentLocal.AutoDeclare = false;
                            readElementAtMethod.Statements.Add(currentLocal.GetDeclarationStatement());
                            currentLocal.InitializationExpression = table.ReaderField.GetReference().GetMethod(string.Format("Read{0}", ((IExternTypeReference) regularDataType.DataType).TypeInstance.Type.Name)).Invoke();
                            createNewRow.Arguments.Add(currentLocal.GetReference());
                            var currentParameter = table.DeclaredTableRowCtor.Parameters.AddNew(new TypedName(currentLocal.Name, currentLocal.LocalType));
                            table.DeclaredTableRowCtor.Statements.Assign(item.FieldReference.GetReference(), currentParameter.GetReference());
                        }
                        break;
                    case FieldDataKind.SelfsufficientDataType:
                        var selfSuffDataType = dataType as MetadataTableTypeDataType;
                        foreach (var item in set)
                        {
                            var currentLocal = readElementAtMethod.Locals.AddNew(new TypedName(Program.lowerFirst(item.FieldName), selfSuffDataType.DataType));
                            currentLocal.InitializationExpression = new CreateNewObjectExpression(selfSuffDataType.DataType);
                            currentLocal.AutoDeclare = false;
                            readElementAtMethod.Statements.Add(currentLocal.GetDeclarationStatement());
                            readElementAtMethod.CallMethod(currentLocal.GetReference().GetMethod("Read").Invoke(table.ReaderField.GetReference()));
                            createNewRow.Arguments.Add(currentLocal.GetReference());
                            var currentParameter = table.DeclaredTableRowCtor.Parameters.AddNew(new TypedName(currentLocal.Name, currentLocal.LocalType));
                            table.DeclaredTableRowCtor.Statements.Assign(item.FieldReference.GetReference(), currentParameter.GetReference());
                        }
                        break;
                    case FieldDataKind.CastDataType:
                        var castDataType = dataType as MetadataTableTypeDataType;
                        foreach (var item in set)
                        {
                            var currentLocal = readElementAtMethod.Locals.AddNew(new TypedName(Program.lowerFirst(item.FieldName), castDataType.DataType));
                            currentLocal.AutoDeclare = false;
                            readElementAtMethod.Statements.Add(currentLocal.GetDeclarationStatement());
                            currentLocal.InitializationExpression = table.ReaderField.GetReference().GetMethod(string.Format("Read{0}", ((IExternTypeReference) (castDataType.CastType)).TypeInstance.Type.Name)).Invoke().Cast(castDataType.DataType);
                            createNewRow.Arguments.Add(currentLocal.GetReference());
                            var currentParameter = table.DeclaredTableRowCtor.Parameters.AddNew(new TypedName(currentLocal.Name, currentLocal.LocalType));
                            table.DeclaredTableRowCtor.Statements.Assign(item.FieldReference.GetReference(), currentParameter.GetReference());
                        }
                        break;
                }
            }
            var fullyRead = table.DeclaredTableClass.Fields.AddNew(new TypedName("fullyRead", typeof(bool)));
            var gotLockCondition = readElementAtMethod.IfThen(readElementAtGotLock.GetReference());
            gotLockCondition.CallMethod(monitorRef.GetMethod("Exit"), table.SyncField.GetReference());

            var readFull = table.DeclaredTableClass.Methods.AddNew(new TypedName("Read", typeof(void)));
            var fullyReadCondition = readFull.IfThen(fullyRead.GetReference());
            fullyReadCondition.Return();
            var readFullRowIndex = readFull.Locals.AddNew(new TypedName("index", typeof(uint)));
            readFullRowIndex.AutoDeclare = false;
            readFullRowIndex.InitializationExpression = PrimitiveExpression.NumberOne;
            var readFullIteration = readFull.Iterate(readFullRowIndex.GetDeclarationStatement(), new CrementStatement(CrementType.Postfix, CrementOperation.Increment, readFullRowIndex.GetReference()), new BinaryOperationExpression(readFullRowIndex.GetReference(), CodeBinaryOperatorType.LessThanOrEqual, new ThisReferenceExpression().GetProperty("Count")));
            readFullIteration.CallMethod(monitorRef.GetMethod("Enter").Invoke(table.SyncField.GetReference(), new DirectionExpression(FieldDirection.Ref, readElementAtGotLock.GetReference())));
            var readFullIterationSkipCheck = readFullIteration.IfThen(new BaseReferenceExpression().GetMethod("ItemLoaded").Invoke(readFullRowIndex.GetReference()).LogicalNot());
            var readFullGotLock = readFullIteration.Locals.AddNew(new TypedName("gotLock", typeof(bool)));
            readFullGotLock.InitializationExpression = PrimitiveExpression.FalseValue;

            offsetLeft = new BinaryOperationExpression(readMethodIndex.GetReference(), CodeBinaryOperatorType.Subtract, new PrimitiveExpression(1));
            if (Count > 0)
                offsetExpression = new BinaryOperationExpression(new BinaryOperationExpression(readFullRowIndex.GetReference(), CodeBinaryOperatorType.Subtract, PrimitiveExpression.NumberOne), CodeBinaryOperatorType.Multiply, rowSize.GetReference());
            else
                offsetExpression = new BinaryOperationExpression(new BinaryOperationExpression(readFullRowIndex.GetReference(), CodeBinaryOperatorType.Subtract, PrimitiveExpression.NumberOne), CodeBinaryOperatorType.Multiply, stateSizeFields[0].GetReference());

            readFull.AccessLevel = DeclarationAccessLevel.Public;
            readFull.Overrides = true;
            readFullIterationSkipCheck.CallMethod(table.StreamField.GetReference().GetMethod("Seek").Invoke(new BinaryOperationExpression(offsetField.GetReference(), CodeBinaryOperatorType.Add, offsetExpression), typeof(SeekOrigin).GetTypeReferenceExpression().GetField("Begin")));
            foreach (var statement in readElementAtMethod.Statements.Skip(3))
                readFullIterationSkipCheck.Statements.Add(statement);
            readFullIterationSkipCheck.Statements.Remove(readFullIterationSkipCheck.Statements[readFullIterationSkipCheck.Statements.Count - 1]);
            gotLockCondition = readFullIteration.IfThen(readElementAtGotLock.GetReference());
            gotLockCondition.CallMethod(monitorRef.GetMethod("Exit"), table.SyncField.GetReference());
            readFullIterationSkipCheck.CallMethod(new BaseReferenceExpression().GetMethod("InjectLoadedItem").Invoke(createNewRow, readFullRowIndex.GetReference()));
            readFull.Assign(fullyRead.GetReference(), PrimitiveExpression.TrueValue);
            readElementAtMethod.Return(createNewRow);
        }

        private static void CreateOneToManyProperty(MetadataTable table, MetadataTableField field, MetadataTable listTable, MetadataTable dictionarySourceTable)
        {
            var listField = table.DeclaredTableRowClass.Fields.AddNew(new TypedName(Program.lowerFirst(field.ImportName), typeof(IControlledCollection<>).GetTypeReference(new TypeReferenceCollection(dictionarySourceTable.DeclaredTableRowInterface.GetTypeReference()))));

            var listProperty = table.DeclaredTableRowClass.Properties.AddNew(new TypedName(field.ImportName, listField.FieldType), true, false);
            listProperty.Summary = field.ImportSummary;
            listProperty.Remarks = field.ListRemarks;
            listProperty.AccessLevel = DeclarationAccessLevel.Public;
            listProperty.Summary = string.Format("Returns {0}", Program.lowerFirst(field.ImportSummary == null ? field.Summary : field.ImportSummary));
            field.PropertyIndexReference.Summary = string.Format("Returns the @s:UInt32; value which determines the index of the first @s:{0}; within @s:{1};", field.PropertyReference.PropertyType.ToString(new IntermediateCodeTranslatorOptions(true)), listProperty.Name);
            listField.Summary = string.Format("Data member for @s:{0};", listProperty.Name);
            var listNullCheck = listProperty.GetPart.IfThen(new BinaryOperationExpression(listField.GetReference(), CodeBinaryOperatorType.IdentityEquality, PrimitiveExpression.NullValue));
            if (field.TargetField != null)
                field.TargetField.PropertyReference.ParentTarget.Properties.Remove(field.TargetField.PropertyReference);
            if (field.ImportType == MetadataTableFieldImportKind.OneToSequentialManyImported)
            {
                var currentEntry = listNullCheck.Locals.AddNew(new TypedName(string.Format("current{0}", listTable.ShortName), listTable.DeclaredTableRowInterface.GetTypeReference()));
                currentEntry.AutoDeclare = false;
                currentEntry.InitializationExpression = PrimitiveExpression.NullValue;
                listNullCheck.Add(currentEntry.GetDeclarationStatement());
                var tableRef = listNullCheck.Locals.AddNew(new TypedName(string.Format("{0}Table", Program.lowerFirst(listTable.ShortName)), listTable.DeclaredTableInterface.GetTypeReference()));
                tableRef.AutoDeclare = false;
                tableRef.InitializationExpression = listTable.MetadataProperty.GetReference(table.RowMetadataRootField.GetReference().GetProperty("TableStream"));
                listNullCheck.Add(tableRef.GetDeclarationStatement());
                var tableNullCheck = listNullCheck.IfThen(new BinaryOperationExpression(tableRef.GetReference(), CodeBinaryOperatorType.IdentityEquality, PrimitiveExpression.NullValue));
                tableNullCheck.Return(PrimitiveExpression.NullValue);
                var importSetEnumeration = listNullCheck.Enumerate(tableRef.GetReference(), listTable.DeclaredTableRowInterface.GetTypeReference());
                importSetEnumeration.CurrentMember.Name = string.Format(Program.lowerFirst(listTable.ShortName));
                var importFind = importSetEnumeration.IfThen(new BinaryOperationExpression(new ThisReferenceExpression().GetProperty("Index"), CodeBinaryOperatorType.IdentityEquality, field.PropertyIndexReference.GetReference(importSetEnumeration.CurrentMember.GetReference())));
                importFind.Assign(currentEntry.GetReference(), importSetEnumeration.CurrentMember.GetReference());
                importFind.Break();
                importSetEnumeration.ExitLabel.Skip = true;
                importSetEnumeration.BreakLocal.AutoDeclare = false;
                var dictType = typeof(CliMetadataLazySequentialSet<>).GetTypeReference(new TypeReferenceCollection(dictionarySourceTable.DeclaredTableRowInterface.GetTypeReference()));

                var currentNullCheck = listNullCheck.IfThen(new BinaryOperationExpression(currentEntry.GetReference(), CodeBinaryOperatorType.IdentityEquality, PrimitiveExpression.NullValue));
                currentNullCheck.Assign(listField.GetReference(), new CreateNewObjectExpression(dictType, PrimitiveExpression.NumberZero, PrimitiveExpression.NumberZero, dictionarySourceTable.MetadataProperty.GetReference(table.RowMetadataRootField.GetReference().GetProperty("TableStream"))));
                var currentBlock = currentNullCheck.FalseBlock;
                string listSingleItem = (field.ResultedListElementName != null) ? field.ResultedListElementName : field.PropertyReference.Name;

                var nextEntry = currentBlock.Locals.AddNew(new TypedName(string.Format("next{0}", listTable.ShortName), listTable.DeclaredTableRowInterface.GetTypeReference()));
                nextEntry.AutoDeclare = false;
                currentBlock.Add(nextEntry.GetDeclarationStatement());
                //dictionarySourceTable.DeclaredTableRowInterface.GetTypeReference()
                nextEntry.InitializationExpression = listTable.MetadataProperty.GetReference(table.RowMetadataRootField.GetReference().GetProperty("TableStream")).GetIndex(new BinaryOperationExpression(currentEntry.GetReference().GetProperty("Index"), CodeBinaryOperatorType.Add, PrimitiveExpression.NumberOne).Cast(typeof(int)));
                var lastItemIndex = currentBlock.Locals.AddNew(new TypedName(string.Format("last{0}Index", listSingleItem), typeof(uint)));
                lastItemIndex.AutoDeclare = false;
                currentBlock.Add(lastItemIndex.GetDeclarationStatement());
                var nextNullCheck = currentBlock.IfThen(new BinaryOperationExpression(nextEntry.GetReference(), CodeBinaryOperatorType.IdentityEquality, PrimitiveExpression.NullValue));
                nextNullCheck.Assign(lastItemIndex.GetReference(), new BinaryOperationExpression(dictionarySourceTable.MetadataProperty.GetReference(table.RowMetadataRootField.GetReference().GetProperty("TableStream")).GetProperty("Count"), CodeBinaryOperatorType.Add, PrimitiveExpression.NumberOne).Cast(typeof(uint).GetTypeReference()));
                //nextNullCheck.Assign(lastItemIndex.GetReference(), field.TargetField.PropertyIndexReference.GetReference(currentEntry.GetReference()));
                nextNullCheck.FalseBlock.Assign(lastItemIndex.GetReference(), field.TargetField.PropertyIndexReference.GetReference(nextEntry.GetReference()));
                currentBlock.Assign(listField.GetReference(), new CreateNewObjectExpression(dictType, field.TargetField.PropertyIndexReference.GetReference(currentEntry.GetReference()), lastItemIndex.GetReference(), dictionarySourceTable.MetadataProperty.GetReference(table.RowMetadataRootField.GetReference().GetProperty("TableStream"))));
                //currentBlock.Assign(listField.GetReference(), new CreateNewObjectExpression(groupDictionary.GetTypeReference(), new ThisReferenceExpression(), field.TargetField.PropertyIndexReference.GetReference(currentEntry.GetReference()), new BinaryOperationExpression(lastItemIndex.GetReference(), CodeBinaryOperatorType.Subtract, field.TargetField.PropertyIndexReference.GetReference(currentEntry.GetReference())).Cast(typeof(int))));
                listProperty.GetPart.Return(listField.GetReference());
            }
            else
            {
                var nextEntry = listNullCheck.Locals.AddNew(new TypedName(string.Format("next{0}", listTable.ShortName), table.DeclaredTableRowInterface.GetTypeReference()));
                nextEntry.AutoDeclare = false;
                listNullCheck.Add(nextEntry.GetDeclarationStatement());
                var lastItemIndex = listNullCheck.Locals.AddNew(new TypedName(string.Format("last{0}", (field.ResultedListElementName != null) ? field.ResultedListElementName : field.PropertyIndexReference.Name), typeof(uint)));
                lastItemIndex.AutoDeclare = false;
                listNullCheck.Add(lastItemIndex.GetDeclarationStatement());

                nextEntry.InitializationExpression = table.MetadataProperty.GetReference(table.RowMetadataRootField.GetReference().GetProperty("TableStream")).GetIndex(new BinaryOperationExpression(new ThisReferenceExpression().GetProperty("Index"), CodeBinaryOperatorType.Add, PrimitiveExpression.NumberOne).Cast(typeof(int)));
                var tableRef = listNullCheck.Locals.AddNew(new TypedName(string.Format("{0}Table", Program.lowerFirst(dictionarySourceTable.ShortName)), dictionarySourceTable.DeclaredTableInterface.GetTypeReference()));
                tableRef.AutoDeclare = false;
                listNullCheck.Add(tableRef.GetDeclarationStatement());
                var nextNullCheck = listNullCheck.IfThen(new BinaryOperationExpression(nextEntry.GetReference(), CodeBinaryOperatorType.IdentityInequality, PrimitiveExpression.NullValue));
                nextNullCheck.Assign(lastItemIndex.GetReference(), field.PropertyIndexReference.GetReference(nextEntry.GetReference()));
                tableRef.InitializationExpression = dictionarySourceTable.MetadataProperty.GetReference(table.RowMetadataRootField.GetReference().GetProperty("TableStream"));
                var tableNullCheck = nextNullCheck.FalseBlock.IfThen(new BinaryOperationExpression(tableRef.GetReference(), CodeBinaryOperatorType.IdentityInequality, PrimitiveExpression.NullValue));
                tableNullCheck.Assign(lastItemIndex.GetReference(), new BinaryOperationExpression(tableRef.GetReference().GetProperty("Count"), CodeBinaryOperatorType.Add, PrimitiveExpression.NumberOne).Cast(typeof(uint)));
                tableNullCheck.FalseBlock.Assign(lastItemIndex.GetReference(), field.PropertyIndexReference.GetReference());
                var dictType = typeof(CliMetadataLazySequentialSet<>).GetTypeReference(new TypeReferenceCollection(dictionarySourceTable.DeclaredTableRowInterface.GetTypeReference()));

                listNullCheck.Assign(listField.GetReference(), new CreateNewObjectExpression(dictType, field.PropertyIndexReference.GetReference(), lastItemIndex.GetReference(), tableRef.GetReference()));
                //listNullCheck.Assign(listField.GetReference(), new CreateNewObjectExpression(dictType, field.PropertyIndexReference.GetReference(), new BinaryOperationExpression(lastItemIndex.GetReference(), CodeBinaryOperatorType.Subtract, field.PropertyIndexReference.GetReference()).Cast(typeof(int))));
                listProperty.GetPart.Return(listField.GetReference());
            }
            listTable.DeclaredTableRowClass.Properties.Remove(field.PropertyReference);
        }

        private static void CreateOneToManyProperty(MetadataTableField field, MetadataTable dictionarySourceTable, MetadataTable dictionaryContainerTable, string listName, string targetListElementName, string indexName, MetadataTable listTable)
        {

            var dictType = typeof(CliMetadataLazySet<>).GetTypeReference(new TypeReferenceCollection(dictionarySourceTable.DeclaredTableRowInterface.GetTypeReference()));
            var fieldType = typeof(IControlledCollection<>).GetTypeReference(new TypeReferenceCollection(dictionarySourceTable.DeclaredTableRowInterface.GetTypeReference()));
            var d = dictType.ToString();
            var currentListHolderField = dictionaryContainerTable.DeclaredTableRowClass.Fields.AddNew(new TypedName(Program.lowerFirst(listName), fieldType));

            var currentListHolderProperty = dictionaryContainerTable.DeclaredTableRowClass.Properties.AddNew(new TypedName(listName, fieldType), true, false);
            currentListHolderField.Summary = string.Format("Data member for @s:{0};.", currentListHolderProperty.Name);
            if (string.IsNullOrEmpty(field.ImportSummary))
                currentListHolderProperty.Summary = string.Format("Returns a set of @s:{0}; which are associated to the @s:{1};.", dictionarySourceTable.DeclaredTableRowInterface.Name, dictionaryContainerTable.DeclaredTableRowClass.Name);
            else
                currentListHolderProperty.Summary = field.ImportSummary;
            currentListHolderProperty.Remarks = string.Format("Created through references from the @s:{0};.", listTable.DeclaredTableInterface.Name);
            currentListHolderProperty.AccessLevel = DeclarationAccessLevel.Public;
            var currentListHolderNullCondition = currentListHolderProperty.GetPart.IfThen(new BinaryOperationExpression(currentListHolderField.GetReference(), CodeBinaryOperatorType.IdentityEquality, PrimitiveExpression.NullValue));
            var currentListHolderLocalList = currentListHolderNullCondition.Locals.AddNew(new TypedName(Program.lowerFirst(listName), typeof(List<uint>).GetTypeReference()));
            currentListHolderLocalList.InitializationExpression = new CreateNewObjectExpression(currentListHolderLocalList.LocalType);
            currentListHolderLocalList.AutoDeclare = false;
            var currentListHolderTableRef = currentListHolderNullCondition.Locals.AddNew(new TypedName(string.Format("{0}Table", Program.lowerFirst(listName)), listTable.DeclaredTableInterface.GetTypeReference()));
            currentListHolderTableRef.InitializationExpression = listTable.MetadataProperty.GetReference(dictionaryContainerTable.RowMetadataRootField.GetReference().GetProperty("TableStream"));
            var holderTableRefNullCheck = currentListHolderNullCondition.IfThen(new BinaryOperationExpression(currentListHolderTableRef.GetReference(), CodeBinaryOperatorType.IdentityInequality, PrimitiveExpression.NullValue));
            holderTableRefNullCheck.Add(currentListHolderLocalList.GetDeclarationStatement());

            var enumeration = holderTableRefNullCheck.Enumerate(currentListHolderTableRef.GetReference(), listTable.DeclaredTableRowInterface.GetTypeReference());
            enumeration.CurrentMember.Name = Program.lowerFirst(targetListElementName);
            IConditionStatement enumerationReferenceCheck;
            if (field.DataType.DataKind == FieldDataKind.Encoding)
            {
                var encoding = field.DataType as IMetadataTableFieldEncodingDataType;
                var encodingSelection = (from etP in encoding.Values
                                         where etP.Item2 == dictionaryContainerTable
                                         select etP.Item1).First();
                var encodedField = field as IMetadataTableEncodedField;

                enumerationReferenceCheck = enumeration.IfThen(new BinaryOperationExpression(new BinaryOperationExpression(encodedField.EncodingProperty.GetReference(enumeration.CurrentMember.GetReference()), CodeBinaryOperatorType.IdentityEquality, encodingSelection), CodeBinaryOperatorType.BooleanAnd, new BinaryOperationExpression(field.PropertyIndexReference.GetReference(enumeration.CurrentMember.GetReference()), CodeBinaryOperatorType.IdentityEquality, new ThisReferenceExpression().GetProperty("Index"))));
                if (field.PropertyIndexReference != null)
                    field.PropertyIndexReference.Summary = string.Format("Returns the decoded index of the {0} relative to the appropriate table.", field.PropertyReference.Name);
                dictionarySourceTable.DeclaredTableRowClass.Properties.Remove(field.PropertyReference);
            }
            else
            {
                enumerationReferenceCheck = enumeration.IfThen(new BinaryOperationExpression(enumeration.CurrentMember.GetReference().GetProperty(field.PropertyIndexReference.Name), CodeBinaryOperatorType.IdentityEquality, new ThisReferenceExpression().GetProperty("Index")));
            }
            enumerationReferenceCheck.CallMethod(currentListHolderLocalList.GetReference().GetMethod("Add").Invoke(enumeration.CurrentMember.GetReference().GetProperty(indexName)));

            holderTableRefNullCheck.Assign(currentListHolderField.GetReference(), new CreateNewObjectExpression(dictType, currentListHolderLocalList.GetReference().GetMethod("ToArray").Invoke(), dictionarySourceTable.MetadataProperty.GetReference(dictionaryContainerTable.RowMetadataRootField.GetReference().GetProperty("TableStream"))));
            //currentListHolderNullCondition.Assign(currentListHolderCheck.GetReference(), PrimitiveExpression.TrueValue);
            currentListHolderProperty.GetPart.Return(currentListHolderField.GetReference());
        }
        /*
        private static IClassType CreateGroupDictionary(MetadataTable dictionarySourceTable, MetadataTable dictionaryContainerTable, string listName, string elementName, string containerName, string indexName, MetadataTableFieldImportKind listKind = MetadataTableFieldImportKind.ManyToOneImport)
        {
            ITypeReference listType = dictionarySourceTable.DeclaredTableRowInterface.GetTypeReference();
            var groupDictionary = dictionaryContainerTable.DeclaredTableRowClass.Partials.AddNew().Classes.AddNew(string.Format("Lazy{0}", listName));
            groupDictionary.ImplementsList.Add(typeof(IControlledCollection<>).GetTypeReference(new TypeReferenceCollection(listType)));
            groupDictionary.ImplementsList.Add(typeof(IControlledCollection).GetTypeReference());

            var groupCtor = groupDictionary.Constructors.AddNew();
            groupCtor.AccessLevel = DeclarationAccessLevel.Internal;
            var groupCtorOwnerParameter = groupCtor.Parameters.AddNew(new TypedName(Program.lowerFirst(containerName), dictionaryContainerTable.DeclaredTableRowClass.GetTypeReference()));
            var groupOwnerRef = groupDictionary.Fields.AddNew(new TypedName(groupCtorOwnerParameter.Name, groupCtorOwnerParameter.ParameterType));
            groupCtor.Statements.Assign(groupOwnerRef.GetReference(), groupCtorOwnerParameter.GetReference());

            IFieldMember groupSetField = null;
            IFieldMember startIndex = null;
            IFieldMember itemCount = null;
            switch (listKind)
            {
                case MetadataTableFieldImportKind.OneToSequentialMany:
                case MetadataTableFieldImportKind.OneToSequentialManyImported:
                    startIndex = groupDictionary.Fields.AddNew(new TypedName(string.Format("{0}StartIndex", Program.lowerFirst(elementName)), typeof(uint)));
                    itemCount = groupDictionary.Fields.AddNew(new TypedName("count", typeof(int)));
                    var groupCtorStartIndex = groupCtor.Parameters.AddNew(new TypedName(startIndex.Name, startIndex.FieldType));
                    var groupCtorCount = groupCtor.Parameters.AddNew(new TypedName(itemCount.Name, itemCount.FieldType));
                    groupCtor.Statements.Assign(startIndex.GetReference(), groupCtorStartIndex.GetReference());
                    groupCtor.Statements.Assign(itemCount.GetReference(), groupCtorCount.GetReference());
                    break;
                case MetadataTableFieldImportKind.ManyToOneImport:
                    var groupCtorSetParameter = groupCtor.Parameters.AddNew(new TypedName(Program.lowerFirst(listName), typeof(uint[])));
                    groupSetField = groupDictionary.Fields.AddNew(new TypedName(groupCtorSetParameter.Name, groupCtorSetParameter.ParameterType));
                    groupCtor.Statements.Assign(groupSetField.GetReference(), groupCtorSetParameter.GetReference());
                    break;
            }


            /*Contains method*//*
            var gircContainsMethod = groupDictionary.Methods.AddNew(new TypedName("Contains", typeof(bool)));
            gircContainsMethod.AccessLevel = DeclarationAccessLevel.Public;
            var gircContainsMethodItem = gircContainsMethod.Parameters.AddNew(Program.lowerFirst(elementName), listType);
            switch (listKind)
            {
                case MetadataTableFieldImportKind.OneToSequentialMany:
                case MetadataTableFieldImportKind.OneToSequentialManyImported:
                    gircContainsMethod.Return(new BinaryOperationExpression(new BinaryOperationExpression(startIndex.GetReference(), CodeBinaryOperatorType.LessThanOrEqual, gircContainsMethodItem.GetReference().GetProperty("Index")), CodeBinaryOperatorType.BooleanAnd, new BinaryOperationExpression(gircContainsMethodItem.GetReference().GetProperty("Index"), CodeBinaryOperatorType.LessThan, new BinaryOperationExpression(startIndex.GetReference(), CodeBinaryOperatorType.Add, itemCount.GetReference()))));
                    break;
                case MetadataTableFieldImportKind.ManyToOneImport:
                    gircContainsMethod.Return(typeof(Enumerable).GetTypeReferenceExpression().GetMethod("Contains", typeof(uint).GetTypeReference()).Invoke(groupSetField.GetReference(), gircContainsMethodItem.GetReference().GetProperty("Index")));
                    break;
            }
            
            /* *
             * Generic copy to.
             * *//*
            IMethodMember gircCopyToMethod = null;
            IMethodParameterMember gircCopyToMethodArray = null;
            IMethodParameterMember gircCopyToMethodArrayIndex = null;

            gircCopyToMethod = groupDictionary.Methods.AddNew(new TypedName("CopyTo", typeof(void)));
            gircCopyToMethodArray = gircCopyToMethod.Parameters.AddNew(new TypedName("array", listType.MakeArray(1)));
            gircCopyToMethodArrayIndex = gircCopyToMethod.Parameters.AddNew(new TypedName("arrayIndex", typeof(int)));

            var tableStreamTableRef = gircCopyToMethod.Locals.AddNew(new TypedName(Program.lowerFirst(string.Format("{0}Table", dictionarySourceTable.ShortName)), dictionarySourceTable.DeclaredTableInterface.GetTypeReference()));

            tableStreamTableRef.InitializationExpression = dictionarySourceTable.MetadataProperty.GetReference(dictionarySourceTable.RowMetadataRootField.GetReference(groupOwnerRef.GetReference()).GetProperty("TableStream"));
            tableStreamTableRef.AutoDeclare = false;
            IConditionStatement gircCopyToLengthCheck = null;
            switch (listKind)
            {
                case MetadataTableFieldImportKind.OneToSequentialMany:
                case MetadataTableFieldImportKind.OneToSequentialManyImported:
                    gircCopyToLengthCheck = gircCopyToMethod.IfThen(new BinaryOperationExpression(gircCopyToMethodArray.GetReference().GetProperty("Length"), CodeBinaryOperatorType.LessThan, itemCount.GetReference()));
                    break;
                case MetadataTableFieldImportKind.ManyToOneImport:
                    gircCopyToLengthCheck = gircCopyToMethod.IfThen(new BinaryOperationExpression(gircCopyToMethodArray.GetReference().GetProperty("Length"), CodeBinaryOperatorType.LessThan, groupSetField.GetReference().GetProperty("Length")));
                    break;
            }

            gircCopyToLengthCheck.Add(new ThrowStatement(new CreateNewObjectExpression(typeof(ArgumentException).GetTypeReference(), new ExpressionCollection(new PrimitiveExpression("array not large enough"), new PrimitiveExpression(gircCopyToMethodArray.Name)))));
            IConditionStatement gircCopyToIndexCheck = null;
            switch (listKind)
            {
                case MetadataTableFieldImportKind.OneToSequentialMany:
                case MetadataTableFieldImportKind.OneToSequentialManyImported:
                    gircCopyToIndexCheck = gircCopyToMethod.IfThen(new BinaryOperationExpression(new BinaryOperationExpression(gircCopyToMethodArrayIndex.GetReference(), CodeBinaryOperatorType.LessThan, PrimitiveExpression.NumberZero), CodeBinaryOperatorType.BooleanOr, new BinaryOperationExpression(new BinaryOperationExpression(gircCopyToMethodArrayIndex.GetReference(), CodeBinaryOperatorType.Add, itemCount.GetReference()), CodeBinaryOperatorType.GreaterThan, gircCopyToMethodArray.GetReference().GetProperty("Length"))));
                    break;
                case MetadataTableFieldImportKind.ManyToOneImport:
                    gircCopyToIndexCheck = gircCopyToMethod.IfThen(new BinaryOperationExpression(new BinaryOperationExpression(gircCopyToMethodArrayIndex.GetReference(), CodeBinaryOperatorType.LessThan, PrimitiveExpression.NumberZero), CodeBinaryOperatorType.BooleanOr, new BinaryOperationExpression(new BinaryOperationExpression(gircCopyToMethodArrayIndex.GetReference(), CodeBinaryOperatorType.Add, groupSetField.GetReference().GetProperty("Length")), CodeBinaryOperatorType.GreaterThan, gircCopyToMethodArray.GetReference().GetProperty("Length"))));
                    break;
            }
            gircCopyToIndexCheck.Add(new ThrowStatement(new CreateNewObjectExpression(typeof(ArgumentOutOfRangeException).GetTypeReference(), new ExpressionCollection(new PrimitiveExpression(gircCopyToMethodArrayIndex.Name)))));
            gircCopyToMethod.Add(tableStreamTableRef.GetDeclarationStatement());
            var gircCopyToTableNullCheck = gircCopyToMethod.IfThen(new BinaryOperationExpression(tableStreamTableRef.GetReference(), CodeBinaryOperatorType.IdentityInequality, PrimitiveExpression.NullValue));
            var iterationIndex = gircCopyToMethod.Locals.AddNew(new TypedName("localIndex", typeof(int)));
            iterationIndex.InitializationExpression = PrimitiveExpression.NumberZero;
            iterationIndex.AutoDeclare = false;
            IIterationStatement iteration = null;

            switch (listKind)
            {
                case MetadataTableFieldImportKind.OneToSequentialMany:
                case MetadataTableFieldImportKind.OneToSequentialManyImported:
                    iteration = gircCopyToTableNullCheck.Iterate(iterationIndex.GetDeclarationStatement(), new CrementStatement(CrementType.Postfix, CrementOperation.Increment, iterationIndex.GetReference()), new BinaryOperationExpression(iterationIndex.GetReference(), CodeBinaryOperatorType.LessThan, itemCount.GetReference()));
                    iteration.Assign(gircCopyToMethodArray.GetReference().GetIndex(new BinaryOperationExpression(gircCopyToMethodArrayIndex.GetReference(), CodeBinaryOperatorType.Add, iterationIndex.GetReference())), tableStreamTableRef.GetReference().GetIndex(new BinaryOperationExpression(startIndex.GetReference(), CodeBinaryOperatorType.Add, iterationIndex.GetReference()).Cast(typeof(int))));
                    break;
                case MetadataTableFieldImportKind.ManyToOneImport:
                    iteration = gircCopyToTableNullCheck.Iterate(iterationIndex.GetDeclarationStatement(), new CrementStatement(CrementType.Postfix, CrementOperation.Increment, iterationIndex.GetReference()), new BinaryOperationExpression(iterationIndex.GetReference(), CodeBinaryOperatorType.LessThan, groupSetField.GetReference().GetProperty("Length")));
                    iteration.Assign(gircCopyToMethodArray.GetReference().GetIndex(new BinaryOperationExpression(gircCopyToMethodArrayIndex.GetReference(), CodeBinaryOperatorType.Add, iterationIndex.GetReference())), tableStreamTableRef.GetReference().GetIndex(groupSetField.GetReference().GetIndex(iterationIndex.GetReference()).Cast(typeof(int))));
                    break;
            }
            gircCopyToTableNullCheck.FalseBlock.Add(new ThrowStatement(new CreateNewObjectExpression(typeof(InvalidOperationException).GetTypeReference(),new ExpressionCollection(new PrimitiveExpression("Invalid object state.")))));

            gircCopyToMethod.AccessLevel = DeclarationAccessLevel.Public;

            /* *
             * Count Property
             * *//*
            var countProperty = groupDictionary.Properties.AddNew(new TypedName("Count", typeof(int)), true, false);
            switch (listKind)
            {
                case MetadataTableFieldImportKind.OneToSequentialMany:
                case MetadataTableFieldImportKind.OneToSequentialManyImported:
                    countProperty.GetPart.Return(itemCount.GetReference());
                    break;
                case MetadataTableFieldImportKind.ManyToOneImport:
                    countProperty.GetPart.Return(groupSetField.GetReference().GetProperty("Length"));
                    break;
            }
            countProperty.AccessLevel = DeclarationAccessLevel.Public;
            IStatementBlockLocalMember currentListHolderTableRef = null;
            iterationIndex = null;
            iteration = null;
            var ircCopyToMethod = groupDictionary.Methods.AddNew(new TypedName("CopyTo", typeof(void)));
            ircCopyToMethod.PrivateImplementationTarget = typeof(IControlledCollection).GetTypeReference();
            var ircCopyToMethodArray = ircCopyToMethod.Parameters.AddNew(new TypedName("array", typeof(Array)));
            var ircCopyToMethodArrayIndex = ircCopyToMethod.Parameters.AddNew(new TypedName("arrayIndex", typeof(int)));

            currentListHolderTableRef = ircCopyToMethod.Locals.AddNew(new TypedName(Program.lowerFirst(string.Format("{0}Table", dictionarySourceTable.ShortName)), dictionarySourceTable.DeclaredTableInterface.GetTypeReference()));

            currentListHolderTableRef.InitializationExpression = dictionarySourceTable.MetadataProperty.GetReference(dictionaryContainerTable.MetadataRootField.GetReference(groupOwnerRef.GetReference()).GetProperty("TableStream"));
            currentListHolderTableRef.AutoDeclare = false;
            IConditionStatement ircCopyToLengthCheck = null;
            
            switch (listKind)
            {
                case MetadataTableFieldImportKind.OneToSequentialMany:
                case MetadataTableFieldImportKind.OneToSequentialManyImported:
                    ircCopyToLengthCheck = ircCopyToMethod.IfThen(new BinaryOperationExpression(ircCopyToMethodArray.GetReference().GetProperty("Length"), CodeBinaryOperatorType.LessThan, itemCount.GetReference()));
                    break;
                case MetadataTableFieldImportKind.ManyToOneImport:
                    ircCopyToLengthCheck = ircCopyToMethod.IfThen(new BinaryOperationExpression(ircCopyToMethodArray.GetReference().GetProperty("Length"), CodeBinaryOperatorType.LessThan, groupSetField.GetReference().GetProperty("Length")));
                    break;
            }

            ircCopyToLengthCheck.Add(new ThrowStatement(new CreateNewObjectExpression(typeof(ArgumentException).GetTypeReference(), new ExpressionCollection(new PrimitiveExpression("array not large enough"), new PrimitiveExpression(ircCopyToMethodArray.Name)))));
            IConditionStatement ircCopyToIndexCheck = null;
            switch (listKind)
            {
                case MetadataTableFieldImportKind.OneToSequentialMany:
                case MetadataTableFieldImportKind.OneToSequentialManyImported:
                    ircCopyToIndexCheck = ircCopyToMethod.IfThen(new BinaryOperationExpression(new BinaryOperationExpression(ircCopyToMethodArrayIndex.GetReference(), CodeBinaryOperatorType.LessThan, PrimitiveExpression.NumberZero), CodeBinaryOperatorType.BooleanOr, new BinaryOperationExpression(new BinaryOperationExpression(ircCopyToMethodArrayIndex.GetReference(), CodeBinaryOperatorType.Add, itemCount.GetReference()), CodeBinaryOperatorType.GreaterThan, ircCopyToMethodArray.GetReference().GetProperty("Length"))));
                    break;
                case MetadataTableFieldImportKind.ManyToOneImport:
                    ircCopyToIndexCheck = ircCopyToMethod.IfThen(new BinaryOperationExpression(new BinaryOperationExpression(ircCopyToMethodArrayIndex.GetReference(), CodeBinaryOperatorType.LessThan, PrimitiveExpression.NumberZero), CodeBinaryOperatorType.BooleanOr, new BinaryOperationExpression(new BinaryOperationExpression(ircCopyToMethodArrayIndex.GetReference(), CodeBinaryOperatorType.Add, groupSetField.GetReference().GetProperty("Length")), CodeBinaryOperatorType.GreaterThan, ircCopyToMethodArray.GetReference().GetProperty("Length"))));
                    break;
            }

            ircCopyToIndexCheck.Add(new ThrowStatement(new CreateNewObjectExpression(typeof(ArgumentOutOfRangeException).GetTypeReference(), new ExpressionCollection(new PrimitiveExpression(ircCopyToMethodArrayIndex.Name)))));
            ircCopyToMethod.Add(currentListHolderTableRef.GetDeclarationStatement());
            var ircCopyToTableNullCheck = ircCopyToMethod.IfThen(new BinaryOperationExpression(currentListHolderTableRef.GetReference(), CodeBinaryOperatorType.IdentityInequality, PrimitiveExpression.NullValue));
            iterationIndex = ircCopyToTableNullCheck.Locals.AddNew(new TypedName("localIndex", typeof(int)));
            iterationIndex.InitializationExpression = PrimitiveExpression.NumberZero;
            iterationIndex.AutoDeclare = false;
            switch (listKind)
            {
                case MetadataTableFieldImportKind.OneToSequentialMany:
                case MetadataTableFieldImportKind.OneToSequentialManyImported:
                    iteration = ircCopyToTableNullCheck.Iterate(iterationIndex.GetDeclarationStatement(), new CrementStatement(CrementType.Postfix, CrementOperation.Increment, iterationIndex.GetReference()), new BinaryOperationExpression(iterationIndex.GetReference(), CodeBinaryOperatorType.LessThan, itemCount.GetReference()));
                    iteration.CallMethod(ircCopyToMethodArray.GetReference().GetMethod("SetValue").Invoke(currentListHolderTableRef.GetReference().GetIndex(new BinaryOperationExpression(startIndex.GetReference(), CodeBinaryOperatorType.Add, iterationIndex.GetReference()).Cast(typeof(int))), new BinaryOperationExpression(ircCopyToMethodArrayIndex.GetReference(), CodeBinaryOperatorType.Add, iterationIndex.GetReference())));
                    break;
                case MetadataTableFieldImportKind.ManyToOneImport:
                    iteration = ircCopyToTableNullCheck.Iterate(iterationIndex.GetDeclarationStatement(), new CrementStatement(CrementType.Postfix, CrementOperation.Increment, iterationIndex.GetReference()), new BinaryOperationExpression(iterationIndex.GetReference(), CodeBinaryOperatorType.LessThan, groupSetField.GetReference().GetProperty("Length")));
                    iteration.CallMethod(ircCopyToMethodArray.GetReference().GetMethod("SetValue").Invoke(currentListHolderTableRef.GetReference().GetIndex(groupSetField.GetReference().GetIndex(iterationIndex.GetReference()).Cast(typeof(int))), new BinaryOperationExpression(ircCopyToMethodArrayIndex.GetReference(), CodeBinaryOperatorType.Add, iterationIndex.GetReference())));
                    break;
            }
            ircCopyToMethod.AccessLevel = DeclarationAccessLevel.Public;
            ircCopyToTableNullCheck.FalseBlock.Add(new ThrowStatement(new CreateNewObjectExpression(typeof(InvalidOperationException).GetTypeReference(), new ExpressionCollection(new PrimitiveExpression("Invalid object state.")))));


            /* *
             * Generic index of
             * *//*
            var gircIndexOf = groupDictionary.Methods.AddNew(new TypedName("IndexOf", typeof(int)));
            var gircIndexOfElement = gircIndexOf.Parameters.AddNew(Program.lowerFirst(elementName), listType);
            var gircIndexOfNullCheck = gircIndexOf.IfThen(new BinaryOperationExpression(gircIndexOfElement.GetReference(), CodeBinaryOperatorType.IdentityEquality, PrimitiveExpression.NullValue));
            gircIndexOfNullCheck.Add(new ThrowStatement(new CreateNewObjectExpression(typeof(ArgumentNullException).GetTypeReference(), new ExpressionCollection(new PrimitiveExpression(gircIndexOfElement.GetReference().Name)))));
            var gircIndexOfItemIndex = gircIndexOf.Locals.AddNew(new TypedName(Program.lowerFirst(indexName), typeof(uint)));
            gircIndexOfItemIndex.InitializationExpression = gircIndexOfElement.GetReference().GetProperty("Index");
            gircIndexOfItemIndex.AutoDeclare = false;
            gircIndexOf.Add(gircIndexOfItemIndex.GetDeclarationStatement());

            IConditionStatement gircIndexOfIterationEquality = null;
            IIterationStatement gircIndexOfIteration = null;
            switch (listKind)
            {
                case MetadataTableFieldImportKind.OneToSequentialMany:
                case MetadataTableFieldImportKind.OneToSequentialManyImported:
                    //gircIndexOfIteration = gircIndexOf.Iterate(gircIndexOfLocalIndex.GetDeclarationStatement(), new CrementStatement(CrementType.Postfix, CrementOperation.Increment, gircIndexOfLocalIndex.GetReference()), new BinaryOperationExpression(gircIndexOfLocalIndex.GetReference(), CodeBinaryOperatorType.LessThan, itemCount.GetReference()));
                    gircIndexOfIterationEquality = gircIndexOf.IfThen(new BinaryOperationExpression(new BinaryOperationExpression(startIndex.GetReference(), CodeBinaryOperatorType.LessThanOrEqual, gircIndexOfItemIndex.GetReference()), CodeBinaryOperatorType.BooleanAnd, new BinaryOperationExpression(gircIndexOfItemIndex.GetReference(), CodeBinaryOperatorType.LessThan, new BinaryOperationExpression(startIndex.GetReference(), CodeBinaryOperatorType.Add, itemCount.GetReference()))));
                    gircIndexOfIterationEquality.Return(new BinaryOperationExpression(gircIndexOfItemIndex.GetReference(), CodeBinaryOperatorType.Subtract, startIndex.GetReference()).Cast(typeof(int)));
                    break;
                case MetadataTableFieldImportKind.ManyToOneImport:
                    var gircIndexOfLocalIndex = gircIndexOf.Locals.AddNew(new TypedName("localIndex", typeof(int)));
                    gircIndexOfLocalIndex.InitializationExpression = PrimitiveExpression.NumberZero;
                    gircIndexOfLocalIndex.AutoDeclare = false;
                    gircIndexOfIteration = gircIndexOf.Iterate(gircIndexOfLocalIndex.GetDeclarationStatement(), new CrementStatement(CrementType.Postfix, CrementOperation.Increment, gircIndexOfLocalIndex.GetReference()), new BinaryOperationExpression(gircIndexOfLocalIndex.GetReference(), CodeBinaryOperatorType.LessThan, groupSetField.GetReference().GetProperty("Length")));
                    gircIndexOfIterationEquality = gircIndexOfIteration.IfThen(new BinaryOperationExpression(gircIndexOfItemIndex.GetReference(), CodeBinaryOperatorType.IdentityEquality, groupSetField.GetReference().GetIndex(gircIndexOfLocalIndex.GetReference())));
                    gircIndexOfIterationEquality.Return(gircIndexOfLocalIndex.GetReference());
                    break;
            }
            gircIndexOf.Return(PrimitiveExpression.NumberMinusOne);
            gircIndexOf.AccessLevel = DeclarationAccessLevel.Public;

            /* *
             * ToArray
             * *//*
            var gircToArray = groupDictionary.Methods.AddNew(new TypedName("ToArray", listType.MakeArray(1)));
            var gircToArrayResult = gircToArray.Locals.AddNew(new TypedName("result", gircToArray.ReturnType));
            switch (listKind)
            {
                case MetadataTableFieldImportKind.OneToSequentialMany:
                case MetadataTableFieldImportKind.OneToSequentialManyImported:
                    gircToArrayResult.InitializationExpression = new CreateArrayExpression(listType, itemCount.GetReference());
                    break;
                case MetadataTableFieldImportKind.ManyToOneImport:
                    gircToArrayResult.InitializationExpression = new CreateArrayExpression(listType, groupSetField.GetReference().GetProperty("Length"));
                    break;
            }
            gircToArray.CallMethod(gircCopyToMethod.GetReference().Invoke(gircToArrayResult.GetReference(), PrimitiveExpression.NumberZero));
            gircToArray.Return(gircToArrayResult.GetReference());
            gircToArray.AccessLevel = DeclarationAccessLevel.Public;


            var gircThis = groupDictionary.Properties.AddNew(listType, true, false, new TypedName("index", typeof(int)));
            gircThis.AccessLevel = DeclarationAccessLevel.Public;
            var gircThisIndex = gircThis.Parameters["index"];
            IConditionStatement gircThisRangeCheck = null;
            switch (listKind)
            {
                case MetadataTableFieldImportKind.OneToSequentialMany:
                case MetadataTableFieldImportKind.OneToSequentialManyImported:
                    gircThisRangeCheck = gircThis.GetPart.IfThen(new BinaryOperationExpression(new BinaryOperationExpression(gircThisIndex.GetReference(), CodeBinaryOperatorType.LessThan, PrimitiveExpression.NumberZero), CodeBinaryOperatorType.BooleanOr, new BinaryOperationExpression(gircThisIndex.GetReference(), CodeBinaryOperatorType.GreaterThanOrEqual, itemCount.GetReference())));
                    gircThisRangeCheck.Add(new ThrowStatement(new CreateNewObjectExpression(typeof(ArgumentOutOfRangeException).GetTypeReference(), new ExpressionCollection(new PrimitiveExpression(gircThisIndex.Name)))));
                    gircThis.GetPart.Return(dictionarySourceTable.MetadataProperty.GetReference(dictionarySourceTable.RowMetadataRootField.GetReference(groupOwnerRef.GetReference()).GetProperty("TableStream")).GetIndex(new BinaryOperationExpression(startIndex.GetReference(), CodeBinaryOperatorType.Add, gircThisIndex.GetReference()).Cast(typeof(int))));
                    break;
                case MetadataTableFieldImportKind.ManyToOneImport:
                    gircThisRangeCheck = gircThis.GetPart.IfThen(new BinaryOperationExpression(new BinaryOperationExpression(gircThisIndex.GetReference(), CodeBinaryOperatorType.LessThan, PrimitiveExpression.NumberZero), CodeBinaryOperatorType.BooleanOr, new BinaryOperationExpression(gircThisIndex.GetReference(), CodeBinaryOperatorType.GreaterThanOrEqual, groupSetField.GetReference().GetProperty("Length"))));
                    gircThisRangeCheck.Add(new ThrowStatement(new CreateNewObjectExpression(typeof(ArgumentOutOfRangeException).GetTypeReference(), new ExpressionCollection(new PrimitiveExpression(gircThisIndex.Name)))));
                    gircThis.GetPart.Return(dictionarySourceTable.MetadataProperty.GetReference(dictionarySourceTable.RowMetadataRootField.GetReference(groupOwnerRef.GetReference()).GetProperty("TableStream")).GetIndex(groupSetField.GetReference().GetIndex(gircThisIndex.GetReference()).Cast(typeof(int))));
                    break;
            }

            var gircEnumeration = groupDictionary.Methods.AddNew(new TypedName("GetEnumerator", typeof(IEnumerator<>).GetTypeReference(new TypeReferenceCollection(gircThis.PropertyType))));
            gircEnumeration.AccessLevel = DeclarationAccessLevel.Public;
            tableStreamTableRef = gircEnumeration.Locals.AddNew(new TypedName(Program.lowerFirst(string.Format("{0}Table", dictionarySourceTable.ShortName)), dictionarySourceTable.DeclaredTableInterface.GetTypeReference()));

            
            tableStreamTableRef.InitializationExpression = dictionarySourceTable.MetadataProperty.GetReference(dictionaryContainerTable.MetadataRootField.GetReference(groupOwnerRef.GetReference()).GetProperty("TableStream"));
            var nullCheck = gircEnumeration.IfThen(new BinaryOperationExpression(tableStreamTableRef.GetReference(), CodeBinaryOperatorType.IdentityInequality, PrimitiveExpression.NullValue));
            iterationIndex = nullCheck.Locals.AddNew(new TypedName("localIndex", typeof(int)));
            iterationIndex.InitializationExpression = PrimitiveExpression.NumberZero;
            iterationIndex.AutoDeclare = false;
            switch (listKind)
            {
                case MetadataTableFieldImportKind.OneToSequentialMany:
                case MetadataTableFieldImportKind.OneToSequentialManyImported:
                    iteration = nullCheck.Iterate(iterationIndex.GetDeclarationStatement(), new CrementStatement(CrementType.Postfix, CrementOperation.Increment, iterationIndex.GetReference()), new BinaryOperationExpression(iterationIndex.GetReference(), CodeBinaryOperatorType.LessThan, itemCount.GetReference()));
                    iteration.Yield(tableStreamTableRef.GetReference().GetIndex(new BinaryOperationExpression(startIndex.GetReference(), CodeBinaryOperatorType.Add, iterationIndex.GetReference()).Cast(typeof(int))));
                    break;
                case MetadataTableFieldImportKind.ManyToOneImport:
                    iteration = nullCheck.Iterate(iterationIndex.GetDeclarationStatement(), new CrementStatement(CrementType.Postfix, CrementOperation.Increment, iterationIndex.GetReference()), new BinaryOperationExpression(iterationIndex.GetReference(), CodeBinaryOperatorType.LessThan, groupSetField.GetReference().GetProperty("Length")));
                    iteration.Yield(tableStreamTableRef.GetReference().GetIndex(groupSetField.GetReference().GetIndex(iterationIndex.GetReference()).Cast(typeof(int))));
                    break;
            }


            var ircEnumeration = groupDictionary.Methods.AddNew(new TypedName("ge", typeof(IEnumerator)));
            ircEnumeration.PrivateImplementationTarget = typeof(IEnumerable).GetTypeReference();
            ircEnumeration.Return(gircEnumeration.GetReference().Invoke());
            ircEnumeration.Name = "GetEnumerator";

            var ircContains = groupDictionary.Methods.AddNew(new TypedName("Contains", typeof(bool)));
            var ircContainsElement = ircContains.Parameters.AddNew(new TypedName(gircContainsMethodItem.Name, typeof(object)));
            ircContains.PrivateImplementationTarget = typeof(IControlledCollection).GetTypeReference();
            ircContains.Return(gircContainsMethod.GetReference().Invoke(ircContainsElement.GetReference().Cast(listType)));


            var ircIndexOf = groupDictionary.Methods.AddNew(new TypedName("IndexOf", typeof(int)));
            ircIndexOf.PrivateImplementationTarget = typeof(IControlledCollection).GetTypeReference();
            var ircIndexOfElement = ircIndexOf.Parameters.AddNew(new TypedName(gircContainsMethodItem.Name, typeof(object)));
            ircIndexOf.Return(new ThisReferenceExpression().GetMethod("IndexOf").Invoke(ircIndexOfElement.GetReference().Cast(listType)));
            var ircThis = groupDictionary.Properties.AddNew(typeof(object).GetTypeReference(), true, false, new TypedName("index", typeof(void)));
            var ircThisIndex = ircThis.Parameters["index"];
            ircThis.PrivateImplementationTarget = typeof(IControlledCollection).GetTypeReference();
            ircThisIndex.ParameterType = typeof(int).GetTypeReference();
            ircThis.GetPart.Return(new ThisReferenceExpression().GetIndex(ircThisIndex.GetReference()));
            return groupDictionary;
        }//*/

        private TypedName CreateParameter(MetadataTableStateMachineDataTypeInfo dataType, out string documentationComment)
        {
            switch (dataType.DataType.DataKind)
            {
                case FieldDataKind.Encoding:
                    var encoding = dataType.DataType as IMetadataTableFieldEncodingDataType;
                    documentationComment = string.Format("The @s:{1}; used to denote the word size of the indices nwith the @s:{0}; encoding.", encoding.EncodingType, typeof(CliMetadataReferenceIndexSize).GetTypeReference().TypeInstance.GetTypeName(new IntermediateCodeTranslatorOptions(true)));
                    return new TypedName(string.Format("{0}Size", Program.lowerFirst(encoding.Name)), typeof(CliMetadataReferenceIndexSize).GetTypeReference());
                case FieldDataKind.HeapIndex:
                    var heapType = (IMetadataTableFieldHeapDataType) dataType.DataType;
                    var heapReferenceType = (ITypeReference) null;
                    switch (heapType.Heap)
                    {
                        case MetadataHeapTarget.StringsHeap:
                            heapReferenceType = typeof(CliMetadataStringsHeaderAndHeap).GetTypeReference();
                            break;
                        case MetadataHeapTarget.UserStringsHeap:
                            heapReferenceType = typeof(CliMetadataUserStringsHeaderAndHeap).GetTypeReference();
                            break;
                        case MetadataHeapTarget.BlobHeap:
                            heapReferenceType = typeof(CliMetadataBlobHeaderAndHeap).GetTypeReference();
                            break;
                        case MetadataHeapTarget.GuidHeap:
                            heapReferenceType = typeof(CliMetadataGuidHeaderAndHeap).GetTypeReference();
                            break;
                    }
                    documentationComment = string.Format("The @s:{1}; used to denote the word size of the @s:{0};.", heapReferenceType, typeof(CliMetadataReferenceIndexSize).GetTypeReference().TypeInstance.GetTypeName(new IntermediateCodeTranslatorOptions(true)));
                    return new TypedName(string.Format("{0}Size", Program.lowerFirst(heapType.Heap.ToString())), typeof(CliMetadataReferenceIndexSize));
                case FieldDataKind.TableReference:
                    var table = dataType.DataType as MetadataTable;
                    documentationComment = string.Format("The @s:{1}; used to denote the word size of the @s:{0};.", table.DeclaredTableInterface.Name, typeof(CliMetadataReferenceIndexSize).GetTypeReference().TypeInstance.GetTypeName(new IntermediateCodeTranslatorOptions(true)));
                    return new TypedName(string.Format("{0}Size", Program.lowerFirst(table.ShortName)), typeof(CliMetadataReferenceIndexSize).GetTypeReference());
            }
            throw new ArgumentOutOfRangeException("dataType.DataType");
        }

        private TypedName CreateField(IMetadataTableFieldDataType dataType)
        {
            switch (dataType.DataKind)
            {
                case FieldDataKind.Encoding:
                    var encoding = dataType as IMetadataTableFieldEncodingDataType;
                    return new TypedName(string.Format("{0}Mask", Program.lowerFirst(encoding.Name)), typeof(CliMetadataReferenceIndexSize).GetTypeReference());
                case FieldDataKind.HeapIndex:
                    var heapType = (IMetadataTableFieldHeapDataType) dataType;
                    return new TypedName(string.Format("{0}Mask", Program.lowerFirst(heapType.Heap.ToString())), typeof(CliMetadataReferenceIndexSize).GetTypeReference());
                case FieldDataKind.TableReference:
                    var table = dataType as MetadataTable;
                    return new TypedName(string.Format("{0}Mask", Program.lowerFirst(table.ShortName)), typeof(CliMetadataReferenceIndexSize).GetTypeReference());
            }
            throw new ArgumentOutOfRangeException("dataType.DataType");
        }

        public void CreateSetStateAlternations(LinkedListNode<MetadataTableStateMachineDataTypeInfo> currentPosition, IStatementBlock target, Dictionary<MetadataTableStateMachineDataTypeInfo, IMethodParameterMember> parameterLookup, IFieldMember stateField, IFieldMember rowSizeField, Dictionary<int, IFieldMember> stateSizeFields, int currentStateValue = 0)
        {
            var condition = target.IfThen(new BinaryOperationExpression(parameterLookup[currentPosition.Value].GetReference(), CodeBinaryOperatorType.IdentityEquality, typeof(CliMetadataReferenceIndexSize).GetTypeReferenceExpression().GetField("DWord")));
            if (currentPosition.Next == null)
            {
                var setStateValue = currentStateValue | (1 << currentPosition.Value.Index);
                condition.Assign(stateField.GetReference(), new PrimitiveExpression(setStateValue));
                condition.Assign(rowSizeField.GetReference(), stateSizeFields[setStateValue].GetReference());
                condition.FalseBlock.Assign(stateField.GetReference(), new PrimitiveExpression(currentStateValue));
                condition.FalseBlock.Assign(rowSizeField.GetReference(), stateSizeFields[currentStateValue].GetReference());
            }
            else
            {
                CreateSetStateAlternations(currentPosition.Next, condition.Statements, parameterLookup, stateField, rowSizeField, stateSizeFields, currentStateValue | (1 << currentPosition.Value.Index));
                CreateSetStateAlternations(currentPosition.Next, condition.FalseBlock, parameterLookup, stateField, rowSizeField, stateSizeFields, currentStateValue);
            }
        }
    }
}
