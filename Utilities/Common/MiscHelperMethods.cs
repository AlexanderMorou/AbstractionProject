using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities
{
    /// <summary>
    /// Provides miscellaneous helper methods.
    /// </summary>
    public static class MiscHelperMethods
    {
        /// <summary>
        /// Obtains the caller information relative to the method that called
        /// this function.
        /// </summary>
        /// <returns>A <see cref="Tuple{T1, T2}"/> with the <see cref="Type"/> and
        /// <see cref="MethodBase"/> specific to the caller; or null if the 
        /// caller cannot be identified.</returns>
        /// <remarks>See: http://haacked.com/archive/2006/08/11/HowToGetTheCallingMethodAndType.aspx
        /// </remarks>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Tuple<Type, MethodBase> GetCallerInfo()
        {
            var sf = new StackFrame(2, false);
            var mi = sf.GetMethod();
            if (mi == null)
                return null;
            return new Tuple<Type, MethodBase>(mi.DeclaringType, mi);
        }
#if DEBUG   
        /// <summary>
        /// Obtains the caller information relative to the method that called
        /// this function.
        /// </summary>
        /// <returns>A <see cref="Tuple{T1, T2, T3, T4, T5}"/> with the <see cref="Type"/> and
        /// <see cref="MethodBase"/> specific to the caller; or null if the 
        /// caller cannot be identified.  The remaining fields are the filename
        /// line, and column.</returns>
        /// <remarks>See: http://haacked.com/archive/2006/08/11/HowToGetTheCallingMethodAndType.aspx
        /// </remarks>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Tuple<Type, MethodBase, string, int, int> GetDebugCallerInfo()
        {
            var sf = new StackFrame(2, true);
            var mi = sf.GetMethod();
            if (mi == null)
                return null;
            return Tuple.Create(mi.DeclaringType, mi, sf.GetFileName(), sf.GetFileLineNumber(), sf.GetFileColumnNumber());
        }

#endif

        public static Func<Tuple<TimeSpan, TResult>> CreateFunctionOfTime<TResult>(Func<TResult> target)
        {
            return () =>
            {
                Stopwatch timer = Stopwatch.StartNew();
                var fResult = target();
                timer.Stop();
                return new Tuple<TimeSpan, TResult>(timer.Elapsed, fResult);
            };
        }

        public static Func<T1, Tuple<TimeSpan, TResult>> CreateFunctionOfTime<T1, TResult>(Func<T1, TResult> target)
        {
            return (T1 arg1) =>
            {
                Stopwatch timer = Stopwatch.StartNew();
                var fResult = target(arg1);
                timer.Stop();
                return new Tuple<TimeSpan, TResult>(timer.Elapsed, fResult);
            };
        }

        public static Func<T1, T2, Tuple<TimeSpan, TResult>> CreateFunctionOfTime<T1, T2, TResult>(Func<T1, T2, TResult> target)
        {
            return (T1 arg1, T2 arg2) =>
            {
                Stopwatch timer = Stopwatch.StartNew();
                var fResult = target(arg1, arg2);
                timer.Stop();
                return new Tuple<TimeSpan, TResult>(timer.Elapsed, fResult);
            };
        }

        public static Func<TimeSpan> CreateActionOfTime(Action target)
        {
            return () =>
            {
                Stopwatch timer = Stopwatch.StartNew();
                target();
                timer.Stop();
                return timer.Elapsed;
            };
        }
        public static Func<T1, TimeSpan> CreateActionOfTime<T1>(Action<T1> target)
        {
            return (T1 arg1) =>
            {
                Stopwatch timer = Stopwatch.StartNew();
                target(arg1);
                timer.Stop();
                return timer.Elapsed;
            };
        }

        public static IEnumerable<Tuple<int, int>> Iterate(int start1, int start2, int end1, int end2, int step1 = 1, int step2 = 1)
        {
            if (end1 < start1 && step1 > 0)
                step1 = -step1;
            if (end2 < start2 && step2 > 0)
                step2 = -step2;
            for (int i = start1; i <= end1; i += step1)
                for (int j = start2; j < end2; j += step2)
                    yield return new Tuple<int, int>(i, j);
        }

        public static IEnumerable<Tuple<int, int, int>> Iterate(int start1, int start2, int start3, int end1, int end2, int end3, int step1 = 1, int step2 = 1, int step3 = 1)
        {
            if (end1 < start1 && step1 > 0)
                step1 = -step1;
            if (end2 < start2 && step2 > 0)
                step2 = -step2;
            if (end3 < start3 && step3 > 0)
                step3 = -step3;
            for (int i = start1; i <= end1; i += step1)
                for (int j = start2; j < end2; j += step2)
                    for (int k = start3; k < end3; k++)
                        yield return new Tuple<int, int, int>(i, j, k);
        }


        public static IEnumerable<Tuple<int, int, int, int>> Iterate(int start1, int start2, int start3, int start4, int end1, int end2, int end3, int end4, int step1 = 1, int step2 = 1, int step3 = 1, int step4 = 1)
        {
            if (end1 < start1 && step1 > 0)
                step1 = -step1;
            if (end2 < start2 && step2 > 0)
                step2 = -step2;
            if (end3 < start3 && step3 > 0)
                step3 = -step3;
            if (end4 < start4 && step4 > 0)
                step4 = -step4;
            for (int i = start1; i <= end1; i += step1)
                for (int j = start2; j < end2; j += step2)
                    for (int k = start3; k < end3; k++)
                        for (int l = start4; l < end4; l++)
                            yield return new Tuple<int, int, int, int>(i, j, k, l);
        }

        public static IEnumerable<Tuple<int, int, int, int, int>> Iterate(int start1, int start2, int start3, int start4, int start5, int end1, int end2, int end3, int end4, int end5, int step1 = 1, int step2 = 1, int step3 = 1, int step4 = 1, int step5 = 1)
        {
            if (end1 < start1 && step1 > 0)
                step1 = -step1;
            if (end2 < start2 && step2 > 0)
                step2 = -step2;
            if (end3 < start3 && step3 > 0)
                step3 = -step3;
            if (end4 < start4 && step4 > 0)
                step4 = -step4;
            if (end5 < start5 && step5 > 0)
                step5 = -step5;
            for (int i = start1; i <= end1; i += step1)
                for (int j = start2; j < end2; j += step2)
                    for (int k = start3; k < end3; k++)
                        for (int l = start4; l < end4; l++)
                            for (int m = start5; m < end5; m++)
                                yield return Tuple.Create(i, j, k, l, m);
        }

        public static IEnumerable<Tuple<int, int, int, int, int, int>> Iterate(int start1, int start2, int start3, int start4, int start5, int start6, int end1, int end2, int end3, int end4, int end5, int end6, int step1 = 1, int step2 = 1, int step3 = 1, int step4 = 1, int step5 = 1, int step6 = 1)
        {
            if (end1 < start1 && step1 > 0)
                step1 = -step1;
            if (end2 < start2 && step2 > 0)
                step2 = -step2;
            if (end3 < start3 && step3 > 0)
                step3 = -step3;
            if (end4 < start4 && step4 > 0)
                step4 = -step4;
            if (end5 < start5 && step5 > 0)
                step5 = -step5;
            if (end6 < start6 && step6 > 0)
                step6 = -step6;
            for (int i = start1; i <= end1; i += step1)
                for (int j = start2; j < end2; j += step2)
                    for (int k = start3; k < end3; k += step3)
                        for (int l = start4; l < end4; l += step4)
                            for (int m = start5; m < end5; m += step5)
                                for (int n = start6; n < end6; n += step6)
                                    yield return Tuple.Create(i, j, k, l, m, n);
        }

        public static IEnumerable<Tuple<int, int, int, int, int, int, int>> Iterate(int start1, int start2, int start3, int start4, int start5, int start6, int start7, int end1, int end2, int end3, int end4, int end5, int end6, int end7, int step1 = 1, int step2 = 1, int step3 = 1, int step4 = 1, int step5 = 1, int step6 = 1, int step7 = 1)
        {
            if (end1 < start1 && step1 > 0)
                step1 = -step1;
            if (end2 < start2 && step2 > 0)
                step2 = -step2;
            if (end3 < start3 && step3 > 0)
                step3 = -step3;
            if (end4 < start4 && step4 > 0)
                step4 = -step4;
            if (end5 < start5 && step5 > 0)
                step5 = -step5;
            if (end6 < start6 && step6 > 0)
                step6 = -step6;
            if (end7 < start7 && step7 > 0)
                step7 = -step7;
            for (int i = start1; i <= end1; i += step1)
                for (int j = start2; j < end2; j += step2)
                    for (int k = start3; k < end3; k += step3)
                        for (int l = start4; l < end4; l += step4)
                            for (int m = start5; m < end5; m += step5)
                                for (int n = start6; n < end6; n += step6)
                                    for (int o = start7; o < end7; o += step7)
                                        yield return Tuple.Create(i, j, k, l, m, n, o);
        }

        public static IEnumerable<Tuple<int, int, int, int, int, int, int, Tuple<int>>> Iterate(int start1, int start2, int start3, int start4, int start5, int start6, int start7, int start8, int end1, int end2, int end3, int end4, int end5, int end6, int end7, int end8, int step1 = 1, int step2 = 1, int step3 = 1, int step4 = 1, int step5 = 1, int step6 = 1, int step7 = 1, int step8 = 1)
        {
            if (end1 < start1 && step1 > 0)
                step1 = -step1;
            if (end2 < start2 && step2 > 0)
                step2 = -step2;
            if (end3 < start3 && step3 > 0)
                step3 = -step3;
            if (end4 < start4 && step4 > 0)
                step4 = -step4;
            if (end5 < start5 && step5 > 0)
                step5 = -step5;
            if (end6 < start6 && step6 > 0)
                step6 = -step6;
            if (end7 < start7 && step7 > 0)
                step7 = -step7;
            if (end8 < start8 && step8 > 0)
                step8 = -step8;
            for (int i = start1; i <= end1; i += step1)
                for (int j = start2; j < end2; j += step2)
                    for (int k = start3; k < end3; k += step3)
                        for (int l = start4; l < end4; l += step4)
                            for (int m = start5; m < end5; m += step5)
                                for (int n = start6; n < end6; n += step6)
                                    for (int o = start7; o < end7; o += step7)
                                        for (int p = start8; p < end8; p += step8)
                                            yield return Tuple.Create(i, j, k, l, m, n, o, p);
        }

        /// <summary>
        /// Times the <paramref name="func"/> with the <paramref name="arg"/>
        /// provided and and yields the <see cref="TimeSpan"/> and the
        /// <typeparamref name="TResult"/> in a tuple pair.
        /// </summary>
        /// <typeparam name="T">The type of parameter used to pass into the 
        /// <paramref name="func"/>.</typeparam>
        /// <typeparam name="TResult">The type of result yielded from
        /// the <paramref name="func"/>.</typeparam>
        /// <param name="func">The <see cref="Func{T, TResult}"/>
        /// to time using the <paramref name="arg"/> provided.</param>
        /// <param name="arg">The <typeparamref name="T"/>
        /// value to send to <paramref name="func"/> in order to perform
        /// the function.</param>
        /// <returns>A tuple pair of <see cref="TimeSpan"/> that denotes
        /// the time taken to complete the <paramref name="func"/>, and the
        /// <typeparamref name="TResult"/> which is the result of the 
        /// <paramref name="func"/>.</returns>
        public static Tuple<TimeSpan, TResult> TimeResult<T, TResult>(this Func<T, TResult> func, T arg)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            TResult result = func(arg);
            sw.Stop();
            return Tuple.Create(sw.Elapsed, result);
        }
    }
}