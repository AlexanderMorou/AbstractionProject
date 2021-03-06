﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    /// <summary>
    /// Defines properties and methods for working with
    /// a method reference signature.
    /// </summary>
    public interface ICliMetadataMethodRefSignature :
        ICliMetadataMethodSignature,
        ICliMetadataMemberRefSignature
    {
        /// <summary>
        /// Returns the <see cref="IControlledCollection{T}"/> of 
        /// <see cref="ICliMetadataVarArgParamSignature"/> elements
        /// associated to the signature defined.
        /// </summary>
        IControlledCollection<ICliMetadataVarArgParamSignature> Parameters { get; }
    }
}
