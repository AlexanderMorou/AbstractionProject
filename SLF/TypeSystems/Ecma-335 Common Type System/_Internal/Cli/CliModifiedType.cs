using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal partial class CliModifiedType :
        TypeBase<IModifiedTypeUniqueIdentifier>,
        IModifiedType
    {

        /// <summary>
        /// Returns the <see cref="ICliType"/> from which the current
        /// <see cref="CliModifiedType"/> originated.
        /// </summary>
        private ICliType original;

        /// <summary>
        /// Data member describing the modifiers associated to the
        /// modified type.
        /// </summary>
        private IControlledCollection<ICliMetadataCustomModifierSignature> modifiers;

        private ModifierCollection modifierCollection;

        public CliModifiedType(ICliType original, IControlledCollection<ICliMetadataCustomModifierSignature> modifiers)
        {
            this.original = original;
            this.modifiers = modifiers;
        }
        #region IModifiedType Members

        public IControlledCollection<TypeModification> Modifiers
        {
            get
            {
                return this._Modifiers;
            }
        }

        private ModifierCollection _Modifiers
        {
            get
            {
                if (this.modifierCollection == null)
                    this.modifierCollection = new ModifierCollection(this.modifiers);
                return this.modifierCollection;
            }
        }

        public IEnumerable<IType> RequiredModifiers
        {
            get
            {
                return this._Modifiers.RequiredModifiers;
            }
        }

        public IEnumerable<IType> OptionalModifiers
        {
            get
            {
                return this._Modifiers.OptionalModifiers;
            }
        }

        #endregion


        protected override ITypeParent OnGetParent()
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

        protected override ILockedTypeCollection OnGetImplementedInterfaces()
        {
            return LockedTypeCollection.Empty;
        }

        protected override ILockedTypeCollection OnGetDirectImplementedInterfaces()
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
    }
}
