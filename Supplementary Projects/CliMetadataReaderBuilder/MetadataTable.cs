using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System.Diagnostics;
using AllenCopeland.Abstraction.OldCodeGen.Types;
using AllenCopeland.Abstraction.OldCodeGen.Types.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.OldCodeGen.Expression;

namespace CliMetadataReader
{
    //[DebuggerDisplay("{TableKind} ({Count} fields)")]
    public class MetadataTable :
        ControlledDictionary<string, MetadataTableField>,
        IMetadataTableFieldDataType
    {
        /// <summary>
        /// Returns the name of the <see cref="MetadataTable"/>.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Returns the byte offset of the <see cref="MetadataTable"/>.
        /// </summary>
        public byte Offset { get; private set; }

        public CliMetadataTableKinds TableKind { get { return (CliMetadataTableKinds) (1UL << ((int) Offset)); } }
        public List<MetadataTableField> ListReferences { get; private set; }

        public MetadataTable(string name, byte offset, string summary = null, string remarks = null, string shortName = null)
        {
            this.ListReferences = new List<MetadataTableField>();
            this.Summary = summary;
            this.ShortName = ShortName;
            this.Remarks = remarks;
            this.Name = name;
            this.Offset = offset;
            if (shortName == null)
                this.ShortName = name;
            else
                this.ShortName = shortName;
        }

        public override string ToString()
        {
            if (this.StateMachine == null)
                return this.Name;
            else
                return string.Format("{{{0}}} {1}", this.StateMachine.MetadataStateCount, this.Name);
        }

        public string Summary { get; set; }
        public string Remarks { get; set; }

        public string ShortName { get; set; }

        public IClassType DeclaredTableClass { get; set; }

        public IClassType DeclaredTableRowClass { get; set; }

        public IConstructorMember DeclaredTableClassCtor { get; set; }

        public IStatementBlockLocalMember WordSizeLocal { get; set; }

        public void Add(MetadataTableField field)
        {
            base._Add(field.FieldName, field);
        }

        public MetadataTableStateMachineInfo StateMachine { get; set; }

        public IConstructorMember DeclaredTableRowCtor { get; set; }

        #region IMetadataTableFieldDataType Members

        public FieldDataKind DataKind
        {
            get { return FieldDataKind.TableReference; }
        }

        #endregion

        public IFieldMember RowCountField { get; set; }

        public BinaryOperationExpression PresenceCheckCondition { get; set; }

        public IFieldReferenceExpression TableKindExpression { get; set; }

        public IFieldMember ReaderField { get; set; }

        public IFieldMember MetadataRootField { get; set; }

        public IMethodMember InitializeMethod { get; set; }

        /// <summary>
        /// Returns the <see cref="IPropertyMember"/> which goes back
        /// to the metadataroot's table stream property.
        /// </summary>
        public IPropertyMember MetadataProperty { get; set; }

        public IFieldMember RowMetadataRootField { get; set; }

        public IPropertyMember LengthProperty { get; set; }

        public IInterfaceType DeclaredTableInterface { get; set; }

        public IInterfaceType DeclaredTableRowInterface { get; set; }

        public bool NeedsIndex { get; set; }

        public IPropertyMember RowMetadataRootProperty { get; set; }

        public IFieldMember StreamField { get; set; }

        public IFieldMember SyncField { get; set; }
    }
}
