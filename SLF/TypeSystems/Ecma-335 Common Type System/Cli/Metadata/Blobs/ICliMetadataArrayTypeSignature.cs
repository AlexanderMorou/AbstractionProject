using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    public interface ICliMetadataArrayTypeSignature :
        ICliMetadataElementTypeSignature
    {
        /// <summary>
        /// Returns the <see cref="ICliMetadataArrayShapeSignature"/> which denotes
        /// the shape of the array.
        /// </summary>
        ICliMetadataArrayShapeSignature Shape { get; }
    }
}
