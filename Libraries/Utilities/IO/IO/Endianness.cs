using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.IO
{
    /// <summary>
    /// The direction of the bytes or bits of a given system that represents
    /// numbers.
    /// </summary>
    public enum Endianness
    {
        /// <summary>
        /// Little endian maintains that the lowest order value is 
        /// last, and the highest order value is first.
        /// </summary>
        LittleEndian,
        /// <summary>
        /// Big endian maintains that the highest order value is first,
        /// and the lowest order value is last.
        /// </summary>
        BigEndian,
    }
}
