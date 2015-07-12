using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    public class ConditionContinuationStatement :
        BlockStatementBase,
        IConditionContinuationStatement
    {
        public ConditionContinuationStatement(IBlockStatementParent parent)
            : base(parent)
        {
        }

        public override TResult Visit<TResult, TContext>(IStatementVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public override void Visit(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
