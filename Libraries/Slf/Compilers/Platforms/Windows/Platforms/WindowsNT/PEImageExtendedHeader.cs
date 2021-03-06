﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using AllenCopeland.Abstraction.IO;

namespace AllenCopeland.Abstraction.Slf.Platforms.WindowsNT
{
    /// <summary>
    /// Provides the layout of the 
    /// <see cref="PEImage"/> optional header.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public partial struct PEImageExtendedHeader
    {
        [FieldOffset(0)]
        private StandardFields standardFields;
        [FieldOffset(28)]
        private NTFields32 ntFields32;
        [FieldOffset(28)]
        private NTFields64 ntFields64;
        [FieldOffset(116)]
        private ConstructRedirects constructRedirects;


        private static readonly int sfSize = Marshal.SizeOf(typeof(StandardFields));
        private static readonly int nt32Size = Marshal.SizeOf(typeof(NTFields32));
        private static readonly int nt64Size = Marshal.SizeOf(typeof(NTFields64));
        private static readonly int ddSize = Marshal.SizeOf(typeof(ConstructRedirects));


        public byte LinkerMajorVersion { get { return this.standardFields.LinkerMajorVersion; } }

        public byte LinkerMinorVersion { get { return this.standardFields.LinkerMinorVersion; } }

        public uint CodeSize { get { return this.standardFields.CodeSize; } }

        public uint InitializedDataSize { get { return this.standardFields.InitializedDataSize; } }

        public uint UninitializedDataSize { get { return this.standardFields.UninitializedDataSize; } }

        public uint EntryPointRVA { get { return this.standardFields.EntryPointRVA; } }

        public uint BaseOfCode { get { return this.standardFields.BaseOfCode; } }


        public uint BaseOfData
        {
            get
            {
                switch (this.ImageKind)
                {
                    case PEImageKind.x86Image:
                        return this.standardFields.BaseOfData;
                    case PEImageKind.RomImage:
                        throw new NotImplementedException();
                    case PEImageKind.x64Image:
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        public PEImageKind ImageKind
        {
            get
            {
                return this.standardFields.ImageKind;
            }
        }


        /// <summary>
        /// Returns the preferred load address of the 
        /// <see cref="PEImage"/>.
        /// </summary>
        public ulong ImageBase
        {
            get
            {
                switch (this.ImageKind)
                {
                    case PEImageKind.x86Image:
                        return this.ntFields32.ImageBase;
                    case PEImageKind.x64Image:
                        return this.ntFields64.ImageBase;
                    case PEImageKind.RomImage:
                        throw new NotImplementedException();
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        /// <summary>
        /// Returns the value used to align the section data
        /// in memory when the <see cref="PEImage"/> is loaded
        /// into memory.
        /// </summary>
        public uint SectionAlignment
        {
            get
            {
                switch (this.ImageKind)
                {
                    case PEImageKind.x86Image:
                        return this.ntFields32.SectionAlignment;
                    case PEImageKind.x64Image:
                        return this.ntFields64.SectionAlignment;
                    case PEImageKind.RomImage:
                        throw new NotImplementedException();
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        /// <summary>
        /// Returns the value used to align the section data
        /// within the file itself, used to calculate where the individual
        /// sections are.
        /// </summary>
        public uint FileAlignment
        {
            get
            {
                switch (this.ImageKind)
                {
                    case PEImageKind.x86Image:
                        return this.ntFields32.FileAlignment;
                    case PEImageKind.x64Image:
                        return this.ntFields64.FileAlignment;
                    case PEImageKind.RomImage:
                        throw new NotImplementedException();
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        /// <summary>
        /// Returns the <see cref="DWordVersion"/> which denotes
        /// the version of the OS the <see cref="PEImage"/> is expected
        /// to target.
        /// </summary>
        public DWordVersion OSVersion
        {
            get
            {
                switch (this.ImageKind)
                {
                    case PEImageKind.x86Image:
                        return this.ntFields32.OSVersion;
                    case PEImageKind.x64Image:
                        return this.ntFields64.OSVersion;
                    case PEImageKind.RomImage:
                        throw new NotImplementedException();
                    default:
                        throw new NotSupportedException();
                }
            }
        }
        /// <summary>
        /// Returns the <see cref="DWordVersion"/> which denotes the
        /// version of the <see cref="PEImage"/> itself.
        /// </summary>
        public DWordVersion BinaryVersion
        {
            get
            {
                switch (this.ImageKind)
                {
                    case PEImageKind.x86Image:
                        return this.ntFields32.BinaryVersion;
                    case PEImageKind.x64Image:
                        return this.ntFields64.BinaryVersion;
                    case PEImageKind.RomImage:
                        throw new NotImplementedException();
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        /// <summary>
        /// Returns the <see cref="DWordVersion"/> which denotes the
        /// version of the subsystem the <see cref="PEImage"/> is
        /// expected to target.
        /// </summary>
        public DWordVersion SubsystemVersion
        {
            get
            {
                switch (this.ImageKind)
                {
                    case PEImageKind.x86Image:
                        return this.ntFields32.SubsystemVersion;
                    case PEImageKind.x64Image:
                        return this.ntFields64.SubsystemVersion;
                    case PEImageKind.RomImage:
                        throw new NotImplementedException();
                    default:
                        throw new NotSupportedException();
                }
            }
        }


        /// <summary>
        /// Returns the <see cref="UInt32"/> value which determines the size,
        /// in bytes, of the <see cref="PEImage"/> including all headers,
        /// and padding.
        /// </summary>
        /// <remarks>Will be a multiple of <see cref="SectionAlignment"/>.</remarks>
        public uint ImageSize
        {
            get
            {
                switch (this.ImageKind)
                {
                    case PEImageKind.x86Image:
                        return this.ntFields32.ImageSize;
                    case PEImageKind.x64Image:
                        return this.ntFields64.ImageSize;
                    case PEImageKind.RomImage:
                        throw new NotImplementedException();
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        /// <summary>
        /// Returns the <see cref="UInt32"/> value which notes the
        /// size of all headers defined within the <see cref="PEImage"/>.
        /// </summary>
        public uint HeaderSize
        {
            get
            {
                switch (this.ImageKind)
                {
                    case PEImageKind.x86Image:
                        return this.ntFields32.HeaderSize;
                    case PEImageKind.x64Image:
                        return this.ntFields64.HeaderSize;
                    case PEImageKind.RomImage:
                        throw new NotImplementedException();
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        /// <summary>
        /// Returns the <see cref="UInt32"/> value which notes the
        /// checksum hash of the file to determine whether it has
        /// been damaged.
        /// </summary>
        /// <remarks>Usually zero for non-microsoft images.</remarks>
        public uint FileChecksum
        {
            get
            {
                switch (this.ImageKind)
                {
                    case PEImageKind.x86Image:
                        return this.ntFields32.FileChecksum;
                    case PEImageKind.x64Image:
                        return this.ntFields64.FileChecksum;
                    case PEImageKind.RomImage:
                        throw new NotImplementedException();
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        /// <summary>
        /// Returns the <see cref="PEImageSubsystem"/> which the <see cref="PEImage"/>
        /// targets.
        /// </summary>
        public PEImageSubsystem Subsystem
        {
            get
            {
                switch (this.ImageKind)
                {
                    case PEImageKind.x86Image:
                        return this.ntFields32.Subsystem;
                    case PEImageKind.x64Image:
                        return this.ntFields64.Subsystem;
                    case PEImageKind.RomImage:
                        throw new NotImplementedException();
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        /// <summary>
        /// Returns the <see cref="PEImageDllCharacteristics"/> which determines the 
        /// characteristics of a dynamic link library image.
        /// </summary>
        public PEImageDllCharacteristics DllCharacteristics
        {
            get
            {
                switch (this.ImageKind)
                {
                    case PEImageKind.x86Image:
                        return this.ntFields32.DllCharacteristics;
                    case PEImageKind.x64Image:
                        return this.ntFields64.DllCharacteristics;
                    case PEImageKind.RomImage:
                        throw new NotImplementedException();
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        /// <summary>
        /// Returns the <see cref="UInt64"/> value which determines the 
        /// size of the stack in virtual memory for the image during run-time.
        /// </summary>
        /// <remarks>In 64-bit images the full <see cref="UInt64"/> value
        /// is used; however in 32-bit images, only the low-order <see cref="UInt32"/>
        /// is used.</remarks>
        public ulong StackReserveSize
        {
            get
            {
                switch (this.ImageKind)
                {
                    case PEImageKind.x86Image:
                        return this.ntFields32.StackReserveSize;
                    case PEImageKind.x64Image:
                        return this.ntFields64.StackReserveSize;
                    case PEImageKind.RomImage:
                        throw new NotImplementedException();
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        /// <summary>
        /// Returns the <see cref="UInt64"/> value which determines the
        /// size of the stack in physical memory for the image 
        /// during run-time.
        /// </summary>
        /// <remarks>In 64-bit images the full <see cref="UInt64"/> value
        /// is used; however in 32-bit images, only the low-order <see cref="UInt32"/>
        /// is used.</remarks>
        public ulong StackCommitSize
        {
            get
            {
                switch (this.ImageKind)
                {
                    case PEImageKind.x86Image:
                        return this.ntFields32.StackCommitSize;
                    case PEImageKind.x64Image:
                        return this.ntFields64.StackCommitSize;
                    case PEImageKind.RomImage:
                        throw new NotImplementedException();
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        /// <summary>
        /// Returns the <see cref="UInt64"/> value which determines the size of the 
        /// heap in virtual memory for the image during run-time.
        /// </summary>
        /// <remarks>In 64-bit images the full <see cref="UInt64"/> value
        /// is used; however in 32-bit images, only the low-order <see cref="UInt32"/>
        /// is used.</remarks>
        public ulong HeapReserveSize
        {
            get
            {
                switch (this.ImageKind)
                {
                    case PEImageKind.x86Image:
                        return this.ntFields32.HeapReserveSize;
                    case PEImageKind.x64Image:
                        return this.ntFields64.HeapReserveSize;
                    case PEImageKind.RomImage:
                        throw new NotImplementedException();
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        /// <summary>
        /// Returns the <see cref="UInt64"/> value which determines the size of the
        /// heap in physical memory for the image during run-time.
        /// </summary>
        /// <remarks>In 64-bit images the full <see cref="UInt64"/> value
        /// is used; however in 32-bit images, only the low-order <see cref="UInt32"/>
        /// is used.</remarks>
        public ulong HeapCommitSize
        {
            get
            {
                switch (this.ImageKind)
                {
                    case PEImageKind.x86Image:
                        return this.ntFields32.HeapCommitSize;
                    case PEImageKind.x64Image:
                        return this.ntFields64.HeapCommitSize;
                    case PEImageKind.RomImage:
                        throw new NotImplementedException();
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        /// <summary>
        /// Returns the <see cref="RVAndSize"/> which denotes the
        /// relative virtual address and size of the exports table
        /// of the <see cref="PEImage"/>.
        /// </summary>
        public RVAndSize ExportTable { get { return this.constructRedirects.ExportTable; } }

        /// <summary>
        /// Returns the <see cref="RVAndSize"/> which denotes the
        /// relative virtual address and size of the exports table
        /// of the <see cref="PEImage"/>.
        /// </summary>
        public RVAndSize ImportTable { get { return this.constructRedirects.ImportTable; } }

        /// <summary>
        /// Returns the <see cref="RVAndSize"/> which denotes the
        /// relative virtual address and size of the exception table
        /// of the <see cref="PEImage"/>.
        /// </summary>
        public RVAndSize ExceptionTable { get { return this.constructRedirects.ExceptionTable; } }

        /// <summary>
        /// Returns the <see cref="RVAndSize"/> which denotes the relative
        /// virtual address, and size, of the certificate table.
        /// </summary>
        public RVAndSize CertificateTable { get { return this.constructRedirects.CertificateTable; } }

        /// <summary>
        /// Returns the <see cref="RVAndSize"/> which denotes the relative virtual
        /// address and size of the base relocation table.
        /// </summary>
        public RVAndSize BaseRelocationTable { get { return this.constructRedirects.BaseRelocationTable; } }

        /// <summary>
        /// Returns the <see cref="RVAndSize"/> which denotes the relative virtual address
        /// and size of the debug information table.
        /// </summary>
        public RVAndSize DebugInformationTable { get { return this.constructRedirects.DebugInformationTable; } }

        /// <summary>
        /// Returns the <see cref="RVAndSize"/> which denotes the relative virtual address and size
        /// of the copyright information for the PE.
        /// </summary>
        public RVAndSize Copyright { get { return this.constructRedirects.Copyright; } }

        /// <summary>
        /// Returns the <see cref="RVAndSize"/> which denotes the relative virtual address
        /// and size of the global pointer.
        /// </summary>
        public RVAndSize GlobalPointer { get { return this.constructRedirects.GlobalPointer; } }

        /// <summary>
        /// Returns the <see cref="RVAndSize"/> which denotes the relative virtual address
        /// and size of the thread local storage.
        /// </summary>
        public RVAndSize ThreadLocalStorage { get { return this.constructRedirects.ThreadLocalStorage; } }

        /// <summary>
        /// Returns the <see cref="RVAndSize"/> which denotes the relative virtual
        /// address and size of the load configuration table.
        /// </summary>
        public RVAndSize LoadConfigurationTable { get { return this.constructRedirects.LoadConfigurationTable; } }

        /// <summary>
        /// Returns the <see cref="RVAndSize"/> which denotes the relative virtual
        /// address and size of the bound import table.
        /// </summary>
        public RVAndSize BoundImportTable { get { return this.constructRedirects.BoundImportTable; } }

        /// <summary>
        /// Returns the <see cref="RVAndSize"/> which denotes the relative virtual address
        /// and size of the import address table.
        /// </summary>
        public RVAndSize ImportAddressTable { get { return this.constructRedirects.ImportAddressTable; } }

        /// <summary>
        /// Returns the <see cref="RVAndSize"/> which denotes the relative virtual
        /// address and size of the delay import address table.
        /// </summary>
        public RVAndSize DelayImportAddressTable { get { return this.constructRedirects.DelayImportAddressTable; } }

        /// <summary>
        /// Returns the <see cref="RVAndSize"/> which denotes the relative virtual
        /// address and size of the common language infrastructure (ECMA-335) header.
        /// </summary>
        public RVAndSize CliHeader { get { return this.constructRedirects.CliHeader; } }


        internal void Read(EndianAwareBinaryReader reader)
        {
            standardFields.Read(reader);
            switch (standardFields.ImageKind)
            {
                case PEImageKind.x86Image:
                    ntFields32.Read(reader);
                    break;
                case PEImageKind.x64Image:
                    ntFields64.Read(reader);
                    break;
                case PEImageKind.RomImage:
                    throw new NotImplementedException();
                default:
                    throw new NotSupportedException();
            }
            constructRedirects.Read(reader);
        }

        internal void Write(EndianAwareBinaryWriter writer)
        {
            standardFields.Write(writer);
            switch (standardFields.ImageKind)
            {
                case PEImageKind.x86Image:
                    ntFields32.Write(writer);
                    break;
                case PEImageKind.x64Image:
                    ntFields64.Write(writer);
                    break;
                case PEImageKind.RomImage:
                    throw new NotImplementedException();
                default:
                    throw new NotSupportedException();
            }
            constructRedirects.Write(writer);
        }


    }
}
