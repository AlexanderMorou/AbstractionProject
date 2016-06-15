using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using AllenCopeland.Abstraction.Slf._Internal.Cli;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    /// <summary>
    /// Defines common .NET Versions.
    /// </summary>
    /// <remarks><para>These versions are included to relate more closely to the common
    /// view of .NET Versioning.  The primary implementation of .NET is the 
    /// Microsoft .NET CLR; as such, it's this author's opinion that the average
    /// programmer will associate with these values.</para></remarks>
    [Flags]
    public enum CliFrameworkVersion
    {
        /// <summary>
        /// Version 1.0 (1.0.3705/1.0.3300) of the Common
        /// Language Infrastructure.
        /// </summary>
        v1_0_3705      = 1 << 0x8,
        /// <summary>
        /// Version 1.1 (1.1.4322) of the Common Language
        /// Infrastructure.
        /// </summary>
        v1_1_4322      = 1 << 0x9,
        /// <summary>
        /// Version 2.0 (2.0.50727) of the Common Language
        /// Infrastructure where generics were introduced.
        /// </summary>
        v2_0_50727     = 1 << 0xA,
        /// <summary>
        /// Version 3.0 of the Common Language Infrastructure where extension methods were introduced to the flagship compilers
        /// of the framework.
        /// </summary>
        v3_0           = 1 << 0xB,
        /// <summary>
        /// Version 3.5 of the Common Language Infrastructure where language integrated query was introduced as
        /// a sugar coating over the extension methods from version
        /// <see cref="v3_0"/>.
        /// </summary>
        v3_5           = 1 << 0xC,
        /// <summary>
        /// Version 4.0 (4.0.30319) of the Common Language Infrastructure where dynamic interop was introduced
        /// on a framework level.
        /// </summary>
        v4_0_30319     = 1 << 0xD,
        /// <summary>
        /// Version 4.5 of the Common Language Infrastructure.
        /// </summary>
        /// <remarks><see cref="ClientProfile"/> is not valid for version 4.5 of the framework.</remarks>
        v4_5           = 1 << 0xE,
        /// <summary>
        /// Version 4.6 of the Common Language Infrastructure.
        /// </summary>
        /// <remarks><see cref="ClientProfile"/> is not valid for version 4.6 of the framework.</remarks>
        v4_6 = 1 << 0xF,
        /// <summary>
        /// A mask for the versions.
        /// </summary>
        VersionMask    = ((1 << 8) - 1) << 8,
        /// <summary>
        /// The current version of the Common Language
        /// Infrastructure The Abstraction Project was
        /// compiled against.
        /// </summary>
        CurrentVersion = 
#if FV_2_0
            v2_0_50727,
#elif FV_3_5
            v3_5,
#elif FV_3_0
            v3_0,
#elif FV_4_0
            v4_0_30319,
#elif FV_4_5
            v4_5,
#elif FV_4_6
            v4_6,
#endif

        /// <summary>
        /// Client profile flag which denotes that the
        /// trimmed version of the framework is being targeted.
        /// </summary>
        /// <remarks>
        /// Used primarily as a target, there's little data within
        /// the metadata of assemblies that indicates this flag
        /// would be set.
        /// </remarks>
        ClientProfile  = 1,
    }
}


