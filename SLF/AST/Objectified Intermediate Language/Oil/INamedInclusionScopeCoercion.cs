using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil
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
