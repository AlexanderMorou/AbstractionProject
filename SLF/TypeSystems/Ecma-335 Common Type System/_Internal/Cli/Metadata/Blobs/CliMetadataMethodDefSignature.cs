using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataMethodDefSignature :
        CliMetadataMethodSignature,
        ICliMetadataMethodDefSignature
    {
        public CliMetadataMethodDefSignature(CliMetadataMethodSigConventions convention, CliMetadataMethodSigFlags flags, ICliMetadataReturnTypeSignature returnType, ICliMetadataParamSignature[] parameters)
            : base(convention, flags, returnType)
        {
            if (parameters == null || parameters.Length == 0)
                this.Parameters = ArrayReadOnlyCollection<ICliMetadataParamSignature>.Empty;
            else
                this.Parameters = new ArrayReadOnlyCollection<ICliMetadataParamSignature>(parameters);
        }

        protected override IReadOnlyCollection<ICliMetadataParamSignature> OnGetParameters()
        {
            return this.Parameters;
        }

        //#region ICliMetadataMethodDefSignature Members

        public new IReadOnlyCollection<ICliMetadataParamSignature> Parameters { get; private set; }

        //#endregion
    }
}
