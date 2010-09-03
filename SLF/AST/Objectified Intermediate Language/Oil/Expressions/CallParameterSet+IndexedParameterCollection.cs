using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
