using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

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
            public override ExpressionKind Type
            {
                get {
                    return ExpressionKind.RangeVariableReference;
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
