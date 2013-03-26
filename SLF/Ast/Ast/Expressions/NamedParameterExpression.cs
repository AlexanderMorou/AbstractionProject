using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Provides a base implementation of a <see cref="INamedParameterExpression"/> 
    /// for working with a named parameter expression.
    /// </summary>
    public class NamedParameterExpression :
        INamedParameterExpression
    {
        /// <summary>
        /// Creates a new <see cref="NamedParameterExpression"/> with the
        /// <paramref name="name"/> and <paramref name="expression"/> provided.
        /// </summary>
        /// <param name="name">The name of the parameter the <see cref="NamedParameterExpression"/>
        /// refers to.</param>
        /// <param name="expression">The name of the <see cref="IExpression"/> the
        /// <see cref="NamedParameterExpression"/> refers to.</param>
        public NamedParameterExpression(string name, IExpression expression)
        {
            this.Name = name;
            this.Expression = expression;
        }

        #region IExpression Members

        /// <summary>
        /// Returns the type of expression the <see cref="NamedParameterExpression"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="ExpressionKind.NamedParameterReference"/>.</remarks>
        public ExpressionKind Type
        {
            get { return ExpressionKind.NamedParameterReference; }
        }

        #endregion

        #region INamedParameterExpression Members

        /// <summary>
        /// Returns/sets the name of the parameter the <see cref="NamedParameterExpression"/>
        /// refers to.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Returns/sets the name of the <see cref="IExpression"/> the
        /// <see cref="NamedParameterExpression"/> refers to.
        /// </summary>
        public IExpression Expression { get; set; }

        #endregion
        /// <summary>
        /// Returns a <see cref="String"/> that represents the current <see cref="NamedParameterExpression"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> that represents the current <see cref="NamedParameterExpression"/>.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{0}: {1}", this.Name, this.Expression);
        }

        #region IExpression Members

        public void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public TResult Visit<TResult>(IExpressionVisitor<TResult> visitor)
        {
            return visitor.Visit(this);
        }

        #endregion

        #region ISourceElement Members

        public string FileName { get; set; }

        public LineColumnPair? Start { get; set; }

        public LineColumnPair? End { get; set; }

        #endregion
    }
}
