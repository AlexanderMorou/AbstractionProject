﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.IO;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Utilities.Arrays;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata
{
    internal class CliMetadataFixedRoot :
        ICliMetadataRoot
    {
        private const uint metadataSignature = 0x424A5342;
        private CliHeader header;

        private uint signature;
        private PEImage sourceImage;
        private DWordVersion depreciatedVersion;
        private uint reserved;
        private int realVersionLength;
        private byte[] version;
        private string realVersion;
        private ushort reservedFlags;
        private bool isDisposed;
        private CliMetadataStringsHeaderAndHeap strings;
        private CliMetadataBlobHeaderAndHeap blob;
        private CliMetadataGuidHeaderAndHeap guids;
        private CliMetadataUserStringsHeaderAndHeap userStrings;
        private CliMetadataTableStreamAndHeader tableStream;
        private uint streamPosition;
        private FileStream originalStream;
        private IEnumerable<ICliMetadataMethodSemanticsTableRow> fullSemantics;
        private IEnumerable<ICliMetadataMethodSemanticsTableRow> propertySemantics;
        private IEnumerable<ICliMetadataMethodSemanticsTableRow> eventSemantics;

        public static int ReadCompressedUnsignedInt(EndianAwareBinaryReader reader)
        {
            byte dummyByte;
            return ReadCompressedUnsignedInt(reader, out dummyByte);
        }

        public static int ReadCompressedSignedInt(EndianAwareBinaryReader reader)
        {
            byte size;
            var result = ReadCompressedUnsignedInt(reader, out size);
            bool negate = (result & 1) == 1;
            result >>= 1;
            if (negate)
                result |= unchecked((int)0x80000000);
            return result;
        }

        public static int ReadCompressedUnsignedInt(EndianAwareBinaryReader reader, out byte bytesUsed)
        {
            byte compressedFirstByte           = reader.ReadByte();
            const int sevenBitMask             = 0x7F;
            const int fourteenBitmask          = 0xBF;
            const int twentyNineBitMask        = 0xDF;
            bytesUsed                          = 1;
            int decompressedResult             = 0;

            if ((compressedFirstByte & sevenBitMask) == compressedFirstByte)
                decompressedResult             = compressedFirstByte;
            else if ((compressedFirstByte & fourteenBitmask) == compressedFirstByte)
            {
                byte hiByte                    = (byte)(compressedFirstByte & 0x3F);
                byte loByte                    = reader.ReadByte();
                decompressedResult             = loByte | hiByte << 8;
                bytesUsed                      = 2;
            }
            else if ((compressedFirstByte & twentyNineBitMask) == compressedFirstByte)
            {
                byte hiWordHiByte              = (byte)(compressedFirstByte & 0x1F);
                byte hiWordLoByte              = reader.ReadByte();
                byte loWordHiByte              = reader.ReadByte();
                byte loWordLoByte              = reader.ReadByte();
                decompressedResult             = loWordLoByte | loWordHiByte << 8 | hiWordLoByte << 16 | hiWordHiByte << 24;
                bytesUsed                      = 4;
            }
            return decompressedResult;
        }

        public CliHeader Header { get { return this.header; } }

        public string Version
        {
            get
            {
                if (this.realVersion == null)
                {
                    bool broke = false;
                    int index = 0;
                    char[] realVersion = new char[this.version.Length];
                    for (index = 0; index < realVersionLength; index++)
                    {
                        if (this.version[index] == '\0')
                        {
                            broke = true;
                            break;
                        }
                        else
                            realVersion[index] = (char)this.version[index];
                    }
                    this.realVersion = new string(realVersion, 0, broke ? index : realVersionLength);
                }
                return this.realVersion;
            }
        }

        /// <summary>
        /// Returns the <see cref="CliMetadataStringsHeaderAndHeap"/>
        /// which denotes the header information for the strings heap and
        /// the data within the strings heap.
        /// </summary>
        public ICliMetadataStringsHeaderAndHeap StringsHeap
        {
            get
            {
                return this.strings;
            }
        }

        /// <summary>
        /// Returns the <see cref="CliMetadataUserStringsHeaderAndHeap"/> which
        /// denotes the header information for the user strings heap and
        /// the data within the user strings heap.
        /// </summary>
        public ICliMetadataUserStringsHeaderAndHeap UserStringsHeap { get { return this.userStrings; } }

        /// <summary>
        /// Returns the <see cref="CliMetadataGuidHeaderAndHeap"/> which
        /// denotes the header information for the guid heap and the
        /// data within the guid heap.
        /// </summary>
        public ICliMetadataGuidHeaderAndHeap GuidHeap { get { return this.guids; } }

        /// <summary>
        /// Returns the <see cref="CliMetadataBlobHeaderAndHeap"/> which
        /// denotes the header information for the blob heap and the
        /// data within the blob heap.
        /// </summary>
        public ICliMetadataBlobHeaderAndHeap BlobHeap { get { return this.blob; } }

        /// <summary>
        /// Returns the <see cref="CliMetadataTableStreamAndHeader"/>
        /// which denotes the header information for the table stream
        /// and the metadata within the table heap.
        /// </summary>
        public ICliMetadataTableStreamAndHeader TableStream { get { return this.tableStream; } }

        /// <summary>
        /// Reads the root structure from the metadata.
        /// </summary>
        /// <param name="header">The <see cref="CliHeader"/> which denotes the location of the information.</param>
        /// <param name="originalStream">The <see cref="FileStream"/> being read from.</param>
        /// <param name="reader">The <see cref="EndianAwareBinaryReader"/> which handles reads.</param>
        /// <param name="relativeVirtualAddress">The <see cref="UInt32"/> value which denotes the relative
        /// virtual address of the metadata header.</param>
        /// <param name="sourceImage"></param>
        internal void Read(CliHeader header, FileStream originalStream, EndianAwareBinaryReader reader, uint relativeVirtualAddress, PEImage sourceImage)
        {
            this.originalStream = originalStream;
            this.header = header;
            this.streamPosition = relativeVirtualAddress;
            this.signature = reader.ReadUInt32();
            if (this.signature != metadataSignature)
                throw new BadImageFormatException();
            this.depreciatedVersion.Read(reader);
            this.reserved = reader.ReadUInt32();
            this.realVersionLength = reader.ReadInt32();
            byte[] version = new byte[(this.realVersionLength + 3) & ~3];//Make it a multiple of four.
            reader.Read(version, 0, version.Length);
            this.version = version;
            this.reservedFlags = reader.ReadUInt16();
            int streamCount = 0;
            streamCount = reader.ReadUInt16();
            this.sourceImage = sourceImage;
            for (int i = 0; i < streamCount; i++)
            {
                var currentHeader = new CliMetadataStreamHeader();
                currentHeader.Read(reader, sourceImage);
                switch (currentHeader.Name)
                {
                    case "#Strings":
                        if (this.strings != null)
                            goto sectionExists;
                        this.strings = new CliMetadataStringsHeaderAndHeap(currentHeader, sourceImage.SyncObject);
                        this.ScanAndReadSection(sourceImage, strings, this.strings.Read);
                        break;
                    case "#Blob":
                        if (this.blob != null)
                            goto sectionExists;
                        this.blob = new CliMetadataBlobHeaderAndHeap(currentHeader, this);
                        this.ScanAndReadSection(sourceImage, blob, this.blob.Read);
                        break;
                    case "#US":
                        if (this.userStrings != null)
                            goto sectionExists;
                        this.userStrings = new CliMetadataUserStringsHeaderAndHeap(currentHeader, sourceImage.SyncObject);
                        this.ScanAndReadSection(sourceImage, this.userStrings, this.userStrings.Read);
                        break;
                    case "#GUID":
                        if (this.guids != null)
                            goto sectionExists;
                        this.guids = new CliMetadataGuidHeaderAndHeap(currentHeader, sourceImage.SyncObject);
                        this.ScanAndReadSection(sourceImage, guids, this.guids.Read);
                        break;
                    case "#-": //https://github.com/jbevain/cecil/blob/8b689ecdc890cbf3715ba8775de1d713d71f09f3/Mono.Cecil.PE/ImageReader.cs#L359
                    case "#~":
                        if (this.tableStream != null)
                            goto sectionExists;
                        this.tableStream = new CliMetadataTableStreamAndHeader(currentHeader);
                        this.ScanAndReadSection(sourceImage, tableStream, sdReader => this.tableStream.Read(sdReader, this));
                        break;
                }
                continue;
            sectionExists:
                throw new BadImageFormatException(string.Format("Duplicate {0} section encountered.", currentHeader.Name));
            }
            if (this.tableStream == null)
                throw new BadImageFormatException("#~ or #- stream not present in image.");
        }

        private void ScanAndReadSection(PEImage sourceImage, ICliMetadataStreamHeader currentHeader, Action<EndianAwareBinaryReader> readFunc)
        {
            var stringsPEScan = sourceImage.ResolveRelativeVirtualAddress((uint)(this.streamPosition + currentHeader.Offset));
            if (!stringsPEScan.Resolved)
                throw new BadImageFormatException(string.Format("{0} offset not valid.", currentHeader));
            var stringsPESection  = stringsPEScan.Section;
            var offset            = stringsPEScan.Offset;
            var sectionDataReader = stringsPESection.SectionDataReader;
            var originalLocale    = sectionDataReader.BaseStream.Position;
            sectionDataReader.BaseStream.Seek(offset, SeekOrigin.Begin);
            readFunc(sectionDataReader);
            sectionDataReader.BaseStream.Position = originalLocale;
        }

        public void Dispose()
        {
            if (this.isDisposed)
                return;
            if (this.tableStream != null)
            {
                foreach (var table in this.tableStream.Values)
                    table.Dispose();
                this.tableStream._Clear();
            }
            if (this.sourceImage != null)
                this.sourceImage.Dispose();
            if (this.blob != null)
            {
                this.blob.Dispose();
                this.blob = null;
            }
            if (this.strings != null)
            {
                this.strings.Dispose();
                this.strings = null;
            }
            if (this.propertySemantics != null)
                this.propertySemantics = null;
            if (this.eventSemantics != null)
                this.eventSemantics = null;
            this.isDisposed = true;
        }

        public PEImage SourceImage { get { return this.sourceImage; } }

        public IEnumerable<ICliMetadataMethodSemanticsTableRow> FullSemantics
        {
            get
            {
                if (this.fullSemantics == null)
                    if (this.tableStream.MethodSemanticsTable == null)
                        this.fullSemantics = new ICliMetadataMethodSemanticsTableRow[0];
                    else
                        this.fullSemantics = this.tableStream.MethodSemanticsTable.ToArray().GetEnumerable();
                return this.fullSemantics;
            }
        }

        public IEnumerable<ICliMetadataMethodSemanticsTableRow> PropertySemantics
        {
            get
            {
                if (this.propertySemantics == null)
                {
                    this.tableStream.MethodSemanticsTable.Read();
                    this.propertySemantics = (from s in this.TableStream.MethodSemanticsTable
                                              where s.AssociationSource == CliMetadataHasSemanticsTag.Property &&
                                                   (s.Semantics & MethodSemanticsAttributes.Getter | MethodSemanticsAttributes.Setter | MethodSemanticsAttributes.Other) != MethodSemanticsAttributes.None
                                              select s).ToArray().GetEnumerable();
                }
                return this.propertySemantics;
            }
        }

        public IEnumerable<ICliMetadataMethodSemanticsTableRow> EventSemantics
        {
            get
            {
                if (this.eventSemantics == null)
                {
                    this.tableStream.MethodSemanticsTable.Read();
                    this.eventSemantics = (from s in this.TableStream.MethodSemanticsTable
                                           where s.AssociationSource == CliMetadataHasSemanticsTag.Event &&
                                                (s.Semantics & MethodSemanticsAttributes.AddOn | MethodSemanticsAttributes.Fire | MethodSemanticsAttributes.RemoveOn | MethodSemanticsAttributes.Other) != MethodSemanticsAttributes.None
                                           select s).ToArray().GetEnumerable();
                }
                return this.eventSemantics;
            }
        }
    }
}
