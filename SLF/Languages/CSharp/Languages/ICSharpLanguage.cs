using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public interface ICSharpLanguage :
        IHighLevelLanguage<ICSharpCompilationUnit>
    {
        new ICSharpProvider GetProvider();
        /// <summary>
        /// Returns the <see cref="CSharpLanguageVersion"/> which denotes
        /// which version of C&#9839; is represented by the <see cref="ICSharpLanguage"/>.
        /// </summary>
        CSharpLanguageVersion Version { get; }
    }
}
