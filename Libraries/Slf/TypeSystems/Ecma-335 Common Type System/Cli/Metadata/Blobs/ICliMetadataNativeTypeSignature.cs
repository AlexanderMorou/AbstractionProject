using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    public interface ICliMetadataNativeTypeSignature :
        ICliMetadataTypeSignature
    {
        CliMetadataNativeTypes TypeKind { get; }
    }
}
