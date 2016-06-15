using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.IO;

namespace AllenCopeland.Abstraction.Slf.Platforms.WindowsNT
{
    /// <summary>
    /// Represents a four-byte version with a 
    /// major and minor aspect.
    /// </summary>
    public struct DWordVersion 
    {
        /// <summary>
        /// Data member for <see cref="MajorVersion"/>.
        /// </summary>
        private ushort majorVersion;
        /// <summary>
        /// Data member for <see cref="MinorVersion"/>.
        /// </summary>
        private ushort minorVersion;

        /// <summary>
        /// Creates a new <see cref="DWordVersion"/> with the
        /// <paramref name="majorVersion"/> and <paramref name="minorVersion"/>
        /// provided.
        /// </summary>
        /// <param name="majorVersion">The <see cref="UInt16"/> value which denotes the
        /// major aspect of the version.</param>
        /// <param name="minorVersion">The <see cref="UInt16"/> value which denotes the
        /// minor aspect of the version.</param>
        public DWordVersion(ushort majorVersion, ushort minorVersion)
        {
            this.majorVersion = majorVersion;
            this.minorVersion = minorVersion;
        }

        /// <summary>
        /// Returns the <see cref="UInt16"/> value which denotes
        /// the major aspect of the version.
        /// </summary>
        public ushort MajorVersion { get { return this.majorVersion; } }

        /// <summary>
        /// Returns the <see cref="UInt16"/> value which denotes
        /// the minor aspect of the version.
        /// </summary>
        public ushort MinorVersion { get { return this.minorVersion; } }

        public void Read(EndianAwareBinaryReader reader)
        {
            this.majorVersion = reader.ReadUInt16();
            this.minorVersion = reader.ReadUInt16();
        }

        public void Write(EndianAwareBinaryWriter writer)
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
