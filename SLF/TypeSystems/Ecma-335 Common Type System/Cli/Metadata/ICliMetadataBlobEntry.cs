using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    public interface ICliMetadataBlobEntry
    {
        int Length { get; }
        byte LengthByteCount { get; }
        ICliMetadataSignature Signature { get; }
        byte[] BlobData { get; }
    }
}
