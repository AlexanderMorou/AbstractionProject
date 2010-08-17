using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working
    /// with the parent of a namespace.
    /// </summary>
    /// <remarks>Namespace parents
    /// are also able to contain types.</remarks>
    public interface INamespaceParent :
        ITypeParent
    {
        /// <summary>
        /// Returns the <see cref="INamespaceDictionary"/>
        /// of <see cref="INamespaceDeclaration"/> instances
        /// contained within the <see cref="INamespaceParent"/>.
        /// </summary>
        INamespaceDictionary Namespaces { get; }
    }
}
