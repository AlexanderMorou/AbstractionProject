using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for working 
    /// with an intermediate namespace declaration.
    /// </summary>
    public interface IIntermediateNamespaceDeclaration :
        IIntermediateNamespaceParent,
        IIntermediateDeclaration,
        INamespaceDeclaration,
        IIntermediateSegmentableDeclaration<IGeneralDeclarationUniqueIdentifier, IIntermediateNamespaceDeclaration>
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateAssembly"/> associated
        /// to the <see cref="IIntermediateNamespaceDeclaration"/>.
        /// </summary>
        new IIntermediateAssembly Assembly { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateNamespaceParent"/>
        /// which contains the <see cref="IIntermediateNamespaceDeclaration"/>.
        /// </summary>
        new IIntermediateNamespaceParent Parent { get; }
        /// <summary>
        /// Suspends the duality in the type layout where members 
        /// inserted in methods, fields, classes and so on are 
        /// dually inserted in a verbatim-order master set.
        /// </summary>
        /// <remarks>Incremental function, all resumes must
        /// be invoked prior to resuming the duality.</remarks>
        void SuspendDualLayout();
        /// <summary>
        /// Resumes the duality in the type layout where members
        /// inserted in methods, fields, classes, and so on are
        /// dually inserted in a verbatim-order master set.
        /// </summary>
        /// <remarks>Incremental function, all resumes must
        /// be invoked prior to resuming the duality.</remarks>
        void ResumeDualLayout();
        /// <summary>
        /// Visits the <paramref name="visitor"/> provided.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateDeclarationVisitor"/>
        /// which should receive the <see cref="IIntermediateNamespaceDeclaration"/> as a visitor.</param>
        void Visit(IIntermediateDeclarationVisitor visitor);
        /// <summary>
        /// Visits the <paramref name="visitor"/> provided.
        /// </summary>
        /// <typeparam name="TResult">The type of value to return for the visitor.</typeparam>
        /// <typeparam name="TContext">The type of context passed to the visitor.</typeparam>
        /// <param name="visitor">The <see cref="IIntermediateDeclarationVisitor{TResult, TContext}"/>
        /// which should receive the <see cref="IntermediateNamespaceDeclaration"/> as a visitor.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        TResult Visit<TResult, TContext>(IIntermediateDeclarationVisitor<TResult, TContext> visitor, TContext context);

    }
}
