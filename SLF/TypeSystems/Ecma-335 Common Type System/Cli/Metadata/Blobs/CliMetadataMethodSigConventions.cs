using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    public enum CliMetadataMethodSigConventions
    {
        Default = 0x0,
        Cdecl = 0x1,
        StdCall = 0x2,
        ThisCall = 0x3,
        FastCall = 0x4,
        VariableArguments = 0x5,
        Mask = 0x7,
        Generic = 0x10,
    }
    [Flags]
    public enum CliMetadataMethodSigFlags
    {
        None = 0x0,
        SentinelLowBit = 0x01,
        Sentinel = NativeTypes.Sentinel,
        HasThis = 0x20,
        ExplicitThis = 0x40,
        Mask = 0x61,
    }
}
