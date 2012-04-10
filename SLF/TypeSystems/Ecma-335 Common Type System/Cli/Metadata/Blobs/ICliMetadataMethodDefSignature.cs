using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    /// <summary>
    /// Defines properties and methods for working with a method
    /// definition signature.
    /// </summary>
    public interface ICliMetadataMethodDefSignature :
        ICliMetadataMethodSignature
    {
        /// <summary>
        /// Returns the <see cref="IReadOnlyCollection{T}"/> of 
        /// <see cref="ICliMetadataParamSignature"/> elements
        /// associated to the signature defined.
        /// </summary>
        IReadOnlyCollection<ICliMetadataParamSignature> Parameters { get; }
    }
}
