using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
namespace AllenCopeland.Abstraction.Slf.Abstract
{
    internal class ModifiedType :
        TypeBase<IModifiedTypeUniqueIdentifier>,
        IModifiedType
    {
        private IType original;
        private IControlledCollection<TypeModification> modifiers;
        private IControlledCollection<IType> optionalModifiers;

        public ModifiedType(IType original, TypeModification[] modifiers)
        {
            this.original = original;
            this.modifiers = new ArrayReadOnlyCollection<TypeModification>(modifiers);
        }

        protected override IType OnGetDeclaringType()
        {
            return null;
        }

        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Modified; }
        }

        protected internal override bool CanCacheImplementsList
        {
            get { return true; }
        }

        public override TypeElementClassification ElementClassification
        {
            get
            {
                return TypeElementClassification.ModifiedType;
            }
        }

        protected override IType OnGetElementType()
        {
            return this.original;
        }

        protected override ILockedTypeCollection OnGetImplementedInterfaces()
        {
            return LockedTypeCollection.Empty;
        }

        protected override IFullMemberDictionary OnGetMembers()
        {
            return EmptyFullMemberDictionary.Empty;
        }

        protected override INamespaceDeclaration OnGetNamespace()
        {
            return this.original.Namespace;
        }

        protected override AccessLevelModifiers OnGetAccessLevel()
        {
            return this.original.AccessLevel;
        }

        protected override IAssembly OnGetAssembly()
        {
            return this.original.Assembly;
        }

        public override bool IsGenericConstruct
        {
            get { return this.original.IsGenericConstruct; }
        }

        protected override bool IsSubclassOfImpl(IType other)
        {
            return this.original.IsSubclassOf(other);
        }

        protected override string OnGetNamespaceName()
        {
            return this.original.NamespaceName;
        }

        protected override IType BaseTypeImpl
        {
            get { return this.original.BaseType; }
        }

        protected override IModifiedTypeUniqueIdentifier OnGetUniqueIdentifier()
        {
            throw new NotImplementedException();
        }

        protected override IMetadataCollection InitializeCustomAttributes()
        {
            return this.original.Metadata;
        }

        public override IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
        {
            get { return this.original.AggregateIdentifiers; }
        }

        protected override ITypeIdentityManager OnGetManager()
        {
            return this.original.IdentityManager;
        }

        protected override string OnGetName()
        {
            return this.original.Name;
        }


        #region IModifiedType Members

        public IControlledCollection<TypeModification> Modifiers
        {
            get { return this.modifiers; }
        }

        public IEnumerable<IType> RequiredModifiers
        {
            get
            {
                return GetModifiers(true);
            }
        }

        private IEnumerable<IType> GetModifiers(bool required)
        {
            return from t in this.modifiers
                   where t.IsRequiredType == required
                   select t.ModifierType;
        }

        public IEnumerable<IType> OptionalModifiers
        {
            get
            {
                return GetModifiers(false);
            }
        }

        #endregion

    }
}
