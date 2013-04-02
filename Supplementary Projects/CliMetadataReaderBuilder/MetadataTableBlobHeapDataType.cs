using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Expression;
using AllenCopeland.Abstraction.OldCodeGen.Types;
namespace CliMetadataReader
{
    public class MetadataTableBlobHeapDataType :
        IMetadataTableBlobHeapDataType
    {
        public MetadataTableBlobHeapDataType(IExpression signatureKind, ITypeReference signatureType)
        {
            this.SignatureKind = signatureKind;
            this.SignatureType = signatureType;
        }

        #region IMetadataTableFieldHeapDataType Members

        public MetadataHeapTarget Heap
        {
            get { return MetadataHeapTarget.BlobHeap; }
        }

        #endregion

        #region IMetadataTableFieldDataType Members

        public FieldDataKind DataKind
        {
            get { return FieldDataKind.HeapIndex; }
        }

        #endregion

        #region IMetadataTableBlobHeapDataType Members

        public IExpression SignatureKind { get; private set; }

        public ITypeReference SignatureType { get; private set; }

        #endregion
    }
}
