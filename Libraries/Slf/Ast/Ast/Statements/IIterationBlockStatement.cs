using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    /// <summary>
    /// Defines properties and methods for working with a block statement
    /// that represents an iteration.
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
    public interface IIterationBlockStatement :
        IIterationBlockBaseStatement
    {
        bool HasInitializers { get; }
        /// <summary>
        /// Returns the <see cref="IMalleableStatementExpressionCollection"/> which executes once at the initialization
        /// of the iteration process.
        /// </summary>
#if DEBUG
        [VisitorPropertyRequirement("HasInitializers")]
#endif
        IMalleableStatementExpressionCollection Initializers { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with a
    /// base iteration block statement.
    /// </summary>
    public interface IIterationBlockBaseStatement :
        IBreakableBlockStatement
    {
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> which evaluates as a boolean to determine
        /// whether to continue the iteration process.
        /// </summary>
        IExpression Condition { get; set; }
        /// <summary>
        /// Returns the <see cref="IMalleableStatementExpressionCollection"/> of expressions that should 
        /// execute at the end of each iteration.
        /// </summary>
#if DEBUG
        [VisitorPropertyRequirement("HasIterations")]
#endif
        IMalleableStatementExpressionCollection Iterations { get; }
        bool HasIterations { get; }

    }
}
