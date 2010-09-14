using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer.Rules
{
    public class SyntacticalFollowTable :
        ControlledStateDictionary<SyntacticalDFAState, SyntacticalFollowInfo>
    {
        public SyntacticalFollowTable()
        {
        }

        public SyntacticalFollowInfo Follow(SyntacticalDFAState origin, SyntacticalDFAState continuesAt)
        {
            SyntacticalFollowInfo result = new SyntacticalFollowInfo(origin, continuesAt);
            this._Add(origin, result);
            return result;
        }

    }
}
