using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract class CliFieldMember<TField, TFieldParent> :
        CliMemberBase<IGeneralMemberUniqueIdentifier, TFieldParent, ICliMetadataFieldTableRow>,
        IFieldMember<TField, TFieldParent>
        where TField :
            class,
            IFieldMember<TField, TFieldParent>
        where TFieldParent :
            IFieldParent<TField, TFieldParent>
    {
        private IGeneralMemberUniqueIdentifier uniqueIdentifier;
        private CliMetadataCollection metadata;

        internal CliFieldMember(TFieldParent parent, ICliMetadataFieldTableRow metadataEntry, IGeneralMemberUniqueIdentifier uniqueIdentifier)
            : base(parent, metadataEntry)
        {
            this.uniqueIdentifier = uniqueIdentifier;
        }
        protected override sealed string OnGetName()
        {
            return this.MetadataEntry.Name;
        }

        public override sealed IGeneralMemberUniqueIdentifier UniqueIdentifier
        {
            get { return this.uniqueIdentifier; }
        }

        public IType FieldType
        {
            get { return this.OnGetFieldType(); }
        }

        protected abstract IType OnGetFieldType();

        protected abstract _ICliManager _IdentityManager { get; }

        #region IMetadataEntity Members

        public IMetadataCollection Metadata
        {
            get
            {
                if (this.metadata == null)
                    this.metadata = new CliMetadataCollection(this.MetadataEntry.CustomAttributes, this, this._IdentityManager);
                return this.metadata;
            }
        }

        public bool IsDefined(IType metadatumType)
        {
            return this.Metadata.Contains(metadatumType);
        }

        #endregion

        #region IFieldMember Members

        public FieldMemberAttributes Attributes
        {
            get
            {
                return (ReadOnly ? FieldMemberAttributes.ReadOnly : FieldMemberAttributes.None) | (Constant ? FieldMemberAttributes.Constant : FieldMemberAttributes.None);
            }
        }

        public bool ReadOnly
        {
            get
            {
                return ((MetadataEntry.FieldAttributes & FieldAttributes.InitOnly) == FieldAttributes.InitOnly);
            }
        }

        public bool Constant
        {
            get
            {
                return ((MetadataEntry.FieldAttributes & FieldAttributes.Literal) == FieldAttributes.Literal);
            }
        }

        #endregion
    }
}
