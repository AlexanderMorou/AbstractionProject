using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Modules;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal partial class CliStructType :
        CliGenericInstantiableTypeBase<IStructCtorMember, IStructEventMember, IStructFieldMember,
             IStructIndexerMember, IStructMethodMember, IStructPropertyMember, IStructType>,
        IStructType
    {
        internal CliStructType(CliAssembly assembly, ICliMetadataTypeDefinitionTableRow metadata)
            : base(assembly, metadata)
        {

        }

        internal ICliAssembly Assembly { get { return (ICliAssembly)base.Assembly; } }

        protected override IStructCtorMember GetConstructor(ICliMetadataMethodDefinitionTableRow metadataEntry, IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier)
        {
            return new CtorMember(this, metadataEntry, this.IdentityManager, uniqueIdentifier);
        }

        protected override IStructEventMember GetEvent(ICliMetadataEventTableRow metadataEntry, IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier)
        {
            return new EventMember(this, uniqueIdentifier, metadataEntry);
        }

        protected override IStructFieldMember GetField(ICliMetadataFieldTableRow metadataEntry)
        {
            return new FieldMember(this, metadataEntry);
        }

        protected override IStructIndexerMember GetIndexer(ICliMetadataPropertyTableRow metadataEntry, IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier)
        {
            return new IndexerMember(this, metadataEntry, uniqueIdentifier);
        }

        protected override IStructMethodMember GetMethod(ICliMetadataMethodDefinitionTableRow metadataEntry, IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier)
        {
            return new MethodMember(metadataEntry, (_ICliAssembly)this.Assembly, this, uniqueIdentifier);
        }

        protected override IStructPropertyMember GetProperty(ICliMetadataPropertyTableRow metadataEntry)
        {
            return new PropertyMember(this, metadataEntry);
        }

        protected override IStructType OnMakeGenericClosure(LockedTypeCollection lockedTypeParameters)
        {
            return new _StructTypeBase(this, lockedTypeParameters);
        }

        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Struct; }
        }

        public new IStructInterfaceMapping GetInterfaceMap(IInterfaceType type)
        {
            throw new NotImplementedException();
        }

    }
}
