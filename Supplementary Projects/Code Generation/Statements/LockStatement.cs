using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Statements
{
    using Expression;

    public class LockStatement :
        BlockedStatement<CodeSnippetStatement>,
        ILockStatement
    {
        public LockStatement(IExpression lockedExpression)
        {
            this.LockedExpression = lockedExpression;
        }

        public override CodeSnippetStatement GenerateCodeDom(Translation.ICodeDOMTranslationOptions options)
        {
            throw new NotImplementedException();
        }

        #region ILockStatement Members

        public IExpression LockedExpression { get; private set; }

        #endregion

    }
}
