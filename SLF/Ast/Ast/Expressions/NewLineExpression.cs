using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public class NewLineExpression :
        DecorationExpression,
        INewLineExpression
    {
        public override ExpressionKind Type
        {
            get { return ExpressionKind.NewLineExpression; }
        }

        public NewLineExpression(DecorationDisplaySide side)
            : base(side)
        { 
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            visitor.Visit(this);
        }

        public override TResult Visit<TContext, TResult>(IExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            return visitor.Visit(this, context);
        }
    }
}
