using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CliEnumType
    {
        private class FieldMember :
            CliFieldMember<IEnumFieldMember, IEnumType>
        {
            internal FieldMember(CliEnumType parent, ICliMetadataFieldTableRow metadataEntry, IGeneralMemberUniqueIdentifier uniqueIdentifier)
                : base(parent, metadataEntry, uniqueIdentifier)
            {
            }
            private CliManager IdentityManager { get { return (CliManager)this.Parent.IdentityManager; } }
            protected override IType OnGetFieldType()
            {
                return IdentityManager.ObtainTypeReference(this.MetadataEntry.FieldType, this.MetadataEntry.FieldType.Type, this.Parent.Parent is IGenericType ? (IType)this.Parent.Parent : null, null);
            }
        }
    }
}
