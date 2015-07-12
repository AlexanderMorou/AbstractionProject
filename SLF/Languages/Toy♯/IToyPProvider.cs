using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages.ToySharp
{
    /// <summary>
    /// Defines properites and methods for working with the provider of the T*y++ Language.
    /// </summary>
    public interface IToySharpProvider :
        ILanguageProvider<IToySharpLanguage, IToySharpProvider>,
        IVersionedLanguageProvider<ToySharpLanguageVersion>
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateCliManager"/> which marshalls
        /// the identities of intermediate and non-intermediate (compiled)
        /// assemblies and types.
        /// </summary>
        new IIntermediateCliManager IdentityManager { get; }
    }
}
