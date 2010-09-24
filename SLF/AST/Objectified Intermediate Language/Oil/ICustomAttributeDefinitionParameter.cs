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

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// custom attribute definition parameter.
    /// </summary>
    public interface ICustomAttributeDefinitionParameter :
        IDisposable
    {
        /// <summary>
        /// Returns the <see cref="ICustomAttributeDefinitionParameter"/>
        /// value defined on one of the <see cref="ICustomAttributeDefinition"/>'s
        /// constructor argument(s).
        /// </summary>
        object Value { get; }
    }
}
