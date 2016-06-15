using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq
{
    /// <summary>
    /// Provides a base class for a language integrated query clause.
    /// </summary>
    public abstract class LinqClauseBase :
        ILinqClause
    {
        #region ILinqClause Members

        /// <summary>
        /// Returns the kind of clause the <see cref="ILinqClause"/> is.
        /// </summary>
        public abstract ClauseType Type { get; }

        /// <summary>
        /// Visits the elements of the <see cref="LinqClauseBase"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateTreeVisitor"/>
        /// to which the <see cref="LinqClauseBase"/> needs to repay the visit
        /// to.</param>
        public abstract void Accept(ILinqClauseVisitor visitor);

        public abstract TResult Accept<TResult, TContext>(ILinqClauseVisitor<TResult, TContext> visitor, TContext context);
        #endregion


    }
}
