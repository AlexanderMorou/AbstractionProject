using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// <see cref="IControlledTypeCollection"/> which cannot be 
    /// modified.
    /// </summary>
    public interface ILockedTypeCollection :
        IControlledTypeCollection,
        IDisposable
    {
        /// <summary>
        /// Returns whether the current <see cref="ILockedTypeCollection"/>
        /// is disposed.
        /// </summary>
        bool IsDisposed { get; }
    }
}
