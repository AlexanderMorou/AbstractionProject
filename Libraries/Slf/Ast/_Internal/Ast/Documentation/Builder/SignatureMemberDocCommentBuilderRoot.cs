using AllenCopeland.Abstraction.Slf.Ast.Documentation.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Ast.Documentation.Builder
{
    internal class SignatureMemberDocCommentBuilderRoot<TDocCommentBuilder> :
        DocCommentBuilderRoot<TDocCommentBuilder>,
        ISignatureMemberDocCommentBuilderRoot<TDocCommentBuilder>
        where TDocCommentBuilder :
            ISignatureMemberDocCommentBuilderRoot<TDocCommentBuilder>
    {
        /// <summary>
        /// Obtains a builder for the returns of the <see cref="IDocComment"/>
        /// that will result.
        /// </summary>
        /// <returns>
        /// A <see cref="ISignatureMemberDocCommentBuilderRoot{TDocCommentBuilder}"/>
        /// instance which will promote progressively building the
        /// returns portion of the comment.</returns>
        IDocCommentSectionBuilderRoot<TDocCommentBuilder> Returns();
    }
    internal class SignatureMemberDocCommentBuilderRoot :
        SignatureMemberDocCommentBuilderRoot<ISignatureMemberDocCommentBuilderRoot>,
        ISignatureMemberDocCommentBuilderRoot
    {
    }
}
