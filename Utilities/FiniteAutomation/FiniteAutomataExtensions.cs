using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Numerics;

#if x64
using SlotType = System.UInt64;
#elif x86
#if Word16
using SlotType = System.UInt16;
#else
using SlotType = System.UInt32;
#endif
#endif

namespace AllenCopeland.Abstraction.Slf.FiniteAutomata
{
    public class FiniteAutomataExtensions
    {
        internal static SlotType[] ContinuousSets = InitializeContinuousSets().ToArray();
        /// <summary>
        /// The number of actual bits per the slot type used in the current
        /// architecture.
        /// </summary>
        internal const SlotType SlotBitCount = 
#if x64
            0x40;
#elif x86
#if Word16
            0x10;
#else
            0x20;
#endif
#endif
        /// <summary>
        /// The shift value in the proper 
        /// data-type used in the current architecture.
        /// </summary>
        internal const SlotType ShiftValue = 1;

 //SlotMaxValue == UInt16.MaxValue ? 0x10 : SlotMaxValue == UInt32.MaxValue ? 0x20 : 0x40;

        private static IEnumerable<SlotType> InitializeContinuousSets()
        {

            int bitCount = (int)SlotBitCount;
            for (int continuousBitLength = 1; continuousBitLength <= bitCount; continuousBitLength++)
            {
                SlotType currentValue = 0;
                for (int bitOffset = 0; bitOffset < continuousBitLength; bitOffset++)
                    currentValue |= (ShiftValue << bitOffset);
                for (int shift = 0; shift <= (bitCount - continuousBitLength); shift++)
                    yield return currentValue << shift;
            }
        }
        /* *
         * Multi-keyed dictionary of:
         *  Key 1. Bit Count
         *  Key 2. Left-Shift Index
         *  To Value
         * */
        internal static MultikeyedDictionary<int, int, SlotType> GroupedContinuousSets = InitializeGroupedContinuousSets();

        private static MultikeyedDictionary<int, int, SlotType> InitializeGroupedContinuousSets()
        {
            var result = new MultikeyedDictionary<int, int, SlotType>();
            var associations = (from element in ContinuousSets
                                let bits = element.CountBits()
                                orderby element
                                group element by bits into bitSets
                                orderby bitSets.Key descending
                                select bitSets).ToDictionary(k => k.Key, v => v.OrderBy(k => k).ToArray());
            foreach (var bitCountGroup in associations)
                foreach (var indexAndValue in bitCountGroup.Value.Select((value, index) => new { Index = index, Value = value }))
                    result.Add(bitCountGroup.Key, (int)indexAndValue.Index, indexAndValue.Value);
            return result;
        }

        //public static IEnumerable<FiniteAutomataBitSpan> GetContinuousBits(params SlotType[] bits)
        //{
            
        //    if (bits == null || bits.Length == 0)
        //        yield break;
        //    bool insideSet = false;
        //    const int bitCount = (int)SlotBitCount;
        //    for (int slotOffset = 0; slotOffset < bits.Length; slotOffset++)
        //    {
        //        SlotType currentValue = bits[slotOffset];
        //        int shiftStartIndex = 0;

        //        int startingPoint = currentValue.CountBits();
        //        for (int currentBitCount = startingPoint; currentBitCount >= 1; currentBitCount--)
        //        {
        //            for (int shiftIndex = shiftStartIndex; shiftIndex < (bitCount - currentBitCount); shiftIndex++)
        //            {
        //                var currentSpanValue = GroupedContinuousSets[currentBitCount, shiftIndex];
        //                if ()
        //            }
        //        }
        //    }
        //}
    }
    public struct FiniteAutomataBitSpan
    {
        public int Start{get;set;}
        public int Length {get;set;}
    }
}
