using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
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
        /// <summary>
        /// Creates a new <see cref="IAssembly"/>
        /// with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// representing part of the identity of the assembly.</param>
        /// <returns>A new <see cref="IAssembly"/>
        /// with the <paramref name="name"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// <paramref name="name"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when
        /// <paramref name="name"/> is <see cref="String.Empty"/>.</exception>
        IAssembly CreateAssembly(string name);
    }

    public interface ILanguage<TLanguage, TProvider> :
        ILanguage
        where TLanguage :
            ILanguage<TLanguage, TProvider>
        where TProvider :
            ILanguageProvider<TLanguage, TProvider>
    {
        /// <summary>
        /// Returns a new <typeparamref name="TProvider"/> associated to the current
        /// <see cref="ILanguage{TLanguage, TProvider}"/>.
        /// </summary>
        /// <returns>A new <typeparamref name="TProvider"/> for the current
        /// <see cref="ILanguage{TLanguage, TProvider}"/>.</returns>
        new TProvider GetProvider();
        /// <summary>
        /// Returns a new <typeparamref name="TProvider"/> associated to the current
        /// <see cref="ILanguage{TLanguage, TProvider}"/>.
        /// </summary>
        /// <param name="identityManager">The <see cref="IIdentityManager"/>
        /// which assists in resolving types</param>
        /// <returns>A new <typeparamref name="TProvider"/> for the current
        /// <see cref="ILanguage{TLanguage, TProvider}"/>.</returns>
        new TProvider GetProvider(IIdentityManager identityManager);
    }
}
