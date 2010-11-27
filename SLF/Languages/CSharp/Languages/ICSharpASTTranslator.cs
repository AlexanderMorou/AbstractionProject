using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public interface ICSharpASTTranslator :
        ILanguageASTTranslator<ICSharpCompilationUnit>
    {
    }
}
