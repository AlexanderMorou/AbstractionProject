using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Defines properties and methods for working with a
    /// language vendor.
    /// </summary>
    public interface ILanguageVendor
    {
        /// <summary>
        /// Returns the <see cref="String"/> value associated to 
        /// the name of a given vendor.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Returns the <see cref="Guid"/> associated to the vendor of a
        /// particular language or languages.
        /// </summary>
        Guid Guid { get; }
    }
}
