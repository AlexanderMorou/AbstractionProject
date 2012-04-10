using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Platforms.WindowsNT
{
    //http://msdn.microsoft.com/en-us/library/windows/desktop/ms680339%28v=vs.85%29.aspx
    /// <summary>
    /// The subsystem utilized by the portable executable image,
    /// whether it's console based, graphical user interface based,
    /// native to the system and lives in the background, or provides
    /// a service of some kind.
    /// </summary>
    public enum PEImageSubsystem :
        ushort
    {
        /// <summary>
        /// The <see cref="PEImage"/>  lives in the background and thus
        /// its subsystem has no interactive aspect.
        /// </summary>
        Native = 0x1,
        /// <summary>
        /// The <see cref="PEImage"/>  lives in the foreground
        /// and potentially displays dialogs and other user interface
        /// elements.
        /// </summary>
        Gui = 0x2,
        /// <summary>
        /// The <see cref="PEImage"/> lives in the foreground using a
        /// windows console interface (CUI).
        /// </summary>
        Cui = 0x3,
        /// <summary>
        /// The <see cref="PEImage"/> is an OS2 console user interface
        /// application (CUI).
        /// </summary>
        OS2 = 0x5,
        /// <summary>
        /// The <see cref="PEImage"/> is a POSIX Console user interface
        /// (CUI) application.
        /// </summary>
        PosixCui = 0x7,
        /// <summary>
        /// The <see cref="PEImage"/> is a Windows CE graphical user
        /// interface (GUI) application.
        /// </summary>
        CeGui = 0x9,
        /// <summary>
        /// The <see cref="PEImage"/> is an Extensible Firmware Interface
        /// application.
        /// </summary>
        EfiApplication,
        /// <summary>
        /// The <see cref="PEImage"/> is an Extensible Firmware Interface
        /// driver application with boot services.
        /// </summary>
        EfiBootServiceDriver,
        /// <summary>
        /// The <see cref="PEImage"/> is an Extensible Firmware Interface
        /// driver application with run-time services.
        /// </summary>
        EfiRuntimeDriver,
        /// <summary>
        /// The <see cref="PEImage"/> is an Extensible Firmware Interface
        /// ROM image.
        /// </summary>
        EfiRom,
        /// <summary>
        /// The <see cref="PEImage"/> is an XBox architecture image.
        /// </summary>
        XBox,
        /// <summary>
        /// The <see cref="PEImage"/> is a boot application.
        /// </summary>
        BootApplication = 16,
    }
}
