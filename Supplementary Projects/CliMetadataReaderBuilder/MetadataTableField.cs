using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types.Members;

namespace CliMetadataReader
{
    public enum MetadataTableFieldImportKind
    {
        None,
        /// <summary>
        /// The list exists as a one to many set of 
        /// sequential items, where the index of the first
        /// item is noted, and the number of items
        /// is relative to the difference between the
        /// index of the current item's first and the
        /// next row's first item index.
        /// </summary>
        OneToSequentialMany,
        /// <summary>
        /// The list exists as a one to many set of
        /// sequential items, where the index of the first
        /// item is noted, and the number of items is relative
        /// to the difference between the index of the 
        /// current item's first and the next row's
        /// first item index, which is imported as a set
        /// on the target field's table.
        /// </summary>
        OneToSequentialManyImported,
        ManyToOneImport,
        TableReference,
        EncodedReference,
    }
    public class MetadataTableField
    {
        private IMetadataTableFieldDataType dataType;
        private Func<IMetadataTableFieldDataType> dataTypeRetrieval;
        public string FieldName { get; private set; }
        public string Summary { get; set; }
        public IMetadataTableFieldDataType DataType
        {
            get
            {
                if (this.dataType == null)
                {
                    this.dataType = dataTypeRetrieval();
                    this.dataTypeRetrieval = null;
                }
                return this.dataType;
            }
            private set
            {
                this.dataType = value;
            }
        }

        public MetadataTableField(string fieldName, IMetadataTableFieldDataType dataType, string summary = null, string remarks = null)
        {
            this.Summary = summary;
            this.Remarks = remarks;
            this.FieldName = fieldName;
            this.DataType = dataType;
            this.DataGroupIndex = -1;
        }

        public MetadataTableField(string fieldName, Func<IMetadataTableFieldDataType> dataTypeRetr, string summary = null, string remarks = null)
        {
            this.Summary = summary;
            this.Remarks = remarks;
            this.FieldName = fieldName;
            this.dataTypeRetrieval = dataTypeRetr;
            this.DataGroupIndex = -1;
        }


        public override string ToString()
        {
            if (DataGroupIndex != -1)
                return string.Format("{0} {1} {2}", this.DataGroupIndex, this.DataType, this.FieldName);
            return string.Format("{0} {1}", this.DataType, this.FieldName);            
        }

        public string ImportName { get; set; }

        public MetadataTable TargetListTable { get; set; }

        public MetadataTableField TargetField { get; set; }

        public int DataGroupIndex { get; set; }

        public IFieldMember FieldReference { get; set; }

        public IPropertyMember PropertyReference { get; set; }

        public string Remarks { get; set; }

        public IPropertyMember PropertyIndexReference { get; set; }

        public MetadataTableFieldImportKind ImportType { get; set; }

        public MetadataTableFieldListSource SourceKind { get; set; }

        public string IndexSummary { get; set; }

        public string IndexRemarks { get; set; }

        public string ResultedListElementName { get; set; }

        public string ImportSummary { get; set; }

        public string ListRemarks { get; set; }

        public string ImportRemarks { get; set; }
    }
}
