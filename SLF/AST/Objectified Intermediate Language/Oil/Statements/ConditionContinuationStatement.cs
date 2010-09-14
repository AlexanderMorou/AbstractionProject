using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    public class ConditionContinuationStatement :
        BlockStatementBase,
        IConditionContinuationStatement
    {
        public ConditionContinuationStatement(IBlockStatementParent parent)
            : base(parent)
        {
        }
    }
}
