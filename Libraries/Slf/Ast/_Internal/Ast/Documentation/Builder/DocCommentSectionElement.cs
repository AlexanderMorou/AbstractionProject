using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Documentation.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Ast.Documentation.Builder
{
    internal class DocCommentSectionElement<TDocCommentBuilder, TSection>
        where TDocCommentBuilder :
            IDocCommentBuilderRoot<TDocCommentBuilder>
        where TSection :
            IDocCommentSectionElement<TDocCommentBuilder, TSection>
    {
        /// <summary>
        /// Denotes the end of the section, back to the parent.
        /// </summary>
        /// <returns>A new instance of the <typeparamref name="TDocCommentBuilder"/>
        /// with the context of the <see cref="IDocCommentSectionBuilderRoot{TDocCommentBuilder}"/>
        /// noted.</returns>
        public TDocCommentBuilder EndSection()
        {

        }
        /// <summary>
        /// Obtains a <see cref="IDocCommentTextElementBuilder{TDocCommentBuilder, TSection}"/>
        /// with the <paramref name="textFragment"/> provided.
        /// </summary>
        /// <param name="textFragment">The <see cref="String"/> value denoting
        /// the text the fragment represents.</param>
        /// <returns>A <see cref="IDocCommentTextElementBuilder{TDocCommentBuilder, TSection}"/>
        /// with the <paramref name="textFragment"/> provided.</returns>
        IDocCommentTextElementBuilder<TDocCommentBuilder, TSection> TextFragment(string textFragment);
        IDocCommentReferenceElementBuilder<TDocCommentBuilder, TSection> Reference(IDeclaration referencedElement);
    }
}
