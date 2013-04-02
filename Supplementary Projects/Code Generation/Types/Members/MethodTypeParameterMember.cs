using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Translation;

namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{
    [Serializable]
    internal class MethodTypeParameterMember :
        TypeParameterMember<CodeTypeParameter, IMethodSignatureMember<IMethodParameterMember, IMethodTypeParameterMember, CodeMemberMethod, IMemberParentType>>,
        IMethodTypeParameterMember
    {

        public MethodTypeParameterMember(string name, IMethodMember parentTarget)
            : base(name, parentTarget)
        {
        }

        public override CodeTypeParameter GenerateCodeDom(ICodeDOMTranslationOptions options)
        {
            return base.GenerateCodeDom(options);
        }

    }
}
