using System;
/*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a declaration.
    /// </summary>
    public interface IDeclaration<TIdentifier> :
        IDeclaration
        where TIdentifier :
            IDeclarationUniqueIdentifier<TIdentifier>
    {
        /// <summary>
        /// Returns the unique identifier for the current <see cref="IDeclaration{TIdentifier}"/> where 
        /// <see cref="Name"/> is not enough to distinguish between two <see cref="IDeclaration{TIdentifier}"/> entities.
        /// </summary>
        new TIdentifier UniqueIdentifier { get; }
    }
    public interface IDeclaration :
        IDisposable
    {
        /// <summary>
        /// Returns the name of the <see cref="IDeclaration"/>.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Returns the unique identifier for the current
        /// <see cref="IDeclaration"/> in its general case form.
        /// </summary>
        IGeneralDeclarationUniqueIdentifier UniqueIdentifier { get; }
        /// <summary>
        /// Invoked when the <see cref="IDeclaration"/> is disposed.
        /// </summary>
        event EventHandler Disposed;
    }
}
