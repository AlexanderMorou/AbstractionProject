using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.IO;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Numerics;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables
{
    internal class CliMetadataMethodHeader :
        ICliMetadataMethodHeader
    {
        private ICliMetadataLocalVarSignature locals;
        private MethodHeaderFlags flags;
        private uint codeSize;
        private CliMetadataMethodExceptionTable exceptionTable;
        private byte[] methodBody;
        private ushort maxStack;
        private byte headerSize;

        internal CliMetadataMethodHeader(ICliMetadataRoot metadataRoot, uint relativeVirtualAddress, Action<byte[]> bodyBuilder)
        {
            var image = metadataRoot.SourceImage;
            var rvaLocationScan = image.ResolveRelativeVirtualAddress(relativeVirtualAddress);
            if (rvaLocationScan.Resolved)
            {
                var section = rvaLocationScan.Section;
                var bodySubstream = new Substream(section.SectionData, rvaLocationScan.Offset, 65536, false);
                var bodyReader = new EndianAwareBinaryReader(bodySubstream, Endianness.LittleEndian, false);
                var peekedChar = bodyReader.PeekByte();
                if (peekedChar != -1)
                {
                    MethodHeaderFlags headerType = ((MethodHeaderFlags)peekedChar) & MethodHeaderFlags.WideFormat;
                    if (headerType == MethodHeaderFlags.NarrowFormat)
                        this.ReadNarrow(bodyReader, metadataRoot, bodyBuilder);
                    else
                        this.ReadWide(bodyReader, metadataRoot, bodyBuilder);
                }
            }
        }
        private void ReadWide(EndianAwareBinaryReader reader, ICliMetadataRoot metadataRoot, Action<byte[]> bodyBuilder)
        {
            var flagsAndSize = reader.ReadUInt16();
            this.flags = ((MethodHeaderFlags)(flagsAndSize & 0x0FFF)) & ~MethodHeaderFlags.WideFormat;
            this.headerSize = (byte)((flagsAndSize & 0xF000) >> 0xA);
            this.maxStack = reader.ReadUInt16();
            this.codeSize = reader.ReadUInt32();
            var localVarSigToken = reader.ReadUInt32();
            if (localVarSigToken != 0)
            {
                var sigTableKind = (CliMetadataTableKinds)(1UL << (int)((localVarSigToken & 0xFF000000) >> 24));
                var sigIndex = localVarSigToken & 0x00FFFFFF;
                ICliMetadataTable table;
                if (metadataRoot.TableStream.TryGetValue(sigTableKind, out table))
                {
                    if (table is ICliMetadataStandAloneSigTable)
                    {
                        ICliMetadataStandAloneSigTable sigTable = (ICliMetadataStandAloneSigTable)table;
                        var entry = sigTable[(int)sigIndex];
                        if (entry.Signature is ICliMetadataLocalVarSignature)
                        {
                            var sigEntry = (ICliMetadataLocalVarSignature)entry.Signature;
                            this.locals = sigEntry;
                        }
                    }
                }
                long codePosition = reader.BaseStream.Position;
                bodyBuilder(reader.ReadBytes((int)this.codeSize));
                if ((reader.BaseStream.Position % 4) != 0)
                    reader.BaseStream.Position += 4 - reader.BaseStream.Position % 4;
                var ehTable = new byte[0];
                if ((flags & MethodHeaderFlags.ContainsMoreSections) == MethodHeaderFlags.ContainsMoreSections)
                {
                readSection:
                    MethodHeaderSectionFlags sectionFlags = (MethodHeaderSectionFlags)reader.ReadByte();
                    var smallFormat = ((sectionFlags & MethodHeaderSectionFlags.FatFormat) != MethodHeaderSectionFlags.FatFormat);
                    int dataSize = 0;
                    if (smallFormat)
                    {
                        dataSize = reader.ReadByte();
                        reader.ReadUInt16();
                    }
                    else
                        dataSize = (int)new BitVector(new byte[] { reader.ReadByte(), reader.ReadByte(), reader.ReadByte() }).GetUInt32Nibbits(0, 24);
                    if ((sectionFlags & MethodHeaderSectionFlags.ExceptionHandlerTable) == MethodHeaderSectionFlags.ExceptionHandlerTable)
                        ehTable = ehTable.AddInlineArray(reader.ReadBytes(dataSize - 4));
                    else
                        reader.BaseStream.Position += dataSize - 4;
                    if ((sectionFlags & MethodHeaderSectionFlags.ContainsMoreSections) == MethodHeaderSectionFlags.ContainsMoreSections)
                        goto readSection;
                }
                if (ehTable.Length > 0)
                    this.exceptionTable = new CliMetadataMethodExceptionTable(ehTable);
                reader.BaseStream.Position = codePosition;
            }
        }

        private void ReadNarrow(EndianAwareBinaryReader reader, ICliMetadataRoot metadataRoot, Action<byte[]> bodyBuilder)
        {
            var firstByte = reader.ReadByte();
            this.codeSize = (byte)((firstByte & 0xFC) >> 2);
            bodyBuilder(reader.ReadBytes((int)this.CodeSize));
        }

        public ICliMetadataLocalVarSignature LocalVariables
        {
            get { return this.locals; }
        }

        public MethodHeaderFlags Flags
        {
            get { return this.flags; }
        }

        public ICliMetadataMethodExceptionTable ExceptionTable
        {
            get { return this.exceptionTable; }
        }

        public uint CodeSize
        {
            get { return this.codeSize; }
        }

        internal byte[] BodyData { get { return this.methodBody; } }

        public ushort MaxStack { get { return this.maxStack; } }

        public byte HeaderSize { get { return this.headerSize; } }
    }
}
