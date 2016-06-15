using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions
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
        /// <param name="operation">The <see cref="CSharpShiftOperation"/> which denotes
        /// the kind of shift action to take between the <paramref name="leftSide"/>
        /// and <paramref name="rightSide"/>.</param>
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
        /// <remarks>Returns <see cref="ExpressionKind.BinaryForwardTerm"/> when <see cref="Operation"/>
        /// is <see cref="CSharpShiftOperation.Term"/>,
        /// <see cref="ExpressionKind.ShiftLeftOperation"/>
        /// when <see cref="Operation"/> is <see cref="CSharpShiftOperation.LeftShift"/>
        /// and <see cref="ExpressionKind.ShiftRightOperation"/> when <see cref="Operation"/>
        /// is <see cref="CSharpShiftOperation.RightShift"/>.</remarks>
        public override ExpressionKind Type
        {
            get {
                switch (this.Operation)
                {
                    case CSharpShiftOperation.Term:
                        return ExpressionKind.BinaryForwardTerm;
                    case CSharpShiftOperation.LeftShift:
                        return ExpressionKind.ShiftLeftOperation;
                    case CSharpShiftOperation.RightShift:
                        return ExpressionKind.ShiftRightOperation;
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

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }


        public override BinaryOperationKind OperationKind
        {
            get
            {
                switch (this.Operation)
                {
                    case CSharpShiftOperation.LeftShift:
                        return BinaryOperationKind.LeftShift;
                    case CSharpShiftOperation.RightShift:
                        return BinaryOperationKind.RightShift;
                    case CSharpShiftOperation.Term:
                    default:
                        return BinaryOperationKind.Term;
                }
            }
        }
    }
}
