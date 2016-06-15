using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

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
            this.SummaryText = summary;
            this.ShortName = ShortName;
            this.RemarksText = remarks;
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

        public string SummaryText { get; set; }
        public string RemarksText { get; set; }

        public string ShortName { get; set; }

        public IIntermediateClassType           DeclaredTableClass { get; set; }

        public IIntermediateClassType           DeclaredMutableTableClass { get; set; }

        public IIntermediateClassType           DeclaredLockedTableRowClass { get; set; }

        public IIntermediateClassType           DeclaredMutableTableRowClass { get; set; }

        public IIntermediateConstructorMember   DeclaredTableClassCtor { get; set; }

        public ILocalMember WordSizeLocal { get; set; }

        public void Add(MetadataTableField field)
        {
            base._Add(field.FieldName, field);
        }

        public MetadataTableStateMachineInfo StateMachine { get; set; }

        public IIntermediateConstructorMember DeclaredTableRowCtor { get; set; }

        #region IMetadataTableFieldDataType Members

        public FieldDataKind DataKind
        {
            get { return FieldDataKind.TableReference; }
        }

        #endregion

        public IIntermediateFieldMember         RowCountField { get; set; }

        public IBinaryOperationExpression       PresenceCheckCondition { get; set; }

        public IFieldReferenceExpression        TableKindExpression { get; set; }

        public IIntermediateFieldMember         ReaderField { get; set; }

        public IIntermediateFieldMember         LockedMetadataRootField { get; set; }

        public IIntermediateMethodMember        InitializeMethod { get; set; }

        /// <summary>
        /// Returns the <see cref="IPropertyMember"/> which goes back
        /// to the metadataroot's table stream property.
        /// </summary>
        public IIntermediatePropertyMember      MetadataProperty { get; set; }

        public IIntermediateClassFieldMember    RowLockedMetadataRootField { get; set; }

        public IIntermediatePropertyMember      LengthProperty { get; set; }

        public IIntermediateInterfaceType       DeclaredLockedTableInterface { get; set; }

        public IIntermediateInterfaceType       DeclaredLockedTableRowInterface { get; set; }

        public bool                             NeedsIndex { get; set; }

        public IIntermediateClassPropertyMember RowLockedMetadataRootProperty { get; set; }
        public IIntermediateClassPropertyMember RowMutableMetadataRootProperty { get; set; }

        public IIntermediateFieldMember         StreamField { get; set; }

        public IIntermediateFieldMember         SyncField { get; set; }

        public IIntermediateInterfaceType       DeclaredMutableTableRowInterface { get; set; }

        public IIntermediateInterfaceType       DeclaredMutableTableInterface { get; set; }

        public IIntermediateFieldMember         MutableMetadataRootField { get; set; }

        public IIntermediateFieldMember         RowMutableMetadataRootField { get; set; }
    }
}
