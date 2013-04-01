using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq
{
    /// <summary>
    /// Provides a base implementation of a language integrated query
    /// expression.
    /// </summary>
    public class LinqExpression :
        ILinqExpression
    {
        #region ILinqExpression Members

        /// <summary>
        /// Returns the <see cref="ILinqFromClause"/> which
        /// specifies the first part of the linq expression.
        /// </summary>
        public ILinqFromClause From { get; set; }

        /// <summary>
        /// Returns/sets the <see cref="ILinqBody"/> which denotes
        /// the clauses and potential further fusion bodies
        /// associated to the <see cref="ILinqExpression"/>.
        /// </summary>
        public ILinqBody Body { get; set; }

        #endregion

        #region IExpression Members

        /// <summary>
        /// Returns the type of expression the <see cref="LinqExpression"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="ExpressionKind.LinqExpression"/>.</remarks>
        public ExpressionKind Type
        {
            get { return ExpressionKind.LinqExpression; }
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="String"/> representing the current
        /// <see cref="LinqExpression"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> representing the current
        /// <see cref="LinqExpression"/>.</returns>
        public override string ToString()
        {
            return string.Format("{0} \r\n{1}", From.ToString(), Body.ToString());
        }

        #region IExpression Members

        public void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public TResult Visit<TContext, TResult>(IExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        #endregion

        #region ISourceElement Members

        public string FileName { get; set; }

        public LineColumnPair? Start { get; set; }

        public LineColumnPair? End { get; set; }

        #endregion
    }
}
