using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataMethodRefSignature :
        CliMetadataMethodSignature,
        ICliMetadataMethodRefSignature
    {
        private IReadOnlyCollection<ICliMetadataParamSignature> _parameters;
        private ArrayReadOnlyCollection<ICliMetadataVarArgParamSignature> parameters;
        public CliMetadataMethodRefSignature(CliMetadataMethodSigConventions convention, CliMetadataMethodSigFlags flags, ICliMetadataReturnTypeSignature returnType, ICliMetadataVarArgParamSignature[] parameters)
            : base(convention, flags, returnType)
        {
            if (parameters == null || parameters.Length == 0)
                this.parameters = ArrayReadOnlyCollection<ICliMetadataVarArgParamSignature>.Empty;
            else
                this.parameters = new ArrayReadOnlyCollection<ICliMetadataVarArgParamSignature>(parameters);
        }

        protected override IReadOnlyCollection<ICliMetadataParamSignature> OnGetParameters()
        {
            return this._Parameters;
        }

        //#region ICliMetadataMethodDefSignature Members

        public new IReadOnlyCollection<ICliMetadataVarArgParamSignature> Parameters { get { return this.parameters; } }

        //#endregion

        private IReadOnlyCollection<ICliMetadataParamSignature> _Parameters
        {
            get
            {
                if (this._parameters == null)
                    if (this.parameters.Count == 0)
                        this._parameters = ArrayReadOnlyCollection<ICliMetadataParamSignature>.Empty;
                    else
                        this._parameters = new ArrayReadOnlyCollection<ICliMetadataParamSignature>(parameters.Items.Cast<ICliMetadataParamSignature, ICliMetadataVarArgParamSignature>());
                return this._parameters;
            }
        }
    }
}
