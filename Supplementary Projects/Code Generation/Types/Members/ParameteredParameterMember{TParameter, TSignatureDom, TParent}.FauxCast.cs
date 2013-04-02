using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using AllenCopeland.Abstraction.OldCodeGen.Utilities.Collections;

namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{
    partial class ParameteredParameterMember<TParameter, TSignatureDom, TParent>
    {
        protected class FauxCast :
            ParameterInfo
        {
        }
    }
}
