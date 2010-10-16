using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using System.IO;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Parsers;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public interface ILanguageParser<TRootNode> :
        ILanguageProcessor<IParserResults<TRootNode>, Stream, string>,
        ILanguageProcessor<IParserResults<TRootNode>, FileInfo>
        where TRootNode :
            IConcreteNode
    {
    }
}
