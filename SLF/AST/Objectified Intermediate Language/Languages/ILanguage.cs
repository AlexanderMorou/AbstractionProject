using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Defines properties and methods information 
    /// about a particular language.
    /// </summary>
    public interface ILanguage
    {
        /// <summary>
        /// Returns the <see cref="String"/> value representing
        /// the unique name of the language.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Returns a new <see cref="ILanguageProvider"/> associated to the current
        /// <see cref="ILanguage"/>.
        /// </summary>
        /// <returns>A new <see cref="ILanguageProvider"/> for the current
        /// <see cref="ILanguage"/>.</returns>
        ILanguageProvider GetProvider();
        /// <summary>
        /// Returns the level of functionality support the compiler contains.
        /// </summary>
        CompilerSupport CompilerSupport { get; }
        /// <summary>
        /// Returns the <see cref="ILanguageVendor"/> of a particular language.
        /// </summary>
        ILanguageVendor Vendor { get; }
        /// <summary>
        /// Returns the <see cref="Guid"/> associated to the language when
        /// defining symbolic documents associated to a given assembly derived
        /// from the language.
        /// </summary>
        Guid Guid { get; }
    }
}
