using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Platforms.WindowsNT
{
    public enum PEImageKind :
        ushort
    {
        /// <summary>
        /// The <see cref="PEImage"/> is a 32-bit image targeting the
        /// x86 instruction set.
        /// </summary>
        x86Image = 0x10B,
        /// <summary>
        /// The <see cref="PEImage"/> is a 64-bit image targeting the
        /// x64 instruction set.
        /// </summary>
        x64Image = 0x20B,
        /// <summary>
        /// The <see cref="PEImage"/> represents a Read-Only-Memory image
        /// for an emulated architecture.
        /// </summary>
        RomImage = 0x107,
    }
}
