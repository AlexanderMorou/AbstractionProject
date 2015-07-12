using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    public class YieldBreakStatement :
        StatementBase,
        IYieldBreakStatement
    {
        public YieldBreakStatement(IStatementParent parent)
            : base(parent)
        {
        }
        public override void Visit(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Visit<TResult, TContext>(IStatementVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }
    }
}
