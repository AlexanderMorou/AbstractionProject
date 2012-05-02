using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using AllenCopeland.Abstraction.IO;
using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Platforms.DOS;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Platforms.WindowsNT
{
    public class PEImage :
        IDisposable
    {
        private DOSHeader dosHeader;
        private PEImageStandardHeader standardHeader;
        private PEImageOptionalHeader optionalHeader;
        private ReadOnlyCollection<PEImageSection> sections;
        private EndianAwareBinaryReader reader;
        string filename;
        private bool keepImageOpen;
        /// <summary>
        /// The old MS-DOS stub program that prints
        /// 'This program cannot be run in DOS mode.'
        /// </summary>
        public static readonly byte[] msDosStubProgram = new byte[]
        {
            0x0e, 0x1f, 0xba, 0x0e, 0x00, 0xb4, 0x09, 0xcd, /* ........ */
            0x21, 0xb8, 0x01, 0x4c, 0xcd, 0x21, 0x54, 0x68, /* !..L.!Th */
            0x69, 0x73, 0x20, 0x70, 0x72, 0x6f, 0x67, 0x72, /* is progr */
            0x61, 0x6d, 0x20, 0x63, 0x61, 0x6e, 0x6e, 0x6f, /* am canno */
            0x74, 0x20, 0x62, 0x65, 0x20, 0x72, 0x75, 0x6e, /* t be run */
            0x20, 0x69, 0x6e, 0x20, 0x44, 0x4f, 0x53, 0x20, /*  in DOS  */
            0x6d, 0x6f, 0x64, 0x65, 0x2e, 0x0d, 0x0d, 0x0a, /* mode.... */
            0x24, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, /* $....... */
        };


        /// <summary>
        /// The stub header that indicates the structure of the
        /// <see cref="msDosStubProgram"/>.
        /// </summary>
        public static readonly DOSHeader dosStubHeader = new DOSHeader(0x0090, 0x0003, 0x0000, 0x0004, 0x0000, 0xFFFF, 0x0000, 0x00b8, 0x0000, 0x0000, 0x0000, 0x0040, 0x0000, 0x0000, 0x0000, 0xFFFFFFFF);

        private void Read(EndianAwareBinaryReader reader, bool keepImageOpen)
        {
            this.reader = reader;
            this.dosHeader.Read(reader);
            if ((dosHeader.PEHeaderPointer - reader.BaseStream.Position) < 0)
                throw new BadImageFormatException();
            byte[] dosStub = new byte[msDosStubProgram.Length];
            reader.Read(dosStub, 0, dosStub.Length);
            /* *
             * ToDo: Handle alternate stub programs that are valid.
             * */
            if (!msDosStubProgram.SequenceEqual(dosStub))
                throw new BadImageFormatException();
            
            reader.BaseStream.Seek(dosHeader.PEHeaderPointer, SeekOrigin.Begin);
            standardHeader.Read(reader);
            optionalHeader.Read(reader);
            PEImageSection[] sections = new PEImageSection[this.standardHeader.SectionCount];
            for (int i = 0; i < sections.Length; i++)
                sections[i] = PEImageSection.Read(reader, keepImageOpen);
            this.sections = new ReadOnlyCollection<PEImageSection>(sections);
            this.keepImageOpen = keepImageOpen;
        }

        public DOSHeader DOSHeader { get { return this.dosHeader; } }
        public PEImageStandardHeader StandardHeader { get { return this.standardHeader; } }
        public PEImageOptionalHeader OptionalHeader { get { return this.optionalHeader; } }

        public static PEImage LoadImage(string filename)
        {
            FileStream dummy;
            return LoadImage(filename, out dummy);
        }

        public static PEImage LoadImage(string filename, out FileStream imageStream, bool keepImageOpen = false)
        {
            /* *
             * Rewrite PE Loader and metadata reader.
             * */
            imageStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            EndianAwareBinaryReader imageReader = new EndianAwareBinaryReader(imageStream, Endianness.LittleEndian, false);
            var result = new PEImage();
            result.filename = filename;
            result.Read(imageReader, keepImageOpen);
            return result;
        }

        public PEImageRVAResolutionResult ResolveRelativeVirtualAddress(uint relativeVirtualAddress)
        {
            foreach (var section in this.sections)
                if (section.VirtualAddress <= relativeVirtualAddress && section.VirtualAddress + section.SizeOfRawData >= relativeVirtualAddress)
                    return new PEImageRVAResolutionResult(relativeVirtualAddress - section.VirtualAddress, section);
            return PEImageRVAResolutionResult.ResolutionFailure;
        }

        public PEImageSection DataSection
        {
            get
            {
                return this.ResolveRelativeVirtualAddress(this.optionalHeader.BaseOfData).Section;
            }
        }

        public PEImageSection CodeSection
        {
            get
            {
                return this.ResolveRelativeVirtualAddress(this.optionalHeader.BaseOfCode).Section;
            }
        }

        public void Dispose()
        {
            DisposeReader();
            if (this.sections != null)
            {
                foreach (var section in this.sections)
                    section.Dispose();
                this.sections = null;
            }
        }

        public string Filename { get { return this.filename; } }

        private void DisposeReader()
        {
            if (this.reader != null)
            {
                if (!keepImageOpen)
                {
                    this.reader.BaseStream.Close();
                    this.reader.BaseStream.Dispose();
                    this.reader.Close();
                    this.reader.Dispose();
                }
                this.reader = null;
            }
        }

    }
}
