using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{

    internal class CliBinaryOperatorUniqueIdentifier :
        IBinaryOperatorUniqueIdentifier,
        IGeneralMemberUniqueIdentifier
    {
        /// <summary>
        /// Returns the <see cref="CoercibleBinaryOperators"/> coerced
        /// by the <see cref="IBinaryOperatorUniqueIdentifier"/>.
        /// </summary>
        public CoercibleBinaryOperators Operator
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns which side the required self reference
        /// the <see cref="IBinaryOperatorUniqueIdentifier"/>'s
        /// parent is on.
        /// </summary>
        public BinaryOpCoercionContainingSide ContainingSide
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the type of the other side of the expression
        /// used when performing the coercion.
        /// </summary>
        /// <remarks>If <see cref="ContainingSide"/>
        /// is <see cref="BinaryOpCoercionContainingSide.Both"/>
        /// <see cref="OtherSide"/> returns null.</remarks>
        public IType OtherSide
        {
            get;
            private set;
        }

        public CliBinaryOperatorUniqueIdentifier(CoercibleBinaryOperators @operator, BinaryOpCoercionContainingSide containingSide, IType otherSide)
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

        //#region IDeclarationUniqueIdentifier Members

        public string Name
        {
            get
            {
                switch (this.Operator)
                {
                    case CoercibleBinaryOperators.Add:
                        return CliCommon.BinaryOperatorNames.Addition;
                    case CoercibleBinaryOperators.Subtract:
                        return CliCommon.BinaryOperatorNames.Subtraction;
                    case CoercibleBinaryOperators.Multiply:
                        return CliCommon.BinaryOperatorNames.Multiply;
                    case CoercibleBinaryOperators.Divide:
                        return CliCommon.BinaryOperatorNames.Division;
                    case CoercibleBinaryOperators.Modulus:
                        return CliCommon.BinaryOperatorNames.Modulus;
                    case CoercibleBinaryOperators.BitwiseAnd:
                        return CliCommon.BinaryOperatorNames.BitwiseAnd;
                    case CoercibleBinaryOperators.BitwiseOr:
                        return CliCommon.BinaryOperatorNames.BitwiseOr;
                    case CoercibleBinaryOperators.ExclusiveOr:
                        return CliCommon.BinaryOperatorNames.ExclusiveOr;
                    case CoercibleBinaryOperators.LeftShift:
                        return CliCommon.BinaryOperatorNames.LeftShift;
                    case CoercibleBinaryOperators.RightShift:
                        return CliCommon.BinaryOperatorNames.RightShift;
                    case CoercibleBinaryOperators.IsEqualTo:
                        return CliCommon.BinaryOperatorNames.Equality;
                    case CoercibleBinaryOperators.IsNotEqualTo:
                        return CliCommon.BinaryOperatorNames.Inequality;
                    case CoercibleBinaryOperators.LessThan:
                        return CliCommon.BinaryOperatorNames.LessThan;
                    case CoercibleBinaryOperators.GreaterThan:
                        return CliCommon.BinaryOperatorNames.GreaterThan;
                    case CoercibleBinaryOperators.LessThanOrEqualTo:
                        return CliCommon.BinaryOperatorNames.LessThanOrEqual;
                    case CoercibleBinaryOperators.GreaterThanOrEqualTo:
                        return CliCommon.BinaryOperatorNames.GreaterThanOrEqual;
                    default:
                        return null;
                }
            }
        }

        //#endregion

        //#region IEquatable<IGeneralMemberUniqueIdentifier> Members

        public bool Equals(IGeneralMemberUniqueIdentifier other)
        {
            if (other is IBinaryOperatorUniqueIdentifier)
                return this.Equals(((IBinaryOperatorUniqueIdentifier) other));
            return false;
        }

        //#endregion

        //#region IEquatable<IGeneralDeclarationUniqueIdentifier> Members

        public bool Equals(IGeneralDeclarationUniqueIdentifier other)
        {
            if (other is IBinaryOperatorUniqueIdentifier)
                return this.Equals(((IBinaryOperatorUniqueIdentifier) other));
            return false;
        }

        //#endregion

        public override bool Equals(object obj)
        {
            if (obj is IBinaryOperatorUniqueIdentifier)
                return this.Equals(((IBinaryOperatorUniqueIdentifier) obj));
            return false;
        }

        public override int GetHashCode()
        {
            switch (this.ContainingSide)
            {
                case BinaryOpCoercionContainingSide.LeftSide:
                    if (this.OtherSide != null)
                        return this.OtherSide.GetHashCode() ^ this.Operator.GetHashCode() ^ this.ContainingSide.GetHashCode();
                    else
                        return -3 ^ this.Operator.GetHashCode() ^ this.ContainingSide.GetHashCode();
                case BinaryOpCoercionContainingSide.RightSide:
                    if (this.OtherSide != null)
                        return this.OtherSide.GetHashCode() ^ this.Operator.GetHashCode() ^ this.ContainingSide.GetHashCode();
                    else
                        return -2 ^ this.Operator.GetHashCode() ^ this.ContainingSide.GetHashCode();
                case BinaryOpCoercionContainingSide.Both:
                    return -4 ^ this.Operator.GetHashCode() ^ this.ContainingSide.GetHashCode();
                default:
                    return -1 ^ this.ContainingSide.GetHashCode();
            }
        }

        public override string ToString()
        {
            switch (this.ContainingSide)
            {
                case BinaryOpCoercionContainingSide.LeftSide:
                    return string.Format("{0}(<originType>, {1})", this.Name, this.OtherSide);
                case BinaryOpCoercionContainingSide.RightSide:
                    return string.Format("{0}({1}, <originType>)", this.Name, this.OtherSide);
                case BinaryOpCoercionContainingSide.Both:
                    return string.Format("{0}(<originType>, <originType>)", this.Name);
                default:
                    return string.Format("{0}(<unknown>, <unknown>)", this.Name);
            }
        }
    }

    internal class CliUnaryOperatorUniqueIdentifier :
        IUnaryOperatorUniqueIdentifier
    {

        public CliUnaryOperatorUniqueIdentifier(CoercibleUnaryOperators @operator)
        {
            this.Operator = @operator;
        }
        //#region IUnaryOperatorUniqueIdentifier Members

        public CoercibleUnaryOperators Operator
        {
            get;
            private set;
        }

        //#endregion

        //#region IDeclarationUniqueIdentifier Members

        public string Name
        {
            get
            {
                switch (this.Operator)
                {
                    case CoercibleUnaryOperators.Plus:
                        return CliCommon.UnaryOperatorNames.Plus;
                    case CoercibleUnaryOperators.Negation:
                        return CliCommon.UnaryOperatorNames.Negation;
                    case CoercibleUnaryOperators.EvaluatesToFalse:
                        return CliCommon.UnaryOperatorNames.False;
                    case CoercibleUnaryOperators.EvaluatesToTrue:
                        return CliCommon.UnaryOperatorNames.True;
                    case CoercibleUnaryOperators.LogicalInvert:
                        return CliCommon.UnaryOperatorNames.LogicalNot;
                    case CoercibleUnaryOperators.Complement:
                        return CliCommon.UnaryOperatorNames.OnesComplement;
                    case CoercibleUnaryOperators.Increment:
                        return CliCommon.UnaryOperatorNames.Increment;
                    case CoercibleUnaryOperators.Decrement:
                        return CliCommon.UnaryOperatorNames.Decrement;
                    default:
                        return string.Empty;
                }
            }
        }

        //#endregion

        //#region IEquatable<IUnaryOperatorUniqueIdentifier> Members

        public bool Equals(IUnaryOperatorUniqueIdentifier other)
        {
            if (other == null)
                return false;
            return other.Operator == this.Operator;
        }

        //#endregion

        //#region IEquatable<IGeneralMemberUniqueIdentifier> Members

        public bool Equals(IGeneralMemberUniqueIdentifier other)
        {
            if (other is IUnaryOperatorUniqueIdentifier)
                return this.Equals((IUnaryOperatorUniqueIdentifier) other);
            return false;
        }

        //#endregion

        //#region IEquatable<IGeneralDeclarationUniqueIdentifier> Members

        public bool Equals(IGeneralDeclarationUniqueIdentifier other)
        {
            if (other is IUnaryOperatorUniqueIdentifier)
                return this.Equals((IUnaryOperatorUniqueIdentifier) other);
            return false;
        }

        //#endregion

        public override int GetHashCode()
        {
            return this.Operator.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is IUnaryOperatorUniqueIdentifier)
                return this.Equals((IUnaryOperatorUniqueIdentifier) obj);
            return false;
        }

        public override string ToString()
        {
            return string.Format("{0}(<originType>)", this.Name);
        }
    }
}
