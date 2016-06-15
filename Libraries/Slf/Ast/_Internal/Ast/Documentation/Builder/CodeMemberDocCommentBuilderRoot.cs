using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Documentation.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Ast.Documentation.Builder
{
    internal class CodeMemberDocCommentBuilderRoot<TDocCommentBuilder> :
        DocCommentBuilderRoot<TDocCommentBuilder>,
        ICodeMemberDocCommentBuilderRoot<TDocCommentBuilder>
        where TDocCommentBuilder :
            ICodeMemberDocCommentBuilderRoot<TDocCommentBuilder>
    {
        IDocCommentExceptionSectionBuilderRoot<TDocCommentBuilder> Exception(IType exceptionType);
    }
    internal class CodeMemberDocCommentBuilderRoot :
        CodeMemberDocCommentBuilderRoot<ICodeMemberDocCommentBuilderRoot>,
        ICodeMemberDocCommentBuilderRoot
    {
    }
}
