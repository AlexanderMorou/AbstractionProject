using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Translation;
using AllenCopeland.Abstraction.OldCodeGen.Expression;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Comments;
using System.Data;
using AllenCopeland.Abstraction.OldCodeGen._Internal;

namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{
    partial class PropertySignatureMember<TParent>
    {
        [Serializable]
        internal protected class ReferenceExpression :
            PropertyReferenceExpression
        {
            internal PropertySignatureMember<TParent> referencePoint;
            public ReferenceExpression(PropertySignatureMember<TParent> referencePoint)
                : base(referencePoint.Name, GetRootReference(referencePoint))
            {
                this.referencePoint = referencePoint;
            }
            public ReferenceExpression(IMemberParentExpression owner, PropertySignatureMember<TParent> referencePoint)
                : base(referencePoint.Name, owner)
            {
                this.referencePoint = referencePoint;
            }

            protected override IMemberReferenceComment OnGetReferenceParticle()
            {
                return new Comment(this);
            }

            public override string Name
            {
                get
                {
                    return this.referencePoint.Name;
                }
            }

            private static IMemberParentExpression GetRootReference(PropertySignatureMember<TParent> referencePoint)
            {
                if (referencePoint is PropertyMember)
                {
                    var rReference = referencePoint as PropertyMember;
                    if (rReference.IsStatic)
                        return rReference.ParentTarget.GetTypeReference().GetTypeExpression();
                    else
                        return rReference.ParentTarget.GetThisExpression();
                }
                else
                    throw new NotSupportedException();
            }

            public override CodePropertyReferenceExpression GenerateCodeDom(ICodeDOMTranslationOptions options)
            {
                CodePropertyReferenceExpression result = base.GenerateCodeDom(options);
                if (options.NameHandler.HandlesName(this.referencePoint))
                    result.PropertyName = options.NameHandler.HandleName(this.referencePoint);
                return result;
            }

            protected class Comment :
                MemberReferenceComment<IPropertyReferenceExpression>,
                IPropertyReferenceComment
            {

                public Comment(ReferenceExpression reference)
                    : base(reference)
                {
                }

                public override IPropertyReferenceExpression Reference
                {
                    get
                    {
                        return base.Reference;
                    }
                    set
                    {
                        throw new ReadOnlyException();
                    }
                }

                public override string BuildCommentBody(ICodeTranslationOptions options)
                {
                    return _OIL._Core.BuildMemberReferenceComment(options, ((ReferenceExpression) this.Reference).referencePoint);
                }

            }
        }
    }
}
