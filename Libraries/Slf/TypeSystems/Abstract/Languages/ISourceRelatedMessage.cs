using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Defines properties and methods for working with a message
    /// associated to a given source <see cref="Uri"/>.
    /// </summary>
    public interface ISourceRelatedMessage
    {
        /// <summary>
        /// Returns the <see cref="String"/> associated to the 
        /// <see cref="ISourceRelatedMessage"/>.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Returns the <see cref="LineColumnPair"/> which denotes the position, within <see cref="Source"/>,
        /// in which the <see cref="ISourceRelatedMessage"/> starts.
        /// </summary>
        LineColumnPair Start { get; }

        /// <summary>
        /// Returns the <see cref="LineColumnPair"/> which denotes the position, within the <see cref="Source"/>,
        /// at which the <see cref="ISourceRelatedMessage"/> ends.
        /// </summary>
        LineColumnPair End { get; }
        
        /// <summary>
        /// Returns the <see cref="Uri"/> which denotes the specific file
        /// in which the source related message pertains to.
        /// </summary>
        Uri Source { get; }
    }
}
