using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// <see cref="ITypeCollectionBase"/> which cannot be 
    /// modified.
    /// </summary>
    public interface ILockedTypeCollection :
        ITypeCollectionBase,
        IDisposable
    {
        /// <summary>
        /// Returns whether the current <see cref="ILockedTypeCollection"/>
        /// is disposed.
        /// </summary>
        bool IsDisposed { get; }
    }
}
