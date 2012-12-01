using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CliGenericInstantiableTypeBase<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType>
        where TIndexer :
            class,
            IIndexerMember<TIndexer, TType>
        where TMethod :
            class,
            IMethodMember<TMethod, TType>,
            IExtendedInstanceMember
        where TProperty :
            class,
            IPropertyMember<TProperty, TType>
        where TField :
            class,
            IFieldMember<TField, TType>,
            IInstanceMember
        where TCtor :
            class,
            IConstructorMember<TCtor, TType>
        where TEvent :
            class,
            IEventMember<TEvent, TType>
        where TType :
            class,
            IGenericType<IGeneralGenericTypeUniqueIdentifier, TType>,
            IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, IGeneralGenericTypeUniqueIdentifier, TType>
    {
        private class BinaryOperatorMember :
            CliMemberBase<IBinaryOperatorUniqueIdentifier, TType, ICliMetadataMethodDefinitionTableRow>,
            IBinaryOperatorCoercionMember<TType>
        {
            private IBinaryOperatorUniqueIdentifier identifier;

            internal BinaryOperatorMember(IBinaryOperatorUniqueIdentifier identifier, ICliMetadataMethodDefinitionTableRow metadataEntry, TType parent)
                : base(parent, metadataEntry)
            {
                this.identifier = identifier;
            }

            protected override string OnGetName()
            {
                IType left = null, right = null;
                switch (this.ContainingSide)
                {
                    case BinaryOpCoercionContainingSide.LeftSide:
                        left = this.Parent;
                        right = this.OtherSide;
                        break;
                    case BinaryOpCoercionContainingSide.RightSide:
                        left = this.OtherSide;
                        right = this.Parent;
                        break;
                    case BinaryOpCoercionContainingSide.Both:
                        left = right = this.Parent;
                        break;
                }
                string operatorText = null;
                switch (this.Operator)
                {
                    case CoercibleBinaryOperators.Add:
                        operatorText = CliCommon.BinaryOperatorNames.Addition;
                        break;
                    case CoercibleBinaryOperators.Subtract:
                        operatorText = CliCommon.BinaryOperatorNames.Subtraction;
                        break;
                    case CoercibleBinaryOperators.Multiply:
                        operatorText = CliCommon.BinaryOperatorNames.Multiply;
                        break;
                    case CoercibleBinaryOperators.Divide:
                        operatorText = CliCommon.BinaryOperatorNames.Division;
                        break;
                    case CoercibleBinaryOperators.Modulus:
                        operatorText = CliCommon.BinaryOperatorNames.Modulus;
                        break;
                    case CoercibleBinaryOperators.BitwiseAnd:
                        operatorText = CliCommon.BinaryOperatorNames.BitwiseAnd;
                        break;
                    case CoercibleBinaryOperators.BitwiseOr:
                        operatorText = CliCommon.BinaryOperatorNames.BitwiseOr;
                        break;
                    case CoercibleBinaryOperators.ExclusiveOr:
                        operatorText = CliCommon.BinaryOperatorNames.ExclusiveOr;
                        break;
                    case CoercibleBinaryOperators.LeftShift:
                        operatorText = CliCommon.BinaryOperatorNames.LeftShift;
                        break;
                    case CoercibleBinaryOperators.RightShift:
                        operatorText = CliCommon.BinaryOperatorNames.RightShift;
                        break;
                    case CoercibleBinaryOperators.IsEqualTo:
                        operatorText = CliCommon.BinaryOperatorNames.Equality;
                        break;
                    case CoercibleBinaryOperators.IsNotEqualTo:
                        operatorText = CliCommon.BinaryOperatorNames.Inequality;
                        break;
                    case CoercibleBinaryOperators.LessThan:
                        operatorText = CliCommon.BinaryOperatorNames.LessThan;
                        break;
                    case CoercibleBinaryOperators.GreaterThan:
                        operatorText = CliCommon.BinaryOperatorNames.GreaterThan;
                        break;
                    case CoercibleBinaryOperators.LessThanOrEqualTo:
                        operatorText = CliCommon.BinaryOperatorNames.LessThanOrEqual;
                        break;
                    case CoercibleBinaryOperators.GreaterThanOrEqualTo:
                        operatorText = CliCommon.BinaryOperatorNames.GreaterThanOrEqual;
                        break;
                    default:
                        throw new InvalidOperationException();
                }
                return string.Format("{0} {1}({2}, {3})", this.ReturnType, operatorText, left, right);
            }

            public override IBinaryOperatorUniqueIdentifier UniqueIdentifier
            {
                get { return this.identifier; }
            }

            ICoercibleType ICoercionMember.Parent
            {
                get { return this.Parent; }
            }

            public AccessLevelModifiers AccessLevel
            {
                get { return AccessLevelModifiers.Public; }
            }

            public CoercibleBinaryOperators Operator
            {
                get { return this.UniqueIdentifier.Operator; }
            }

            public BinaryOpCoercionContainingSide ContainingSide
            {
                get { return this.UniqueIdentifier.ContainingSide; }
            }

            public IType OtherSide
            {
                get {
                    switch (this.ContainingSide)
                    {
                        case BinaryOpCoercionContainingSide.LeftSide:
                        case BinaryOpCoercionContainingSide.RightSide:
                            return this.UniqueIdentifier.OtherSide;
                        case BinaryOpCoercionContainingSide.Both:
                            return this.Parent;
                        default:
                            throw new InvalidOperationException();
                    }
                }
            }

            private _ICliManager IdentityManager { get { return (_ICliManager)this.Parent.IdentityManager; } }

            public IType ReturnType
            {
                get { return this.IdentityManager.ObtainTypeReference(this.MetadataEntry.Signature.ReturnType, this.Parent, null); }
            }
        }
    }
}
