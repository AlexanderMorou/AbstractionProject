using System;
using System.Collections.Generic;
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
    /* *
     * A switch statement's 'cases' are nothing more than a label that's 
     * jumped to when a condition is met.
     * */
    /// <summary>
    /// Defines properties and methods for working with a switch case block statement.
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
    public interface ISwitchCaseBlockStatement :
        IBreakableBlockStatement,
        IJumpTarget
    {
        /// <summary>
        /// Returns the <see cref="IMalleableExpressionCollection"/> that relates to the current 
        /// switch case.
        /// </summary>
        IMalleableExpressionCollection Cases { get; }
        /// <summary>
        /// Returns the <see cref="ISwitchStatement"/> in which the current
        /// <see cref="ISwitchCaseBlockStatement"/> exists within.
        /// </summary>
#if DEBUG
        [VisitorImplementationIgnoreProperty]
#endif
        new ISwitchStatement Parent { get; }
        /// <summary>
        /// Returns whether the current <see cref="ISwitchCaseBlockStatement"/>
        /// represents the default case
        /// </summary>
        bool IsDefault { get; set; }
        /// <summary>
        /// Returns/sets whether the <see cref="ISwitchCaseBlockStatement"/> should
        /// have a new line after each of its <see cref="Cases"/>.
        /// </summary>
        /// <remarks>A given language's compliance with this property is optional.</remarks>
        bool LineBreakAfterEachCase { get; set; }
        IGoToCaseStatement GetGoTo(IStatementParent gotoContainer);
    }
}
