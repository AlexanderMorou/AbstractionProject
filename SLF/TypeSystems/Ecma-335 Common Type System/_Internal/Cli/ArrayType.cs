using System;
using System.Collections;
using System.Collections.Generic;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;
/*---------------------------------------------------------------------\
| Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal sealed class ArrayType :
        IArrayType,
        _IType
    {
        /// <summary>
        /// Data member for <see cref="ElementType"/>.
        /// </summary>
        private IType elementType;

        /// <summary>
        /// Data member for <see cref="ArrayRank"/>.
        /// </summary>
        private int rank;
        private object syncObject = new object();
        /// <summary>
        /// Data member for <see cref="MakeArray(Int32)"/>.
        /// </summary>
        private TypeArrayCache arrayCache;
        private TypeModifiedCache modifiedTypeCache;

        /// <summary>
        /// Data member for <see cref="ImplementedInterfaces"/>.
        /// </summary>
        private ILockedTypeCollection implInterfaces;
        private IMetadataCollection metadata;

        private IReadOnlyCollection<int> lowerBounds;
        private IReadOnlyCollection<uint> lengths;
        private IMetadataCollection customAttributes;

        private CliManager manager;

        /// <summary>
        /// Creates a new <see cref="ArrayType"/> with the
        /// <paramref name="elementType"/> and <paramref name="rank"/>
        /// provided.
        /// </summary>
        /// <param name="elementType">The <see cref="IType"/> which represents the 
        /// elements of the <see cref="ArrayType"/>.</param>
        /// <param name="rank">The <see cref="Int32"/> which represents the number
        /// of dimensions the <see cref="ArrayType"/> has.</param>
        internal ArrayType(IType elementType, int rank, CliManager manager)
        {
            if (elementType.ElementClassification == TypeElementClassification.Reference)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.elementType, ExceptionMessageId.TypeInvalidElementType, ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.by_reference_type), ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.array));
            if (rank < 1)
                throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.rank, ExceptionMessageId.RankMustBeOneOrGreater, rank.ToString());
            this.rank = rank;
            this.elementType = elementType;
            this.manager = manager;
        }

        internal ArrayType(IType elementType, CliManager manager)
            : this(elementType, 1, manager)
        {
        }

        internal ArrayType(IType elementType, CliManager manager, int[] lowerBounds, uint[] lengths, int rank = -1)
        {
            if (lowerBounds == null)
                throw new ArgumentNullException("lowerBounds");
            this.elementType = elementType;
            this.lowerBounds = new ArrayReadOnlyCollection<int>(lowerBounds);
            this.lengths = new ArrayReadOnlyCollection<uint>(lengths);
            this.rank = rank < 0 ? Math.Max(lowerBounds == null ? 0 : lowerBounds.Length, (lengths == null) ? 0 : lengths.Length) : rank;
            this.manager = manager;
        }


        TypeArrayCache _IType.ArrayCache { get { this.CacheCheck(); return this.arrayCache; } }

        TypeModifiedCache _IType.ModifiedTypeCache { get { this.CacheCheck(); return this.modifiedTypeCache; } }


        internal CliManager IdentityManager { get { return this.manager; } }

        ITypeIdentityManager IType.IdentityManager
        {
            get
            {
                return this.elementType.IdentityManager;
            }
        }

        //#region IArrayType Members

        /// <summary>
        /// Returns whether every dimension has a zero-based index.
        /// </summary>
        public bool IsZeroBased
        {
            get
            {
                for (int i = 0; i < this.rank; i++)
                    if (this.lowerBounds[i] != 0)
                        return false;
                return true;
            }
        }

        /// <summary>
        /// Returns the <see cref="System.Int32"/> series
        /// representing the lower bound values for the 
        /// <see cref="ArrayType"/>.
        /// </summary>
        /// <remarks>
        /// Arrays which define a specialized lower bounds will have
        /// associated field, parameter, property, and return type
        /// marked with the <see cref="LowerBoundTargetAttribute"/>.
        /// </remarks>
        public IReadOnlyCollection<int> LowerBounds
        {
            get
            {
                return this.lowerBounds;
            }
        }

        /// <summary>
        /// Returns the array rank of the <see cref="ArrayType"/>.
        /// </summary>
        public int ArrayRank
        {
            get { return this.rank; }
        }

        //#endregion

        //#region IType Members

        public IEnumerable<IDeclaration> Declarations
        {
            get { return TypeBase<IGeneralTypeUniqueIdentifier>.EmptyDeclarations; }
        }

        public TypeElementClassification ElementClassification
        {
            get { return TypeElementClassification.Array; }
        }

        public IType ElementType
        {
            get { return this.elementType; }
        }

        public bool ContainsGenericParameters
        {
            get { return this.ContainsGenericParameters(); }
        }

        public bool IsGenericConstruct
        {
            get { return false; }
        }

        public bool IsGenericDefinition
        {
            get { throw new InvalidOperationException("Not a generic type."); }
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
            return this.arrayCache.CreateArray(lowerBounds, lengths);
        }

        public IType MakePointer()
        {
            throw new InvalidOperationException(Resources.MakePointerError_ArrayType);
        }

        public IType MakeByReference()
        {
            return this.IdentityManager.MakeClassificationType(this, TypeElementClassification.Reference);
        }

        public IType MakeNullable()
        {
            throw new InvalidOperationException("Array types cannot be nullable value types.");
        }

        public bool IsSubclassOf(IType other)
        {
            if (other == null)
                return false;
            //return false;
            return this.BaseType.Equals(other) ||
                   other != null && other.Equals(this.IdentityManager.ObtainTypeReference(RuntimeCoreType.RootType));
        }

        public bool IsAssignableFrom(IType target)
        {
            return target == this;
        }

        public string FullName
        {
            get { return this.BuildTypeName(); }
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
            get
            {
                return this.manager.ObtainTypeReference(this.manager.RuntimeEnvironment.GetCoreIdentifier(RuntimeCoreType.Array), elementType.Assembly);
            }
        }

        public ILockedTypeCollection ImplementedInterfaces
        {
            get
            {
                if (this.implInterfaces == null)
                    this.implInterfaces = this.InitializeImplementedInterfaces();
                return this.implInterfaces;
            }
        }

        private ILockedTypeCollection InitializeImplementedInterfaces()
        {
            var tAssem = this.Assembly;
            throw new NotSupportedException();
            //if (this.ArrayRank == 1)
            //    return new LockedTypeCollection(
            //            this.IdentityManager.ObtainTypeReference(typeof(ICloneable), tAssem),
            //            this.IdentityManager.ObtainTypeReference(typeof(IList), tAssem),
            //            this.IdentityManager.ObtainTypeReference(typeof(ICollection), tAssem),
            //            this.IdentityManager.ObtainTypeReference(typeof(IEnumerable), tAssem),
            //            ((IGenericType) this.IdentityManager.ObtainTypeReference((typeof(IList<>)), tAssem))
            //                .MakeGenericClosure(this.ElementType),
            //            ((IGenericType) this.IdentityManager.ObtainTypeReference((typeof(ICollection<>)), tAssem))
            //                .MakeGenericClosure(this.ElementType),
            //            ((IGenericType) this.IdentityManager.ObtainTypeReference((typeof(IEnumerable<>)), tAssem))
            //                .MakeGenericClosure(this.ElementType));
            //else
            //    return new LockedTypeCollection(
            //            this.IdentityManager.ObtainTypeReference(typeof(ICloneable), tAssem),
            //            this.IdentityManager.ObtainTypeReference(typeof(IList), tAssem),
            //            this.IdentityManager.ObtainTypeReference(typeof(ICollection), tAssem),
            //            this.IdentityManager.ObtainTypeReference(typeof(IEnumerable), tAssem));
        }

        public IAssembly Assembly
        {
            get { return this.ElementType.Assembly; }
        }

        public IFullMemberDictionary Members
        {
            get { return LockedFullMembersBase.Empty; }
        }

        public IMetadataCollection Metadata
        {
            get
            {
                return this.metadata ?? (metadata = new LockedEmptyMetadataCollection(this));
            }
        }

        public IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
        {
            get
            {
                yield break;
            }
        }

        //#endregion

        //#region IEquatable<IType> Members

        public bool Equals(IType other)
        {
            return other == this;
        }

        //#endregion

        //#region IDisposable Members

        public void Dispose()
        {
            try
            {
                if (this.arrayCache != null)
                {
                    this.arrayCache.Dispose();
                    this.arrayCache = null;
                }
                if (this.customAttributes != null)
                {
                    this.customAttributes.Dispose();
                    this.customAttributes = null;
                }
            }
            finally
            {
                this.OnDisposed();
                GC.SuppressFinalize(this);
            }
        }

        private void OnDisposed()
        {
            var disposeCopy = this.Disposed;
            if (disposeCopy != null)
                disposeCopy(this, EventArgs.Empty);
            this.Disposed = null;
        }

        //#endregion

        //#region IScopedDeclaration Members

        public AccessLevelModifiers AccessLevel
        {
            get { return AccessLevelModifiers.Private; }
        }

        //#endregion

        //#region IDeclaration Members

        public event EventHandler Disposed;

        public string Name
        {
            get { return string.Format("{0}[{1}]", this.ElementType.Name, ','.Repeat(this.ArrayRank - 1)); }
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

        public override string ToString()
        {
            return this.BuildTypeName(true);
        }

        private void CacheCheck()
        {
            lock (this.syncObject)
            if (this.modifiedTypeCache == null)
                this.modifiedTypeCache = new TypeModifiedCache();
            if (this.arrayCache == null)
                this.arrayCache = new TypeArrayCache(this, k => new ArrayType(this, k, this.manager), (lowerBounds, lengths) => new ArrayType(this, this.manager, lowerBounds, lengths));
        }

        //#region IMetadataEntity Members

        public IMetadataCollection CustomAttributes
        {
            get
            {
                if (this.customAttributes == null)
                    this.customAttributes = new LockedEmptyMetadataCollection(this);
                return this.customAttributes;
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

        public IReadOnlyCollection<uint> Lengths
        {
            get { return this.lengths; }
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


        public ArrayFlags Flags
        {
            get {
                ArrayFlags result = ArrayFlags.Vector;
                if (this.lengths != null && this.lengths.Count > 0)
                    result |= ArrayFlags.Multidimensional | ArrayFlags.DimensionLengths;
                if (this.lowerBounds != null && this.lowerBounds.Count > 0)
                    result |= ArrayFlags.Multidimensional | ArrayFlags.DimensionLowerBounds;
                return result;
            }
        }
    }
}
