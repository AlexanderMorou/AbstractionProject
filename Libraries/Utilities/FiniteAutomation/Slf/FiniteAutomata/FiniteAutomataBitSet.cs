using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

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

    //using OperationSignature = Action<uint, uint, uint, uint, uint, uint, bool, bool, SlotType[], SlotType[], SlotType[]>;
    /// <summary>
    /// Provides a base implementation of a 
    /// <see cref="IFiniteAutomataSet{TCheck}"/> whose individual elements
    /// from the full set are represented by a single bit.
    /// </summary>
    /// <typeparam name="TCheck">The direct inheritor of the 
    /// <see cref="FiniteAutomataBitSet{TCheck}"/> used as a result
    /// of set operations.</typeparam>
    public abstract partial class FiniteAutomataBitSet<TCheck> :
        IFiniteAutomataSet<TCheck>
        where TCheck :
            FiniteAutomataBitSet<TCheck>,
            new()
    {
#if x86
#if Word16
        /// <summary>
        /// Denotes the set operation delegate in an effort to simplify the parameter intake of a set operation.
        /// </summary>
        /// <param name="leftOffset">The offset of the <paramref name="leftWorkingSet"/> relative to
        /// the start bit of the full set for the current type of 
        /// <typeparamref name="TCheck"/>.</param>
        /// <param name="rightOffset">The offset of the <paramref name="rightWorkingSet"/> relative to
        /// the start bit of the full set for the current type of 
        /// <typeparamref name="TCheck"/>.</param>
        /// <param name="leftLength">The number of bits the <paramref name="leftWorkingSet"/> is.</param>
        /// <param name="rightLength">The number of bits the <paramref name="rightWorkingSet"/> is.</param>
        /// <param name="minimumOffset">The <see cref="Int32"/> value denoting the minimum 
        /// offset between <paramref name="leftOffset"/> and <paramref name="rightOffset"/></param>
        /// <param name="newLength">The new number of bits within the <paramref name="resultWorkingSet"/>.</param>
        /// <param name="leftIsNegativeSet">Whether the left set is a negative set.</param>
        /// <param name="rightIsNegativeSet">Whether the right set is a negative set.</param>
        /// <param name="leftWorkingSet">The <see cref="UInt16"/> array which denotes
        /// the first set to perform the operation on.
        /// </param>
        /// <param name="rightWorkingSet">The <see cref="UInt16"/> array which denotes
        /// the second set to perform the operation on.</param>
        /// <param name="resultWorkingSet">The <see cref="UInt16"/> array which denotes
        /// result set of the operation.</param>
#else
        /// <summary>
        /// Denotes the set operation delegate in an effort to simplify the parameter intake of a set operation.
        /// </summary>
        /// <param name="leftOffset">The offset of the <paramref name="leftWorkingSet"/> relative to
        /// the start bit of the full set for the current type of 
        /// <typeparamref name="TCheck"/>.</param>
        /// <param name="rightOffset">The offset of the <paramref name="rightWorkingSet"/> relative to
        /// the start bit of the full set for the current type of 
        /// <typeparamref name="TCheck"/>.</param>
        /// <param name="leftLength">The number of bits the <paramref name="leftWorkingSet"/> is.</param>
        /// <param name="rightLength">The number of bits the <paramref name="rightWorkingSet"/> is.</param>
        /// <param name="minimumOffset">The <see cref="Int32"/> value denoting the minimum 
        /// offset between <paramref name="leftOffset"/> and <paramref name="rightOffset"/></param>
        /// <param name="newLength">The new number of bits within the <paramref name="resultWorkingSet"/>.</param>
        /// <param name="leftIsNegativeSet">Whether the left set is a negative set.</param>
        /// <param name="rightIsNegativeSet">Whether the right set is a negative set.</param>
        /// <param name="leftWorkingSet">The <see cref="UInt32"/> array which denotes
        /// the first set to perform the operation on.
        /// </param>
        /// <param name="rightWorkingSet">The <see cref="UInt32"/> array which denotes
        /// the second set to perform the operation on.</param>
        /// <param name="resultWorkingSet">The <see cref="UInt32"/> array which denotes
        /// result set of the operation.</param>
#endif
#elif x64
        /// <summary>
        /// Denotes the set operation delegate in an effort to simplify the parameter intake of a set operation.
        /// </summary>
        /// <param name="leftOffset">The offset of the <paramref name="leftWorkingSet"/> relative to
        /// the start bit of the full set for the current type of 
        /// <typeparamref name="TCheck"/>.</param>
        /// <param name="rightOffset">The offset of the <paramref name="rightWorkingSet"/> relative to
        /// the start bit of the full set for the current type of 
        /// <typeparamref name="TCheck"/>.</param>
        /// <param name="leftLength">The number of bits the <paramref name="leftWorkingSet"/> is.</param>
        /// <param name="rightLength">The number of bits the <paramref name="rightWorkingSet"/> is.</param>
        /// <param name="minimumOffset">The <see cref="Int32"/> value denoting the minimum 
        /// offset between <paramref name="leftOffset"/> and <paramref name="rightOffset"/></param>
        /// <param name="resultLength">The new number of bits within the <paramref name="resultWorkingSet"/>.</param>
        /// <param name="leftIsNegativeSet">Whether the left set is a negative set.</param>
        /// <param name="rightIsNegativeSet">Whether the right set is a negative set.</param>
        /// <param name="leftWorkingSet">The <see cref="UInt64"/> array which denotes
        /// the first set to perform the operation on.
        /// </param>
        /// <param name="rightWorkingSet">The <see cref="UInt64"/> array which denotes
        /// the second set to perform the operation on.</param>
        /// <param name="resultWorkingSet">The <see cref="UInt64"/> array which denotes
        /// result set of the operation.</param>
#endif
        private delegate void SetOperation(uint leftOffset, uint rightOffset, uint leftLength, uint rightLength, uint minimumOffset, uint resultLength, bool leftIsNegativeSet, bool rightIsNegativeSet, SlotType[] leftWorkingSet, SlotType[] rightWorkingSet, SlotType[] resultWorkingSet);
        /// <summary>
        /// Data member for <see cref="IsNegativeSet"/>.
        /// </summary>
        private bool isNegativeSet;
        /// <summary>
        /// Data member for <see cref="Offset"/>.
        /// </summary>
        private uint offset;
        /// <summary>
        /// Data member for <see cref="FullLength"/>
        /// </summary>
        private uint fullLength;
        /// <summary>
        /// Data member for <see cref="Length"/>.
        /// </summary>
        private uint length;
        /// <summary>
        /// Data member for <see cref="Complement"/>.
        /// </summary>
        private TCheck complement;
        private static MultikeyedDictionary<TCheck, TCheck, TCheck> IntersectCache = new MultikeyedDictionary<TCheck, TCheck, TCheck>();
        private static MultikeyedDictionary<TCheck, TCheck, TCheck> SymmetricDifferenceCache = new MultikeyedDictionary<TCheck, TCheck, TCheck>();
        private static MultikeyedDictionary<TCheck, TCheck, TCheck> UnionCache = new MultikeyedDictionary<TCheck, TCheck, TCheck>();

        internal static TCheck NullInst;
        private static int unionCacheHits = 0;
        private static int intersectionCacheHits = 0;
        private static int symmetricDifferenceCacheHits = 0;

        private static object unionCacheLock = new object();
        private static object intersectCacheLock = new object();
        private static object symmetricDiffCacheLock = new object();
        /// <summary>
        /// 
        /// </summary>
        private const SlotType SlotMaxValue = SlotType.MaxValue;
        private static SetOperation unionOp = UnionProcess;
        private static SetOperation intersectOp = IntersectionProcess;
        private static SetOperation symmetricDifferenceOp = SymmetricDifferenceProcess;
        private SlotType[] values;
        private SlotType lastMask;
        private SlotType lastMod;
        protected void Set(SlotType[] values, uint offset, uint length, bool isNegativeSet = false, bool reduce = true)
        {
            this.isNegativeSet = isNegativeSet;
            this.values = values;
            this.offset = offset;
            this.length = length;
            this.lastMod = FiniteAutomataExtensions.SlotBitCount;
            this.lastMask = SlotMaxValue;
            this.fullLength = fullLength;
            if (reduce)
                this.Reduce();
        }
        private static bool gettingSD = false;
        public FiniteAutomataBitSet()
        {
            if (NullInst == null && !gettingSD)
            {
                gettingSD = true;
                NullInst = this.GetCheck();
            }
        }

        /// <summary>
        /// Obtains a new <typeparamref name="TCheck"/> instance
        /// for set operations.
        /// </summary>
        /// <returns>A new <typeparamref name="TCheck"/> instance.</returns>
        /// <remarks>Inheritors override if there is initialization
        /// code necessary for proper function.</remarks>
        protected virtual TCheck GetCheck()
        {
            return new TCheck();
        }

        /// <summary>
        /// Returns whether the <paramref name="obj"/> provided is
        /// equal to the current <see cref="FiniteAutomataBitSet{TCheck}"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare
        /// against.</param>
        /// <returns>true if the subset information from the
        /// <paramref name="obj"/> is equal to the current 
        /// <see cref="FiniteAutomataBitSet{TCheck}"/>;
        /// and false, if <paramref name="obj"/> is not an instance
        /// of <typeparamref name="TCheck"/>, or otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is TCheck))
                return false;
            return this.Equals((TCheck)obj);
        }

        /// <summary>
        /// Obtains the hash for the current <see cref="FiniteAutomataBitSet{TCheck}"/>.
        /// </summary>
        /// <returns>A <see cref="Int32"/> value representing the 
        /// bitwise hash of the current <see cref="FiniteAutomataBitSet{TCheck}"/>.</returns>
        public override int GetHashCode()
        {
            int r = (int)(this.offset ^ this.length);
            if (this.values != null) 
                r ^= ~this.values.Length;
            return r;
        }

        #region IFiniteAutomataSet<TCheck> Members

        /// <summary>Determines whether the current <see cref="FiniteAutomataBitSet{TCheck}"/> contains no elements from the full set.</summary>
        public bool IsEmpty { get { return ((this.values == null) || (this.length == 0)) && (!(this.isNegativeSet)); } }

        /// <summary>
        /// Creates a union of the current <see cref="FiniteAutomataBitSet{TCheck}"/> and the
        /// <paramref name="other"/> provided.
        /// </summary>
        /// <param name="other">The other <typeparamref name="TCheck"/> to
        /// create a union with.</param>
        /// <returns>A new <typeparamref name="TCheck"/> which contains
        /// the elements of both the current instance and the 
        /// <paramref name="other"/> instance provided.</returns>
        public TCheck Union(TCheck other)
        {
            var tThis = (TCheck)this;
            if (Equals(tThis, other))
                return tThis;
            if (other == null)
                return (TCheck)this;
            return ProcessSetOperation((TCheck)(this), other, unionOp, (li, ri) => li || ri, GetCheck, ref unionCacheHits, unionCacheLock, UnionCache);
        }

        /// <summary>
        /// Creates an intersection of the current <see cref="FiniteAutomataBitSet{TCheck}"/>
        /// and the <paramref name="other"/> provided.
        /// </summary>
        /// <param name="other">The <typeparamref name="TCheck"/> to make the 
        /// intersection of.</param>
        /// <returns>A new <typeparamref name="TCheck"/> which represents
        /// the points of overlap between the current <see cref="FiniteAutomataBitSet{TCheck}"/>
        /// and the <paramref name="other"/> provided.</returns>
        public TCheck Intersect(TCheck other)
        {
            var tThis = (TCheck)this;
            if (Equals(tThis, other))
                return tThis;
            if (other == null)
            {
                var result = new TCheck();
                result.Set(null, 0, 0);
                return result;
            }
            return ProcessSetOperation(tThis, other, intersectOp, (li, ri) => li && ri, GetCheck, ref intersectionCacheHits, intersectCacheLock, IntersectCache);
        }
        /// <summary>
        /// Returns the complement of the current <see cref="FiniteAutomataBitSet{TCheck}"/>.
        /// </summary>
        /// <returns>A new <typeparamref name="TCheck"/> which represents the
        /// complement of the current <see cref="FiniteAutomataBitSet{TCheck}"/>.</returns>
        public TCheck Complement()
        {
            if (this.complement == null)
            {
                this.complement = GetCheck();
                this.complement.values = this.values;
                this.complement.offset = this.offset;
                this.complement.length = this.length;
                this.complement.lastMask = this.lastMask;
                this.complement.lastMod = this.lastMod;
                //this.complement.fullLength = this.FullLength;
                this.complement.complement = (TCheck)this;
                this.complement.isNegativeSet = !this.isNegativeSet;
            }
            return this.complement;
        }

        /// <summary>
        /// Creates a new set which represents the symmetric
        /// difference between the current <see cref="FiniteAutomataBitSet{TCheck}"/> 
        /// and the <paramref name="other"/> provided.
        /// </summary>
        /// <param name="other">The <typeparamref name="TCheck"/> to make the
        /// symmetric difference of.</param>
        /// <returns>A new <typeparamref name="TCheck"/> instance which represents the
        /// elements from both sets that are mutually exclusive between the two.</returns>
        public TCheck SymmetricDifference(TCheck other)
        {
            var tThis = (TCheck)this;
            if (Equals(tThis, other))
                return NullInst;
            if (other == null)
                return (TCheck)this;
            return ProcessSetOperation((TCheck)this, other, symmetricDifferenceOp, (li, ri) => li ^ ri, GetCheck, ref symmetricDifferenceCacheHits, symmetricDiffCacheLock, SymmetricDifferenceCache);
        }

        /// <summary>
        /// Creates a new set which represents the relative complement
        /// of the <paramref name="other"/> <typeparamref name="TCheck"/> instance
        /// relative to the current <see cref="FiniteAutomataBitSet{TCheck}"/>.
        /// </summary>
        /// <param name="other">The <typeparamref name="TCheck"/> to
        /// use to create the symmetric difference to intersect.</param>
        /// <returns>A new <typeparamref name="TCheck"/> instance which represents
        /// the elements exclusive to the current 
        /// <see cref="FiniteAutomataBitSet{TCheck}"/>.
        /// </returns>
        /// <remarks><para>A&#8898;&#8705;B</para><para>
        /// i.e. The intersection of A and the complement of B.</para></remarks>
        public TCheck RelativeComplement(TCheck other)
        {
            return (this & other.Complement());
        }

        #endregion

        #region IEquatable<TCheck> Members

        /// <summary>
        /// Returns whether the <paramref name="other"/> is equal to the
        /// current <see cref="FiniteAutomataBitSet{TCheck}"/>.
        /// </summary>
        /// <param name="other">The <typeparamref name="TCheck"/>
        /// to compare against.</param>
        /// <returns>true if the subset information from the <paramref name="other"/>
        /// is equal to the current <see cref="FiniteAutomataBitSet{TCheck}"/></returns>
        public bool Equals(TCheck other)
        {
            return Equals((TCheck)this, (TCheck)other);
        }

        #endregion

        /// <summary>
        /// Returns the number of bits within the subset.
        /// </summary>
        public uint Length { get { return this.length; } }

        /// <summary>
        /// Returns the number of bits within the full set.
        /// </summary>
        public uint FullLength { get { return OnGetFullLength(); } /*protected set { this.fullLength = value; } */}

        internal abstract uint OnGetFullLength();

        /// <summary>
        /// Returns the number of bits from the full set which are skipped because no pertinent data exists.
        /// </summary>
        public uint Offset { get { return this.offset; } }

        /// <summary>
        /// Returns whether the current subset is a negative set.
        /// </summary>
        public bool IsNegativeSet
        {
            get
            {
                return this.isNegativeSet;
            }
            protected set
            {
                this.isNegativeSet = value;
            }
        }

        /// <summary>
        /// Returns whether the element at the specified
        /// <paramref name="index"/> is within the current
        /// subset.
        /// </summary>
        /// <param name="index">The <see cref="Int32"/> value relative
        /// to the full set to check for within the current
        /// subset.</param>
        /// <returns>true if the element at the <paramref name="index"/>
        /// provided is within the current subset; false, otherwise.</returns>
        /// <remarks>Adjusted to accomodate whether the
        /// current <see cref="FiniteAutomataBitSet{TCheck}"/>
        /// is a negative set.</remarks>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when the
        /// <paramref name="index"/> provided is less than zero or greater than
        /// the length of the full set in which the <see cref="FiniteAutomataBitSet{TCheck}"/>
        /// is a subset of.</exception>
        public bool this[uint index]
        {
            get
            {
                if (index < 0 || index >= this.FullLength)
                    throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
                else if (index < this.offset || index >= this.offset + this.length)
                    return this.isNegativeSet;
                uint indexShift = index - this.Offset;
                return ((this.values[indexShift / FiniteAutomataExtensions.SlotBitCount] & (SlotType)(FiniteAutomataExtensions.ShiftValue << ((int)(indexShift % FiniteAutomataExtensions.SlotBitCount)))) != 0) ^ this.isNegativeSet;
            }
        }

        internal bool GetThisInternal(uint index)
        {
            if (index < this.Offset || index >= this.Offset + this.Length)
                throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
            uint indexShift = index - this.Offset;
            return ((this.values[indexShift / FiniteAutomataExtensions.SlotBitCount] & (SlotType)(FiniteAutomataExtensions.ShiftValue << ((int)(indexShift % FiniteAutomataExtensions.SlotBitCount)))) != 0);
        }

        /// <summary>
        /// Determines whether the <paramref name="left"/> 
        /// <typeparamref name="TCheck"/> instance and the 
        /// <paramref name="right"/> <typeparamref name="TCheck"/>
        /// instance have equal subset data.
        /// </summary>
        /// <param name="left">The left <typeparamref name="TCheck"/> operand.</param>
        /// <param name="right">The right <typeparamref name="TCheck"/> operand.</param>
        /// <returns>true if the <paramref name="left"/> and <paramref name="right"/>
        /// operands have equal subset information.</returns>
        public static bool Equals(TCheck left, TCheck right)
        {
            if (left == null)
                return right == null;
            else if (right == null)
                return false;
            if ((left.values == null && left.isNegativeSet) && right.values != null)
                return false;
            else if (left.values != null && right.values == null && right.isNegativeSet)
                return false;
            if (left.values == null && right.values == null &&
                left.isNegativeSet && right.isNegativeSet)
                return true;
            else if (left.values == null && right.values == null &&
                     left.isNegativeSet == right.isNegativeSet)
                return true;
            if (left.offset != right.offset)
                return false;
            if (left.length != right.length)
                return false;
            if (left.isNegativeSet != right.isNegativeSet)
                return false;
            SlotType[] leftSet = left.values;
            SlotType[] rightSet = right.values;
            for (int i = 0; i < leftSet.Length; i++)
                if (leftSet[i] != rightSet[i])
                    return false;
            return true;
        }

        private static TCheck ProcessSetOperation(
            TCheck left, TCheck right,
            SetOperation @operator,
            Func<bool, bool, bool> inversionPredicate,
            Func<TCheck> checkCreator,
            ref int cacheHits,
            object cacheLock,
            MultikeyedDictionary<TCheck, TCheck, TCheck> dictionaryCache)
        {

            TCheck operation;
            if (!dictionaryCache.TryGetValue(left, right, out operation))
            {

                /* left offset */   uint                 leftOffset = left.offset,
               /* right offset */                       rightOffset = right.offset,
             /* minimum offset */                     minimumOffset = Math.Min(leftOffset, rightOffset);
                /* left length */   uint                 leftLength = left.length,
               /* right length */                       rightLength = right.length,
                 /* new length */                   resultSetLength = Math.Max(leftOffset + leftLength, rightOffset + rightLength);
              /* left inverted */   bool          leftIsNegativeSet = left.isNegativeSet,
             /* right inverted */                rightIsNegativeSet = right.isNegativeSet;
       /* out Int32 set length */   uint               resultLength = (uint)(Math.Ceiling(((double)(resultSetLength - minimumOffset)) / FiniteAutomataExtensions.SlotBitCount));
           /* left working set */   SlotType[]       leftWorkingSet = left.values,
          /* right working set */                   rightWorkingSet = right.values,
            /* out working set */                  resultWorkingSet = resultLength > 0 ? new SlotType[resultLength] : null;

                /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
                 * Eases code management when setup/finalization logic is in one place.   *
                 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * **
                 * Loops and other logic out-sourced to @operator(...).                   *
                 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
                @operator(leftOffset, rightOffset, leftLength, rightLength, minimumOffset, resultSetLength, leftIsNegativeSet, rightIsNegativeSet, leftWorkingSet, rightWorkingSet, resultWorkingSet);
                /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
                 * Different bitwise operations have different rules for whether the result set is  *
                 * inverted.                                                                        *
                 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
                bool invert = inversionPredicate(leftIsNegativeSet, rightIsNegativeSet);
                if (invert)
                    //If it is inverted, flip the bits of each subset.
                    for (int i = 0; i < resultLength; i++)
                        resultWorkingSet[i] = (SlotType)~resultWorkingSet[i];
                operation = checkCreator();
                operation.offset = minimumOffset;
                operation.length = resultSetLength - minimumOffset;
                operation.isNegativeSet = invert;
                //operation.fullLength = Math.Max(left.FullLength, right.FullLength);

                SlotType lastMod = (SlotType)(operation.length % FiniteAutomataExtensions.SlotBitCount);
                if (lastMod == 0)
                    if (resultSetLength == 0)
                    {
                        operation.lastMod = 0;
                        operation.lastMask = 0;
                    }
                    else
                    {
                        operation.lastMask = SlotMaxValue;
                        operation.lastMod = FiniteAutomataExtensions.SlotBitCount;
                    }
                else
                {
                    operation.lastMod = lastMod;
                    operation.lastMask = (SlotType)((FiniteAutomataExtensions.ShiftValue << (int)lastMod) - 1);
                }
                if (invert && resultLength > 0)
                    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
                     * On inverted sets, fix the excessive '1' bits that shouldn't be there, but get placed   *
                     * due to the lazy nature of the method employed.                                         *
                     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * **
                     * This fixup is to ensure counts return correctly based on the last element in the set.  *
                     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
                    resultWorkingSet[resultWorkingSet.Length - 1] &= operation.lastMask;
                if (resultLength > 0)
                    operation.values = resultWorkingSet;
                else
                    operation.values = null;
                /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
                 * Reduction especially necessary for exclusive operations which only select parts.         *
                 * It goes beyond the scope of this method to handle both individual operations and minimal *
                 * sets at once.                                                                            *
                 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
                operation.Reduce();
                if (operation.IsEmpty)
                {
                    operation.values = null;
                    operation = NullInst;
                }
                lock (dictionaryCache)
                {
                    dictionaryCache.TryAdd(left, right, operation);
                    dictionaryCache.TryAdd(right, left, operation);
                }
            }
            else lock(cacheLock)
                cacheHits++;
            Debug.Assert(operation != null);
            return operation;
        }

        protected internal void Reduce()
        {
            //Skip instances where there's nothing to do.
            if (IsEmpty)
                return;
            if (AllTrue)
                return;
            if (AllFalse)
            {
                if (this.values != null)
                {
                    this.isNegativeSet = false;
                    goto _Nullset;
                }
                return;
            }
            SlotType first  = SlotMaxValue;
            SlotType last   = SlotMaxValue;
            for (uint i = 0; i < (SlotType)this.values.Length; i++)
            {
                SlotType current = this.values[i];
                if (current == 0)
                    continue;
                SlotType currentSize = i == (SlotType)(this.values.Length - 1) ? this.lastMod : FiniteAutomataExtensions.SlotBitCount;
                for (uint j = 0; j < currentSize; j++)
                    if (((current & (SlotType)(FiniteAutomataExtensions.ShiftValue << (int)j)) != 0))
                    {
                        SlotType curIndex = (SlotType)(i * FiniteAutomataExtensions.SlotBitCount + j);
                        if (first == SlotMaxValue)
                            first = (SlotType)(i * FiniteAutomataExtensions.SlotBitCount + j);
                        last = curIndex;
                    }
            }
            /* *
             * Make sure the reduction is necessary.
             * */
            if (first == 0 &&
                last == (this.length - 1))
                return;
            /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
             * If there was no element hit, there's no data. It's *highly unlikely* the current *
             * subset is exactly 2^[31 or 63]+ elements.                                        *
             * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
            if (last == SlotMaxValue)
                goto _Nullset;
            SlotType     finalCount = (SlotType)((last - first) + 1);
            SlotType   finalSetSize = (SlotType)((finalCount + (FiniteAutomataExtensions.SlotBitCount - 1)) / FiniteAutomataExtensions.SlotBitCount);
            if (finalSetSize == 0)
                goto _Nullset;
            SlotType       bitShift = (SlotType)(first % (int)FiniteAutomataExtensions.SlotBitCount);
                uint    indexOffset = (uint)(first / (int)FiniteAutomataExtensions.SlotBitCount);
            SlotType    checkHeight = (SlotType)Math.Ceiling(((double)(last + 1)) / FiniteAutomataExtensions.SlotBitCount);
            SlotType[]    resultSet = new SlotType[finalSetSize];
            for (uint slotIndex = indexOffset; slotIndex < checkHeight; slotIndex++)
            {
                SlotType currentSize = slotIndex == (SlotType)(this.values.Length - 1) ? this.lastMod : FiniteAutomataExtensions.SlotBitCount;
                for (uint slotBitIndex = 0; slotBitIndex < currentSize; slotBitIndex++)
                {
                    SlotType       curIndex = (SlotType)(slotIndex * FiniteAutomataExtensions.SlotBitCount + slotBitIndex);
                    SlotType resultSetIndex = (SlotType)((curIndex - first) / FiniteAutomataExtensions.SlotBitCount);
                    SlotType    resultIndex = (SlotType)(slotIndex * FiniteAutomataExtensions.SlotBitCount + slotBitIndex - bitShift);
                    if ((this.values[slotIndex] & (SlotType)(FiniteAutomataExtensions.ShiftValue << (int)slotBitIndex)) != 0)
                        resultSet[resultSetIndex] |= (SlotType)(FiniteAutomataExtensions.ShiftValue << (int)(resultIndex % FiniteAutomataExtensions.SlotBitCount));
                }
            }
            SlotType lastSlotModulus  = (SlotType)(finalCount % (int)FiniteAutomataExtensions.SlotBitCount);
            if (lastSlotModulus == 0)
            {
                this.lastMask = SlotMaxValue;
                this.lastMod  = FiniteAutomataExtensions.SlotBitCount;
            }
            else
            {
                this.lastMask   = (SlotType)((FiniteAutomataExtensions.ShiftValue << (int)lastSlotModulus) - 1);
                this.lastMod    = lastSlotModulus;
            }
            this.offset += (uint)first;
            this.values = resultSet;
            this.length = (uint)finalCount;
            return;
        _Nullset: /* The set is empty, so there's no need to store anything to represent it. */
            this.values     = null;
            this.offset     = 0;
            this.lastMod    = 0;
            this.lastMask   = 0;
            this.length     = 0;
        }

        /// <summary>
        /// Returns whether the values of the current <see cref="FiniteAutomataBitSet{TCheck}"/>
        /// all false.
        /// </summary>
        /// <returns>true if all elements are false, when <see cref="IsNegativeSet"/>
        /// all values in the <see cref="FiniteAutomataBitSet{TCheck}"/> must be
        /// true.  If the <see cref="values"/> are null returns false if 
        /// <see cref="IsNegativeSet"/> is true.</returns>
        public bool AllFalse
        {
            get
            {
                if (this.values == null)
                    return !this.isNegativeSet;
                SlotType exclusivePoint = (SlotType)(this.values.Length < 2 ? 0 : (SlotType)(this.values.Length - 1));
                for (uint i = 0; i < exclusivePoint; i++)
                    if (this.IsNegativeSet)
                    {
                        if (this.values[i] != SlotMaxValue)
                            return false;
                    }
                    else if (this.values[i] != SlotType.MinValue)
                        return false;
                if (this.IsNegativeSet)
                    return (this.values[this.values.Length - 1] & this.lastMask) == this.lastMask;
                else
                    return (this.values[this.values.Length - 1] & this.lastMask) == 0;
            }
        }

        /// <summary>
        /// Returns whether the values of the current <see cref="FiniteAutomataBitSet{TCheck}"/>
        /// all true.
        /// </summary>
        /// <returns>true if all elements are true, when <see cref="IsNegativeSet"/>
        /// all values in the <see cref="FiniteAutomataBitSet{TCheck}"/> must be
        /// false.  If the associated <see cref="values"/> are null returns true if 
        /// <see cref="IsNegativeSet"/> is true.</returns>
        public bool AllTrue
        {
            get
            {
                if (this.values == null)
                    return this.isNegativeSet;
                SlotType exclusivePoint = (SlotType)(this.values.Length < 2 ? 0 : (SlotType)(this.values.Length - 1));
                for (uint bitIndex = 0; bitIndex < exclusivePoint; bitIndex++)
                    if (this.IsNegativeSet)
                    {
                        if (this.values[bitIndex] != SlotType.MinValue)
                            return false;
                    }
                    else if (this.values[bitIndex] != SlotMaxValue)
                        return false;
                return ((this.values[this.values.Length - 1] & lastMask) == lastMask);
            }
        }

        [DebuggerStepThrough]
        private static void UnionProcess(uint leftOffset, uint rightOffset, uint leftLength, uint rightLength, uint minimumOffset, uint newLength, bool leftIsNegativeSet, bool rightIsNegativeSet, SlotType[] leftWorkingSet, SlotType[] rightWorkingSet, SlotType[] resultWorkingSet)
        {
            if (resultWorkingSet == null)
                return;
            for (uint bitIndex = minimumOffset; bitIndex < newLength; bitIndex++)
            {
                uint leftRelativeIndex      = (uint)(bitIndex - leftOffset), 
                     rightRelativeIndex     = (uint)(bitIndex - rightOffset),
                     resultRelativeIndex    = (uint)(bitIndex - minimumOffset); 
                if (((leftRelativeIndex >= 0 && leftRelativeIndex < leftLength) && ((leftWorkingSet[leftRelativeIndex / FiniteAutomataExtensions.SlotBitCount] & (SlotType)(FiniteAutomataExtensions.ShiftValue << (int)(leftRelativeIndex % FiniteAutomataExtensions.SlotBitCount))) != 0)) ^ leftIsNegativeSet ||
                    ((rightRelativeIndex >= 0 && rightRelativeIndex < rightLength) && ((rightWorkingSet[rightRelativeIndex / FiniteAutomataExtensions.SlotBitCount] & (SlotType)(FiniteAutomataExtensions.ShiftValue << (int)(rightRelativeIndex % FiniteAutomataExtensions.SlotBitCount))) != 0)) ^ rightIsNegativeSet)
                    resultWorkingSet[resultRelativeIndex / FiniteAutomataExtensions.SlotBitCount] |= (SlotType)(FiniteAutomataExtensions.ShiftValue << (int)(resultRelativeIndex % FiniteAutomataExtensions.SlotBitCount));
            }
        }

        private static void IntersectionProcess(uint leftOffset, uint rightOffset, uint leftLength, uint rightLength, uint minimumOffset, uint newLength, bool leftIsNegativeSet, bool rightIsNegativeSet, SlotType[] leftWorkingSet, SlotType[] rightWorkingSet, SlotType[] resultWorkingSet)
        {
            if (resultWorkingSet == null)
                return;
            for (uint bitIndex = minimumOffset; bitIndex < newLength; bitIndex++)
            {
                uint leftRelativeIndex      = (bitIndex - leftOffset),
                     rightRelativeIndex     = (bitIndex - rightOffset),
                     resultRelativeIndex    = (bitIndex - minimumOffset);
                if (((leftRelativeIndex >= 0 && leftRelativeIndex < leftLength) && ((leftWorkingSet[(SlotType)leftRelativeIndex / FiniteAutomataExtensions.SlotBitCount] & (SlotType)(FiniteAutomataExtensions.ShiftValue << (int)((SlotType)leftRelativeIndex % FiniteAutomataExtensions.SlotBitCount))) != 0)) ^ leftIsNegativeSet &&
                    ((rightRelativeIndex >= 0 && rightRelativeIndex < rightLength) && ((rightWorkingSet[(SlotType)rightRelativeIndex / FiniteAutomataExtensions.SlotBitCount] & (SlotType)(FiniteAutomataExtensions.ShiftValue << (int)((SlotType)rightRelativeIndex % FiniteAutomataExtensions.SlotBitCount))) != 0)) ^ rightIsNegativeSet)
                    resultWorkingSet[(SlotType)resultRelativeIndex / FiniteAutomataExtensions.SlotBitCount] |= (SlotType)(FiniteAutomataExtensions.ShiftValue << (int)((SlotType)resultRelativeIndex % FiniteAutomataExtensions.SlotBitCount));
            }
        }

