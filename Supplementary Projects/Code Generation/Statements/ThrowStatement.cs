using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Expression;
using AllenCopeland.Abstraction.OldCodeGen.Translation;

namespace AllenCopeland.Abstraction.OldCodeGen.Statements
{
    public class ThrowStatement :
        Statement<CodeThrowExceptionStatement>,
        IThrowStatement
    {
        public override CodeThrowExceptionStatement GenerateCodeDom(ICodeDOMTranslationOptions options)
        {
            throw new NotImplementedException();
        }

        public ThrowStatement(IExpression exceptionExpression)
        {
            this.ExceptionExpression = exceptionExpression;
        }

        public override void GatherTypeReferences(ref Types.ITypeReferenceCollection result, Translation.ICodeTranslationOptions options)
        {
            if (result == null)
                result = new TypeReferenceCollection();
            this.ExceptionExpression.GatherTypeReferences(ref result, options);
        }

        #region IThrowStatement Members

        public IExpression ExceptionExpression { get; private set; }

        #endregion

    }
}
