using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Provides a base implementation of <see cref="ICSharpAddSubtExpression"/>
    /// which represents an add or subtract operation.
    /// </summary>
    public sealed class CSharpAddSubtExpression :
        CSharpBinaryOperationExpressionBase<ICSharpAddSubtExpression, ICSharpMulDivExpression>,
        ICSharpAddSubtExpression
    {
        /// <summary>
        /// Data member for <see cref="Operation"/>.
        /// </summary>
        private CSharpAddSubtOperation operation;

        /// <summary>
        /// Creates a new <see cref="CSharpAddSubtExpression"/> instance
        /// with the <paramref name="leftSide"/>, <paramref name="operation"/> and 
        /// <paramref name="rightSide"/> provided.
        /// </summary>
        /// <param name="leftSide">The <see cref="ICSharpAddSubtExpression"/> which makes up the
        /// left operand.</param>
        /// <param name="operation">The <see cref="CSharpAddSubtOperation"/> that is
        /// represented by the <see cref="CSharpAddSubtExpression"/>.</param>
        /// <param name="rightSide">The <see cref="ICSharpMulDivExpression"/> which makes up the
        /// right operand.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when <paramref name="operation"/> is
        /// <see cref="CSharpAddSubtOperation.Term"/> or is not one of the other elements in the
        /// <see cref="CSharpAddSubtOperation"/> enumeration.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="leftSide"/>
        /// is null; or when <paramref name="rightSide"/> is null.</exception>
        public CSharpAddSubtExpression(ICSharpAddSubtExpression leftSide, CSharpAddSubtOperation operation, ICSharpMulDivExpression rightSide) 
            : base(leftSide, rightSide)
        {
            if (operation == CSharpAddSubtOperation.Term || 
                operation != CSharpAddSubtOperation.Addition &&
                operation != CSharpAddSubtOperation.Subtraction)
                throw new ArgumentOutOfRangeException("operation");
            this.operation = operation;
        }

        /// <summary>
        /// Creates a new <see cref="CSharpAddSubtExpression"/> with the <paramref name="term"/> which will
        /// be pointed to by the new instance.
        /// </summary>
        /// <param name="term">The <see cref="ICSharpMulDivExpression"/> pointed to by the <see cref="CSharpAddSubtExpression"/>.</param>
        public CSharpAddSubtExpression(ICSharpMulDivExpression term)
            : base(term)
        {
            this.operation = CSharpAddSubtOperation.Term;
        }

        /// <summary>
        /// Returns the type of expression the <see cref="CSharpAddSubtExpression"/> is.
        /// </summary>
        /// <remarks>Returns the appropriate <see cref="ExpressionKind"/> value
        /// relative to the <see cref="CSharpAddSubtOperation"/>
        /// of the <see cref="Operation"/>.</remarks>
        public override ExpressionKind Type
        {
            get {
                switch (this.Operation)
                {
                    case CSharpAddSubtOperation.Term:
                        return ExpressionKind.BinaryForwardTerm;
                    case CSharpAddSubtOperation.Addition:
                        return ExpressionKind.AddOperation;
                    case CSharpAddSubtOperation.Subtraction:
                        return ExpressionKind.SubtractOperation;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        /// <summary>
        /// Returns the <see cref="BinaryOperationAssociativity"/> associated to the 
        /// <see cref="CSharpAddSubtExpression"/>.
        /// </summary>
        /// <remarks>Returns <see cref="BinaryOperationAssociativity.Left"/>.</remarks>
        public override BinaryOperationAssociativity Associativity
        {
            get { return BinaryOperationAssociativity.Left; }
        }


        #region ICSharpAddSubtExpression Members

        /// <summary>
        /// Returns/sets the <see cref="CSharpAddSubtOperation"/> the <see cref="CSharpAddSubtExpression"/>
        /// represents.
        /// </summary>
        public CSharpAddSubtOperation Operation
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
            char op = char.MinValue;
            switch (this.Operation)
            {
                case CSharpAddSubtOperation.Term:
                    return this.RightSide.ToString();
                case CSharpAddSubtOperation.Addition:
                    op = '+';
                    break;
                case CSharpAddSubtOperation.Subtraction:
                    op = '-';
                    break;
                default:
                    return null;
            }
            return string.Format("{0} {1} {2}", LeftSide.ToString(), op, RightSide.ToString());
        }


        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override BinaryOperationKind OperationKind
        {
            get {
                switch (this.Operation)
                {
                    case CSharpAddSubtOperation.Addition:
                        return BinaryOperationKind.Add;
                    case CSharpAddSubtOperation.Subtraction:
                        return BinaryOperationKind.Subtract;
                    case CSharpAddSubtOperation.Term:
                    default:
                        return BinaryOperationKind.Term;
                }
            }
        }
    }
}
