using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    /// <summary>
    /// Defines properties and methods for working with a type signature
    /// which points to a method signature.
    /// </summary>
    public interface ICliMetadataFunctionPointerTypeSignature :
        ICliMetadataTypeSignature
    {
        /// <summary>
        /// Returns the <see cref="ICliMetadataMethodSignature"/> which is encapsulated by the
        /// <see cref="ICliMetadataFunctionPointerTypeSignature"/>
        /// </summary>
        ICliMetadataMethodSignature Signature { get; }
    }
}
