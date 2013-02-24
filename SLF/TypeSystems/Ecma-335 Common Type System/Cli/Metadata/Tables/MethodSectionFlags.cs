using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables
{
    /// <summary>
    /// Provides a series of values which represent information about a section
    /// within a method header.
    /// </summary>
    [Flags]
    public enum MethodHeaderSectionFlags :
        byte
    {
        /// <summary>
        /// The section represents an exception handler table.
        /// </summary>
        ExceptionHandlerTable = 0x01,
        /// <summary>
        /// The section represents optimized IL.
        /// </summary>
        OptIL                 = 0x02,
        /// <summary>
        /// The data represented within the section is in the fat format.
        /// </summary>
        FatFormat             = 0x40,
        /// <summary>
        /// There are more sections following the current section.
        /// </summary>
        ContainsMoreSections          = 0x80,
    }
}
