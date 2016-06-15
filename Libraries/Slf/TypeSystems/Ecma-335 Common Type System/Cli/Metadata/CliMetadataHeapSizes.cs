using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    [Flags]
    public enum CliMetadataHeapSizes :
        byte
    {
        /// <summary>
        /// The #String heap needs more than 2<sup>16</sup> elements, 
        /// or greater than or equal to 65536 items.
        /// </summary>
        StringStream = 0x01,
        /// <summary>
        /// The #GUID heap needs more than 2^16 elements, 
        /// or greater than or equal to 65536 items.
        /// </summary>
        GuidStream = 0x02,
        /// <summary>
        /// The #Blob heap needs more than 2^16 elements, 
        /// or greater than or equal to 65536 items.
        /// </summary>
        BlobStream   = 0x04,
    }
}
