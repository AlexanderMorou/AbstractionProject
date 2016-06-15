using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.IO;
using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata
{
    partial class CliMetadataTableStreamAndHeader
    {
        //#region ICliMetadataStreamHeader Members

        /// <summary>
        /// Returns the <see cref="UInt32"/> value which denotes the
        /// start of the stream relative to the <see cref="CliMetadataFixedRoot"/>.
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

        public override string ToString()
        {
            return string.Format("{0} section - {1} bytes at {2} - {3} tables", this.Name, this.Size, this.Offset, this.Count);
        }

        #region ICliMetadataTableStreamAndHeader Members

        public CliMetadataTableKinds TablesPresent
        {
            get { return this.tablesPresent; }
        }

        public CliMetadataTableKinds SortedTables
        {
            get { return this.sortedTables; }
        }

        #endregion
    }
}
