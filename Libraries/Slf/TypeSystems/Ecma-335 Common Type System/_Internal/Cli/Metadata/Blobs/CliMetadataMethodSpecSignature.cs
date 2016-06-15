using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataMethodSpecSignature :
        ICliMetadataMethodSpecSignature
    {
        public IControlledCollection<ICliMetadataTypeSignature> GenericParameters { get; private set; }
        public CliMetadataMethodSpecSignature(ICliMetadataTypeSignature[] genericParameters)
            : base()
        {
            if (genericParameters == null || genericParameters.Length == 0)
                this.GenericParameters = ArrayReadOnlyCollection<ICliMetadataTypeSignature>.Empty;
            else
                this.GenericParameters = new ArrayReadOnlyCollection<ICliMetadataTypeSignature>(genericParameters);
        }


        #region ICliMetadataSignature Members

        public SignatureKinds SignatureKind
        {
            get { return SignatureKinds.MethodSpec; }
        }

        #endregion
    }
}
