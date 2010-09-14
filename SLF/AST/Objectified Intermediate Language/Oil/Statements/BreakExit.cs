using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    public class BreakExit :
        StatementBase,
        IBreakExit
    {
        public BreakExit(IStatementParent parent)
            : base(parent)
        {

        }

        public override void Visit(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
