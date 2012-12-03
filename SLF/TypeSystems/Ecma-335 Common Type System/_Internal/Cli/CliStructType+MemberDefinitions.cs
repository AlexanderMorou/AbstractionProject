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
    internal partial class CliStructType
    {
        private class FieldMember :
            CliMemberBase<IGeneralMemberUniqueIdentifier, IStructType, ICliMetadataFieldTableRow>,
            IStructFieldMember
        {
            public FieldMember(IStructType parent, ICliMetadataFieldTableRow metadataEntry)
                : base(parent, metadataEntry)
            {
            }

            protected override string OnGetName()
            {
                return this.MetadataEntry.Name;
            }

            public override IGeneralMemberUniqueIdentifier UniqueIdentifier
            {
                get { return AstIdentifier.GetMemberIdentifier(this.Name); }
            }

            private _ICliManager IdentityManager { get { return (_ICliManager)this.Parent.IdentityManager; } }

            public IType FieldType
            {
                get { 
                    var type = this.IdentityManager.ObtainTypeReference(this.MetadataEntry.FieldType.Type, this.Parent, null); ;
                    if (this.MetadataEntry.FieldType.CustomModifiers.Count > 0)
                    {
                        return type.MakeModified((from mod in this.MetadataEntry.FieldType.CustomModifiers
                                                  select new TypeModification(() => this.IdentityManager.ObtainTypeReference(mod.ModifierType), mod.Required)).ToArray());
                    }
                    return type;
                }
            }

            public InstanceMemberFlags InstanceFlags
            {
                get {
                    return CliCommon.GetFieldInstanceFlags(this.MetadataEntry.FieldAttributes);
                }
            }

            public bool IsHideBySignature
            {
                get { return false; }
            }

            public bool IsStatic
            {
                get { return (this.InstanceFlags & InstanceMemberFlags.Static) == InstanceMemberFlags.Static; }
            }

            public AccessLevelModifiers AccessLevel
            {
                get {
                    return CliCommon.GetFieldAccessModifiers(this.MetadataEntry.FieldAttributes);
                }
            }
        }
    }
}
