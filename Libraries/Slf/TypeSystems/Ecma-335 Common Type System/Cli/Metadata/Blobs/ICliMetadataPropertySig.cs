using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    /// <summary>
    /// Defines properties and methods for working with the signature
    /// of a property.
    /// </summary>
    public interface ICliMetadataPropertySignature :
        ICliMetadataSignature
    {
        /// <summary>
        /// Returns whether the property is an instance member.
        /// </summary>
        bool Instance { get; }

        /// <summary>
        /// Returns the <see cref="IControlledCollection{T}"/> of
        /// the property's parameters.
        /// </summary>
        IControlledCollection<ICliMetadataParamSignature> Parameters { get; }

        IControlledCollection<ICliMetadataCustomModifierSignature> CustomModifiers { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataTypeSignature"/> which determines the 
        /// type of the property.
        /// </summary>
        ICliMetadataTypeSignature PropertyType { get; }
    }
}
