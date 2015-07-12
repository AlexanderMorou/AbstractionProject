using AllenCopeland.Abstraction.Slf.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Parsers
{
    public interface IParserSyntaxWarning :
        ISourceRelatedWarning,
        IParserSyntaxMessage
    {
    }
}
