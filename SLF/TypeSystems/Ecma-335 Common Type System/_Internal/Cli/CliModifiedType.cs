using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal partial class CliModifiedType :
        TypeBase<IModifiedTypeUniqueIdentifier>,
        IModifiedType
    {

        /// <summary>
        /// Returns the <see cref="_ICliType"/> from which the current
        /// <see cref="CliModifiedType"/> originated.
        /// </summary>
        private _ICliType original;

        /// <summary>
        /// Data member describing the modifiers associated to the
        /// modified type.
        /// </summary>
        private IReadOnlyCollection<ICliMetadataCustomModifierSignature> modifiers;

        private ModifierCollection modifierCollection;

        public CliModifiedType(_ICliType original, IReadOnlyCollection<ICliMetadataCustomModifierSignature> modifiers)
        {
            this.original = original;
            this.modifiers = modifiers;
        }
        #region IModifiedType Members

        public IReadOnlyCollection<TypeModification> Modifiers
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


        protected override IType OnGetDeclaringType()
        {
            return null;
        }

        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Modified; }
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

        protected override IArrayType OnMakeArray(int rank)
        {
            throw new NotSupportedException("Modified types are the final order of type, and thus no array-based derivations can exist.");
        }

        protected override IArrayType OnMakeArray(params int[] lowerBounds)
        {
            return OnMakeArray(0);
        }

        protected override IType OnMakeByReference()
        {
            throw new NotSupportedException("Modified types are the final order of type, and thus no by-reference based derivations can exist.");
        }

        protected override IType OnMakePointer()
        {
            throw new NotSupportedException("Modified types are the final order of type, and thus no pointer based derivations can exist.");
        }

        protected override IType OnMakeNullable()
        {
            throw new NotSupportedException("Modified types are the final order of type, and thus no nullable based derivations can exist.");
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
            return this.original.Manager;
        }

        protected override string OnGetName()
        {
            return this.original.Name;
        }
    }
}
