using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataVarArgParamSignature :
        CliMetadataParamSignature,
        ICliMetadataVarArgParamSignature
    {
        internal CliMetadataVarArgParamSignature(ICliMetadataTypeSignature parameterType, bool isVariableArgument, IEnumerable<ICliMetadataCustomModifierSignature> customModifiers)
            : base(parameterType, customModifiers)
        {
            this.IsVariableArgument = isVariableArgument;
        }

        //#region ICliMetadataVarArgParamSignature Members

        public bool IsVariableArgument { get; private set; }

        //#endregion
    }
}
