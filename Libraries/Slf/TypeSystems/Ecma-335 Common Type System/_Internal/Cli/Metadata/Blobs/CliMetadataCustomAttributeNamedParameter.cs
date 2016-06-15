using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataCustomAttributeNamedParameter :
        CliMetadataCustomAttributeParameter,
        ICliMetadataCustomAttributeNamedParameter
    {
        internal CliMetadataCustomAttributeNamedParameter(string name, NamedParameterTargetType target, CustomAttributeParameterValueType valueType, object value)
            : base(valueType, value)
        {
            this.Name = name;
            this.ParameterTarget = target;
        }

        #region ICliMetadataCustomAttributeNamedParameter Members

        public string Name { get; private set; }

        public NamedParameterTargetType ParameterTarget { get; private set; }

        #endregion
    }
}
