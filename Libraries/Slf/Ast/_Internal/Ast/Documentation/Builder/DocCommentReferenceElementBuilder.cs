using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Documentation.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Ast.Documentation.Builder
{
    internal class DocCommentReferenceElementBuilder<TDocCommentBuilder, TSection> :
        DocCommentSectionElement<TDocCommentBuilder, TSection>,
        IDocCommentReferenceElementBuilder<TDocCommentBuilder, TSection>
        where TDocCommentBuilder :
            IDocCommentBuilderRoot<TDocCommentBuilder>
        where TSection :
            IDocCommentSectionElement<TDocCommentBuilder, TSection>
    {
        /// <summary>
        /// Returns the <see cref="IDeclaration"/> referenced by the
        /// <see cref="IDocCommentReferenceElementBuilder{TDocCommentBuilder, TSection}"/>
        /// </summary>
        IDeclaration ReferencedElement { get; }
    }
}
