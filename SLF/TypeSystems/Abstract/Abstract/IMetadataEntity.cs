/*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a declaration
    /// that contains custom attributes.
    /// </summary>
    public interface IMetadataEntity
    {
        /// <summary>
        /// Returns the <see cref="IMetadataCollection"/> 
        /// associated to the <see cref="IMetadataEntity"/>.
        /// </summary>
        IMetadataCollection Metadata { get; }
        /// <summary>
        /// Determines whether the <paramref name="metadatumType"/> 
        /// is defined on the current 
        /// <see cref="IMetadataEntity"/>.
        /// </summary>
        /// <param name="metadatumType">The <see cref="IType"/> of
        /// the attribute to check the presence of.</param>
        /// <returns>true if an attribute of the given 
        /// <paramref name="metadatumType"/> is defined
        /// on the current <see cref="IMetadataEntity"/>.
        /// </returns>
        bool IsDefined(IType metadatumType);
    }
}
