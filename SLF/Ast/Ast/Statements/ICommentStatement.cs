using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    /// <summary>
    /// Defines properties and methods for working with a statement
    /// which describes a block of code.
    /// </summary>
    public interface ICommentStatement :
        IStatement
    {
        /// <summary>
        /// Returns/sets the <see cref="String"/> value
        /// representing the comment.
        /// </summary>
        string Comment { get; set; }
    }
}
