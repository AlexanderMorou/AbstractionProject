using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Provides a base implementation of <see cref="ICSharpMulDivExpression"/>;
    /// a multiplication, division or division remainder operation expression.
    /// </summary>
    public sealed class CSharpMulDivExpression :
        CSharpBinaryOperationExpressionBase<ICSharpMulDivExpression, ICSharpUnaryOperationExpression>,
        ICSharpMulDivExpression
    {
        /// <summary>
        /// Data member for <see cref="Operation"/>.
        /// </summary>
        private CSharpMulDivOperation operation;
        /// <summary>
        /// Returns the type of expression the <see cref="CSharpMulDivExpression"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="ExpressionType.MulDivOperation"/>.</remarks>
        public override ExpressionKind Type
        {
            get
            {
                switch (this.Operation)
                {
                    case CSharpMulDivOperation.Multiplication:
                        return ExpressionKinds.MultiplyOperation;
                    case CSharpMulDivOperation.Division:
                        return ExpressionKinds.StrictDivisionOperation;
                    case CSharpMulDivOperation.Remainder:
                        return ExpressionKinds.ModulusOperation;
                    case CSharpMulDivOperation.Term:
                        return ExpressionKinds.BinaryForwardTerm;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        public CSharpMulDivExpression(ICSharpMulDivExpression leftSide, CSharpMulDivOperation operation, ICSharpUnaryOperationExpression rightSide)
            : base(leftSide, rightSide)
        {
            this.operation = operation;
        }

        public CSharpMulDivExpression(ICSharpUnaryOperationExpression term)
            : base(term)
        {
            this.operation = CSharpMulDivOperation.Term;
        }

        /// <summary>
        /// Returns the <see cref="BinaryOperationAssociativity"/> associated to the 
        /// <see cref="CSharpMulDivExpression"/>.
        /// </summary>
        /// <remarks>Returns <see cref="BinaryOperationAssociativity.Left"/>.</remarks>
        public override BinaryOperationAssociativity Associativity
        {
            get { return BinaryOperationAssociativity.Left; }
        }


        #region ICSharpMulDivExpression Members

        /// <summary>
        /// Returns/sets the <see cref="MulDivOperation"/> represented by the <see cref="CSharpMulDivExpression"/>.
        /// </summary>
        public CSharpMulDivOperation Operation
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
        public static implicit operator CSharpMulDivExpression(CSharpUnaryOperationExpression term)
        {
            return new CSharpMulDivExpression(term);
        }

        public override string ToString()
        {
            char op = char.MinValue;
            switch (this.operation)
            {
                case CSharpMulDivOperation.Multiplication:
                    op = '*';
                    break;
                case CSharpMulDivOperation.Division:
                    op = '/';
                    break;
                case CSharpMulDivOperation.Remainder:
                    op = '%';
                    break;
                case CSharpMulDivOperation.Term:
                    return this.RightSide.ToString();
                default:
                    return null;
            }
            return string.Format("{0} {1} {2}", LeftSide.ToString(), op, RightSide.ToString());
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
