using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Platforms.WindowsNT
{
    // http://mobius.sourceforge.net/documentation/kernel/pe_8h-source.php
    // http://msdn.microsoft.com/en-us/library/windows/desktop/ms680313%28v=vs.85%29.aspx
    /// <summary>
    /// The kind of machine the <see cref="PEImage"/>
    /// targets.
    /// </summary>
    public enum PEImageMachine :
        ushort
    {
        /// <summary>
        /// The machine the <see cref="PEImage"/> targets
        /// is unknown.
        /// </summary>
        Unknown = 0x00,
        /// <summary>
        /// The machine targets the i386 machine, which is
        /// what most PCs are defined as today.
        /// </summary>
        i386 = 0x14C,
        /// <summary>
        /// Targets the Reduced Instruction Set Computer 
        /// R3000 machine (circa 1988) by MIPS Computer
        /// Systems using big endian word byte addressing
        /// format.
        /// </summary>
        R3000BE = 0x160,
        /// <summary>
        /// Targets the Reduced Instruction Set Computer 
        /// R3000 machine (circa 1988) by MIPS Computer
        /// Systems using little endian word byte addressing
        /// format.
        /// </summary>
        R3000LE = 0x162,
        /// <summary>
        /// Targets the Reduced Instruction Set Computer
        /// R4000 machine (circa 1991) by MIPS Computer
        /// Systems using little endian word byte addressing
        /// format.
        /// </summary>
        R4000 = 0x166,
        /// <summary>
        /// Targets the Reduced Instruction Set Computer
        /// R10000 machine (circa 1996) by MIPS Computer
        /// Systems using little endian word byte addressing
        /// format.
        /// </summary>
        R10000 = 0x168,
        /// <summary>
        /// Targets the Alpha AXP 64-bit processor by digital equipment 
        /// corporation using bi-endian word byte addressing
        /// formats.
        /// </summary>
        AlphaAXP = 0x184,
        /// <summary>
        /// Targets the PowerPC runtimeEnvironment by International Business
        /// Machines (IBM).
        /// </summary>
        PowerPC = 0x1F0,
        /// <summary>
        /// Targets the Intel Itanium runtimeEnvironment using selectable
        /// endianness word byte addressing format.
        /// </summary>
        Itanium = 0x200,
    }
}
