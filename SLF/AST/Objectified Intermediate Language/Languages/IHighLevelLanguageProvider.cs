using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Compilers;
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
        ILanguageParser<TRootNode> Parser { get; }
        ILanguageASTTranslator<TRootNode> ASTTranslator { get; }
        IIntermediateCodeTranslator Translator { get; }
        IHighLevelLanguage<TRootNode> Language { get; }
    }
}
