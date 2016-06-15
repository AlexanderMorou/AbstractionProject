using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public interface ICliLanguageProvider<TLanguage, TProvider> :
        ILanguageProvider<TLanguage, TProvider>
        where TLanguage :
            ILanguage<TLanguage, TProvider>
        where TProvider :
            ICliLanguageProvider<TLanguage, TProvider>
    {
    }
}
