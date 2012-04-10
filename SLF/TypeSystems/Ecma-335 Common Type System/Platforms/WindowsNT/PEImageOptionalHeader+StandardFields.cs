using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.IO;
using System.Runtime.InteropServices;

namespace AllenCopeland.Abstraction.Slf.Platforms.WindowsNT
{
    partial struct PEImageOptionalHeader
    {
        private struct StandardFields
        {

            private PEImageKind imageKind;
            private byte lMajor;
            private byte lMinor;
            private uint codeSize;
            private uint initializedDataSize;
            private uint uninitializedDataSize;
            private uint entryPointRVA;
            private uint baseOfCode;
            private uint baseOfData;

            public PEImageKind ImageKind
            {
                get
                {
                    return this.imageKind;
                }
            }

            public byte LinkerMajorVersion { get { return this.lMajor; } }

            public byte LinkerMinorVersion { get { return this.lMinor; } }

            public uint CodeSize { get { return this.codeSize; } }

            public uint InitializedDataSize { get { return this.initializedDataSize; } }

            public uint UninitializedDataSize { get { return this.uninitializedDataSize; } }

            public uint EntryPointRVA { get { return this.entryPointRVA; } }

            public uint BaseOfCode { get { return this.baseOfCode; } }

            public uint BaseOfData { get { return this.baseOfData; } }

            internal StandardFields(byte lMajor, byte lMinor, uint codeSize, uint initializedDataSize, uint uninitializedDataSize, uint entryPointRVA, uint baseOfCode, uint baseOfData)
            {
                this.imageKind = PEImageKind.x86Image;
                this.lMajor = lMajor;
                this.lMinor = lMinor;
                this.codeSize = codeSize;
                this.initializedDataSize = initializedDataSize;
                this.uninitializedDataSize = uninitializedDataSize;
                this.entryPointRVA = entryPointRVA;
                this.baseOfCode = baseOfCode;
                this.baseOfData = baseOfData;
            }

            internal StandardFields(byte lMajor, byte lMinor, uint codeSize, uint initializedDataSize, uint uninitializedDataSize, uint entryPointRVA, uint baseOfCode)
            {
                this.imageKind = PEImageKind.x64Image;
                this.lMajor = lMajor;
                this.lMinor = lMinor;
                this.codeSize = codeSize;
                this.initializedDataSize = initializedDataSize;
                this.uninitializedDataSize = uninitializedDataSize;
                this.entryPointRVA = entryPointRVA;
                this.baseOfCode = baseOfCode;
                this.baseOfData = 0;
            }

            internal void Read(EndianAwareBinaryReader reader)
            {
                this.imageKind = (PEImageKind)reader.ReadUInt16();
                this.lMajor = reader.ReadByte();
                this.lMinor = reader.ReadByte();
                this.codeSize = reader.ReadUInt32();
                this.initializedDataSize = reader.ReadUInt32();
                this.uninitializedDataSize = reader.ReadUInt32();
                this.entryPointRVA = reader.ReadUInt32();
                this.baseOfCode = reader.ReadUInt32();
                switch (this.imageKind)
                {
                    case PEImageKind.x86Image:
                        this.baseOfData = reader.ReadUInt32();
                        break;
                    case PEImageKind.x64Image:
                        break;
                    case PEImageKind.RomImage:
                        throw new NotImplementedException();
                        break;
                    default:
                        throw new BadImageFormatException();
                        break;
                }
            }

            internal void Write(EndianAwareBinaryWriter writer)
            {
                writer.Write((ushort)this.imageKind);
                writer.Write(this.lMajor);
                writer.Write(this.lMinor);
                writer.Write(this.codeSize);
                writer.Write(this.initializedDataSize);
                writer.Write(this.uninitializedDataSize);
                writer.Write(this.entryPointRVA);
                writer.Write(this.baseOfCode);
                switch (this.imageKind)
                {
                    case PEImageKind.x86Image:
                        writer.Write(this.baseOfData);
                        break;
                    case PEImageKind.x64Image:
                        break;
                    case PEImageKind.RomImage:
                        throw new NotImplementedException();
                        break;
                    default:
                        throw new BadImageFormatException();
                        break;
                }
            }
        }
    }
}