using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;
using System.IO;
using AllenCopeland.Abstraction.IO;
using System.Runtime.InteropServices;

namespace AllenCopeland.Abstraction.Slf.Platforms.DOS
{
    /// <summary>
    /// Defines information about the DOS image.
    /// </summary>
    public struct DOSHeader
    {
        /* *
         * http://en.wikipedia.org/wiki/Mark_Zbikowski
         * */
        /// <summary>
        /// The magic number that starts the DOS header, which is MZ for Mark Zbikowski, the
        /// format creator..
        /// </summary>
        /// <remarks>'MZ' in big endian and 'ZM' in little-endian</remarks>
        private const ushort magicConst = 0x5A4D;
        private const int pageSize = 512;
        /// <summary>
        /// e_magic
        /// </summary>
        private ushort magic;
        /// <summary>
        /// e_cblp
        /// </summary>
        private ushort lastPageSize;
        /// <summary>
        /// e_cp
        /// </summary>
        private ushort pageCount;
        /// <summary>
        /// e_crlc
        /// </summary>
        private ushort relocations;
        /// <summary>
        /// e_cparhdr
        /// </summary>
        private ushort headerSize;
        /// <summary>
        /// e_minalloc
        /// </summary>
        private ushort minimumParagraphsNeeded;
        /// <summary>
        /// e_maxalloc
        /// </summary>
        private ushort maximumParagraphsNeeded;
        /// <summary>
        /// e_ss
        /// </summary>
        private ushort initialStackSegment;
        /// <summary>
        /// e_sp
        /// </summary>
        private ushort initialStackPointer;
        /// <summary>
        /// e_csum
        /// </summary>
        private ushort checksum;
        /// <summary>
        /// e_ip
        /// </summary>
        private ushort initialInstructionPointer;
        /// <summary>
        /// e_cs
        /// </summary>
        private ushort initialCodeSegment;
        /// <summary>
        /// e_lfarlc
        /// </summary>
        private ushort relocationTablePtr;
        /// <summary>
        /// e_ovno
        /// </summary>
        private ushort overlayNumber;
        /// <summary>
        /// e_res1
        /// </summary>
        private ulong reserved;
        /// <summary>
        /// e_oemid
        /// </summary>
        private ushort oemIdentifier;
        /// <summary>
        /// e_oeminfo
        /// </summary>
        private ushort oemInformation;
        /// <summary>
        /// e_res2
        /// </summary>
        private ulong reserved2A;
        /// <summary>
        /// e_res2
        /// </summary>
        private ulong reserved2B;
        /// <summary>
        /// e_res2
        /// </summary>
        private uint reserved2C;
        /// <summary>
        /// e_lfanew
        /// </summary>
        private uint peHeaderPointer;

