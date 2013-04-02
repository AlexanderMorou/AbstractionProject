using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions
{
    /// <summary>
    /// Provides a base implementation of <see cref="ICSharpLogicalOrExpression"/>; 
    /// a logical and binary operation ('|'; "Or", "or").
    /// </summary>
    public sealed class CSharpLogicalOrExpression :
        CSharpBinaryOperationExpressionBase<ICSharpLogicalOrExpression, ICSharpLogicalAndExpression>,
        ICSharpLogicalOrExpression
    {
        /// <summary>
        /// Creates a new non-operational <see cref="CSharpLogicalOrExpression"/> with the <paramref name="term"/>
        /// provided.
        /// </summary>
        /// <param name="term">The term the non-operational <see cref="ICSharpLogicalAndExpression"/>
        /// points to.</param>
        public CSharpLogicalOrExpression(ICSharpLogicalAndExpression term)
            : base(term)
        {

        }

        /// <summary>
        /// Creates a new <see cref="CSharpLogicalOrExpression"/> with the <paramref name="leftSide"/> and
        /// <paramref name="rightSide"/> provided.
        /// </summary>
        /// <param name="leftSide">The <see cref="ICSharpLogicalOrExpression"/> to evaluate alongside 
        /// <paramref name="rightSide"/></param>
        /// <param name="rightSide">The <see cref="ICSharpLogicalAndExpression"/> to evaluate
        /// when <paramref name="leftSide"/> is false.</param>
        public CSharpLogicalOrExpression(ICSharpLogicalOrExpression leftSide, ICSharpLogicalAndExpression rightSide)
            : base(leftSide, rightSide)
        {

        }

        /// <summary>
        /// Returns the type of expression the <see cref="CSharpLogicalOrExpression"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="ExpressionKind.LogicalOrOperation"/>.</remarks>
        public override ExpressionKind Type
        {
            get { return ExpressionKind.LogicalOrOperation; }
        }

        /// <summary>
        /// Returns the <see cref="BinaryOperationAssociativity"/> associated to the 
        /// <see cref="CSharpLogicalOrExpression"/>.
        /// </summary>
        /// <remarks>Returns <see cref="BinaryOperationAssociativity.Left"/>.</remarks>
        public override BinaryOperationAssociativity Associativity
        {
            get { return BinaryOperationAssociativity.Left; }
        }
        public override string ToString()
        {
            if (this.LeftSide == null)
                return this.RightSide.ToString();
            else
                return string.Format("{0} {1} {2}", this.LeftSide.ToString(), "||", this.RightSide.ToString());
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }


        public override BinaryOperationKind OperationKind
        {
            get { return BinaryOperationKind.LogicalOr; }
        }
    }
}
