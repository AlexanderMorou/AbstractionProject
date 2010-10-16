using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Translation;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public interface IHighLevelLanguageProvider<TRootNode> :
        ILanguageProvider
        where TRootNode :
            IConcreteNode
    {
        ILanguageParser<TRootNode> Parser { get; }
        ILanguageASTTranslator<TRootNode> ASTTranslator { get; }
        IIntermediateCompiler<TRootNode> Compiler { get; }
        IIntermediateCodeTranslator Translator { get; }
        IHighLevelLanguage<TRootNode> Language { get; }
    }
}