#if x86
        /// <summary>
        /// Performs a symmetric difference on a <paramref name="leftWorkingSet"/> and <paramref name="rightWorkingSet"/> with the result within the
        /// <paramref name="resultWorkingSet"/>.
        /// </summary>
        /// <param name="leftOffset">The offset of the <paramref name="leftWorkingSet"/> relative to
        /// the start bit of the full set for the current type of 
        /// <typeparamref name="TCheck"/>.</param>
        /// <param name="rightOffset">The offset of the <paramref name="rightWorkingSet"/> relative to
        /// the start bit of the full set for the current type of 
        /// <typeparamref name="TCheck"/>.</param>
        /// <param name="leftLength">The number of bits the <paramref name="leftWorkingSet"/> is.</param>
        /// <param name="rightLength">The number of bits the <paramref name="rightWorkingSet"/> is.</param>
        /// <param name="minimumOffset">The <see cref="Int32"/> value denoting the minimum 
        /// offset between <paramref name="leftOffset"/> and <paramref name="rightOffset"/></param>
        /// <param name="newLength">The new number of bits within the <paramref name="resultWorkingSet"/>.</param>
        /// <param name="leftIsNegativeSet">Whether the left set is a negative set.</param>
        /// <param name="rightIsNegativeSet">Whether the right set is a negative set.</param>
        /// <param name="leftWorkingSet">The <see cref="Int32"/> array which denotes
        /// the first set to perform the operation on.
        /// </param>
        /// <param name="rightWorkingSet">The <see cref="Int32"/> array which denotes
        /// the second set to perform the operation on.</param>
        /// <param name="resultWorkingSet">The <see cref="Int32"/> array which denotes
        /// result set of the operation.</param>
