using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Translation;

namespace AllenCopeland.Abstraction.OldCodeGen.Expression
{
    public interface IExpression<TDom> :
        IExpression
        where TDom :
            CodeExpression,
            new()
    {

        new TDom GenerateCodeDom(ICodeDOMTranslationOptions options);

    }
}
