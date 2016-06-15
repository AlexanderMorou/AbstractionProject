using AllenCopeland.Abstraction.Slf.Ast.Documentation.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Ast.Documentation.Builder
{
    internal class DocCommentSectionBuilderRoot<TDocCommentBuilder, TSection> :
        DocCommentSectionElement<TDocCommentBuilder, TSection>,
        IDocCommentSectionBuilderRoot<TDocCommentBuilder, TSection>
        where TDocCommentBuilder :
            IDocCommentBuilderRoot<TDocCommentBuilder>
        where TSection :
            IDocCommentSectionElement<TDocCommentBuilder, TSection>
    {
        /// <summary>
        /// Returns the <see cref="String"/> value denoting the name
        /// of the section.
        /// </summary>
        public string SectionName { get; private set; }
        internal DocCommentSectionBuilderRoot(string sectionName, DocCommentBuilderRoot<TDocCommentBuilder> parent)
            : base(parent)
        {

        }

    }
    internal class DocCommentSectionBuilderRoot<TDocCommentBuilder> :
        DocCommentSectionBuilderRoot<TDocCommentBuilder, IDocCommentSectionBuilderRoot<TDocCommentBuilder>>,
        IDocCommentSectionBuilderRoot<TDocCommentBuilder>
        where TDocCommentBuilder :
            IDocCommentBuilderRoot<TDocCommentBuilder>
    {
        internal DocCommentSectionBuilderRoot(string sectionName, DocCommentBuilderRoot<TDocCommentBuilder> parent)
            : base(sectionName, parent)
        {

        }
    }
}