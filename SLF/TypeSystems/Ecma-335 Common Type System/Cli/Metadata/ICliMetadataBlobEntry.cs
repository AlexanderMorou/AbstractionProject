using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    internal interface ICliMetadataBlobEntry
    {
        uint Index { get; }
        int Length { get; }
        byte LengthByteCount { get; }
        ICliMetadataSignature Signature { get; }
    }
}
