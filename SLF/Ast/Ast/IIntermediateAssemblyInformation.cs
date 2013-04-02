using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Globalization;
using AllenCopeland.Abstraction.Globalization;
using StringChangeArgs= AllenCopeland.Abstraction.Utilities.Events.EventArgsR1R2<string, string>;
using VersionChangeArgs = AllenCopeland.Abstraction.Utilities.Events.EventArgsR1R2<AllenCopeland.Abstraction.Slf.Abstract.IVersion, AllenCopeland.Abstraction.Slf.Abstract.IVersion>;
using CultureChangeArgs = AllenCopeland.Abstraction.Utilities.Events.EventArgsR1R2<AllenCopeland.Abstraction.Globalization.ICultureIdentifier, AllenCopeland.Abstraction.Globalization.ICultureIdentifier>;

 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    using StringChangeHandler = System.EventHandler<StringChangeArgs>;
    using VersionChangeHandler = System.EventHandler<VersionChangeArgs>;
    using CultureChangeHandler = System.EventHandler<CultureChangeArgs>;
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
        /// Occurs when <see cref="AssemblyName"/> changes.
        /// </summary>
        event StringChangeHandler AssemblyNameChanged;
        /// <summary>
        /// Returns/sets the title of the assembly.
        /// </summary>
        new string Title { get; set; }
        /// <summary>
        /// Occurs when the title of the assembly changes.
        /// </summary>
        event StringChangeHandler TitleChanged;
        /// <summary>
        /// Returns/sets the description of the assembly.
        /// </summary>
        new string Description { get; set; }
        /// <summary>
        /// Occurs when the description of the assembly changes.
        /// </summary>
        event StringChangeHandler DescriptionChanged;
        /// <summary>
        /// Returns/sets the company name of the assembly.
        /// </summary>
        new string Company { get; set; }
        /// <summary>
        /// Occurs when the company of the assembly changes.
        /// </summary>
        event StringChangeHandler CompanyChanged;
        /// <summary>
        /// Returns/sets the product name of the assembly.
        /// </summary>
        new string Product { get; set; }
        /// <summary>
        /// Occurs when the product of the assembly changes.
        /// </summary>
        event StringChangeHandler ProductChanged;
        /// <summary>
        /// Returns/sets the copyright information of the assembly.
        /// </summary>
        new string Copyright { get; set; }
        /// <summary>
        /// Occurs when the copyright of the assembly changes.
        /// </summary>
        event StringChangeHandler CopyrightChanged;
        /// <summary>
        /// Returns/sets the trademark of the assembly.
        /// </summary>
        new string Trademark { get; set; }
        /// <summary>
        /// Occurs when the trademark of the assembly changes.
        /// </summary>
        event StringChangeHandler TrademarkChanged;
        /// <summary>
        /// Returns/sets the culture, relative to the <see cref="CultureInfo"/>, of the assembly.
        /// </summary>
        new ICultureIdentifier Culture { get; set; }
        /// <summary>
        /// Occurs when the culture of the assembly changes.
        /// </summary>
        event CultureChangeHandler CultureChanged;
        /// <summary>
        /// Returns/sets the version of the assembly file.
        /// </summary>
        new IIntermediateVersion FileVersion { get; set; }
        /// <summary>
        /// Occurs when the file version of the assembly changes.
        /// </summary>
        event VersionChangeHandler FileVersionChanged;
        /// <summary>
        /// Returns/sets the version of the assembly.
        /// </summary>
        new IIntermediateVersion AssemblyVersion { get; set; }
        /// <summary>
        /// Occurs when the assembly version changes.
        /// </summary>
        event VersionChangeHandler AssemblyVersionChanged;
        /// <summary>
        /// Instructs the <see cref="IIntermediateAssemblyInformation"/> to
        /// emit the associated custom attributes onto the assembly.
        /// </summary>
        /// <remarks>Typically used by a compiler or code translator.</remarks>
        void ReadyAssemblyMetaData(bool full = true);
    }
}
