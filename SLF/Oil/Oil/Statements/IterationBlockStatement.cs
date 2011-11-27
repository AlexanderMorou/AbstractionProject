using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    public class IterationBlockStatement :
        IterationBlockBaseStatement,
        IIterationBlockStatement
    {

        public IterationBlockStatement(IBlockStatementParent parent, IEnumerable<IStatementExpression> initializers, IExpression condition, IEnumerable<IStatementExpression> iterations)
            : base(parent, condition, iterations)
        {
            this.Initializers = new MalleableStatementExpressionCollection(initializers);
        }

        #region IIterationBlockStatement Members

        public IMalleableStatementExpressionCollection Initializers { get; private set; }

        #endregion

    }
}
