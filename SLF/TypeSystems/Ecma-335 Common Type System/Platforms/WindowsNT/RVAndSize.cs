using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.IO;

namespace AllenCopeland.Abstraction.Slf.Platforms.WindowsNT
{
    /// <summary>
    /// Provides a relative virtual address and size
    /// for a given unit of information.
    /// </summary>
    public struct RVAndSize
    {
        /// <summary>
        /// Data member for <see cref="RelativeVirtualAddress"/>.
        /// </summary>
        uint relativeVirtualAddress;
        /// <summary>
        /// Data member for <see cref="Size"/>.
        /// </summary>
        uint size;

        /// <summary>
        /// Creates a new <see cref="RVAndSize"/> with the
        /// <paramref name="relativeVirtualAddress"/> and 
        /// <paramref name="size"/>
        /// </summary>
        /// <param name="relativeVirtualAddress">The <see cref="UInt32"/>
        /// value which denotes the relative virtual address represented
        /// by the <see cref="RVAndSize"/>.</param>
        /// <param name="size">The <see cref="UInt32"/> value which
        /// denotes the size of the target of the 
        /// <see cref="RVAndSize"/>.</param>
        public RVAndSize(uint relativeVirtualAddress, uint size)
        {
            this.relativeVirtualAddress = relativeVirtualAddress;
            this.size = size;
        }

        /// <summary>
        /// Returns the <see cref="UInt32"/> value which denotes
        /// the relative virtual address of the information pointed
        /// to by the <see cref="RVAndSize"/>.
        /// </summary>
        public uint RelativeVirtualAddress
        {
            get
            {
                return this.relativeVirtualAddress;
            }
        }

        /// <summary>
        /// Returns the <see cref="UInt32"/> value which denotes the
        /// number of bytes represented by the blobCacheData pointed to by the
        /// <see cref="RVAndSize"/>.
        /// </summary>
        public uint Size
        {
            get
            {
                return this.size;
            }
        }

        internal void Read(EndianAwareBinaryReader reader)
        {
            this.relativeVirtualAddress = reader.ReadUInt32();
            this.size = reader.ReadUInt32();
        }

        internal void Write(EndianAwareBinaryWriter writer)
        {
            writer.Write(this.relativeVirtualAddress);
            writer.Write(this.size);
        }

        public override string ToString()
        {
            return string.Format("{0:x}b@{1:x}", this.size, this.relativeVirtualAddress);
        }
    }
}
