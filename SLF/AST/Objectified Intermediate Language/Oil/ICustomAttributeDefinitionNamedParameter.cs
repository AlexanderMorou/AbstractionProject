using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for working with a named parameter
    /// on a custom attribute.
    /// </summary>
    public interface ICustomAttributeDefinitionNamedParameter :
        ICustomAttributeDefinitionParameter
    {
        /// <summary>
        /// Returns/sets the name of the parameter
        /// defined on the custom attribute.
        /// </summary>
        string Name { get; set; }
    }
}
