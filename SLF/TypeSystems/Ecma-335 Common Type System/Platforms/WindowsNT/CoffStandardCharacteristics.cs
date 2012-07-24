using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Platforms.WindowsNT
{
    /// <summary>
    /// The characteristics of a standard image.
    /// </summary>
    [Flags]
    public enum CoffStandardCharacteristics :
        ushort
    {
        /// <summary>
        /// Relocation information has been stripped
        /// from the file.
        /// </summary>
        RelocationInformationStripped       = 1 << 00,
        /// <summary>
        /// The <see cref="PEImage"/> described contains
        /// executable code.
        /// </summary>
        ExecutableImage                     = 1 << 01,
        /// <summary>
        /// The <see cref="PEImage"/> described has had
        /// its line numbers stripped.
        /// </summary>
        LineNumbersStripped                 = 1 << 02,
        /// <summary>
        /// The <see cref="PEImage"/> described has had
        /// its symbols stripped.
        /// </summary>
        SymbolsStripped                     = 1 << 03,
        /// <summary>
        /// The <see cref="PEImage"/> described should have
        /// its working set aggressively trimmed because it acts
        /// very sparingly.
        /// </summary>
        AggressiveWorkingSetTrim            = 1 << 04,
        /// <summary>
        /// The <see cref="PEImage"/> described is capable of using
        /// large memory addresses because it is a 64-bit image or
        /// it is a 32-bit application that is large-address aware.
        /// </summary>
        IsLargeAddressAware                 = 1 << 05,
        /// <summary>
        /// The <see cref="PEImage"/> described expects a
        /// 32 bit machine.
        /// </summary>
        Expect32BitMachine                  = 1 << 08,
        /// <summary>
        /// The <see cref="PEImage"/> described has its
        /// debug information stripped.
        /// </summary>
        DebugInfoStripped                   = 1 << 09,
        /// <summary>
        /// The <see cref="PEImage"/> described was not intended
        /// to be ran from a removeable data source, and thus should be
        /// moved to the swap file.
        /// </summary>
        IfRanFromRemoveableSourceUseSwap    = 1 << 10,
        /// <summary>
        /// The <see cref="PEImage"/> described was not intended
        /// to be ran from a network source, and thus should be
        /// moved to the swap file.
        /// </summary>
        IfRanFromNetUseSwap                 = 1 << 11,
        /// <summary>
        /// The <see cref="PEImage"/> described is a system
        /// image.
        /// </summary>
        SystemImage                         = 1 << 12,
        /// <summary>
        /// The <see cref="PEImage"/> described is a dynamic
        /// link library.
        /// </summary>
        DynamicLinkLibrary                  = 1 << 13,
        /// <summary>
        /// The <see cref="PEImage"/> described expects
        /// a single processor machine, and may fail to function properly
        /// if ran on a multi-processor machine.
        /// </summary>
        SingleProcessorMachine              = 1 << 14,
    }
}
