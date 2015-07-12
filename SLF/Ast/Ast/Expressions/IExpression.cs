using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with an expression.
    /// </summary>
    public interface IExpression :
        ISourceElement
    {
        /// <summary>
        /// Returns the type of expression the <see cref="IExpression"/> is.
        /// </summary>
        ExpressionKind Type { get; }
        /// <summary>
        /// Visits the elements of the <see cref="IExpression"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateCodeVisitor"/>
        /// to which the <see cref="IExpression"/> needs to repay the visit
        /// to.</param>
        void Visit(IExpressionVisitor visitor);

        /// <summary>
        /// Visits the elements of the <see cref="IExpression"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IExpressionVisitor{TResult, TContext}"/>
        /// to which the <see cref="IExpression"/> needs to repay the visit
        /// to.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <typeparam name="TResult">The type returned by the visit.</typeparam>
        /// <typeparam name="TContext">The type of context passed to the visitor.</typeparam>
        /// <returns>The <typeparamref name="TResult"/> of the 
        /// current implementation.</returns>
        TResult Visit<TContext, TResult>(IExpressionVisitor<TResult, TContext> visitor, TContext context);
    }
}
