﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Tuples;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Common
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

        /// <summary>
        /// Creates a <see cref="Func{TResult}"/>
        /// of the given <paramref name="action"/> which
        /// yields a <see cref="TimeSpan"/> value on the length
        /// of time needed to complete the <paramref name="action"/>.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to wrap into a
        /// timing function.</param>
        /// <returns>A <see cref="Func{TResult}"/> of the given 
        /// <paramref name="action"/> which yields a <see cref="TimeSpan"/>
        /// value on the length of time needed to complete the
        /// <paramref name="action"/>.
        /// </returns>
        public static Func<TimeSpan> TimeActionFunc(this Action action)
        {
            return () => TimeAction(action);
        }

        /// <summary>
        /// Creates a <see cref="Func{TResult}"/> of the given
        /// <paramref name="action"/> which takes a single parameter of type
        /// <typeparamref name="T"/> which yields a <see cref="TimeSpan"/>
        /// value on the length of time needed to complete the 
        /// <paramref name="action"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter taken by the
        /// <paramref name="action"/> provided.</typeparam>
        /// <param name="action">The <see cref="Action{T}"/>
        /// which takes a single parameter which needs timed function
        /// produced for it..</param>
        /// <returns>A new <see cref="Func{T, TResult}"/> for the specified 
        /// <paramref name="action"/> which takes a single parameter of
        /// type <typeparamref name="T"/> which yields a <see cref="TimeSpan"/>
        /// value on the length of time needed to complete the 
        /// <paramref name="action"/>.
        /// </returns>
        public static Func<T, TimeSpan> TimeActionFunc<T>(this Action<T> action)
        {
            return (T arg) => TimeAction(action, arg);
        }

        /// <summary>
        /// Creates a <see cref="Func{TResult}"/> with a <paramref name="TimeSpan"/> and
        /// <typeparamref name="TResult"/> as the return within a <see cref="Tuple{T1, T2}"/>
        /// as a pair.
        /// </summary>
        /// <typeparam name="TResult">The type of the result of the <paramref name="func"/> to
        /// create a time function for.</typeparam>
        /// <param name="func">The <see cref="Func{TResult}"/> which needs a time metric
        /// function applied to it.</param>
        /// <returns>A <see cref="Func{TResult}"/> with a <paramref name="TimeSpan"/> and
        /// <typeparamref name="TResult"/> as the return within a <see cref="Tuple{T1, T2}"/>
        /// as a pair.</returns>
        public static Func<Tuple<TimeSpan, TResult>> TimeResultFunc<TResult>(this Func<TResult> func)
        {
            return () => TimeResult(func);
        }
        /// <summary>
        /// Creates a <see cref="Func{TResult}"/> with a <paramref name="TimeSpan"/> and
        /// <typeparamref name="TResult"/> as the return within a <see cref="Tuple{T1, T2}"/>
        /// as a pair.
        /// </summary>
        /// <typeparam name="TResult">The type of the result of the <paramref name="func"/> to
        /// create a time function for.</typeparam>
        /// <param name="func">The <see cref="Func{TResult}"/> which needs a time metric
        /// function applied to it.</param>
        /// <returns>A <see cref="Func{TResult}"/> with a <paramref name="TimeSpan"/> and
        /// <typeparamref name="TResult"/> as the return within a <see cref="Tuple{T1, T2}"/>
        /// as a pair.</returns>
        public static Func<Tuple<TimeSpan, TResult>> TimeResultFunc<T, TResult>(this Func<T, TResult> func, Func<T> arg)
        {
            return () => TimeResult(func, arg());
        }
        /// <summary>
        /// Creates a <see cref="Func{TResult}"/> with a <paramref name="TimeSpan"/> and
        /// <typeparamref name="TResult"/> as the return within a <see cref="Tuple{T1, T2}"/>
        /// as a pair.
        /// </summary>
        /// <typeparam name="TResult">The type of the result of the <paramref name="func"/> to
        /// create a time function for.</typeparam>
        /// <param name="func">The <see cref="Func{TResult}"/> which needs a time metric
        /// function applied to it.</param>
        /// <returns>A <see cref="Func{TResult}"/> with a <paramref name="TimeSpan"/> and
        /// <typeparamref name="TResult"/> as the return within a <see cref="Tuple{T1, T2}"/>
        /// as a pair.</returns>
        public static Func<Tuple<TimeSpan, TResult>> TimeResultFunc<T, TResult>(this Func<T, TResult> func, T arg)
        {
            return () => TimeResult(func, arg);
        }

        public static TimeSpan TimeAction(this Action action)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            action();
            sw.Stop();
            return sw.Elapsed;
        }

        public static TimeSpan TimeAction<T>(this Action<T> action, T arg)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            action(arg);
            sw.Stop();
            return sw.Elapsed;
        }

        public static Tuple<TimeSpan, TResult> TimeResult<TResult>(this Func<TResult> func)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            TResult result = func();
            sw.Stop();
            return Tuple.Create(sw.Elapsed, result);
        }
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