using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    /// <summary>
    /// The kind of platform targeted by a <see cref="ICliManager"/>.
    /// </summary>
    public enum CliFrameworkPlatform :
        ushort
    {
        /// <summary>
        /// The exptected <see cref="CliFrameworkPlatform"/> for a given
        /// <see cref="ICliManager"/> is any platform, be it
        /// x86, x64 or otherwise.
        /// </summary>
        AnyPlatform = 0x00B,
        /// <summary>
        /// The expected <see cref="CliFrameworkPlatform"/> for a given
        /// <see cref="ICliManager"/> is the 32-bit machine for the
        /// x86 instruction set.
        /// </summary>
        /// <remarks>See also: <seealso cref="PEImageKind.x86Image"/>.</remarks>
        x86Platform = PEImageKind.x86Image,
        /// <summary>
        /// The expected <see cref="CliFrameworkPlatform"/> for a given
        /// <see cref="ICliManager"/> is the 64-bit machine for the
        /// x64 instruction set.
        /// </summary>
        /// <remarks>See also: <seealso cref="PEImageKind.x64Image"/>.</remarks>
        x64Platform = PEImageKind.x64Image,
#if x86
#if PLATFORM_ANY
        AbstractionPlatform = AnyPlatform,
#else
        AbstractionPlatform = x86Platform,
#endif
#elif x64
        AbstractionPlatform = x64Platform,
#endif
    }
}
