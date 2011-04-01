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
    /// Provides a base implementation of <see cref="ICSharpBitwiseOrExpression"/>; 
    /// a logical and binary operation ('|'; "Or", "or").
    /// </summary>
    public class CSharpBitwiseOrExpression :
        CSharpBinaryOperationExpressionBase<ICSharpBitwiseOrExpression, ICSharpBitwiseExclusiveOrExpression>,
        ICSharpBitwiseOrExpression
    {
        /// <summary>
        /// Creates a new non-operational <see cref="CSharpBitwiseOrExpression"/> with the <paramref name="term"/>
        /// provided.
        /// </summary>
        /// <param name="term">The term the non-operational <see cref="CSharpBitwiseOrExpression"/>
        /// points to.</param>
        public CSharpBitwiseOrExpression(ICSharpBitwiseExclusiveOrExpression term)
            : base(term)
        {

        }

        /// <summary>
        /// Creates a new <see cref="CSharpBitwiseOrExpression"/> with the <paramref name="leftSide"/> and
        /// <paramref name="rightSide"/> provided.
        /// </summary>
        /// <param name="leftSide">The <see cref="ICSharpBitwiseOrExpression"/> to evaluate alongside 
        /// <paramref name="rightSide"/></param>
        /// <param name="rightSide">The <see cref="ICSharpBitwiseExclusiveOrExpression"/> to evaluate
        /// with <paramref name="leftSide"/>.</param>
        public CSharpBitwiseOrExpression(ICSharpBitwiseOrExpression leftSide, ICSharpBitwiseExclusiveOrExpression rightSide)
            : base(leftSide, rightSide)
        {

        }

        /// <summary>
        /// Returns the type of expression the <see cref="CSharpBitwiseOrExpression"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="ExpressionKinds.BitwiseOrOperation"/>.</remarks>
        public override ExpressionKinds Type
        {
            get { return ExpressionKinds.BitwiseOrOperation; }
        }

        /// <summary>
        /// Returns the <see cref="BinaryOperationAssociativity"/> associated to the 
        /// <see cref="CSharpBitwiseOrExpression"/>.
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
                return string.Format("{0} {1} {2}", this.LeftSide.ToString(), "|", this.RightSide.ToString());
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
