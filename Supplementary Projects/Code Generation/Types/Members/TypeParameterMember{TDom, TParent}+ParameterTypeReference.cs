using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Translation;

namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{

    partial class TypeParameterMember<TDom, TParent>
    {

        [Serializable]
        private class ParameterTypeReference :
            TypeReferenceBase,
            ITypeReference
        {
            public ParameterTypeReference(TypeParameterMember<TDom, TParent> reference)
                : base(reference)
            {
            }
            public override CodeTypeReference GenerateCodeDom(ICodeDOMTranslationOptions options)
            {
                CodeTypeReference result = base.GenerateCodeDom(options);
                result.Options = CodeTypeReferenceOptions.GenericTypeParameter;
                return result;
            }
        }
    }
}
