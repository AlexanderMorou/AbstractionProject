using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
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
        private class _BinaryOperatorsBase :
            _GroupedMembersBase<TType, IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<IGeneralGenericTypeUniqueIdentifier, TType>, IBinaryOperatorCoercionMemberDictionary<IGeneralGenericTypeUniqueIdentifier, TType>>,
            IBinaryOperatorCoercionMemberDictionary<IGeneralGenericTypeUniqueIdentifier, TType>,
            IBinaryOperatorCoercionMemberDictionary
        {

            public _BinaryOperatorsBase(_FullMembersBase master, IBinaryOperatorCoercionMemberDictionary<IGeneralGenericTypeUniqueIdentifier, TType> originalSet, TType parent)
                : base(master, originalSet, parent)
            {
            }

            #region IBinaryOperatorCoercionMemberDictionary<TType> Members

            public IBinaryOperatorCoercionMember<IGeneralGenericTypeUniqueIdentifier, TType> this[CoercibleBinaryOperators op, BinaryOpCoercionContainingSide side, IType otherSide]
            {
                get
                {
                    foreach (var binOpC in this.Values)
                        if (binOpC.Operator == op && binOpC.ContainingSide == side && binOpC.OtherSide.Equals(otherSide))
                            return binOpC;
                    return null;
                }
            }

            public IBinaryOperatorCoercionMember<IGeneralGenericTypeUniqueIdentifier, TType> this[CoercibleBinaryOperators op]
            {
                get
                {
                    foreach (var binOpC in this.Values)
                        if (binOpC.ContainingSide == BinaryOpCoercionContainingSide.Both && op == binOpC.Operator)
                        {
                            return binOpC;
                        }
                    return null;
                }
            }

            #endregion

            #region IBinaryOperatorCoercionMemberDictionary Members

            IBinaryOperatorCoercionMember IBinaryOperatorCoercionMemberDictionary.this[CoercibleBinaryOperators op, BinaryOpCoercionContainingSide side, IType otherSide]
            {
                get { return this[op, side, otherSide]; }
            }

            IBinaryOperatorCoercionMember IBinaryOperatorCoercionMemberDictionary.this[CoercibleBinaryOperators op]
            {
                get { return this[op]; }
            }

            #endregion

            protected override IBinaryOperatorCoercionMember<IGeneralGenericTypeUniqueIdentifier, TType> ObtainWrapper(IBinaryOperatorCoercionMember<IGeneralGenericTypeUniqueIdentifier, TType> item)
            {
                return new _BinaryOperator(item, this.Parent);
            }

            private class _BinaryOperator :
                _MemberBase<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<IGeneralGenericTypeUniqueIdentifier, TType>, TType>,
                IBinaryOperatorCoercionMember<IGeneralGenericTypeUniqueIdentifier, TType>
            {
                internal _BinaryOperator(IBinaryOperatorCoercionMember<IGeneralGenericTypeUniqueIdentifier, TType> original, TType parent)
                    : base(original, parent)
                {
                }


                #region ICoercionMember Members

                ICoercibleType ICoercionMember.Parent
                {
                    get { return base.Parent; }
                }

                #endregion

                #region IScopedDeclaration Members

                public AccessLevelModifiers AccessLevel
                {
                    get { return base.Original.AccessLevel; }
                }

                #endregion

                #region IBinaryOperatorCoercionMember Members

                public CoercibleBinaryOperators Operator
                {
                    get { return base.Original.Operator; }
                }

                public BinaryOpCoercionContainingSide ContainingSide
                {
                    get { return base.Original.ContainingSide; }
                }

                public IType OtherSide
                {
                    get
                    {
                        if (Parent.IsGenericConstruct && !Parent.IsGenericDefinition)
                            return Original.OtherSide.Disambiguify(Parent.GenericParameters, null, TypeParameterSources.Type);
                        return Original.OtherSide;
                    }
                }

                public IType ReturnType
                {
                    get
                    {
                        if (Parent.IsGenericConstruct && !Parent.IsGenericDefinition)
                            return Original.ReturnType.Disambiguify(Parent.GenericParameters, null, TypeParameterSources.Type);
                        return Original.ReturnType;
                    }
                }

                #endregion

                public override IBinaryOperatorUniqueIdentifier UniqueIdentifier
                {
                    get
                    {
                        return AstIdentifier.BinaryOperator(this.Operator, this.ContainingSide, this.OtherSide);
                    }
                }
            }
        }
    }
}
