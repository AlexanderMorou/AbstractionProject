using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq
{
    /// <summary>
    /// Defines generic properties and methods for visiting a series of
    /// Language Integrated Query expressions.
    /// </summary>
    public interface ILinqVisitor
    {
        /// <summary>
        /// Visits the <see cref="ILinqSelectBody"/> 
        /// <paramref name="expression"/>.
        /// </summary>
        /// <param name="expression">The <see cref="ILinqSelectBody"/>
        /// to visit.</param>
        void Visit(ILinqSelectBody expression);
        /// <summary>
        /// Visits the <see cref="ILinqGroupBody"/> 
        /// <paramref name="expression"/>.
        /// </summary>
        /// <param name="expression">The <see cref="ILinqGroupBody"/>
        /// to visit.</param>
        void Visit(ILinqGroupBody expression);
        /// <summary>
        /// Visits the <see cref="ILinqFusionSelectBody"/> 
        /// <paramref name="expression"/>.
        /// </summary>
        /// <param name="expression">The <see cref="ILinqFusionSelectBody"/>
        /// to visit.</param>
        void Visit(ILinqFusionSelectBody expression);
        /// <summary>
        /// Visits the <see cref="ILinqFusionGroupBody"/> 
        /// <paramref name="expression"/>.
        /// </summary>
        /// <param name="expression">The <see cref="ILinqFusionGroupBody"/>
        /// to visit.</param>
        void Visit(ILinqFusionGroupBody expression);
        /// <summary>
        /// Visits the <see cref="ILinqFromClause"/>.
        /// </summary>
        /// <param name="linqClause">The <see cref="ILinqFromClause"/>
        /// to visit.</param>
        void Visit(ILinqFromClause linqClause);
        /// <summary>
        /// Visits the <see cref="ILinqJoinClause"/>.
        /// </summary>
        /// <param name="linqClause">The <see cref="ILinqJoinClause"/>
        /// to visit.</param>
        void Visit(ILinqJoinClause linqClause);
        /// <summary>
        /// Visits the <see cref="ILinqLetClause"/>.
        /// </summary>
        /// <param name="linqClause">The <see cref="ILinqLetClause"/>
        /// to visit.</param>
        void Visit(ILinqLetClause linqClause);
        /// <summary>
        /// Visits the <see cref="ILinqOrderByClause"/>.
        /// </summary>
        /// <param name="linqClause">The <see cref="ILinqOrderByClause"/>
        /// to visit.</param>
        void Visit(ILinqOrderByClause linqClause);
        /// <summary>
        /// Visits the <see cref="ILinqTypedFromClause"/>.
        /// </summary>
        /// <param name="linqClause">The <see cref="ILinqTypedFromClause"/>
        /// to visit.</param>
        void Visit(ILinqTypedFromClause linqClause);
        /// <summary>
        /// Visits the <see cref="ILinqTypedJoinClause"/>.
        /// </summary>
        /// <param name="linqClause">The <see cref="ILinqTypedJoinClause"/>
        /// to visit.</param>
        void Visit(ILinqTypedJoinClause linqClause);
        /// <summary>
        /// Visits the <see cref="ILinqWhereClause"/>.
        /// </summary>
        /// <param name="linqClause">The <see cref="ILinqWhereClause"/>
        /// to visit.</param>
        void Visit(ILinqWhereClause linqClause);
    }
    /// <summary>
    /// Defines generic properties and methods for visiting a series of
    /// Language Integrated Query expressions.
    /// </summary>
    /// <typeparam name="TResult">The value returned upon visiting the expression.</typeparam>
    /// <typeparam name="TContext">The type of context passed to the visitor.</typeparam>
    public interface ILinqVisitor<TResult, TContext>
    {
        /// <summary>
        /// Visits the <see cref="ILinqSelectBody"/> 
        /// <paramref name="expression"/>.
        /// </summary>
        /// <param name="expression">The <see cref="ILinqSelectBody"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ILinqSelectBody expression, TContext context);
        /// <summary>
        /// Visits the <see cref="ILinqGroupBody"/> 
        /// <paramref name="expression"/>.
        /// </summary>
        /// <param name="expression">The <see cref="ILinqGroupBody"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ILinqGroupBody expression, TContext context);
        /// <summary>
        /// Visits the <see cref="ILinqFusionSelectBody"/> 
        /// <paramref name="expression"/>.
        /// </summary>
        /// <param name="expression">The <see cref="ILinqFusionSelectBody"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ILinqFusionSelectBody expression, TContext context);
        /// <summary>
        /// Visits the <see cref="ILinqFusionGroupBody"/> 
        /// <paramref name="expression"/>.
        /// </summary>
        /// <param name="expression">The <see cref="ILinqFusionGroupBody"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ILinqFusionGroupBody expression, TContext context);
        /// <summary>
        /// Visits the <see cref="ILinqFromClause"/>.
        /// </summary>
        /// <param name="linqClause">The <see cref="ILinqFromClause"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ILinqFromClause linqClause, TContext context);
        /// <summary>
        /// Visits the <see cref="ILinqJoinClause"/>.
        /// </summary>
        /// <param name="linqClause">The <see cref="ILinqJoinClause"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ILinqJoinClause linqClause, TContext context);
        /// <summary>
        /// Visits the <see cref="ILinqLetClause"/>.
        /// </summary>
        /// <param name="linqClause">The <see cref="ILinqLetClause"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ILinqLetClause linqClause, TContext context);
        /// <summary>
        /// Visits the <see cref="ILinqOrderByClause"/>.
        /// </summary>
        /// <param name="linqClause">The <see cref="ILinqOrderByClause"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ILinqOrderByClause linqClause, TContext context);
        /// <summary>
        /// Visits the <see cref="ILinqTypedFromClause"/>.
        /// </summary>
        /// <param name="linqClause">The <see cref="ILinqTypedFromClause"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ILinqTypedFromClause linqClause, TContext context);
        /// <summary>
        /// Visits the <see cref="ILinqTypedJoinClause"/>.
        /// </summary>
        /// <param name="linqClause">The <see cref="ILinqTypedJoinClause"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ILinqTypedJoinClause linqClause, TContext context);
        /// <summary>
        /// Visits the <see cref="ILinqWhereClause"/>.
        /// </summary>
        /// <param name="linqClause">The <see cref="ILinqWhereClause"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ILinqWhereClause linqClause, TContext context);
    }
}
