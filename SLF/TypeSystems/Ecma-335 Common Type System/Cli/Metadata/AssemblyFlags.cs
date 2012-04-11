using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    /// <summary>
    /// The flags applied to assembly rows entered into
    /// a <see cref="CliMetadataAssemblyTable"/>.
    /// </summary>
    public enum CliMetadataAssemblyFlags :
        uint
    {
        /// <summary>
        /// The metadata of the <see cref="ICliAssembly"/>
        /// contains the full unhashed public key.
        /// </summary>
        PublicKey                   = 0x0001,
        /// <summary>
        /// The metadata of the <see cref="ICliAssembly"/>
        /// is retargetable.
        /// </summary>
        Retargetable                = 0x0100,
        /// <summary>
        /// The CLI implementation should disable the optimizations
        /// performed by the Just-in-Time (JIT) Compiler, a conforming
        /// implementation can ignore this flag.
        /// </summary>
        DisableJITOptimizer         = 0x4000,
        /// <summary>
        /// The CLI implementation should create a Common Intermediate Language
        /// to native code mapping for debugging purposes, a conforming
        /// implementation can ignore this flag.
        /// </summary>
        EnableJITCompileTracking    = 0x8000,
    }
}
