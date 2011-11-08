using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Modules
{
    /// <summary>
    /// Defines properties and methods for working with a series of <see cref="IModule"/>
    /// instances for an <see cref="IAssembly"/>.
    /// </summary>
    public interface IModuleDictionary :
        IControlledStateDictionary<IGeneralDeclarationUniqueIdentifier, IModule>
    {
        /// <summary>
        /// Returns the <see cref="IAssembly"/> that contains the <see cref="IModuleDictionary"/>.
        /// </summary>
        IAssembly Parent { get; }
    }
}
