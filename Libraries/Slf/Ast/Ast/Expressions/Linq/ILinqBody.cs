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
    /// Defines properties and methods for working with a language integrated 
    /// query expression body.
    /// </summary>
    public interface ILinqBody
    {
        /// <summary>
        /// Returns the <see cref="ILinqClauseCollection"/>
        /// associated to the <see cref="ILinqBody"/>.
        /// </summary>
        ILinqClauseCollection Clauses { get; }
        /// <summary>
        /// Visits the elements of the <see cref="ILinqBody"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateTreeVisitor"/>
        /// to which the <see cref="ILinqBody"/> needs to repay the visit
        /// to.</param>
        void Accept(ILinqBodyVisitor visitor);
        TResult Accept<TResult, TContext>(ILinqBodyVisitor<TResult, TContext> visitor, TContext context);
    }
}
