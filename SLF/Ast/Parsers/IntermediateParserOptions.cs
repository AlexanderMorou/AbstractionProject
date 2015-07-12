using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Parsers
{
    public interface IIntermediateParserOptions
    {
        IControlledCollection<INamedStreamInfo> Files { get; }
    }
}
