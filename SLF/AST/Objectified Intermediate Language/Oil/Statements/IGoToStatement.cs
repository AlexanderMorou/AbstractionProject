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
    /// Defines properties and methods for working with a 
    /// go to statement.
    /// </summary>
    public interface IGoToStatement :
        IJumpStatement
    {
        /// <summary>
        /// Returns/sets the <see cref="ILabelStatement"/>
        /// of the <see cref="IGoToStatement"/>.
        /// </summary>
        new ILabelStatement Target { get; set; }
    }
}
