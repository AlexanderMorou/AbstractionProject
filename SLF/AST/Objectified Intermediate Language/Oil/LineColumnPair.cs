using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Provides source element with a line/column pairing for 
    /// localization within a source file.
    /// </summary>
    public struct LineColumnPair
    {
        /// <summary>
        /// Returns/sets the <see cref="Int32"/> value
        /// representing the line associated to the
        /// <see cref="LineColumnPair"/>.
        /// </summary>
        public int Line { get; set; }
        /// <summary>
        /// Returns/sets the <see cref="Int32"/> value
        /// representing the column associated to the
        /// <see cref="LineColumnPair"/>.
        /// </summary>
        public int Column { get; set; }
    }
}
