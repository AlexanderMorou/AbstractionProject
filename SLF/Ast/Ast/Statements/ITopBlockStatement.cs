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

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    /// <summary>
    /// Defines properties and methods for a classification of a
    /// top-level block statement used for 
    /// methods, operator coercions, type coercions,
    /// the get/set parts of a property and expression
    /// workspaces.
    /// </summary>
    public interface ITopBlockStatement :
        IBlockStatementParent
    {

    }
}
