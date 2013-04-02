using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Provides an expression fusion expression which fuses
    /// a <see cref="IFusionTargetExpression"/> with a 
    /// <see cref="IFusionTermExpression"/>.
    /// </summary>
    public class ExpressionFusionExpression :
        ExpressionBase,
        IExpressionFusionExpression
    {
        public ExpressionFusionExpression(IFusionTargetExpression left, IFusionTermExpression right)
        {
            this.Left = left;
            this.Right = right;
        }

        #region IExpressionFusionExpression Members

        public IFusionTargetExpression Left { get; set; }

        public IFusionTermExpression Right { get; set; }

        #endregion

        public override ExpressionKind Type
        {
            get { return ExpressionKind.ExpressionFusion; }
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}", Left, Right);
        }

        public static ExpressionFusionExpression operator +(ExpressionFusionExpression left, IFusionTermExpression right)
        {
            return (ExpressionFusionExpression)((IFusionTargetExpression)(left)).Fuse(right);
        }

        /// <summary>
        /// Creates a fusionn between the <see cref="ExpressionFusionExpression"/> on the left and the
        /// series of <see cref="IExpression"/> elements on the <paramref name="right"/> with respect
        /// to invocation site and parameter list.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static ExpressionToCommaFusionExpression operator +(ExpressionFusionExpression left, IExpression[] right)
        {
            return new ExpressionToCommaFusionExpression(left, right);
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Visit<TContext, TResult>(IExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

    }
}
