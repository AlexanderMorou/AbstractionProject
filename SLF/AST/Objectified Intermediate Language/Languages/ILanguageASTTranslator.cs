using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Cst;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public interface ILanguageASTTranslator<TRootNode> :
        ILanguageProcessor<TRootNode, IIntermediateAssembly>
        where TRootNode :
            IConcreteNode
    {

	}
}
