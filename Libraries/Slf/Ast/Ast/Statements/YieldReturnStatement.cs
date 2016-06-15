using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    public class YieldReturnStatement :
        StatementBase,
        IYieldReturnStatement
    {
        public YieldReturnStatement(IStatementParent parent, IExpression yieldedResult)
            : base(parent)
        {
            this.YieldedResult = yieldedResult;
        }

        public override void Accept(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult, TContext>(IStatementVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public IExpression YieldedResult { get; set; }
    }
}
