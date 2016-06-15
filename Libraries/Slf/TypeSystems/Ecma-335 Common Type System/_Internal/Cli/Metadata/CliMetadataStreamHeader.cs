using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.IO;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata
{
    internal class CliMetadataStreamHeader :
        ICliMetadataStreamHeader
    {
        private uint offset;
        private uint size;
        private byte[] name;
        private string _name;

        public CliMetadataStreamHeader()
        {
        }

        internal Tuple<uint, uint, String> GetData()
        {
            return new Tuple<uint, uint, string>(this.Offset, this.Size, this.Name);
        }

        protected CliMetadataStreamHeader(CliMetadataStreamHeader header)
        {
            this.offset = header.offset;
            this.size = header.size;
            this.name = header.name;
            this._name = header._name;
        }

        /// <summary>
        /// Returns the <see cref="UInt32"/> value which denotes the
        /// start of the stream relative to the <see cref="CliMetadataFixedRoot"/>.
        /// </summary>
        public uint Offset
        {
            get
            {
                return this.offset;
            }
        }

        /// <summary>
        /// Returns the <see cref="UInt32"/> value which denotes
        /// the size of the stream, in bytes.
        /// </summary>
        /// <remarks>Shall be a multiple of four (4.)</remarks>
        public virtual uint Size
        {
            get
            {
                return this.size;
            }
        }

        /// <summary>
        /// Returns the <see cref="String"/> value which denotes
        /// the name of the stream.
        /// </summary>
        public string Name
        {
            get
            {
                if (this._name == null)
                {
                    char[] resultName = new char[this.name.Length];
                    int index = 0;
                    bool broke = false;
                    for (index = 0; index < resultName.Length; index++)
                        if (this.name[index] == '\0')
                        {
                            broke = true;
                            break;
                        }
                        else
                            resultName[index] = (char) this.name[index];
                    this._name = new string(resultName, 0, broke ? index : this.name.Length);
                }
                return this._name;
            }
        }

        internal void Read(EndianAwareBinaryReader reader, PEImage sourceImage)
        {
            this.offset = reader.ReadUInt32();
            this.size = reader.ReadUInt32();
            byte[] name = new byte[32];
            bool broke = false;
            int index = 0;
            for (index = 0; index < name.Length; index++)
            {
                name[index] = reader.ReadByte();
                if (name[index] == '\0')
                {
                    broke = true;
                    index++;
                    break;
                }
            }
            if (broke)
            {
                int remainder = index & 3; // aka: index % 4
                int bytesRemaining = 4 - remainder;
                if (remainder > 0)
                {
                    for (int i = 0; i < bytesRemaining; i++)
                        reader.ReadByte();
                    int paddedLength = index + bytesRemaining;
                    this.name = new byte[paddedLength];
                    Array.ConstrainedCopy(name, 0, this.name, 0, paddedLength);
                }
                else
                {
                    this.name = new byte[index];
                    Array.ConstrainedCopy(name, 0, this.name, 0, index);
                }
            }
            else
                this.name = name;
        }

        public void Write(EndianAwareBinaryWriter writer)
        {
            writer.Write(this.offset);
            writer.Write(this.size);
            int nameLength = this.name.Length;
            if ((nameLength & 3) == 0)
                writer.Write(this.name, 0, nameLength);
            else
            {
                byte[] name = new byte[(nameLength + 3) << 2 >> 2];
                Array.ConstrainedCopy(this.name, 0, name, 0, nameLength);
                writer.Write(name, 0, name.Length);
            }
        }

        public override string ToString()
        {
            return string.Format("{0} section - {1} bytes at {2}", this.Name, this.Size, this.Offset);
        }
    }
}
