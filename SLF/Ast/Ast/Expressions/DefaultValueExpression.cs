using AllenCopeland.Abstraction.Slf.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public class DefaultValueExpression :
        ExpressionBase,
        IDefaultValueExpression
    {
        public DefaultValueExpression() { }
        public DefaultValueExpression(IType typeToDefault) { this.TypeToDefault = typeToDefault; }

        public override ExpressionKind Type
        {
            get { return ExpressionKind.DefaultValueExpression; }
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Visit<TContext, TResult>(IExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public IType TypeToDefault { get; set; }
    }
}
