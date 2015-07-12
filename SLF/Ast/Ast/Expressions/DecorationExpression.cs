using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public abstract class DecorationExpression :
        IDecorationExpression
    {
        public DecorationDisplaySide Side { get; set; }

        public abstract ExpressionKind Type { get; }

        public abstract void Visit(IExpressionVisitor visitor);

        public abstract TResult Visit<TContext, TResult>(IExpressionVisitor<TResult, TContext> visitor, TContext context);

        public Uri Location { get; set; }

        public LineColumnPair? Start { get; set; }

        public LineColumnPair? End { get; set; }

        public DecorationExpression(DecorationDisplaySide side)
        {
            this.Side = side;
        }
    }
}
