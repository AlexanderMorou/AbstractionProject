using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Provides a base implementation of <see cref="ICSharpShiftExpression"/> which represents
    /// a shift expression operation.
    /// </summary>
    public partial class CSharpShiftExpression :
        CSharpBinaryOperationExpressionBase<ICSharpShiftExpression, ICSharpAddSubtExpression>,
        ICSharpShiftExpression
    {
        /// <summary>
        /// Data member for <see cref="Operation"/>.
        /// </summary>
        private CSharpShiftOperation operation;

        /// <summary>
        /// Creates a new <see cref="CSharpShiftExpression"/> with the <paramref name="leftSide"/>,
        /// <paramref name="operation"/> and <paramref name="rightSide"/> provided.
        /// </summary>
        /// <param name="leftSide">The <see cref="ICSharpShiftExpression"/> the 
        /// <see cref="CSharpShiftExpression"/> uses as its left operand.</param>
        /// <param name="rightSide">The <see cref="ICSharpAddSubtExpression"/> the <see cref="CSharpShiftExpression"/>
        /// uses as its right operand.</param>
        public CSharpShiftExpression(ICSharpShiftExpression leftSide, CSharpShiftOperation operation, ICSharpAddSubtExpression rightSide)
            : base(leftSide, rightSide)
        {
            this.operation = operation;
        }

        /// <summary>
        /// Creates a new <see cref="CSharpShiftExpression"/> with the <paramref name="term"/> provided.
        /// </summary>
        /// <param name="term">The <see cref="ICSharpAddSubtExpression"/> the non-operational
        /// <see cref="CSharpShiftExpression"/> points to.</param>
        public CSharpShiftExpression(ICSharpAddSubtExpression term)
            : base(term)
        {
        }

        /// <summary>
        /// Returns the <see cref="BinaryOperationAssociativity"/> associated to the 
        /// <see cref="CSharpShiftExpression"/>.
        /// </summary>
        /// <remarks>Returns <see cref="BinaryOperationAssociativity.Left"/>.</remarks>
        public override BinaryOperationAssociativity Associativity
        {
            get { return BinaryOperationAssociativity.Left; }
        }

        /// <summary>
        /// Returns the type of expression the <see cref="CSharpShiftExpression"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="ExpressionType.CSharpShiftOperation"/>.</remarks>
        public override ExpressionKind Type
        {
            get {
                switch (this.Operation)
                {
                    case CSharpShiftOperation.Term:
                        return ExpressionKinds.BinaryForwardTerm;
                    case CSharpShiftOperation.LeftShift:
                        return ExpressionKinds.ShiftLeftOperation;
                    case CSharpShiftOperation.RightShift:
                        return ExpressionKinds.ShiftRightOperation;
                    default:
                        break;
                }
                throw new InvalidOperationException();
            }
        }

        public static implicit operator CSharpShiftExpression(CSharpAddSubtExpression term)
        {
            return new CSharpShiftExpression(term);
        }

        #region ICSharpShiftExpression Members

        /// <summary>
        /// Returns/sets the <see cref="CSharpShiftOperation"/> associated to the <see cref="CSharpShiftExpression"/>.
        /// </summary>
        public CSharpShiftOperation Operation
        {
            get
            {
                return this.operation;
            }
            set
            {
                this.operation = value;
            }
        }

        #endregion
        public override string ToString()
        {
            string op = string.Empty;
            switch (this.Operation)
            {
                case CSharpShiftOperation.Term:
                    return this.RightSide.ToString();
                case CSharpShiftOperation.LeftShift:
                    op = "<<";
                    break;
                case CSharpShiftOperation.RightShift:
                    op = ">>";
                    break;
                default:
                    return null;
            }
            return string.Format("{0} {1} {2}", LeftSide.ToString(), op, RightSide.ToString());
        }

        public override void Visit(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
