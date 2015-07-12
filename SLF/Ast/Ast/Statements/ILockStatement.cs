using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{

    public interface ILockStatement :
        IBlockStatement
    {
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> for which to enter (and after, exit) the monitor lock on.
        /// </summary>
        IExpression MonitorLock { get; set; }
    }
}
