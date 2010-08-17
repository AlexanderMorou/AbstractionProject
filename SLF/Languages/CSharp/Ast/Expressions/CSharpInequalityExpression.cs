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
    /// Provides a root implementation of <see cref="ICSharpInequalityExpression"/>.
    /// </summary>
    public class CSharpInequalityExpression :
        CSharpBinaryOperationExpressionBase<ICSharpInequalityExpression, ICSharpRelationalExpression>,
        ICSharpInequalityExpression
    {
        /// <summary>
        /// Data member for <see cref="Operation"/>.
        /// </summary>
        private CSharpInequalityOperation operation;

        /// <summary>
        /// Creates a new <see cref="CSharpInequalityExpression"/> instance
        /// with the <paramref name="leftSide"/>, <paramref name="equals"/>,
        /// and <paramref name="rightSide"/> provided.
        /// </summary>
        /// <param name="leftSide">The <see cref="ICSharpInequalityExpression"/> which makes up the
        /// left-operand of the <see cref="CSharpInequalityExpression"/>.</param>
        /// <param name="equals">if true <see cref="Operation"/> is <see cref="CSharpInequalityOperation.Equality"/>;
        /// <see cref="CSharpInequalityOperation.Inequality"/> otherwise.</param>
        /// <param name="rightSide">The <see cref="ICSharpRelationalExpression"/> which makes
        /// up the right-operand of the <see cref="CSharpInequalityExpression"/>.</param>
        public CSharpInequalityExpression(ICSharpInequalityExpression leftSide, bool equals, ICSharpRelationalExpression rightSide)
            : base(leftSide, rightSide)
        {
            if (equals)
                this.operation = CSharpInequalityOperation.Equality;
            else
                this.operation = CSharpInequalityOperation.Inequality;
        }

        /// <summary>
        /// Creates a new <see cref="CSharpInequalityExpression"/> instance
        /// with the <paramref name="nonOperationalTerm"/> provided.
        /// </summary>
        /// <param name="nonOperationalTerm">An <see cref="ICSharpRelationalExpression"/> instance
        /// which yields an <see cref="CSharpInequalityExpression"/> which is a forward to a simple term.</param>
        /// <remarks>Used to indicate an inequality expression that is built as a result of a
        /// language parse.</remarks>
        public CSharpInequalityExpression(ICSharpRelationalExpression nonOperationalTerm)
            : base(nonOperationalTerm)
        {
            this.operation = CSharpInequalityOperation.Term;
        }

        /// <summary>
        /// Returns the type of expression the <see cref="CSharpInequalityExpression"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="ExpressionType.CSharpInequalityOperation"/>.</remarks>
        public override ExpressionKind Type
        {
            get {
                switch (this.Operation)
                {
                    case CSharpInequalityOperation.Term:
                        return ExpressionKinds.BinaryForwardTerm;
                    case CSharpInequalityOperation.Equality:
                        return ExpressionKinds.EqualityOperation;
                    case CSharpInequalityOperation.Inequality:
                        return ExpressionKinds.InequalityOperation;
                    default:
                        //Invalid object state.
                        throw new InvalidOperationException();
                }
            }
        }

        /// <summary>
        /// Returns the <see cref="BinaryOperationAssociativity"/> associated to the 
        /// <see cref="CSharpInequalityExpression"/>.
        /// </summary>
        /// <remarks>Returns <see cref="BinaryOperationAssociativity.Left"/>.</remarks>
        public override BinaryOperationAssociativity Associativity
        {
            get { return BinaryOperationAssociativity.Left; }
        }

        #region ICSharpInequalityExpression Members

        /// <summary>
        /// Returns the <see cref="CSharpInequalityOperation"/> defined by the <see cref="CSharpInequalityExpression"/>.
        /// </summary>
        public CSharpInequalityOperation Operation
        {
            get
            {
                return this.operation;
            }
            set
            {
                if (LeftSide == null && value != CSharpInequalityOperation.Term)
                    throw new ArgumentException("Operation must be 'Term' on non-binary expression.");
                else if (LeftSide != null && value == CSharpInequalityOperation.Term)
                    throw new ArgumentException("Binary operations cannot be a term.");
                switch (value)
                {
                    case CSharpInequalityOperation.Term:
                    case CSharpInequalityOperation.Equality:
                    case CSharpInequalityOperation.Inequality:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("value");
                }
                this.operation = value;
            }
        }

        #endregion

        public static CSharpInequalityExpression operator ==(CSharpInequalityExpression leftSide, ICSharpRelationalExpression rightSide)
        {
            return new CSharpInequalityExpression(leftSide, true, rightSide);
        }

        public static CSharpInequalityExpression operator !=(CSharpInequalityExpression leftSide, ICSharpRelationalExpression rightSide)
        {
            return new CSharpInequalityExpression(leftSide, false, rightSide);
        }

        /// <summary>
        /// Implicitly converts the <see cref="CSharpRelationalExpression"/> into a 
        /// non-operational term <see cref="CSharpInequalityExpression"/>.
        /// </summary>
        /// <param name="term">The <see cref="CSharpRelationalExpression"/> that will take place of
        /// <see cref="RightSide"/> as the term.</param>
        /// <returns>A new <see cref="CSharpInequalityExpression"/> with the <paramref name="term"/> as the
        /// <see cref="RightSide"/>.</returns>
        public static implicit operator CSharpInequalityExpression(CSharpRelationalExpression term)
        {
            return new CSharpInequalityExpression(term);
        }

        public override string ToString()
        {
            string op = string.Empty;
            switch (this.operation)
            {
                case CSharpInequalityOperation.Term:
                    return this.RightSide.ToString();
                case CSharpInequalityOperation.Equality:
                    op = "==";
                    break;
                case CSharpInequalityOperation.Inequality:
                    op = "!=";
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
