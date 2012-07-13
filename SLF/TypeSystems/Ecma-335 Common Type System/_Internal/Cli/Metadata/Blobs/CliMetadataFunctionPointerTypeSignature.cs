using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataFunctionPointerTypeSignature :
        ICliMetadataFunctionPointerTypeSignature
    {
        internal CliMetadataFunctionPointerTypeSignature(ICliMetadataMethodSignature signature)
        {
            this.Signature = signature;
        }

        //#region ICliMetadataFunctionPointerTypeSignature Members

        public ICliMetadataMethodSignature Signature { get; private set; }

        //#endregion

        #region ICliMetadataTypeSignature Members

        public CliMetadataTypeSignatureKind TypeSignatureKind
        {
            get { return CliMetadataTypeSignatureKind.FunctionPointerType; }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("({0})*", this.Signature);
        }
    }
}
