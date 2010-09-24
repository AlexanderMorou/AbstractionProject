using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Parsers
{
    /// <summary>
    /// Provides a standard file locale used by tokens and rules.
    /// </summary>
    public struct FileLocale
    {
        /// <summary>
        /// Creates a new <see cref="FileLocale"/> instance with the 
        /// <paramref name="fileName"/>, <paramref name="location"/>,
        /// <paramref name="line"/> and <paramref name="column"/>
        /// a token is defined at.
        /// </summary>
        /// <param name="fileName">The <see cref="String"/> which denotes
        /// the file the <see cref="FileLocale"/> refers to.</param>
        /// <param name="location">The <see cref="UInt64"/> value indicating the exact
        /// point in the file the <see cref="FileLocale"/> refers to.</param>
        /// <param name="line">The <see cref="UInt32"/> value indicating the line
        /// the <see cref="FileLocale"/> refers to.</param>
        /// <param name="column">The <see cref="UInt32"/> value indicating what column
        /// the <see cref="FileLocale"/> refers to on the <paramref name="line"/> provided.</param>
        public FileLocale(string fileName, ulong location, uint line, uint column)
            : this()
        {
            this.FileName = fileName;
            this.Location = location;
            this.Line = line;
            this.Column = column;
        }

        #region IFileLocale Members

        /// <summary>
        /// Returns the <see cref="String"/> which denotes
        /// the file the <see cref="FileLocale"/> was defined in.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Returns the <see cref="UInt64"/> value indicating the exact
        /// point in the file the <see cref="FileLocale"/> is.
        /// </summary>
        public ulong Location { get; private set; }

        /// <summary>
        /// Returns the <see cref="UInt32"/> value indicating the line
        /// the <see cref="FileLocale"/> was defined at.
        /// </summary>
        public uint Line { get; private set; }

        /// <summary>
        /// Returns the <see cref="UInt32"/> value indicating what column
        /// the <see cref="FileLocale"/> is in the <see cref="Line"/>.
        /// </summary>
        public uint Column { get; private set; }

        #endregion
    }
}
