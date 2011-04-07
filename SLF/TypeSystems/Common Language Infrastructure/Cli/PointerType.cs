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
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Cli
{
    public class PointerType :
        IType
    {
        private IType elementType;
        private ByRefType byRef;
        private PointerType pointer;
        private ICustomAttributeCollection customAttributes;
        /// <summary>
        /// Data member for <see cref="MakeArray(Int32)"/>.
        /// </summary>
        private TypeArrayCache arrayCache;

        internal PointerType(IType elementType)
        {
            this.elementType = elementType;
        }

        #region IType Members

        public IEnumerable<string> AggregateIdentifiers
        {
            get { return TypeBase.EmptyIdentifiers; }
        }

        public IEnumerable<IDeclaration> Declarations
        {
            get { return TypeBase.EmptyDeclarations; }
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

        public IArrayType MakeArray(params int[] lowerBounds)
        {
            CacheCheck();
            return this.arrayCache.CreateArray(lowerBounds);
        }

        public IType MakePointer()
        {
            if (this.pointer == null)
                this.pointer = new PointerType(this);
            return this.pointer;
        }

        public IType MakeByReference()
        {
            if (this.byRef == null)
                this.byRef = new ByRefType(this);
            return this.byRef;
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

        #endregion

        #region IEquatable<IType> Members

        public bool Equals(IType other)
        {
            return other.ElementClassification == TypeElementClassification.Pointer
                && this.ElementType.Equals(other.ElementType);
        }

        #endregion

        #region IDisposable Members

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
                if (this.pointer != null)
                {
                    this.pointer.Dispose();
                    this.pointer = null;
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
            finally { 
                this.OnDisposed();
                this.Disposed = null;
                GC.SuppressFinalize(this);
            }
        }

        #endregion

        #region IScopedDeclaration Members

        public AccessLevelModifiers AccessLevel
        {
            get { return AccessLevelModifiers.Private; }
        }

        #endregion

        private void OnDisposed()
        {
            if (this.Disposed != null)
                this.Disposed(this, EventArgs.Empty);
        }

        #region IDeclaration Members

        public event EventHandler Disposed;

        public string Name
        {
            get { return string.Format("{0}*", this.ElementType.Name); }
        }

        public string UniqueIdentifier
        {
            get { return this.FullName; }
        }

        #endregion

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

        public override string ToString()
        {
            return this.BuildTypeName(true);
        }

        private void CacheCheck()
        {
            if (this.arrayCache == null)
                this.arrayCache = new TypeArrayCache(this, k => new ArrayType(this, k), l => new ArrayType(this, l));
        }

    }
}
