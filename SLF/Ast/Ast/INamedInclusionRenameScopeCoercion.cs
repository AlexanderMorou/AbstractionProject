using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for working with a name
    /// that has been included in the scope which has not been 
    /// resolved as either a type or a namespace, which has 
    /// been given an alias, or renamed.
    /// </summary>
    public interface INamedInclusionRenameScopeCoercion :
        INamedInclusionScopeCoercion,
        IRenameScopeCoercion
    {
    }
}
