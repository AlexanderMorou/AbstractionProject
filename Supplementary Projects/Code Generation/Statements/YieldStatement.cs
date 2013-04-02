using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Expression;
using AllenCopeland.Abstraction.OldCodeGen.Types;
using AllenCopeland.Abstraction.OldCodeGen.Translation;
/* * 
 * Oilexer is an open-source project and must be released
 * as per the license associated to the project.
 * */
namespace AllenCopeland.Abstraction.OldCodeGen.Statements
{
    public class YieldStatement :
        Statement<CodeSnippetStatement>,
        IYieldStatement
    {
        public override CodeSnippetStatement GenerateCodeDom(ICodeDOMTranslationOptions options)
        {
            throw new NotImplementedException();
        }

        public override void GatherTypeReferences(ref ITypeReferenceCollection result, ICodeTranslationOptions options)
        {
            if (result == null)
                result = new TypeReferenceCollection();
            if (this.Result != null)
                this.Result.GatherTypeReferences(ref result, options);
        }

        #region IYieldStatement Members

        public IExpression Result { get; set; }

        #endregion
    }
}
