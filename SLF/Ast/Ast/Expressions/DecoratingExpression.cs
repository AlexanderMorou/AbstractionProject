using AllenCopeland.Abstraction.Slf.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public class DecoratingExpression :
        IDecoratingExpression
    {
        IMalleableExpressionCollection<IDecorationExpression> decorations;

        public IExpression ContainedExpression { get; set; }

        public DecoratingExpression(IExpression containedExpression, params IDecorationExpression[] decorations)
        {
            this.ContainedExpression = containedExpression;
            if (decorations != null && decorations.Length > 0)
                this.decorations = new MalleableExpressionCollection<IDecorationExpression>(decorations);
        }

        public IMalleableExpressionCollection<IDecorationExpression> Decorations
        {
            get { return decorations ?? (this.decorations = new MalleableExpressionCollection<IDecorationExpression>()); }
        }

        public ExpressionKind Type
        {
            get
            {
                return ExpressionKind.DecoratingExpression;
            }
        }

        public void Visit(IExpressionVisitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            visitor.Visit(this);
        }

        public TResult Visit<TContext, TResult>(IExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            return visitor.Visit(this, context);
        }

        public Uri Location { get; set; }

        public LineColumnPair? Start { get; set; }

        public LineColumnPair? End { get; set; }
    }
}
