using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Provides a root abstract implementation of <see cref="IExpression"/> as well as operator
    /// overloads to aid in expression building.
    /// </summary>
    public abstract class ExpressionBase :
        MemberParentReferenceExpressionBase,
        IExpression
    {
        #region IExpression Members
        /** *
         * <summary>
         * Used in <see cref="op_True(ExpressionBase)"/> and <see cref="op_False(ExpressionBase)"/>
         * to determine which expressions are being compared for 
         * LogicalOr and LogicalAnd coercions.
         * </summary>
         * */
        private static List<ExpressionBase> tfOpCalls = new List<ExpressionBase>();
        #endregion

        /// <summary>
        /// Used to discern proper handling of "&amp;&amp;" and "||" calls.
        /// </summary>
        /// <param name="target">The <see cref="ExpressionBase"/>
        /// used in the '<see cref="op_True"/>' call.</param>
        /// <returns>false, inserting the <paramref name="target"/> into
        /// <see cref="tfOpCalls"/>.</returns>
        public static bool operator true(ExpressionBase target)
        {
            tfOpCalls.Add(target);
            return false;
        }

        /// <summary>
        /// Used to discern proper handling of "&amp;&amp;" and "||" calls.
        /// </summary>
        /// <param name="target">The <see cref="ExpressionBase"/>
        /// used in the '<see cref="op_False"/>' call.</param>
        /// <returns>false, inserting the <paramref name="target"/> into
        /// <see cref="tfOpCalls"/>.</returns>
        public static bool operator false(ExpressionBase target)
        {
            tfOpCalls.Add(target);
            return false;
        }
#if PRECEDENCEPRESENT
        /// <summary>
        /// Returns an <see cref="CSharpAddSubtExpression"/> concatination of 
        /// <paramref name="left"/> and <paramref name="right"/> with the 
        /// <see cref="CSharpAddSubtExpression.Operation"/> set to 
        /// <see cref="AddSubtOperation.Addition"/>.
        /// </summary>
        /// <param name="left">The <see cref="ExpressionBase"/> as the left-node of the
        /// <see cref="CSharpAddSubtExpression"/>.</param>
        /// <param name="right">The <see cref="ExpressionBase"/> as the right-node of the
        /// <see cref="CSharpAddSubtExpression"/>.</param>
        /// <returns>An <see cref="CSharpAddSubtExpression"/> concatination of 
        /// <paramref name="left"/> and <paramref name="right"/> with the 
        /// <see cref="CSharpAddSubtExpression.Operation"/> set to 
        /// <see cref="AddSubtOperation.Addition"/>.</returns>
        public static CSharpAddSubtExpression operator +(ExpressionBase left, ExpressionBase right)
        {
            ICSharpAddSubtExpression leftSide = (ICSharpAddSubtExpression)left.AffixTo(OperatorPrecedences.AddSubtOperation);
            ICSharpMulDivExpression rightSide = (ICSharpMulDivExpression)right.AffixTo(OperatorPrecedences.CSharpMulDivOperation);
            return new CSharpAddSubtExpression(leftSide, AddSubtOperation.Addition, rightSide);
        }

        /// <summary>
        /// Returns an <see cref="CSharpAddSubtExpression"/> concatination of 
        /// <paramref name="left"/> and <paramref name="right"/> with the 
        /// <see cref="CSharpAddSubtExpression.Operation"/> set to 
        /// <see cref="AddSubtOperation.Addition"/>.
        /// </summary>
        /// <param name="left">The <see cref="ExpressionBase"/> as the left-node of the
        /// <see cref="CSharpAddSubtExpression"/>.</param>
        /// <param name="right">The <see cref="CSharpIExpression"/> as the right-node of the
        /// <see cref="CSharpAddSubtExpression"/>.</param>
        /// <returns>An <see cref="CSharpAddSubtExpression"/> concatination of 
        /// <paramref name="left"/> and <paramref name="right"/> with the 
        /// <see cref="CSharpAddSubtExpression.Operation"/> set to 
        /// <see cref="AddSubtOperation.Addition"/>.</returns>
        public static CSharpAddSubtExpression operator +(ExpressionBase left, CSharpIExpression right)
        {
            ICSharpAddSubtExpression leftSide = (ICSharpAddSubtExpression)left.AffixTo(OperatorPrecedences.AddSubtOperation);
            ICSharpMulDivExpression rightSide = (ICSharpMulDivExpression)right.AffixTo(OperatorPrecedences.CSharpMulDivOperation);
            return new CSharpAddSubtExpression(leftSide, AddSubtOperation.Addition, rightSide);
        }

        /// <summary>
        /// Returns an <see cref="CSharpAddSubtExpression"/> concatination of 
        /// <paramref name="left"/> and <paramref name="right"/> with the 
        /// <see cref="CSharpAddSubtExpression.Operation"/> set to 
        /// <see cref="AddSubtOperation.Subtraction"/>.
        /// </summary>
        /// <param name="left">The <see cref="ExpressionBase"/> as the left-node of the
        /// <see cref="CSharpAddSubtExpression"/>.</param>
        /// <param name="right">The <see cref="ExpressionBase"/> as the right-node of the
        /// <see cref="CSharpAddSubtExpression"/>.</param>
        /// <returns>An <see cref="CSharpAddSubtExpression"/> concatination of 
        /// <paramref name="left"/> and <paramref name="right"/> with the 
        /// <see cref="CSharpAddSubtExpression.Operation"/> set to 
        /// <see cref="AddSubtOperation.Subtraction"/>.</returns>
        public static CSharpAddSubtExpression operator -(ExpressionBase left, ExpressionBase right)
        {
            ICSharpAddSubtExpression leftSide = (ICSharpAddSubtExpression)left.AffixTo(OperatorPrecedences.AddSubtOperation);
            ICSharpMulDivExpression rightSide = (ICSharpMulDivExpression)right.AffixTo(OperatorPrecedences.CSharpMulDivOperation);
            return new CSharpAddSubtExpression(leftSide, AddSubtOperation.Subtraction, rightSide);
        }

        /// <summary>
        /// Returns an <see cref="CSharpAddSubtExpression"/> concatination of 
        /// <paramref name="left"/> and <paramref name="right"/> with the 
        /// <see cref="CSharpAddSubtExpression.Operation"/> set to 
        /// <see cref="AddSubtOperation.Subtraction"/>.
        /// </summary>
        /// <param name="left">The <see cref="ExpressionBase"/> as the left-node of the
        /// <see cref="CSharpAddSubtExpression"/>.</param>
        /// <param name="right">The <see cref="CSharpIExpression"/> as the right-node of the
        /// <see cref="CSharpAddSubtExpression"/>.</param>
        /// <returns>An <see cref="CSharpAddSubtExpression"/> concatination of 
        /// <paramref name="left"/> and <paramref name="right"/> with the 
        /// <see cref="CSharpAddSubtExpression.Operation"/> set to 
        /// <see cref="AddSubtOperation.Subtraction"/>.</returns>
        public static CSharpAddSubtExpression operator -(ExpressionBase left, CSharpIExpression right)
        {
            ICSharpAddSubtExpression leftSide = (ICSharpAddSubtExpression)left.AffixTo(OperatorPrecedences.AddSubtOperation);
            ICSharpMulDivExpression rightSide = (ICSharpMulDivExpression)right.AffixTo(OperatorPrecedences.CSharpMulDivOperation);
            return new CSharpAddSubtExpression(leftSide, AddSubtOperation.Subtraction, rightSide);
        }

        public static CSharpMulDivExpression operator *(ExpressionBase left, ExpressionBase right)
        {
            ICSharpMulDivExpression leftSide = (ICSharpMulDivExpression)left.AffixTo(OperatorPrecedences.CSharpMulDivOperation);
            IUnaryOperationExpression rightSide = (IUnaryOperationExpression)right.AffixTo(OperatorPrecedences.UnaryOperation);
            return new CSharpMulDivExpression(leftSide, CSharpMulDivOperation.Multiplication, rightSide);
        }

        public static CSharpMulDivExpression operator *(ExpressionBase left, CSharpIExpression right)
        {
            ICSharpMulDivExpression leftSide = (ICSharpMulDivExpression)left.AffixTo(OperatorPrecedences.CSharpMulDivOperation);
            IUnaryOperationExpression rightSide = (IUnaryOperationExpression)right.AffixTo(OperatorPrecedences.UnaryOperation);
            return new CSharpMulDivExpression(leftSide, CSharpMulDivOperation.Multiplication, rightSide);
        }

        public static CSharpMulDivExpression operator /(ExpressionBase left, ExpressionBase right)
        {
            ICSharpMulDivExpression leftSide = (ICSharpMulDivExpression)left.AffixTo(OperatorPrecedences.CSharpMulDivOperation);
            IUnaryOperationExpression rightSide = (IUnaryOperationExpression)right.AffixTo(OperatorPrecedences.UnaryOperation);
            return new CSharpMulDivExpression(leftSide, CSharpMulDivOperation.Division, rightSide);
        }

        public static CSharpMulDivExpression operator /(ExpressionBase left, CSharpIExpression right)
        {
            ICSharpMulDivExpression leftSide = (ICSharpMulDivExpression)left.AffixTo(OperatorPrecedences.CSharpMulDivOperation);
            IUnaryOperationExpression rightSide = (IUnaryOperationExpression)right.AffixTo(OperatorPrecedences.UnaryOperation);
            return new CSharpMulDivExpression(leftSide, CSharpMulDivOperation.Division, rightSide);
        }

        public static CSharpMulDivExpression operator %(ExpressionBase left, ExpressionBase right)
        {
            ICSharpMulDivExpression leftSide = (ICSharpMulDivExpression)left.AffixTo(OperatorPrecedences.CSharpMulDivOperation);
            IUnaryOperationExpression rightSide = (IUnaryOperationExpression)right.AffixTo(OperatorPrecedences.UnaryOperation);
            return new CSharpMulDivExpression(leftSide, CSharpMulDivOperation.Remainder, rightSide);
        }

        public static CSharpMulDivExpression operator %(ExpressionBase left, CSharpIExpression right)
        {
            ICSharpMulDivExpression leftSide = (ICSharpMulDivExpression)left.AffixTo(OperatorPrecedences.CSharpMulDivOperation);
            IUnaryOperationExpression rightSide = (IUnaryOperationExpression)right.AffixTo(OperatorPrecedences.UnaryOperation);
            return new CSharpMulDivExpression(leftSide, CSharpMulDivOperation.Remainder, rightSide);
        }

        public static ICSharpnequalityExpression operator ==(ExpressionBase left, ExpressionBase right)
        {
            ICSharpInequalityExpression leftSide = (ICSharpInequalityExpression)left.AffixTo(OperatorPrecedences.InequalityOperation);
            ICSharpRelationalExpression rightSide = (ICSharpRelationalExpression)right.AffixTo(OperatorPrecedences.RelationalOperation);
            return new ICSharpnequalityExpression(leftSide, true, rightSide);
        }

        public static ICSharpnequalityExpression operator ==(ExpressionBase left, CSharpIExpression right)
        {
            ICSharpInequalityExpression leftSide = (ICSharpInequalityExpression)left.AffixTo(OperatorPrecedences.InequalityOperation);
            ICSharpRelationalExpression rightSide = (ICSharpRelationalExpression)right.AffixTo(OperatorPrecedences.RelationalOperation);
            return new ICSharpnequalityExpression(leftSide, true, rightSide);
        }

        public static ICSharpnequalityExpression operator !=(ExpressionBase left, ExpressionBase right)
        {
            ICSharpInequalityExpression leftSide = (ICSharpInequalityExpression)left.AffixTo(OperatorPrecedences.InequalityOperation);
            ICSharpRelationalExpression rightSide = (ICSharpRelationalExpression)right.AffixTo(OperatorPrecedences.RelationalOperation);
            return new ICSharpnequalityExpression(leftSide, false, rightSide);
        }

        public static ICSharpnequalityExpression operator !=(ExpressionBase left, CSharpIExpression right)
        {
            ICSharpInequalityExpression leftSide = (ICSharpInequalityExpression)left.AffixTo(OperatorPrecedences.InequalityOperation);
            ICSharpRelationalExpression rightSide = (ICSharpRelationalExpression)right.AffixTo(OperatorPrecedences.RelationalOperation);
            return new ICSharpnequalityExpression(leftSide, false, rightSide);
        }

        public static CSharpRelationalExpression operator <(ExpressionBase left, ExpressionBase right)
        {
            ICSharpRelationalExpression leftSide = (ICSharpRelationalExpression)left.AffixTo(OperatorPrecedences.RelationalOperation);
            ICSharpShiftExpression rightSide = (ICSharpShiftExpression)right.AffixTo(OperatorPrecedences.ShiftOperation);
            return new CSharpRelationalExpression(leftSide, RelationalOperation.LessThan, rightSide);
        }

        public static CSharpRelationalExpression operator <(ExpressionBase left, CSharpIExpression right)
        {
            ICSharpRelationalExpression leftSide = (ICSharpRelationalExpression)left.AffixTo(OperatorPrecedences.RelationalOperation);
            ICSharpShiftExpression rightSide = (ICSharpShiftExpression)right.AffixTo(OperatorPrecedences.ShiftOperation);
            return new CSharpRelationalExpression(leftSide, RelationalOperation.LessThan, rightSide);
        }

        public static CSharpRelationalExpression operator <=(ExpressionBase left, ExpressionBase right)
        {
            ICSharpRelationalExpression leftSide = (ICSharpRelationalExpression)left.AffixTo(OperatorPrecedences.RelationalOperation);
            ICSharpShiftExpression rightSide = (ICSharpShiftExpression)right.AffixTo(OperatorPrecedences.ShiftOperation);
            return new CSharpRelationalExpression(leftSide, RelationalOperation.LessThanOrEqualTo, rightSide);
        }

        public static CSharpRelationalExpression operator <=(ExpressionBase left, CSharpIExpression right)
        {
            ICSharpRelationalExpression leftSide = (ICSharpRelationalExpression)left.AffixTo(OperatorPrecedences.RelationalOperation);
            ICSharpShiftExpression rightSide = (ICSharpShiftExpression)right.AffixTo(OperatorPrecedences.ShiftOperation);
            return new CSharpRelationalExpression(leftSide, RelationalOperation.LessThanOrEqualTo, rightSide);
        }

        public static CSharpRelationalExpression operator >(ExpressionBase left, ExpressionBase right)
        {
            ICSharpRelationalExpression leftSide = (ICSharpRelationalExpression)left.AffixTo(OperatorPrecedences.RelationalOperation);
            ICSharpShiftExpression rightSide = (ICSharpShiftExpression)right.AffixTo(OperatorPrecedences.ShiftOperation);
            return new CSharpRelationalExpression(leftSide, RelationalOperation.GreaterThan, rightSide);
        }

        public static CSharpRelationalExpression operator >(ExpressionBase left, CSharpIExpression right)
        {
            ICSharpRelationalExpression leftSide = (ICSharpRelationalExpression)left.AffixTo(OperatorPrecedences.RelationalOperation);
            ICSharpShiftExpression rightSide = (ICSharpShiftExpression)right.AffixTo(OperatorPrecedences.ShiftOperation);
            return new CSharpRelationalExpression(leftSide, RelationalOperation.GreaterThan, rightSide);
        }

        public static CSharpRelationalExpression operator >=(ExpressionBase left, ExpressionBase right)
        {
            ICSharpRelationalExpression leftSide = (ICSharpRelationalExpression)left.AffixTo(OperatorPrecedences.RelationalOperation);
            ICSharpShiftExpression rightSide = (ICSharpShiftExpression)right.AffixTo(OperatorPrecedences.ShiftOperation);
            return new CSharpRelationalExpression(leftSide, RelationalOperation.GreaterThanOrEqualTo, rightSide);
        }

        public static CSharpRelationalExpression operator >=(ExpressionBase left, CSharpIExpression right)
        {
            ICSharpRelationalExpression leftSide = (ICSharpRelationalExpression)left.AffixTo(OperatorPrecedences.RelationalOperation);
            ICSharpShiftExpression rightSide = (ICSharpShiftExpression)right.AffixTo(OperatorPrecedences.ShiftOperation);
            return new CSharpRelationalExpression(leftSide, RelationalOperation.GreaterThanOrEqualTo, rightSide);
        }

        public static CSharpBitwiseExclusiveOrExpression operator ^(ExpressionBase left, ExpressionBase right)
        {
            ICSharpBitwiseExclusiveOrExpression leftSide = (ICSharpBitwiseExclusiveOrExpression)left.AffixTo(OperatorPrecedences.BitwiseExclusiveOrOperation);
            ICSharpBitwiseAndExpression rightSide = (ICSharpBitwiseAndExpression)right.AffixTo(OperatorPrecedences.BitwiseAndOperation);
            return new CSharpBitwiseExclusiveOrExpression(leftSide, rightSide);
        }

        public static CSharpBitwiseExclusiveOrExpression operator ^(ExpressionBase left, CSharpIExpression right)
        {
            ICSharpBitwiseExclusiveOrExpression leftSide = (ICSharpBitwiseExclusiveOrExpression)left.AffixTo(OperatorPrecedences.BitwiseExclusiveOrOperation);
            ICSharpBitwiseAndExpression rightSide = (ICSharpBitwiseAndExpression)right.AffixTo(OperatorPrecedences.BitwiseAndOperation);
            return new CSharpBitwiseExclusiveOrExpression(leftSide, rightSide);
        }

        public static ExpressionBase operator |(ExpressionBase left, ExpressionBase right)
        {
            if (tfOpCalls.Contains(left))
            {
                tfOpCalls.Remove(left);
                ICSharpLogicalOrExpression leftSide = (ICSharpLogicalOrExpression)left.AffixTo(OperatorPrecedences.LogicalOrOperation);
                ICSharpLogicalAndExpression rightSide = (ICSharpLogicalAndExpression)right.AffixTo(OperatorPrecedences.LogicalAndOperation);
                return new CSharpLogicalOrExpression(leftSide, rightSide);
            }
            else
            {
                ICSharpBitwiseOrExpression leftSide = (ICSharpBitwiseOrExpression)left.AffixTo(OperatorPrecedences.BitwiseOrOperation);
                ICSharpBitwiseExclusiveOrExpression rightSide = (ICSharpBitwiseExclusiveOrExpression)right.AffixTo(OperatorPrecedences.BitwiseExclusiveOrOperation);
                return new CSharpBitwiseOrExpression(leftSide, rightSide);
            }
        }

        public static ExpressionBase operator |(ExpressionBase left, CSharpIExpression right)
        {
            if (tfOpCalls.Contains(left))
            {
                tfOpCalls.Remove(left);
                ICSharpLogicalOrExpression leftSide = (ICSharpLogicalOrExpression)left.AffixTo(OperatorPrecedences.LogicalOrOperation);
                ICSharpLogicalAndExpression rightSide = (ICSharpLogicalAndExpression)right.AffixTo(OperatorPrecedences.LogicalAndOperation);
                return new CSharpLogicalOrExpression(leftSide, rightSide);
            }
            else
            {
                ICSharpBitwiseOrExpression leftSide = (ICSharpBitwiseOrExpression)left.AffixTo(OperatorPrecedences.BitwiseOrOperation);
                ICSharpBitwiseExclusiveOrExpression rightSide = (ICSharpBitwiseExclusiveOrExpression)right.AffixTo(OperatorPrecedences.BitwiseExclusiveOrOperation);
                return new CSharpBitwiseOrExpression(leftSide, rightSide);
            }
        }

        public static ExpressionBase operator &(ExpressionBase left, ExpressionBase right)
        {
            if (tfOpCalls.Contains(left))
            {
                tfOpCalls.Remove(left);
                ICSharpLogicalAndExpression leftSide = (ICSharpLogicalAndExpression)left.AffixTo(OperatorPrecedences.LogicalAndOperation);
                ICSharpBitwiseOrExpression rightSide = (ICSharpBitwiseOrExpression)right.AffixTo(OperatorPrecedences.BitwiseOrOperation);
                return new CSharpLogicalAndExpression(leftSide, rightSide);
            }
            else
            {
                ICSharpBitwiseAndExpression leftSide = (ICSharpBitwiseAndExpression)left.AffixTo(OperatorPrecedences.BitwiseAndOperation);
                ICSharpInequalityExpression rightSide = (ICSharpInequalityExpression)right.AffixTo(OperatorPrecedences.InequalityOperation);
                return new CSharpBitwiseAndExpression(leftSide, rightSide);
            }
        }

        public static ExpressionBase operator &(ExpressionBase left, CSharpIExpression right)
        {
            if (tfOpCalls.Contains(left))
            {
                tfOpCalls.Remove(left);
                ICSharpLogicalAndExpression leftSide = (ICSharpLogicalAndExpression)left.AffixTo(OperatorPrecedences.LogicalAndOperation);
                ICSharpBitwiseOrExpression rightSide = (ICSharpBitwiseOrExpression)right.AffixTo(OperatorPrecedences.BitwiseOrOperation);
                return new CSharpLogicalAndExpression(leftSide, rightSide);
            }
            else
            {
                ICSharpBitwiseAndExpression leftSide = (ICSharpBitwiseAndExpression)left.AffixTo(OperatorPrecedences.BitwiseAndOperation);
                ICSharpInequalityExpression rightSide = (ICSharpInequalityExpression)right.AffixTo(OperatorPrecedences.InequalityOperation);
                return new CSharpBitwiseAndExpression(leftSide, rightSide);
            }
        }

        public static UnaryOperationExpression operator !(ExpressionBase left)
        {
            return new UnaryOperationExpression((IUnaryOperationPrimaryTerm)(left.AffixTo(OperatorPrecedences.UnaryTerm)), CSharpUnaryOperationFlags.Invert);
        }
        public static UnaryOperationExpression operator -(ExpressionBase left)
        {
            return new UnaryOperationExpression((IUnaryOperationPrimaryTerm)(left.AffixTo(OperatorPrecedences.UnaryTerm)), CSharpUnaryOperationFlags.Negate);
        }
#endif
        public static implicit operator ExpressionBase(int value)
        {
            return value.ToPrimitive();
        }

        public static implicit operator ExpressionBase(uint value)
        {
            return value.ToPrimitive();
        }

        public static implicit operator ExpressionBase(decimal value)
        {
            return value.ToPrimitive();
        }

        public static implicit operator ExpressionBase(bool value)
        {
            return value.ToPrimitive();
        }

        public static implicit operator ExpressionBase(float value)
        {
            return value.ToPrimitive();
        }

        public static implicit operator ExpressionBase(double value)
        {
            return value.ToPrimitive();
        }

        public static implicit operator ExpressionBase(ulong value)
        {
            return value.ToPrimitive();
        }

        public static implicit operator ExpressionBase(long value)
        {
            return value.ToPrimitive();
        }

        public static implicit operator ExpressionBase(string value)
        {
            return value.ToPrimitive();
        }


    }
}
