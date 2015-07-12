using AllenCopeland.Abstraction.Slf.Ast.Documentation.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Ast.Documentation.Builder
{
    internal class DocCommentTextElementBuilder<TDocCommentBuilder, TSection> :
        DocCommentSectionElement<TDocCommentBuilder, TSection>,
        IDocCommentTextElementBuilder<TDocCommentBuilder, TSection>
        where TDocCommentBuilder :
            IDocCommentBuilderRoot<TDocCommentBuilder>
        where TSection :
            IDocCommentSectionElement<TDocCommentBuilder, TSection>
    {
        /// <summary>
        /// Returns the text fragment which is represented by the current
        /// <see cref="IDocCommentTextElementBuilder{TDocCommentBuilder, TSection}"/>.
        /// </summary>
        string TextFragment { get; }
    }
}
