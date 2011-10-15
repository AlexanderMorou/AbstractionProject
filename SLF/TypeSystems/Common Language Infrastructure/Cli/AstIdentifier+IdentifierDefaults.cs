using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    partial class AstIdentifier
    {
        private class DefaultGeneralDeclarationUniqueIdentifier :
            IGeneralDeclarationUniqueIdentifier
        {

            #region IDeclarationUniqueIdentifier<IGeneralDeclarationUniqueIdentifier> Members

            public string Name { get; private set; }
            #endregion

            public DefaultGeneralDeclarationUniqueIdentifier(string name)
            {
                this.Name = name;
            }

            #region IEquatable<IGeneralDeclarationUniqueIdentifier> Members

            public bool Equals(IGeneralDeclarationUniqueIdentifier other)
            {
                if (other is IGeneralMemberUniqueIdentifier || 
                    other is IGeneralTypeUniqueIdentifier ||
                    other is IAssemblyUniqueIdentifier)
                    return other.Equals(this);
                return other.Name == this.Name;
            }

            #endregion
        }

        private class DefaultBinaryOperatorUniqueIdentifier :
            IBinaryOperatorUniqueIdentifier,
            IGeneralMemberUniqueIdentifier,
            IGeneralDeclarationUniqueIdentifier
        {
            /// <summary>
            /// Returns the <see cref="CoercibleBinaryOperators"/> coerced
            /// by the <see cref="IBinaryOperatorUniqueIdentifier"/>.
            /// </summary>
            public CoercibleBinaryOperators Operator { get; private set; }

            /// <summary>
            /// Returns which side the required self reference
            /// the <see cref="IBinaryOperatorUniqueIdentifier"/>'s
            /// parent is on.
            /// </summary>
            public BinaryOpCoercionContainingSide ContainingSide { get; private set; }

            /// <summary>
            /// Returns the type of the other side of the expression
            /// used when performing the coercion.
            /// </summary>
            /// <remarks>If <see cref="ContainingSide"/>
            /// is <see cref="BinaryOpCoercionContainingSide.Both"/>
            /// <see cref="OtherSide"/> returns null.</remarks>
            public IType OtherSide { get; private set; }

            public DefaultBinaryOperatorUniqueIdentifier(CoercibleBinaryOperators @operator, BinaryOpCoercionContainingSide containingSide, IType otherSide)
            {
                this.Operator = @operator;
                this.ContainingSide = containingSide;
                this.OtherSide = otherSide;
            }

            public bool Equals(IBinaryOperatorUniqueIdentifier other)
            {
                if (other == null)
                    return false;
                switch (this.ContainingSide)
                {
                    case BinaryOpCoercionContainingSide.LeftSide:
                    case BinaryOpCoercionContainingSide.RightSide:
                        return other.Operator == this.Operator &&
                               other.ContainingSide == this.ContainingSide;
                    case BinaryOpCoercionContainingSide.Both:
                        return other.Operator == this.Operator &&
                            other.ContainingSide == BinaryOpCoercionContainingSide.Both;
                    default:
                        return false;
                }
            }

            #region IDeclarationUniqueIdentifier<IBinaryOperatorUniqueIdentifier> Members

            public string Name
            {
                get {
                    switch (this.Operator)
                    {
                        case CoercibleBinaryOperators.Add:
                            return CLICommon.BinaryOperatorNames.Addition;
                        case CoercibleBinaryOperators.Subtract:
                            return CLICommon.BinaryOperatorNames.Subtraction;
                        case CoercibleBinaryOperators.Multiply:
                            return CLICommon.BinaryOperatorNames.Multiply;
                        case CoercibleBinaryOperators.Divide:
                            return CLICommon.BinaryOperatorNames.Division;
                        case CoercibleBinaryOperators.Modulus:
                            return CLICommon.BinaryOperatorNames.Modulus;
                        case CoercibleBinaryOperators.BitwiseAnd:
                            return CLICommon.BinaryOperatorNames.BitwiseAnd;
                        case CoercibleBinaryOperators.BitwiseOr:
                            return CLICommon.BinaryOperatorNames.BitwiseOr;
                        case CoercibleBinaryOperators.ExclusiveOr:
                            return CLICommon.BinaryOperatorNames.ExclusiveOr;
                        case CoercibleBinaryOperators.LeftShift:
                            return CLICommon.BinaryOperatorNames.LeftShift;
                        case CoercibleBinaryOperators.RightShift:
                            return CLICommon.BinaryOperatorNames.RightShift;
                        case CoercibleBinaryOperators.IsEqualTo:
                            return CLICommon.BinaryOperatorNames.Equality;
                        case CoercibleBinaryOperators.IsNotEqualTo:
                            return CLICommon.BinaryOperatorNames.Inequality;
                        case CoercibleBinaryOperators.LessThan:
                            return CLICommon.BinaryOperatorNames.LessThan;
                        case CoercibleBinaryOperators.GreaterThan:
                            return CLICommon.BinaryOperatorNames.GreaterThan;
                        case CoercibleBinaryOperators.LessThanOrEqualTo:
                            return CLICommon.BinaryOperatorNames.LessThanOrEqual;
                        case CoercibleBinaryOperators.GreaterThanOrEqualTo:
                            return CLICommon.BinaryOperatorNames.GreaterThanOrEqual;
                        default:
                            return null;
                    }
                }
            }

            #endregion

            #region IEquatable<IGeneralMemberUniqueIdentifier> Members

            public bool Equals(IGeneralMemberUniqueIdentifier other)
            {
                if (other is IBinaryOperatorUniqueIdentifier)
                    return this.Equals(((IBinaryOperatorUniqueIdentifier)other));
                return false;
            }

            #endregion

            #region IEquatable<IGeneralDeclarationUniqueIdentifier> Members

            public bool Equals(IGeneralDeclarationUniqueIdentifier other)
            {
                if (other is IBinaryOperatorUniqueIdentifier)
                    return this.Equals(((IBinaryOperatorUniqueIdentifier)other));
                return false;
            }

            #endregion
        }

    }
}
