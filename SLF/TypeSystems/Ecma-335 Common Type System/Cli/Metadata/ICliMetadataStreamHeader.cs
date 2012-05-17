using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    public interface ICliMetadataStreamHeader
    {
        /// <summary>
        /// Returns the <see cref="UInt32"/> value which denotes the
        /// start of the stream relative to the <see cref="ICliMetadataRoot"/>.
        /// </summary>
        uint Offset { get; }
        /// <summary>
        /// Returns the <see cref="UInt32"/> value which denotes
        /// the size of the stream, in bytes.
        /// </summary>
        /// <remarks>Shall be a multiple of four (4.)</remarks>
        uint Size { get; }
        /// <summary>
        /// Returns the <see cref="String"/> value which denotes
        /// the name of the stream.
        /// </summary>
        string Name { get; }
    }
}
