using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
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

        public override void Accept(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool HasInitializers
        {
            get { return this.Initializers != null && this.Initializers.Count > 0; }
        }
    }
}
