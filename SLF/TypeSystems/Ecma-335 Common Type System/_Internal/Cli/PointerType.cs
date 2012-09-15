using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
using AllenCopeland.Abstraction.Slf.Cli;
/*---------------------------------------------------------------------\
| Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    public class PointerType :
        _IType
    {
        private IType elementType;
        private IMetadataCollection metadata;
        private CliManager manager;
        private object syncObject;
        /// <summary>
        /// Data member for <see cref="MakeArray(Int32)"/>.
        /// </summary>
        private TypeArrayCache arrayCache;
        private TypeModifiedCache modifiedTypeCache;

        internal PointerType(IType elementType, CliManager manager)
        {
            this.elementType = elementType;
            this.manager = manager;
        }

        //#region IType Members

        TypeArrayCache _IType.ArrayCache { get { this.CacheCheck(); return this.arrayCache; } }

        TypeModifiedCache _IType.ModifiedTypeCache { get { this.CacheCheck(); return this.modifiedTypeCache; } }

        public IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
        {
            get { return TypeBase<IGeneralTypeUniqueIdentifier>.EmptyIdentifiers; }
        }

        public IEnumerable<IDeclaration> Declarations
        {
            get { return TypeBase<IGeneralTypeUniqueIdentifier>.EmptyDeclarations; }
        }

        public TypeElementClassification ElementClassification
        {
            get { return TypeElementClassification.Pointer; }
        }

        public IType ElementType
        {
            get { return this.elementType; }
        }

        public bool IsGenericConstruct
        {
            get { return false; }
        }

        public bool IsGenericTypeParameter
        {
            get { return false; }
        }

        public IType DeclaringType
        {
            get { return null; }
        }

        public TypeKind Type
        {
            get { return TypeKind.Class; }
        }

        public bool IsNullable
        {
            get { return false; }
        }

        public IArrayType MakeArray(int rank)
        {
            CacheCheck();
            return arrayCache.CreateArray(rank);
        }

        public IArrayType MakeArray()
        {
            CacheCheck();
            return this.arrayCache.CreateArray();
        }

        public IArrayType MakeArray(int[] lowerBounds, uint[] lengths = null)
        {
            CacheCheck();
            return this.arrayCache.CreateArray(lowerBounds);
        }

        public IType MakePointer()
        {
            return this.Manager.MakeClassificationType(this, TypeElementClassification.Pointer);
        }

        public IType MakeByReference()
        {
            return this.Manager.MakeClassificationType(this, TypeElementClassification.Reference);
        }

        public IType MakeNullable()
        {
            throw new NotSupportedException("Pointer types cannot be nullable.");
        }

        public bool IsSubclassOf(IType other)
        {
            return false;
        }

        public bool IsAssignableFrom(IType target)
        {
            if (target.Equals(this))
                return true;
            return false;
        }

        public string FullName
        {
            get { return string.Format("{0}*", this.ElementType.FullName); }
        }

        public INamespaceDeclaration Namespace
        {
            get { return null; }
        }

        public string NamespaceName
        {
            get
            {
                return null;
            }
        }

        public IType BaseType
        {
            get { return null; }
        }

        public ILockedTypeCollection ImplementedInterfaces
        {
            get { return LockedTypeCollection.Empty; }
        }

        public IAssembly Assembly
        {
            get { return this.ElementType.Assembly; }
        }

        public IFullMemberDictionary Members
        {
            get { return LockedFullMembersBase.Empty; }
        }

        //#endregion

        //#region IEquatable<IType> Members

        public bool Equals(IType other)
        {
            return other.ElementClassification == TypeElementClassification.Pointer
                && this.ElementType.Equals(other.ElementType);
        }

        //#endregion

        //#region IDisposable Members

        public void Dispose()
        {
            try
            {
                this.elementType = null;
                if (this.arrayCache != null)
                {
                    this.arrayCache.Dispose();
                    this.arrayCache = null;
                }
                if (this.metadata != null)
                {
                    this.metadata.Dispose();
                    this.metadata = null;
                }
            }
            finally
            {
                this.OnDisposed();
                this.Disposed = null;
                GC.SuppressFinalize(this);
            }
        }

        //#endregion

        //#region IScopedDeclaration Members

        public AccessLevelModifiers AccessLevel
        {
            get { return AccessLevelModifiers.Private; }
        }

        //#endregion

        private void OnDisposed()
        {
            if (this.Disposed != null)
                this.Disposed(this, EventArgs.Empty);
        }

        //#region IDeclaration Members

        public event EventHandler Disposed;

        public string Name
        {
            get { return string.Format("{0}*", this.ElementType.Name); }
        }

        public IGeneralTypeUniqueIdentifier UniqueIdentifier
        {
            get { return this.elementType.UniqueIdentifier; }
        }

        IGeneralDeclarationUniqueIdentifier IDeclaration.UniqueIdentifier
        {
            get { return this.UniqueIdentifier; }
        }
        //#endregion

        //#region IMetadataEntity Members

        public IMetadataCollection Metadata
        {
            get
            {
                if (this.metadata == null)
                    this.metadata = new LockedEmptyMetadataCollection(this);
                return this.metadata;
            }
        }

        public bool IsDefined(IType metadatumType)
        {
            return false;
        }

        public bool IsDefined(IType metadatumType, bool inherited)
        {
            return false;
        }

        //#endregion

        public ITypeIdentityManager Manager { get { return this.manager; } }

        public override string ToString()
        {
            return this.BuildTypeName(true);
        }

        private void CacheCheck()
        {
            lock (this.syncObject)
            {
                if (this.arrayCache == null)
                    this.arrayCache = new TypeArrayCache(this, k => new ArrayType(this, k, this.manager), (lowerBounds, lengths) => new ArrayType(this, this.manager, lowerBounds, lengths));
                if (this.modifiedTypeCache == null)
                    this.modifiedTypeCache = new TypeModifiedCache();
            }
        }

        public IModifiedType MakeModified(TypeModification[] modifiers)
        {
            IModifiedType result;
            lock (this.syncObject)
            {
                this.CacheCheck();
                var modifiedTypeKey = new TypeModifierSetEntry(modifiers);
                if (!this.modifiedTypeCache.TryObtainConstruct(modifiedTypeKey, out result))
                    this.modifiedTypeCache.RegisterConstruct(modifiedTypeKey, result = new ModifiedType(this, modifiers));
            }
            return result;
        }


    }
}
