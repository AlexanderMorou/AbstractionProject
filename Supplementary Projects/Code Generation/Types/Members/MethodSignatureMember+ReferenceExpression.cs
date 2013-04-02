using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Expression;
using AllenCopeland.Abstraction.OldCodeGen.Translation;

namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{
    partial class MethodSignatureMember<TParameter, TTypeParameter, TSignatureDom, TParent>
        where TParameter :
            IParameteredParameterMember<TParameter, TSignatureDom, TParent>
        where TTypeParameter :
            IMethodSignatureTypeParameterMember<TParameter, TTypeParameter, TSignatureDom, TParent>
        where TParent :
            IDeclarationTarget
        where TSignatureDom :
            CodeMemberMethod,
            new()
    {
        [Serializable]
        public class ReferenceExpression :
            MethodReferenceExpression
        {
            internal MethodSignatureMember<TParameter, TTypeParameter, TSignatureDom, TParent> referencePoint;
            public ReferenceExpression(MethodSignatureMember<TParameter, TTypeParameter, TSignatureDom, TParent> referencePoint)
                : base(referencePoint.Name, GetRootReference(referencePoint))
            {
                this.referencePoint = referencePoint;
            }

            public ReferenceExpression(MethodSignatureMember<TParameter, TTypeParameter, TSignatureDom, TParent> referencePoint, IMemberParentExpression parentExpression)
                : base(referencePoint.Name, parentExpression)
            {
                this.referencePoint = referencePoint;
            }
            public override string Name
            {
                get
                {
                    return this.referencePoint.Name;
                }
            }
            private static IMemberParentExpression GetRootReference(MethodSignatureMember<TParameter, TTypeParameter, TSignatureDom, TParent> referencePoint)
            {
                if (referencePoint is MethodMember)
                {
                    var mReference = referencePoint as MethodMember;
                    if (mReference.IsStatic)
                        return mReference.ParentTarget.GetTypeReference().GetTypeExpression();
                    else
                        return mReference.ParentTarget.GetThisExpression();
                }
                else
                    throw new NotSupportedException();
            }

            public override CodeMethodReferenceExpression GenerateCodeDom(ICodeDOMTranslationOptions options)
            {
                CodeMethodReferenceExpression result = base.GenerateCodeDom(options);
                if (options.NameHandler.HandlesName(this.referencePoint))
                    result.MethodName = options.NameHandler.HandleName(this.referencePoint);
                return result;
            }

        }
    }
}
