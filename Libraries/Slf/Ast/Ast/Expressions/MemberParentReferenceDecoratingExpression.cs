using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public class MemberParentReferenceDecoratingExpression :
        ExpressionBase,
        IMemberParentReferenceDecoratingExpression
    {
        IMalleableExpressionCollection<IDecorationExpression> decorations;

        public IMemberParentReferenceExpression ContainedExpression { get; set; }

        public MemberParentReferenceDecoratingExpression(IMemberParentReferenceExpression containedExpression, params IDecorationExpression[] decorations)
        {
            this.ContainedExpression = containedExpression;
            if (decorations != null && decorations.Length > 0)
                this.decorations = new MalleableExpressionCollection<IDecorationExpression>(decorations);
        }

        public IMalleableExpressionCollection<IDecorationExpression> Decorations
        {
            get { return decorations ?? (this.decorations = new MalleableExpressionCollection<IDecorationExpression>()); }
        }

        public override ExpressionKind Type
        {
            get
            {
                return ExpressionKind.DecoratingExpression;
            }
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            visitor.Visit(this);
        }

        public override TResult Accept<TResult, TContext>(ICommonExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            return visitor.Visit(this, context);
        }

        IExpression IDecoratingExpression.ContainedExpression
        {
            get
            {
                return this.ContainedExpression;
            }
            set
            {
                if (value is IMemberParentReferenceExpression)
                    throw new ArgumentException("value must be a `IMemberParentReferenceExpression`", "value");
                this.ContainedExpression = (IMemberParentReferenceExpression)value;
            }
        }
    }
}
