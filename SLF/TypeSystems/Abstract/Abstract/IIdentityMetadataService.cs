using AllenCopeland.Abstraction.Slf.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    public interface IIdentityMetadataService :
        IIdentityService
    {
        /// <summary>
        /// Returns the <see cref="IIdentityManager"/> associated to the
        /// <see cref="IIdentityMetadataService"/>.
        /// </summary>
        IIdentityManager IdentityManager { get; }
        /// <summary>
        /// Returns whether the <paramref name="metadatumType"/>
        /// is inheritable through the type hierarchy.
        /// </summary>
        /// <param name="metadatumType">The <see cref="IType"/>
        /// to check inheritability as a metadatum.</param>
        /// <returns>true if the <paramref name="metadatumType"/>
        /// is inheritable through the type hierarchy; false, otherwise.
        /// </returns>
        bool IsMetadatumInheritable(IType metadatumType);
        /// <summary>
        /// Returns whether the <paramref name="metadatumType"/>
        /// can be repeated on the target multiple times.
        /// </summary>
        /// <param name="metadatumType">The <see cref="IType"/> to check 
        /// for repeatability as a metadatum.</param>
        /// <returns>true, if the <paramref name="metadatumType"/> is able to be repeated
        /// on the target; false, otherwise.</returns>
        bool IsMetadatumRepeatable(IType metadatumType);
        /// <summary>
        /// Returns whether the <paramref name="metadatumType"/> is actually
        /// a metadatum, or whether it groups metadata since the type is an 
        /// interface.
        /// </summary>
        /// <param name="metadatumType">The <see cref="IType"/>
        /// to check for metadatum status.</param>
        /// <returns>A <see cref="TypeMetadatumRepresentation"/> instance which denotes
        /// the <paramref name="metadatumType"/>'s validity as proper metadata, whether
        /// it's purely an interface, or both.</returns>
        /// <remarks>Depending on the underlying infrastructure,
        /// the type may be an interface and a metadatum type.</remarks>
        TypeMetadatumRepresentation GetTypeMetadatumRepresentation(IType metadatumType);
        /// <summary>
        /// Returns a <see cref="MetadatumInfo"/> structure which denotes whether
        /// the <paramref name="metadatumType"/> is valid for metadata,
        /// whether it is inheritable through the type hierarchy, and whether
        /// multiple instances can be present on a target.
        /// </summary>
        /// <param name="metadatumType">The <see cref="IType"/>
        /// to check for metadatum status, inheritability, and repeatability.</param>
        /// <returns>A <see cref="MetadatumInfo"/> instance which denotes
        /// the <paramref name="metadatumType"/>'s validity as proper metadata, inheritability
        /// of the metadata, and whether it can be repeated.</returns>
        /// <remarks>Depending on the underlying infrastructure,
        /// the type may be an interface and a metadatum type.</remarks>
        MetadatumInfo GetMetadatumInfo(IType metadatumType);

    }
    /// <summary>
    /// Defines data related to a metadatum type and how it can potentially be used
    /// as portion of metadata for other declarations.
    /// </summary>
    public struct MetadatumInfo
    {
        public MetadatumInfo(TypeMetadatumRepresentation metadatumRepresentation, bool repeatable, bool inheritable, MetadatumTargets targets)
            : this()
        {
            this.MetadatumRepresentation = metadatumRepresentation;
            this.Repeatable = repeatable;
            this.Inheritable = inheritable;
            this.Targets = targets;
        }

        public TypeMetadatumRepresentation MetadatumRepresentation { get; private set; }
        public bool Repeatable { get; private set; }
        public bool Inheritable { get; private set; }
        public MetadatumTargets Targets { get; private set; }
    }
    /// <summary>
    /// Defines type representations relative to metadatum types.
    /// </summary>
    public enum TypeMetadatumRepresentation
    {
        /// <summary>
        /// The determination made was that the type involved is
        /// not a metadatum type.
        /// </summary>
        IsNotMetadata = 0x0,
        /// <summary>
        /// The determination made was that the type involved is 
        /// a metadatum type.
        /// </summary>
        IsMetadata = 0x1,
        /// <summary>
        /// The determination made was that the type involved is
        /// an interface and can potentially group multiple classes
        /// of metadata.
        /// </summary>
        GroupsMetadata = 0x2,
    }
}
