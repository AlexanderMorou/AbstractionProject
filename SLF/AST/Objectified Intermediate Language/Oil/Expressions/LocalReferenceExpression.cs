using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public class LocalReferenceExpression :
        MemberParentReferenceExpressionBase,
        IMemberReferenceExpression,
        ILocalReferenceExpression
    {
        #region IMemberReferenceExpression Members

        /// <summary>
        /// Returns/sets the name of the local to reference.
        /// </summary>
        public string Name { get; set; }

        #endregion

        public override ExpressionKind Type
        {
            get { return ExpressionKinds.LocalReference; }
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
