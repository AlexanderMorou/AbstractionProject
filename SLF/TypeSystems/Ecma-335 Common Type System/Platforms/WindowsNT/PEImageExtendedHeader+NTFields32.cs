using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using AllenCopeland.Abstraction.IO;

namespace AllenCopeland.Abstraction.Slf.Platforms.WindowsNT
{
    partial struct PEImageExtendedHeader
    {
        /* *
         * http://webster.cs.ucr.edu/Page_TechDocs/pe.txt
         * */
        private struct NTFields32
        {
            private uint imageBase;

            private uint sectionAlignment;

            private uint fileAlignment;

            private DWordVersion osVersion;

            private DWordVersion binaryVersion;

            private DWordVersion subsystemVersion;

            private uint reserved;

            private uint imageSize;

            private uint headerSize;

            private uint fileChecksum;

            private PEImageSubsystem subsystem;

            private PEImageDllCharacteristics dllCharacteristics;

            private uint stackReserveSize;

            private uint stackCommitSize;

            private uint heapReserveSize;

            private uint heapCommitSize;

            private uint loaderFlags;

            private uint dataDirectoryCount;

            public const int DefaultDataDirectoryCount = 0x10;

            /// <summary>
            /// Creates a new <see cref="NTFields32"/>
            /// instance with the <paramref name="imageBase"/>
            /// <paramref name="sectionAlignment"/>, <paramref name="fileAlignment"/>,
            /// <paramref name="osMajor"/>, <paramref name="osMinor"/>, <paramref name="userMajor"/>,
            /// <paramref name="userMinor"/>, <paramref name="subSysMajor"/>, 
            /// <paramref name="subSysMinor"/>, <paramref name="imageSize"/>,
            /// <paramref name="headerSize"/>,
            /// <paramref name="fileChecksum"/>, <paramref name="dllCharacteristics"/>,
            /// <paramref name="stackReserveSize"/>, <paramref name="stackCommitSize"/>, 
            /// <paramref name="heapReserveSize"/>, <paramref name="heapCommitSize"/>,
            /// <paramref name="loaderFlags"/>, <paramref name="dataDirectoryCountand"/>
            /// and <paramref name="subSystem"/>
            /// provided.
            /// </summary>
            /// <param name="imageBase">The preferred load address of the 
            /// <see cref="PEImage"/>.</param>
            /// <param name="sectionAlignment">The <see cref="UInt32"/> value
            /// used to align the section data in memory when the
            /// <see cref="PEImage"/> is loaded into memory.</param>
            /// <param name="fileAlignment">The <see cref="UInt32"/>
            /// value used to align the section data within the file itself,
            /// used to calculate where the individual sections are.
            /// </param>
            /// <param name="osMajor">The <see cref="UInt16"/> value determining 
            /// the operating system's major version the <see cref="PEImage"/>
            /// targets.</param>
            /// <param name="osMinor">the <see cref="UInt16"/> value determining 
            /// the operating system's minor version the <see cref="PEImage"/>
            /// targets.</param>
            /// <param name="userMajor">The <see cref="UInt16"/> value which denotes the
            /// major part of the <see cref="PEImage"/> version.</param>
            /// <param name="userMinor">The <see cref="UInt16"/> value which denotes the
            /// minor part of the <see cref="PEImage"/> version.</param>
            /// <param name="subSysMajor">The <see cref="UInt16"/> value which denotes the
            /// major version of the <paramref name="subsystem"/> the
            /// <see cref="PEImage"/> targets.</param>
            /// <param name="subSysMinor">The <see cref="UInt16"/> value which denotes the
            /// minor version of the <paramref name="subsystem"/> the
            /// <see cref="PEImage"/> targets.</param>
            /// <param name="imageSize">The <see cref="UInt32"/> value which determines the size,
            /// in bytes, of the <see cref="PEImage"/> including all headers,
            /// and padding.</param>
            /// <param name="headerSize">The <see cref="UInt32"/> value which notes the
            /// size of all headers defined within the <see cref="PEImage"/>.</param>
            /// <param name="fileChecksum">The <see cref="UInt32"/> value which notes the
            /// checksum hash of the file to determine whether it has
            /// been damaged (usually zero).</param>
            /// <param name="subsystem">The <see cref="PEImageSubsystem"/> which
            /// the <see cref="PEImage"/>
            /// targets.</param>
            public NTFields32(
                uint imageBase, uint sectionAlignment, uint fileAlignment, DWordVersion osVersion,
                DWordVersion binaryVersion, DWordVersion subsystemVersion, uint imageSize,
                uint headerSize, uint fileChecksum, PEImageSubsystem subsystem,
                PEImageDllCharacteristics dllCharacteristics, uint stackReserveSize, uint stackCommitSize,
                uint heapReserveSize, uint heapCommitSize, uint dataDirectoryCount = DefaultDataDirectoryCount)
            {
                this.imageBase = imageBase;
                this.sectionAlignment = sectionAlignment;
                this.fileAlignment = fileAlignment;
                this.osVersion = osVersion;
                this.binaryVersion = binaryVersion;
                this.subsystemVersion = subsystemVersion;
                this.reserved = 0;
                this.imageSize = imageSize;
                this.headerSize = headerSize;
                this.fileChecksum = fileChecksum;
                this.subsystem = subsystem;
                this.dllCharacteristics = dllCharacteristics;
                this.stackReserveSize = stackReserveSize;
                this.stackCommitSize = stackCommitSize;
                this.heapReserveSize = heapReserveSize;
                this.heapCommitSize = heapCommitSize;
                this.loaderFlags = 0;
                this.dataDirectoryCount = dataDirectoryCount;
            }


