using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf.Oil
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
}
