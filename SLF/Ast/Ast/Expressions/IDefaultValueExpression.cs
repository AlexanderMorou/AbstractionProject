using AllenCopeland.Abstraction.Slf.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public interface IDefaultValueExpression :
        IMemberParentReferenceExpression
    {
        /// <summary>
        /// Returns/sets the <see cref="IType"/> for which the <see cref="IDefaultValueExpression"/>
        /// should yield the default of.
        /// </summary>
        IType TypeToDefault { get; set; }
    }
}
