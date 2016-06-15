using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Documentation.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Ast.Documentation.Builder
{
    internal class DocCommentExceptionSectionBuilderRoot<TDocCommentBuilder> :
        DocCommentSectionBuilderRoot<TDocCommentBuilder>,
        IDocCommentExceptionSectionBuilderRoot<TDocCommentBuilder>
        where TDocCommentBuilder :
            IDocCommentBuilderRoot<TDocCommentBuilder>
    {
        /// <summary>
        /// Returns the <see cref="IType"/> which denotes
        /// the type used for the exception.
        /// </summary>
        IType ExceptionType { get; }
    }
}
