using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataStandAloneMethodSignature :
        ICliMetadataStandAloneMethodSignature
    {

        internal CliMetadataStandAloneMethodSignature(CliMetadataMethodSigConventions convention, CliMetadataMethodSigFlags flags, ICliMetadataReturnTypeSignature returnType, ICliMetadataVarArgParamSignature[] parameters)
        {
            this.CallingConvention = convention;
            this.Flags = flags;
            this.ReturnType = returnType;
            this.Parameters = new ReadOnlyCollection<ICliMetadataVarArgParamSignature>(parameters);
        }

        #region ICliMetadataStandAloneMethodSignature Members

        public ICliMetadataReturnTypeSignature ReturnType { get; private set; }

        public IReadOnlyCollection<ICliMetadataVarArgParamSignature> Parameters { get; private set; }

        public CliMetadataMethodSigConventions CallingConvention { get; private set; }

        public CliMetadataMethodSigFlags Flags { get; private set; }

        #endregion
        public override string ToString()
        {
            return string.Format("{0} ({1})", ReturnType, string.Join<ICliMetadataVarArgParamSignature>(", ", Parameters));
        }
    }
}
