using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    partial class AssemblyWorkspace
    {
        /// <summary>
        /// Provides a class type dictionary for an assembly workspace.
        /// </summary>
        protected class ClassTypeDictionary :
            GroupedTypeDictionary<IClassType>,
            IClassTypeDictionary
        {
            /// <summary>
            /// Creates a new <see cref="ClassTypeDictionary"/> with the <paramref name="master"/>,
            /// <paramref name="owner"/>, and <paramref name="sources"/> provided.
            /// </summary>
            /// <param name="master">
            /// The <see cref="FullTypesMasterDictionary"/> which manages the full series.</param>
            /// <param name="owner">The <see cref="ITypeParent"/> which owns the <see cref="ClassTypeDictionary"/>.</param>
            /// <param name="sources">The <see cref="ITypeParent"/> series from which the current
            /// <see cref="ClassTypeDictionary"/> is based off of.</param>
            public ClassTypeDictionary(FullTypesMasterDictionary master, ITypeParent owner, ITypeParent[] sources)
                : base(master, owner, sources, ClassTypeDictionary.ObtainParentClasses)
            {
            }

            /* *
             * Used as a selector for the base class to determine which types to use.
             * */
            private static IClassType[] ObtainParentClasses(ITypeParent target)
            {
                return target.Classes.Values.ToArray();
            }

            /// <summary>
            /// Obtains a <see cref="IClassType"/> ambiguous type
            /// instance with the <paramref name="name"/> and array of 
            /// <see cref="IClassType"/> instances which created the 
            /// ambiguity.
            /// </summary>
            /// <param name="name">The name of the <see cref="IClassType"/>
            /// to create.</param>
            /// <param name="tType">The array of <see cref="IClassType"/>
            /// instances which created the ambiguity.</param>
            /// <returns>An <see cref="IClassType"/> ambiguous type
            /// instance.</returns>
            protected override IClassType GetAmbiguousType(string name, IClassType[] tType)
            {
                return new _AmbiguousType(tType);
            }

            private sealed class _AmbiguousType :
                AmbiguousType,
                IClassType
            {
                public _AmbiguousType(IClassType[] source)
                    : base(source)
                {
                }

                #region IClassType Members

                public IClassInterfaceMapping GetInterfaceMap(IInterfaceType type)
                {
                    throw new NotSupportedException();
                }

                public SpecialClassModifier SpecialModifier
                {
                    get {
                        throw new NotSupportedException();
                    }
                }

                public bool IsDefined(IType attributeType, bool inherit)
                {
                    throw new NotSupportedException();
                }

                public new IClassType BaseType
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IGenericType<IClassType> Members

                public IClassType MakeGenericType(ITypeCollectionBase typeParameters)
                {
                    throw new NotSupportedException();
                }

                public IClassType MakeGenericType(params IType[] typeParameters)
                {
                    throw new NotSupportedException();
                }

                #endregion

                #region IType<IClassType> Members

                public new IClassType ElementType
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IGenericParamParent<IGenericTypeParameter<IClassType>,IClassType> Members

                public IGenericParameterDictionary<IGenericTypeParameter<IClassType>, IClassType> TypeParameters
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IGenericParamParent Members

                IGenericParameterDictionary IGenericParamParent.TypeParameters
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IGenericType Members

                public bool IsGenericTypeDefinition
                {
                    get { return this.Source[0].IsGenericType; }
                }

                public bool ContainsGenericParameters
                {
                    get { return this.Source[0].IsGenericType; }
                }

                public ILockedTypeCollection GenericParameters
                {
                    get { throw new NotSupportedException(); }
                }

                IGenericType IGenericType.MakeGenericType(ITypeCollectionBase typeParameters)
                {
                    throw new NotSupportedException();
                }

                IGenericType IGenericType.MakeGenericType(params IType[] typeParameters)
                {
                    throw new NotSupportedException();
                }

                public IGenericType MakeVerifiedGenericType(ITypeCollection typeParameters)
                {
                    throw new NotSupportedException();
                }

                public void ReverifyTypeParameters()
                {
                    throw new NotSupportedException();
                }

                #endregion

                #region IInstantiableType<IClassCtorMember,IClassEventMember,IClassFieldMember,IClassIndexerMember,IClassMethodMember,IClassPropertyMember,IClassType> Members

                IInterfaceMemberMapping<IClassMethodMember, IInterfaceMethodMember, IClassPropertyMember, IInterfacePropertyMember, IClassEventMember, IInterfaceEventMember, IClassIndexerMember, IInterfaceIndexerMember, IClassType, IInterfaceType> IInstantiableType<IClassCtorMember, IClassEventMember, IClassFieldMember, IClassIndexerMember, IClassMethodMember, IClassPropertyMember, IClassType>.GetInterfaceMap(IInterfaceType type)
                {
                    throw new NotSupportedException();
                }

                #endregion

                #region ICreatableType<IClassCtorMember,IClassType> Members

                public IConstructorMemberDictionary<IClassCtorMember, IClassType> Constructors
                {
                    get { throw new NotSupportedException(); }
                }

                public IClassCtorMember TypeInitializer
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region ICreatableType Members

                IConstructorMemberDictionary ICreatableType.Constructors
                {
                    get { throw new NotSupportedException(); }
                }

                IConstructorMember ICreatableType.TypeInitializer
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IMethodParent<IClassMethodMember,IClassType> Members

                public IMethodMemberDictionary<IClassMethodMember, IClassType> Methods
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IMethodParent Members

                IMethodMemberDictionary IMethodParent.Methods
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IPropertyParentType<IClassPropertyMember,IClassType> Members

                public IPropertyMemberDictionary<IClassPropertyMember, IClassType> Properties
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IPropertyParentType Members

                IPropertyMemberDictionary IPropertyParentType.Properties
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IFieldParent<IClassFieldMember,IClassType> Members

                public IFieldMemberDictionary<IClassFieldMember, IClassType> Fields
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IFieldParent Members

                IFieldMemberDictionary IFieldParent.Fields
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region ICoercibleType<IClassType> Members

                public IBinaryOperatorCoercionMemberDictionary<IClassType> BinaryOperatorCoercions
                {
                    get { throw new NotSupportedException(); }
                }

                public ITypeCoercionMemberDictionary<IClassType> TypeCoercions
                {
                    get { throw new NotSupportedException(); }
                }

                public IUnaryOperatorCoercionMemberDictionary<IClassType> UnaryOperatorCoercions
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region ICoercibleType Members

                IBinaryOperatorCoercionMemberDictionary ICoercibleType.BinaryOperatorCoercions
                {
                    get { throw new NotSupportedException(); }
                }

                ITypeCoercionMemberDictionary ICoercibleType.TypeCoercions
                {
                    get { throw new NotSupportedException(); }
                }

                IUnaryOperatorCoercionMemberDictionary ICoercibleType.UnaryOperatorCoercions
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IEventParent<IClassEventMember,IClassType> Members

                public IEventMemberDictionary<IClassEventMember, IClassType> Events
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IEventSignatureParent<IClassEventMember,IEventParameterMember<IClassEventMember,IClassType>,IClassType> Members

                IEventSignatureMemberDictionary<IClassEventMember, IEventParameterMember<IClassEventMember, IClassType>, IClassType> IEventSignatureParent<IClassEventMember, IEventParameterMember<IClassEventMember, IClassType>, IClassType>.Events
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IEventSignatureParent Members

                IEventSignatureMemberDictionary IEventSignatureParent.Events
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IIndexerParent<IClassIndexerMember,IClassType> Members

                public IIndexerMemberDictionary<IClassIndexerMember, IClassType> Indexers
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IIndexerParent Members

                IIndexerMemberDictionary IIndexerParent.Indexers
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region ITypeParent Members

                public IClassTypeDictionary Classes
                {
                    get { throw new NotSupportedException(); }
                }

                public IDelegateTypeDictionary Delegates
                {
                    get { throw new NotSupportedException(); }
                }

                public IEnumTypeDictionary Enums
                {
                    get { throw new NotSupportedException(); }
                }

                public IInterfaceTypeDictionary Interfaces
                {
                    get { throw new NotSupportedException(); }
                }

                public IStructTypeDictionary Structs
                {
                    get { throw new NotSupportedException(); }
                }

                public IFullTypeDictionary Types
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion
            }
        }

        /// <summary>
        /// Provides a delegate type dictionary for an assembly workspace.
        /// </summary>
        protected class DelegateTypeDictionary :
            GroupedTypeDictionary<IDelegateType>,
            IDelegateTypeDictionary
        {
            /// <summary>
            /// Creates a new <see cref="DelegateTypeDictionary"/> with the <paramref name="master"/>,
            /// <paramref name="owner"/>, and <paramref name="sources"/> provided.
            /// </summary>
            /// <param name="master">
            /// The <see cref="FullTypesMasterDictionary"/> which manages the full series.</param>
            /// <param name="owner">The <see cref="ITypeParent"/> which owns the <see cref="DelegateTypeDictionary"/>.</param>
            /// <param name="sources">The <see cref="ITypeParent"/> series from which the current
            /// <see cref="DelegateTypeDictionary"/> is based off of.</param>
            public DelegateTypeDictionary(FullTypesMasterDictionary master, ITypeParent owner, ITypeParent[] sources)
                : base(master, owner, sources, DelegateTypeDictionary.ObtainParentDelegatees)
            {
            }

            /* *
             * Used as a selector for the base class to determine which types to use.
             * */
            private static IDelegateType[] ObtainParentDelegatees(ITypeParent target)
            {
                return target.Delegates.Values.ToArray();
            }

            protected override IDelegateType GetAmbiguousType(string name, IDelegateType[] tType)
            {
                return new _AmbiguousType(tType);
            }

            private class _AmbiguousType :
                AmbiguousType,
                IDelegateType
            {

                public _AmbiguousType(IDelegateType[] types)
                    : base(types)
                {
                }

                #region IDelegateType Members

                public IType ReturnType
                {
                    get { throw new NotSupportedException(); }
                }

                public IDelegateTypeParameterDictionary Parameters
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IParameterParent<IDelegateType,IDelegateTypeParameterMember> Members

                IParameterMemberDictionary<IDelegateType, IDelegateTypeParameterMember> IParameterParent<IDelegateType, IDelegateTypeParameterMember>.Parameters
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IParameterParent Members

                IParameterMemberDictionary IParameterParent.Parameters
                {
                    get { throw new NotSupportedException(); }
                }

                public bool LastIsParams
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IGenericType<IDelegateType> Members

                public IDelegateType MakeGenericType(ITypeCollectionBase typeParameters)
                {
                    throw new NotSupportedException(); 
                }

                public IDelegateType MakeGenericType(params IType[] typeParameters)
                {
                    throw new NotSupportedException();
                }

                #endregion

                #region IType<IDelegateType> Members

                public new IDelegateType ElementType
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IGenericParamParent<IGenericTypeParameter<IDelegateType>,IDelegateType> Members

                public IGenericParameterDictionary<IGenericTypeParameter<IDelegateType>, IDelegateType> TypeParameters
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IGenericParamParent Members

                IGenericParameterDictionary IGenericParamParent.TypeParameters
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IGenericType Members

                public bool IsGenericTypeDefinition
                {
                    get { return this.Source[0].IsGenericType; }
                }

                public bool ContainsGenericParameters
                {
                    get { return this.Source[0].IsGenericType; }
                }

                public ILockedTypeCollection GenericParameters
                {
                    get { throw new NotSupportedException(); }
                }

                IGenericType IGenericType.MakeGenericType(ITypeCollectionBase typeParameters)
                {
                    throw new NotSupportedException();
                }

                IGenericType IGenericType.MakeGenericType(params IType[] typeParameters)
                {
                    throw new NotSupportedException();
                }

                public IGenericType MakeVerifiedGenericType(ITypeCollection typeParameters)
                {
                    throw new NotSupportedException();
                }

                public void ReverifyTypeParameters()
                {
                    throw new NotSupportedException();
                }

                #endregion
            }
        }

        /// <summary>
        /// Provides an enumeration type dictionary for an assembly workspace.
        /// </summary>
        protected class EnumTypeDictionary :
            GroupedTypeDictionary<IEnumType>,
            IEnumTypeDictionary
        {
            /// <summary>
            /// Creates a new <see cref="EnumTypeDictionary"/> with the <paramref name="master"/>,
            /// <paramref name="owner"/>, and <paramref name="sources"/> provided.
            /// </summary>
            /// <param name="master">
            /// The <see cref="FullTypesMasterDictionary"/> which manages the full series.</param>
            /// <param name="owner">The <see cref="ITypeParent"/> which owns the <see cref="EnumTypeDictionary"/>.</param>
            /// <param name="sources">The <see cref="ITypeParent"/> series from which the current
            /// <see cref="EnumTypeDictionary"/> is based off of.</param>
            public EnumTypeDictionary(FullTypesMasterDictionary master, ITypeParent owner, ITypeParent[] sources)
                : base(master, owner, sources, EnumTypeDictionary.ObtainParentEnumes)
            {
            }

            /* *
             * Used as a selector for the base class to determine which types to use.
             * */
            private static IEnumType[] ObtainParentEnumes(ITypeParent target)
            {
                return target.Enums.Values.ToArray();
            }

            protected override IEnumType GetAmbiguousType(string name, IEnumType[] tType)
            {
                return new _AmbiguousType(tType);
            }

            private class _AmbiguousType :
                AmbiguousType,
                IEnumType
            {
                public _AmbiguousType(IEnumType[] types)
                    : base(types)
                {
                }

                #region IType<IEnumType> Members

                public new IEnumType ElementType
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IFieldParent<IEnumFieldMember,IEnumType> Members

                public IFieldMemberDictionary<IEnumFieldMember, IEnumType> Fields
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IFieldParent Members

                IFieldMemberDictionary IFieldParent.Fields
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion
            }
        }

        /// <summary>
        /// Provides an interface type dictionary for an assembly workspace.
        /// </summary>
        protected class InterfaceTypeDictionary :
            GroupedTypeDictionary<IInterfaceType>,
            IInterfaceTypeDictionary
        {
            /// <summary>
            /// Creates a new <see cref="InterfaceTypeDictionary"/> with the <paramref name="master"/>,
            /// <paramref name="owner"/>, and <paramref name="sources"/> provided.
            /// </summary>
            /// <param name="master">
            /// The <see cref="FullTypesMasterDictionary"/> which manages the full series.</param>
            /// <param name="owner">The <see cref="ITypeParent"/> which owns the <see cref="InterfaceTypeDictionary"/>.</param>
            /// <param name="sources">The <see cref="ITypeParent"/> series from which the current
            /// <see cref="InterfaceTypeDictionary"/> is based off of.</param>
            public InterfaceTypeDictionary(FullTypesMasterDictionary master, ITypeParent owner, ITypeParent[] sources)
                : base(master, owner, sources, InterfaceTypeDictionary.ObtainParentInterfacees)
            {
            }

            /* *
             * Used as a selector for the base class to determine which types to use.
             * */
            private static IInterfaceType[] ObtainParentInterfacees(ITypeParent target)
            {
                return target.Interfaces.Values.ToArray();
            }

            protected override IInterfaceType GetAmbiguousType(string name, IInterfaceType[] tType)
            {
                return new _AmbiguousType(tType);
            }

            private class _AmbiguousType :
                AmbiguousType, 
                IInterfaceType
            {
                public _AmbiguousType(IInterfaceType[] sources)
                    : base(sources)
                {
                }

                #region IGenericType<IInterfaceType> Members

                public IInterfaceType MakeGenericType(ITypeCollectionBase typeParameters)
                {
                    throw new NotSupportedException();
                }

                public IInterfaceType MakeGenericType(params IType[] typeParameters)
                {
                    throw new NotSupportedException();
                }

                #endregion

                #region IType<IInterfaceType> Members

                public new IInterfaceType ElementType
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IGenericParamParent<IGenericTypeParameter<IInterfaceType>,IInterfaceType> Members

                public IGenericParameterDictionary<IGenericTypeParameter<IInterfaceType>, IInterfaceType> TypeParameters
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IGenericParamParent Members

                IGenericParameterDictionary IGenericParamParent.TypeParameters
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IGenericType Members

                public bool IsGenericTypeDefinition
                {
                    get { return this.Source[0].IsGenericType; }
                }

                public bool ContainsGenericParameters
                {
                    get { return this.Source[0].IsGenericType; }
                }

                public ILockedTypeCollection GenericParameters
                {
                    get { throw new NotSupportedException(); }
                }

                IGenericType IGenericType.MakeGenericType(ITypeCollectionBase typeParameters)
                {
                    throw new NotSupportedException();
                }

                IGenericType IGenericType.MakeGenericType(params IType[] typeParameters)
                {
                    throw new NotSupportedException();
                }

                public IGenericType MakeVerifiedGenericType(ITypeCollection typeParameters)
                {
                    throw new NotSupportedException();
                }

                public void ReverifyTypeParameters()
                {
                    throw new NotSupportedException();
                }

                #endregion

                #region IMethodSignatureParent<IInterfaceMethodMember,IInterfaceType> Members

                public IMethodSignatureMemberDictionary<IInterfaceMethodMember, IInterfaceType> Methods
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IMethodSignatureParent Members

                IMethodSignatureMemberDictionary IMethodSignatureParent.Methods
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IPropertySignatureParentType<IInterfacePropertyMember,IInterfaceType> Members

                public IPropertySignatureMemberDictionary<IInterfacePropertyMember, IInterfaceType> Properties
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IPropertySignatureParentType Members

                IPropertySignatureMemberDictionary IPropertySignatureParentType.Properties
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IEventSignatureParent<IInterfaceEventMember,IInterfaceType> Members

                public IEventSignatureMemberDictionary<IInterfaceEventMember, IInterfaceType> Events
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IEventSignatureParent<IInterfaceEventMember,IEventSignatureParameterMember<IInterfaceEventMember,IInterfaceType>,IInterfaceType> Members

                IEventSignatureMemberDictionary<IInterfaceEventMember, IEventSignatureParameterMember<IInterfaceEventMember, IInterfaceType>, IInterfaceType> IEventSignatureParent<IInterfaceEventMember, IEventSignatureParameterMember<IInterfaceEventMember, IInterfaceType>, IInterfaceType>.Events
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IEventSignatureParent Members

                IEventSignatureMemberDictionary IEventSignatureParent.Events
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IIndexerSignatureParent<IInterfaceIndexerMember,IInterfaceType> Members

                public IIndexerSignatureMemberDictionary<IInterfaceIndexerMember, IInterfaceType> Indexers
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IIndexerSignatureParent Members

                IIndexerSignatureMemberDictionary IIndexerSignatureParent.Indexers
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region ITypeParent Members

                public IClassTypeDictionary Classes
                {
                    get { throw new NotSupportedException(); }
                }

                public IDelegateTypeDictionary Delegates
                {
                    get { throw new NotSupportedException(); }
                }

                public IEnumTypeDictionary Enums
                {
                    get { throw new NotSupportedException(); }
                }

                public IInterfaceTypeDictionary Interfaces
                {
                    get { throw new NotSupportedException(); }
                }

                public IStructTypeDictionary Structs
                {
                    get { throw new NotSupportedException(); }
                }

                public IFullTypeDictionary Types
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion
            }
        }

        /// <summary>
        /// Provides a struct type dictionary for an assembly workspace.
        /// </summary>
        protected class StructTypeDictionary :
            GroupedTypeDictionary<IStructType>,
            IStructTypeDictionary
        {
            /// <summary>
            /// Creates a new <see cref="StructTypeDictionary"/> with the <paramref name="master"/>,
            /// <paramref name="owner"/>, and <paramref name="sources"/> provided.
            /// </summary>
            /// <param name="master">
            /// The <see cref="FullTypesMasterDictionary"/> which manages the full series.</param>
            /// <param name="owner">The <see cref="ITypeParent"/> which owns the <see cref="StructTypeDictionary"/>.</param>
            /// <param name="sources">The <see cref="ITypeParent"/> series from which the current
            /// <see cref="StructTypeDictionary"/> is based off of.</param>
            public StructTypeDictionary(FullTypesMasterDictionary master, ITypeParent owner, ITypeParent[] sources)
                : base(master, owner, sources, StructTypeDictionary.ObtainParentStructes)
            {
            }

            /* *
             * Used as a selector for the base class to determine which types to use.
             * */
            private static IStructType[] ObtainParentStructes(ITypeParent target)
            {
                return target.Structs.Values.ToArray();
            }

            protected override IStructType GetAmbiguousType(string name, IStructType[] tType)
            {
                return new _AmbiguousType(tType);
            }

            private class _AmbiguousType :
                AmbiguousType,
                IStructType
            {
                public _AmbiguousType(IStructType[] sources)
                    : base(sources)
                {
                }

                #region IGenericType<IStructType> Members

                public IStructType MakeGenericType(ITypeCollectionBase typeParameters)
                {
                    throw new NotSupportedException();
                }

                public IStructType MakeGenericType(params IType[] typeParameters)
                {
                    throw new NotSupportedException();
                }

                #endregion

                #region IType<IStructType> Members

                public new IStructType ElementType
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IGenericParamParent<IGenericTypeParameter<IStructType>,IStructType> Members

                public IGenericParameterDictionary<IGenericTypeParameter<IStructType>, IStructType> TypeParameters
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IGenericParamParent Members

                IGenericParameterDictionary IGenericParamParent.TypeParameters
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IGenericType Members

                public bool IsGenericTypeDefinition
                {
                    get { return this.Source[0].IsGenericType; }
                }

                public bool ContainsGenericParameters
                {
                    get { return this.Source[0].IsGenericType; }
                }

                public ILockedTypeCollection GenericParameters
                {
                    get { throw new NotSupportedException(); }
                }

                IGenericType IGenericType.MakeGenericType(ITypeCollectionBase typeParameters)
                {
                    throw new NotSupportedException();
                }

                IGenericType IGenericType.MakeGenericType(params IType[] typeParameters)
                {
                    throw new NotSupportedException();
                }

                public IGenericType MakeVerifiedGenericType(ITypeCollection typeParameters)
                {
                    throw new NotSupportedException();
                }

                public void ReverifyTypeParameters()
                {
                    throw new NotSupportedException();
                }

                #endregion

                #region IInstantiableType<IStructCtorMember,IStructEventMember,IStructFieldMember,IStructIndexerMember,IStructMethodMember,IStructPropertyMember,IStructType> Members

                public IInterfaceMemberMapping<IStructMethodMember, IInterfaceMethodMember, IStructPropertyMember, IInterfacePropertyMember, IStructEventMember, IInterfaceEventMember, IStructIndexerMember, IInterfaceIndexerMember, IStructType, IInterfaceType> GetInterfaceMap(IInterfaceType type)
                {
                    throw new NotSupportedException();
                }

                #endregion

                #region ICreatableType<IStructCtorMember,IStructType> Members

                public IConstructorMemberDictionary<IStructCtorMember, IStructType> Constructors
                {
                    get { throw new NotSupportedException(); }
                }

                public IStructCtorMember TypeInitializer
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region ICreatableType Members

                IConstructorMemberDictionary ICreatableType.Constructors
                {
                    get { throw new NotSupportedException(); }
                }

                IConstructorMember ICreatableType.TypeInitializer
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IMethodParent<IStructMethodMember,IStructType> Members

                public IMethodMemberDictionary<IStructMethodMember, IStructType> Methods
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IMethodParent Members

                IMethodMemberDictionary IMethodParent.Methods
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IPropertyParentType<IStructPropertyMember,IStructType> Members

                public IPropertyMemberDictionary<IStructPropertyMember, IStructType> Properties
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IPropertyParentType Members

                IPropertyMemberDictionary IPropertyParentType.Properties
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IFieldParent<IStructFieldMember,IStructType> Members

                public IFieldMemberDictionary<IStructFieldMember, IStructType> Fields
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IFieldParent Members

                IFieldMemberDictionary IFieldParent.Fields
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region ICoercibleType<IStructType> Members

                public IBinaryOperatorCoercionMemberDictionary<IStructType> BinaryOperatorCoercions
                {
                    get { throw new NotSupportedException(); }
                }

                public ITypeCoercionMemberDictionary<IStructType> TypeCoercions
                {
                    get { throw new NotSupportedException(); }
                }

                public IUnaryOperatorCoercionMemberDictionary<IStructType> UnaryOperatorCoercions
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region ICoercibleType Members

                IBinaryOperatorCoercionMemberDictionary ICoercibleType.BinaryOperatorCoercions
                {
                    get { throw new NotSupportedException(); }
                }

                ITypeCoercionMemberDictionary ICoercibleType.TypeCoercions
                {
                    get { throw new NotSupportedException(); }
                }

                IUnaryOperatorCoercionMemberDictionary ICoercibleType.UnaryOperatorCoercions
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IEventParent<IStructEventMember,IStructType> Members

                public IEventMemberDictionary<IStructEventMember, IStructType> Events
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IEventSignatureParent<IStructEventMember,IEventParameterMember<IStructEventMember,IStructType>,IStructType> Members

                IEventSignatureMemberDictionary<IStructEventMember, IEventParameterMember<IStructEventMember, IStructType>, IStructType> IEventSignatureParent<IStructEventMember, IEventParameterMember<IStructEventMember, IStructType>, IStructType>.Events
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IEventSignatureParent Members

                IEventSignatureMemberDictionary IEventSignatureParent.Events
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IIndexerParent<IStructIndexerMember,IStructType> Members

                public IIndexerMemberDictionary<IStructIndexerMember, IStructType> Indexers
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region IIndexerParent Members

                IIndexerMemberDictionary IIndexerParent.Indexers
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion

                #region ITypeParent Members

                public IClassTypeDictionary Classes
                {
                    get { throw new NotSupportedException(); }
                }

                public IDelegateTypeDictionary Delegates
                {
                    get { throw new NotSupportedException(); }
                }

                public IEnumTypeDictionary Enums
                {
                    get { throw new NotSupportedException(); }
                }

                public IInterfaceTypeDictionary Interfaces
                {
                    get { throw new NotSupportedException(); }
                }

                public IStructTypeDictionary Structs
                {
                    get { throw new NotSupportedException(); }
                }

                public IFullTypeDictionary Types
                {
                    get { throw new NotSupportedException(); }
                }

                #endregion
            }
        }

    }
}
