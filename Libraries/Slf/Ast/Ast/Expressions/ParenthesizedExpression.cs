﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Provides a base implementation for <see cref="IParenthesizedExpression"/>.
    /// </summary>
    public class ParenthesizedExpression :
        ExpressionBase,
        IParenthesizedExpression
    {
        IExpression parenthesized;

        public ParenthesizedExpression(IExpression parenthesized)
        {
            if (parenthesized == null)
                throw new ArgumentNullException("parenthesized");
            this.parenthesized = parenthesized;
        }

        /// <summary>
        /// Returns the type of expression the <see cref="ParenthesizedExpression"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="ExpressionKind.ParenthesizedExpression"/>.</remarks>
        public override ExpressionKind Type
        {
            get { return ExpressionKind.ParenthesizedExpression; }
        }

        #region IParenthesizedExpression Members

        /// <summary>
        /// Returns/sets the expression that's surrounded by parenthesis.
        /// </summary>
        public IExpression Parenthesized
        {
            get { return this.parenthesized; }
            set { this.parenthesized = value; }
        }

        #endregion
        public override string ToString()
        {
            return string.Format("({0})", this.Parenthesized.ToString());
        }
        /*
        public override IType ForwardType
        {
            get {
                if (this.parenthesized is IMemberParentReferenceExpression)
                {
                    return ((IMemberParentReferenceExpression)(this.Parametered)).ForwardType;
                }
                else
                    return typeof(void).GetTypeReference();
            }
        }

        protected override void OnLink()
        {
            if (this.parenthesized is ILinkableExpression)
                ((ILinkableExpression)(this.Parametered)).Link();
        }
         */


        public override TResult Accept<TResult, TContext>(ICommonExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
