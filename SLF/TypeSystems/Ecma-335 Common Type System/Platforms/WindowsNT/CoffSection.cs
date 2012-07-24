using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using AllenCopeland.Abstraction.IO;
using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Platforms.WindowsNT
{
    [StructLayout(LayoutKind.Sequential)]
    public class CoffSection :
        IDisposable
    {
        private const byte sizeOfName = 0x08;
        private byte[] name;
        private string _name;
        private uint virtualSize;
        private uint virtualAddress;
        private uint sizeOfRawData;
        private uint pointerToRawData;
        private uint pointerToRelocations;
        private uint pointerToLineNumbers;
        private ushort numberOfRelocations;
        private ushort numberOfLineNumbers;
        private CoffSectionCharacteristics characteristics;
        private Substream sectionData;
        private EndianAwareBinaryReader sectionDataReader;
        private bool keepImageOpen;
        /// <summary>
        /// Returns the eight (8) <see cref="Byte"/> null-padded
        /// ascii <see cref="String"/>.
        /// </summary>
        /// <remarks>The <see cref="String"/> that results may contain embedded
        /// null characters as it is exactly eight bytes long.</remarks>
        public string Name
        {
            get
            {
                if (this._name == null)
                {
                    char[] result = new char[name.Length];
                    int index = 0;
                    bool broke = false;
                    for (index = 0; index < result.Length; index++)
                        if (this.name[index] != 0)
                            result[index] = (char) this.name[index];
                        else
                        {
                            broke = true;
                            break;
                        }
                    _name = new string(result, 0, broke ? index : result.Length);
                }
                return this._name;
            }
        }
        /// <summary>
        /// Returns the total size of the section in bytes.  If greater
        /// than <see cref="SizeOfRawData"/> the section will be
        /// padded with null characters.
        /// </summary>
        public uint VirtualSize { get { return this.virtualSize; } }
        /// <summary>
        /// Returns the address of the section when loaded into memory
        /// relative to the image base.
        /// </summary>
        public uint VirtualAddress { get { return this.virtualAddress; } }
        /// <summary>
        /// Returns the size of the initialized data on disk in
        /// bytes, is a multiple of
        /// <see cref="PEImageExtendedHeader.NTFields32.FileAlignment"/>, or
        /// <see cref="PEImageExtendedHeader.NTFields64.FileAlignment"/>
        /// (iif the image is a 64-bit image.)
        /// </summary>
        public uint SizeOfRawData { get { return this.sizeOfRawData; } }
        /// <summary>
        /// Returns the <see cref="Int32"/> value denoting the pointer
        /// to the raw data of the section.
        /// </summary>
        public uint PointerToRawData { get { return this.pointerToRawData; } }
        /// <summary>
        /// Returns the <see cref="Int32"/> value to the relocations
        /// contained within the <see cref="CoffSection"/>.
        /// </summary>
        public uint PointerToRelocations { get { return this.pointerToRelocations; } }

        /// <summary>
        /// Returns the <see cref="Int32"/> value to the line numbers
        /// contained within the <see cref="CoffSection"/>.
        /// </summary>
        public uint PointerToLineNumbers { get { return this.pointerToLineNumbers; } }
        public ushort NumberOfRelocations { get { return this.numberOfRelocations; } }
        public ushort NumberOfLineNumbers { get { return this.numberOfLineNumbers; } }

        public CoffSectionCharacteristics Characteristics { get { return this.characteristics; } }

        internal static CoffSection Read(EndianAwareBinaryReader reader, bool keepImageOpen)
        {
            CoffSection result = new CoffSection();
            result.name = new byte[sizeOfName];
            reader.Read(result.name, 0, sizeOfName);
            result.virtualSize = reader.ReadUInt32();
            result.virtualAddress = reader.ReadUInt32();
            result.sizeOfRawData = reader.ReadUInt32();
            result.pointerToRawData = reader.ReadUInt32();
            result.pointerToRelocations = reader.ReadUInt32();
            result.pointerToLineNumbers = reader.ReadUInt32();
            result.numberOfRelocations = reader.ReadUInt16();
            result.numberOfLineNumbers = reader.ReadUInt16();
            result.characteristics = (CoffSectionCharacteristics) reader.ReadUInt32();
            result.sectionData = new Substream(reader.BaseStream, result.pointerToRawData, result.virtualSize);
            result.keepImageOpen = keepImageOpen;
            //if (bufferSectionData)
            //{
            //    if (!reader.BaseStream.CanSeek)
            //        throw new InvalidOperationException();
            //    ReadSectionData(reader, result);
            //}
            //else
            //    result.sectionDataReader = reader;
            return result;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1:X}b[{4:X}b]@{2:X} ({3})", this.Name, this.SizeOfRawData, this.pointerToRawData, this.Characteristics, this.sectionData.Length);
        }
        
#if FALSE
        private static void ReadSectionData(EndianAwareBinaryReader reader, PEImageSection result)
        {
            long oldLocation = reader.BaseStream.Position;
            reader.BaseStream.Seek(result.pointerToRawData, SeekOrigin.Begin);
            int dataLength;
            byte[] data;
            if (result.VirtualSize != 0)
            {
                if (result.VirtualSize > result.sizeOfRawData)
                {
                    data = new byte[result.virtualSize];
                    reader.Read(data, 0, (int) result.sizeOfRawData);
                }
                else
                {
                    dataLength = (int) result.virtualSize;
                    data = new byte[dataLength];
                    reader.Read(data, 0, dataLength);
                }
            }
            else
            {
                data = new byte[result.sizeOfRawData];
                reader.Read(data, 0, data.Length);
            }
            reader.BaseStream.Seek(oldLocation, SeekOrigin.Begin);
            result.sectionData = new MemoryStream(data, 0, data.Length, false, true);
        }


        internal void Write(EndianAwareBinaryWriter writer)
        {
            writer.Write(this.name, 0, sizeOfName);
            writer.Write(this.virtualSize);
            writer.Write(this.virtualAddress);
            writer.Write(this.sizeOfRawData);
            writer.Write(this.pointerToRawData);
            writer.Write(this.pointerToRelocations);
            writer.Write(this.pointerToLineNumbers);
            writer.Write(this.numberOfRelocations);
            writer.Write(this.numberOfLineNumbers);
            writer.Write((uint) this.characteristics);
            if (!writer.BaseStream.CanSeek)
                throw new InvalidOperationException();

            long oldLocation = writer.BaseStream.Position;
            writer.BaseStream.Seek(this.pointerToRawData, SeekOrigin.Begin);
            if (this.virtualSize == 0)
                writer.Write(this.sectionData.GetBuffer(), 0, (int) this.sizeOfRawData);
            else
            {
                if (this.virtualSize > this.sizeOfRawData)
                    writer.Write(this.sectionData.GetBuffer(), 0, (int) this.sizeOfRawData);
                else
                {
                    byte[] sectionDataCopy = new byte[this.sizeOfRawData];
                    this.sectionData.GetBuffer().CopyTo(sectionDataCopy, 0);
                    writer.Write(sectionDataCopy, 0, (int) this.sizeOfRawData);
                }
            }
            writer.BaseStream.Seek(oldLocation, SeekOrigin.Begin);
        }
#endif

        public Substream SectionData
        {
            get
            {
                return this.sectionData;
            }
        }

        public EndianAwareBinaryReader SectionDataReader
        {
            get
            {
                if (this.sectionDataReader == null)
                    this.sectionDataReader = new EndianAwareBinaryReader(this.SectionData, Endianness.LittleEndian, false);
                return this.sectionDataReader;
            }
        }

        //#region IDisposable Members

        public void Dispose()
        {
            
            if (this.sectionDataReader != null)
            {
                sectionDataReader.Dispose();
                this.sectionDataReader = null;
            }
            if (this.sectionData != null)
            {
                this.sectionData.Close();
                this.sectionData.Dispose();
                this.sectionData = null;
            }
        }

        //#endregion
    }
}
