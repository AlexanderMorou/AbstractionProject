using AllenCopeland.Abstraction.IO;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Platforms.WindowsNT
{
    public class CoffRelocBlock :
        ControlledCollection<CoffRelocEntry>
    {
        private RVAndSize _relativeVirtualAddressAndSize;
        public RVAndSize RelativeVirtualAddressAndSize { get { return this._relativeVirtualAddressAndSize; } }
        public unsafe void Read(EndianAwareBinaryReader reader)
        {
            this._relativeVirtualAddressAndSize.Read(reader);
            var elementCount = (this.RelativeVirtualAddressAndSize.Size - Marshal.SizeOf(typeof(RVAndSize))) / Marshal.SizeOf(typeof(CoffRelocEntry));
            for (int i = 0; i < elementCount; i++)
            {
                var typeOffset = reader.ReadUInt16();
                base.baseList.Add(*(CoffRelocEntry*)&typeOffset);
            }
        }

        public int Length { get { return Marshal.SizeOf(typeof(RVAndSize)) + this.Count * Marshal.SizeOf(typeof(CoffRelocEntry)); } }
        public override string ToString()
        {
            return string.Format("COFF .reloc block ({0} entries)", this.Count);
        }
    }
    public struct CoffRelocEntry
    {
        private readonly ushort _typeOffset;

        public CoffRelocationType Type { get { return (CoffRelocationType)(byte)((_typeOffset & 0xF000) >> 12); } }
        public short Offset { get { return (short)((_typeOffset & 0x0FFF)); } }
        public override string ToString() { return string.Format("{0} @ {1:X8}", this.Type,  this.Offset); }
    }
    /** <summary>From pecoff_v83.docx</summary>*/
    public enum CoffRelocationType :
        byte
    {
        /** <summary>The base relocation is skipped. This type can be used to pad a block.</summary> */
        Absolute = 0,
        /** <summary>The base relocation adds the high 16 bits of the difference to the 16-bit field at offset. The 16-bit field represents the high value of a 32-bit word.</summary> */
        High = 1,
        /** <summary>The base relocation adds the low 16 bits of the difference to the 16-bit field at offset. The 16-bit field represents the low half of a 32-bit word.</summary> */
        Low = 2,
        /** <summary>The base relocation applies all 32 bits of the difference to the 32-bit field at offset.</summary> */
        HighLow = 3,
        /** <summary>The base relocation adds the high 16 bits of the difference to the 16-bit field at offset. The 16-bit field represents the high value of a 32-bit word.
         * The low 16 bits of the 32-bit value are stored in the 16-bit word that follows this base relocation. This means that this base relocation occupies two slots.</summary> */
        HighAdj = 4,
        /** <summary>For MIPS machine types, the base relocation applies to a MIPS jump instruction.</summary> */
        Mips_JumpAddress = 5,
        /** <summary>For ARM machine types, the base relocation applies the difference to the 32-bit value encoded in the immediate fields of a contiguous MOVW+MOVT pair in ARM mode at offset.</summary> */
        Arm_Mov32A = 5,
        /** <summary>Reserved, must be zero.</summary> */
        Reserved = 6,
        /** <summary>For ARM Machines, the base relocation applies the difference to the 32-bit value encoded in the immediate fields of a contiguous MOVW+MOVT pair in Thumb mode at offset</summary> */
        Arm_Mov32T = 7,
        /** <summary></summary> */
        Mips_JumpAddress16 = 9,
        /** <summary></summary> */
        Dir64 = 10,

    }
}
