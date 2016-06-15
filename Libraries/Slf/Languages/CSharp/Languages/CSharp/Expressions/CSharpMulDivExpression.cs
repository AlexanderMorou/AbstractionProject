using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
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
    /// Provides a base implementation of <see cref="ICSharpMulDivExpression"/>;
    /// a multiplication, division or division remainder operation expression.
    /// </summary>
    public sealed class CSharpMulDivExpression :
        CSharpBinaryOperationExpressionBase<ICSharpMulDivExpression, IUnaryOperationExpression>,
        ICSharpMulDivExpression
    {
        /// <summary>
        /// Data member for <see cref="Operation"/>.
        /// </summary>
        private CSharpMulDivOperation operation;
        /// <summary>
        /// Returns the type of expression the <see cref="CSharpMulDivExpression"/> is.
        /// </summary>
        /// <remarks>Returns the appropriate <see cref="ExpressionKind"/> value
        /// relative to the <see cref="CSharpMulDivOperation"/>.</remarks>
        public override ExpressionKind Type
        {
            get
            {
                switch (this.Operation)
                {
                    case CSharpMulDivOperation.Multiplication:
                        return ExpressionKind.MultiplyOperation;
                    case CSharpMulDivOperation.Division:
                        return ExpressionKind.StrictDivisionOperation;
                    case CSharpMulDivOperation.Remainder:
                        return ExpressionKind.ModulusOperation;
                    case CSharpMulDivOperation.Term:
                        return ExpressionKind.BinaryForwardTerm;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        public CSharpMulDivExpression(ICSharpMulDivExpression leftSide, CSharpMulDivOperation operation, IUnaryOperationExpression rightSide)
            : base(leftSide, rightSide)
        {
            this.operation = operation;
        }

        public CSharpMulDivExpression(IUnaryOperationExpression term)
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
        /// Returns/sets the <see cref="CSharpMulDivOperation"/> represented by the <see cref="CSharpMulDivExpression"/>.
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
        public static implicit operator CSharpMulDivExpression(UnaryOperationExpression term)
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
                    case CSharpMulDivOperation.Multiplication:
                        return BinaryOperationKind.Multiply;
                    case CSharpMulDivOperation.Division:
                        return BinaryOperationKind.IntegerDivision;
                    case CSharpMulDivOperation.Remainder:
                        return BinaryOperationKind.Modulus;
                    case CSharpMulDivOperation.Term:
                    default:
                        return BinaryOperationKind.Term;
                }
            }
        }
    }
}
