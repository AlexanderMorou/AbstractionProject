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
    public class IterationDeclarationBlockStatement :
        IterationBlockBaseStatement,
        IIterationDeclarationBlockStatement
    {
        public IterationDeclarationBlockStatement(IBlockStatementParent parent, ILocalDeclarationsStatement localDeclaration, IExpression condition, IEnumerable<IStatementExpression> iterations)
            : base(parent, condition, iterations)
        {
            this.LocalDeclaration = localDeclaration;
        }

        #region IIterationDeclarationBlockStatement Members

        public ILocalDeclarationsStatement LocalDeclaration { get; set; }

        #endregion


        public override void Visit(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
