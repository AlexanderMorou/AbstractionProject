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
    /// The ordering direction used for a 
    /// <see cref="ILinqOrderByClause"/>
    /// to determine which direction the results are 
    /// ordered.
    /// </summary>
    public enum LinqOrderByDirection
    {
        /// <summary>
        /// The <see cref="ILinqOrderByClause"/> is ordered 
        /// in a way that is unspecified, and thus will be ordered
        /// by the default set forth by the language.
        /// </summary>
        Unspecified,
        /// <summary>
        /// The <see cref="ILinqOrderByClause"/> is ordered
        /// in ascending (normal) order.
        /// </summary>
        Ascending,
        /// <summary>
        /// The <see cref="ILinqOrderByClause"/> is ordered
        /// in descending (reverse) order.
        /// </summary>
        Descending,
    }
    /// <summary>
    /// Defines properties and methods for working with a directed
    /// order by clause which potentially has multiple ordering keys and
    /// directions.
    /// </summary>
#if DEBUG
    [VisitorTargetAttribute("LinqClauseVisitor", ContextualVisitor  = true)]
    [VisitorTargetAttribute("LinqClauseVisitor", YieldingVisitor    = true)]
    [VisitorTargetAttribute("LinqClauseVisitor", ContextualVisitor  = true,
                                                 YieldingVisitor    = true)]
    [VisitorTargetAttribute("LinqClauseVisitor")]
    [VisitorTargetAttribute("ExpressionVisitor", DerivedThroughInheriting = "LinqClauseVisitor")]
    [VisitorTargetAttribute("IntermediateCodeVisitor", DerivedThroughInheriting = "ExpressionVisitor")]
    [VisitorTargetAttribute("IntermediateTreeVisitor", DerivedThroughInheriting = "IntermediateCodeVisitor")]
#endif
    public interface ILinqOrderByClause :
        ILinqClause
    {
        /// <summary>
        /// Returns the <see cref="LinqOrderingPair"/> collection
        /// that directs the output data set.
        /// </summary>
        ILinqOrderingPairCollection Orderings { get; }
    }

}
