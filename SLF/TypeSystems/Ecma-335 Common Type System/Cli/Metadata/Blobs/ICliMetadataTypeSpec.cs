using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    public interface ICliMetadataTypeSpecSignature :
        ICliMetadataSignature
    {
        CliMetadataTypeSignatureKind TypeSignatureKind { get; }
    }
}
