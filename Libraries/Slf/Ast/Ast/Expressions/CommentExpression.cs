using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public class CommentExpression :
        DecorationExpression,
        ICommentExpression
    {
        public override ExpressionKind Type
        {
            get { return ExpressionKind.CommentExpression; }
        }

        public CommentExpression(string comment, DecorationDisplaySide side)
            : base(side)
        {
            this.Comment = comment;
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

        public string Comment { get; set; }
    }
}
