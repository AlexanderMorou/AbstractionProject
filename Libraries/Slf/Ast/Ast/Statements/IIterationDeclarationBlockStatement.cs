﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
#if DEBUG
    [VisitorTargetAttribute("StatementVisitor", ContextualVisitor = true)]
    [VisitorTargetAttribute("StatementVisitor", YieldingVisitor = true)]
    [VisitorTargetAttribute("StatementVisitor", ContextualVisitor = true,
                                                YieldingVisitor = true)]
    [VisitorTargetAttribute("StatementVisitor")]
    [VisitorTargetAttribute("IntermediateCodeVisitor", DerivedThroughInheriting = "StatementVisitor")]
    [VisitorTargetAttribute("IntermediateTreeVisitor", DerivedThroughInheriting = "IntermediateCodeVisitor")]
#endif
    public interface IIterationDeclarationBlockStatement :
        IIterationBlockBaseStatement
    {
        /// <summary>
        /// Returns/sets the <see cref="ILocalDeclarationsStatement"/> which executes once at the initialization
        /// of the iteration process.
        /// </summary>
        ILocalDeclarationsStatement LocalDeclaration { get; set; }
    }
}
