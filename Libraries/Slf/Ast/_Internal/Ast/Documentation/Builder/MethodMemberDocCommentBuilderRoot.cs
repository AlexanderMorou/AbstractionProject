using AllenCopeland.Abstraction.Slf.Ast.Documentation.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Ast.Documentation.Builder
{
    internal class MethodMemberDocCommentBuilderRoot<TDocCommentBuilder> :
        SignatureMemberDocCommentBuilderRoot<TDocCommentBuilder>,
        IMethodMemberDocCommentBuilderRoot<TDocCommentBuilder>
        where TDocCommentBuilder :
            IMethodMemberDocCommentBuilderRoot<TDocCommentBuilder>
    {

    }
}