        /// <summary>
        /// Creates a new <see cref="DOSHeader"/> with the 
        /// information about the image.
        /// </summary>
        /// <param name="lastPageSize">The <see cref="UInt16"/> value which denotes the
        /// number of bytes in the last page of the executable.</param>
        /// <param name="pageCount"><see cref="UInt16"/> value which determines the
        /// number of 512-byte pages within the image.</param>
        /// <param name="relocations">The <see cref="UInt16"/> value which determines the
        /// number of relocations within the DOS image</param>
        /// <param name="headerSize">The <see cref="UInt16"/> value which determines the
        /// size of the header.</param>
        /// <param name="minimumParagraphsNeeded">The <see cref="UInt16"/> value which determines the 
        /// minimum number of paragraphs needed in the
        /// DOS image.</param>
        /// <param name="maximumParagraphsNeeded">The <see cref="UInt16"/> value which determines
        /// the maximum number of paragraphs needed in the
        /// DOS image.</param>
        /// <param name="initialStackSegment">The <see cref="UInt16"/> value which determines the initial
        /// stack segment in the DOS image.</param>
        /// <param name="initialStackPointer">The <see cref="UInt16"/> value which determines the initial
        /// stack pointer.</param>
        /// <param name="checksum">The <see cref="UInt16"/> value which determines the
        /// checksum hash of the file which determines whether it has
        /// been damaged or not.</param>
        /// <param name="initialInstructionPointer">The <see cref="UInt16"/> value which determines the initial
        /// instruction pointer.</param>
        /// <param name="initialCodeSegment">The <see cref="UInt16"/> value which determines the 
        /// initial code segment to start in.</param>
        /// <param name="relocationTablePtr">The <see cref="UInt16"/> value which determines the
        /// relocation table within the file.</param>
        /// <param name="overlayNumber"></param>
        /// <param name="oemIdentifier">The <see cref="UInt16"/> value which determines the 
        /// OEM-specific identifier for the DOS image.</param>
        /// <param name="oemInformation">The <see cref="UInt16"/> value which determines 
        /// OEM-specific information about the DOS image.</param>
        /// <param name="peHeaderPointer">The <see cref="UInt32"/> value which determines the
        /// offset of the <see cref="PEImage"/>.</param>
        public DOSHeader(ushort lastPageSize, ushort pageCount, ushort relocations, ushort headerSize, ushort minimumParagraphsNeeded, ushort maximumParagraphsNeeded, ushort initialStackSegment, ushort initialStackPointer, ushort checksum, ushort initialInstructionPointer, ushort initialCodeSegment, ushort relocationTablePtr, ushort overlayNumber, ushort oemIdentifier, ushort oemInformation, uint peHeaderPointer)
        {
            this.magic = magicConst;
            this.lastPageSize = lastPageSize;
            this.pageCount = pageCount;
            this.relocations = relocations;
            this.headerSize = headerSize;
            this.minimumParagraphsNeeded = minimumParagraphsNeeded;
            this.maximumParagraphsNeeded = maximumParagraphsNeeded;
            this.initialStackSegment = initialStackSegment;
            this.initialStackPointer = initialStackPointer;
            this.checksum = checksum;
            this.initialInstructionPointer = initialInstructionPointer;
            this.initialCodeSegment = initialCodeSegment;
            this.relocationTablePtr = relocationTablePtr;
            this.overlayNumber = overlayNumber;
            this.reserved = 0;
            this.oemIdentifier = oemIdentifier;
            this.oemInformation = oemInformation;
            this.reserved2A = 0;
            this.reserved2B = 0;
            this.reserved2C = 0;
            this.peHeaderPointer = peHeaderPointer;
        }

        //#region Properties

        /// <summary>
        /// Returns the <see cref="UInt16"/> value which denotes the
        /// number of bytes in the last page of the executable.
        /// </summary>
        public ushort LastPageSize { get { return this.lastPageSize; } }

        /// <summary>
        /// Returns the <see cref="UInt16"/> value which determines the
        /// number of pages within the image.
        /// </summary>
        public ushort PageCount { get { return this.pageCount; } }

        /// <summary>
        /// Returns the <see cref="UInt16"/> value which determines the
        /// number of relocations within the DOS image.
        /// </summary>
        public ushort Relocations { get { return this.relocations; } }

        /// <summary>
        /// Returns the <see cref="UInt16"/> value which determines the
        /// size of the header.
        /// </summary>
        public ushort HeaderSize { get { return this.headerSize; } }

        /// <summary>
        /// Returns the <see cref="UInt16"/> value which determines the 
        /// minimum number of paragraphs needed in the
        /// DOS image.
        /// </summary>
        public ushort MinimumParagraphsNeeded { get { return this.minimumParagraphsNeeded; } }

        /// <summary>
        /// Returns the <see cref="UInt16"/> value which determines
        /// the maximum number of paragraphs needed in the
        /// DOS image.
        /// </summary>
        public ushort MaximumParagraphsNeeded { get { return this.maximumParagraphsNeeded; } }

        /// <summary>
        /// Returns the <see cref="UInt16"/> value which determines the initial
        /// stack segment in the DOS image.
        /// </summary>
        public ushort InitialStackSegment { get { return this.initialStackSegment; } }

        /// <summary>
        /// Returns the <see cref="UInt16"/> value which determines the initial
        /// stack pointer.
        /// </summary>
        public ushort InitialStackPointer { get { return this.initialStackPointer; } }

        /// <summary>
        /// Returns the <see cref="UInt16"/> value which determines the
        /// checksum hash of the file which determines whether it has
        /// been damaged or not.
        /// </summary>
        public ushort Checksum { get { return this.checksum; } }

