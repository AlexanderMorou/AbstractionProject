using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;

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
