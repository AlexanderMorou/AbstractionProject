using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Events;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public interface IIntermediateDeclaration<TIdentifier> :
        IDeclaration<TIdentifier>,
        IIntermediateDeclaration
        where TIdentifier :
            IDeclarationUniqueIdentifier<TIdentifier>,
            IGeneralDeclarationUniqueIdentifier
    {
        /// <summary>
        /// Occurs after the 
        /// <see cref="IIntermediateDeclaration{TIdentifier}"/>
        /// has changed in a way which invalidates the previous unique
        /// identifier.
        /// </summary>
        new event EventHandler<DeclarationIdentifierChangeEventArgs<TIdentifier>> IdentifierChanged;
    }
    /// <summary>
    /// Defines properties and methods for working with an 
    /// intermediate declaration.
    /// </summary>
    public interface IIntermediateDeclaration :
        IDeclaration
    {
        /// <summary>
        /// Returns/sets the name of the <see cref="IIntermediateDeclaration"/>.
        /// </summary>
        new string Name { get; set; }
        /// <summary>
        /// Occurs after the <see cref="IIntermediateDeclaration"/> has been renamed.
        /// </summary>
        event EventHandler<DeclarationNameChangedEventArgs> Renamed;
        /// <summary>
        /// Occurs while the <see cref="IIntermediateDeclaration"/> is being renamed.
        /// </summary>
        event EventHandler<DeclarationRenamingEventArgs> Renaming;
        /// <summary>
        /// Occurs after the <see cref="IIntermediateDeclaration"/> has changed in a way
        /// which invalidates the previous unique identifier.
        /// </summary>
        event EventHandler<DeclarationIdentifierChangeEventArgs<IGeneralDeclarationUniqueIdentifier>> IdentifierChanged;
    }
}
