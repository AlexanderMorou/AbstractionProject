using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

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
