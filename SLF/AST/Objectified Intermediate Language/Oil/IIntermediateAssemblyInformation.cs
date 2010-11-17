using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for interacting with an intermediate
    /// assembly's information.
    /// </summary>
    public interface IIntermediateAssemblyInformation :
        IAssemblyInformation,
        IDisposable
    {
        /// <summary>
        /// Returns/sets the name of the assembly.
        /// </summary>
        new string AssemblyName { get; set; }
        /// <summary>
        /// Returns/sets the title of the assembly.
        /// </summary>
        new string Title { get; set; }
        /// <summary>
        /// Returns/sets the description of the assembly.
        /// </summary>
        new string Description { get; set; }
        /// <summary>
        /// Returns/sets the company name of the assembly.
        /// </summary>
        new string Company { get; set; }
        /// <summary>
        /// Returns/sets the product name of the assembly.
        /// </summary>
        new string Product { get; set; }
        /// <summary>
        /// Returns/sets the copyright information of the assembly.
        /// </summary>
        new string Copyright { get; set; }
        /// <summary>
        /// Returns/sets the trademark of the assembly.
        /// </summary>
        new string Trademark { get; set; }
        /// <summary>
        /// Returns/sets the culture, relative to the <see cref="CultureInfo"/>, of the assembly.
        /// </summary>
        new ICultureIdentifier Culture { get; set; }
        /// <summary>
        /// Returns/sets the version of the assembly file.
        /// </summary>
        new Version FileVersion { get; set; }
        /// <summary>
        /// Returns/sets the version of the assembly.
        /// </summary>
        new Version AssemblyVersion { get; set; }
        /// <summary>
        /// Instructs the <see cref="IIntermediateAssemblyInformation"/> to
        /// emit the associated custom attributes onto the assembly.
        /// </summary>
        /// <remarks>Typically used by a compiler or code translator.</remarks>
        void ReadyAssemblyMetaData(bool full = true);
    }
}
