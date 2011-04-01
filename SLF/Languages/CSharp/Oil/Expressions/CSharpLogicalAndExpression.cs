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
    /// Provides a base implementation of <see cref="ICSharpLogicalAndExpression"/>; 
    /// a logical and binary operation (C&#9839;: "&amp;&amp;"; VB: "AndAlso"; CIL: "brfalse[.s]" (in case of false, short circuit)).
    /// </summary>
    public sealed class CSharpLogicalAndExpression :
        CSharpBinaryOperationExpressionBase<ICSharpLogicalAndExpression, ICSharpBitwiseOrExpression>,
        ICSharpLogicalAndExpression
    {
        /// <summary>
        /// Creates a new non-operational <see cref="CSharpLogicalAndExpression"/> with the <paramref name="term"/>
        /// provided.
        /// </summary>
        /// <param name="term">The <see cref="ICSharpBitwiseOrExpression"/> term the non-operational
        /// <see cref="CSharpLogicalAndExpression"/>
        /// points to.</param>
        public CSharpLogicalAndExpression(ICSharpBitwiseOrExpression term)
            : base(term)
        {

        }

        /// <summary>
        /// Creates a new <see cref="CSharpLogicalAndExpression"/> with the <paramref name="leftSide"/> and
        /// <paramref name="rightSide"/> provided.
        /// </summary>
        /// <param name="leftSide">The <see cref="ICSharpLogicalAndExpression"/> to evaluate alongside 
        /// <paramref name="rightSide"/></param>
        /// <param name="rightSide">The <see cref="ICSharpBitwiseOrExpression"/> to evaluate
        /// with <paramref name="leftSide"/>.</param>
        public CSharpLogicalAndExpression(ICSharpLogicalAndExpression leftSide, ICSharpBitwiseOrExpression rightSide)
            : base(leftSide, rightSide)
        {

        }

        /// <summary>
        /// Returns the type of expression the <see cref="CSharpLogicalAndExpression"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="ExpressionKinds.BitwiseAndOperation"/>.</remarks>
        public override ExpressionKinds Type
        {
            get { return ExpressionKinds.LogicalAndOperation; }
        }

        /// <summary>
        /// Returns the <see cref="BinaryOperationAssociativity"/> associated to the 
        /// <see cref="CSharpLogicalAndExpression"/>.
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


        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
