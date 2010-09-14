using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;

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
