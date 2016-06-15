using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Numerics;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
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
    public struct LineColumnPair :
        IEquatable<LineColumnPair>
    {
        public static LineColumnPair Zero = new LineColumnPair();
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

        /// <summary>
        /// Creates a new instance of a <see cref="LineColumnPair"/>
        /// with the <paramref name="line"/> and <paramref name="column"/>
        /// provided.
        /// </summary>
        /// <param name="line">The 1-indexed <see cref="Int32"/> value denoting
        /// how many line breaks into the file the
        /// <see cref="LineColumnPair"/> points to.</param>
        /// <param name="column">The <see cref="Int32"/> value representing
        /// the number of characters from the last line-break or
        /// start of the file the <see cref="LineColumnPair"/> refers to.</param>
        public LineColumnPair(int line, int column)
            : this()
        {
            this.Line = line;
            this.Column = column;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", this.Line, this.Column);
        }

        #region IEquatable<LineColumnPair> Members

        public bool Equals(LineColumnPair other)
        {
            return this.Column == other.Column &&
                   this.Line == other.Line;
        }

        #endregion

        public override int GetHashCode()
        {
            return this.Line.GetHashCode() ^ (this.Column.GetHashCode().RotL(20));
        }

        public override bool Equals(object obj)
        {
            if (obj is LineColumnPair)
                return this.Equals((LineColumnPair)obj);
            return false;
        }

        public static bool operator ==(LineColumnPair left, LineColumnPair right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LineColumnPair left, LineColumnPair right)
        {
            return !left.Equals(right);
        }
    }
}
