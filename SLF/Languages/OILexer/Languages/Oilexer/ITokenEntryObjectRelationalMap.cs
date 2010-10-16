using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer
{
    public interface ITokenEntryObjectRelationalMap :
        IEntryObjectRelationalMap
    {
        new ITokenEntry Entry { get; }
    }
}
