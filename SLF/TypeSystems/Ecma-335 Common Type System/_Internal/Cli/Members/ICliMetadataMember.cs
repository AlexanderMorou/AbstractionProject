using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal interface ICliMetadataMember
    {
        ICliMetadataTableRow MetadataEntry { get; }
    }
}
