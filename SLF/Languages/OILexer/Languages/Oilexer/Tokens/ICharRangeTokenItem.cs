using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using AllenCopeland.Abstraction.Slf.Languages.Oilexer.Tokens;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer.Tokens
{
    public interface ICharRangeTokenItem :
        ITokenItem
    {
        RegularLanguageSet Range { get; }
        bool Inverted { get; }
    }
}
