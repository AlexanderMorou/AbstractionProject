using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public class AwaitExpression :
        MemberParentReferenceExpressionBase,
        IAwaitExpression
    {
        public override ExpressionKind Type
        {
            get {return ExpressionKind.AwaitOperation; }
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult, TContext>(ICommonExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public IExpression Proffer { get; set; }
    }

    public class AwaitStatementExpression :
        AwaitExpression,
        IAwaitStatementExpression
    {
        public bool ValidAsStatement
        {
            get { return this.ValidAsStatement; }
        }

        public new IStatementExpression Proffer { get { return (IStatementExpression)base.Proffer; } set { base.Proffer = value; } }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult, TContext>(ICommonExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }
    }
}
