using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
    /// <summary>
    /// Defines properties and methods for working with a section of a 
    /// documentation
    /// </summary>
    public interface IDocCommentSection :
        IControlledCollection<IDocCommentElement>
    {
        /// <summary>
        /// Returns the <see cref="String"/> value which
        /// represents the name of the section.
        /// </summary>
        string SectionName { get; }
    }
}
