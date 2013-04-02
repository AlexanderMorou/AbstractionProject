using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public interface IIntermediateInclusionVisitor
    {
        /// <summary>
        /// Visits the <paramref name="namedInclusion"/> provided.
        /// </summary>
        /// <param name="namedInclusion">The <see cref="INamedInclusionScopeCoercion"/>
        /// to visit.</param>
        void Visit(INamedInclusionScopeCoercion namedInclusion);
        /// <summary>
        /// Visits the <paramref name="renamedInclusion"/> provided.
        /// </summary>
        /// <param name="renamedInclusion">The <see cref="INamedInclusionRenameScopeCoercion"/>
        /// to visit.</param>
        void Visit(INamedInclusionRenameScopeCoercion renamedInclusion);
        /// <summary>
        /// Visits the <paramref name="namespaceInclusion"/> provided.
        /// </summary>
        /// <param name="namespaceInclusion">The <see cref="INamespaceInclusionScopeCoercion"/>
        /// to visit.</param>
        void Visit(INamespaceInclusionScopeCoercion namespaceInclusion);
        /// <summary>
        /// Visits the <paramref name="renamedNamespaceInclusion"/> to visit.
        /// </summary>
        /// <param name="renamedNamespaceInclusion">The <see cref="INamespaceInclusionRenameScopeCoercion"/>
        /// to visit.</param>
        void Visit(INamespaceInclusionRenameScopeCoercion renamedNamespaceInclusion);
        /// <summary>
        /// Visits the <paramref name="typeInclusion"/> provided.
        /// </summary>
        /// <param name="typeInclusion">The <see cref="ITypeInclusionScopeCoercion"/>
        /// to visit.</param>
        void Visit(ITypeInclusionScopeCoercion typeInclusion);
        /// <summary>
        /// Visits the <paramref name="renamedTypeInclusion"/> provided.
        /// </summary>
        /// <param name="renamedTypeInclusion">The <see cref="ITypeInclusionRenameScopeCoercion"/>
        /// to visit.</param>
        void Visit(ITypeInclusionRenameScopeCoercion renamedTypeInclusion);
        /// <summary>
        /// Visits the <paramref name="staticInclusion"/> provided.
        /// </summary>
        /// <param name="staticInclusion">The <see cref="IStaticInclusionScopeCoercion"/>
        /// to visit.</param>
        void Visit(IStaticInclusionScopeCoercion staticInclusion);
    }
    /// <summary>
    /// Defines generic properties and methods for working with
    /// an intermediate inclusion visitor.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public interface IIntermediateInclusionVisitor<TResult, TContext>
    {
        /// <summary>
        /// Visits the <paramref name="namedInclusion"/> provided.
        /// </summary>
        /// <param name="namedInclusion">The <see cref="INamedInclusionScopeCoercion"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        TResult Visit(INamedInclusionScopeCoercion namedInclusion, TContext context);
        /// <summary>
        /// Visits the <paramref name="renamedInclusion"/> provided.
        /// </summary>
        /// <param name="renamedInclusion">The <see cref="INamedInclusionRenameScopeCoercion"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        TResult Visit(INamedInclusionRenameScopeCoercion renamedInclusion, TContext context);
        /// <summary>
        /// Visits the <paramref name="namespaceInclusion"/> provided.
        /// </summary>
        /// <param name="namespaceInclusion">The <see cref="INamespaceInclusionScopeCoercion"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        TResult Visit(INamespaceInclusionScopeCoercion namespaceInclusion, TContext context);
        /// <summary>
        /// Visits the <paramref name="renamedNamespaceInclusion"/> to visit.
        /// </summary>
        /// <param name="renamedNamespaceInclusion">The <see cref="INamespaceInclusionRenameScopeCoercion"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        TResult Visit(INamespaceInclusionRenameScopeCoercion renamedNamespaceInclusion, TContext context);
        /// <summary>
        /// Visits the <paramref name="typeInclusion"/> provided.
        /// </summary>
        /// <param name="typeInclusion">The <see cref="ITypeInclusionScopeCoercion"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        TResult Visit(ITypeInclusionScopeCoercion typeInclusion, TContext context);
        /// <summary>
        /// Visits the <paramref name="renamedTypeInclusion"/> provided.
        /// </summary>
        /// <param name="renamedTypeInclusion">The <see cref="ITypeInclusionRenameScopeCoercion"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        TResult Visit(ITypeInclusionRenameScopeCoercion renamedTypeInclusion, TContext context);
        /// <summary>
        /// Visits the <paramref name="staticInclusion"/> provided.
        /// </summary>
        /// <param name="staticInclusion">The <see cref="IStaticInclusionScopeCoercion"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        TResult Visit(IStaticInclusionScopeCoercion staticInclusion, TContext context);
    }
}
