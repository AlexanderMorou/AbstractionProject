using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    /// <summary>
    /// Defines properties and methods for working with a signature which
    /// defines the shape of an array.
    /// </summary>
    public interface ICliMetadataArrayShapeSignature :
        ICliMetadataSignature
    {
        uint Rank { get; }
        IReadOnlyCollection<uint> Sizes { get; }
        IReadOnlyCollection<uint> LowerBounds { get; }
    }
}
