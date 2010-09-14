using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    public class ExpressionStatement :
        StatementBase,
        IExpressionStatement
    {
        public ExpressionStatement(IStatementParent parent, IStatementExpression expression)
            : base(parent)
        {
            this.Expression = expression;
        }

        public override void Visit(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }

        #region IExpressionStatement Members

        public IStatementExpression Expression { get; set; }

        #endregion
    }
}
