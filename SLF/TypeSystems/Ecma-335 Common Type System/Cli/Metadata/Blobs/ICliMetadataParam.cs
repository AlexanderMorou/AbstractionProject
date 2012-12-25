using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    public interface ICliMetadataVarArgParamSignature :
        ICliMetadataParamSignature
    {
        /// <summary>
        /// Returns whether the <see cref="ICliMetadataVarArgParamSignature"/>
        /// is a variable argument in the defining signature.
        /// </summary>
        bool IsVariableArgument { get; }
    }

    public interface ICliMetadataParamSignature :
        ICliMetadataCustomModifierTypeSignature,
        ICliMetadataSignature
    {
        ICliMetadataTypeSignature ParameterType { get; }
    }
}
