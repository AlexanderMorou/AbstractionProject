using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Parsers;
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
