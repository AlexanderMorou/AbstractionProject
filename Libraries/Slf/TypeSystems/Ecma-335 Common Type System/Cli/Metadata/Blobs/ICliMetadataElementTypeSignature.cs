using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    public interface ICliMetadataElementTypeSignature :
        ICliMetadataTypeSignature
    {
        /// <summary>
        /// Returns the <see cref="TypeElementClassification"/>
        /// which determines the kind of entity the wrapper
        /// <see cref="ICliMetadataElementTypeSignature"/> is.
        /// </summary>
        TypeElementClassification Classification { get; }
        ICliMetadataTypeSignature ElementType { get; }

    }
}
