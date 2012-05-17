using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata
{
    internal interface _ICliMetadataBlobEntry :
        ICliMetadataBlobEntry
    {
        new ICliMetadataSignature Signature { get; set; }
        new byte[] BlobData { get; set; }
    }
}