#elif x64
        /// <summary>
        /// Performs a symmetric difference on a <paramref name="leftWorkingSet"/> and <paramref name="rightWorkingSet"/> with the result within the
        /// <paramref name="resultWorkingSet"/>.
        /// </summary>
        /// <param name="leftOffset">The offset of the <paramref name="leftWorkingSet"/> relative to
        /// the start bit of the full set for the current type of 
        /// <typeparamref name="TCheck"/>.</param>
        /// <param name="rightOffset">The offset of the <paramref name="rightWorkingSet"/> relative to
        /// the start bit of the full set for the current type of 
        /// <typeparamref name="TCheck"/>.</param>
        /// <param name="leftLength">The number of bits the <paramref name="leftWorkingSet"/> is.</param>
        /// <param name="rightLength">The number of bits the <paramref name="rightWorkingSet"/> is.</param>
        /// <param name="minimumOffset">The <see cref="Int32"/> value denoting the minimum 
        /// offset between <paramref name="leftOffset"/> and <paramref name="rightOffset"/></param>
        /// <param name="resultLength">The new number of bits within the <paramref name="resultWorkingSet"/>.</param>
        /// <param name="leftIsNegativeSet">Whether the left set is a negative set.</param>
        /// <param name="rightIsNegativeSet">Whether the right set is a negative set.</param>
        /// <param name="leftWorkingSet">The <see cref="Int64"/> array which denotes
        /// the first set to perform the operation on.
        /// </param>
        /// <param name="rightWorkingSet">The <see cref="Int64"/> array which denotes
        /// the second set to perform the operation on.</param>
        /// <param name="resultWorkingSet">The <see cref="Int64"/> array which denotes
        /// result set of the operation.</param>
