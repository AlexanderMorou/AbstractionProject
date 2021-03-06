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

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for working with an
    /// included type which has been given an alias.
    /// </summary>
#if DEBUG
    [VisitorTargetAttribute("ScopeCoercionVisitor", ContextualVisitor   = true)]
    [VisitorTargetAttribute("ScopeCoercionVisitor", YieldingVisitor     = true)]
    [VisitorTargetAttribute("ScopeCoercionVisitor", ContextualVisitor   = true,
                                                    YieldingVisitor     = true)]
    [VisitorTargetAttribute("ScopeCoercionVisitor")]
#endif
    public interface ITypeInclusionRenameScopeCoercion :
        ITypeInclusionScopeCoercion,
        IRenameScopeCoercion
    {
    }
}
