﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using AllenCopeland.Abstraction.IO;
//using AllenCopeland.Abstraction.Numerics;
//using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Platforms.DOS;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Platforms.WindowsNT
{
    /// <summary>
    /// Provides a basic implementation of a Portable Executable image.
    /// </summary>
    public class PEImage :
        IDisposable
    {
        private DOSHeader dosHeader;
        private CoffHeader coffHeader;
        private PEImageExtendedHeader extendedHeader;
        private ControlledCollection<CoffSection> sections;
        private EndianAwareBinaryReader reader;
        string filename;
        private bool keepImageOpen;
        private object syncObject = new object();
        private int streamIndex = 0;
        private Tuple<object, FileStream, EndianAwareBinaryReader> sync1;
        private Tuple<object, FileStream, EndianAwareBinaryReader> sync2;
        private Tuple<object, FileStream, EndianAwareBinaryReader> sync3;
        private Tuple<object, FileStream, EndianAwareBinaryReader> sync4;
        private Tuple<object, FileStream, EndianAwareBinaryReader> sync5;
        /// <summary>
        /// The old MS-DOS stub program that prints
        /// 'This program cannot be run in DOS mode.'
        /// </summary>
        public static readonly byte[] msDosStubProgram = new byte[]
        {
            0x0e, 0x1f, 0xba, 0x0e, 0x00, 0xb4, 0x09, 0xcd, 0x21, 0xb8, 0x01, 0x4c, 0xcd, 0x21, 0x54, 0x68, /* ........!..L.!Th */
            0x69, 0x73, 0x20, 0x70, 0x72, 0x6f, 0x67, 0x72, 0x61, 0x6d, 0x20, 0x63, 0x61, 0x6e, 0x6e, 0x6f, /* is program canno */
            0x74, 0x20, 0x62, 0x65, 0x20, 0x72, 0x75, 0x6e, 0x20, 0x69, 0x6e, 0x20, 0x44, 0x4f, 0x53, 0x20, /* t be run in DOS  */
            0x6d, 0x6f, 0x64, 0x65, 0x2e, 0x0d, 0x0d, 0x0a, 0x24, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, /* mode....$....... */
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
            coffHeader.Read(reader);
            extendedHeader.Read(reader);
            CoffSection[] sections = new CoffSection[this.coffHeader.SectionCount];
            for (int i = 0; i < sections.Length; i++)
                sections[i] = CoffSection.Read(reader, keepImageOpen);
            this.sections = new ControlledCollection<CoffSection>(sections);
            this.keepImageOpen = keepImageOpen;
        }

        /// <summary>
        /// Returns the <see cref="DOSHeader"/> which contains information about the 
        /// antiquated Disk Operating System stub program that identifies the program
        /// cannot operate outside of Windows.
        /// </summary>
        public DOSHeader DOSHeader { get { return this.dosHeader; } }
        /// <summary>
        /// Returns the Common Object File Format header.
        /// </summary>
        public CoffHeader CoffHeader { get { return this.coffHeader; } }
        /// <summary>
        /// Returns the <see cref="PEImageExtendedHeader"/> which denotes the extended 
        /// information relative to WinNT based applications.
        /// </summary>
        public PEImageExtendedHeader ExtendedHeader { get { return this.extendedHeader; } }

        public static PEImage LoadImage(string filename)
        {
            FileStream dummy;
            return LoadImage(filename, out dummy);
        }

        private PEImage() { }

        public PEImage CreateNew() 
        {
            return new PEImage();
        }

        public static PEImage LoadImage(string filename, out FileStream imageStream, bool keepImageOpen = false)
        {
            /* *
             * Rewrite PE Loader and metadata reader.
             * */
            var result = new PEImage();
            result.filename = filename;
            var streamSyncObject = result.SecureReader();
            imageStream = streamSyncObject.Item2;
            EndianAwareBinaryReader imageReader = streamSyncObject.Item3;
            result.syncObject = streamSyncObject.Item1;
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

        public CoffSection DataSection
        {
            get
            {
                return this.ResolveRelativeVirtualAddress(this.extendedHeader.BaseOfData).Section;
            }
        }

        public CoffSection CodeSection
        {
            get
            {
                return this.ResolveRelativeVirtualAddress(this.extendedHeader.BaseOfCode).Section;
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
            if (this.sync1 != null)
            {
                this.sync1.Item2.Close();
                this.sync1.Item2.Dispose();
                this.sync1.Item3.Close();
                this.sync1.Item3.Dispose();
            }
            if (this.sync2 != null)
            {
                this.sync2.Item2.Close();
                this.sync2.Item2.Dispose();
                this.sync2.Item3.Close();
                this.sync2.Item3.Dispose();
            }
            if (this.sync3 != null)
            {
                this.sync3.Item2.Close();
                this.sync3.Item2.Dispose();
                this.sync3.Item3.Close();
                this.sync3.Item3.Dispose();
            }
            if (this.sync4 != null)
            {
                this.sync4.Item2.Close();
                this.sync4.Item2.Dispose();
                this.sync4.Item3.Close();
                this.sync4.Item3.Dispose();
            }
            if (this.sync5 != null)
            {
                this.sync5.Item2.Close();
                this.sync5.Item2.Dispose();
                this.sync5.Item3.Close();
                this.sync5.Item3.Dispose();
            }
        }

        public object SyncObject { get { return this.syncObject; } }


        internal Tuple<object, FileStream, EndianAwareBinaryReader> SecureReader()
        {
            Tuple<object, FileStream, EndianAwareBinaryReader> result;

            lock (this.syncObject)
            {
                switch (streamIndex)
                {
                    case 0:
                        result = sync1 ?? (sync1 = CreateReader(streamIndex));
                        break;
                    case 1:
                        result = sync2 ?? (sync2 = CreateReader(streamIndex));
                        break;
                    case 2:
                        result = sync3 ?? (sync3 = CreateReader(streamIndex));
                        break;
                    case 3:
                        result = sync4 ?? (sync4 = CreateReader(streamIndex));
                        break;
                    case 4:
                    default:
                        result = sync5 ?? (sync5 = CreateReader(streamIndex));
                        break;
                }
                streamIndex = (streamIndex + 1) % 5;
            }
            return result;
        }

        private Tuple<object, FileStream, EndianAwareBinaryReader> CreateReader(int index)
        {
            object syncObject = new { Name = string.Format("Stream {0} for {1}", index + 1, this.filename) };
            FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            return new Tuple<object, FileStream, EndianAwareBinaryReader>(syncObject, stream, new EndianAwareBinaryReader(stream, Endianness.LittleEndian, false));
        }
    }
}
