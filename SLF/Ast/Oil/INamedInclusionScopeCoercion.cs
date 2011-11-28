using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for working with a name to 
    /// be included within the active scope to alter identity resolution.
    /// </summary>
    /// <remarks>Associates a specific name to the active scope; requires
    /// further resolution to identify the specific kind of resolution that
    /// results, be it namespace, type, static or otherwise.</remarks>
    public interface INamedInclusionScopeCoercion :
        IScopeCoercion
    {
        /// <summary>
        /// Returns/sets the name included in the scope
        /// which coerces symbol table resolution.
        /// </summary>
        string IncludedName { get; set; }
    }
}
