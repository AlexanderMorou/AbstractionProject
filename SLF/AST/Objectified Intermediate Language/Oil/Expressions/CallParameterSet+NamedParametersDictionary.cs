using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    partial class CallParameterSet 
    {
        protected class NamedParametersDictionary :
            CallNamedParameterDictionary
        {
            private CallParameterSet owner;
            internal NamedParametersDictionary(CallParameterSet owner)
            {
                this.owner = owner;
            }

            protected internal override void _Add(string key, INamedParameterExpression value)
            {
                this.owner.ElementAdded(value);
                base.Add(key, value);
            }
        }

        internal void ElementAdded(IExpression value)
        {
            this.verbatimOrder.Add(value);
        }
    }
}
