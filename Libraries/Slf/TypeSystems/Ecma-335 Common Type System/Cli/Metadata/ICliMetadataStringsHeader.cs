using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    public interface ICliMetadataStringsHeap :
        ICliMetadataStreamHeader
    {
        string this[uint heapIndex] { get; }
    }
}
