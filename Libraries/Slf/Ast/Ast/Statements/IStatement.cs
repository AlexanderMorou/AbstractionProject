using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    /// <summary>
    /// A label to classify all statements under a common point.
    /// </summary>
    public interface IStatement
    {
        /// <summary>
        /// Returns the <see cref="IStatementParent"/> in which the current <see cref="IStatement"/>
        /// was declared.
        /// </summary>
#if DEBUG
        [VisitorImplementationIgnoreProperty]
#endif
        IStatementParent Parent { get; }
        /// <summary>
        /// Visits the <paramref name="visitor"/> based upon the type of the
        /// <see cref="IStatement"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IStatementVisitor"/> 
        /// to visit.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="visitor"/>
        /// is null.</exception>
        void Accept(IStatementVisitor visitor);
        /// <summary>
        /// Visits the <paramref name="visitor"/> based upon the type of the
        /// <see cref="IStatement"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IStatementVisitor{TResult, TContext}"/> 
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/>
        /// which is associated to the visitor during the trip.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="visitor"/>
        /// is null.</exception>
        /// <typeparam name="TContext">The type of context associated to, and used by, the visitor.</typeparam>
        /// <typeparam name="TResult">The type of result associated to the visitor.</typeparam>
        TResult Accept<TResult, TContext>(IStatementVisitor<TResult, TContext> visitor, TContext context);
        /// <summary>
        /// Returns the <see cref="Int32"/> value which denotes
        /// where within the <see cref="Parent"/> the
        /// <see cref="IStatement"/> is.
        /// </summary>
        int Index { get; }
    }
}
