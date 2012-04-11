using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    /// <summary>
    /// Calling conventions used by method signatures.
    /// </summary>
    public enum CliMetadataMethodSigConventions
    {
        /// <summary>
        /// Default calling convention.
        /// </summary>
        Default = 0x0,
        /// <summary>
        /// C style calling convention.
        /// </summary>
        Cdecl = 0x1,
        /// <summary>
        /// Standard  calling convention used in Win32 API.
        /// </summary>
        StdCall = 0x2,
        /// <summary>
        /// Calling convention used for nonstatic method calls in
        /// unmanaged applications.
        /// </summary>
        ThisCall = 0x3,
        /// <summary>
        /// Calling convention used by various compilers to expedite 
        /// calls to other methods via register population by 
        /// arguments.
        /// </summary>
        FastCall = 0x4,
        /// <summary>
        /// Calling convention for methods which contain a variable
        /// number of arguments in an unmanaged environment.
        /// </summary>
        VariableArguments = 0x5,
        Mask = 0x7,
        /// <summary>
        /// Generic calling convention used by managed architecture 
        /// to allow methods to be differentiated by number of generic
        /// parameters.
        /// </summary>
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
