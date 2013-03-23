using AllenCopeland.Abstraction.Slf.FiniteAutomata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if x64
using SlotType = System.UInt64;
#elif x86
using SlotType = System.UInt32;
#endif

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliFlagsEnumerationEntry :
        FiniteAutomataBitSet<CliFlagsEnumerationEntry>
    {
        public CliFlagsEnumerationEntry()
            : base()
        {
        }

        public static implicit operator CliFlagsEnumerationEntry(int value)
        {
            var result = new CliFlagsEnumerationEntry();
            result.Set(new SlotType[] { (SlotType)value }, 0, sizeof(int), sizeof(int));
            return result;
        }

        public static implicit operator CliFlagsEnumerationEntry(uint value)
        {
            var result = new CliFlagsEnumerationEntry();
            result.Set(new SlotType[] { (SlotType)value }, 0, sizeof(int), sizeof(int));
            return result;
        }

        public static implicit operator CliFlagsEnumerationEntry(sbyte value)
        {
            var result = new CliFlagsEnumerationEntry();
            result.Set(new SlotType[] { (SlotType)value }, 0, sizeof(sbyte), sizeof(sbyte));
            return result;
        }

        public static implicit operator CliFlagsEnumerationEntry(byte value)
        {
            var result = new CliFlagsEnumerationEntry();
            result.Set(new SlotType[] { (SlotType)value }, 0, sizeof(byte), sizeof(byte));
            return result;
        }

        public static implicit operator CliFlagsEnumerationEntry(short value)
        {
            var result = new CliFlagsEnumerationEntry();
            result.Set(new SlotType[] { (SlotType)value }, 0, sizeof(short), sizeof(short));
            return result;
        }

        public static implicit operator CliFlagsEnumerationEntry(ushort value)
        {
            var result = new CliFlagsEnumerationEntry();
            result.Set(new SlotType[] { (SlotType)value }, 0, sizeof(ushort), sizeof(ushort));
            return result;
        }

        public static implicit operator CliFlagsEnumerationEntry(long value)
        {
            var result = new CliFlagsEnumerationEntry();
#if x86
            var hi = (SlotType)(((ulong)value) & 0xFFFFFFFF00000000 >> 32);
            var lo = (SlotType)(value          & 0x00000000FFFFFFFF);
            result.Set(new SlotType[] { lo, hi }, 0, sizeof(long), sizeof(long));
#elif x64
            result.Set(new SlotType[] { (SlotType)value }, 0, sizeof(ushort), sizeof(ushort));
#endif
            return result;
        }
        public static implicit operator CliFlagsEnumerationEntry(ulong value)
        {
            var result = new CliFlagsEnumerationEntry();
#if x86
            var hi = (SlotType)(value & 0xFFFFFFFF00000000 >> 32);
            var lo = (SlotType)(value & 0x00000000FFFFFFFF);
            result.Set(new SlotType[] { lo, hi }, 0, sizeof(ulong), sizeof(ulong));
#elif x64
            result.Set(new SlotType[] { (SlotType)value }, 0, sizeof(ushort), sizeof(ushort));
#endif
            return result;
        }
    }
}
