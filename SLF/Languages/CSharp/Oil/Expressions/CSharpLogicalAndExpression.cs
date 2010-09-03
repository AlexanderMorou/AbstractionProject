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
    /// Provides a base implementation of <see cref="ICSharpConditionalAndExpression"/>; 
    /// a logical and binary operation (C&#9839;: "&amp;&amp;"; VB: "AndAlso"; CIL: "brfalse[.s]" (in case of false, short circuit)).
    /// </summary>
    public sealed class CSharpLogicalAndExpression :
        CSharpBinaryOperationExpressionBase<ICSharpLogicalAndExpression, ICSharpBitwiseOrExpression>,
        ICSharpLogicalAndExpression
    {
        /// <summary>
        /// Creates a new non-operational <see cref="CSharpConditionalAndExpression"/> with the <paramref name="term"/>
        /// provided.
        /// </summary>
        /// <param name="term">The term the non-operational <see cref="CSharpConditionalAndExpression"/>
        /// points to.</param>
        public CSharpLogicalAndExpression(ICSharpBitwiseOrExpression term)
            : base(term)
        {

        }

        /// <summary>
        /// Creates a new <see cref="CSharpConditionalAndExpression"/> with the <paramref name="leftSide"/> and
        /// <paramref name="rightSide"/> provided.
        /// </summary>
        /// <param name="leftSide">The <see cref="ICSharpConditionalAndExpression"/> to evaluate alongside 
        /// <paramref name="rightSide"/></param>
        /// <param name="rightSide">The <see cref="ICSharpBitwiseOrExpression"/> to evaluate
        /// with <paramref name="leftSide"/>.</param>
        public CSharpLogicalAndExpression(ICSharpLogicalAndExpression leftSide, ICSharpBitwiseOrExpression rightSide)
            : base(leftSide, rightSide)
        {

        }

        /// <summary>
        /// Returns the type of expression the <see cref="CSharpConditionalAndExpression"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="ExpressionType.BitwiseAndOperation"/>.</remarks>
        public override ExpressionKind Type
        {
            get { return ExpressionKinds.LogicalAndOperation; }
        }

        /// <summary>
        /// Returns the <see cref="BinaryOperationAssociativity"/> associated to the 
        /// <see cref="CSharpConditionalAndExpression"/>.
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
                return string.Format("{0} {1} {2}", this.LeftSide.ToString(), "&&", this.RightSide.ToString());
        }


        public override void Visit(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
