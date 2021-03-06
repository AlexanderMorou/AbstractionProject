﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    public interface ICliMetadataMethodSignature :
        ICliMetadataStandAloneCommonMethodSignature,
        ICliMetadataMemberRefSignature
    {
        ICliMetadataReturnTypeSignature ReturnType { get; }
        /// <summary>
        /// Returns the <see cref="IControlledCollection{T}"/> of 
        /// <see cref="ICliMetadataParamSignature"/> elements
        /// associated to the signature defined.
        /// </summary>
        IControlledCollection<ICliMetadataParamSignature> Parameters { get; }

        CliMetadataMethodSigConventions CallingConvention { get; }
        /// <summary>
        /// Returns the <see cref="CliMetadataMethodSigFlags"/>
        /// </summary>
        CliMetadataMethodSigFlags Flags { get; }

    }
}
