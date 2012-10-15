using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    public enum CliMetadataLocalVarEntryKind
    {
        Full,
        TypedReference,
    }

    public interface ICliMetadataLocalVarFullEntrySignature :
        ICliMetadataLocalVarEntrySignature
    {
        /// <summary>
        /// Returns the <see cref="IControlledCollection{T}"/> of 
        /// <see cref="ICliMetadataCustomModifierSignature"/> elements
        /// which represent the modifiers on the type of the local variable represented by
        /// the <see cref="ICliMetadataLocalVarFullEntrySignature"/>
        /// </summary>
        IControlledCollection<ICliMetadataCustomModifierSignature> CustomModifiers { get; }
        /// <summary>
        /// Returns the <see cref="ICliMetadataTypeSignature"/>
        /// associated to the local variable.
        /// </summary>
        ICliMetadataTypeSignature LocalType { get; }
        /// <summary>
        /// Returns whether the local variable represented is pinned.
        /// </summary>
        bool IsPinned { get; }
    }

    public interface ICliMetadataLocalVarEntrySignature
    {
        CliMetadataLocalVarEntryKind VariableKind { get; }
    }
}
