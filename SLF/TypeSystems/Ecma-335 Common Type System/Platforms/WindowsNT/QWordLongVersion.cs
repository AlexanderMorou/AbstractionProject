using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.IO;

namespace AllenCopeland.Abstraction.Slf.Platforms.WindowsNT
{
    public struct QWordLongVersion
    {
        private ushort majorVersion;
        private ushort minorVersion;
        private ushort buildNumber;
        private ushort revision;

        /// <summary>
        /// Creates a new <see cref="QWordLongVersion"/> with the
        /// <paramref name="majorVersion"/>, <paramref name="minorVersion"/>,
        /// <paramref name="buildNumber"/> and <paramref name="revision"/>.
        /// </summary>
        /// <param name="majorVersion">The <see cref="UInt16"/> value which denotes
        /// the major aspect of the version.</param>
        /// <param name="minorVersion">The <see cref="UInt16"/> value which denotes
        /// the build aspect of the version.</param>
        /// <param name="buildNumber">The <see cref="UInt16"/> value which denotes
        /// the build number of the version.</param>
        /// <param name="revision">The <see cref="UInt16"/> value which denotes
        /// the revision number of the version.</param>
        public QWordLongVersion(ushort majorVersion, ushort minorVersion, ushort buildNumber, ushort revision)
        {
            this.majorVersion = majorVersion;
            this.minorVersion = minorVersion;
            this.buildNumber = buildNumber;
            this.revision = revision;
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

        /// <summary>
        /// Returns the <see cref="UInt16"/> value which denotes
        /// the build number of the version.
        /// </summary>
        public ushort BuildNumber { get { return this.buildNumber; } }

        /// <summary>
        /// Returns the <see cref="UInt16"/> value which denotes
        /// the revision number of the version.
        /// </summary>
        public ushort RevisionNumber { get { return this.revision; } }


        internal void Read(EndianAwareBinaryReader reader)
        {
            this.majorVersion = reader.ReadUInt16();
            this.minorVersion = reader.ReadUInt16();
            this.buildNumber = reader.ReadUInt16();
            this.revision = reader.ReadUInt16();
        }

        internal void Write(EndianAwareBinaryWriter writer)
        {
            writer.Write(this.majorVersion);
            writer.Write(this.MinorVersion);
            writer.Write(this.buildNumber);
            writer.Write(this.revision);
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}.{2}.{3}", this.majorVersion,this.minorVersion,this.buildNumber,this.revision);
        }
    }
}
