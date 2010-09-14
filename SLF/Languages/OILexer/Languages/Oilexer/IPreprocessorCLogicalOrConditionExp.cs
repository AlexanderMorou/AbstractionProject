using System;
using System.Collections.Generic;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer
{
    public interface IPreprocessorCLogicalOrConditionExp :
        IPreprocessorCExp
    {
        IPreprocessorCLogicalOrConditionExp Left { get; }
        IPreprocessorCLogicalAndConditionExp Right { get; }

    }
}
