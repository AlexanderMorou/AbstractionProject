using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    public interface ICliMetadataFieldSignature :
        ICliMetadataSignature,
        ICliMetadataCustomModifierTypeSignature,
        ICliMetadataStandaloneSignature
    {
        /// <summary>
        /// Returns the <see cref="ICliMetadataTypeSignature"/> associated
        /// to the signature.
        /// </summary>
        ICliMetadataTypeSignature Type { get; }
        /// <summary>
        /// Returns the unusual case where the field is pinned.
        /// </summary>
        bool IsPinned { get; }
    }
}
