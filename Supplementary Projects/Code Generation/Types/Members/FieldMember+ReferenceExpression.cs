using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Expression;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Comments;
using System.Data;
using AllenCopeland.Abstraction.OldCodeGen._Internal;
using AllenCopeland.Abstraction.OldCodeGen.Translation;

namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{
    [Serializable]
    partial class FieldMember
    {
        public class ReferenceExpression :
            FieldReferenceExpression
        {
            internal FieldMember referencePoint;
            public ReferenceExpression(FieldMember referencePoint)
                : base(referencePoint.Name, GetRootReference(referencePoint))
            {
                this.referencePoint = referencePoint;
            }
            public ReferenceExpression(FieldMember referencePoint, IMemberParentExpression owner)
                : base(referencePoint.Name, owner)
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
            private static IMemberParentExpression GetRootReference(FieldMember referencePoint)
            {
                if (referencePoint.IsStatic || referencePoint.IsConstant)
                    return referencePoint.ParentTarget.GetTypeReference().GetTypeExpression();
                else if (referencePoint.ParentTarget is IMemberParentType)
                    return ((IMemberParentType)(referencePoint.ParentTarget)).GetThisExpression();
                else
                    return null;
            }

            public override CodeFieldReferenceExpression GenerateCodeDom(ICodeDOMTranslationOptions options)
            {
                CodeFieldReferenceExpression result = base.GenerateCodeDom(options);
                if (options.NameHandler.HandlesName(this.referencePoint))
                    result.FieldName = options.NameHandler.HandleName(this.referencePoint);
                return result;
            }

            protected override IMemberReferenceComment OnGetReferenceParticle()
            {
                return new Comment(this);
            }

            protected class Comment :
                MemberReferenceComment<IFieldReferenceExpression>,
                IFieldReferenceComment
            {

                public Comment(ReferenceExpression reference)
                    : base(reference)
                {
                }

                public override IFieldReferenceExpression Reference
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