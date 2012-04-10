using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    public enum CliMetadataGenericParameterParent
    {
        /// <summary>
        /// The parent of the generic parameter is a type.
        /// </summary>
        Type,
        /// <summary>
        /// The parent of the generic parameter is a method.
        /// </summary>
        Method,
    }
    public interface ICliMetadataGenericParameterSignature :
        ICliMetadataTypeSignature
    {
        /// <summary>
        /// Returns the <see cref="CliMetadataGenericParameterParent"/>
        /// which denotes whether the owner is a type
        /// or a method.
        /// </summary>
        CliMetadataGenericParameterParent Parent { get; }
        /// <summary>
        /// Returns the <see cref="UInt32"/> value which denotes
        /// the ordinal index of the generic parameter represented by the
        /// <see cref="ICliMetadataGenericParameter"/>
        /// </summary>
        uint Position { get; }
    }
}
