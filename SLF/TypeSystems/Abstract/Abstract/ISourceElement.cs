using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with an
    /// element defined within source.
    /// </summary>
    public interface ISourceElement
    {
        /// <summary>
        /// Returns/sets the filename associated to the <see cref="ISourceElement"/>.
        /// </summary>
        string FileName { get; set; }
        /// <summary>
        /// The <see cref="LineColumnPair"/> which denotes
        /// the start point of the <see cref="ISourceElement"/>.
        /// </summary>
        LineColumnPair? Start { get; set; }
        /// <summary>
        /// The <see cref="LineColumnPair"/> which denotes the
        /// end point of the <see cref="ISourceElement"/>.
        /// </summary>
        LineColumnPair? End { get; set; }
    }
}
