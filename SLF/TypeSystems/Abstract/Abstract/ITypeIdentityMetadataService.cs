using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    public interface ITypeIdentityMetadataService
    {
        /// <summary>
        /// Returns the <see cref="ITypeIdentityManager"/> associated to the
        /// <see cref="ITypeIdentityMetadataService"/>.
        /// </summary>
        ITypeIdentityManager IdentityManager { get; }
        /// <summary>
        /// Returns whether the <paramref name="metadatum"/>
        /// is inheritable through the type hierarchy.
        /// </summary>
        /// <param name="metadatum">The <see cref="IType"/>
        /// to check inheritability as a metadatum.</param>
        /// <returns>true if the <paramref name="metadatum"/>
        /// is inheritable through the type hierarchy; false, otherwise.
        /// </returns>
        bool IsMetadatumInheritable(IType metadatum);
        /// <summary>
        /// Returns whether the <paramref name="metadatum"/>
        /// can be repeated on the target multiple times.
        /// </summary>
        /// <param name="metadatum">The <see cref="IType"/> to check 
        /// for repeatability as a metadatum.</param>
        /// <returns>true, if the <paramref name="metadatum"/> is able to be repeated
        /// on the target; false, otherwise.</returns>
        bool IsMetadatumRepeatable(IType metadatum);
        /// <summary>
        /// Returns whether the <paramref name="metadatum"/> is actually
        /// a metadatum, or whether it's inconclusive since the type is an 
        /// interface.
        /// </summary>
        /// <param name="metadatum">The <see cref="IType"/>
        /// to check for metadatum status.</param>
        /// <returns>A <see cref="TypeIsMetadata"/> instance which denotes
        /// the <paramref name="metadatum"/>'s validity as proper metadata, whether
        /// it's purely an interface, or both.</returns>
        /// <remarks>Depending on the underlying infrastructure,
        /// the type may be an interface and a metadatum type.</remarks>
        TypeIsMetadata IsMetadatum(IType metadatum);
        /// <summary>
        /// Returns a <see cref="MetadatumInfo"/> structure which denotes whether
        /// the <paramref name="metadatum"/> is valid for metadata,
        /// whether it is inheritable through the type hierarchy, and whether
        /// multiple instances can be present on a target.
        /// </summary>
        /// <param name="metadatum">The <see cref="IType"/>
        /// to check for metadatum status, inheritability, and repeatability.</param>
        /// <returns>A <see cref="MetadatumInfo"/> instance which denotes
        /// the <paramref name="metadatum"/>'s validity as proper metadata, inheritability
        /// of the metadata, and whether it can be repeated.</returns>
        /// <remarks>Depending on the underlying infrastructure,
        /// the type may be an interface and a metadatum type.</remarks>
        MetadatumInfo GetMetadatumInfo(IType metadatum);

    }
    public struct MetadatumInfo
    {
        public TypeIsMetadata IsMetadatum { get; private set; }
        public bool Repeatable { get; private set; }
        public bool Inheritable { get; private set; }
    }
    public enum TypeIsMetadata
    {
        No = 0x0,
        Yes = 0x1,
        Interface = 0x2,
    }
}
