using AllenCopeland.Abstraction.Slf.Abstract.Documentation;
using AllenCopeland.Abstraction.Slf.Ast.Documentation.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Ast.Documentation.Builder
{
    internal class DocCommentBuilderRoot<TDocCommentBuilder> :
        IDocCommentBuilderRoot<TDocCommentBuilder>
        where TDocCommentBuilder :
            IDocCommentBuilderRoot<TDocCommentBuilder>
    {
        /// <summary>
        /// Obtains a builder for the summary of the <see cref="IDocComment"/>
        /// that will result.
        /// </summary>
        /// <returns>
        /// A <see cref="IDocCommentSectionBuilderRoot{TDocCommentBuilder}"/>
        /// instance which will promote progressively building the
        /// summary portion of the comment.</returns>
        public IDocCommentSectionBuilderRoot<TDocCommentBuilder> Summary()
        {
            var result = new DocCommentSectionBuilderRoot<TDocCommentBuilder>(this);
        }
        /// <summary>
        /// Obtains a builder for the remarks of the <see cref="IDocComment"/>
        /// that will result.
        /// </summary>
        /// <returns>
        /// A <see cref="IDocCommentSectionBuilderRoot{TDocCommentBuilder}"/>
        /// instance which will promote progressively building the
        /// remarks portion of the comment.</returns>
        public IDocCommentSectionBuilderRoot<TDocCommentBuilder> Remarks()
        {

        }
        /// <summary>
        /// Builds the <see cref="IDocComment"/> from the call chain observed
        /// from the root instance.
        /// </summary>
        /// <returns>A <see cref="IDocComment"/> from the call chain observed
        /// from the root instance.</returns>
        public IDocComment Build()
        {

        }
    }
    internal class DocCommentBuilderRoot :
        DocCommentBuilderRoot<IDocCommentBuilderRoot>,
        IDocCommentBuilderRoot
    {

    }
}
