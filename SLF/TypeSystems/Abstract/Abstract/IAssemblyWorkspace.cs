using System;
using System.Collections.Generic;
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
    /// Defines properties and methods for working with an 
    /// assembly workspace to unify the references
    /// of an assembly into one global namespace.
    /// </summary>
    public interface IAssemblyWorkspace :
        INamespaceParent
    {
        /// <summary>
        /// Returns the assembly in which work space is intended for.
        /// </summary>
        new IAssembly Assembly { get; }

        /// <summary>
        /// Returns the series of <see cref="IAssembly"/> instances
        /// referenced by the workspace.
        /// </summary>
        IAssemblyReferenceCollection References { get; }
    }
}
