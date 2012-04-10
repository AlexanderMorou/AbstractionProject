using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables
{
    public interface ICliMetadataTable :
        IControlledCollection,
        IDisposable
    {
        /// <summary>
        /// Returns the <see cref="CliMetadataTableKinds"/>
        /// value which denotes the kind of table represented
        /// by the <see cref="ICliMetadataTable{T}"/>.
        /// </summary>
        CliMetadataTableKinds Kind { get; }

        /// <summary>
        /// Reads the full table.
        /// </summary>
        void Read();

        /// <summary>
        /// Returns the <see cref="UInt32"/> value which denotes the number of rows
        /// expected within the <see cref="ICliMetadataTable"/>.
        /// </summary>
        uint RowCount { get; }
    }
}
