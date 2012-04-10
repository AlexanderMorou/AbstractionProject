using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables
{
    /// <summary>
    /// Determines the size of reference index
    /// read from the metadata stream.
    /// </summary>
    public enum CliMetadataReferenceIndexSize
    {
        /// <summary>
        /// The reference indexes are word-sized or two (2)
        /// bytes.
        /// </summary>
        Word = 2,
        /// <summary>
        /// The reference indexes are double word-sized or
        /// four (4) bytes.
        /// </summary>
        DWord = 4,
    }
}
