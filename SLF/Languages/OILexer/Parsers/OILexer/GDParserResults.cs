using System;
using System.Collections.Generic;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Parsers.Oilexer
{
    internal class GDParserResults :
        ParserResults<IGDFile>
    {
        internal void SetResult(IGDFile result)
        {
            base.Result = result;
        }
    }
}