#endif
        [DebuggerStepThrough]
        private static void SymmetricDifferenceProcess(uint leftOffset, uint rightOffset, uint leftLength, uint rightLength, uint minimumOffset, uint newLength, bool leftIsNegativeSet, bool rightIsNegativeSet, SlotType[] leftWorkingSet, SlotType[] rightWorkingSet, SlotType[] resultWorkingSet)
        {
            if (resultWorkingSet == null)
                return;
            for (uint bitIndex = minimumOffset; bitIndex < newLength; bitIndex++)
            {
                uint leftRelativeIndex      = (bitIndex - leftOffset),
                     rightRelativeIndex     = (bitIndex - rightOffset),
                     resultRelativeIndex    = (bitIndex - minimumOffset);
                if (((leftRelativeIndex >= 0 && leftRelativeIndex < leftLength) && ((leftWorkingSet[leftRelativeIndex / FiniteAutomataExtensions.SlotBitCount] &  (SlotType)(FiniteAutomataExtensions.ShiftValue  << (int)(leftRelativeIndex % FiniteAutomataExtensions.SlotBitCount))) != 0)) ^ leftIsNegativeSet ^
                    ((rightRelativeIndex >= 0 && rightRelativeIndex < rightLength) && ((rightWorkingSet[rightRelativeIndex / FiniteAutomataExtensions.SlotBitCount] & (SlotType)(FiniteAutomataExtensions.ShiftValue << (int)(rightRelativeIndex % FiniteAutomataExtensions.SlotBitCount))) != 0)) ^ rightIsNegativeSet)
                    resultWorkingSet[resultRelativeIndex / FiniteAutomataExtensions.SlotBitCount] |= (SlotType)(FiniteAutomataExtensions.ShiftValue << (int)(resultRelativeIndex % FiniteAutomataExtensions.SlotBitCount));
            }
        }

        /// <summary>
        /// Performs a bitwise or operation on the <paramref name="left"/>
        /// and <paramref name="right"/> sets returning a new <typeparamref name="TCheck"/>
        /// instance of the elements from both the <paramref name="left"/> set
        /// and the <paramref name="right"/> set.
        /// </summary>
        /// <param name="left">The left <typeparamref name="TCheck"/> set of the operation.</param>
        /// <param name="right">The right <typeparamref name="TCheck"/> set of the operation.</param>
        /// <returns>A new <typeparamref name="TCheck"/> instance as a union of the <paramref name="left"/> and <paramref name="right"/> sets.</returns>
        public static TCheck operator |(FiniteAutomataBitSet<TCheck> left, FiniteAutomataBitSet<TCheck> right)
        {
            if (Object.ReferenceEquals(left, null))
                if (right is TCheck)
                    return (TCheck)right;
                else
                    throw new ArgumentException(string.Format("left or right must be an instance of TCheck ({0})", typeof(TCheck).Name), "right");
            else if (Object.ReferenceEquals(right, null))
                if (left is TCheck)
                    return (TCheck)left;
                else
                    throw new ArgumentException(string.Format("left or right must be an instance of TCheck ({0})", typeof(TCheck).Name), "left");
            if (right is TCheck)
                return left.Union((TCheck)right);
            else if (left is TCheck)
                return right.Union((TCheck)left);
            else
                throw new ArgumentException(string.Format("left or right must be an instance of TCheck ({0})", typeof(TCheck).Name), "left");
        }

        /// <summary>Obtains the complement of the <paramref name="operand"/> provided.</summary>
        /// <param name="operand">The <see cref="FiniteAutomataBitSet{TCheck}"/> instance to obtain the complement of.</param>
        /// <returns>A new <typeparamref name="TCheck"/> which represents the complement of the <paramref name="operand"/>.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when the <paramref name="operand"/> is null.</exception>
        public static TCheck operator ~(FiniteAutomataBitSet<TCheck> operand)
        {
            if (operand == null)
                throw new ArgumentNullException("operand");
            return operand.Complement();
        }

        /// <summary>
        /// Performs a bitwise And operation on the <paramref name="left"/> and 
        /// <paramref name="right"/> sets returning a new <typeparamref name="TCheck"/>
        /// instance of the elements from the <paramref name="left"/> set that
        /// are also in the <paramref name="right"/> set.
        /// </summary>
        /// <param name="left">The left <typeparamref name="TCheck"/> 
        /// set of the operation.</param>
        /// <param name="right">The right <typeparamref name="TCheck"/> 
        /// set of the operation.</param>
        /// <returns>A new <typeparamref name="TCheck"/> instance which
        /// contains the elements from the <paramref name="left"/>
        /// set which are also contained within the <paramref name="right"/>
        /// set.</returns>
        public static TCheck operator &(FiniteAutomataBitSet<TCheck> left, FiniteAutomataBitSet<TCheck> right)
        {
            if (Object.ReferenceEquals(left, null) ||
                Object.ReferenceEquals(right, null))
                return null;
            if (right is TCheck)
                return left.Intersect((TCheck)right);
            else if (left is TCheck)
                return right.Intersect((TCheck)left);
            else
                throw new ArgumentException(string.Format("left or right must be an instance of TCheck ({0})", typeof(TCheck).Name), "left");
        }

        /// <summary>
        /// Performs a bitwise exclusive or operation on the 
        /// <paramref name="left"/> and <paramref name="right"/> sets.
        /// </summary>
        /// <param name="left">The left <typeparamref name="TCheck"/> 
        /// set of the operation.</param>
        /// <param name="right">The right <typeparamref name="TCheck"/> 
        /// set of the operation.</param>
        /// <returns>A new <typeparamref name="TCheck"/> instance
        /// with the elements from the <paramref name="left"/> set
        /// which aren't present in the <paramref name="right"/> set as well as the
        /// elements from the <paramref name="right"/> set which aren't present 
        /// in the <paramref name="left"/> set.</returns>
        public static TCheck operator ^(FiniteAutomataBitSet<TCheck> left, FiniteAutomataBitSet<TCheck> right)
        {
            if (Object.ReferenceEquals(left, null))
                if (right is TCheck)
                    return (TCheck)right;
                else
                    throw new ArgumentException(string.Format("left or right must be an instance of TCheck ({0})", typeof(TCheck).Name), "right");
            else if (Object.ReferenceEquals(right, null))
                if (left is TCheck)
                    return (TCheck)left;
                else
                    throw new ArgumentException(string.Format("left or right must be an instance of TCheck ({0})", typeof(TCheck).Name), "left");
            if (right is TCheck)
                return left.SymmetricDifference((TCheck)right);
            else if (left is TCheck)
                return right.SymmetricDifference((TCheck)left);
            else
                throw new ArgumentException(string.Format("left or right must be an instance of TCheck ({0})", typeof(TCheck).Name), "left");
        }

        /// <summary>
        /// Performs a bitwise exclusive or operation on the 
        /// <paramref name="left"/> and <paramref name="right"/> sets.
        /// </summary>
        /// <param name="left">The left <typeparamref name="TCheck"/> 
        /// set of the operation.</param>
        /// <param name="right">The right <typeparamref name="TCheck"/> 
        /// set of the operation.</param>
        /// <returns>A new <typeparamref name="TCheck"/> instance
        /// with the elements from the <paramref name="left"/> set
        /// which aren't present in the <paramref name="right"/> set as well as the
        /// elements from the <paramref name="right"/> set which aren't present 
        /// in the <paramref name="left"/> set.</returns>
        public static TCheck BitwiseExclusiveOr(TCheck left, TCheck right)
        {
            if (object.ReferenceEquals(left, null))
                return right;
            else if (object.ReferenceEquals(right, null))
                return left;
            return left.SymmetricDifference(right);
        }

        /// <summary>
        /// Performs a bitwise or operation on the <paramref name="left"/>
        /// and <paramref name="right"/> sets returning a new <typeparamref name="TCheck"/>
        /// instance of the elements from both the <paramref name="left"/> set
        /// and the <paramref name="right"/> set.
        /// </summary>
        /// <param name="left">The left <typeparamref name="TCheck"/> 
        /// set of the operation.</param>
        /// <param name="right">The right <typeparamref name="TCheck"/> 
        /// set of the operation.</param>
        /// <returns>A new <typeparamref name="TCheck"/> instance
        /// as a union of the <paramref name="left"/> and
        /// <paramref name="right"/> sets.</returns>
        public static TCheck BitwiseOr(TCheck left, TCheck right)
        {
            if (object.ReferenceEquals(left, null))
                return right;
            else if (object.ReferenceEquals(right, null))
                return left;
            return left.Union(right);
        }

        /// <summary>
        /// Performs a bitwise And operation on the <paramref name="left"/> and 
        /// <paramref name="right"/> sets returning a new <typeparamref name="TCheck"/>
        /// instance of the elements from the <paramref name="left"/> set that
        /// are also in the <paramref name="right"/> set.
        /// </summary>
        /// <param name="left">The left <typeparamref name="TCheck"/> 
        /// set of the operation.</param>
        /// <param name="right">The right <typeparamref name="TCheck"/> 
        /// set of the operation.</param>
        /// <returns>A new <typeparamref name="TCheck"/> instance which
        /// contains the elements from the <paramref name="left"/>
        /// set which are also contained within the <paramref name="right"/>
        /// set.</returns>
        public static TCheck BitwiseAnd(TCheck left, TCheck right)
        {
            if (object.ReferenceEquals(left, null) || object.ReferenceEquals(right, null))
                return null;
            return left.Intersect(right);
        }

        /// <summary>
        /// Subtracts the elements in the <paramref name="right"/> set
        /// from the <paramref name="left"/> set.
        /// </summary>
        /// <param name="left">The <typeparamref name="TCheck"/> instance which
        /// contains the primary set of elements.</param>
        /// <param name="right">The <typeparamref name="TCheck"/> instance which
        /// contains the secondary set of elements to remove from the <paramref name="left"/>
        /// set.</param>
        /// <returns>A new <typeparamref name="TCheck"/> instance with the
        /// elements from the <paramref name="left"/> set that are not an
        /// element of the <paramref name="right"/> set.</returns>
        public static TCheck SetSubtraction(TCheck left, TCheck right)
        {
            if (object.ReferenceEquals(left, null))
                return null;
            else if (object.ReferenceEquals(right, null))
                return left;
            return left.RelativeComplement(right);
        }

        public static int CountComputations()
        {
            return CountComputationCaches() +
                   unionCacheHits + 
                   symmetricDifferenceCacheHits +
                   intersectionCacheHits;
        }

        public static int CountComputationCaches()
        {
            return Sum(UnionCache, SymmetricDifferenceCache, IntersectCache);
        }

        private static int Sum(params IMultikeyedDictionary<TCheck, TCheck, TCheck>[] set)
        {
            int result = 0;
            foreach (var dict in set)
                result += dict.Count;
            return result;
        }

        /// <summary>Counts the number of bits within the set which are set, irrespective of whether the set is a negative set.</summary>
        /// <returns>A <see cref="UInt32"/> value denoting the number of bits, which are set, within the <see cref="FiniteAutomataBitSet{TCheck}"/>.</returns>
        /// <remarks>The number of bits observed are noted by <see cref="Length"/>.  The <see cref="FullLength"/> of the whole set is not considered.</remarks>
        public uint CountTrue()
        {
            uint r = 0;
            if (this.values == null)
                return 0;
            for (int i = 0; i < this.values.Length; i++)
                r += this.values[i].CountBits();
            return r;
        }

        /// <summary>Counts the number of bits within the set which are not set, irrespective of whether the set is a negative set.</summary>
        /// <returns>A <see cref="UInt32"/> value denoting the number of bits, which are not set, within the <see cref="FiniteAutomataBitSet{TCheck}"/>.</returns>
        /// <remarks>The number of bits observed are noted by <see cref="Length"/>.  The <see cref="FullLength"/> of the whole set is not considered.</remarks>
        public uint CountFalse()
        {
            uint r = 0;
            if (this.values == null)
                return 0;
            for (int i = 0; i < this.values.Length; i++)
                if (i == this.values.Length - 1)
                    r += (~this.values[i] & this.lastMask).CountBits();
                else
                    r += (~this.values[i]).CountBits();
            return r;
        }

        public uint TrueCount
        {
            get
            {
                if (this.IsNegativeSet)
                    return this.FullLength - CountFalse();
                else
                    return this.CountTrue();
            }
        }

        internal static void ClearCache()
        {
            lock (FiniteAutomataBitSet<TCheck>.intersectCacheLock)
                FiniteAutomataBitSet<TCheck>.IntersectCache.Clear();
            lock (FiniteAutomataBitSet<TCheck>.unionCacheLock)
                FiniteAutomataBitSet<TCheck>.UnionCache.Clear();
            lock (FiniteAutomataBitSet<TCheck>.symmetricDiffCacheLock)
                FiniteAutomataBitSet<TCheck>.SymmetricDifferenceCache.Clear();
            FiniteAutomataBitSet<TCheck>.NullInst = null;
            gettingSD = false;
        }

        internal static Func<TCheck, TCheck, TCheck> UnionAggregateDelegate = (a, b) =>
        {
            if (a == null || a.IsEmpty)
                return b;
            if (b == null || b.IsEmpty)
                return a;
            return a | b;
        };
    }
}
