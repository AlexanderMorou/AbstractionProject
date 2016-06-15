using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    public interface ICliMetadataCustomModifierSignature :
        ICliMetadataSignature
    {
        /// <summary>
        /// Returns whether the <see cref="ICliMetadataCustomModifierSignature"/>
        /// represents a required modifier.
        /// </summary>
        /// <remarks>Modifier is optional if false.</remarks>
        bool Required { get; }
        /// <summary>
        /// Returns the <see cref="ICliMetadataTypeDefOrRefRow"/> which is a typespec,
        /// type definition, or type reference which represents the
        /// <see cref="ICliMetadataCustomModifierSignature"/>.
        /// </summary>
        ICliMetadataTypeDefOrRefRow ModifierType { get; }
    }
}
