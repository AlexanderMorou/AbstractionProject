using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types;
using AllenCopeland.Abstraction.OldCodeGen.Types.Members;
using AllenCopeland.Abstraction.OldCodeGen;
namespace CliMetadataReader
{
    public class MetadataTableFieldHeapDataType :
        IMetadataTableFieldHeapDataType
    {
        public MetadataHeapTarget Heap { get; private set; }

        public MetadataTableFieldHeapDataType(MetadataHeapTarget target)
        {
            this.Heap = target;
        }

        public override string ToString()
        {
            return this.Heap.ToString();
        }

        #region IMetadataTableFieldDataType Members

        public FieldDataKind DataKind
        {
            get { return FieldDataKind.HeapIndex; }
        }

        #endregion


    }
}
