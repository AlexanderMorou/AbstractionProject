using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
//using AllenCopeland.Abstraction.OldCodeGen.Types.Members;
//using AllenCopeland.Abstraction.OldCodeGen.Types;

namespace CliMetadataReader
{
    public class MetadataTableStateMachineDataTypeInfo :
        ControlledCollection<MetadataTableField>
    {
        private int index;
        private MetadataTable table;
        private IMetadataTableFieldDataType dataType;

        public MetadataTableStateMachineDataTypeInfo(IMetadataTableFieldDataType dataType, MetadataTable table, int index)
        {
            this.dataType = dataType;
            this.table = table;
            this.index = index;
        }

        public void Add(MetadataTableField field)
        {
            field.DataGroupIndex = index;
            base.AddImpl(field);
        }

        public IMetadataTableFieldDataType DataType { get { return this.dataType; } }

        public int Index { get { return this.index; } }

    }
}
