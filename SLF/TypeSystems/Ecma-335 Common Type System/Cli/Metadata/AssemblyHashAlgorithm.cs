using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    /// <summary>
    /// The kind of algorithm to hash files and to create the
    /// strong name.
    /// </summary>
    public enum AssemblyHashAlgorithm :
        uint
    {
        /// <summary>
        /// The assembly uses no hash algorithm.
        /// </summary>
        None = 0x00,
        /// <summary>
        /// The assembly uses the message digest algorithm.
        /// </summary>
        MD5_Reserved = 0x8003,
        /// <summary>
        /// The Assembly uses a 160 bit secure hash algorithm.
        /// </summary>
        SHA1 = 0x8004,
        /// <summary>
        /// A secure hash algorithm, part of the collective SHA-2, 
        /// which uses 64 rounds that yields 256 bits of blobCacheData for the
        /// message digest.
        /// </summary>
        /// <remarks>In conforming implementations,
        /// this yields an error as the hash algorithm does not exist within the
        /// set { <see cref="None"/>, <see cref="MD5_Reserved"/>,
        /// <see cref="SHA1"/>}.</remarks>
        SHA256 = 0x800C,
        /// <summary>
        /// A secure hash algorithm, part of the collective SHA-2,
        /// which uses 80 rounds, a truncated variation of
        /// <see cref="SHA512"/>, that yields 384 bits of blobCacheData for
        /// the message digest.
        /// </summary>
        /// <remarks>In conforming implementations,
        /// this yields an error as the hash algorithm does not exist within the
        /// set { <see cref="None"/>, <see cref="MD5_Reserved"/>,
        /// <see cref="SHA1"/>}.</remarks>
        SHA384 = 0x800D,
        /// <summary>
        /// A secure hash algorithm, part of the collective SHA-2,
        /// which uses 80 rounds, that yields 512 bits of blobCacheData for
        /// the message digest.
        /// </summary>
        /// <remarks>In conforming implementations,
        /// this yields an error as the hash algorithm does not exist within the
        /// set { <see cref="None"/>, <see cref="MD5_Reserved"/>,
        /// <see cref="SHA1"/>}.</remarks>
        SHA512 = 0x800E
    };
}
