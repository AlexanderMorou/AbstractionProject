using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Parsers
{
    /// <summary>
    /// Defines properties and methods for working with a production rule.
    /// </summary>
    public interface IProductionRule
    {
        /// <summary>
        /// Returns the <see cref="FileLocale"/> which indicates
        /// where in the source the <see cref="IProductionRule"/> is.
        /// </summary>
        FileLocale Location { get; }
    }
}
