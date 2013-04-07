using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataCustomAttributeParameter :
        ICliMetadataCustomAttributeParameter
    {
        public CliMetadataCustomAttributeParameter(CustomAttributeParameterValueType valueType, object value)
        {
            this.ValueType = valueType;
            this.Value = value;
        }
        
        #region ICliMetadataCustomAttributeParameter Members

        public CustomAttributeParameterValueType ValueType { get; private set; }

        public object Value { get; private set; }

        #endregion
    }
}
