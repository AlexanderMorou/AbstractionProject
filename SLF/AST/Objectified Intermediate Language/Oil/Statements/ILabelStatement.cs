using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    /// <summary>
    /// Defines propertise and methods for working with a label statement
    /// used to name jump-targets in code.
    /// </summary>
    public interface ILabelStatement :
        IJumpTarget,
        IStatement
    {
        /// <summary>
        /// Returns/sets the name associated to the 
        /// <see cref="ILabelStatement"/>.
        /// </summary>
        string Name { get; set; }
    }
}
