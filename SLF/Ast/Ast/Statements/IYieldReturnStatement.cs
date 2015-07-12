using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    public interface IYieldReturnStatement :
        IStatement
    {
        /// <summary>
        /// Returns the <see cref="IExpression"/> which is yielded as a result of
        /// the <see cref="IYieldReturnStatement"/>.
        /// </summary>
        IExpression YieldedResult { get; set; }
    }
}
