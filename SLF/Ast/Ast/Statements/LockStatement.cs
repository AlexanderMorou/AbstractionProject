using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    public class LockStatement :
        BlockStatementBase,
        ILockStatement
    {
        protected LockStatement(IBlockStatementParent parent)
            : base(parent)
        {
        }

        public LockStatement(IBlockStatementParent parent, IExpression monitorLock)
            : this(parent)
        {
            this.MonitorLock = monitorLock;
        }


        public override void Visit(IStatementVisitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            visitor.Visit(this);
        }

        public override TResult Visit<TResult, TContext>(IStatementVisitor<TResult, TContext> visitor, TContext context)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            return visitor.Visit(this, context);
        }

        public IExpression MonitorLock { get; set; }
    }
}
