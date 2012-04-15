using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    /// <summary>
    /// Defines properties and methods for working with the root of an
    /// assembly or module's metadata targeting the common language 
    /// infrastructure.
    /// </summary>
    public interface ICliMetadataRoot
    {
        /// <summary>
        /// Returns the <see cref="PEImage"/> which ponts to the
        /// <see cref="ICliMetadataRoot"/>.
        /// </summary>
        PEImage BaseImage { get; }
        /// <summary>
        /// Returns the string embedded within the metadata
        /// which relates to the version of the target runtime.
        /// </summary>
        string MetadataVersion { get; }
        /// <summary>
        /// Returns the <see cref="ICliMetadataStringsHeap"/> which
        /// defines the strings of the assembly.
        /// </summary>
        /// <remarks>To save space, some string indices start within 
        /// the string itself, reusing the tail end of the string.
        /// </remarks>
        ICliMetadataStringsHeap StringsHeap { get; }

    }
}
