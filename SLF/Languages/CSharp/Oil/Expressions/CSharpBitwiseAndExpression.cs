﻿using System;
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
    /// Provides a base implementation of <see cref="ICSharpBitwiseAndExpression"/>; 
    /// a logical and binary operation (C&#9839;: '&'; VB: "And"; CIL: "and").
    /// </summary>
    public class CSharpBitwiseAndExpression :
        CSharpBinaryOperationExpressionBase<ICSharpBitwiseAndExpression, ICSharpInequalityExpression>,
        ICSharpBitwiseAndExpression
    {
        /// <summary>
        /// Creates a new non-operational <see cref="CSharpBitwiseAndExpression"/> with the <paramref name="term"/>
        /// provided.
        /// </summary>
        /// <param name="term">The term the non-operational <see cref="CSharpBitwiseAndExpression"/>
        /// points to.</param>
        public CSharpBitwiseAndExpression(ICSharpInequalityExpression term)
            : base(term)
        {

        }

        /// <summary>
        /// Creates a new <see cref="CSharpBitwiseAndExpression"/> with the <paramref name="leftSide"/> and
        /// <paramref name="rightSide"/> provided.
        /// </summary>
        /// <param name="leftSide">The <see cref="ICSharpBitwiseAndExpression"/> to evaluate alongside 
        /// <paramref name="rightSide"/></param>
        /// <param name="rightSide">The <see cref="ICSharpInequalityExpression"/> to evaluate
        /// with <paramref name="leftSide"/>.</param>
        public CSharpBitwiseAndExpression(ICSharpBitwiseAndExpression leftSide, ICSharpInequalityExpression rightSide)
            : base(leftSide, rightSide)
        {

        }

        /// <summary>
        /// Returns the type of expression the <see cref="CSharpBitwiseAndExpression"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="ExpressionType.BitwiseAndOperation"/>.</remarks>
        public override ExpressionKind Type
        {
            get { return ExpressionKinds.BitwiseAndOperation; }
        }

        /// <summary>
        /// Returns the <see cref="BinaryOperationAssociativity"/> associated to the 
        /// <see cref="CSharpBitwiseAndExpression"/>.
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
                return string.Format("{0} {1} {2}", this.LeftSide.ToString(), "&", this.RightSide.ToString());
        }

        public override void Visit(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}