using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables
{
    /// <summary>
    /// Flags associated to a file associated to an
    /// assembly.
    /// </summary>
    public enum CliMetadataFileAttributes
    {
        ContainsMetadata = 0x00,
        ContainsNoMetadata = 0x01
    }
}
