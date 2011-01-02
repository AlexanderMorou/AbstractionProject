using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    public class IterationDeclarationBlockStatement :
        IterationBlockBaseStatement,
        IIterationDeclarationBlockStatement
    {
        public IterationDeclarationBlockStatement(IBlockStatementParent parent, ILocalDeclarationStatement localDeclaration, IExpression condition, IEnumerable<IStatementExpression> iterations)
            : base(parent, condition, iterations)
        {
            this.LocalDeclaration = localDeclaration;
        }

        #region IIterationDeclarationBlockStatement Members

        public ILocalDeclarationStatement LocalDeclaration { get; set; }

        #endregion

    }
}
