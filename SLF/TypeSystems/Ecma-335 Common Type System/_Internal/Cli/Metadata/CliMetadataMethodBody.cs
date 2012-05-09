using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;
using AllenCopeland.Abstraction.IO;
using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata
{
    internal class CliMetadataMethodBody :
        ICliMetadataMethodBody
    {
        CliMetadataRoot metadataRoot;
        private uint rva;
        private Substream bodySubstream;
        private EndianAwareBinaryReader bodyReader;
        public CliMetadataMethodBody(CliMetadataRoot metadataRoot, uint rva)
        {

            var image = metadataRoot.SourceImage;
            this.metadataRoot = metadataRoot;
            var rvaLocationScan = image.ResolveRelativeVirtualAddress(rva);
            if (rvaLocationScan.Resolved)
            {

                var section = rvaLocationScan.Section;
                this.bodySubstream = new Substream(section.SectionData, rvaLocationScan.Offset, 32, false);
                this.bodyReader = new EndianAwareBinaryReader(bodySubstream, Endianness.LittleEndian, false);
                var peekedChar = bodyReader.PeekByte();
                //var peekedChar = bodyReader.ReadByte();
                if (peekedChar != -1)
                {
                    MethodHeaderFlags headerType = ((MethodHeaderFlags) peekedChar) & (MethodHeaderFlags.NarrowFormat | MethodHeaderFlags.WideFormat);
                    if (headerType == MethodHeaderFlags.NarrowFormat)
                        this.ReadNarrow((byte) peekedChar);
                    else
                        this.ReadWide((byte) peekedChar);
                }
                this.rva = rva;
            }
        }

        private void ReadWide(byte firstByte)
        {
            var flagsAndSize = bodyReader.ReadUInt16();
            var flags = ((MethodHeaderFlags)(flagsAndSize & 0x0FFF)) & ~MethodHeaderFlags.WideFormat;
            var size = (flagsAndSize & 0xF000) >> 0xC;
            var maxStack = bodyReader.ReadUInt16();
            var codeSize = bodyReader.ReadUInt32();
            var localVarSigToken = bodyReader.ReadUInt32();
            if (localVarSigToken != 0)
            {
                var sigTableKind = (CliMetadataTableKinds) (1UL << (int)((localVarSigToken & 0xFF000000) >> 24));
                var sigIndex = localVarSigToken & 0x00FFFFFF;
                ICliMetadataTable sigTable;
                if (metadataRoot.TableStream.TryGetValue(sigTableKind, out sigTable))
                {
                    var entry = (ICliMetadataTableRow) sigTable[(int)sigIndex];
                    if (entry is ICliMetadataStandAloneSigTableRow)
                    {
                        var sigEntry = (ICliMetadataStandAloneSigTableRow) entry;
                        
                    }
                }
            }
        }

        private void ReadNarrow(byte firstByte)
        {
            byte nBytes = (byte) ((firstByte & 0xFC) >> 2);

        }
    }
}
