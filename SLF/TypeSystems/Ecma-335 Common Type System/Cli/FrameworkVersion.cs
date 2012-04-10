using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    /// <summary>
    /// Defines common .NET Versions.
    /// </summary>
    public enum FrameworkVersion
    {
        v1_0_3705     = 1 << 8,
        v1_1_4322     = 2 << 8,
        v2_0_50727    = 3 << 8,
        v3_0          = 4 << 8,
        v3_5          = 5 << 8,
        v4_0_30319    = 6 << 8,
        v4_5          = 7 << 8,
        VersionMask   = 7 << 8,
        ClientProfile = 1,
    }
}


