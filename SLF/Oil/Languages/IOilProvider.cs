using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Translation;
using AllenCopeland.Abstraction.Slf.Compilers;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public interface IOilProvider :
        IHighLevelLanguageProvider<IIntermediateAssembly>
    {
        /// <summary>
        /// Returns the <see cref="ILanguageParser{TRootNode}"/> for the <see cref="IOilProvider"/>.
        /// </summary>
        /// <exception cref="System.NotSupportedException">Not supported, language purely used to provide default
        /// compiler support for dynamic code.</exception>
        new ILanguageParser<IIntermediateAssembly> Parser { get; }
        /// <summary>
        /// Returns the <see cref="ILanguageASTTranslator"/> associated to the <see cref="IOilProvider"/>.
        /// </summary>
        new ILanguageASTTranslator<IIntermediateAssembly> ASTTranslator { get; }
        new IOilIntermediateCompiler Compiler { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateCodeTranslator"/> associated
        /// to the <see cref="IOilProvider"/>.
        /// </summary>
        /// <exception cref="System.NotSupportedException">Not supported, language purely used to provide default
        /// compiler support for dynamic code.</exception>
        new IIntermediateCodeTranslator Translator { get; }
        /// <summary>
        /// Returns the <see cref="IOilIntermediateLanguage"/> associated to the
        /// <see cref="IOilProvider"/>.
        /// </summary>
        new IOilIntermediateLanguage Language { get; }
    }
}
