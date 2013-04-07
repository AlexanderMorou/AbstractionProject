using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataCustomAttribute :
        ICliMetadataCustomAttribute
    {
        public CliMetadataCustomAttribute(ICliMetadataCustomAttributeParameter[] fixedParameters, ICliMetadataCustomAttributeNamedParameter[] namedParameters) 
        {
            if (fixedParameters == null ||
                fixedParameters.Length == 0)
                this.FixedParameters = ArrayReadOnlyCollection<ICliMetadataCustomAttributeParameter>.Empty;
            else
                this.FixedParameters = new ArrayReadOnlyCollection<ICliMetadataCustomAttributeParameter>(fixedParameters);
            if (namedParameters == null ||
                namedParameters.Length == 0)
                this.NamedParameters = ArrayReadOnlyCollection<ICliMetadataCustomAttributeNamedParameter>.Empty;
            else
                this.NamedParameters = new ArrayReadOnlyCollection<ICliMetadataCustomAttributeNamedParameter>(namedParameters);
        }


        #region ICliMetadataCustomAttribute Members

        public IControlledCollection<ICliMetadataCustomAttributeParameter> FixedParameters { get; private set; }

        public IControlledCollection<ICliMetadataCustomAttributeNamedParameter> NamedParameters { get; private set; }

        #endregion

        #region ICliMetadataSignature Members

        public SignatureKinds SignatureKind
        {
            get { return SignatureKinds.CustomAttribute; }
        }

        #endregion
    }
}
