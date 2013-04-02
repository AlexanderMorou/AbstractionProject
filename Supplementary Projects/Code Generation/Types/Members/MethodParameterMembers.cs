using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Utilities.Collections;
using System.Runtime.Serialization;

namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{
    [Serializable]
    public class MethodParameterMembers :
        ParameteredParameterMembers<IMethodParameterMember, CodeMemberMethod, IMemberParentType>,
        IMethodParameterMembers
    {

        public MethodParameterMembers(IMethodMember targetDeclaration)
            : base(targetDeclaration)
        {

        }

        protected override IMembers<IMethodParameterMember, IParameteredDeclaration<IMethodParameterMember, CodeMemberMethod, IMemberParentType>, CodeParameterDeclarationExpression> OnGetPartialClone(IParameteredDeclaration<IMethodParameterMember, CodeMemberMethod, IMemberParentType> parent)
        {
            throw new NotSupportedException("Method Parameter sets cannot be spanned across multiple instances, methods aren't segmentable.");
        }

        protected override IMethodParameterMember GetParameterMember(TypedName data)
        {
            return new MethodParameterMember(data, (IMethodMember)this.TargetDeclaration);
        }
    }
}
