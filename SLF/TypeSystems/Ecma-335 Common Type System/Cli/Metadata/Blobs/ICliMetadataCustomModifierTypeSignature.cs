using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    public interface ICliMetadataCustomModifierTypeSignature
    {
        /// <summary>
        /// Returns the <see cref="IReadOnlyCollection{T}"/> of the custom modifiers
        /// related to the <see cref="ICliMetadataCustomModifierTypeSignature"/>.
        /// </summary>
        IReadOnlyCollection<ICliMetadataCustomModifierSignature> CustomModifiers { get; }
    }
}
