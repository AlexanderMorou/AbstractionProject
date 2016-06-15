using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    /// <summary>
    /// Defines properties and methods for working with a type which
    /// contains pointer semantics.
    /// </summary>
    public interface ICliMetadataElementTypeAndModifiersSignature :
        ICliMetadataCustomModifierTypeSignature,
        ICliMetadataElementTypeSignature,
        ICliMetadataTypeSignature
    {
        
    }
}
