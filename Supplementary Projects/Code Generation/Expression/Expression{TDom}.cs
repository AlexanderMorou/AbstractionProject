using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Translation;

namespace AllenCopeland.Abstraction.OldCodeGen.Expression
{
    [Serializable]
    public abstract class Expression<TDom> :
        Expression,
        IExpression<TDom>
        where TDom :
            CodeExpression,
            new()
    {
        #region IExpression<TDom> Members

        public new virtual TDom GenerateCodeDom(ICodeDOMTranslationOptions options)
        {
            return new TDom();
        }

        #endregion

        protected override CodeExpression OnGenerateCodeDom(ICodeDOMTranslationOptions options)
        {
            return this.GenerateCodeDom(options);
        }


    }
}
