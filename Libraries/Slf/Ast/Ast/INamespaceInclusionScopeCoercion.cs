using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
#if DEBUG
    [VisitorTargetAttribute("ScopeCoercionVisitor", ContextualVisitor   = true)]
    [VisitorTargetAttribute("ScopeCoercionVisitor", YieldingVisitor     = true)]
    [VisitorTargetAttribute("ScopeCoercionVisitor", ContextualVisitor   = true,
                                                    YieldingVisitor     = true)]
    [VisitorTargetAttribute("ScopeCoercionVisitor")]
#endif
    public interface INamespaceInclusionScopeCoercion :
        IScopeCoercion
    {
        /// <summary>
        /// Returns/sets the <see cref="String"/> value associated to the 
        /// namespace to include to coerce identity resolution.
        /// </summary>
        string Namespace { get; set; }
    }
}
