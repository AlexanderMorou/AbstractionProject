using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public class ExpressionToCommaFusionExpression :
        ExpressionBase,
        IExpressionToCommaFusionExpression
    {
        private MalleableExpressionCollection right;
        public ExpressionToCommaFusionExpression(IFusionCommaTargetExpression target)
        {
            this.Left = target;
        }
        public ExpressionToCommaFusionExpression(IFusionCommaTargetExpression target, params IExpression[] terms)
        {
            this.Left = target;
            foreach (var item in terms)
                this.Right.Add(item);
        }

        #region IExpressionToCommaFusionExpression Members

        public IFusionCommaTargetExpression Left { get; set; }

        /// <summary>
        /// Returns the <see cref="IMalleableExpressionCollection"/> on the right side
        /// of the expression to comma fusion.
        /// </summary>
        public IMalleableExpressionCollection Right
        {
            get
            {
                if (this.right == null)
                    this.right = new MalleableExpressionCollection();
                return this.right;
            }
        }

        #endregion

        public override ExpressionKind Type
        {
            get { return ExpressionKinds.ExpressionToCommaFusion; }
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", this.Left, string.Join(", ", this.Right.OnAll(p=>p.ToString()).ToArray()));
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
