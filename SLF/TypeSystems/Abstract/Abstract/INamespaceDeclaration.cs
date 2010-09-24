using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with
    /// a namespace declaration.
    /// </summary>
    public interface INamespaceDeclaration :
        INamespaceParent,
        IDeclaration
    {
        /// <summary>
        /// Returns the <see cref="IAssembly"/> associated
        /// to the <see cref="INamespaceDeclaration"/>.
        /// </summary>
        new IAssembly Assembly { get; }
        /// <summary>
        /// Returns the <see cref="INamespaceParent"/>
        /// which contains the <see cref="INamespaceDeclaration"/>.
        /// </summary>
        INamespaceParent Parent { get; }
        /// <summary>
        /// Returns the <see cref="INamespaceDeclaration"/>'s
        /// full name.
        /// </summary>
        string FullName { get; }
    }
}
