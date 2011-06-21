using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;
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
    /// language which is versioned.
    /// </summary>
    /// <typeparam name="TVersion"></typeparam>
    public interface IVersionedLanguage<TVersion> :
        ILanguage
    {
        /// <summary>
        /// Returns a new <see cref="ILanguageProvider"/> associated to the current
        /// <see cref="IVersionedLanguage{TVersion}"/>.
        /// </summary>
        /// <param name="version">The <typeparamref name="TVersion"/>
        /// which denotes what specific version of the language to obtain
        /// the provider for.</param>
        /// <returns>A new <see cref="IVersionedLanguageProvider"/> for the current
        /// <see cref="IVersionedLanguage{TVersion}"/>.</returns>
        IVersionedLanguageProvider<TVersion> GetProvider(TVersion version);
        /// <summary>
        /// Returns the level of functionality support the compiler contains
        /// for the given <paramref name="version"/> of the language.
        /// </summary>
        /// <param name="version">The <typeparamref name="TVersion"/></param>
        CompilerSupport GetCompilerSupport(TVersion version);
        /// <summary>
        /// Returns a <see cref="IEnumerable{T}"/> of
        /// elements which denotes the various versions of 
        /// the language.
        /// </summary>
        IEnumerable<TVersion> Versions { get; }
    }
}
