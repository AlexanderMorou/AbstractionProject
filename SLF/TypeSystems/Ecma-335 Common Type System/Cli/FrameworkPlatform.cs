﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    /// <summary>
    /// The kind of runtimeEnvironment targeted by a <see cref="ICliManager"/>.
    /// </summary>
    public enum FrameworkPlatform :
        ushort
    {
        /// <summary>
        /// The expected <see cref="FrameworkPlatform"/> for a given
        /// <see cref="CliManager"/> is the 32-bit machine for the
        /// x86 instruction set.
        /// </summary>
        /// <remarks>See also: <seealso cref="PEImageKind.x86Image"/>.</remarks>
        x86Platform = PEImageKind.x86Image,
        /// <summary>
        /// The expected <see cref="FrameworkPlatform"/> for a given
        /// <see cref="CliManager"/> is the 64-bit machine for the
        /// x64 instruction set.
        /// </summary>
        /// <remarks>See also: <seealso cref="PEImageKind.x64Image"/>.</remarks>
        x64Platform = PEImageKind.x64Image,
    }
}