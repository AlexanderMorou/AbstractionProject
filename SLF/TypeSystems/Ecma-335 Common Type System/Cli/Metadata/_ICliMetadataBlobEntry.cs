using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    public interface _ICliMetadataBlobEntry :
        ICliMetadataBlobEntry
    {
        new ICliMetadataSignature Signature { get; set; }
        new byte[] BlobData { get; set; }
    }
}