        /// <summary>
        /// Returns the <see cref="UInt16"/> value which determines the initial
        /// instruction pointer.
        /// </summary>
        public ushort InitialInstructionPointer { get { return this.initialInstructionPointer; } }

        /// <summary>
        /// Returns the <see cref="UInt16"/> value which determines the 
        /// initial code segment to start in.
        /// </summary>
        public ushort InitialCodeSegment { get { return this.initialCodeSegment; } }

        /// <summary>
        /// Returns the <see cref="UInt16"/> value which determines the
        /// relocation table within the file.
        /// </summary>
        public ushort RelocationTablePointer { get { return this.relocationTablePtr; } }

        public ushort OverlayNumber { get { return this.overlayNumber; } }

        /// <summary>
        /// Returns the <see cref="UInt16"/> value which determines the 
        /// OEM-specific identifier for the DOS image.
        /// </summary>
        public ushort OemIdentifier { get { return this.oemIdentifier; } }

        /// <summary>
        /// Returns the <see cref="UInt16"/> value which determines 
        /// OEM-specific information about the DOS image.
        /// </summary>
        public ushort OemInformation { get { return this.oemInformation; } }

        /// <summary>
        /// Returns the <see cref="UInt32"/> value which determines the
        /// offset of the <see cref="PEImage"/>.
        /// </summary>
        public uint PEHeaderPointer { get { return this.peHeaderPointer; } }

        //http://code.google.com/p/sawbuck/source/browse/trunk/syzygy/pe/pe_utils.cc?spec=svn629&r=548
        /// <summary>
        /// Returns whether the header size provided is valid.
        /// </summary>
        public bool IsHeaderSizeValid
        {
            get
            {
                int e_cpahdr = this.headerSize;
                int c_cpahdr = this.headerSize * pageSize;
                if (this.lastPageSize != 0 && c_cpahdr < pageSize)
                        return false;
                if (c_cpahdr < Marshal.SizeOf(typeof(DOSHeader)))
                    return false;
                return true;
            }
        }
        //#endregion

        public void Read(EndianAwareBinaryReader reader)
        {
            this.magic = reader.ReadUInt16();
            if (magic != magicConst)
                throw new BadImageFormatException();
            this.lastPageSize = reader.ReadUInt16();
            this.pageCount = reader.ReadUInt16();
            this.relocations = reader.ReadUInt16();
            this.headerSize = reader.ReadUInt16();
            this.minimumParagraphsNeeded = reader.ReadUInt16();
            this.maximumParagraphsNeeded = reader.ReadUInt16();
            this.initialStackSegment = reader.ReadUInt16();
            this.initialStackPointer = reader.ReadUInt16();
            this.checksum = reader.ReadUInt16();
            this.initialInstructionPointer = reader.ReadUInt16();
            this.initialCodeSegment = reader.ReadUInt16();
            this.relocationTablePtr = reader.ReadUInt16();
            this.overlayNumber = reader.ReadUInt16();
            this.reserved = reader.ReadUInt64();
            this.oemIdentifier = reader.ReadUInt16();
            this.oemInformation = reader.ReadUInt16();
            this.reserved2A = reader.ReadUInt64();
            this.reserved2B = reader.ReadUInt64();
            this.reserved2C = reader.ReadUInt32();
            this.peHeaderPointer = reader.ReadUInt32();
        }

        public void Write(EndianAwareBinaryWriter writer)
        {

            writer.Write(this.magic);
            writer.Write(this.lastPageSize);
            writer.Write(this.pageCount);
            writer.Write(this.relocations);
            writer.Write(this.headerSize);
            writer.Write(this.minimumParagraphsNeeded);
            writer.Write(this.maximumParagraphsNeeded);
            writer.Write(this.initialStackSegment);
            writer.Write(this.initialStackPointer);
            writer.Write(this.checksum);
            writer.Write(this.initialInstructionPointer);
            writer.Write(this.initialCodeSegment);
            writer.Write(this.relocationTablePtr);
            writer.Write(this.overlayNumber);
            writer.Write(this.reserved);
            writer.Write(this.oemIdentifier);
            writer.Write(this.oemInformation);
            writer.Write(this.reserved2A);
            writer.Write(this.reserved2B);
            writer.Write(this.reserved2C);
            writer.Write(this.peHeaderPointer);
        }


    }
}
