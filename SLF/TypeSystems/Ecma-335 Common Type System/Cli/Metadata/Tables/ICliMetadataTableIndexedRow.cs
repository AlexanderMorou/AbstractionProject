using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables
{
    public interface ICliMetadataIndexedRow :
        ICliMetadataTableRow
    {
        /// <summary>
        /// Returns the <see cref="UInt32"/> value which denotes
        /// where within the owning table the <see cref="ICliMetadataIndexedRow"/>
        /// is.
        /// </summary>
        uint Index { get; }
    }
}
