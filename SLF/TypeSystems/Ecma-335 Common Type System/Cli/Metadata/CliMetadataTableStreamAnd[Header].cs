using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.IO;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;
using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System.IO;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    public partial class CliMetadataTableStreamAndHeader 
    {
        //#region ICliMetadataStreamHeader Members

        /// <summary>
        /// Returns the <see cref="UInt32"/> value which denotes the
        /// start of the stream relative to the <see cref="CliMetadataRoot"/>.
        /// </summary>
        public uint Offset { get; private set; }

        /// <summary>
        /// Returns the <see cref="UInt32"/> value which denotes
        /// the size of the stream, in bytes.
        /// </summary>
        /// <remarks>Shall be a multiple of four (4.)</remarks>
        public uint Size { get; private set; }

        /// <summary>
        /// Returns the <see cref="String"/> value which denotes
        /// the name of the stream.
        /// </summary>
        public string Name { get; private set; }

        //#endregion
    }
}
