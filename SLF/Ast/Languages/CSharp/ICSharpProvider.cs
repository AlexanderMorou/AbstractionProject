using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Parsers;
using AllenCopeland.Abstraction.Slf.Translation;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// provider for the C&#9839; language.
    /// </summary>
    public interface ICSharpProvider :
        ILanguageProvider<ICSharpLanguage, ICSharpProvider>,
        IVersionedHighLevelLanguageProvider<CSharpLanguageVersion, ICSharpCompilationUnit>
    {
        /// <summary>
        /// Returns the <see cref="ICSharpParser"/>
        /// of the current high level language provider instance.
        /// </summary>
        new ICSharpParser Parser { get; }
        /// <summary>
        /// Returns the <see cref="ICSharpCSTTranslator"/>
        /// of the current C&#9839; provider.
        /// </summary>
        new ICSharpCSTTranslator CSTTranslator { get; }
        /// <summary>
        /// Returns the <see cref="ICSharpLanguage"/>
        /// associated to the current C&#9839; provider.
        /// </summary>
        new ICSharpLanguage Language { get; }
        /// <summary>
        /// Creates a new <see cref="ICSharpAssembly"/>
        /// with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// representing part of the identity of the assembly.</param>
        /// <returns>A new <see cref="ICSharpAssembly"/>
        /// with the <paramref name="name"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// <paramref name="name"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when
        /// <paramref name="name"/> is <see cref="String.Empty"/>.</exception>
        new ICSharpAssembly CreateAssembly(string name);
    }
}
