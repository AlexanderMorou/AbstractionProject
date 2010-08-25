using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using System.IO;
using AllenCopeland.Abstraction.Slf.Cst;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public interface ILanguageParser<TRootNode> :
        ILanguageProcessor<TRootNode, TextReader>
        where TRootNode :
            IConcreteNode
    {
    }
}
