using System;
using System.Collections.Generic;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Parsers.Oilexer
{
    /// <summary>
    /// Defines properties and methods for working with a parser that reads in a 
    /// <see cref="IGDFile"/>.
    /// </summary>
    public interface IGDParser :
        IParser<IGDToken, IGDTokenizer, IGDFile>
    {
    }
}
