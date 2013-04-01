using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Visits declarations within an <see cref="IIntermediateAssembly"/>
    /// which defines the structure of some code in an abstract manner.
    /// </summary>
    public interface IIntermediateDeclarationVisitor
    {
        /// <summary>
        /// Visits the <paramref name="assembly"/> provided.
        /// </summary>
        /// <param name="assembly">The <see cref="IIntermediateAssembly"/> 
        /// to visit.</param>
        void Visit(IIntermediateAssembly assembly);
        /// <summary>
        /// Visits the <paramref name="namespace"/> provided.
        /// </summary>
        /// <param name="namespace">The <see cref="IIntermediateNamespaceDeclaration"/>
        /// to visit.</param>
        void Visit(IIntermediateNamespaceDeclaration @namespace);
    }
    /// <summary>
    /// Visits declarations within an <see cref="IIntermediateAssembly"/>
    /// which defines the structure of some code in an abstract manner.
    /// </summary>
    /// <typeparam name="TResult">The value returned upon visiting the expression.</typeparam>
    /// <typeparam name="TContext">The type of context passed to the visitor.</typeparam>
    public interface IIntermediateDeclarationVisitor<TResult, TContext>
    {
        /// <summary>
        /// Visits the <paramref name="assembly"/> provided.
        /// </summary>
        /// <param name="assembly">The <see cref="IIntermediateAssembly"/> 
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IIntermediateAssembly assembly, TContext context);
        /// <summary>
        /// Visits the <paramref name="namespace"/> provided.
        /// </summary>
        /// <param name="namespace">The <see cref="IIntermediateNamespaceDeclaration"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IIntermediateNamespaceDeclaration @namespace, TContext context);
    }
}
