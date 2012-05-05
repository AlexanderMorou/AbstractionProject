using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;
using AllenCopeland.Abstraction.IO;
using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Members;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata
{
    internal class CliMetadataMethodBody :
        ICliMetadataMethodBody
    {
        private PEImage image;
        private uint rva;
        private Substream bodySubstream;
        private EndianAwareBinaryReader bodyReader;
        public CliMetadataMethodBody(CliMetadataRoot metadataRoot, uint rva)
        {
            this.image = metadataRoot.SourceImage;
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

        }

        private void ReadNarrow(byte firstByte)
        {
            byte nBytes = (byte) ((firstByte & 0xFC) >> 2);

        }
    }
}
