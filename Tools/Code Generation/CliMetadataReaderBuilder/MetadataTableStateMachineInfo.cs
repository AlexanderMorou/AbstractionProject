using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions;
//using AllenCopeland.Abstraction.OldCodeGen;
//using AllenCopeland.Abstraction.OldCodeGen.Expression;
//using AllenCopeland.Abstraction.OldCodeGen.Statements;
//using AllenCopeland.Abstraction.OldCodeGen.Translation;
//using AllenCopeland.Abstraction.OldCodeGen.Types;
//using AllenCopeland.Abstraction.OldCodeGen.Types.Members;
//using AllenCopeland.Abstraction.OldCodeGen.Utilities.Arrays;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Ast.Cli;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables;
using System.Threading;
using System.Collections.ObjectModel;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

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

        private IIntermediateClassFieldMember CreateSizeFor(int state, Dictionary<int, IIntermediateClassFieldMember> stateSizes, MetadataTable targetTable)
        {
            var nonSpecialFields = from f in targetTable.Values
                                   where f.DataType is MetadataTableTypeDataType
                                   let typeDataType = (MetadataTableTypeDataType) f.DataType
                                   select typeDataType.GetSize();

            var specialFieldSizes = from f in targetTable.Values
                                    where this.Keys.Contains(f.DataType)
                                    select ((state & (1 << f.DataGroupIndex)) != 0) ? 4 : 2;
            var calculatedSize = specialFieldSizes.Sum() + nonSpecialFields.Sum();
            IIntermediateClassFieldMember result;
            if (!stateSizes.TryGetValue(calculatedSize, out result))
            {
                var field = targetTable.DeclaredTableClass.Fields.Add(typeof(int).WithName(string.Format("__COR_{0}_CALC_SIZE_{1}__", targetTable.ShortName.ToUpper(), stateSizes.Count + 1)));
                field.AccessLevel = AccessLevelModifiers.Internal;
                field.InitializationExpression = calculatedSize.ToPrimitive();//new PrimitiveExpression(calculatedSize);
                field.SummaryText = string.Format("Size constant used when the total size of the @s:{0}; is {1} bytes long.", targetTable.DeclaredLockedTableRowClass.Name, calculatedSize);
                field.Constant = true;
                //field.IsStatic = true;
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

            Dictionary<int, IIntermediateClassFieldMember> stateSizeFields  = new Dictionary<int, IIntermediateClassFieldMember>();
            Dictionary<int, IIntermediateClassFieldMember> stateSizes       = new Dictionary<int, IIntermediateClassFieldMember>();

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
            IIntermediateClassFieldMember rowSize = null;
            IIntermediateClassFieldMember stateField = null;
            IIntermediateClassFieldMember rowStateField = null;
            if (this.Count > 0)
            {
                rowSize = table.DeclaredTableClass.Fields.Add(typeof(int).WithName("__size"));
                rowSize.AccessLevel = AccessLevelModifiers.Private;
                stateField = table.DeclaredTableClass.Fields.Add(typeof(byte).WithName("state"));
                stateField.AccessLevel = AccessLevelModifiers.Private;

                rowStateField = table.DeclaredLockedTableRowClass.Fields.Add(typeof(byte).WithName("state"));
                rowStateField.SummaryText = string.Format("Data member which denotes the state of the row, used to calculate the size of the\r\n@s:{0};", table.DeclaredTableClass);
                rowStateField.AccessLevel = AccessLevelModifiers.Private;
            }
            var initializeMethod = table.DeclaredTableClass.Methods.Add(typeof(void).WithName("Initialize"));
            table.InitializeMethod = initializeMethod;
            initializeMethod.AccessLevel = AccessLevelModifiers.Internal;
            var offsetParameter = initializeMethod.Parameters.Add(typeof(long).WithName("streamOffset"));

            var offsetField = table.DeclaredTableClass.Fields.Add(typeof(long).WithName("streamOffset"));
            offsetField.AccessLevel = AccessLevelModifiers.Private;
            offsetField.SummaryText = string.Format("Data member which denotes where in the original stream the @s:{0}; is.", table.DeclaredTableClass.Name);
            offsetParameter.SummaryText = string.Format("The @s:Int64; value which denotes where in the stream of @s:{0}; the @s:{1}; is.", table.ReaderField.Name, table.DeclaredTableClass.Name);
            var totalSize = table.DeclaredTableClass.Fields.Add(typeof(long).WithName("length"));
            var lengthProperty = table.DeclaredTableClass.Properties.Add(typeof(long).WithName("Length"), true, false);
            totalSize.AccessLevel = AccessLevelModifiers.Private;
            table.LengthProperty = lengthProperty;
            lengthProperty.GetMethod.Return(totalSize.GetReference());
            lengthProperty.AccessLevel = AccessLevelModifiers.Public;
            initializeMethod.Assign(offsetField.GetReference(), offsetParameter.GetReference());


            LinkedList<MetadataTableStateMachineDataTypeInfo> dataTypeList = new LinkedList<MetadataTableStateMachineDataTypeInfo>();
            var parameterLookup = new Dictionary<MetadataTableStateMachineDataTypeInfo, IIntermediateMethodParameterMember>();
            StringBuilder sizes = new StringBuilder();
            if (this.Count == 0)
                initializeMethod.Assign(totalSize, stateSizeFields[0].Multiply(table.RowCountField));
                //initializeMethod.Assign(totalSize.GetReference(), new BinaryOperationExpression(stateSizeFields[0].GetReference(), CodeBinaryOperatorType.Multiply, table.RowCountField.GetReference()));
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
                    var currentParameter = initializeMethod.Parameters.Add(CreateParameter(dataType, out documentationComment));
                    currentParameter.SummaryText = documentationComment;
                    if (dataType.Index > 0 && dataType.Index == this.Count - 1)
                        sizes.Append("and ");
                    sizes.AppendFormat("@p:{0};", currentParameter.Name);
                    parameterLookup.Add(dataType, currentParameter);

                }
            }


            IIntermediateClassMethodMember readElementAtMethod = table.DeclaredTableClass.Methods.Add(new TypedName("ReadElementAt", table.DeclaredLockedTableRowInterface));
            var readElementAtGotLock = readElementAtMethod.Locals.Add(typeof(bool).WithName("gotLock"));
            readElementAtGotLock.InitializationExpression = IntermediateGateway.FalseValue;
            var monitorRef = typeof(Monitor).GetTypeExpression();
            readElementAtMethod.Call(monitorRef.GetMethod("Enter").Invoke(table.SyncField.GetReference(), readElementAtGotLock.GetReference().Direct(ParameterCoercionDirection.Reference)));
            readElementAtMethod.AccessLevel = AccessLevelModifiers.Protected;
            readElementAtMethod.IsOverride = true;
            var readMethodIndex = readElementAtMethod.Parameters.Add(typeof(uint).WithName("index"));
            if (this.Count > 0)
                CreateSetStateAlternations(dataTypeList.First, initializeMethod, parameterLookup, stateField, rowSize, stateSizeFields);

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
                initializeMethod.Assign(
                    totalSize, 
                    rowSize.Multiply(
                        table.RowCountField));
            /*initializeMethod.Assign(
             *   totalSize.GetReference(), 
             *   new BinaryOperationExpression(
             *      rowSize.GetReference(), 
             *      CodeBinaryOperatorType.Multiply, 
             *      table.RowCountField.GetReference()));*/
            var zeroCondition =
                readElementAtMethod
                .If(readMethodIndex.EqualTo(IntermediateGateway.NumberZero));
                    //new BinaryOperationExpression(
                    //    readMethodIndex.GetReference(),
                    //    CodeBinaryOperatorType.IdentityEquality,
                    //    IntermediateGateway.NumberZero));
            zeroCondition.Return(IntermediateGateway.NullValue);
            IExpression offsetExpression;
            IExpression offsetLeft = readMethodIndex.Subtract(1);//new BinaryOperationExpression(readMethodIndex.GetReference(), CodeBinaryOperatorType.Subtract, new PrimitiveExpression(1));

            if (Count > 0)
                offsetExpression = offsetLeft.Multiply(rowSize);//new BinaryOperationExpression(offsetLeft, CodeBinaryOperatorType.Multiply, rowSize.GetReference());
            else
                offsetExpression = offsetLeft.Multiply(stateSizeFields[0]);//new BinaryOperationExpression(offsetLeft, CodeBinaryOperatorType.Multiply, stateSizeFields[0].GetReference());
            readElementAtMethod.Call(
                table.StreamField.GetReference()
                .GetMethod("Seek")
                .Invoke(offsetField.Add(offsetExpression), typeof(SeekOrigin).GetTypeExpression().GetField("Begin")));//new BinaryOperationExpression(offsetField.GetReference(), CodeBinaryOperatorType.Add, offsetExpression), typeof(SeekOrigin).GetTypeExpression().GetField("Begin")));
            var createNewRow = table.DeclaredLockedTableRowClass.GetNewExpression();// new CreateNewObjectExpression(table.DeclaredLockedTableRowClass);
            var rowSizeProperty = table.DeclaredLockedTableRowClass.Properties.Add(typeof(int).WithName("Size"), true, false);
            rowSizeProperty.AccessLevel = AccessLevelModifiers.Public;
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
                createNewRow.Arguments.Add(table.LockedMetadataRootField.GetReference());
                var ctorStateParameter = table.DeclaredTableRowCtor.Parameters.Add(typeof(byte).WithName("state"));
                var ctorMetadataRootParameter = table.DeclaredTableRowCtor.Parameters.Add(metadataRoot.WithName("metadataRoot"));
                table.DeclaredTableRowCtor.Assign(table.RowLockedMetadataRootField.GetReference(), ctorMetadataRootParameter.GetReference());
                table.DeclaredTableRowCtor.Assign(rowStateField.GetReference(), ctorStateParameter.GetReference());
                var sizeSwitch = rowSizeProperty.GetMethod.Switch(rowStateField.GetReference());
                foreach (var state in stateSizeQuery)
                {
                    var stateList = (from s in state.Value
                                     where s > 0
                                     select s.ToPrimitive()).ToArray();
                    if (stateList.Length == 0)
                        continue;
                    var currentCase = sizeSwitch.Case(false, stateList);
                    currentCase.Return(state.Key.GetReference());
                }
            }
            else
            {
                table.RowLockedMetadataRootProperty.Parent.Properties.Remove(table.RowLockedMetadataRootProperty, false);
                table.RowLockedMetadataRootField.Parent.Fields.Remove(table.RowLockedMetadataRootField, false);
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

                //var groupDictionary = CreateGroupDictionary(dictionarySourceTable, dictionaryContainerTable, field.ImportName, sequentialSeriesData.TargetTable.ShortName, table.ShortName, indexName, field.ImportType);

                CreateOneToManyProperty(table, field, table, dictionarySourceTable);
            }

            foreach (var import in importReferences)
            {
                var importTable = import.DataType as MetadataTable;
                if (importTable != null)
                {
                    var importTableLookedUp = importTable.DeclaredLockedTableRowClass.Fields.Add(typeof(bool).WithName(string.Format("checked{0}", import.ImportName)));
                    importTableLookedUp.AccessLevel = AccessLevelModifiers.Private;
                    var importAltTarget = import.TargetField;
                    MetadataTable targetType = null;
                    if (importAltTarget != null)
                        targetType = import.TargetField.DataType as MetadataTable;
                    else
                        targetType = table;
                    var importTableField = importTable.DeclaredLockedTableRowClass.Fields.Add(new TypedName(Program.lowerFirst(import.ImportName), targetType.DeclaredLockedTableRowInterface));
                    importTableField.AccessLevel = AccessLevelModifiers.Private;
                    var importTableProperty = importTable.DeclaredLockedTableRowClass.Properties.Add(new TypedName(import.ImportName, targetType.DeclaredLockedTableRowInterface), true, false);
                    importTableField.SummaryText = string.Format("Data member for @s:{0};", importTableProperty.Name);
                    importTableProperty.SummaryText = import.ImportSummary;
                    importTableProperty.RemarksText = import.ImportRemarks;

                    importTableProperty.AccessLevel = AccessLevelModifiers.Public;
                    /* *
                    importTableProperty.GetMethod.If(
                       new BinaryOperationExpression(
                          importTableLookedUp.GetReference().Not(), 
                       CodeBinaryOperatorType.BooleanAnd, 
                       new BinaryOperationExpression(
                        importTableField.GetReference(), CodeBinaryOperatorType.IdentityEquality, IntermediateGateway.NullValue)));
                     * */
                    var importCheck = importTableProperty.GetMethod.If(
                        importTableLookedUp.GetReference()
                            .Not()
                        .LogicalAnd(importTableField.EqualTo(IntermediateGateway.NullValue)));
                    //var importCheck = importTableProperty.GetMethod.If(new BinaryOperationExpression(importTableLookedUp.GetReference().Not(), CodeBinaryOperatorType.BooleanAnd, new BinaryOperationExpression(importTableField.GetReference(), CodeBinaryOperatorType.IdentityEquality, IntermediateGateway.NullValue)));

                    var importCheckTable = importCheck.Locals.Add(new TypedName(string.Format("{0}Table", Program.lowerFirst(table.Name)), table.DeclaredLockedTableInterface));
                    importCheckTable.AutoDeclare = false;
                    importCheck.Add(importCheckTable.GetDeclarationStatement());
                    //importCheckTable.InitializationExpression = ;
                    importCheckTable.InitializationExpression = table.MetadataProperty.GetReference(table.RowLockedMetadataRootField.GetReference().GetProperty("TableStream"));
                    var importTableNullCheck = importCheck.If(
                        importCheckTable.InequalTo(IntermediateGateway.NullValue));
                        //new BinaryOperationExpression(
                        //    importCheckTable.GetReference(),
                        //    CodeBinaryOperatorType.IdentityInequality, 
                        //    IntermediateGateway.NullValue));
                    var iteration = importTableNullCheck.Enumerate(Program.lowerFirst(import.ImportName), importCheckTable.GetReference());//, table.DeclaredLockedTableRowInterface);
                    //iteration.Local.Name = Program.lowerFirst(import.ImportName);
                    var iterationIndexCheck =
                        iteration.If(
                            import.PropertyIndexReference
                            .GetReference(
                                iteration.Local.GetReference())
                            .EqualTo(new SpecialReferenceExpression(SpecialReferenceKind.This).GetProperty("Index")));
                        //iteration.If(
                        //    new BinaryOperationExpression(
                        //        import.PropertyIndexReference.GetReference(
                        //            iteration.Local.GetReference()),
                        //        CodeBinaryOperatorType.IdentityEquality, 
                        //        new SpecialReferenceExpression(SpecialReferenceKind.This).GetProperty("Index")));
                    if (importAltTarget != null)
                        iterationIndexCheck.Assign(importTableField.GetReference(), importAltTarget.PropertyReference.GetReference(iteration.Local.GetReference()));
                    else
                        iterationIndexCheck.Assign(importTableField.GetReference(), iteration.Local.GetReference());

                    iterationIndexCheck.Break();

                    //iteration.BreakLocal.AutoDeclare = false;
                    //iteration.ExitLabel.Skip = true;

                    importCheck.Assign(importTableLookedUp.GetReference(), IntermediateGateway.TrueValue);
                    table.DeclaredLockedTableRowClass.Properties.Remove(import.PropertyReference, false);
                    //import.PropertyIndexReference.AccessLevel = AccessLevelModifiers.Internal;
                    importTableProperty.GetMethod.Return(importTableField.GetReference());
                }
            }

            rowSizeProperty.GetMethod.Return(stateSizeFields[0].GetReference());
            if (this.Count > 0)
                initializeMethod.SummaryText = string.Format("Initializes the @s:{0}; with the @p:streamOffset;,{1} provided.", table.DeclaredTableClass.Name, sizes.ToString());
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
                            var currentLocals    = new Dictionary<MetadataTableField, ITypedLocalMember>();
                            var currentTagLocals = new Dictionary<MetadataTableField,ITypedLocalMember>();
                            foreach (var item in set)
                            {
                                var encodedField = item as IMetadataTableEncodedField;
                                var currentLocal = readElementAtMethod.Locals.Add(typeof(uint).WithName(string.Format("{0}Index", Program.lowerFirst(item.FieldName))));
                                currentLocal.AutoDeclare = false;
                                readElementAtMethod.Add(currentLocal.GetDeclarationStatement());
                                currentLocals.Add(item, currentLocal);
                                var currentEncodedLocal = readElementAtMethod.Locals.Add(encoding.EncodingType.WithName(Program.lowerFirst(encodedField.EncodingIdName)));
                                currentEncodedLocal.AutoDeclare = false;
                                readElementAtMethod.Add(currentEncodedLocal.GetDeclarationStatement());
                                currentTagLocals.Add(item, currentEncodedLocal);
                            }
                            var stateSwitch = readElementAtMethod.Switch(stateField.GetReference());
                            IExpression[] currentCases = new IExpression[stateWordSizeQuery.Length];
                            for (int i = 0; i < stateWordSizeQuery.Length; i++)
                                currentCases[i] = stateWordSizeQuery[i].ToPrimitive();// new PrimitiveExpression(stateWordSizeQuery[i]);
                            var dwordCase = stateSwitch.Case(false, currentCases);
                            var wordCase = stateSwitch.Case(true);
                            var mask = (1 << encoding.BitEncodingSize) - 1;
                            foreach (var item in set)
                            {
                                var encodedField = item as IMetadataTableEncodedField;
                                var currentLocal = currentLocals[item];
                                var currentEncodedLocal = currentTagLocals[item];
                                dwordCase.Assign(currentLocal.GetReference(), table.ReaderField.GetReference().GetMethod("ReadUInt32").Invoke());
                                wordCase.Assign(currentLocal.GetReference(), table.ReaderField.GetReference().GetMethod("ReadUInt16").Invoke());
                                readElementAtMethod.Assign(
                                    currentEncodedLocal,
                                    currentLocal
                                    .BitwiseAnd(
                                        mask.ToPrimitive())
                                    .Cast(currentEncodedLocal.LocalType));
                                //readElementAtMethod.Assign(
                                //    currentEncodedLocal.GetReference(),
                                //    new BinaryOperationExpression(
                                //        currentLocal.GetReference(),
                                //        CodeBinaryOperatorType.BitwiseAnd, 
                                //        new PrimitiveExpression(mask))
                                //    .Cast(
                                //        currentEncodedLocal.LocalType));
                                readElementAtMethod.Assign(currentLocal.GetReference(), Program.MakeRightShiftCall(currentLocal.GetReference(), ((int)encoding.BitEncodingSize).ToPrimitive()));//new PrimitiveExpression((int) encoding.BitEncodingSize)));
                                createNewRow.Arguments.Add(currentEncodedLocal.GetReference());
                                createNewRow.Arguments.Add(currentLocal.GetReference());
                                var currentEncodedParameter = table.DeclaredTableRowCtor.Parameters.Add(new TypedName(currentEncodedLocal.Name, currentEncodedLocal.LocalType));
                                var currentParameter = table.DeclaredTableRowCtor.Parameters.Add(new TypedName(currentLocal.Name, currentLocal.LocalType));
                                table.DeclaredTableRowCtor.Assign(encodedField.EncodedField.GetReference(), currentEncodedParameter.GetReference());
                                table.DeclaredTableRowCtor.Assign(item.FieldReference.GetReference(), currentParameter.GetReference());
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
                            var currentLocals = new Dictionary<MetadataTableField, ITypedLocalMember>();
                            foreach (var item in set)
                            {
                                var currentLocal = readElementAtMethod.Locals.Add(typeof(uint).WithName(string.Format("{0}Index", Program.lowerFirst(item.FieldName))));
                                currentLocal.AutoDeclare = false;
                                readElementAtMethod.Add(currentLocal.GetDeclarationStatement());
                                currentLocals.Add(item, currentLocal);
                            }
                            var stateSwitch = readElementAtMethod.Switch(stateField.GetReference());
                            IExpression[] currentCases = new IExpression[stateWordSizeQuery.Length];
                            for (int i = 0; i < stateWordSizeQuery.Length; i++)
                                currentCases[i] = stateWordSizeQuery[i].ToPrimitive();// new PrimitiveExpression(stateWordSizeQuery[i]);
                            var dwordCase = stateSwitch.Case(false, currentCases);
                            var wordCase = stateSwitch.Case(true);
                            foreach (var item in set)
                            {
                                var currentLocal = currentLocals[item];
                                dwordCase.Assign(currentLocal.GetReference(), table.ReaderField.GetReference().GetMethod("ReadUInt32").Invoke());
                                wordCase.Assign(currentLocal.GetReference(), table.ReaderField.GetReference().GetMethod("ReadUInt16").Invoke());
                                createNewRow.Arguments.Add(currentLocal.GetReference());
                                var currentParameter = table.DeclaredTableRowCtor.Parameters.Add(new TypedName(currentLocal.Name, currentLocal.LocalType));
                                table.DeclaredTableRowCtor.Assign(item.FieldReference.GetReference(), currentParameter.GetReference());
                            }
                            dwordCase.Break();
                            wordCase.Break();
                        }
                        break;
                    case FieldDataKind.DataType:
                        var regularDataType = dataType as MetadataTableTypeDataType;
                        foreach (var item in set)
                        {
                            var currentLocal = readElementAtMethod.Locals.Add(new TypedName(Program.lowerFirst(item.FieldName), regularDataType.DataType));
                            currentLocal.AutoDeclare = false;
                            readElementAtMethod.Add(currentLocal.GetDeclarationStatement());
                            currentLocal.InitializationExpression = table.ReaderField.GetReference().GetMethod(string.Format("Read{0}", regularDataType.DataType.Name /* ((IExternTypeReference) regularDataType.DataType).TypeInstance.Type.Name)*/)).Invoke();
                            createNewRow.Arguments.Add(currentLocal.GetReference());
                            var currentParameter = table.DeclaredTableRowCtor.Parameters.Add(new TypedName(currentLocal.Name, currentLocal.LocalType));
                            table.DeclaredTableRowCtor.Assign(item.FieldReference.GetReference(), currentParameter.GetReference());
                        }
                        break;
                    case FieldDataKind.SelfsufficientDataType:
                        var selfSuffDataType = dataType as MetadataTableTypeDataType;
                        foreach (var item in set)
                        {
                            var currentLocal = readElementAtMethod.Locals.Add(new TypedName(Program.lowerFirst(item.FieldName), selfSuffDataType.DataType));
                            currentLocal.InitializationExpression = selfSuffDataType.DataType.GetNewExpression();// new CreateNewObjectExpression(selfSuffDataType.DataType);
                            currentLocal.AutoDeclare = false;
                            readElementAtMethod.Add(currentLocal.GetDeclarationStatement());
                            readElementAtMethod.Call(currentLocal.GetReference().GetMethod("Read").Invoke(table.ReaderField.GetReference()));
                            createNewRow.Arguments.Add(currentLocal.GetReference());
                            var currentParameter = table.DeclaredTableRowCtor.Parameters.Add(new TypedName(currentLocal.Name, currentLocal.LocalType));
                            table.DeclaredTableRowCtor.Assign(item.FieldReference.GetReference(), currentParameter.GetReference());
                        }
                        break;
                    case FieldDataKind.CastDataType:
                        var castDataType = dataType as MetadataTableTypeDataType;
                        foreach (var item in set)
                        {
                            var currentLocal = readElementAtMethod.Locals.Add(new TypedName(Program.lowerFirst(item.FieldName), castDataType.DataType));
                            currentLocal.AutoDeclare = false;
                            readElementAtMethod.Add(currentLocal.GetDeclarationStatement());
                            currentLocal.InitializationExpression = table.ReaderField.GetReference().GetMethod(string.Format("Read{0}", castDataType.CastType.Name /* ((IExternTypeReference) (castDataType.CastType)).TypeInstance.Type.Name*/)).Invoke().Cast(castDataType.DataType);
                            createNewRow.Arguments.Add(currentLocal.GetReference());
                            var currentParameter = table.DeclaredTableRowCtor.Parameters.Add(new TypedName(currentLocal.Name, currentLocal.LocalType));
                            table.DeclaredTableRowCtor.Assign(item.FieldReference.GetReference(), currentParameter.GetReference());
                        }
                        break;
                }
            }
            var fullyRead = table.DeclaredTableClass.Fields.Add(typeof(bool).WithName("fullyRead"));
            fullyRead.AccessLevel = AccessLevelModifiers.Private;
            var gotLockCondition = readElementAtMethod.If(readElementAtGotLock.GetReference());
            gotLockCondition.Call(monitorRef.GetMethod("Exit"), table.SyncField.GetReference());

            var readFull = table.DeclaredTableClass.Methods.Add(typeof(void).WithName("Read"));
            var fullyReadCondition = readFull.If(fullyRead.GetReference());
            fullyReadCondition.Return();
            var readFullRowIndex = readFull.Locals.Add(typeof(uint).WithName("index"));
            readFullRowIndex.AutoDeclare = false;
            readFullRowIndex.InitializationExpression = 1.ToPrimitive();
            var readFullIteration =
                readFull.Iterate(
                readFullRowIndex.GetDeclarationStatement(),
                readFullRowIndex.LessThanOrEqualTo(
                    new SpecialReferenceExpression(SpecialReferenceKind.This)
                    .GetProperty("Count")),
                new IStatementExpression[1] { readFullRowIndex.Increment() });
                //readFull.Iterate(
                //    readFullRowIndex.GetDeclarationStatement(),
                //    new CrementStatement(
                //        CrementType.Postfix, 
                //        CrementOperation.Increment,
                //        readFullRowIndex.GetReference()),
                //    new BinaryOperationExpression(
                //        readFullRowIndex.GetReference(),
                //        CodeBinaryOperatorType.LessThanOrEqual,
                //        new SpecialReferenceExpression(SpecialReferenceKind.This).GetProperty("Count")));
            readFullIteration
                .Call(
                    monitorRef
                    .GetMethod("Enter")
                    .Invoke(
                        table.SyncField
                        .GetReference(),
                        readElementAtGotLock
                        .GetReference()
                        .Direct(
                            ParameterCoercionDirection.Reference)));
            //readFullIteration
            //    .Call(
            //        monitorRef
            //        .GetMethod("Enter")
            //        .Invoke(
            //            table.SyncField.GetReference(),
            //            new DirectionExpression(
            //                FieldDirection.Ref, 
            //                readElementAtGotLock.GetReference())));
            var readFullIterationSkipCheck = readFullIteration.If(new SpecialReferenceExpression(SpecialReferenceKind.Base).GetMethod("ItemLoaded").Invoke(readFullRowIndex.GetReference()).Not());
            var readFullGotLock = readFullIteration.Locals.Add(typeof(bool).WithName("gotLock"));
            readFullGotLock.InitializationExpression = IntermediateGateway.FalseValue;

            offsetLeft = readMethodIndex.Subtract(1);
                //new BinaryOperationExpression(
                //    readMethodIndex.GetReference(), 
                //    CodeBinaryOperatorType.Subtract, 
                //    new PrimitiveExpression(1));
            if (Count > 0)
                offsetExpression =
                    readFullRowIndex.Subtract(1).Multiply(rowSize);
            //new BinaryOperationExpression(
            //    new BinaryOperationExpression(
            //        readFullRowIndex.GetReference(),
            //        CodeBinaryOperatorType.Subtract, 
            //        1.ToPrimitive()),
            //    CodeBinaryOperatorType.Multiply,
            //    rowSize.GetReference());
            else
                offsetExpression =
                    readFullRowIndex
                    .Subtract(1)
                    .Multiply(stateSizeFields[0]);
                    //new BinaryOperationExpression(
                    //    new BinaryOperationExpression(
                    //        readFullRowIndex.GetReference(),
                    //        CodeBinaryOperatorType.Subtract, 1.ToPrimitive()),
                    //        CodeBinaryOperatorType.Multiply,
                    //        stateSizeFields[0].GetReference());

            readFull.AccessLevel = AccessLevelModifiers.Public;
            readFull.IsOverride = true;
            readFullIterationSkipCheck.Call(
                table.StreamField
                .GetReference()
                .GetMethod("Seek")
                .Invoke(
                    offsetField.Add(offsetExpression),
                    typeof(SeekOrigin)
                    .GetTypeExpression()
                    .GetField("Begin")));
            //readFullIterationSkipCheck.Call(
            //    table.StreamField
            //    .GetReference()
            //    .GetMethod("Seek")
            //    .Invoke(
            //        new BinaryOperationExpression(
            //            offsetField.GetReference(),
            //            CodeBinaryOperatorType.Add, 
            //            offsetExpression), 
            //        typeof(SeekOrigin)
            //        .GetTypeExpression()
            //        .GetField("Begin")));
            foreach (var statement in readElementAtMethod.Skip(3))
                readFullIterationSkipCheck.Add(statement);
            readFullIterationSkipCheck.Remove(readFullIterationSkipCheck[readFullIterationSkipCheck.Count - 1]);
            gotLockCondition = readFullIteration.If(readElementAtGotLock.GetReference());
            gotLockCondition.Call(monitorRef.GetMethod("Exit"), table.SyncField.GetReference());
            
            readFullIterationSkipCheck.Call(new SpecialReferenceExpression(SpecialReferenceKind.Base).GetMethod("InjectLoadedItem").Invoke(createNewRow, readFullRowIndex.GetReference()));
            readFull.Assign(fullyRead.GetReference(), IntermediateGateway.TrueValue);
            readElementAtMethod.Return(createNewRow);
        }

        private static void CreateOneToManyProperty(MetadataTable table, MetadataTableField field, MetadataTable listTable, MetadataTable dictionarySourceTable)
        {
            var cov = typeof(CovariantReadOnlyCollection<,>).GetTypeReference<IClassType>().MakeGenericClosure(dictionarySourceTable.DeclaredLockedTableRowInterface, dictionarySourceTable.DeclaredMutableTableRowInterface);
            var listLockedField = table.DeclaredLockedTableRowClass.Fields.Add(new TypedName(Program.lowerFirst(field.ImportName), typeof(IControlledCollection<>).GetTypeReference<IInterfaceType>(dictionarySourceTable.DeclaredLockedTableRowInterface)));
            var listMutableField = table.DeclaredMutableTableRowClass.Fields.Add(new TypedName(Program.lowerFirst(field.ImportName), typeof(Collection<>).GetTypeReference<IClassType>(dictionarySourceTable.DeclaredMutableTableRowInterface)));
            var listMutableLockedField = table.DeclaredMutableTableRowClass.Fields.Add(new TypedName(string.Format("_{0}", Program.lowerFirst(field.ImportName)), cov));
            var listLockedProperty = table.DeclaredLockedTableRowClass.Properties.Add(new TypedName(field.ImportName, listLockedField.FieldType), true, false);
            var listMutableProperty = table.DeclaredMutableTableRowClass.Properties.Add(new TypedName(field.ImportName, listMutableField.FieldType), true, false);
            var listMutableLockedProperty = table.DeclaredMutableTableRowClass.Properties.Add(new TypedName(field.ImportName + "{", listLockedField.FieldType), true, false);
            listLockedField.AccessLevel = AccessLevelModifiers.Private;
            listMutableField.AccessLevel = AccessLevelModifiers.Private;
            listMutableLockedField.AccessLevel = AccessLevelModifiers.Private;

            listMutableLockedProperty.Implementations.Add(table.DeclaredLockedTableRowInterface);
            //listMutableLockedProperty.PrivateImplementationTarget = table.DeclaredLockedTableRowInterface;
            listMutableLockedProperty.Name = field.ImportName;
            var lockedMutablePropCheck =
                listMutableLockedProperty.GetMethod
                .If(listMutableLockedField.EqualTo(IntermediateGateway.NullValue));
                //.If(new BinaryOperationExpression(
                //        listMutableLockedField.GetReference(),
                //        CodeBinaryOperatorType.IdentityEquality,
                //        IntermediateGateway.NullValue));
            lockedMutablePropCheck.Assign(listMutableLockedField.GetReference(), cov.GetNewExpression(typeof(ControlledCollection<>).GetTypeReference<IClassType>(dictionarySourceTable.DeclaredMutableTableRowInterface).GetNewExpression(listMutableProperty.GetReference())));
            listMutableLockedProperty.GetMethod.Return(listMutableLockedField.GetReference());

            var mutablePropCheck =
                listMutableProperty
                .GetMethod
                .If(listMutableField.EqualTo(IntermediateGateway.NullValue));
                //.If(new BinaryOperationExpression(
                //        listMutableField.GetReference(),
                //        CodeBinaryOperatorType.IdentityEquality,
                //        IntermediateGateway.NullValue));

            mutablePropCheck.Assign(listMutableField.GetReference(), listMutableField.FieldType.GetNewExpression(new ExpressionCollection()));
            listMutableProperty.GetMethod.Return(listMutableField.GetReference());
            listMutableProperty.SummaryText = field.ImportSummary;
            listMutableProperty.RemarksText = field.ListRemarks;
            listMutableProperty.AccessLevel = AccessLevelModifiers.Public;
            listMutableProperty.SummaryText = string.Format("Returns {0}", Program.lowerFirst(field.ImportSummary == null ? field.SummaryText : field.ImportSummary));

            listLockedProperty.SummaryText = field.ImportSummary;
            listLockedProperty.RemarksText = field.ListRemarks;
            listLockedProperty.AccessLevel = AccessLevelModifiers.Public;
            listLockedProperty.SummaryText = string.Format("Returns {0}", Program.lowerFirst(field.ImportSummary == null ? field.SummaryText : field.ImportSummary));
            field.PropertyIndexReference.SummaryText = string.Format("Returns the @s:UInt32; value which determines the index of the first @s:{0}; within @s:{1};", field.PropertyReference.PropertyType.BuildTypeName(typeParameterDisplayMode: TypeParameterDisplayMode.CommentStandard)/*.ToString(new IntermediateCodeTranslatorOptions(true))*/, listLockedProperty.Name);
            listLockedField.SummaryText = string.Format("Data member for @s:{0};", listLockedProperty.Name);
            var listNullCheck =
                listLockedProperty
                .GetMethod
                .If(listLockedField.EqualTo(IntermediateGateway.NullValue));
                //.If(new BinaryOperationExpression(
                //    listLockedField.GetReference(),
                //    CodeBinaryOperatorType.IdentityEquality,
                //    IntermediateGateway.NullValue));
            if (field.TargetField != null)
                field.TargetField.PropertyReference.Parent.Properties.Remove(field.TargetField.PropertyReference.UniqueIdentifier, false);
            if (field.ImportType == MetadataTableFieldImportKind.OneToSequentialManyImported)
            {
                var currentEntry = listNullCheck.Locals.Add(new TypedName(string.Format("current{0}", listTable.ShortName), listTable.DeclaredLockedTableRowInterface));
                currentEntry.AutoDeclare = false;
                currentEntry.InitializationExpression = IntermediateGateway.NullValue;
                listNullCheck.Add(currentEntry.GetDeclarationStatement());
                var tableRef = listNullCheck.Locals.Add(new TypedName(string.Format("{0}Table", Program.lowerFirst(listTable.ShortName)), listTable.DeclaredLockedTableInterface));
                tableRef.AutoDeclare = false;
                tableRef.InitializationExpression = listTable.MetadataProperty.GetReference(table.RowLockedMetadataRootField.GetReference().GetProperty("TableStream"));
                listNullCheck.Add(tableRef.GetDeclarationStatement());
                var tableNullCheck = 
                    listNullCheck
                    .If(
                        tableRef.EqualTo(IntermediateGateway.NullValue));
                        //new BinaryOperationExpression(
                        //    tableRef.GetReference(),
                        //    CodeBinaryOperatorType.IdentityEquality,
                        //    IntermediateGateway.NullValue));
                tableNullCheck.Return(IntermediateGateway.NullValue);
                var importSetEnumeration = listNullCheck.Enumerate(string.Format(Program.lowerFirst(listTable.ShortName)), tableRef.GetReference());//, listTable.DeclaredLockedTableRowInterface);
                //importSetEnumeration.Local.Name = string.Format(Program.lowerFirst(listTable.ShortName));
                var importFind =
                    importSetEnumeration.If(
                        new SpecialReferenceExpression(SpecialReferenceKind.This).GetProperty("Index")
                        .EqualTo(
                            field.PropertyIndexReference.GetReference(
                                importSetEnumeration.Local.GetReference())));
                    //importSetEnumeration.If(
                    //new BinaryOperationExpression(
                    //        new SpecialReferenceExpression(SpecialReferenceKind.This).GetProperty("Index"),
                    //        CodeBinaryOperatorType.IdentityEquality, 
                    //        field.PropertyIndexReference.GetReference(
                    //            importSetEnumeration.Local.GetReference())));
                importFind.Assign(currentEntry.GetReference(), importSetEnumeration.Local.GetReference());
                importFind.Break();
                //importSetEnumeration.ExitLabel.Skip = true;
                //importSetEnumeration.BreakLocal.AutoDeclare = false;
                var dictType = typeof(CliMetadataLazySequentialSet<>).GetTypeReference<IClassType>(dictionarySourceTable.DeclaredLockedTableRowInterface);

                var currentNullCheck =
                    listNullCheck.If(currentEntry.EqualTo(IntermediateGateway.NullValue));
                    //listNullCheck.If(
                    //    new BinaryOperationExpression(
                    //        currentEntry.GetReference(),
                    //        CodeBinaryOperatorType.IdentityEquality, 
                    //        IntermediateGateway.NullValue));
                currentNullCheck.Assign(
                    listLockedField.GetReference(), 
                    dictType.GetNewExpression(
                        IntermediateGateway.NumberZero, 
                        IntermediateGateway.NumberZero,
                        dictionarySourceTable.MetadataProperty.GetReference(
                            table.RowLockedMetadataRootField
                            .GetReference()
                            .GetProperty("TableStream"))));

                var currentBlock = currentNullCheck.Next;
                string listSingleItem = (field.ResultedListElementName != null) ? field.ResultedListElementName : field.PropertyReference.Name;

                var nextEntry = currentBlock.Locals.Add(new TypedName(string.Format("next{0}", listTable.ShortName), listTable.DeclaredLockedTableRowInterface));
                nextEntry.AutoDeclare = false;
                currentBlock.Add(nextEntry.GetDeclarationStatement());
                //dictionarySourceTable.DeclaredLockedTableRowInterface
                nextEntry.InitializationExpression = listTable.MetadataProperty.GetReference(table.RowLockedMetadataRootField.GetReference().GetProperty("TableStream")).GetIndexer(currentEntry.GetReference().GetProperty("Index").Add(1).Cast(typeof(int)));
                var lastItemIndex = currentBlock.Locals.Add(typeof(uint).WithName(string.Format("last{0}Index", listSingleItem)));
                lastItemIndex.AutoDeclare = false;
                currentBlock.Add(lastItemIndex.GetDeclarationStatement());
                var nextNullCheck = currentBlock.If(nextEntry.EqualTo(IntermediateGateway.NullValue));//new BinaryOperationExpression(nextEntry.GetReference(), CodeBinaryOperatorType.IdentityEquality, IntermediateGateway.NullValue));
                nextNullCheck.Assign(lastItemIndex.GetReference(), dictionarySourceTable.MetadataProperty.GetReference(table.RowLockedMetadataRootField.GetReference().GetProperty("TableStream")).GetProperty("Count").Add(1).Cast(typeof(uint).GetTypeReference()));
                //nextNullCheck.Assign(lastItemIndex.GetReference(), field.TargetField.PropertyIndexReference.GetReference(currentEntry.GetReference()));
                nextNullCheck.Next.Assign(lastItemIndex.GetReference(), field.TargetField.PropertyIndexReference.GetReference(nextEntry.GetReference()));
                currentBlock.Assign(listLockedField.GetReference(), dictType.GetNewExpression(field.TargetField.PropertyIndexReference.GetReference(currentEntry.GetReference()), lastItemIndex.GetReference(), dictionarySourceTable.MetadataProperty.GetReference(table.RowLockedMetadataRootField.GetReference().GetProperty("TableStream"))));
                //currentBlock.Assign(listLockedField.GetReference(), new CreateNewObjectExpression(groupDictionary, new SpecialReferenceExpression(SpecialReferenceKind.This), field.TargetField.PropertyIndexReference.GetReference(currentEntry.GetReference()), new BinaryOperationExpression(lastItemIndex.GetReference(), CodeBinaryOperatorType.Subtract, field.TargetField.PropertyIndexReference.GetReference(currentEntry.GetReference())).Cast(typeof(int))));
                listLockedProperty.GetMethod.Return(listLockedField.GetReference());
            }
            else
            {
                var nextEntry = listNullCheck.Locals.Add(new TypedName(string.Format("next{0}", listTable.ShortName), table.DeclaredLockedTableRowInterface));
                nextEntry.AutoDeclare = false;
                listNullCheck.Add(nextEntry.GetDeclarationStatement());
                var lastItemIndex = listNullCheck.Locals.Add(new TypedName(string.Format("last{0}", (field.ResultedListElementName != null) ? field.ResultedListElementName : field.PropertyIndexReference.Name), typeof(uint).GetTypeReference()));
                lastItemIndex.AutoDeclare = false;
                listNullCheck.Add(lastItemIndex.GetDeclarationStatement());

                nextEntry.InitializationExpression = table.MetadataProperty.GetReference(table.RowLockedMetadataRootField.GetReference().GetProperty("TableStream")).GetIndexer(new SpecialReferenceExpression(SpecialReferenceKind.This).GetProperty("Index").Add(1).Cast(typeof(int)));
                var tableRef = listNullCheck.Locals.Add(new TypedName(string.Format("{0}Table", Program.lowerFirst(dictionarySourceTable.ShortName)), dictionarySourceTable.DeclaredLockedTableInterface));
                tableRef.AutoDeclare = false;
                listNullCheck.Add(tableRef.GetDeclarationStatement());
                var nextNullCheck = listNullCheck.If(nextEntry.InequalTo(IntermediateGateway.NullValue));
                //new BinaryOperationExpression(nextEntry.GetReference(), CodeBinaryOperatorType.IdentityInequality, IntermediateGateway.NullValue));
                nextNullCheck.Assign(lastItemIndex.GetReference(), field.PropertyIndexReference.GetReference(nextEntry.GetReference()));
                tableRef.InitializationExpression = dictionarySourceTable.MetadataProperty.GetReference(table.RowLockedMetadataRootField.GetReference().GetProperty("TableStream"));
                var tableNullCheck = nextNullCheck.Next.If(tableRef.InequalTo(IntermediateGateway.NullValue));//new BinaryOperationExpression(tableRef.GetReference(), CodeBinaryOperatorType.IdentityInequality, IntermediateGateway.NullValue));
                tableNullCheck.Assign(lastItemIndex.GetReference(), tableRef.GetReference().GetProperty("Count").Add(1).Cast(typeof(uint)));
                tableNullCheck.Next.Assign(lastItemIndex.GetReference(), field.PropertyIndexReference.GetReference());
                var dictType = typeof(CliMetadataLazySequentialSet<>).GetTypeReference<IClassType>().MakeGenericClosure(dictionarySourceTable.DeclaredLockedTableRowInterface);

                listNullCheck.Assign(listLockedField.GetReference(), dictType.GetNewExpression(field.PropertyIndexReference.GetReference(), lastItemIndex.GetReference(), tableRef.GetReference()));
                //listNullCheck.Assign(listLockedField.GetReference(), new CreateNewObjectExpression(dictType, field.PropertyIndexReference.GetReference(), new BinaryOperationExpression(lastItemIndex.GetReference(), CodeBinaryOperatorType.Subtract, field.PropertyIndexReference.GetReference()).Cast(typeof(int))));
                listLockedProperty.GetMethod.Return(listLockedField.GetReference());
            }
            listTable.DeclaredLockedTableRowClass.Properties.Remove((IGeneralMemberUniqueIdentifier)field.PropertyReference.UniqueIdentifier, false);
        }

        private static void CreateOneToManyProperty(MetadataTableField field, MetadataTable dictionarySourceTable, MetadataTable dictionaryContainerTable, string listName, string targetListElementName, string indexName, MetadataTable listTable)
        {

            var dictType = typeof(CliMetadataLazySet<>).GetTypeReference<IClassType>(dictionarySourceTable.DeclaredLockedTableRowInterface);// (new TypeReferenceCollection(dictionarySourceTable.DeclaredLockedTableRowInterface));
            var lockedFieldType = typeof(IControlledCollection<>).GetTypeReference<IInterfaceType>(dictionarySourceTable.DeclaredLockedTableRowInterface);// (new TypeReferenceCollection(dictionarySourceTable.DeclaredLockedTableRowInterface));
            var mutableFieldType = typeof(Collection<>).GetTypeReference<IClassType>(dictionarySourceTable.DeclaredLockedTableRowInterface);// (new TypeReferenceCollection(dictionarySourceTable.DeclaredLockedTableRowInterface));
            var currentListHolderField = dictionaryContainerTable.DeclaredLockedTableRowClass.Fields.Add(new TypedName(Program.lowerFirst(listName), lockedFieldType));
            currentListHolderField.AccessLevel = AccessLevelModifiers.Private;

            var currentListHolderProperty = dictionaryContainerTable.DeclaredLockedTableRowClass.Properties.Add(new TypedName(listName, lockedFieldType), true, false);
            currentListHolderField.SummaryText = string.Format("Data member for @s:{0};.", currentListHolderProperty.Name);
            if (string.IsNullOrEmpty(field.ImportSummary))
                currentListHolderProperty.SummaryText = string.Format("Returns a set of @s:{0}; which are associated to the @s:{1};.", dictionarySourceTable.DeclaredLockedTableRowInterface.Name, dictionaryContainerTable.DeclaredLockedTableRowClass.Name);
            else
                currentListHolderProperty.SummaryText = field.ImportSummary;
            currentListHolderProperty.RemarksText = string.Format("Created through references from the @s:{0};.", listTable.DeclaredLockedTableInterface.Name);
            currentListHolderProperty.AccessLevel = AccessLevelModifiers.Public;
            var currentListHolderNullCondition = 
                currentListHolderProperty
                .GetMethod
                .If(currentListHolderField.EqualTo(IntermediateGateway.NullValue));
                    //new BinaryOperationExpression(
                    //    currentListHolderField.GetReference(),
                    //    CodeBinaryOperatorType.IdentityEquality,
                    //    IntermediateGateway.NullValue));
            var currentListHolderLocalList = currentListHolderNullCondition.Locals.Add(new TypedName(Program.lowerFirst(listName), typeof(List<uint>).GetTypeReference()));
            currentListHolderLocalList.InitializationExpression = currentListHolderLocalList.LocalType.GetNewExpression();// new CreateNewObjectExpression();
            currentListHolderLocalList.AutoDeclare = false;
            var currentListHolderTableRef = currentListHolderNullCondition.Locals.Add(string.Format("{0}Table", Program.lowerFirst(listName)), listTable.MetadataProperty.GetReference(dictionaryContainerTable.RowLockedMetadataRootField.GetReference().GetProperty("TableStream")));//new TypedName(string.Format("{0}Table", Program.lowerFirst(listName)), listTable.DeclaredLockedTableInterface));
            //currentListHolderTableRef.InitializationExpression = ;
            var holderTableRefNullCheck = currentListHolderNullCondition
                .If(currentListHolderTableRef.InequalTo(IntermediateGateway.NullValue));
                    //new BinaryOperationExpression(
                    //    currentListHolderTableRef.GetReference(),
                    //    CodeBinaryOperatorType.IdentityInequality,
                    //    IntermediateGateway.NullValue));

            holderTableRefNullCheck.Add(currentListHolderLocalList.GetDeclarationStatement());

            var enumeration = holderTableRefNullCheck.Enumerate(Program.lowerFirst(targetListElementName), currentListHolderTableRef.GetReference());//, listTable.DeclaredLockedTableRowInterface);
            //enumeration.Local.Name = Program.lowerFirst(targetListElementName);
            IBreakableConditionBlockStatement enumerationReferenceCheck;

            if (field.DataType.DataKind == FieldDataKind.Encoding)
            {
                var encoding = field.DataType as IMetadataTableFieldEncodingDataType;
                var encodingSelection = (from etP in encoding.Values
                                         where etP.Item2 == dictionaryContainerTable
                                         select etP.Item1).First();
                var encodedField = field as IMetadataTableEncodedField;
                if (encodedField is IMetadataTableFilteredEncodedField)
                {
                    var filteredEncodedField = (IMetadataTableFilteredEncodedField)encodedField;
                    currentListHolderTableRef.InitializationExpression = dictionaryContainerTable.RowLockedMetadataRootField.GetReference().GetProperty(string.Format("{0}{1}", dictionaryContainerTable.Name, filteredEncodedField.FilteredSuffix));
                    enumerationReferenceCheck =
                        enumeration.If(
                            field.PropertyIndexReference.GetReference(enumeration.Local.GetReference())
                            .EqualTo(
                                new SpecialReferenceExpression(SpecialReferenceKind.This)
                                .GetProperty("Index")));
                }
                else
                {
                    enumerationReferenceCheck =
                        enumeration.If(
                            encodedField.EncodingProperty.EqualTo(encodingSelection)
                            .LogicalAnd(field.PropertyIndexReference.GetReference(enumeration.Local.GetReference())
                            .EqualTo(
                                new SpecialReferenceExpression(SpecialReferenceKind.This)
                                .GetProperty("Index"))));
                }
                enumerationReferenceCheck.CreateNext(field.PropertyIndexReference.GetReference(enumeration.Local.GetReference()).GreaterThan(new SpecialReferenceExpression(SpecialReferenceKind.This)
                                .GetProperty("Index")));
                enumerationReferenceCheck.Next.Break();
                    //.If(
                    //    new BinaryOperationExpression(
                    //        new BinaryOperationExpression(
                    //            encodedField.EncodingProperty.GetReference(
                    //                enumeration.Local.GetReference()),
                    //                CodeBinaryOperatorType.IdentityEquality,
                    //                encodingSelection),
                    //            CodeBinaryOperatorType.BooleanAnd,
                    //            new BinaryOperationExpression(
                    //                field.PropertyIndexReference.GetReference(
                    //                    enumeration.Local.GetReference()),
                    //                    CodeBinaryOperatorType.IdentityEquality,
                    //                    new SpecialReferenceExpression(SpecialReferenceKind.This)
                    //                    .GetProperty("Index"))));
                if (field.PropertyIndexReference != null)
                    field.PropertyIndexReference.SummaryText = string.Format("Returns the decoded index of the {0} relative to the appropriate table.", field.PropertyReference.Name);
                if (dictionarySourceTable.DeclaredLockedTableRowClass.Properties.ContainsKey(field.PropertyReference.UniqueIdentifier))
                    dictionarySourceTable.DeclaredLockedTableRowClass.Properties.Remove(field.PropertyReference.UniqueIdentifier, false);
            }
            else
            {
                enumerationReferenceCheck = 
                    enumeration
                    .If(enumeration.Local.GetReference().GetProperty(field.PropertyIndexReference.Name).EqualTo(new SpecialReferenceExpression(SpecialReferenceKind.This)
                                .GetProperty("Index")));
                        //new BinaryOperationExpression(
                        //    enumeration.Local
                        //    .GetReference()
                        //    .GetProperty(
                        //        field.PropertyIndexReference.Name),
                        //        CodeBinaryOperatorType.IdentityEquality,
                        //        new SpecialReferenceExpression(SpecialReferenceKind.This)
                        //        .GetProperty("Index")));
            }
            enumerationReferenceCheck.Call(currentListHolderLocalList.GetReference().GetMethod("Add").Invoke(enumeration.Local.GetReference().GetProperty(indexName)));

            holderTableRefNullCheck.Assign(currentListHolderField.GetReference(), dictType.GetNewExpression(currentListHolderLocalList.GetReference().GetMethod("ToArray").Invoke(), dictionarySourceTable.MetadataProperty.GetReference(dictionaryContainerTable.RowLockedMetadataRootField.GetReference().GetProperty("TableStream"))));
            //currentListHolderNullCondition.Assign(currentListHolderCheck.GetReference(), IntermediateGateway.TrueValue);
            currentListHolderProperty.GetMethod.Return(currentListHolderField.GetReference());
        }
        /*
        private static IClassType CreateGroupDictionary(MetadataTable dictionarySourceTable, MetadataTable dictionaryContainerTable, string listName, string elementName, string containerName, string indexName, MetadataTableFieldImportKind listKind = MetadataTableFieldImportKind.ManyToOneImport)
        {
            IType listType = dictionarySourceTable.DeclaredLockedTableRowInterface;
            var groupDictionary = dictionaryContainerTable.DeclaredLockedTableRowClass.Partials.Add().Classes.Add(string.Format("Lazy{0}", listName));
            groupDictionary.ImplementsList.Add(typeof(IControlledCollection<>).GetTypeReference(new TypeReferenceCollection(listType)));
            groupDictionary.ImplementsList.Add(typeof(IControlledCollection).GetTypeReference());

            var groupCtor = groupDictionary.Constructors.Add();
            groupCtor.AccessLevel = AccessLevelModifiers.Internal;
            var groupCtorOwnerParameter = groupCtor.Parameters.Add(new TypedName(Program.lowerFirst(containerName), dictionaryContainerTable.DeclaredLockedTableRowClass));
            var groupOwnerRef = groupDictionary.Fields.Add(new TypedName(groupCtorOwnerParameter.Name, groupCtorOwnerParameter.ParameterType));
            groupCtor.Assign(groupOwnerRef.GetReference(), groupCtorOwnerParameter.GetReference());

            IFieldMember groupSetField = null;
            IFieldMember startIndex = null;
            IFieldMember itemCount = null;
            switch (listKind)
            {
                case MetadataTableFieldImportKind.OneToSequentialMany:
                case MetadataTableFieldImportKind.OneToSequentialManyImported:
                    startIndex = groupDictionary.Fields.Add(new TypedName(string.Format("{0}StartIndex", Program.lowerFirst(elementName)), typeof(uint)));
                    itemCount = groupDictionary.Fields.Add(typeof(int).WithName("count"));
                    var groupCtorStartIndex = groupCtor.Parameters.Add(new TypedName(startIndex.Name, startIndex.FieldType));
                    var groupCtorCount = groupCtor.Parameters.Add(new TypedName(itemCount.Name, itemCount.FieldType));
                    groupCtor.Assign(startIndex.GetReference(), groupCtorStartIndex.GetReference());
                    groupCtor.Assign(itemCount.GetReference(), groupCtorCount.GetReference());
                    break;
                case MetadataTableFieldImportKind.ManyToOneImport:
                    var groupCtorSetParameter = groupCtor.Parameters.Add(new TypedName(Program.lowerFirst(listName), typeof(uint[])));
                    groupSetField = groupDictionary.Fields.Add(new TypedName(groupCtorSetParameter.Name, groupCtorSetParameter.ParameterType));
                    groupCtor.Assign(groupSetField.GetReference(), groupCtorSetParameter.GetReference());
                    break;
            }


            /*Contains method*//*
            var gircContainsMethod = groupDictionary.Methods.Add(typeof(bool).WithName("Contains"));
            gircContainsMethod.AccessLevel = AccessLevelModifiers.Public;
            var gircContainsMethodItem = gircContainsMethod.Parameters.Add(Program.lowerFirst(elementName), listType);
            switch (listKind)
            {
                case MetadataTableFieldImportKind.OneToSequentialMany:
                case MetadataTableFieldImportKind.OneToSequentialManyImported:
                    gircContainsMethod.Return(new BinaryOperationExpression(new BinaryOperationExpression(startIndex.GetReference(), CodeBinaryOperatorType.LessThanOrEqual, gircContainsMethodItem.GetReference().GetProperty("Index")), CodeBinaryOperatorType.BooleanAnd, new BinaryOperationExpression(gircContainsMethodItem.GetReference().GetProperty("Index"), CodeBinaryOperatorType.LessThan, new BinaryOperationExpression(startIndex.GetReference(), CodeBinaryOperatorType.Add, itemCount.GetReference()))));
                    break;
                case MetadataTableFieldImportKind.ManyToOneImport:
                    gircContainsMethod.Return(typeof(Enumerable).GetTypeExpression().GetMethod("Contains", typeof(uint).GetTypeReference()).Invoke(groupSetField.GetReference(), gircContainsMethodItem.GetReference().GetProperty("Index")));
                    break;
            }
            
            /* *
             * Generic copy to.
             * *//*
            IMethodMember gircCopyToMethod = null;
            IMethodParameterMember gircCopyToMethodArray = null;
            IMethodParameterMember gircCopyToMethodArrayIndex = null;

            gircCopyToMethod = groupDictionary.Methods.Add(typeof(void).WithName("CopyTo"));
            gircCopyToMethodArray = gircCopyToMethod.Parameters.Add(new TypedName("array", listType.MakeArray(1)));
            gircCopyToMethodArrayIndex = gircCopyToMethod.Parameters.Add(typeof(int).WithName("arrayIndex"));

            var tableStreamTableRef = gircCopyToMethod.Locals.Add(new TypedName(Program.lowerFirst(string.Format("{0}Table", dictionarySourceTable.ShortName)), dictionarySourceTable.DeclaredLockedTableInterface));

            tableStreamTableRef.InitializationExpression = dictionarySourceTable.MetadataProperty.GetReference(dictionarySourceTable.RowLockedMetadataRootField.GetReference(groupOwnerRef.GetReference()).GetProperty("TableStream"));
            tableStreamTableRef.AutoDeclare = false;
            IConditionStatement gircCopyToLengthCheck = null;
            switch (listKind)
            {
                case MetadataTableFieldImportKind.OneToSequentialMany:
                case MetadataTableFieldImportKind.OneToSequentialManyImported:
                    gircCopyToLengthCheck = gircCopyToMethod.If(new BinaryOperationExpression(gircCopyToMethodArray.GetReference().GetProperty("Length"), CodeBinaryOperatorType.LessThan, itemCount.GetReference()));
                    break;
                case MetadataTableFieldImportKind.ManyToOneImport:
                    gircCopyToLengthCheck = gircCopyToMethod.If(new BinaryOperationExpression(gircCopyToMethodArray.GetReference().GetProperty("Length"), CodeBinaryOperatorType.LessThan, groupSetField.GetReference().GetProperty("Length")));
                    break;
            }

            gircCopyToLengthCheck.Add(new ThrowStatement(new CreateNewObjectExpression(typeof(ArgumentException).GetTypeReference(), new ExpressionCollection(new PrimitiveExpression("array not large enough"), new PrimitiveExpression(gircCopyToMethodArray.Name)))));
            IConditionStatement gircCopyToIndexCheck = null;
            switch (listKind)
            {
                case MetadataTableFieldImportKind.OneToSequentialMany:
                case MetadataTableFieldImportKind.OneToSequentialManyImported:
                    gircCopyToIndexCheck = gircCopyToMethod.If(new BinaryOperationExpression(new BinaryOperationExpression(gircCopyToMethodArrayIndex.GetReference(), CodeBinaryOperatorType.LessThan, IntermediateGateway.NumberZero), CodeBinaryOperatorType.BooleanOr, new BinaryOperationExpression(new BinaryOperationExpression(gircCopyToMethodArrayIndex.GetReference(), CodeBinaryOperatorType.Add, itemCount.GetReference()), CodeBinaryOperatorType.GreaterThan, gircCopyToMethodArray.GetReference().GetProperty("Length"))));
                    break;
                case MetadataTableFieldImportKind.ManyToOneImport:
                    gircCopyToIndexCheck = gircCopyToMethod.If(new BinaryOperationExpression(new BinaryOperationExpression(gircCopyToMethodArrayIndex.GetReference(), CodeBinaryOperatorType.LessThan, IntermediateGateway.NumberZero), CodeBinaryOperatorType.BooleanOr, new BinaryOperationExpression(new BinaryOperationExpression(gircCopyToMethodArrayIndex.GetReference(), CodeBinaryOperatorType.Add, groupSetField.GetReference().GetProperty("Length")), CodeBinaryOperatorType.GreaterThan, gircCopyToMethodArray.GetReference().GetProperty("Length"))));
                    break;
            }
            gircCopyToIndexCheck.Add(new ThrowStatement(new CreateNewObjectExpression(typeof(ArgumentOutOfRangeException).GetTypeReference(), new ExpressionCollection(new PrimitiveExpression(gircCopyToMethodArrayIndex.Name)))));
            gircCopyToMethod.Add(tableStreamTableRef.GetDeclarationStatement());
            var gircCopyToTableNullCheck = gircCopyToMethod.If(new BinaryOperationExpression(tableStreamTableRef.GetReference(), CodeBinaryOperatorType.IdentityInequality, IntermediateGateway.NullValue));
            var iterationIndex = gircCopyToMethod.Locals.Add(typeof(int).WithName("localIndex"));
            iterationIndex.InitializationExpression = IntermediateGateway.NumberZero;
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

            gircCopyToMethod.AccessLevel = AccessLevelModifiers.Public;

            /* *
             * Count Property
             * *//*
            var countProperty = groupDictionary.Properties.Add(typeof(int).WithName("Count"), true, false);
            switch (listKind)
            {
                case MetadataTableFieldImportKind.OneToSequentialMany:
                case MetadataTableFieldImportKind.OneToSequentialManyImported:
                    countProperty.GetMethod.Return(itemCount.GetReference());
                    break;
                case MetadataTableFieldImportKind.ManyToOneImport:
                    countProperty.GetMethod.Return(groupSetField.GetReference().GetProperty("Length"));
                    break;
            }
            countProperty.AccessLevel = AccessLevelModifiers.Public;
            ILocalMember currentListHolderTableRef = null;
            iterationIndex = null;
            iteration = null;
            var ircCopyToMethod = groupDictionary.Methods.Add(typeof(void).WithName("CopyTo"));
            ircCopyToMethod.PrivateImplementationTarget = typeof(IControlledCollection).GetTypeReference();
            var ircCopyToMethodArray = ircCopyToMethod.Parameters.Add(typeof(Array).WithName("array"));
            var ircCopyToMethodArrayIndex = ircCopyToMethod.Parameters.Add(typeof(int).WithName("arrayIndex"));

            currentListHolderTableRef = ircCopyToMethod.Locals.Add(new TypedName(Program.lowerFirst(string.Format("{0}Table", dictionarySourceTable.ShortName)), dictionarySourceTable.DeclaredLockedTableInterface));

            currentListHolderTableRef.InitializationExpression = dictionarySourceTable.MetadataProperty.GetReference(dictionaryContainerTable.LockedMetadataRootField.GetReference(groupOwnerRef.GetReference()).GetProperty("TableStream"));
            currentListHolderTableRef.AutoDeclare = false;
            IConditionStatement ircCopyToLengthCheck = null;
            
            switch (listKind)
            {
                case MetadataTableFieldImportKind.OneToSequentialMany:
                case MetadataTableFieldImportKind.OneToSequentialManyImported:
                    ircCopyToLengthCheck = ircCopyToMethod.If(new BinaryOperationExpression(ircCopyToMethodArray.GetReference().GetProperty("Length"), CodeBinaryOperatorType.LessThan, itemCount.GetReference()));
                    break;
                case MetadataTableFieldImportKind.ManyToOneImport:
                    ircCopyToLengthCheck = ircCopyToMethod.If(new BinaryOperationExpression(ircCopyToMethodArray.GetReference().GetProperty("Length"), CodeBinaryOperatorType.LessThan, groupSetField.GetReference().GetProperty("Length")));
                    break;
            }

            ircCopyToLengthCheck.Add(new ThrowStatement(new CreateNewObjectExpression(typeof(ArgumentException).GetTypeReference(), new ExpressionCollection(new PrimitiveExpression("array not large enough"), new PrimitiveExpression(ircCopyToMethodArray.Name)))));
            IConditionStatement ircCopyToIndexCheck = null;
            switch (listKind)
            {
                case MetadataTableFieldImportKind.OneToSequentialMany:
                case MetadataTableFieldImportKind.OneToSequentialManyImported:
                    ircCopyToIndexCheck = ircCopyToMethod.If(new BinaryOperationExpression(new BinaryOperationExpression(ircCopyToMethodArrayIndex.GetReference(), CodeBinaryOperatorType.LessThan, IntermediateGateway.NumberZero), CodeBinaryOperatorType.BooleanOr, new BinaryOperationExpression(new BinaryOperationExpression(ircCopyToMethodArrayIndex.GetReference(), CodeBinaryOperatorType.Add, itemCount.GetReference()), CodeBinaryOperatorType.GreaterThan, ircCopyToMethodArray.GetReference().GetProperty("Length"))));
                    break;
                case MetadataTableFieldImportKind.ManyToOneImport:
                    ircCopyToIndexCheck = ircCopyToMethod.If(new BinaryOperationExpression(new BinaryOperationExpression(ircCopyToMethodArrayIndex.GetReference(), CodeBinaryOperatorType.LessThan, IntermediateGateway.NumberZero), CodeBinaryOperatorType.BooleanOr, new BinaryOperationExpression(new BinaryOperationExpression(ircCopyToMethodArrayIndex.GetReference(), CodeBinaryOperatorType.Add, groupSetField.GetReference().GetProperty("Length")), CodeBinaryOperatorType.GreaterThan, ircCopyToMethodArray.GetReference().GetProperty("Length"))));
                    break;
            }

            ircCopyToIndexCheck.Add(new ThrowStatement(new CreateNewObjectExpression(typeof(ArgumentOutOfRangeException).GetTypeReference(), new ExpressionCollection(new PrimitiveExpression(ircCopyToMethodArrayIndex.Name)))));
            ircCopyToMethod.Add(currentListHolderTableRef.GetDeclarationStatement());
            var ircCopyToTableNullCheck = ircCopyToMethod.If(new BinaryOperationExpression(currentListHolderTableRef.GetReference(), CodeBinaryOperatorType.IdentityInequality, IntermediateGateway.NullValue));
            iterationIndex = ircCopyToTableNullCheck.Locals.Add(typeof(int).WithName("localIndex"));
            iterationIndex.InitializationExpression = IntermediateGateway.NumberZero;
            iterationIndex.AutoDeclare = false;
            switch (listKind)
            {
                case MetadataTableFieldImportKind.OneToSequentialMany:
                case MetadataTableFieldImportKind.OneToSequentialManyImported:
                    iteration = ircCopyToTableNullCheck.Iterate(iterationIndex.GetDeclarationStatement(), new CrementStatement(CrementType.Postfix, CrementOperation.Increment, iterationIndex.GetReference()), new BinaryOperationExpression(iterationIndex.GetReference(), CodeBinaryOperatorType.LessThan, itemCount.GetReference()));
                    iteration.Call(ircCopyToMethodArray.GetReference().GetMethod("SetValue").Invoke(currentListHolderTableRef.GetReference().GetIndex(new BinaryOperationExpression(startIndex.GetReference(), CodeBinaryOperatorType.Add, iterationIndex.GetReference()).Cast(typeof(int))), new BinaryOperationExpression(ircCopyToMethodArrayIndex.GetReference(), CodeBinaryOperatorType.Add, iterationIndex.GetReference())));
                    break;
                case MetadataTableFieldImportKind.ManyToOneImport:
                    iteration = ircCopyToTableNullCheck.Iterate(iterationIndex.GetDeclarationStatement(), new CrementStatement(CrementType.Postfix, CrementOperation.Increment, iterationIndex.GetReference()), new BinaryOperationExpression(iterationIndex.GetReference(), CodeBinaryOperatorType.LessThan, groupSetField.GetReference().GetProperty("Length")));
                    iteration.Call(ircCopyToMethodArray.GetReference().GetMethod("SetValue").Invoke(currentListHolderTableRef.GetReference().GetIndex(groupSetField.GetReference().GetIndex(iterationIndex.GetReference()).Cast(typeof(int))), new BinaryOperationExpression(ircCopyToMethodArrayIndex.GetReference(), CodeBinaryOperatorType.Add, iterationIndex.GetReference())));
                    break;
            }
            ircCopyToMethod.AccessLevel = AccessLevelModifiers.Public;
            ircCopyToTableNullCheck.FalseBlock.Add(new ThrowStatement(new CreateNewObjectExpression(typeof(InvalidOperationException).GetTypeReference(), new ExpressionCollection(new PrimitiveExpression("Invalid object state.")))));


            /* *
             * Generic index of
             * *//*
            var gircIndexOf = groupDictionary.Methods.Add(typeof(int).WithName("IndexOf"));
            var gircIndexOfElement = gircIndexOf.Parameters.Add(Program.lowerFirst(elementName), listType);
            var gircIndexOfNullCheck = gircIndexOf.If(new BinaryOperationExpression(gircIndexOfElement.GetReference(), CodeBinaryOperatorType.IdentityEquality, IntermediateGateway.NullValue));
            gircIndexOfNullCheck.Add(new ThrowStatement(new CreateNewObjectExpression(typeof(ArgumentNullException).GetTypeReference(), new ExpressionCollection(new PrimitiveExpression(gircIndexOfElement.GetReference().Name)))));
            var gircIndexOfItemIndex = gircIndexOf.Locals.Add(new TypedName(Program.lowerFirst(indexName), typeof(uint)));
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
                    gircIndexOfIterationEquality = gircIndexOf.If(new BinaryOperationExpression(new BinaryOperationExpression(startIndex.GetReference(), CodeBinaryOperatorType.LessThanOrEqual, gircIndexOfItemIndex.GetReference()), CodeBinaryOperatorType.BooleanAnd, new BinaryOperationExpression(gircIndexOfItemIndex.GetReference(), CodeBinaryOperatorType.LessThan, new BinaryOperationExpression(startIndex.GetReference(), CodeBinaryOperatorType.Add, itemCount.GetReference()))));
                    gircIndexOfIterationEquality.Return(new BinaryOperationExpression(gircIndexOfItemIndex.GetReference(), CodeBinaryOperatorType.Subtract, startIndex.GetReference()).Cast(typeof(int)));
                    break;
                case MetadataTableFieldImportKind.ManyToOneImport:
                    var gircIndexOfLocalIndex = gircIndexOf.Locals.Add(typeof(int).WithName("localIndex"));
                    gircIndexOfLocalIndex.InitializationExpression = IntermediateGateway.NumberZero;
                    gircIndexOfLocalIndex.AutoDeclare = false;
                    gircIndexOfIteration = gircIndexOf.Iterate(gircIndexOfLocalIndex.GetDeclarationStatement(), new CrementStatement(CrementType.Postfix, CrementOperation.Increment, gircIndexOfLocalIndex.GetReference()), new BinaryOperationExpression(gircIndexOfLocalIndex.GetReference(), CodeBinaryOperatorType.LessThan, groupSetField.GetReference().GetProperty("Length")));
                    gircIndexOfIterationEquality = gircIndexOfIteration.If(new BinaryOperationExpression(gircIndexOfItemIndex.GetReference(), CodeBinaryOperatorType.IdentityEquality, groupSetField.GetReference().GetIndex(gircIndexOfLocalIndex.GetReference())));
                    gircIndexOfIterationEquality.Return(gircIndexOfLocalIndex.GetReference());
                    break;
            }
            gircIndexOf.Return(PrimitiveExpression.NumberMinusOne);
            gircIndexOf.AccessLevel = AccessLevelModifiers.Public;

            /* *
             * ToArray
             * *//*
            var gircToArray = groupDictionary.Methods.Add(new TypedName("ToArray", listType.MakeArray(1)));
            var gircToArrayResult = gircToArray.Locals.Add(new TypedName("result", gircToArray.ReturnType));
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
            gircToArray.Call(gircCopyToMethod.GetReference().Invoke(gircToArrayResult.GetReference(), IntermediateGateway.NumberZero));
            gircToArray.Return(gircToArrayResult.GetReference());
            gircToArray.AccessLevel = AccessLevelModifiers.Public;


            var gircThis = groupDictionary.Properties.Add(listType, true, false, typeof(int).WithName("index"));
            gircThis.AccessLevel = AccessLevelModifiers.Public;
            var gircThisIndex = gircThis.Parameters["index"];
            IConditionStatement gircThisRangeCheck = null;
            switch (listKind)
            {
                case MetadataTableFieldImportKind.OneToSequentialMany:
                case MetadataTableFieldImportKind.OneToSequentialManyImported:
                    gircThisRangeCheck = gircThis.GetMethod.If(new BinaryOperationExpression(new BinaryOperationExpression(gircThisIndex.GetReference(), CodeBinaryOperatorType.LessThan, IntermediateGateway.NumberZero), CodeBinaryOperatorType.BooleanOr, new BinaryOperationExpression(gircThisIndex.GetReference(), CodeBinaryOperatorType.GreaterThanOrEqual, itemCount.GetReference())));
                    gircThisRangeCheck.Add(new ThrowStatement(new CreateNewObjectExpression(typeof(ArgumentOutOfRangeException).GetTypeReference(), new ExpressionCollection(new PrimitiveExpression(gircThisIndex.Name)))));
                    gircThis.GetMethod.Return(dictionarySourceTable.MetadataProperty.GetReference(dictionarySourceTable.RowLockedMetadataRootField.GetReference(groupOwnerRef.GetReference()).GetProperty("TableStream")).GetIndex(new BinaryOperationExpression(startIndex.GetReference(), CodeBinaryOperatorType.Add, gircThisIndex.GetReference()).Cast(typeof(int))));
                    break;
                case MetadataTableFieldImportKind.ManyToOneImport:
                    gircThisRangeCheck = gircThis.GetMethod.If(new BinaryOperationExpression(new BinaryOperationExpression(gircThisIndex.GetReference(), CodeBinaryOperatorType.LessThan, IntermediateGateway.NumberZero), CodeBinaryOperatorType.BooleanOr, new BinaryOperationExpression(gircThisIndex.GetReference(), CodeBinaryOperatorType.GreaterThanOrEqual, groupSetField.GetReference().GetProperty("Length"))));
                    gircThisRangeCheck.Add(new ThrowStatement(new CreateNewObjectExpression(typeof(ArgumentOutOfRangeException).GetTypeReference(), new ExpressionCollection(new PrimitiveExpression(gircThisIndex.Name)))));
                    gircThis.GetMethod.Return(dictionarySourceTable.MetadataProperty.GetReference(dictionarySourceTable.RowLockedMetadataRootField.GetReference(groupOwnerRef.GetReference()).GetProperty("TableStream")).GetIndex(groupSetField.GetReference().GetIndex(gircThisIndex.GetReference()).Cast(typeof(int))));
                    break;
            }

            var gircEnumeration = groupDictionary.Methods.Add(new TypedName("GetEnumerator", typeof(IEnumerator<>).GetTypeReference(new TypeReferenceCollection(gircThis.PropertyType))));
            gircEnumeration.AccessLevel = AccessLevelModifiers.Public;
            tableStreamTableRef = gircEnumeration.Locals.Add(new TypedName(Program.lowerFirst(string.Format("{0}Table", dictionarySourceTable.ShortName)), dictionarySourceTable.DeclaredLockedTableInterface));

            
            tableStreamTableRef.InitializationExpression = dictionarySourceTable.MetadataProperty.GetReference(dictionaryContainerTable.LockedMetadataRootField.GetReference(groupOwnerRef.GetReference()).GetProperty("TableStream"));
            var nullCheck = gircEnumeration.If(new BinaryOperationExpression(tableStreamTableRef.GetReference(), CodeBinaryOperatorType.IdentityInequality, IntermediateGateway.NullValue));
            iterationIndex = nullCheck.Locals.Add(typeof(int).WithName("localIndex"));
            iterationIndex.InitializationExpression = IntermediateGateway.NumberZero;
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


            var ircEnumeration = groupDictionary.Methods.Add(typeof(IEnumerator).WithName("ge"));
            ircEnumeration.PrivateImplementationTarget = typeof(IEnumerable).GetTypeReference();
            ircEnumeration.Return(gircEnumeration.GetReference().Invoke());
            ircEnumeration.Name = "GetEnumerator";

            var ircContains = groupDictionary.Methods.Add(typeof(bool).WithName("Contains"));
            var ircContainsElement = ircContains.Parameters.Add(new TypedName(gircContainsMethodItem.Name, typeof(object)));
            ircContains.PrivateImplementationTarget = typeof(IControlledCollection).GetTypeReference();
            ircContains.Return(gircContainsMethod.GetReference().Invoke(ircContainsElement.GetReference().Cast(listType)));


            var ircIndexOf = groupDictionary.Methods.Add(typeof(int).WithName("IndexOf"));
            ircIndexOf.PrivateImplementationTarget = typeof(IControlledCollection).GetTypeReference();
            var ircIndexOfElement = ircIndexOf.Parameters.Add(new TypedName(gircContainsMethodItem.Name, typeof(object)));
            ircIndexOf.Return(new SpecialReferenceExpression(SpecialReferenceKind.This).GetMethod("IndexOf").Invoke(ircIndexOfElement.GetReference().Cast(listType)));
            var ircThis = groupDictionary.Properties.Add(typeof(object).GetTypeReference(), true, false, typeof(void).WithName("index"));
            var ircThisIndex = ircThis.Parameters["index"];
            ircThis.PrivateImplementationTarget = typeof(IControlledCollection).GetTypeReference();
            ircThisIndex.ParameterType = typeof(int).GetTypeReference();
            ircThis.GetMethod.Return(new SpecialReferenceExpression(SpecialReferenceKind.This).GetIndex(ircThisIndex.GetReference()));
            return groupDictionary;
        }//*/

        private TypedName CreateParameter(MetadataTableStateMachineDataTypeInfo dataType, out string documentationComment)
        {
            switch (dataType.DataType.DataKind)
            {
                case FieldDataKind.Encoding:
                    var encoding = dataType.DataType as IMetadataTableFieldEncodingDataType;
                    documentationComment = string.Format("The @s:{1}; used to denote the word size of the indices nwith the @s:{0}; encoding.", encoding.EncodingType, typeof(CliMetadataReferenceIndexSize).GetTypeReference().BuildTypeName(typeParameterDisplayMode: TypeParameterDisplayMode.CommentStandard));
                    return typeof(CliMetadataReferenceIndexSize).WithName(string.Format("{0}Size", Program.lowerFirst(encoding.Name)));
                case FieldDataKind.HeapIndex:
                    var heapType = (IMetadataTableFieldHeapDataType) dataType.DataType;
                    var heapReferenceType = (IType) null;
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
                    documentationComment = string.Format("The @s:{1}; used to denote the word size of the @s:{0};.", heapReferenceType, typeof(CliMetadataReferenceIndexSize).GetTypeReference().BuildTypeName(typeParameterDisplayMode: TypeParameterDisplayMode.CommentStandard));
                    return typeof(CliMetadataReferenceIndexSize).WithName(string.Format("{0}Size", Program.lowerFirst(heapType.Heap.ToString())));
                case FieldDataKind.TableReference:
                    var table = dataType.DataType as MetadataTable;
                    documentationComment = string.Format("The @s:{1}; used to denote the word size of the @s:{0};.", table.DeclaredLockedTableInterface.Name, typeof(CliMetadataReferenceIndexSize).GetTypeReference().BuildTypeName(typeParameterDisplayMode: TypeParameterDisplayMode.CommentStandard));
                    return typeof(CliMetadataReferenceIndexSize).WithName(string.Format("{0}Size", Program.lowerFirst(table.ShortName)));
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

        public void CreateSetStateAlternations(LinkedListNode<MetadataTableStateMachineDataTypeInfo> currentPosition, IBlockStatementParent target, Dictionary<MetadataTableStateMachineDataTypeInfo, IIntermediateMethodParameterMember> parameterLookup, IIntermediateFieldMember stateField, IIntermediateFieldMember rowSizeField, Dictionary<int, IIntermediateClassFieldMember> stateSizeFields, int currentStateValue = 0)
        {
            var condition = target.If(
                parameterLookup[currentPosition.Value]
                .EqualTo(
                    typeof(CliMetadataReferenceIndexSize)
                    .GetTypeExpression()
                    .GetField("DWord")));
                //new BinaryOperationExpression(
                //    parameterLookup[currentPosition.Value].GetReference(), 
                //    CodeBinaryOperatorType.IdentityEquality, 
                //    typeof(CliMetadataReferenceIndexSize)
                //    .GetTypeExpression()
                //    .GetField("DWord")));
            if (currentPosition.Next == null)
            {
                var setStateValue = currentStateValue | (1 << currentPosition.Value.Index);
                condition.Assign(stateField.GetReference(), setStateValue.ToPrimitive());
                condition.Assign(rowSizeField.GetReference(), stateSizeFields[setStateValue].GetReference());
                condition.Next.Assign(stateField.GetReference(), currentStateValue.ToPrimitive());
                condition.Next.Assign(rowSizeField.GetReference(), stateSizeFields[currentStateValue].GetReference());
            }
            else
            {
                CreateSetStateAlternations(currentPosition.Next, condition, parameterLookup, stateField, rowSizeField, stateSizeFields, currentStateValue | (1 << currentPosition.Value.Index));
                CreateSetStateAlternations(currentPosition.Next, condition.Next, parameterLookup, stateField, rowSizeField, stateSizeFields, currentStateValue);
            }
        }
    }
}
