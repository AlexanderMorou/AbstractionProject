using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    public class ThrowStatement :
        StatementBase,
        IThrowStatement
    {
        protected ThrowStatement(IStatementParent parent)
            : base(parent)
        {
        }

        public ThrowStatement(IStatementParent parent, IExpression throwTarget)
            : this(parent)
        {
            this.ThrowTarget = throwTarget;
        }

        public override void Visit(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Visit<TResult, TContext>(IStatementVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public IExpression ThrowTarget { get; set; }
    }
}
