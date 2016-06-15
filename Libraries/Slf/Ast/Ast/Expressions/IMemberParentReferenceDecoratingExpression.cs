using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public interface IMemberParentReferenceDecoratingExpression :
        IMemberParentReferenceExpression,
        IDecoratingExpression,
        IUnaryOperationPrimaryTerm
    {
        /// <summary>
        /// Returns/sets the <see cref="IMemberParentReferenceExpression"/> which
        /// is decorated by the <see cref="IMemberParentReferenceDecoratingExpression"/>.
        /// </summary>
        new IMemberParentReferenceExpression ContainedExpression { get; set; }
    }
}
