using System;
using System.Collections;
using System.Collections.Generic;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
using AllenCopeland.Abstraction.Utilities.Common;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Cli
{
    public sealed class ArrayType :
        IArrayType
    {
        /// <summary>
        /// Data member for <see cref="ElementType"/>.
        /// </summary>
        private IType elementType;

        /// <summary>
        /// Data member for <see cref="ArrayRank"/>.
        /// </summary>
        private int rank;
        private bool isVectorArray = true;
        /// <summary>
        /// Data member for <see cref="MakeArray(Int32)"/>.
        /// </summary>
        private TypeArrayCache arrayCache;

        /// <summary>
        /// Data member for <see cref="ImplementedInterfaces"/>.
        /// </summary>
        private ILockedTypeCollection implInterfaces;

        private ByRefType byRef;

        private int[] lowerBounds;
        private ICustomAttributeCollection customAttributes;

        /// <summary>
        /// Creates a new <see cref="ArrayType"/> with the
        /// <paramref name="elementType"/> and <paramref name="rank"/>
        /// provided.
        /// </summary>
        /// <param name="elementType">The <see cref="IType"/> which represents the 
        /// elements of the <see cref="ArrayType"/>.</param>
        /// <param name="rank">The <see cref="Int32"/> which represents the number
        /// of dimensions the <see cref="ArrayType"/> has.</param>
        internal ArrayType(IType elementType, int rank)
        {
            if (elementType.ElementClassification == TypeElementClassification.Reference)
                throw new ArgumentException("elementType");

            if (rank < 1)
                throw new ArgumentOutOfRangeException("rank");
            this.rank = rank;
            this.elementType = elementType;
            this.lowerBounds = new int[this.rank];
            if (rank > 1)
                isVectorArray = false;
        }

        internal ArrayType(IType elementType)
            : this(elementType, 1)
        {

        }

        internal ArrayType(IType elementType, params int[] lowerBounds)
        {
            if (lowerBounds == null)
                throw new ArgumentNullException("lowerBounds");
            this.elementType = elementType;
            this.lowerBounds = lowerBounds;
            this.rank = lowerBounds.Length;
            isVectorArray = false;
        }

        #region IArrayType Members

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

        public int[] LowerBounds
        {
            get
            {
                int[] lb = new int[this.lowerBounds.Length];
                this.lowerBounds.CopyTo(lb, 0);
                return lb;
            }
        }

        public int ArrayRank
        {
            get { return this.rank; }
        }

        #endregion

        #region IType Members

        public IEnumerable<IDeclaration> Declarations
        {
            get { return TypeBase.EmptyDeclarations; }
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

        public IArrayType MakeArray(params int[] lowerBounds)
        {
            CacheCheck();
            return this.arrayCache.CreateArray(lowerBounds);
        }

        public IType MakePointer()
        {
            throw new InvalidOperationException(Resources.MakePointerError_ArrayType);
        }

        public IType MakeByReference()
        {
            if (this.byRef == null)
                this.byRef = new ByRefType(this);
            return this.byRef;
        }

        public IType MakeNullable()
        {
            throw new InvalidOperationException("Array types cannot be nullable value types.");
        }

        public bool IsSubclassOf(IType other)
        {
            //return false;
            return typeof(Array).GetTypeReference().Equals(other) ||
                   CommonTypeRefs.Object.Equals(other);
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
            get { return typeof(Array).GetTypeReference(); }
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
            if (this.ArrayRank == 1)
                return new LockedTypeCollection(
                        typeof(ICloneable).GetTypeReference(),
                        typeof(IList).GetTypeReference(),
                        typeof(ICollection).GetTypeReference(),
                        typeof(IEnumerable).GetTypeReference(),
                        ((IGenericType)(typeof(IList<>).GetTypeReference()))
                            .MakeGenericClosure(this.ElementType),
                        ((IGenericType)(typeof(ICollection<>).GetTypeReference()))
                            .MakeGenericClosure(this.ElementType),
                        ((IGenericType)(typeof(IEnumerable<>).GetTypeReference()))
                            .MakeGenericClosure(this.ElementType));
            else
                return new LockedTypeCollection(
                        typeof(ICloneable).GetTypeReference(),
                        typeof(IList).GetTypeReference(),
                        typeof(ICollection).GetTypeReference(),
                        typeof(IEnumerable).GetTypeReference());
        }

        public IAssembly Assembly
        {
            get { return this.ElementType.Assembly; }
        }

        public IFullMemberDictionary Members
        {
            get { return LockedFullMembersBase.Empty; }
        }

        public IEnumerable<string> AggregateIdentifiers
        {
            get
            {
                yield break;
            }
        }

        #endregion

        #region IEquatable<IType> Members

        public bool Equals(IType other)
        {
            return other == this;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                if (this.arrayCache != null)
                {
                    this.arrayCache.Dispose();
                    this.arrayCache = null;
                }
                if (this.byRef != null)
                {
                    this.byRef.Dispose();
                    this.byRef = null;
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

        #endregion

        #region IScopedDeclaration Members

        public AccessLevelModifiers AccessLevel
        {
            get { return AccessLevelModifiers.Private; }
        }

        #endregion

        #region IDeclaration Members
        public event EventHandler Disposed;

        public string Name
        {
            get { return string.Format("{0}[{1}]", this.ElementType.Name, ','.Repeat(this.ArrayRank - 1)); }
        }

        public string UniqueIdentifier
        {
            get { return this.FullName; }
        }

        #endregion
        public override string ToString()
        {
            return this.BuildTypeName(true);
        }

        private void CacheCheck()
        {
            if (this.arrayCache == null)
                this.arrayCache = new TypeArrayCache(this, k => new ArrayType(this, k), l => new ArrayType(this, l));
        }

        #region ICustomAttributedDeclaration Members

        public ICustomAttributeCollection CustomAttributes
        {
            get
            {
                if (this.customAttributes == null)
                    this.customAttributes = new LockedEmptyCustomAttributeCollection(this);
                return this.customAttributes;
            }
        }

        public bool IsDefined(IType attributeType)
        {
            return false;
        }

        #endregion

        public bool IsVectorArray
        {
            get
            {
                return this.isVectorArray;
            }
        }
    }
}
