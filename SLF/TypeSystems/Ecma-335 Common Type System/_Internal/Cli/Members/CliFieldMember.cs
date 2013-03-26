using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
