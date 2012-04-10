using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    [Flags]
    public enum CliRuntimeFlags :
        uint
    {
        /// <summary>
        /// The image only contains intermediate language
        /// members.
        /// </summary>
        IntermediateLanguageOnly = 1 << 00,
        /// <summary>
        /// The image targets a 32-bit architecture, as such
        /// this restricts 64-bit environments from loading the
        /// library.
        /// </summary>
        Requires32BitProcess     = 1 << 01,
        /// <summary>
        /// The image has been signed with a strong name.
        /// </summary>
        StrongNameSigned         = 1 << 03,
        /// <summary>
        /// The image has a native entrypoint defined.
        /// </summary>
        NativeEntrypointDefined  = 1 << 04,
        /// <summary>
        /// The runtime environment should track debug blobCacheData.
        /// </summary>
        TrackDebugData           = 1 << 16,
    }
}
