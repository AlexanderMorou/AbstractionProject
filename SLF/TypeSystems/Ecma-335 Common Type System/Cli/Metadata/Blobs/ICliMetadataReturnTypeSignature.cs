using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    public interface ICliMetadataReturnTypeSignature :
        ICliMetadataCustomModifierTypeSignature,
        ICliMetadataTypeSignature
    {
        /// <summary>
        /// Returns the <see cref="ICliMetadataTypeSignature"/> which represents
        /// the return type.
        /// </summary>
        ICliMetadataTypeSignature ReturnType { get; }
    }
}
