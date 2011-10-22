using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines generic properties and methods for working with the
    /// parts of a segmentable declaration.
    /// </summary>
    /// <typeparam name="TDeclaration">The type of declaration that needs
    /// segmentation functionality.</typeparam>
    public interface IIntermediateSegmentableDeclarationPartCollection<TIdentifier, TDeclaration> :
        IControlledStateCollection<TDeclaration>
        where TIdentifier :
            IDeclarationUniqueIdentifier<TIdentifier>
        where TDeclaration :
            IIntermediateSegmentableDeclaration<TIdentifier, TDeclaration>
    {
        /// <summary>
        /// Returns the <typeparamref name="TDeclaration"/> which
        /// owns the <see cref="IIntermediateSegmentableDeclarationPartCollection{TIdentifier, TDeclaration}"/>.
        /// </summary>
        /// <remarks>The <see cref="Root"/> is not a part of the 
        /// <see cref="IIntermediateSegmentableDeclarationPartCollection{TIdentifier, TDeclaration}"/>.</remarks>
        TDeclaration Root { get; }
        /// <summary>
        /// Adds a <typeparamref name="TDeclaration"/> part to the
        /// current <see cref="IIntermediateSegmentableDeclarationPartCollection{TIdentifier, TDeclaration}"/>.
        /// </summary>
        /// <returns>A new <typeparamref name="TDeclaration"/> instance
        /// if successful.</returns>
        TDeclaration Add();
        /// <summary>
        /// Adds a <typeparamref name="TDeclaration"/> <paramref name="part"/> to the
        /// current <see cref="IIntermediateSegmentableDeclarationPartCollection{TIdentifier, TDeclaration}"/>.
        /// </summary>
        /// <param name="part">A <typeparamref name="TDeclaration"/>
        /// instance as a part of the <see cref="Root"/> <typeparamref name="TDeclaration"/>.</param>
        /// <exception cref="System.ArgumentException">The 
        /// <paramref name="part"/> provided is its own root or is not a part of the
        /// <see cref="Root"/> instance.</exception>
        void Add(TDeclaration part);
    }

    /// <summary>
    /// Defines properties and methods for working with the parts of a
    /// segmentable declaration.
    /// </summary>
    public interface IIntermediateSegmentableDeclarationPartCollection :
        IControlledStateCollection
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateSegmentableDeclaration"/> which
        /// owns the <see cref="IIntermediateSegmentableDeclarationPartCollection"/>.
        /// </summary>
        /// <remarks>The <see cref="Root"/> is not a part of the 
        /// <see cref="IIntermediateSegmentableDeclarationPartCollection"/>.</remarks>
        IIntermediateSegmentableDeclaration Root { get; }
        /// <summary>
        /// Adds a <see cref="IIntermediateSegmentableDeclaration"/> part to the
        /// current <see cref="IIntermediateSegmentableDeclarationPartCollection"/>.
        /// </summary>
        /// <returns>A new <see cref="IIntermediateSegmentableDeclaration"/> instance
        /// if successful.</returns>
        IIntermediateSegmentableDeclaration Add();
        /// <summary>
        /// Adds a <see cref="IIntermediateSegmentableDeclaration"/> <paramref name="part"/> to the
        /// current <see cref="IIntermediateSegmentableDeclarationPartCollection"/>.
        /// </summary>
        /// <param name="part">A <see cref="IIntermediateSegmentableDeclaration"/>
        /// instance as a part of the <see cref="Root"/> 
        /// <see cref="IIntermediateSegmentableDeclaration"/>.</param>
        /// <exception cref="System.ArgumentException">the type of <paramref name="part"/> does not
        /// match the underlying type of the segmentable declaration; -or- the 
        /// <paramref name="part"/> provided is its own root or is not a part of the
        /// <see cref="Root"/> instance.</exception>
        void Add(IIntermediateSegmentableDeclaration part);
    }
}
