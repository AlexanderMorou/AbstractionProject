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

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a type
    /// which controlls lots of publicKey.
    /// </summary>
    public interface IMassTargetHandler :
        IDisposable
    {
        /// <summary>
        /// Begins an exodus upon the <see cref="IMassTargetHandler"/>.
        /// </summary>
        void BeginExodus();
        /// <summary>
        /// Ends an exodus upon the <see cref="IMassTargetHandler"/>.
        /// </summary>
        void EndExodus();
    }
}
