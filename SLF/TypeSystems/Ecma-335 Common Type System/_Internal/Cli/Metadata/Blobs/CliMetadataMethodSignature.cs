using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal abstract class CliMetadataMethodSignature :
        ICliMetadataMethodSignature
    {
        protected CliMetadataMethodSignature(CliMetadataMethodSigConventions convention, CliMetadataMethodSigFlags flags, ICliMetadataReturnTypeSignature returnType)
        {
            this.ReturnType = returnType;
            this.CallingConvention = convention;
            this.Flags = flags;
        }

        //#region ICliMetadataMethodSignature Members

        public ICliMetadataReturnTypeSignature ReturnType { get; private set; }

        public IControlledCollection<ICliMetadataParamSignature> Parameters
        {
            get
            {
                return this.OnGetParameters();
            }
        }

        protected abstract IControlledCollection<ICliMetadataParamSignature> OnGetParameters();

        //#endregion

        public CliMetadataMethodSigConventions CallingConvention { get; private set; }

        public CliMetadataMethodSigFlags Flags { get; private set; }

        public override string ToString()
        {
            return string.Format("{0} ({1})", this.ReturnType, string.Join<ICliMetadataParamSignature>(", ", this.Parameters));
        }

    }
}
