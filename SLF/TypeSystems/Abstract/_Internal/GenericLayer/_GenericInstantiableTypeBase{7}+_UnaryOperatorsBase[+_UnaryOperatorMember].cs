using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    partial class _GenericInstantiableTypeBase<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType>
        where TCtor :
            class,
            IConstructorMember<TCtor, TType>
        where TEvent :
            IEventMember<TEvent, TType>
        where TField :
            IFieldMember<TField, TType>,
            IInstanceMember
        where TIndexer :
            IIndexerMember<TIndexer, TType>
        where TMethod :
            IMethodMember<TMethod, TType>,
            IExtendedInstanceMember
        where TProperty :
            IPropertyMember<TProperty, TType>
        where TType :
            class,
            IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, IGeneralGenericTypeUniqueIdentifier, TType>,
            IGenericType<IGeneralGenericTypeUniqueIdentifier, TType>
    {
        private class _UnaryOperatorsBase :
            _GroupedMembersBase<TType, IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TType>, IUnaryOperatorCoercionMemberDictionary<TType>>,
            IUnaryOperatorCoercionMemberDictionary<TType>,
            IUnaryOperatorCoercionMemberDictionary
        {
            public _UnaryOperatorsBase(_FullMembersBase master, IUnaryOperatorCoercionMemberDictionary<TType> originalSet, _GenericInstantiableTypeBase<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType> parent)
                : base(master, originalSet, ((TType)(object)(parent)))
            {
            }
            private new _GenericInstantiableTypeBase<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType> Parent
            {
                get
                {
                    return (_GenericInstantiableTypeBase<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType>)(object)base.Parent;
                }
            }

            #region IUnaryOperatorCoercionMemberDictionary<TType> Members

            public IUnaryOperatorCoercionMember<TType> this[CoercibleUnaryOperators op]
            {
                get
                {
                    foreach (var unop in this.Values)
                        if (unop.Operator == op)
                            return unop;
                    return null;
                }
            }

            #endregion


            #region IUnaryOperatorCoercionMemberDictionary Members

            IUnaryOperatorCoercionMember IUnaryOperatorCoercionMemberDictionary.this[CoercibleUnaryOperators op]
            {
                get 
                {
                    return this[op];
                }
            }

            public bool ContainsOverload(CoercibleUnaryOperators op)
            {
                foreach (var unop in this.Values)
                    if (unop.Operator == op)
                        return true;
                return false;
            }

            #endregion

            protected override IUnaryOperatorCoercionMember<TType> ObtainWrapper(IUnaryOperatorCoercionMember<TType> item)
            {
                return new _UnaryOperatorMember(item, this.Parent);
            }

            private class _UnaryOperatorMember :
                _MemberBase<IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TType>, TType>,
                IUnaryOperatorCoercionMember<TType>
            {
                private new _GenericInstantiableTypeBase<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType> Parent
                {
                    get
                    {
                        return (_GenericInstantiableTypeBase<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType>)(object)base.Parent;
                    }
                }
                internal _UnaryOperatorMember(IUnaryOperatorCoercionMember<TType> original, _GenericInstantiableTypeBase<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType> parent)
                    : base(original, ((TType)(object)(parent)))
                {
                }
                public override IUnaryOperatorUniqueIdentifier UniqueIdentifier
                {
                    get
                    {
                        return TypeSystemIdentifiers.GetUnaryOperatorIdentifier(this.Operator);
                    }
                }

                #region IMetadataEntity Members

                public IMetadataCollection Metadata
                {
                    get { return this.Original.Metadata; }
                }

                public bool IsDefined(IType metadatumType)
                {
                    return this.Original.IsDefined(metadatumType);
                }

                #endregion

                #region ICoercionMember Members

                ICoercibleType ICoercionMember.Parent
                {
                    get { return base.Parent; }
                }

                #endregion

                #region IScopedDeclaration Members

                public AccessLevelModifiers AccessLevel
                {
                    get { return this.Original.AccessLevel; }
                }

                #endregion

                #region IUnaryOperatorCoercionMember Members

                public CoercibleUnaryOperators Operator
                {
                    get { return this.Original.Operator; }
                }

                public IType ResultedType
                {
                    get {
                        if (Parent.IsGenericConstruct && !Parent.IsGenericDefinition)
                            return Original.ResultedType.Disambiguify(Parent.GenericParameters, null, TypeParameterSources.Type);
                        return Original.ResultedType;
                    }
                }

                #endregion
            }
        }
    }
}
