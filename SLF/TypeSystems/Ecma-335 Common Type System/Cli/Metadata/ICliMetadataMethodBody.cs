using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    /// <summary>
    /// Defines properties and methods for working with a method's body.
    /// </summary>
    public interface ICliMetadataMethodBody
    {
        ICliMetadataLocalVarSignature Locals { get; }
    }
}
