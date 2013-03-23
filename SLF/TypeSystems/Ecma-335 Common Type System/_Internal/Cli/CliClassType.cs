using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal partial class CliClassType :
        CliGenericInstantiableTypeBase<IClassCtorMember, IClassEventMember, IClassFieldMember, 
             IClassIndexerMember, IClassMethodMember, IClassPropertyMember, IClassType>,
        IClassType
    {
        internal CliClassType(CliAssembly assembly, ICliMetadataTypeDefinitionTableRow metadata)
            : base(assembly, metadata)
        {

        }
        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Class; }
        }

        public new IClassInterfaceMapping GetInterfaceMap(IInterfaceType type)
        {
            throw new NotImplementedException();
        }

        public SpecialClassModifier SpecialModifier
        {
            get { throw new NotImplementedException(); }
        }

        public new IClassType BaseType
        {
            get { return (IClassType)base.BaseType; }
        }

        protected override IClassType OnMakeGenericClosure(LockedTypeCollection lockedTypeParameters)
        {
            return new _ClassTypeBase(this, lockedTypeParameters);
        }

        protected override IClassCtorMember GetConstructor(ICliMetadataMethodDefinitionTableRow metadataEntry, IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier)
        {
            return new CtorMember(this, metadataEntry, this.IdentityManager, uniqueIdentifier);
        }

        protected override IClassEventMember GetEvent(ICliMetadataEventTableRow metadataEntry, IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier)
        {
            return new EventMember(this, uniqueIdentifier, metadataEntry);
        }

        protected override IClassFieldMember GetField(ICliMetadataFieldTableRow metadataEntry)
        {
            return new FieldMember(this, metadataEntry);
        }

        protected override IClassIndexerMember GetIndexer(ICliMetadataPropertyTableRow metadataEntry, IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier)
        {
            return new IndexerMember(this, metadataEntry, uniqueIdentifier);
        }

        protected override IClassMethodMember GetMethod(ICliMetadataMethodDefinitionTableRow metadataEntry, IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier)
        {
            return new MethodMember(metadataEntry, (_ICliAssembly)this.Assembly, this, uniqueIdentifier);
        }

        protected override IClassPropertyMember GetProperty(ICliMetadataPropertyTableRow metadataEntry)
        {
            return new PropertyMember(this, metadataEntry);
        }
    }
}
