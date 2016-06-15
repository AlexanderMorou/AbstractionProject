using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    public class BlockStatement :
        BlockStatementBase
    {
        public BlockStatement(IBlockStatementParent parent)
            : base(parent)
        {
        }

        public override TResult Accept<TResult, TContext>(IStatementVisitor<TResult, TContext> visitor, TContext context)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            return visitor.Visit(this, context);
        }

        public override void Accept(IStatementVisitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            visitor.Visit(this);
        }
    }
}
