using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Cst;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public interface IIntermediateAssembly<TLanguage, TRootNode, TProvider, TVersion> :
        IIntermediateAssembly<TLanguage, TRootNode, TProvider>
        where TLanguage :
            IVersionedHighLevelLanguage<TVersion, TRootNode>
        where TRootNode :
            IConcreteNode
        where TProvider :
            IVersionedHighLevelLanguageProvider<TVersion, TRootNode>
    {
    }
}
