﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Provides a base class for <see cref="IType"/> implementations.
    /// </summary>
    [DebuggerDisplay("Name = {FullName}, FullName = {UniqueIdentifierString}")]
    public abstract class TypeBase<TIdentifier> :
        DeclarationBase<TIdentifier>,
        //_IConstructCacheRegistrar<IModifiedType, ITypeModifierSetEntry>,
        IType
        where TIdentifier :
            ITypeUniqueIdentifier
    {
        internal static IEnumerable<IDeclaration> EmptyDeclarations = GetEmptyDeclarations();
        internal static IEnumerable<IGeneralDeclarationUniqueIdentifier> EmptyIdentifiers = GetEmptyIdentifiers();
        /// <summary>
        /// Data member for <see cref="Metadata"/>.
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
        /// Data member for <see cref="ImplementedInterfaces"/>.
        /// </summary>
        private ILockedTypeCollection implementedInterfaces;
        private ILockedTypeCollection directImplementedInterfaces;

        /// <summary>
        /// Returns the <see cref="ITypeParent"/> from which the current
        /// <see cref="TypeBase{TIdentifier}"/> is declared.
        /// </summary>
        /// <returns>An <see cref="ITypeParent"/> instance denoting
        /// the current <see cref="TypeBase{TIdentifier}"/>'s point 
        /// of declaration.</returns>
        protected abstract ITypeParent OnGetParent();
        /// <summary>
        /// Returns the <see cref="TypeKind"/> the <see cref="TypeBase{TIdentifier}"/>
        /// is.
        /// </summary>
        protected abstract TypeKind TypeImpl { get; }
        /// <summary>
        /// 
        /// </summary>
        protected abstract bool CanCacheImplementsList { get; }

        /* *
         * Fix attributed to issue with inheritance of abstract member marked
         * with protected and internal.  Inheritance tree broke on inheritors
         * which implemented the abstract property properly, but the C# compiler
         * failed to accept the entry.
         * */
        internal bool _CanCacheImplementsList { get { return this.CanCacheImplementsList; } }

        /// <summary>
        /// Returns the <see cref="ITypeCollection"/> which represents
        /// the interfaces implemented by the current <see cref="TypeBase{TIdentifier}"/>.
        /// </summary>
        /// <returns>A <see cref="ITypeCollection"/> instance.</returns>
        protected abstract ILockedTypeCollection OnGetImplementedInterfaces();

        /// <summary>
        /// Returns the <see cref="ITypeCollection"/> which represents
        /// the interfaces implemented by the current <see cref="TypeBase{TIdentifier}"/>.
        /// </summary>
        /// <returns>A <see cref="ITypeCollection"/> instance relative to the interfaces
        /// implemented directly by this <see cref="TypeBase{TIdentifier}"/>.</returns>
        protected abstract ILockedTypeCollection OnGetDirectImplementedInterfaces();

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
        internal IArrayType OnMakeArray(int rank)
        {
            return this.IdentityManager.MakeArray(this, rank);
        }

        /// <summary>
        /// Implementation version of <see cref="MakeArray(Int32[], UInt32[])"/> which 
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
        internal IArrayType OnMakeArray(params int[] lowerBounds)
        {
            return this.IdentityManager.MakeArray(this, lowerBounds: lowerBounds);
        }

        /// <summary>
        /// Implementation version of <see cref="MakeByReference"/> which creates 
        /// a new pointer <see cref="IType"/>.
        /// </summary>
        /// <returns>A new <see cref="IType"/> by reference.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when the current
        /// <see cref="IType"/> is already a by-reference type.</exception>
        internal IType OnMakeByReference()
        {
            return this.IdentityManager.MakeClassificationType(this, TypeElementClassification.Reference);
        }

        /// <summary>
        /// Implementation version of <see cref="MakePointer"/> which creates a new 
        /// pointer <see cref="IType"/>.
        /// </summary>
        /// <returns>A new <see cref="IType"/> as a pointer type.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when the current
        /// <see cref="IType"/> is a by-reference type.</exception>
        internal IType OnMakePointer()
        {
            return this.IdentityManager.MakeClassificationType(this, TypeElementClassification.Pointer);
        }

        /// <summary>
        /// Makes the current <see cref="TypeBase{TIdentifier}"/> as a 
        /// nullable <see cref="IType"/>.
        /// </summary>
        /// <returns>A <see cref="IType"/> instance
        /// which represents the current <see cref="TypeBase{TIdentifier}"/>
        /// as a nullable type.</returns>
        internal IType OnMakeNullable()
        {
            return this.IdentityManager.MakeClassificationType(this, TypeElementClassification.Nullable);
        }

        private object syncObject = new object();

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
        public ITypeParent Parent
        {
            get { return this.OnGetParent(); }
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
            return this.IdentityManager.MakeArray(this, rank);
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
        public IArrayType MakeArray(int[] lowerBounds, uint[] lengths = null)
        {
            return this.IdentityManager.MakeArray(this, lowerBounds, lengths);
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

            return IsSubclassOfImpl(other);
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
        /// Returns a collection of <see cref="IType"/> instances that are directly implemented by the current
        /// <see cref="IType"/>.
        /// </summary>
        /// <returns>A <see cref="ILockedTypeCollection"/> which represents the <see cref="IType"/> instances which directly implemented by the current
        /// <see cref="TypeBase{TIdentifier}"/></returns>
        public ILockedTypeCollection GetDirectlyImplementedInterfaces()
        {
            if (this.CanCacheImplementsList)
            {
                lock (this.syncObject)
                {
                    if (this.directImplementedInterfaces == null)
                        this.directImplementedInterfaces = this.OnGetDirectImplementedInterfaces();
                    return this.directImplementedInterfaces;
                }
            }
            else
                return this.OnGetDirectImplementedInterfaces();
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
        public virtual bool Equals(IType other)
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
        /// <see cref="DeclarationBase{TIdentifier}.Name"/> is not enough to distinguish between two 
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
        public IMetadataCollection Metadata
        {
            get
            {
                lock (this.syncObject)
                {
                    if (this.customAttributes == null)
                        this.customAttributes = this.InitializeMetadata();
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
        /// <returns>true if <paramref name="metadatumType"/> can be assigned to
        /// from a type of one of the custom attributes contained within 
        /// the <see cref="TypeBase{TIdentifier}"/>.</returns>
        public bool IsDefined(IType metadatumType)
        {
            return this.StandardIsDefined(metadatumType);
        }

        public bool IsDefined(IType metadatumType, bool inherited)
        {
            bool canInherit = this.IdentityManager.MetadatumHandler.IsMetadatumInheritable(metadatumType);
            for (IType targetType = this; targetType != null; targetType = targetType.BaseType)
            {
                if (targetType.IsDefined(metadatumType))
                    return true;
                else if (!(canInherit && inherited))
                    return false;
            }
            return false;
        }

        public IMetadatum GetMetadatum(IType metadatumType, bool inherited)
        {
            bool canInherit = this.IdentityManager.MetadatumHandler.IsMetadatumInheritable(metadatumType);
            for (IType targetType = this; targetType != null; targetType = targetType.BaseType)
            {
                if (targetType.IsDefined(metadatumType))
                    return targetType.Metadata[metadatumType];
                else if (!(canInherit && inherited))
                    return null;
            }
            return null;
        }

        #endregion

        /// <summary>
        /// Initializes the <see cref="Metadata"/> for the current
        /// <see cref="TypeBase{TIdentifier}"/>.
        /// </summary>
        /// <returns>A <see cref="IMetadataCollection"/> of
        /// attributes relative to the current instance.</returns>
        protected abstract IMetadataCollection InitializeMetadata();

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
        /// Returns the <see cref="IIdentityManager"/> which was used
        /// to construct the current <see cref="IType"/>.
        /// </summary>
        public IIdentityManager IdentityManager
        {
            get
            {
                return this.OnGetManager();
            }
        }

        protected abstract IIdentityManager OnGetManager();

        #endregion

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string UniqueIdentifierString
        {
            get
            {
                return this.UniqueIdentifier.ToString();
            }
        }

        #region IType Members

        public IModifiedType MakeModified(TypeModification[] modifiers)
        {
            return this.IdentityManager.MakeModifiedType(this, modifiers);
        }

        #endregion

    }
}
