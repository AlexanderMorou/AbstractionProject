using System;
using System.Collections.Generic;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer
{
    public interface IPreprocessorCLogicalAndConditionExp :
        IPreprocessorCExp
    {
        IPreprocessorCLogicalAndConditionExp Left { get; }
        IPreprocessorCEqualityExp Right { get; }
    }
}
