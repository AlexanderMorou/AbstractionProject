using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.IO;

namespace AllenCopeland.Abstraction.Slf.Platforms.WindowsNT
{
    /// <summary>
    /// Represents a two-byte version with a 
    /// major and minor aspect.
    /// </summary>
    public struct WordVersion
    {
        /// <summary>
        /// Data member for <see cref="MajorVersion"/>.
        /// </summary>
        private byte majorVersion;
        /// <summary>
        /// Data member for <see cref="MinorVersion"/>.
        /// </summary>
        private byte minorVersion;

        /// <summary>
        /// Creates a new <see cref="WordVersion"/> with the
        /// <paramref name="majorVersion"/> and <paramref name="minorVersion"/>
        /// provided.
        /// </summary>
        /// <param name="majorVersion">The <see cref="Byte"/> value which denotes the
        /// major aspect of the version.</param>
        /// <param name="minorVersion">The <see cref="Byte"/> value which denotes the
        /// minor aspect of the version.</param>
        public WordVersion(byte majorVersion, byte minorVersion)
        {
            this.majorVersion = majorVersion;
            this.minorVersion = minorVersion;
        }

        /// <summary>
        /// Returns the <see cref="Byte"/> value which denotes
        /// the major aspect of the version.
        /// </summary>
        public byte MajorVersion { get { return this.majorVersion; } }

        /// <summary>
        /// Returns the <see cref="Byte"/> value which denotes
        /// the minor aspect of the version.
        /// </summary>
        public byte MinorVersion { get { return this.minorVersion; } }

        internal void Read(EndianAwareBinaryReader reader)
        {
            this.majorVersion = reader.ReadByte();
            this.minorVersion = reader.ReadByte();
        }

        internal void Write(EndianAwareBinaryWriter writer)
        {
            writer.Write(this.majorVersion);
            writer.Write(this.MinorVersion);
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}", this.MajorVersion, this.MinorVersion);
        }
    }
}
