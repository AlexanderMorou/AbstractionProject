using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Translation;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{

    public interface IHighLevelLanguageProvider<TRootNode> :
        ILanguageProvider
        where TRootNode :
            IConcreteNode
    {
        /// <summary>
        /// Returns the <see cref="ILanguageParser{TRootNode}"/>
        /// of the current high level language provider instance.
        /// </summary>
        ILanguageParser<TRootNode> Parser { get; }
        /// <summary>
        /// Returns the <see cref="ILanguageASTTranslator{TRootNode}"/>
        /// of the current high level language provider instance.
        /// </summary>
        ILanguageASTTranslator<TRootNode> ASTTranslator { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateCodeTranslator"/>
        /// of the current language provider.
        /// </summary>
        IIntermediateCodeTranslator Translator { get; }
        /// <summary>
        /// Returns the <see cref="IHighLevelLanguage{TRootNode}"/>
        /// of the current high level language provider instance.
        /// </summary>
        IHighLevelLanguage<TRootNode> Language { get; }
    }
}
