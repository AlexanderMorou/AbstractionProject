using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
using System.ComponentModel;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Provides a base class for <see cref="IType"/> implementations.
    /// </summary>
    [DebuggerDisplay("Name = {UniqueIdentifierString}, FullName = {FullName}")]
    public abstract class TypeBase<TIdentifier> :
        DeclarationBase<TIdentifier>,
        IType
        where TIdentifier :
            ITypeUniqueIdentifier
    {
        internal static IEnumerable<IDeclaration> EmptyDeclarations = GetEmptyDeclarations();
        internal static IEnumerable<IGeneralDeclarationUniqueIdentifier> EmptyIdentifiers = GetEmptyIdentifiers();
        /// <summary>
        /// Data member for <see cref="CustomAttributes"/>.
        /// </summary>
        private IMetadataCollection customAttributes;
        /// <summary>
        /// Data member for the pointer cache.
        /// </summary>
        private IType pointer;
        /// <summary>
        /// Data member for the nullable cache.
        /// </summary>
        private IType nullable;
        /// <summary>
        /// Data member for the byreference cache.
        /// </summary>
        private IType byRefType;
        /// <summary>
        /// Data member for <see cref="MakeArray(Int32)"/>.
        /// </summary>
        private TypeArrayCache arrayCache;
        /// <summary>
        /// Data member for <see cref="ImplementedInterfaces"/>.
        /// </summary>
        private ILockedTypeCollection implementedInterfaces;

        /// <summary>
        /// Returns the <see cref="IType"/> from which the current
        /// <see cref="TypeBase{TIdentifier}"/> is declared.
        /// </summary>
        /// <returns>An <see cref="IType"/> instance denoting
        /// the current <see cref="TypeBase{TIdentifier}"/>'s point 
        /// of declaration.</returns>
        protected abstract IType OnGetDeclaringType();
        /// <summary>
        /// Returns the <see cref="TypeKind"/> the <see cref="TypeBase{TIdentifier}"/>
        /// is.
        /// </summary>
        protected abstract TypeKind TypeImpl { get; }
        /// <summary>
        /// 
        /// </summary>
        protected abstract bool CanCacheImplementsList { get; }

        /// <summary>
        /// Returns the <see cref="ITypeCollection"/> which represents
        /// the interfaces implemented by the current <see cref="TypeBase{TIdentifier}"/>.
        /// </summary>
        /// <returns>A <see cref="ITypeCollection"/> instance.</returns>
        protected abstract ILockedTypeCollection OnGetImplementedInterfaces();

        /// <summary>
        /// Returns the <see cref="IFullMemberDictionary"/> for the current
        /// <see cref="TypeBase{TIdentifier}"/>.
        /// </summary>
        /// <returns>An <see cref="IFullMemberDictionary"/> instance.</returns>
        protected abstract IFullMemberDictionary OnGetMembers();

        /// <summary>
        /// Returns the namespace the current <see cref="TypeBase{TIdentifier}"/> belongs to.
        /// </summary>
        /// <returns>A <see cref="INamespaceDeclaration"/> value representing the namespace of 
        /// the current <see cref="TypeBase{TIdentifier}"/>.</returns>
        protected abstract INamespaceDeclaration OnGetNamespace();

        /// <summary>
        /// Returns the <see cref="AccessLevelModifiers"/> which determine
        /// the accessibility of the current <see cref="TypeBase{TIdentifier}"/>.
        /// </summary>
        /// <returns>A <see cref="AccessLevelModifiers"/> value which determine
        /// the accessibility of the current <see cref="TypeBase{TIdentifier}"/>.</returns>
        protected abstract AccessLevelModifiers OnGetAccessLevel();

        /// <summary>
        /// Implementation version of <see cref="Assembly"/> which obtains 
        /// the <see cref="IAssembly"/> associated
        /// to the current <see cref="TypeBase{TIdentifier}"/>.
        /// </summary>
        /// <returns>A <see cref="IAssembly"/> instance
        /// of the assembly that defines the
        /// <see cref="TypeBase{TIdentifier}"/>.</returns>
        protected abstract IAssembly OnGetAssembly();

        /// <summary>
        /// Implementation version of <see cref="MakeArray(Int32)"/> which creates 
        /// a new <see cref="IArrayType"/> with the 
        /// <paramref name="rank"/> provided.
        /// </summary>
        /// <param name="rank">
        /// The array rank or number of dimensions.</param>
        /// <returns>A new <see cref="IArrayType"/> as an array 
        /// with the <paramref name="rank"/> provided.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="rank"/> is zero or below.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when the current <see cref="TypeBase{TIdentifier}"/>
        /// is a pointer or by-reference type.</exception>
        protected abstract IArrayType OnMakeArray(int rank);

        /// <summary>
        /// Implementation version of <see cref="MakeArray(Int32[])"/> which 
        /// creates a new non-standard multi-dimensional 
        /// or single-dimension array with the 
        /// <paramref name="lowerBounds"/> of each dimension
        /// specified.
        /// </summary>
        /// <param name="lowerBounds">The <see cref="Int32"/> which
        /// represents the lower-bounds of the <see cref="IArrayType"/> resulted.</param>
        /// <returns>A <see cref="IArrayType"/> </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="lowerBounds"/>
        /// is null.</exception>
        /// <exception cref="System.ArgumentException"><paramref name="lowerBounds"/>
        /// had zero elements.</exception>
        protected abstract IArrayType OnMakeArray(params int[] lowerBounds);

        /// <summary>
        /// Implementation version of <see cref="MakeByReference"/> which creates 
        /// a new pointer <see cref="IType"/>.
        /// </summary>
        /// <returns>A new <see cref="IType"/> by reference.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when the current
        /// <see cref="IType"/> is already a by-reference type.</exception>
        protected abstract IType OnMakeByReference();

        /// <summary>
        /// Implementation version of <see cref="MakePointer"/> which creates a new 
        /// pointer <see cref="IType"/>.
        /// </summary>
        /// <returns>A new <see cref="IType"/> as a pointer type.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when the current
        /// <see cref="IType"/> is a by-reference type.</exception>
        protected abstract IType OnMakePointer();

        /// <summary>
        /// Makes the current <see cref="TypeBase{TIdentifier}"/> as a 
        /// nullable <see cref="IType"/>.
        /// </summary>
        /// <returns>A <see cref="IType"/> instance
        /// which represents the current <see cref="TypeBase{TIdentifier}"/>
        /// as a nullable type.</returns>
        protected abstract IType OnMakeNullable();

        private object syncObject = new object();

        private void CacheCheck()
        {
            lock (this.syncObject)
                if (this.arrayCache == null)
                    this.arrayCache = new TypeArrayCache(this, this.OnMakeArray, this.OnMakeArray);
        }

        protected virtual IType OnGetElementType()
        {
            throw new InvalidOperationException("Available on closed generic, by-reference, pointer, and array types only.");
        }

        #region IType Members

        /// <summary>
        /// Returns the special classification given to <see cref="ElementType"/>.
        /// </summary>
        public virtual TypeElementClassification ElementClassification
        {
            get { return TypeElementClassification.None; }
        }

        /// <summary>
        /// Returns the element type of special classification types.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">thrown when
        /// <see cref="ElementClassification"/> is <see cref="TypeElementClassification.None"/>.</exception>
        public IType ElementType
        {
            get
            {
                return this.OnGetElementType();
            }
        }

        /// <summary>
        /// Returns whether the current type is a generic type with generic parameters.
        /// </summary>
        public abstract bool IsGenericConstruct { get; }

        /// <summary>
        /// Returns the <see cref="IType"/> in which the current <see cref="TypeBase{TIdentifier}"/> is declared.
        /// </summary>
        public IType DeclaringType
        {
            get { return this.OnGetDeclaringType(); }
        }

        /// <summary>
        /// Returns the kind of type the <see cref="TypeBase{TIdentifier}"/> is.
        /// </summary>
        public TypeKind Type
        {
            get { return TypeImpl; }
        }

        /// <summary>
        /// Returns whether the current <see cref="TypeBase{TIdentifier}"/> is nullable.
        /// </summary>
        public bool IsNullable
        {
            get { return OnGetIsNullable(); }
        }

        protected virtual bool OnGetIsNullable()
        {
            return !(this is IReferenceType);
        }

        /// <summary>
        /// Creates a new <see cref="IArrayType"/> with the <paramref name="rank"/> provided.
        /// </summary>
        /// <param name="rank">The array rank.</param>
        /// <returns>A new <see cref="IType"/> as an array with the 
        /// <paramref name="rank"/> provided.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="rank"/> is zero or below.</exception>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when the current <see cref="IType"/> is a pointer 
        /// or by-reference type.</exception>
        public IArrayType MakeArray(int rank)
        {
            CacheCheck();
            lock (this.syncObject)
                return arrayCache.CreateArray(rank);
        }

        /// <summary>
        /// Creates a new single-dimension <see cref="IArrayType"/>.
        /// </summary>
        /// <returns>A new single-dimension <see cref="IArrayType"/>.</returns>
        public IArrayType MakeArray()
        {
            return this.MakeArray(1);
        }

        /// <summary>
        /// Creates a new non-standard multi-dimensional 
        /// or single-dimension array with the 
        /// <paramref name="lowerBounds"/> of each dimension
        /// specified.
        /// </summary>
        /// <param name="lowerBounds">The <see cref="Int32"/> which
        /// represents the lower-bounds of the <see cref="IArrayType"/> resulted.</param>
        /// <returns>A <see cref="IArrayType"/> </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="lowerBounds"/>
        /// is null.</exception>
        /// <exception cref="System.ArgumentException"><paramref name="lowerBounds"/>
        /// had zero elements.</exception>
        public IArrayType MakeArray(params int[] lowerBounds)
        {
            CacheCheck();
            lock (this.syncObject)
                return this.arrayCache.CreateArray(lowerBounds);
        }

        /// <summary>
        /// Creates a new pointer <see cref="IType"/>.
        /// </summary>
        /// <returns>A new <see cref="IType"/> as a pointer type.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when the current
        /// <see cref="IType"/> is a by-reference type.</exception>
        public IType MakePointer()
        {
            lock (this.syncObject)
            {
                if (this.pointer == null)
                    this.pointer = this.OnMakePointer();
                return this.pointer;
            }
        }

        /// <summary>
        /// Creates a new pointer <see cref="IType"/>.
        /// </summary>
        /// <returns>A new <see cref="IType"/> by reference.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when the current
        /// <see cref="IType"/> is already a by-reference type.</exception>
        public IType MakeByReference()
        {
            if (this.ElementClassification == TypeElementClassification.Reference)
                throw new InvalidOperationException("Type is already a by-reference type.");
            lock (this.syncObject)
            {
                if (this.byRefType == null)
                    this.byRefType = this.OnMakeByReference();
                return this.byRefType;
            }
        }

        /// <summary>
        /// Creates a new nullable <see cref="IType"/>.
        /// </summary>
        /// <returns>A new <see cref="IType"/> as a nullable type relative to the current
        /// <see cref="TypeBase{TIdentifier}"/>.</returns>
        /// <exception cref="System.InvalidOperationException">thrown when the current <see cref="IType"/>
        /// is a poinoter, array, generic type definition, by-reference, or when 
        /// <see cref="Type"/> is something other than <see cref="TypeKind.Struct"/> or when the
        /// special restriction on a generic parameter does not include struct as a condition.</exception>
        public IType MakeNullable()
        {
            if (this.ElementClassification == TypeElementClassification.Nullable)
                throw new InvalidOperationException("Type is already nullable.");
            lock (this.syncObject)
            {
                if (this.nullable == null)
                    this.nullable = this.OnMakeNullable();
                return this.nullable;
            }
        }

        /// <summary>
        /// Returns whether the <see cref="TypeBase{TIdentifier}"/> is a 
        /// sub-class of the <paramref name="other"/> type provided.
        /// </summary>
        /// <param name="other">The <see cref="IType"/> to check for in the
        /// current <see cref="TypeBase{TIdentifier}"/> hierarchy.</param>
        /// <returns>true if <paramref name="other"/> is contained within the 
        /// current <see cref="TypeBase{TIdentifier}"/> inheritance hierarchy.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="other"/> is null.</exception>
        public bool IsSubclassOf(IType other)
        {
            if (other == null)
                throw new ArgumentNullException("other");
            for (IType temp = this; temp != null; temp = temp.BaseType)
                if (temp.Equals(other))
                    return true;

            return IsSubclassOfImpl(other) && other != this;
        }

        /// <summary>
        /// Implementation version of <see cref="IsSubclassOf(IType)"/>
        /// which returns whether the <see cref="TypeBase{TIdentifier}"/> is a 
        /// sub-class of the <paramref name="other"/> type provided.
        /// </summary>
        /// <param name="other">The <see cref="IType"/> to check for in the
        /// current <see cref="TypeBase{TIdentifier}"/> hierarchy.</param>
        /// <returns>true if <paramref name="other"/> is contained within the 
        /// current <see cref="TypeBase{TIdentifier}"/> inheritance hierarchy.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="other"/> is null.</exception>
        protected abstract bool IsSubclassOfImpl(IType other);

        /// <summary>
        /// Returns whether the current <see cref="TypeBase{TIdentifier}"/> is 
        /// assignable from the <paramref name="target"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="IType"/> to see if 
        /// the current type is assignable from.</param>
        /// <returns>
        /// true if <paramref name="target"/> contains the current 
        /// <see cref="TypeBase{TIdentifier}"/> in its implemented interfaces, 
        /// if <paramref name="target"/> inherits the current 
        /// <see cref="TypeBase{TIdentifier}"/>, or if <paramref name="target"/> 
        /// equals the current <see cref="TypeBase{TIdentifier}"/>; 
        /// false otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="target"/> is null.</exception>
        public virtual bool IsAssignableFrom(IType target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (target.Equals(this))
                return true;
            if (target.IsSubclassOf(this))
                return true;
            return target.ImplementedInterfaces.Any(t => t.Equals(this));
        }

        /// <summary>
        /// Returns the full name of the <see cref="TypeBase{TIdentifier}"/>.
        /// </summary>
        public virtual string FullName
        {
            get { return this.BuildTypeName(); }
        }

        /// <summary>
        /// Returns the namespace in which the <see cref="TypeBase{TIdentifier}"/> is declared.
        /// </summary>
        [DebuggerDisplay("{NamespaceName}")]
        public INamespaceDeclaration Namespace
        {
            get { return this.OnGetNamespace(); }
        }

        /// <summary>
        /// Returns the name of the namespace in which the 
        /// <see cref="TypeBase{TIdentifier}"/> is declared.
        /// </summary>
        public string NamespaceName
        {
            get
            {
                return this.OnGetNamespaceName();
            }
        }

        protected abstract string OnGetNamespaceName();

        /// <summary>
        /// Returns the base type of the current <see cref="TypeBase{TIdentifier}"/>.
        /// </summary>
        public IType BaseType { get { return this.BaseTypeImpl; } }

        /// <summary>
        /// Implementation version of <see cref="BaseType"/> which 
        /// returns the base type of the current <see cref="TypeBase{TIdentifier}"/>
        /// </summary>
        protected abstract IType BaseTypeImpl { get; }

        /// <summary>
        /// Returns a collection of <see cref="IType"/> instances that
        /// are implemented by the current
        /// <see cref="TypeBase{TIdentifier}"/>.
        /// </summary>
        public ILockedTypeCollection ImplementedInterfaces
        {
            get
            {
                if (this.CanCacheImplementsList)
                {
                    lock (this.SyncObject)
                    {
                        if (this.implementedInterfaces == null)
                            this.implementedInterfaces = this.OnGetImplementedInterfaces();
                        return this.implementedInterfaces;
                    }
                }
                else
                    return this.OnGetImplementedInterfaces();
            }
        }

        /// <summary>
        /// Returns the <see cref="IAssembly"/> in which the <see cref="TypeBase{TIdentifier}"/> is declared
        /// </summary>
        public IAssembly Assembly
        {
            get { return this.OnGetAssembly(); }
        }

        /// <summary>
        /// Returns the <see cref="IFullMemberDictionary"/> of 
        /// a series of <see cref="IGroupedMemberDictionary"/>.
        /// </summary>
        /// <exception cref="System.NotSupportedException">The <see cref="IType"/>
        /// does not support members.</exception>
        public IFullMemberDictionary Members
        {
            get { return OnGetMembers(); }
        }

        #endregion

        #region IEquatable<IType> Members

        /// <summary>
        /// Determines whether the <paramref name="other"/>
        /// <see cref="IType"/> is equal to
        /// the current <see cref="TypeBase{TIdentifier}"/>.
        /// </summary>
        /// <param name="other">The other <see cref="IType"/>
        /// to check against.</param>
        /// <returns>true if the <see cref="IType"/> is
        /// equal to the current <see cref="TypeBase{TIdentifier}"/>.</returns>
        public bool Equals(IType other)
        {
            if (other.GetType() != this.GetType())
                return false;
            return object.ReferenceEquals(this, other);
        }

        #endregion

        #region IScopedDeclaration Members

        /// <summary>
        /// Returns the access level of the <see cref="TypeBase{TIdentifier}"/>.
        /// </summary>
        public AccessLevelModifiers AccessLevel
        {
            get { return this.OnGetAccessLevel(); }
        }

        #endregion

        /// <summary>
        /// Returns whether the <see cref="TypeBase{TIdentifier}"/> is 
        /// an <see cref="IGenericParameter"/>.
        /// </summary>
        public bool IsGenericTypeParameter
        {
            get { return this is IGenericParameter; }
        }

        /// <summary>
        /// Performs application-defined tasks associated with 
        /// freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            //Dispose
            lock (this.syncObject)
            {
                try
                {
                    if (this.arrayCache != null)
                        this.arrayCache.Dispose();
                    if (this.byRefType != null)
                    {
                        this.byRefType.Dispose();
                        this.byRefType = null;
                    }
                    if (this.nullable != null)
                    {
                        this.nullable.Dispose();
                        this.nullable = null;
                    }
                    if (this.pointer != null)
                    {
                        this.pointer.Dispose();
                        this.pointer = null;
                    }
                    GC.SuppressFinalize(this);
                }
                finally
                {
                    this.OnDisposed();
                }
            }
        }

        /// <summary>
        /// Returns the unique identifier for the current <see cref="TypeBase{TIdentifier}"/> where 
        /// <see cref="DeclarationBase.Name"/> is not enough to distinguish between two 
        /// <see cref="TypeBase{TIdentifier}"/> entities.
        /// </summary>
        public override sealed TIdentifier UniqueIdentifier
        {
            get
            {
                return this.OnGetUniqueIdentifier();
            }
        }

        /// <summary>
        /// Obtains the unique identifier for the current <see cref="TypeBase{TIdentifier}"/>.
        /// </summary>
        /// <returns>A <typeparamref name="TIdentifier"/> which properly
        /// represents the <see cref="TypeBase{TIdentifier}"/>.</returns>
        /// <remarks>Inheritors are required to implement this method
        /// if not abstract.</remarks>
        protected abstract TIdentifier OnGetUniqueIdentifier();

        /// <summary>
        /// Converts the <see cref="TypeBase{TIdentifier}"/> to a <see cref="String"/>
        /// representation.
        /// </summary>
        /// <returns>The full name of the type as a string.</returns>
        public override string ToString()
        {
            return this.BuildTypeName(true);
        }

        #region IMetadataEntity Members

        /// <summary>
        /// Returns the <see cref="IMetadataCollection"/> associated to the
        /// <see cref="TypeBase{TIdentifier}"/>.
        /// </summary>
        public IMetadataCollection CustomAttributes
        {
            get
            {
                lock (this.syncObject)
                {
                    if (this.customAttributes == null)
                        this.customAttributes = this.InitializeCustomAttributes();
                    return this.customAttributes;
                }
            }
        }

        /// <summary>
        /// Determines whether the <see cref="IType"/>
        /// is defined within the custom attributes of the 
        /// <see cref="TypeBase{TIdentifier}"/>.
        /// </summary>
        /// <param name="metadatumType">The <see cref="IType"/> which determines
        /// the </param>
        /// <returns>true if <see cref="metadatumType"/> can be assigned to
        /// from a type of one of the custom attributes contained within 
        /// the <see cref="TypeBase{TIdentifier}"/>.</returns>
        public bool IsDefined(IType metadatumType)
        {
            return this.StandardIsDefined(metadatumType);
        }

        public bool IsDefined(IType metadatumType, bool inherited)
        {
            bool? canInherit = null;
            for (IType targetType = this; targetType != null; targetType = targetType.BaseType)
            {
                if (targetType.IsDefined(metadatumType))
                    return true;
                if (canInherit == null)
                    canInherit = this.Manager.IsMetadatumInheritable(metadatumType);
                else if (!canInherit.Value)
                    return false;
            }
            return false;
        }

        #endregion

        /// <summary>
        /// Initializes the <see cref="CustomAttributes"/> for the current
        /// <see cref="TypeBase{TIdentifier}"/>.
        /// </summary>
        /// <returns>A <see cref="IMetadataCollection"/> of
        /// attributes relative to the current instance.</returns>
        protected abstract IMetadataCollection InitializeCustomAttributes();

        /// <summary>
        /// Returns a <see cref="IEnumerable{T}"/> of the elements
        /// contained within the <see cref="TypeBase{TIdentifier}"/>.
        /// </summary>
        public IEnumerable<IDeclaration> Declarations
        {
            get
            {
                return this.OnGetDeclarations();
            }
        }

        /// <summary>
        /// Obtains the <see cref="IEnumerable{T}"/> which contains all of the 
        /// declarations for the <see cref="TypeBase{TIdentifier}"/>.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> capable of iterating the declarations
        /// contained within the <see cref="TypeBase{TIdentifier}"/>.</returns>
        protected virtual IEnumerable<IDeclaration> OnGetDeclarations()
        {
            foreach (var item in from item in this.Members.Values
                                 orderby item.Entry.Name
                                 select item)
                yield return item.Entry;
        }

        private static IEnumerable<IGeneralDeclarationUniqueIdentifier> GetEmptyIdentifiers()
        {
            yield break;
        }

        private static IEnumerable<IDeclaration> GetEmptyDeclarations()
        {
            yield break;
        }

        internal static IEnumerable<IDeclaration> GetTypeParentDeclarations<T>(T parent)
            where T :
                IType,
                ITypeParent
        {
            return from element in GetTypeParentDeclarationsInternal(parent)
                   orderby element.Name
                   select element;
        }

        private static IEnumerable<IDeclaration> GetTypeParentDeclarationsInternal<T>(T parent)
            where T :
                IType,
                ITypeParent
        {
            foreach (var type in parent.Types.Values)
                yield return type.Entry;
            foreach (var member in parent.Members.Values)
                yield return member.Entry;
        }
        /// <summary>
        /// Returns a series of string values which relate to the 
        /// identifiers contained within the <see cref="IType"/>.
        /// </summary>
        public abstract IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers { get; }

        protected object SyncObject { get { return this.syncObject; } }

        #region IType Members

        IGeneralTypeUniqueIdentifier IType.UniqueIdentifier
        {
            get { return (IGeneralTypeUniqueIdentifier)this.UniqueIdentifier; }
        }

        /// <summary>
        /// Returns the <see cref="ITypeIdentityManager"/> which was used
        /// to construct the current <see cref="IType"/>.
        /// </summary>
        public ITypeIdentityManager Manager
        {
            get
            {
                return this.OnGetManager();
            }
        }

        protected abstract ITypeIdentityManager OnGetManager();

        #endregion

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string UniqueIdentifierString
        {
            get
            {
                return this.UniqueIdentifier.ToString();
            }
        }


    }
}
