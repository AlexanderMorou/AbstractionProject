using System;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Provides a series of metadatum entries on a <see cref="IMetadataEntity"/>.
    /// </summary>
    public interface IMetadataCollection :
        IControlledCollection<IMetadatum>,
        IDisposable
    {
        /// <summary>
        /// Returns whether a metadatum of the
        /// <paramref name="metadatumType"/> provided is in the 
        /// <see cref="IMetadataCollection"/>.
        /// </summary>
        /// <param name="metadatumType">The type of attribute to check for.</param>
        /// <returns>true if an attribute of the <paramref name="metadatumType"/>
        /// provided exists; false, otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="metadatumType"/> is null.</exception>
        bool Contains(IType metadatumType);

        /// <summary>
        /// Returns the <see cref="IMetadataEntity"/> which contains
        /// the current <see cref="IMetadataCollection"/>.
        /// </summary>
        IMetadataEntity Parent { get; }

        /// <summary>
        /// Returns the <see cref="IMetadatum"/> of the
        /// <paramref name="metadatumType"/> provided.
        /// </summary>
        /// <param name="metadatumType">The <see cref="IType"/>
        /// of the metadatum to return the <see cref="IMetadatum"/>
        /// of.</param>
        /// <returns>The <see cref="IMetadatum"/> of the
        /// <paramref name="metadatumType"/> provided.</returns>
        IMetadatum this[IType metadatumType] {get;}
    }
}
