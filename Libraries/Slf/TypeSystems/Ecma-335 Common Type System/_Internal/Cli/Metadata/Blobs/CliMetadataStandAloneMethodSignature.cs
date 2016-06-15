using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataStandAloneMethodSignature :
        ICliMetadataStandAloneVarArgMethodSignature
    {
        private IControlledCollection<ICliMetadataParamSignature> _parameters;
        private ArrayReadOnlyCollection<ICliMetadataVarArgParamSignature> parameters;
        internal CliMetadataStandAloneMethodSignature(CliMetadataMethodSigConventions convention, CliMetadataMethodSigFlags flags, ICliMetadataReturnTypeSignature returnType, ICliMetadataVarArgParamSignature[] parameters)
        {
            this.CallingConvention = convention;
            this.Flags = flags;
            this.ReturnType = returnType;
            if (parameters == null || parameters.Length == 0)
                this.parameters = ArrayReadOnlyCollection<ICliMetadataVarArgParamSignature>.Empty;
            else
                this.parameters = new ArrayReadOnlyCollection<ICliMetadataVarArgParamSignature>(parameters);
        }

        //#region ICliMetadataStandAloneVarArgMethodSignature Members

        public ICliMetadataReturnTypeSignature ReturnType { get; private set; }

        public IControlledCollection<ICliMetadataVarArgParamSignature> Parameters { get { return this.parameters; } }

        public CliMetadataMethodSigConventions CallingConvention { get; private set; }

        public CliMetadataMethodSigFlags Flags { get; private set; }

        //#endregion
        public override string ToString()
        {
            return string.Format("{0} ({1})", ReturnType, string.Join<ICliMetadataVarArgParamSignature>(", ", Parameters));
        }

        //#region ICliMetadataMethodSignature Members


        IControlledCollection<ICliMetadataParamSignature> ICliMetadataMethodSignature.Parameters
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

        //#endregion

        #region ICliMetadataSignature Members

        public SignatureKinds SignatureKind
        {
            get { return SignatureKinds.StandaloneLocalVarSig; }
        }

        #endregion
    }
}
