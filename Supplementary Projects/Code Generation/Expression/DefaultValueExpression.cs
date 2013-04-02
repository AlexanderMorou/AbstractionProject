using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types;
using AllenCopeland.Abstraction.OldCodeGen.Translation;

namespace AllenCopeland.Abstraction.OldCodeGen.Expression
{
    public class DefaultValueExpression :
        IDefaultValueExpression
    {

        public DefaultValueExpression(ITypeReference defaultType)
        {
            if (defaultType == null)
                throw new ArgumentNullException("defaultType");
            this.DefaultType = defaultType;
        }
        #region IDefaultValueExpression Members

        public ITypeReference DefaultType { get; private set; }

        #endregion

        #region IExpression Members

        public System.CodeDom.CodeExpression GenerateCodeDom(ICodeDOMTranslationOptions options)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ITypeReferenceable Members

        public void GatherTypeReferences(ref ITypeReferenceCollection result, ICodeTranslationOptions options)
        {
            if (result == null)
                result = new TypeReferenceCollection();
            if (DefaultType != null)
                result.Add(this.DefaultType);
        }

        #endregion
    }
}
