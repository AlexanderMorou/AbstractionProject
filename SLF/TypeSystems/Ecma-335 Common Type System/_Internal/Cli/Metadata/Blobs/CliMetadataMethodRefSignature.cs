using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataMethodRefSignature :
        CliMetadataMethodSignature,
        ICliMetadataMethodRefSignature
    {
        public CliMetadataMethodRefSignature(CliMetadataMethodSigConventions convention, CliMetadataMethodSigFlags flags, ICliMetadataReturnTypeSignature returnType, IEnumerable<ICliMetadataVarArgParamSignature> parameters)
            : base(convention, flags,returnType)
        {
            this.Parameters = new ReadOnlyCollection<ICliMetadataVarArgParamSignature>(parameters.ToArray());
        }

        protected override IEnumerable<ICliMetadataParamSignature> OnGetParameters()
        {
            return this.Parameters;
        }

        //#region ICliMetadataMethodDefSignature Members

        public new IReadOnlyCollection<ICliMetadataVarArgParamSignature> Parameters { get; private set; }

        //#endregion
    }
}
