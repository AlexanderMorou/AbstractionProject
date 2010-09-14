using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Parsers.Oilexer
{
    public interface ITokenStream<T> :
        IControlledStateCollection<T>
        where T :
            IToken
    {
        
    }
}
