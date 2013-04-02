using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types;
using AllenCopeland.Abstraction.OldCodeGen.Types.Members;
using AllenCopeland.Abstraction.OldCodeGen.Comments;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Translation;

namespace AllenCopeland.Abstraction.OldCodeGen.Expression
{
    [Serializable]
    public class FieldReferenceExpression :
        ParentMemberReference<CodeFieldReferenceExpression>,
        IFieldReferenceExpression
    {
        public FieldReferenceExpression(string name, IMemberParentExpression reference)
            : base(name, reference)
        {
        }

        public override CodeFieldReferenceExpression GenerateCodeDom(ICodeDOMTranslationOptions options)
        {
            CodeFieldReferenceExpression fieldRef = new CodeFieldReferenceExpression();
            fieldRef.TargetObject = Reference.GenerateCodeDom(options);
            fieldRef.FieldName = this.Name;
            return fieldRef;
        }

        #region IFieldReferenceExpression Members

        public new IFieldReferenceComment GetReferenceParticle()
        {
            return ((IFieldReferenceComment)(this.OnGetReferenceParticle()));
        }

        #endregion

        public override void GatherTypeReferences(ref ITypeReferenceCollection result, ICodeTranslationOptions options)
        {
            base.GatherTypeReferences(ref result, options);
        }
    }
}