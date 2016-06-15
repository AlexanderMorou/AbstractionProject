using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CliMetadataReader
{
    public interface IMetadataTableFieldHeapDataType :
        IMetadataTableFieldDataType
    {
        MetadataHeapTarget Heap { get; }
    }
}
