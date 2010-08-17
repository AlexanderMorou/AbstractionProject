using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a declaration.
    /// </summary>
    public interface IDeclaration :
        IDisposable
    {
        /// <summary>
        /// Returns the name of the <see cref="IDeclaration"/>.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Returns the unique identifier for the current <see cref="IDeclaration"/> where 
        /// <see cref="Name"/> is not enough to distinguish between two <see cref="IDeclaration"/> entities.
        /// </summary>
        string UniqueIdentifier { get; }
        /// <summary>
        /// Invoked when the <see cref="IDeclaration"/> is disposed.
        /// </summary>
        event EventHandler Disposed;
    }
}
