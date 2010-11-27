using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Parsers;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Translation;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public interface ICSharpProvider :
        IHighLevelLanguageProvider<ICSharpCompilationUnit>
    {
        new ICSharpParser Parser { get; }
        new ICSharpASTTranslator ASTTranslator { get; }
        new ICSharpCompiler Compiler { get; }
        new ICSharpCodeTranslator Translator { get; }
        new ICSharpLanguage Language { get; }
    }
}
