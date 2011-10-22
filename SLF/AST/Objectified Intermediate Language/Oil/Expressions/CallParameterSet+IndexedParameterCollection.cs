using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    partial class CallParameterSet
    {
        protected class IndexedParameterCollection :
            MalleableExpressionCollection
        {
            private CallParameterSet owner;
            public IndexedParameterCollection(CallParameterSet owner)
            {
                this.owner = owner;
            }
            protected override void AddImpl(IExpression expression)
            {
                if (expression is INamedParameterExpression)
                    this.owner.Add((INamedParameterExpression)expression);
                else
                {
                    this.owner.verbatimOrder.Add(expression);
                    base.AddImpl(expression);
                }
            }
        }
    }
}
