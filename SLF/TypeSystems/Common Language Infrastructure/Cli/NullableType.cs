using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Cli
{
    public class NullableType :
        IType
    {
        /// <summary>
        /// Data member for <see cref="ElementType"/>.
        /// </summary>
        private IType elementType;
        /// <summary>
        /// Data member for <see cref="Members"/>.
        /// </summary>
        private IFullMemberDictionary members;

        /// <summary>
        /// Data member for <see cref="MakeArray(Int32)"/>.
        /// </summary>
        private TypeArrayCache arrayCache;

        private static string _NamespaceName = "System";

        private PointerType pointer;
        private ByRefType byRef;
        private ICustomAttributeCollection customAttributes;
        private static INamespaceDeclaration @namespace;

        /// <summary>
        /// Creates a new <see cref="NullableType"/> with the 
        /// given <paramref name="elementType"/>.
        /// </summary>
        /// <param name="elementType">The <see cref="IType"/>
        /// which is a <see cref="TypeKind.Struct"/>
        /// to be made into a <see cref="NullableType"/>.</param>
        internal NullableType(IType elementType)
        {
            if (!(elementType.ElementClassification == TypeElementClassification.None ||
                  elementType.ElementClassification == TypeElementClassification.GenericTypeDefinition))
                throw new ArgumentException("Nullable types must be normal types, array, by-reference, nullable, and pointer types are not allowed.");
            if (elementType.Type != TypeKind.Struct &&
                !(elementType is IGenericParameter &&
                  ((IGenericParameter)(elementType)).SpecialConstraint == GenericTypeParameterSpecialConstraint.Struct))
                throw new ArgumentException("elementType");
            this.elementType = elementType;
        }

        #region IType Members

        public TypeElementClassification ElementClassification
        {
            get { return TypeElementClassification.Nullable; }
        }

        public IType ElementType
        {
            get { return this.elementType; }
        }

        public bool ContainsGenericParameters
        {
            get { return this.ContainsGenericParameters(); }
        }

        public bool IsGenericType
        {
            get { return false; }
        }

        public bool IsGenericTypeDefinition
        {
            get { throw new InvalidOperationException("Not a generic type."); }
        }

        public bool IsGenericTypeParameter
        {
            get { return false; }
        }

        public ITypeCollection GenericParameters
        {
            get { throw new NotSupportedException("Not a generic type"); }
        }

        public IType DeclaringType
        {
            get { return null; }
        }

        public IType MakeGenericType(ITypeCollection typeParameters)
        {
            throw new InvalidOperationException("Not a generic type.");
        }

        public IType MakeGenericType(params IType[] typeParameters)
        {
            throw new InvalidOperationException("Not a generic type.");
        }

        public IType MakeGenericType(params Type[] typeParameters)
        {
            throw new InvalidOperationException("Not a generic type.");
        }

        public IType GetGenericTypeDefinition()
        {
            throw new InvalidOperationException("Not a generic type.");
        }

        public TypeKind Type
        {
            get { return TypeKind.Struct; }
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
            throw new InvalidOperationException("Cannot make a nullable type nullable.");
        }

        public bool IsSubclassOf(IType other)
        {
            if (other.Equals(typeof(ValueType).GetTypeReference()))
                return true;
            return false;
        }

        public bool IsAssignableFrom(IType target)
        {
            return this.Equals(target);
        }

        public string FullName
        {
            get { return string.Format("{0}?",this.ElementType.FullName); }
        }

        public INamespaceDeclaration Namespace
        {
            get
            {
                return _Namespace;
            }
        }

        public string NamespaceName
        {
            get
            {
                return _NamespaceName;
            }
        }

        private static INamespaceDeclaration _Namespace
        {
            get
            {
                if (@namespace == null)
                {
                    var assem = typeof(Nullable<>).Assembly.GetAssemblyReference();
                    @namespace = assem.Namespaces[_NamespaceName];
                    @namespace.Disposed += new EventHandler(namespace_Disposed);
                }
                return @namespace;
            }
        }

        static void namespace_Disposed(object sender, EventArgs e)
        {
            if (@namespace != null)
            {
                @namespace.Disposed -= new EventHandler(namespace_Disposed);
                @namespace = null;
            }
            else if (sender is INamespaceDeclaration)
            {
                var ns = (INamespaceDeclaration)sender;
                ns.Disposed -= new EventHandler(namespace_Disposed);
                ns = null;
            }
        }

        public IType BaseType
        {
            get { return typeof(ValueType).GetTypeReference(); }
        }

        public ILockedTypeCollection ImplementedInterfaces
        {
            get { return LockedTypeCollection.Empty; }
        }

        public IAssembly Assembly
        {
            get { return typeof(Nullable<>).Assembly.GetAssemblyReference(); }
        }

        public IFullMemberDictionary Members
        {
            get {
                if (members == null)
                    members = this.InitializeMembers();
                return this.members;
            }
        }

        private IFullMemberDictionary InitializeMembers()
        {
            IGenericType u = (IGenericType)typeof(Nullable<>).GetTypeReference();
            u = u.MakeGenericType(this.elementType);
            return u.Members;
        }
        #endregion

        #region IEquatable<IType> Members

        public bool Equals(IType other)
        {
            return (other.ElementClassification == TypeElementClassification.Nullable &&
                this.ElementType.Equals(other.ElementType));
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                if (this.arrayCache != null)
                    this.arrayCache.Dispose();
                if (this.byRef != null)
                {
                    this.byRef.Dispose();
                    this.byRef = null;
                }
                if (this.pointer != null)
                {
                    this.pointer.Dispose();
                    this.pointer = null;
                }
                if (this.customAttributes != null)
                {
                    this.customAttributes.Dispose();
                    this.customAttributes = null;
                }
                this.members = null;
                this.elementType = null;
            }
            finally
            {
                this.OnDisposed();
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
            get { return string.Format("{0}?", this.ElementType.Name); }
        }

        public string UniqueIdentifier
        {
            get { return this.FullName; }
        }

        #endregion

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

        public override string ToString()
        {
            return this.BuildTypeName(true);
        }

    }
}
