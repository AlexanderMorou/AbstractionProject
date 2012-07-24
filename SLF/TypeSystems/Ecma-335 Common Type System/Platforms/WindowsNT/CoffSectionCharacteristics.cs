using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Platforms.WindowsNT
{
    /// <summary>
    /// Defines the characteristics of a given
    /// <see cref="CoffSection"/>.
    /// </summary>
    [Flags]
    public enum CoffSectionCharacteristics :
        uint
    {
        /// <summary>
        /// The <see cref="CoffSection"/> contains code.
        /// </summary>
        ContainsCode                = 1 << 5,
        /// <summary>
        /// The <see cref="CoffSection"/> contains initialized
        /// data.
        /// </summary>
        ContainsInitializedData     = 1 << 6,
        /// <summary>
        /// The <see cref="CoffSection"/> contains uninitialized
        /// data.
        /// </summary>
        ContainsUninitializedData   = 1 << 7,
        /// <summary>
        /// The <see cref="CoffSection"/> contains data
        /// associated to the linking process.
        /// </summary>
        LinkerInfo                  = 1 << 9,
        /// <summary>
        /// The <see cref="CoffSection"/> contains data
        /// associated to the linking process that is supposed
        /// to be omitted when the <see cref="PEImage"/> is created.
        /// </summary>
        LinkRemove                  = 1 << 11,
        /// <summary>
        /// The <see cref="CoffSection"/> contains data
        /// associated to common blocks of code.
        /// </summary>
        /// <remarks>
        /// Certain compilers use this information to compare 
        /// methods at an instruction level, later reducing
        /// the working set by merging the identities under
        /// a common name.
        /// </remarks>
        LinkContainsCommonBlockData = 01 << 12,
        GlobalPointerRelative       = 01 << 15,
        AlignTo1Byte                = 01 << 20,
        AlignTo2Bytes               = 02 << 20,
        AlignTo4Bytes               = 03 << 20,
        AlignTo8Bytes               = 04 << 20,
        AlignTo16Bytes              = 05 << 20, //Default if none specified.
        AlignTo32Bytes              = 06 << 20,
        AlignTo64Bytes              = 07 << 20,
        AlignTo128Bytes             = 08 << 20,
        AlignTo256Bytes             = 09 << 20,
        AlignTo512Bytes             = 10 << 20,
        AlignTo1024Bytes            = 11 << 20,
        AlignTo2048Bytes            = 12 << 20,
        AlignTo4096Bytes            = 13 << 20,
        AlignTo8192Bytes            = 14 << 20,
        AlignmentMask               = 15 << 20,
        /// <summary>
        /// The number of relocations contained within the 
        /// <see cref="CoffSection"/> exceeds the 16-bit boundary.
        /// </summary>
        /// <remarks><para>The <see cref="CoffSection.NumberOfRelocations"/>
        /// is <see cref="UInt16.MaxValue"/> and the actual 
        /// relocation count is stored in the VirtualAddress of the
        /// first relocation.</para>
        /// <para>It is an error if <see cref="RelocationOverflow"/>
        /// is set and <see cref="CoffSection.NumberOfRelocations"/>
        /// is less than <see cref="UInt16.MaxValue"/></para></remarks>
        RelocationOverflow          = 01 << 24,
        /// <summary>
        /// The <see cref="CoffSection"/> can be discarded as needed.
        /// </summary>
        MemoryIsDiscardable         = 01 << 25,
        /// <summary>
        /// The memory for the <see cref="CoffSection"/> cannot be cached.
        /// </summary>
        MemoryNotCached             = 01 << 26,
        /// <summary>
        /// The Memory for the <see cref="CoffSection"/>
        /// cannot be paged.
        /// </summary>
        MemoryCannotBePaged         = 01 << 27,
        /// <summary>
        /// The memory for the <see cref="CoffSection"/> can be shared
        /// in memory.
        /// </summary>
        MemoryCanBeShared           = 01 << 28,
        /// <summary>
        /// The <see cref="CoffSection"/> at runtime can be
        /// executed.
        /// </summary>
        MemoryIsExecutable          = 1 << 29,
        /// <summary>
        /// The <see cref="CoffSection"/> at runtime can be
        /// read from.
        /// </summary>
        MemoryIsReadable            = 1 << 30,
        /// <summary>
        /// The <see cref="CoffSection"/> at runtime can be
        /// written to.
        /// </summary>
        /// <remarks>The data being written to is a copy of the
        /// <see cref="CoffSection"/>, any changes won't be
        /// persisted across program sessions.</remarks>
        MemoryIsWritable            = 1U << 31,
    }
}
