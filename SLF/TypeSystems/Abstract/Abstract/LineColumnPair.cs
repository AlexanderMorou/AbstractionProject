using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Provides source element with a line/column pairing for 
    /// localization within a source file.
    /// </summary>
    public struct LineColumnPair
    {
        /// <summary>
        /// Returns the <see cref="Int32"/> value
        /// representing the line associated to the
        /// <see cref="LineColumnPair"/>.
        /// </summary>
        public int Line { get; private set; }
        /// <summary>
        /// Returns/sets the <see cref="Int32"/> value
        /// representing the column associated to the
        /// <see cref="LineColumnPair"/>.
        /// </summary>
        public int Column { get; private set; }

        public LineColumnPair(int line, int column)
            : this()
        {
            this.Line = line;
            this.Column = column;
        }
    }
}
