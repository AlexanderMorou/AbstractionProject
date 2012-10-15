using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    public interface ICliMetadataStandAloneVarArgMethodSignature :
        ICliMetadataStandAloneCommonMethodSignature,
        ICliMetadataMethodSignature
    {
        /// <summary>
        /// Returns the <see cref="ICliMetadataReturnTypeSignature"/>
        /// which denotes the signature's return type.
        /// </summary>
        ICliMetadataReturnTypeSignature ReturnType { get; }
        /// <summary>
        /// Returns the <see cref="IControlledCollection{T}"/> of the
        /// <see cref="ICliMetadataParamSignature"/> elements which 
        /// complete the signature.
        /// </summary>
        IControlledCollection<ICliMetadataVarArgParamSignature> Parameters { get; }

        CliMetadataMethodSigConventions CallingConvention { get; }
        /// <summary>
        /// Returns the <see cref="CliMetadataMethodSigFlags"/>
        /// </summary>
        CliMetadataMethodSigFlags Flags { get; }
    }
}
