using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Parsers;
using AllenCopeland.Abstraction.Slf.Translation;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Cli;
using AllenCopeland.Abstraction.Slf.Languages.CSharp.Parsers;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
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
        IVersionedLanguageProvider<CSharpLanguageVersion>
    {
        /// <summary>
        /// Returns the <see cref="ICSharpParser"/>
        /// of the current high level language provider instance.
        /// </summary>
        ICSharpParser Parser { get; }
        /// <summary>
        /// Returns the <see cref="ICSharpCSTTranslator"/>
        /// of the current C&#9839; provider.
        /// </summary>
        ICSharpCSTTranslator CSTTranslator { get; }
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
        /// <summary>
        /// Returns the <see cref="IIntermediateCliManager"/> which marshalls
        /// the identities of intermediate and non-intermediate (compiled)
        /// assemblies and types.
        /// </summary>
        new IIntermediateCliManager IdentityManager { get; }
        /// <summary>
        /// Returns the series of types, relative to the active provider context,
        /// which are auto-form types.
        /// </summary>
        new IEnumerable<IType> AutoFormTypes { get; }

    }
}