            /// <summary>
            /// Returns the preferred load address of the 
            /// <see cref="PEImage"/>.
            /// </summary>
            public uint ImageBase { get { return this.imageBase; } }

            /// <summary>
            /// Returns the value used to align the section data
            /// in memory when the <see cref="PEImage"/> is loaded
            /// into memory.
            /// </summary>
            public uint SectionAlignment { get { return this.sectionAlignment; } }

            /// <summary>
            /// Returns the value used to align the section data
            /// within the file itself, used to calculate where the individual
            /// sections are.
            /// </summary>
            public uint FileAlignment { get { return this.fileAlignment; } }

            /// <summary>
            /// Returns the <see cref="DWordVersion"/> which denotes
            /// the version of the OS the <see cref="PEImage"/> is expected
            /// to target.
            /// </summary>
            public DWordVersion OSVersion { get { return this.osVersion; } }
            /// <summary>
            /// Returns the <see cref="DWordVersion"/> which denotes the
            /// version of the <see cref="PEImage"/> itself.
            /// </summary>
            public DWordVersion BinaryVersion { get { return this.binaryVersion; } }
            /// <summary>
            /// Returns the <see cref="DWordVersion"/> which denotes the
            /// version of the subsystem the <see cref="PEImage"/> is
            /// expected to target.
            /// </summary>
            public DWordVersion SubsystemVersion { get { return this.subsystemVersion; } }

            /// <summary>
            /// Returns the <see cref="UInt32"/> value which determines
            /// the size, in bytes, of the <see cref="PEImage"/> including
            /// all headers, and padding.
            /// </summary>
            /// <remarks>Will be a multiple of <see cref="SectionAlignment"/>.</remarks>
            public uint ImageSize { get { return this.imageSize; } }

            /// <summary>
            /// Returns the <see cref="UInt32"/> value which notes the
            /// size of all headers defined within the <see cref="PEImage"/>.
            /// </summary>
            public uint HeaderSize { get { return this.headerSize; } }

            /// <summary>
            /// Returns the <see cref="UInt32"/> value which notes the
            /// checksum hash of the file to determine whether it has
            /// been damaged.
            /// </summary>
            /// <remarks>Usually zero for non-microsoft images.</remarks>
            public uint FileChecksum { get { return this.fileChecksum; } }

            /// <summary>
            /// Returns the <see cref="PEImageSubsystem"/> which the <see cref="PEImage"/>
            /// targets.
            /// </summary>
            public PEImageSubsystem Subsystem { get { return this.subsystem; } }

            public PEImageDllCharacteristics DllCharacteristics { get { return this.dllCharacteristics; } }

            public uint StackReserveSize { get { return this.stackReserveSize; } }
            public uint StackCommitSize { get { return this.stackCommitSize; } }
            public uint HeapReserveSize { get { return this.heapReserveSize; } }
            public uint HeapCommitSize { get { return this.heapCommitSize; } }

            [Obsolete("Loader flags is opsolete.", true)]
            public uint LoaderFlags { get { return this.loaderFlags; } }

            internal void Read(EndianAwareBinaryReader reader)
            {
                this.imageBase = reader.ReadUInt32();
                this.sectionAlignment = reader.ReadUInt32();
                this.fileAlignment = reader.ReadUInt32();
                this.osVersion.Read(reader);
                this.binaryVersion.Read(reader);
                this.subsystemVersion.Read(reader);
                this.reserved = reader.ReadUInt32();
                this.imageSize = reader.ReadUInt32();
                this.headerSize = reader.ReadUInt32();
                this.fileChecksum = reader.ReadUInt32();
                this.subsystem = (PEImageSubsystem) reader.ReadUInt16();
                this.dllCharacteristics = (PEImageDllCharacteristics) reader.ReadUInt16();
                this.stackReserveSize = reader.ReadUInt32();
                this.stackCommitSize = reader.ReadUInt32();
                this.heapReserveSize = reader.ReadUInt32();
                this.heapCommitSize = reader.ReadUInt32();
                this.loaderFlags = reader.ReadUInt32();
                this.dataDirectoryCount = reader.ReadUInt32();
            }

            internal void Write(EndianAwareBinaryWriter writer)
            {
                writer.Write(this.imageBase);
                writer.Write(this.sectionAlignment);
                writer.Write(this.fileAlignment);
                this.osVersion.Write(writer);
                this.binaryVersion.Write(writer);
                this.subsystemVersion.Write(writer);
                writer.Write(this.reserved);
                writer.Write(this.imageSize);
                writer.Write(this.headerSize);
                writer.Write(this.fileChecksum);
                writer.Write((ushort) this.subsystem);
                writer.Write((ushort) this.dllCharacteristics);
                writer.Write(this.stackReserveSize);
                writer.Write(this.stackCommitSize);
                writer.Write(this.heapReserveSize);
                writer.Write(this.heapCommitSize);
                writer.Write(this.loaderFlags);
                writer.Write(this.dataDirectoryCount);
            }
        }
    }
}