using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    /// <summary>
    /// Defines properties and methods for working with an iterative loop which repeates so 
    /// long as its <see cref="IWhileStatement.Condition"/> is true.
    /// </summary>
    public interface IWhileStatement :
        IBreakableBlockStatement
    {
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> which must evaluate to true for the
        /// block of the <see cref="IWhileStatement"/> to be evaluated.
        /// </summary>
        IExpression Condition { get; set; }
    }
}
