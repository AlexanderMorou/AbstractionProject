using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Platforms.WindowsNT
{
    //http://msdn.microsoft.com/en-us/library/windows/desktop/ms680339%28v=vs.85%29.aspx
    /// <summary>
    /// Defines characteristics of a dynamic link
    /// library.
    /// </summary>
    [Flags]
    public enum PEImageDllCharacteristics :
        ushort
    {
        /// <summary>
        /// The <see cref="PEImage"/>
        /// dynamic link library can be relocated
        /// at any time.
        /// </summary>
        DynamicBase             = 1 << 06,
        /// <summary>
        /// Forces an integrity check on the PE
        /// Image.
        /// </summary>
        ForceIntegrity          = 1 << 07,
        /// <summary>
        /// The <see cref="PEImage"/> is compatible
        /// with data execution prevention (DEP.)
        /// </summary>
        DEPCompatible           = 1 << 08,
        /// <summary>
        /// The <see cref="PEImage"/> is aware of
        /// isolation but should not be isolated. 
        /// </summary>
        DoNotIsolate            = 1 << 9,
        /// <summary>
        /// No structured exception handling is
        /// used.
        /// </summary>
        NoSEHUsed               = 1 << 10,
        /// <summary>
        /// Do not bind the image.
        /// </summary>
        DoNotNoBind             = 1 << 11,
        /// <summary>
        /// The <see cref="PEImage"/> is a windows
        /// driver model driver.
        /// </summary>
        Driver                  = 1 << 13,
        /// <summary>
        /// The <see cref="PEImage"/> is terminal
        /// server aware.
        /// </summary>
        TerminalServerAware     = 1 << 15,
    }
}
