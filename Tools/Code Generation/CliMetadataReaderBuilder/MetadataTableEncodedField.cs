using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using AllenCopeland.Abstraction.OldCodeGen.Types.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;

namespace CliMetadataReader
{
    public class MetadataTableEncodedField<T> :
        MetadataTableField,
        IMetadataTableEncodedField
    {
        public string EncodingIdName { get; private set; }
        public MetadataTableEncodedField(string name, MetadataTableEncoding<T> encoding, string encodingIdName, string summary = null, string remarks = null)
            : base(name, encoding, summary, remarks)
        {
            this.EncodingIdName = encodingIdName;
        }

        public override string ToString()
        {
            if (DataGroupIndex != -1)
                return string.Format("{0} variedTable {1} => {3} {2}", this.DataGroupIndex, base.FieldName, this.EncodingIdName, this.DataType);
            return string.Format("variedTable {0} => {2} {1}", base.FieldName, this.EncodingIdName, this.DataType);
        }

        #region IMetadataTableEncodedField Members

        public IIntermediateClassFieldMember EncodedField { get; set; }

        public IIntermediateClassPropertyMember EncodingProperty { get; set; }

        #endregion
    }

    public class MetadataTableFilteredEncodedField<T> :
        MetadataTableEncodedField<T>,
        IMetadataTableFilteredEncodedField
    {
        public MetadataTableFilteredEncodedField(string name, MetadataTableEncoding<T> encoding, string encodingIdName, string filterSuffix, string summary = null, string remarks = null)
            : base(name, encoding, encodingIdName, summary, remarks)
        {
            this.FilteredSuffix = filterSuffix;
        }

        public string FilteredSuffix { get; set; }
    }
}
