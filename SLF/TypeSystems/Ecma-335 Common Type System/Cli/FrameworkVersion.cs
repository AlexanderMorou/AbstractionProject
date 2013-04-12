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
        v1_0_3705      = 1 << 8,
        v1_1_4322      = 2 << 8,
        v2_0_50727     = 3 << 8,
        v3_0           = 4 << 8,
        v3_5           = 5 << 8,
        v4_0_30319     = 6 << 8,
        v4_5           = 7 << 8,
        VersionMask    = 7 << 8,
        CurrentVersion = v4_5,
        ClientProfile  = 1,
    }
}


