using System;
using System.Collections.Generic;
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
    /// Defines properties and methods for working with a break statement;
    /// which, in turn, is a goto statement where the target
    /// is the exit-point for break-capable block parents.
    /// </summary>
    public interface IBreakStatement :
        IJumpStatement
    {
        /// <summary>
        /// Returns the <see cref="IBreakExit"/> of 
        /// the <see cref="IBreakStatement"/>.
        /// </summary>
        new IBreakExit Target { get; }
    }
}