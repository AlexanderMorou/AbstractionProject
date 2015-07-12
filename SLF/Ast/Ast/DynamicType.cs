using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Ast.Properties;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf._Internal.Cli;

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public class DynamicType :
        TypeBase<IGeneralTypeUniqueIdentifier, DynamicType>
    {
        private IIdentityManager manager;

        private DynamicType(IIdentityManager manager) { this.manager = manager; }

        protected override bool Equals(DynamicType other)
        {
            return object.ReferenceEquals(this, other);
        }

        protected override ITypeParent OnGetParent()
        {
            return null;
        }

        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Dynamic; }
        }

        protected override bool CanCacheImplementsList
        {
            get { return true; }
        }

        protected override ILockedTypeCollection OnGetImplementedInterfaces()
        {
            return LockedTypeCollection.Empty;
        }

        protected override IFullMemberDictionary OnGetMembers()
        {
            return LockedFullMembersBase.Empty;
        }

        protected override INamespaceDeclaration OnGetNamespace()
        {
            return null;
        }

        protected override AccessLevelModifiers OnGetAccessLevel()
        {
            return AccessLevelModifiers.PrivateScope;
        }

        protected override IAssembly OnGetAssembly()
        {
            return null;
        }

        public override bool IsGenericConstruct
        {
            get { return false; }
        }

        protected override bool IsSubclassOfImpl(IType other)
        {
            return false;
        }

        protected override string OnGetNamespaceName()
        {
            return null;
        }

        protected override IType BaseTypeImpl
        {
            get { return null; }
        }

        protected override IMetadataCollection InitializeMetadata()
        {
            return null;
        }

        public override IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
        {
            get { return TypeBase<IGeneralTypeUniqueIdentifier>.EmptyIdentifiers; }
        }

        protected override string OnGetName()
        {
            return "dynamic";
        }

        protected override IGeneralTypeUniqueIdentifier OnGetUniqueIdentifier()
        {
            return TypeSystemIdentifiers.GetTypeIdentifier((IGeneralDeclarationUniqueIdentifier)null, "dynamic");
        }

        protected override IIdentityManager OnGetManager()
        {
            return this.manager;
        }

        protected override ILockedTypeCollection OnGetDirectImplementedInterfaces()
        {
            return LockedTypeCollection.Empty;
        }
    }
}
