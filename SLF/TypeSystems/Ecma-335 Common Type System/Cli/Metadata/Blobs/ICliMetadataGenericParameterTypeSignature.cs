using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    public interface ICliMetadataGenericParameterTypeSignature :
        ICliMetadataTypeSignature
    {
        /// <summary>
        /// Returns the <see cref="UInt32"/> value which denotes
        /// the index of the <see cref="ICliMetadataGenericParameterTypeSignature"/>
        /// within its declaration's set of generic parameters.
        /// </summary>
        uint Index { get; }
    }
}
