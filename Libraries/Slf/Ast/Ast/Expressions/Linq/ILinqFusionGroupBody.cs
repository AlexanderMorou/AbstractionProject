using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// language integrated query body which acts as
    /// a group body that fuses with another query.
    /// </summary>
#if DEBUG
    [VisitorTargetAttribute("LinqBodyVisitor", ContextualVisitor    = true)]
    [VisitorTargetAttribute("LinqBodyVisitor", YieldingVisitor      = true)]
    [VisitorTargetAttribute("LinqBodyVisitor", ContextualVisitor    = true,
                                               YieldingVisitor      = true)]
    [VisitorTargetAttribute("LinqBodyVisitor")]
    [VisitorTargetAttribute("ExpressionVisitor", DerivedThroughInheriting = "LinqBodyVisitor")]
    [VisitorTargetAttribute("IntermediateCodeVisitor", DerivedThroughInheriting = "ExpressionVisitor")]
    [VisitorTargetAttribute("IntermediateTreeVisitor", DerivedThroughInheriting = "IntermediateCodeVisitor")]
#endif
    public interface ILinqFusionGroupBody :
        ILinqGroupBody,
        ILinqFusionBody,
        ILinqRangeVariableParent
    {
    }
}
