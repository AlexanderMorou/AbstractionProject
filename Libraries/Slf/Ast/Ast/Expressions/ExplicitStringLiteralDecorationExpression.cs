using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public class ExplicitStringLiteralDecorationExpression :
        DecorationExpression,
        IExplicitStringLiteralDecorationExpression
    {
        public ExplicitStringLiteralDecorationExpression(DecorationDisplaySide side)
            : base(side)
        {
        }
        public override ExpressionKind Type
        {
            get { return ExpressionKind.LiteralInsert; }
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult, TContext>(ICommonExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public string LiteralInsert { get; set; }
    }
}
