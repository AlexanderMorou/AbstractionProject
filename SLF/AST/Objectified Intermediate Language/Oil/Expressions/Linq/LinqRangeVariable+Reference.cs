using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    partial class LinqRangeVariable
    {
        private class Reference : 
            MemberParentReferenceExpressionBase,
            ILinqRangeVariableReference
        {
            private LinqRangeVariable target;
            public Reference(LinqRangeVariable target)
            {
                this.target = target;
            }
            public override ExpressionKinds Type
            {
                get {
                    return ExpressionKinds.RangeVariableReference;
                }
            }

            public override void Visit(IExpressionVisitor visitor)
            {
                visitor.Visit(this);
            }

            #region IMemberReferenceExpression Members

            public string Name
            {
                get { return this.Target.Name; }
            }

            #endregion

            #region ILinqRangeVariableReference Members

            public ILinqRangeVariable Target
            {
                get { return this.target; }
            }

            #endregion
        }
    }
}
