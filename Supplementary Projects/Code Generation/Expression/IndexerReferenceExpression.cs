using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types;
using AllenCopeland.Abstraction.OldCodeGen.Properties;
using AllenCopeland.Abstraction.OldCodeGen.Comments;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Translation;

namespace AllenCopeland.Abstraction.OldCodeGen.Expression
{
    public class IndexerReferenceExpression :
        ParentMemberReference<CodeIndexerExpression>,
        IIndexerReferenceExpression
    {
        private IExpressionCollection indices;
        public IndexerReferenceExpression(IMemberParentExpression reference, IExpressionCollection indices)
            : base(Resources.IndexerName, reference)
        {
            if (indices != null)
                foreach (IExpression exp in indices)
                    this.Indices.Add(exp);
        }

        public IExpressionCollection Indices
        {
            get
            {
                if (this.indices == null)
                    this.indices = new ExpressionCollection();
                return this.indices;
            }
        }

        public override CodeIndexerExpression GenerateCodeDom(ICodeDOMTranslationOptions options)
        {
            return new CodeIndexerExpression(this.Reference.GenerateCodeDom(options), this.Indices.GenerateCodeDom(options));
        }

    }
}
