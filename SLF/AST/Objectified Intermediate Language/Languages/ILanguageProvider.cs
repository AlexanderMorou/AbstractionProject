using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Parsers;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    
    /// <summary>
    /// Defines properties and methods for working with a language provider.
    /// </summary>
    public interface ILanguageProvider
    {
        /// <summary>
        /// Obtains the compiler for the language.
        /// </summary>
        /// <returns>A <see cref="ICompiler"/> instance which can
        /// compile the language provided by the <see cref="ILanguageProvider"/>.
        /// </returns>
        ICompiler GetCompiler();
        /// <summary>
        /// Obtains a parser from the <paramref name="parserGuid"/> provided.
        /// </summary>
        /// <param name="parserGuid">A <see cref="Guid"/> of the parser
        /// to obtain.</param>
        /// <returns>A new <see cref="IParser"/> instance from the 
        /// <paramref name="parserGuid"/> provided.</returns>
        /// <exception cref="System.NotSupportedException">thrown when the <paramref name="parserGuid"/>
        /// is not a supported parser of the <see cref="ILanguageProvider"/>.</exception>
        IParser GetParser(Guid parserGuid);
        /// <summary>
        /// Returns a <see cref="IEnumerable{T}"/> of <see cref="Guid"/> instances
        /// of the parsers the <see cref="ILanguageProvider"/> provides.
        /// </summary>
        IEnumerable<Guid> ParserGuids { get; }
        /// <summary>
        /// Returns the <see cref="String"/> name for a given parser
        /// based off of its <paramref name="parserGuid"/>.
        /// </summary>
        /// <param name="parserGuid">The <see cref="Guid"/> of the parser
        /// to obtain the name of.</param>
        /// <returns>A <see cref="String"/> name for a given <paramref name="parserGuid"/>.</returns>
        /// <exception cref="System.NotSupportedException">thrown when the <paramref name="parserGuid"/>
        /// is not a supported parser of the <see cref="ILanguageProvider"/>.</exception>
        string GetParserName(Guid parserGuid);

    }
}
