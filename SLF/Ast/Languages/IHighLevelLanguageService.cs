using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public interface IHighLevelLanguageService<TProvider, TLanguage, TRootNode> :
        IHighLevelLanguageService<TLanguage, TProvider>,
        IHighLevelLanguageService
        where TProvider :
            ILanguageProvider<TLanguage, TProvider>,
            IHighLevelLanguageProvider<TRootNode>
        where TLanguage :
            ILanguage<TLanguage, TProvider>,
            IHighLevelLanguage<TRootNode>
        where TRootNode :
            IConcreteNode
    {
    }
    public interface IHighLevelLanguageService<TLanguage, TProvider> :
        ILanguageService<TLanguage, TProvider>
        where TProvider :
            ILanguageProvider<TLanguage, TProvider>,            
            IHighLevelLanguageProvider
        where TLanguage :
            ILanguage<TLanguage, TProvider>,            
            IHighLevelLanguage
    {

    }
    public interface IHighLevelLanguageService :
        ILanguageService
    {
    }
}
