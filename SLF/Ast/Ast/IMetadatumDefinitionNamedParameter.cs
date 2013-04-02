using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for working with a named parameter
    /// on a custom attribute.
    /// </summary>
    public interface IMetadatumDefinitionNamedParameter :
        IMetadatumDefinitionParameter
    {
        /// <summary>
        /// Returns/sets the name of the parameter
        /// defined on the custom attribute.
        /// </summary>
        string Name { get; set; }
    }
}
