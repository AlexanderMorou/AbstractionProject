using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables
{
    public interface ICliMetadataTableSubset<T> :
        IReadOnlyCollection<T>
        where T :
            class,
            ICliMetadataTableRow
    {
        /// <summary>
        /// Returns the <see cref="IEnumerable{T}"/> of 
        /// <see cref="UInt32"/> values relative to the
        /// indices of the elements represented by the
        /// <see cref="ICliMetadataTableSubset{T}"/>.
        /// </summary>
        IEnumerable<uint> SubsetIndices { get; }
        /// <summary>
        /// Returns the <see cref="CliMetadataTableKinds"/>
        /// which denotes which table the subset derives.
        /// </summary>
        CliMetadataTableKinds SubsetIndexType { get; }
    }
}
