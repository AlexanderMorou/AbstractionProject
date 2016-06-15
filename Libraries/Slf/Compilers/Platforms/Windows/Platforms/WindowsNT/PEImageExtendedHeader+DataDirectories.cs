using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using AllenCopeland.Abstraction.IO;

namespace AllenCopeland.Abstraction.Slf.Platforms.WindowsNT
{
    public partial struct PEImageExtendedHeader
    {
        /// <summary>
        /// The data directories which denote the relative virtual address and sizes of the tables contained within the <see cref="PEImage"/>.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Size = 1 << 7)]
        private struct ConstructRedirects
        {
            /// <summary>
            /// Data member for <see cref="ExportTable"/>.
            /// </summary>
            private RVAndSize exportTable;
            /// <summary>
            /// Data member for <see cref="ImportTable"/>.
            /// </summary>
            private RVAndSize importTable;
            /// <summary>
            /// Data member for <see cref="ResourceTable"/>.
            /// </summary>
            private RVAndSize resourceTable;
            /// <summary>
            /// Data member for <see cref="ExceptionTable"/>.
            /// </summary>
            private RVAndSize exceptionTable;
            /// <summary>
            /// Data member for <see cref="CertificateTable"/>.
            /// </summary>
            private RVAndSize certificateTable;
            /// <summary>
            /// Data member for <see cref="BaseRelocationTable"/>.
            /// </summary>
            private RVAndSize baseRelocationTable;
            /// <summary>
            /// Data member for <see cref="DebugInformationTable"/>.
            /// </summary>
            private RVAndSize debugInformationTable;
            /// <summary>
            /// Data member for <see cref="Copyright"/> information.
            /// </summary>
            private RVAndSize copyright;
            /// <summary>
            /// Data member for <see cref="GlobalPointer"/>.
            /// </summary>
            private RVAndSize globalPointer;
            /// <summary>
            /// Data member for <see cref="ThreadLocalStorage"/>.
            /// </summary>
            private RVAndSize threadLocalStorage;
            /// <summary>
            /// Data member for <see cref="LoadConfigurationTable"/>.
            /// </summary>
            private RVAndSize loadConfigurationTable;
            /// <summary>
            /// Data member for <see cref="BoundImportTable"/>.
            /// </summary>
            private RVAndSize boundImportTable;
            /// <summary>
            /// Data member for <see cref="ImportAddressTable"/>.
            /// </summary>
            private RVAndSize importAddressTable;
            /// <summary>
            /// Data member for <see cref="DelayImportAddressTable"/>.
            /// </summary>
            private RVAndSize delayImportAddressTable;
            /// <summary>
            /// Data member for <see cref="CliHeader"/>
            /// </summary>
            private RVAndSize cliHeader;
            /// <summary>
            /// Data member is reserved.
            /// </summary>
            private RVAndSize reserved;

            /// <summary>
            /// Returns the <see cref="RVAndSize"/> which denotes the
            /// relative virtual address and size of the exports table
            /// of the <see cref="PEImage"/>.
            /// </summary>
            /// <value>Uses the <see cref="exportTable"/> variable.</value>
            public RVAndSize ExportTable { get { return this.exportTable; } }

            /// <summary>
            /// Returns the <see cref="RVAndSize"/> which denotes the
            /// relative virtual address and size of the exports table
            /// of the <see cref="PEImage"/>.
            /// </summary>
            /// <value>Uses the <see cref="importTable"/> variable.</value>
            public RVAndSize ImportTable { get { return this.importTable; } }

            public RVAndSize ExceptionTable { get { return this.exceptionTable; } }

            public RVAndSize CertificateTable { get { return this.certificateTable; } }

            public RVAndSize BaseRelocationTable { get { return this.baseRelocationTable; } }

            public RVAndSize DebugInformationTable { get { return this.debugInformationTable; } }

            public RVAndSize Copyright { get { return this.copyright; } }

            public RVAndSize GlobalPointer { get { return this.globalPointer; } }

            public RVAndSize ThreadLocalStorage { get { return this.threadLocalStorage; } }

            public RVAndSize LoadConfigurationTable { get { return this.loadConfigurationTable; } }

            public RVAndSize BoundImportTable { get { return this.boundImportTable; } }

            public RVAndSize ImportAddressTable { get { return this.importAddressTable; } }

            public RVAndSize DelayImportAddressTable { get { return this.delayImportAddressTable; } }

            public RVAndSize CliHeader { get { return this.cliHeader; } }

            internal void Read(EndianAwareBinaryReader reader)
            {
                if (reader.BaseStream.Position + 128 > reader.BaseStream.Length)
                    throw new BadImageFormatException("Image ends suddenly in data directories.");
                this.exportTable.Read(reader);
                this.importTable.Read(reader);
                this.resourceTable.Read(reader);
                this.exceptionTable.Read(reader);
                this.certificateTable.Read(reader);
                this.baseRelocationTable.Read(reader);
                this.debugInformationTable.Read(reader);
                this.copyright.Read(reader);
                this.globalPointer.Read(reader);
                this.threadLocalStorage.Read(reader);
                this.loadConfigurationTable.Read(reader);
                this.boundImportTable.Read(reader);
                this.importAddressTable.Read(reader);
                this.delayImportAddressTable.Read(reader);
                this.cliHeader.Read(reader);
                this.reserved.Read(reader);
            }

            internal void Write(EndianAwareBinaryWriter writer)
            {
                this.exportTable.Write(writer);
                this.importTable.Write(writer);
                this.resourceTable.Write(writer);
                this.exceptionTable.Write(writer);
                this.certificateTable.Write(writer);
                this.baseRelocationTable.Write(writer);
                this.debugInformationTable.Write(writer);
                this.copyright.Write(writer);
                this.globalPointer.Write(writer);
                this.threadLocalStorage.Write(writer);
                this.loadConfigurationTable.Write(writer);
                this.boundImportTable.Write(writer);
                this.importAddressTable.Write(writer);
                this.delayImportAddressTable.Write(writer);
                this.cliHeader.Write(writer);
                this.reserved.Write(writer);
            }
        }
    }
}