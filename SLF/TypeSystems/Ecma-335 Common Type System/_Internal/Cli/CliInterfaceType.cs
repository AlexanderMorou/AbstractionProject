using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal partial class CliInterfaceType :
        CliGenericParentType<IGeneralGenericTypeUniqueIdentifier, IInterfaceType>,
        IInterfaceType,
        _ICliMemberParent
    {
        private IMethodSignatureMemberDictionary<IInterfaceMethodMember, IInterfaceType> methods;
        private IPropertySignatureMemberDictionary<IInterfacePropertyMember, IInterfaceType> properties;
        private IEventSignatureMemberDictionary<IInterfaceEventMember, IInterfaceType> events;
        private IIndexerSignatureMemberDictionary<IInterfaceIndexerMember, IInterfaceType> indexers;
        private IFullMemberDictionary _members;
        public CliInterfaceType(CliAssembly owner, ICliMetadataTypeDefinitionTableRow metadataEntry)
            : base(owner, metadataEntry)
        {
        }

        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Interface; }
        }

        public IMethodSignatureMemberDictionary<IInterfaceMethodMember, IInterfaceType> Methods
        {
            get {
                if (this.methods == null)
                    this.methods = this.InitializeMethods();
                return this.methods;
            }
        }

        public IPropertySignatureMemberDictionary<IInterfacePropertyMember, IInterfaceType> Properties
        {
            get
            {
                if (this.properties == null)
                    this.properties = this.InitializeProperties();
                return this.properties;
            }
        }

        public IEventSignatureMemberDictionary<IInterfaceEventMember, IInterfaceType> Events
        {
            get {
                if (this.events == null)
                    this.events = this.InitializeEvents();
                return this.events;
            }
        }

        public IIndexerSignatureMemberDictionary<IInterfaceIndexerMember, IInterfaceType> Indexers
        {
            get {
                if (this.indexers == null)
                    this.indexers = this.InitializeIndexers();
                return this.indexers;
            }
        }

        private IMethodSignatureMemberDictionary<IInterfaceMethodMember, IInterfaceType> InitializeMethods()
        {
            return new CliMethodSignatureMemberDictionary<IInterfaceMethodMember, IInterfaceType>(this, (CliFullMemberDictionary)this.Members);
        }

        private IPropertySignatureMemberDictionary<IInterfacePropertyMember, IInterfaceType> InitializeProperties()
        {
            return new CliPropertySignatureMemberDictionary<IInterfacePropertyMember, IInterfaceType>(this, (CliFullMemberDictionary)this.Members);
        }

        private IEventSignatureMemberDictionary<IInterfaceEventMember, IInterfaceType> InitializeEvents()
        {
            return new CliEventSignatureMemberDictionary<IInterfaceEventMember, IInterfaceType>(this, (CliFullMemberDictionary)this.Members);
        }

        private IIndexerSignatureMemberDictionary<IInterfaceIndexerMember, IInterfaceType> InitializeIndexers()
        {
            return new CliIndexerSignatureMemberDictionary<IInterfaceIndexerMember, IInterfaceType>(this, (CliFullMemberDictionary)this.Members);
        }

        IMethodSignatureMemberDictionary IMethodSignatureParent.Methods
        {
            get { return (IMethodSignatureMemberDictionary)this.Methods; }
        }

        IPropertySignatureMemberDictionary IPropertySignatureParent.Properties
        {
            get { return (IPropertySignatureMemberDictionary)this.Properties; }
        }

        IEventSignatureMemberDictionary<IInterfaceEventMember, IEventSignatureParameterMember<IInterfaceEventMember, IInterfaceType>, IInterfaceType> IEventSignatureParent<IInterfaceEventMember, IEventSignatureParameterMember<IInterfaceEventMember, IInterfaceType>, IInterfaceType>.Events
        {
            get { return (IEventSignatureMemberDictionary<IInterfaceEventMember, IEventSignatureParameterMember<IInterfaceEventMember, IInterfaceType>, IInterfaceType>)this.Events; }
        }

        IEventSignatureMemberDictionary IEventSignatureParent.Events
        {
            get { return (IEventSignatureMemberDictionary)this.Events; }
        }

        IIndexerSignatureMemberDictionary IIndexerSignatureParent.Indexers
        {
            get { return (IIndexerSignatureMemberDictionary)this.Indexers; }
        }

        protected override IFullMemberDictionary OnGetMembers()
        {
            if (this._members == null)
                this._members = this.Initialize_Members();
            return this._members;
        }

        private IFullMemberDictionary Initialize_Members()
        {
            return new CliFullMemberDictionary(this);
        }

        public IControlledCollection<ICliMetadataPropertyTableRow> _Properties
        {
            get { return this.MetadataEntry.Properties; }
        }

        public IControlledCollection<ICliMetadataEventTableRow> _Events
        {
            get { return this.MetadataEntry.Events; }
        }

        public IControlledCollection<ICliMetadataMethodDefinitionTableRow> _Methods
        {
            get { return this.MetadataEntry.Methods; }
        }

        public IControlledCollection<ICliMetadataFieldTableRow> _Fields
        {
            get { return null; }
        }

        public ISubordinateDictionary BinaryOperators
        {
            get { return null; }
        }

        ISubordinateDictionary _ICliMemberParent.Constructors
        {
            get { return null; }
        }

        ISubordinateDictionary _ICliMemberParent.Events
        {
            get { return (ISubordinateDictionary)this.Events; }
        }

        ISubordinateDictionary _ICliMemberParent.Fields
        {
            get { return null; }
        }

        ISubordinateDictionary _ICliMemberParent.Indexers
        {
            get { return (ISubordinateDictionary)this.Indexers; }
        }

        ISubordinateDictionary _ICliMemberParent.Methods
        {
            get { return (ISubordinateDictionary)this.Methods; }
        }

        ISubordinateDictionary _ICliMemberParent.Properties
        {
            get { return (ISubordinateDictionary)this.Properties; }
        }

        ISubordinateDictionary _ICliMemberParent.TypeCoercions
        {
            get { return null; }
        }

        ISubordinateDictionary _ICliMemberParent.UnaryOperators
        {
            get { return null; }
        }

        public ICliMetadataRoot MetadataRoot
        {
            get { return this.MetadataEntry.MetadataRoot; }
        }

        public IMember CreateItem(CliMemberType memberKind, ICliMetadataTableRow metadataEntry, IMemberUniqueIdentifier uniqueIdentifier, int index)
        {
            switch (memberKind)
            {
                case CliMemberType.Event:
                    return new Event(this, (ICliMetadataEventTableRow)metadataEntry, (IGeneralSignatureMemberUniqueIdentifier)uniqueIdentifier);
                case CliMemberType.Indexer:
                    return new Indexer(this, (ICliMetadataPropertyTableRow)metadataEntry, (IGeneralSignatureMemberUniqueIdentifier)uniqueIdentifier);
                case CliMemberType.Method:
                    return new Method(this, (ICliMetadataMethodDefinitionTableRow)metadataEntry, (IGeneralGenericSignatureMemberUniqueIdentifier)uniqueIdentifier);
                case CliMemberType.Property:
                    return new Property(this, (ICliMetadataPropertyTableRow)metadataEntry);
            }
            throw new NotSupportedException();
        }


        protected override IInterfaceType OnMakeGenericClosure(LockedTypeCollection lockedTypeParameters)
        {
            return new _InterfaceTypeBase(this, lockedTypeParameters);
        }

        public override IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
        {
            get { return this.Members.Keys.Cast<IGeneralDeclarationUniqueIdentifier>().Concat(this.Types.Keys.Cast<IGeneralDeclarationUniqueIdentifier>()); }
        }
    }
}
