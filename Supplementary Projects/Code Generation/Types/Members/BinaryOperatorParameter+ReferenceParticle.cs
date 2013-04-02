using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Comments;
using AllenCopeland.Abstraction.OldCodeGen.Expression;
using System.Data;
using AllenCopeland.Abstraction.OldCodeGen.Translation;

namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{
    partial class BinaryOperatorParameter
    {
        class ReferenceParticle :
            MemberReferenceComment<IBinaryOperatorParameter>,
            IParameterReferenceComment
        {
            /// <summary>
            /// Creates a new <see cref="ReferenceParticle"/> which is a documentation comment reference
            /// to the <see cref="BinaryOperatorParameter"/> which was used to create this object.
            /// </summary>
            /// <param name="reference">The <see cref="BinaryOperatorParameter"/> that the
            /// <see cref="ReferenceParticle"/> references.</param>
            public ReferenceParticle(BinaryOperatorParameter reference)
                : base(reference)
            {

            }
            public override string BuildCommentBody(ICodeTranslationOptions options)
            {
                return string.Format("<paramref name=\"{0}\"/>", this.Reference.Name);
            }

            #region IMemberReferenceComment<IParameterReferenceExpression> Members

            public new IParameterReferenceExpression Reference
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

            #endregion

        }
    }
}
