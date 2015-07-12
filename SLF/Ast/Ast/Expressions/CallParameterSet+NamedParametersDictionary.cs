using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
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
