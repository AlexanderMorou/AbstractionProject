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
        /// The machine the <see cref="PEImage"/> targets is unknown.
        /// </summary>
        /// <remarks>Original Name: IMAGE_FILE_MACHINE_UNKNOWN</remarks>
        Unknown = 0x00,
        /// <summary>
        /// Targets the CE-Based system AM33 for AV Equipment.
        /// </summary>
        /// <remarks><para>Original Name: IMAGE_FILE_MACHINE_AM33</para><para>http://www.prnewswire.com/news-releases/matsushita-electric-industrial-co-and-microsoft-agree-to-combine-efforts-on-digital-audio-video-and-pc-technologies-as-part-of-expanded-relationship-75659297.html</para></remarks>
        MatsushitaAm33 = 0x1D3,
        /// <summary>
        /// Targets 64-bit architecture.
        /// </summary>
        /// <remarks>Original Name: IMAGE_FILE_MACHINE_AMD64</remarks>
        x64 = 0x8664,
        /// <summary>
        /// Targets the Acorn RISC Machine (ARM) machine in little endian.
        /// </summary>
        /// <remarks>Original Name: IMAGE_FILE_MACHINE_ARM</remarks>
        Arm = 0x1c0,
        /// <summary>
        /// Targets the Acorn RISC Machine (ARM) v7, or higher, Thumb mode only.
        /// </summary>
        /// <remarks>Original Name: IMAGE_FILE_MACHINE_ARMNT</remarks>
        ArmNT = 0x1c4,
        /// <summary>
        /// Targets the Acorn RISC Machine (ARM) v8 in 64-bit mode.
        /// </summary>
        /// <remarks>Original Name: IMAGE_FILE_MACHINE_ARM64</remarks>
        Arm64 = 0xaa64,
        /// <summary>
        /// The machine targets the i386 machine, which is
        /// what most PCs are defined as today.
        /// </summary>
        /// <remarks>Original Name: IMAGE_FILE_MACHINE_I386</remarks>
        i386 = 0x14C,
        /// <summary>
        /// Targets the Reduced Instruction Set Computer R3000 machine (circa 1988) by MIPS Computer
        /// Systems using big endian word byte addressing format.
        /// </summary>
        /// <remarks>Original Name: </remarks>
        R3000BE = 0x160,
        /// <summary>
        /// Targets the Reduced Instruction Set Computer 
        /// R3000 machine (circa 1988) by MIPS Computer
        /// Systems using little endian word byte addressing
        /// format.
        /// </summary>
        /// <remarks>Original Name: </remarks>
        R3000LE = 0x162,
        /// <summary>
        /// Targets the Reduced Instruction Set Computer
        /// R4000 machine (circa 1991) by MIPS Computer
        /// Systems using little endian word byte addressing
        /// format.
        /// </summary>
        /// <remarks>Original Name: </remarks>
        R4000 = 0x166,
        /// <summary>
        /// Targets the Reduced Instruction Set Computer
        /// R10000 machine (circa 1996) by MIPS Computer
        /// Systems using little endian word byte addressing
        /// format.
        /// </summary>
        /// <remarks>Original Name: </remarks>
        R10000 = 0x168,
        /// <summary>
        /// Targets the Alpha AXP 64-bit processor by digital equipment 
        /// corporation using bi-endian word byte addressing
        /// formats.
        /// </summary>
        /// <remarks>Original Name: </remarks>
        AlphaAXP = 0x184,
        /// <summary>
        /// Targets the PowerPC platform by International Business
        /// Machines (IBM).
        /// </summary>
        /// <remarks>Original Name: </remarks>
        PowerPC = 0x1F0,
        /// <summary>
        /// Targets the Intel Itanium platform using selectable
        /// endianness word byte addressing format.
        /// </summary>
        /// <remarks>Original Name: </remarks>
        Itanium = 0x200,
    }
}
