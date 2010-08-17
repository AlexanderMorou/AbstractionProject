using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Assembly information pertinent to an <see cref="IAssembly"/>
    /// </summary>
    public interface IAssemblyInformation
    {
        /// <summary>
        /// Returns the name of the assembly.
        /// </summary>
        string AssemblyName { get; }
        /// <summary>
        /// Returns the title of the assembly.
        /// </summary>
        string Title { get; }
        /// <summary>
        /// Returns the description of the assembly.
        /// </summary>
        string Description { get; }
        /// <summary>
        /// Returns the company name of the assembly.
        /// </summary>
        string Company { get; }
        /// <summary>
        /// Returns the product name of the assembly.
        /// </summary>
        string Product { get; }
        /// <summary>
        /// Returns the copyright information of the assembly.
        /// </summary>
        string Copyright { get; }
        /// <summary>
        /// Returns the trademark of the assembly.
        /// </summary>
        string Trademark { get; }
        /// <summary>
        /// Returns the culture, relative to the <see cref="CultureInfo"/>, of the assembly.
        /// </summary>
        ICultureIdentifier Culture { get; }
        /// <summary>
        /// Returns the version of the assembly file.
        /// </summary>
        Version FileVersion { get; }
        /// <summary>
        /// Returns the version of the assembly.
        /// </summary>
        Version AssemblyVersion { get; }
    }
}
