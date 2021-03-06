using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    /// <summary>
    /// Defines propertise and methods for working with a label statement
    /// used to name jump-targets in code.
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
    public interface ILabelStatement :
        IJumpTarget,
        IStatement
    {
        /// <summary>
        /// Returns/sets the name associated to the 
        /// <see cref="ILabelStatement"/>.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Obtains a <see cref="IGoToStatement"/> statement for the
        /// <see cref="ILabelStatement"/> which allows a jump
        /// to the label to occur.
        /// </summary>
        /// <param name="gotoContainer">The <see cref="IStatementParent"/>
        /// which contains the <see cref="IGoToStatement"/>
        /// created.</param>
        /// <returns>A new <see cref="IGoToStatement"/>
        /// associated to the current <see cref="ILabelStatement"/>.</returns>
        IGoToStatement GetGoTo(IStatementParent gotoContainer);
        bool IsReferenced { get; }
    }
}
