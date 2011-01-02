using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Tuples
{
    // Module: RootModule
    public static class TupleHelper
    {
        #region TupleHelper methods
        /// <summary>
        /// Obtains a new tuple with the item provided
        /// </summary>
        public static Tuple<TItem1> GetTuple<TItem1>(TItem1 item)
        {
            return new Tuple<TItem1>(item);
        }

        /// <summary>
        /// Obtains a new tuple with the item, and item2 provided
        /// </summary>
        public static Tuple<TItem1, TItem2> GetTuple<TItem1, TItem2>(TItem1 item, TItem2 item2)
        {
            return new Tuple<TItem1, TItem2>(item, item2);
        }

        /// <summary>
        /// Obtains a new tuple with the item, item2, item3, and item4 provided
        /// </summary>
        public static Tuple<TItem1, TItem2, TItem3, TItem4> GetTuple<TItem1, TItem2, TItem3, TItem4>(TItem1 item, TItem2 item2, TItem3 item3, TItem4 item4)
        {
            return new Tuple<TItem1, TItem2, TItem3, TItem4>(item, item2, item3, item4);
        }

        /// <summary>
        /// Obtains a new tuple with the item, item2, item3, item4, item5, item6, item7, and item8 provided
        /// </summary>
        public static Tuple<TItem1, TItem2, TItem3, TItem4, TItem5, TItem6, TItem7, TItem8> GetTuple<TItem1, TItem2, TItem3, TItem4, TItem5, TItem6, TItem7, TItem8>(TItem1 item, TItem2 item2, TItem3 item3, TItem4 item4, TItem5 item5, TItem6 item6, TItem7 item7, TItem8 item8)
        {
            return new Tuple<TItem1, TItem2, TItem3, TItem4, TItem5, TItem6, TItem7, TItem8>(item, item2, item3, item4, item5, item6, item7, item8);
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> GetTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>(item1, item2, item3,
                item4, item5, item6,
                item7, item8, item9);
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> GetTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(item1, item2, item3,
                item4, item5, item6,
                item7, item8, item9,
                item10);
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GetTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(item1, item2, item3,
                item4, item5, item6,
                item7, item8, item9,
                item10, item11);
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> GetTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(item1, item2, item3,
                item4, item5, item6,
                item7, item8, item9,
                item10, item11, item12);
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> GetTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(item1, item2, item3,
                item4, item5, item6,
                item7, item8, item9,
                item10, item11, item12,
                item13);
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> GetTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(item1, item2, item3,
                item4, item5, item6,
                item7, item8, item9,
                item10, item11, item12,
                item13, item14);
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> GetTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(item1, item2, item3,
                item4, item5, item6,
                item7, item8, item9,
                item10, item11, item12,
                item13, item14, item15);
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> GetTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(item1, item2, item3,
                item4, item5, item6,
                item7, item8, item9,
                item10, item11, item12,
                item13, item14, item15,
                item16);
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> GetTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, T17 item17)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(item1, item2, item3,
                item4, item5, item6,
                item7, item8, item9,
                item10, item11, item12,
                item13, item14, item15,
                item16, item17);
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18> GetTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, T17 item17, T18 item18)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(item1, item2, item3,
                item4, item5, item6,
                item7, item8, item9,
                item10, item11, item12,
                item13, item14, item15,
                item16, item17, item18);
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19> GetTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, T17 item17, T18 item18, T19 item19)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(item1, item2, item3,
                item4, item5, item6,
                item7, item8, item9,
                item10, item11, item12,
                item13, item14, item15,
                item16, item17, item18,
                item19);
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20> GetTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, T17 item17, T18 item18, T19 item19, T20 item20)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(item1, item2, item3,
                item4, item5, item6,
                item7, item8, item9,
                item10, item11, item12,
                item13, item14, item15,
                item16, item17, item18,
                item19, item20);
        }
        #endregion // TupleHelper methods


    }
    // Module: RootModule
}
