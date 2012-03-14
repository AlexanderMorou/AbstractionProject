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
    /// Defines properties and methods for working with a 
    /// custom attribute definition parameter.
    /// </summary>
    public interface IMetadatumDefinitionParameter :
        IDisposable
    {
        /// <summary>
        /// Returns the <see cref="IMetadatumDefinitionParameter"/>
        /// value defined on one of the <see cref="IMetadatumDefinition"/>'s
        /// constructor argument(s).
        /// </summary>
        object Value { get; }
    }
}
