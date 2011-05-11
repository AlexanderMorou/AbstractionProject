using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public interface IMicrosoftLanguageVendor :
        ILanguageVendor
    {
        ICSharpLanguage CSharp { get; }
    }
}
