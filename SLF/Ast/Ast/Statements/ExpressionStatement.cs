using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
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

        public override string ToString()
        {
            return string.Format("{0};", this.Expression);
        }

        public override TResult Visit<TResult, TContext>(IStatementVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

    }
}
