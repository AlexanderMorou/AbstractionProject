using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Expression;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Comments;
using AllenCopeland.Abstraction.OldCodeGen._Internal;
using System.Data;
using AllenCopeland.Abstraction.OldCodeGen.Translation;

namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{
    partial class PropertyMember
    {
        [Serializable]
        internal protected class ReferenceExpression :
            PropertyReferenceExpression
        {
            internal PropertyMember referencePoint;
            public ReferenceExpression(PropertyMember referencePoint)
                : base(referencePoint.Name, GetRootReference(referencePoint))
            {
                this.referencePoint = referencePoint;
            }
            public ReferenceExpression(IMemberParentExpression owner, PropertyMember referencePoint)
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
            private static IMemberParentExpression GetRootReference(PropertyMember referencePoint)
            {

                if (referencePoint.IsStatic)
                    return referencePoint.ParentTarget.GetTypeReference().GetTypeExpression();
                else
                    return referencePoint.ParentTarget.GetThisExpression();
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
                    return _OIL._Core.BuildMemberReferenceComment(options, ((ReferenceExpression)this.Reference).referencePoint);
                }

            }
        }
    }
}