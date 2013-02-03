using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System.Diagnostics;

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    partial class _TypeParameterDictionary<TGenericParameter, TParent, TInternalParent>
        where TGenericParameter :
            class,
            IGenericParameter<TGenericParameter, TParent>
        where TParent :
            IGenericParamParent<TGenericParameter, TParent>
        where TInternalParent :
            TParent
    {
        protected class TypeParameter :
            IGenericParameter<TGenericParameter>
        {
            private TParent parent;
            private TGenericParameter original;
            private TypeModifiedCache typeModifiedCache = new TypeModifiedCache();
            /// <summary>
            /// Data member for the pointer cache.
            /// </summary>
            private IType pointer;
            /// <summary>
            /// Data member for the byreference cache.
            /// </summary>
            private IType byRefType;
            /// <summary>
            /// Data member for <see cref="MakeArray(Int32)"/>.
            /// </summary>
            private TypeArrayCache arrayCache;
            private IType nullable;

            #region IGenericParameter<TGenericParameter> Members

            public IGenericParameterConstructorMemberDictionary<TGenericParameter> Constructors
            {
                get { throw new NotImplementedException(); }
            }

            public IGenericParameterEventMemberDictionary<TGenericParameter> Events
            {
                get { throw new NotImplementedException(); }
            }

            public IGenericParameterIndexerMemberDictionary<TGenericParameter> Indexers
            {
                get { throw new NotImplementedException(); }
            }

            public IGenericParameterMethodMemberDictionary<TGenericParameter> Methods
            {
                get { throw new NotImplementedException(); }
            }

            public IGenericParameterPropertyMemberDictionary<TGenericParameter> Properties
            {
                get { throw new NotImplementedException(); }
            }

            #endregion

            #region ICreatableParent<IGenericParameterConstructorMember<TGenericParameter>,TGenericParameter> Members

            IConstructorMemberDictionary<IGenericParameterConstructorMember<TGenericParameter>, TGenericParameter> ICreatableParent<IGenericParameterConstructorMember<TGenericParameter>, TGenericParameter>.Constructors
            {
                get { return this.Constructors; }
            }

            public IGenericParameterConstructorMember<TGenericParameter> TypeInitializer
            {
                get { return null; }
            }

            #endregion

            #region ICreatableParent Members

            IConstructorMemberDictionary ICreatableParent.Constructors
            {
                get { return (IConstructorMemberDictionary)this.Constructors; }
            }

            IConstructorMember ICreatableParent.TypeInitializer
            {
                get { return this.TypeInitializer; }
            }

            #endregion

            #region IType Members
            public ITypeIdentityManager IdentityManager { get { return this.original.IdentityManager; } }

            public TypeElementClassification ElementClassification
            {
                get { return TypeElementClassification.None; }
            }

            IType IType.ElementType
            {
                get
                {
                    return this.ElementType;
                }
            }

            public bool IsGenericTypeParameter
            {
                get { return true; }
            }

            public bool IsGenericConstruct
            {
                get { return false; }
            }

            ITypeParent IType.Parent
            {
                get
                {
                    if (this.Parent is ITypeParent)
                        return (ITypeParent)this.Parent;
                    return null;
                }
            }

            public TypeKind Type
            {
                get { return TypeKind.Class; }
            }

            public bool IsNullable
            {
                get { return this.original.IsNullable; }
            }

            private void ArrayCacheCheck()
            {
                if (this.arrayCache == null)
                    this.arrayCache = new TypeArrayCache(this, this.OnMakeArray, this.OnMakeArray);
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
                ArrayCacheCheck();
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
            public IArrayType MakeArray(int[] lowerBounds, uint[] lengths = null)
            {
                ArrayCacheCheck();
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
                if (this.pointer == null)
                    this.pointer = this.OnMakePointer();
                return this.pointer;
            }

            /// <summary>
            /// Creates a new pointer <see cref="IType"/>.
            /// </summary>
            /// <returns>A new <see cref="IType"/> by reference.</returns>
            /// <exception cref="System.InvalidOperationException">Thrown when the current
            /// <see cref="IType"/> is already a by-reference type.</exception>
            public IType MakeByReference()
            {
                if (this.byRefType == null)
                    this.byRefType = this.OnMakeByReference();
                return this.byRefType;
            }

            /// <summary>
            /// Creates a new nullable <see cref="IType"/>.
            /// </summary>
            /// <returns>A new <see cref="IType"/> as a nullable type relative to the current
            /// <see cref="TypeParameter"/>.</returns>
            /// <exception cref="System.InvalidOperationException">thrown when the current <see cref="IType"/>
            /// is a poinoter, array, generic type definition, by-reference, or when 
            /// <see cref="GenericParameter"/> is something other than <see cref="TypeKind.Struct"/> or when the
            /// special restriction on a generic parameter does not include struct as a condition.</exception>
            public IType MakeNullable()
            {
                if (this.nullable == null)
                    this.nullable = this.OnMakeNullable();
                return this.nullable;
            }

            public bool IsSubclassOf(IType other)
            {
                return other.Equals(this.IdentityManager.ObtainTypeReference(RuntimeCoreType.RootType));
            }

            public bool IsAssignableFrom(IType target)
            {
                throw new NotImplementedException();
            }

            public string FullName
            {
                get { return this.original.FullName; }
            }

            public INamespaceDeclaration Namespace
            {
                get { return this.original.Namespace; }
            }

            public string NamespaceName
            {
                get { return this.original.NamespaceName; }
            }

            public IType BaseType
            {
                get { return this.IdentityManager.ObtainTypeReference(RuntimeCoreType.RootType); }
            }

            public ILockedTypeCollection ImplementedInterfaces
            {
                get { throw new NotImplementedException(); }
            }

            public ILockedTypeCollection GetDirectlyImplementedInterfaces()
            {
                throw new NotImplementedException();
            }

            public IAssembly Assembly
            {
                get { return this.original.Assembly; }
            }

            public IFullMemberDictionary Members
            {
                get { throw new NotImplementedException(); }
            }

            public IEnumerable<IDeclaration> Declarations
            {
                get { throw new NotImplementedException(); }
            }

            public IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
            {
                get { return this.original.AggregateIdentifiers; }
            }

            #endregion

            #region IEquatable<IType> Members

            public bool Equals(IType other)
            {
                return object.ReferenceEquals(this, other);
            }

            #endregion

            #region IMetadataEntity Members

            public IMetadataCollection Metadata
            {
                get { return this.original.Metadata; }
            }

            public bool IsDefined(IType metadatumType)
            {
                return this.original.IsDefined(metadatumType);
            }

            public bool IsDefined(IType metadatumType, bool inherited)
            {
                return this.original.IsDefined(metadatumType, inherited);
            }

            #endregion

            #region IDeclaration Members

            public string Name
            {
                get { return this.original.Name; }
            }

            public IGenericParameterUniqueIdentifier UniqueIdentifier
            {
                get { return this.original.UniqueIdentifier; }
            }

            IGeneralDeclarationUniqueIdentifier IDeclaration.UniqueIdentifier
            {
                get
                {
                    return this.UniqueIdentifier;
                }
            }

            IGeneralTypeUniqueIdentifier IType.UniqueIdentifier
            {
                get
                {
                    return this.UniqueIdentifier;
                }
            }

            public event EventHandler Disposed;

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IScopedDeclaration Members

            public AccessLevelModifiers AccessLevel
            {
                get { return this.original.AccessLevel; }
            }

            #endregion

            #region IType<TGenericParameter> Members

            public TGenericParameter ElementType
            {
                get
                {
                    throw new InvalidOperationException("Available on closed generic, by-reference, pointer, and array types only.");
                }
            }

            #endregion

            #region IMethodSignatureParent<IGenericParameterMethodMember<TGenericParameter>,TGenericParameter> Members

            IMethodSignatureMemberDictionary<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter> IMethodSignatureParent<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>.Methods
            {
                get { return this.Methods; }
            }

            #endregion

            #region IMethodSignatureParent Members

            IMethodSignatureMemberDictionary IMethodSignatureParent.Methods
            {
                get { return (IMethodSignatureMemberDictionary)this.Methods; }
            }

            #endregion

            #region IEventSignatureParent<IGenericParameterEventMember<TGenericParameter>,TGenericParameter> Members

            IEventSignatureMemberDictionary<IGenericParameterEventMember<TGenericParameter>, TGenericParameter> IEventSignatureParent<IGenericParameterEventMember<TGenericParameter>, TGenericParameter>.Events
            {
                get { return this.Events; }
            }

            #endregion

            #region IEventSignatureParent<IGenericParameterEventMember<TGenericParameter>,IEventSignatureParameterMember<IGenericParameterEventMember<TGenericParameter>,TGenericParameter>,TGenericParameter> Members

            IEventSignatureMemberDictionary<IGenericParameterEventMember<TGenericParameter>, IEventSignatureParameterMember<IGenericParameterEventMember<TGenericParameter>, TGenericParameter>, TGenericParameter> IEventSignatureParent<IGenericParameterEventMember<TGenericParameter>, IEventSignatureParameterMember<IGenericParameterEventMember<TGenericParameter>, TGenericParameter>, TGenericParameter>.Events
            {
                get { return this.Events; }
            }

            #endregion

            #region IEventSignatureParent Members

            IEventSignatureMemberDictionary IEventSignatureParent.Events
            {
                get { return (IEventSignatureMemberDictionary)this.Events; }
            }

            #endregion

            #region IIndexerSignatureParent<IGenericParameterIndexerMember<TGenericParameter>,TGenericParameter> Members

            IIndexerSignatureMemberDictionary<IGenericParameterIndexerMember<TGenericParameter>, TGenericParameter> IIndexerSignatureParent<IGenericParameterIndexerMember<TGenericParameter>, TGenericParameter>.Indexers
            {
                get { return this.Indexers; }
            }

            #endregion

            #region IIndexerSignatureParent Members

            IIndexerSignatureMemberDictionary IIndexerSignatureParent.Indexers
            {
                get { return (IIndexerSignatureMemberDictionary)this.Indexers; }
            }

            #endregion

            #region IPropertySignatureParent<IGenericParameterPropertyMember<TGenericParameter>,TGenericParameter> Members

            IPropertySignatureMemberDictionary<IGenericParameterPropertyMember<TGenericParameter>, TGenericParameter> IPropertySignatureParent<IGenericParameterPropertyMember<TGenericParameter>, TGenericParameter>.Properties
            {
                get { return this.Properties; }
            }

            #endregion

            #region IPropertySignatureParent Members

            IPropertySignatureMemberDictionary IPropertySignatureParent.Properties
            {
                get { return (IPropertySignatureMemberDictionary)this.Properties; }
            }

            #endregion

            #region IGenericParameter Members

            public GenericParameterVariance Variance
            {
                get { return this.original.Variance; }
            }

            public GenericTypeParameterSpecialConstraint SpecialConstraint
            {
                get { return this.original.SpecialConstraint; }
            }

            public int Position
            {
                get { return this.original.Position; }
            }

            public ILockedTypeCollection Constraints
            {
                get { throw new NotImplementedException(); }
            }

            IGenericParamParent IGenericParameter.Parent
            {
                get { return this.parent; }
            }

            IGenericParameterConstructorMemberDictionary IGenericParameter.Constructors
            {
                get { return (IGenericParameterConstructorMemberDictionary)this.Constructors; }
            }

            IGenericParameterEventMemberDictionary IGenericParameter.Events
            {
                get { return (IGenericParameterEventMemberDictionary)this.Events; }
            }

            IGenericParameterIndexerMemberDictionary IGenericParameter.Indexers
            {
                get { return (IGenericParameterIndexerMemberDictionary)this.Indexers; }
            }

            IGenericParameterMethodMemberDictionary IGenericParameter.Methods
            {
                get { return (IGenericParameterMethodMemberDictionary)this.Methods; }
            }

            IGenericParameterPropertyMemberDictionary IGenericParameter.Properties
            {
                get { return (IGenericParameterPropertyMemberDictionary)this.Properties; }
            }

            #endregion

            public TParent Parent { get { return this.parent; } }

            #region OnMake* Methods
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
            /// Thrown when the current <see cref="TypeParameter"/>
            /// is a pointer or by-reference type.</exception>
            protected IArrayType OnMakeArray(int rank)
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
            protected IArrayType OnMakeArray(int[] lowerBounds, uint[] lengths)
            {
                return this.IdentityManager.MakeArray(this, lowerBounds, lengths);
            }

            /// <summary>
            /// Implementation version of <see cref="MakeByReference"/> which creates 
            /// a new pointer <see cref="IType"/>.
            /// </summary>
            /// <returns>A new <see cref="IType"/> by reference.</returns>
            /// <exception cref="System.InvalidOperationException">Thrown when the current
            /// <see cref="IType"/> is already a by-reference type.</exception>
            protected IType OnMakeByReference()
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
            protected IType OnMakePointer()
            {
                return this.IdentityManager.MakeClassificationType(this, TypeElementClassification.Pointer);
            }

            /// <summary>
            /// Makes the current <see cref="TypeParameter"/> as a 
            /// nullable <see cref="IType"/>.
            /// </summary>
            /// <returns>A <see cref="IType"/> instance
            /// which represents the current <see cref="TypeParameter"/>
            /// as a nullable type.</returns>
            protected IType OnMakeNullable()
            {
                if (this.IsNullable)
                    return this.IdentityManager.MakeClassificationType(this, TypeElementClassification.Nullable);
                throw new InvalidOperationException("Cannot make into a nullable type, generic parameters are neither reference nor non-reference types unless explicitly stated as a struct.");
            }

            public IModifiedType MakeModified(TypeModification[] modifiers)
            {
                IModifiedType result;
                lock (typeModifiedCache)
                {
                    TypeModifierSetEntry entry;
                    if (!typeModifiedCache.TryObtainConstruct(entry = new TypeModifierSetEntry(modifiers), out result))
                        typeModifiedCache.RegisterConstruct(entry, result);
                }
                return result;
            }
            #endregion
        }
    }
}
