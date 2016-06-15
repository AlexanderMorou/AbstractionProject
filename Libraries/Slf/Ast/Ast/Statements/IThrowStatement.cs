using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    /// <summary>
    /// Defines properties and methods for working with a statement which causes the runtime environment to encapsulate the stack details and throw an exception.
    /// </summary>
#if DEBUG
    [VisitorTargetAttribute("StatementVisitor", ContextualVisitor  = true)]
    [VisitorTargetAttribute("StatementVisitor", YieldingVisitor    = true)]
    [VisitorTargetAttribute("StatementVisitor", ContextualVisitor  = true,
                                                YieldingVisitor    = true)]
    [VisitorTargetAttribute("StatementVisitor")]
    [VisitorTargetAttribute("IntermediateCodeVisitor", DerivedThroughInheriting = "StatementVisitor")]
    [VisitorTargetAttribute("IntermediateTreeVisitor", DerivedThroughInheriting = "IntermediateCodeVisitor")]
#endif
    public interface IThrowStatement :
        IStatement
    {
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> which represents the exception to be thrown.
        /// </summary>
        /// <remarks>Different runtime environments may have different conceptual limitations on the
        /// evaluated type of the expression represented by this statement.  If throw target is null, a 'rethrow' is intended.</remarks>
        IExpression ThrowTarget { get; set; }
    }
}
