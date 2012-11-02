using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    /// <summary>
    /// Provides information about the target of a platform
    /// invoke call and the character set of the target.
    /// </summary>
    public enum PlatformInvokeCharacterSet
    {
        NoMangle = 0x01,
        NotSpecified = 0x00,
        Ansi = 0x02,
        Unicode = 0x04,
        Automatic = 0x06,
        SupportsLastError = 0x40,
    }

    /// <summary>
    /// Provides information about the target of a platform
    /// invoke call and the calling convention to use to make
    /// the call.
    /// </summary>
    public enum PlatformInvokeCallingConvention
    {
        PlatformAPI = 0x1,
        Cdecl = 0x2,
        StdCall = 0x3,
        ThisCall = 0x4,
        FastCall = 0x5,
    }
}
