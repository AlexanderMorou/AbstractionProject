using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;
/*---------------------------------------------------------------------\
| Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities
{
    // Module: RootModule
    /// <summary>
    /// Defines static methods for creating nonuple (9-tuple) through
    /// the unvigenuple (21-tuple).
    /// </summary>
    /// <remarks>This finishes out the second tuple depth, started by <see cref="Tuple.Create{T1,T2,T3,T4,T5,T6,T7,T8}(T1,T2,T3,T4,T5,T6,T7,T8)"/>
    /// and finishes out the third tuple depth.</remarks>
    public static class TupleHelper
    {
        #region TupleHelper methods
        /// <summary>
        /// Creates a new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> nonuple
        /// with the primary through nonary components provided.
        /// </summary>
        /// <typeparam name="T1">The type of the nonuple's primary component.</typeparam>
        /// <typeparam name="T2">The type of the nonuple's secondary component.</typeparam>
        /// <typeparam name="T3">The type of the nonuple's tertiary component.</typeparam>
        /// <typeparam name="T4">The type of the nonuple's quaternary component.</typeparam>
        /// <typeparam name="T5">The type of the nonuple's quinary component.</typeparam>
        /// <typeparam name="T6">The type of the nonuple's senary component.</typeparam>
        /// <typeparam name="T7">The type of the nonuple's septenary component.</typeparam>
        /// <typeparam name="T8">The type of the nonuple's octonary component.</typeparam>
        /// <typeparam name="T9">The type of the nonuple's nonary component.</typeparam>
        /// <param name="item1">The value of the nonuple's primary component.</param>
        /// <param name="item2">The value of the nonuple's secondary component.</param>
        /// <param name="item3">The value of the nonuple's tertiary component.</param>
        /// <param name="item4">The value of the nonuple's quaternary component.</param>
        /// <param name="item5">The value of the nonuple's quinary component.</param>
        /// <param name="item6">The value of the nonuple's senary component.</param>
        /// <param name="item7">The value of the nonuple's septenary component.</param>
        /// <param name="item8">The value of the nonuple's octonary component.</param>
        /// <param name="item9">The value of the nonuple's nonary component.</param>
        /// <returns>A new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> nonuple instance
        /// with the first through ninth components provided.</returns>
        [StaticContainerExtensionAttribute(typeof(Tuple))]
        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9>> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9>>(item1, item2, item3,
                item4, item5, item6,
                item7, new Tuple<T8, T9>(item8, item9));
        }

        /// <summary>
        /// Creates a new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> decuple
        /// with the first through tenth components provided.
        /// </summary>
        /// <typeparam name="T1">The type of the decuple's primary component.</typeparam>
        /// <typeparam name="T2">The type of the decuple's secondary component.</typeparam>
        /// <typeparam name="T3">The type of the decuple's tertiary component.</typeparam>
        /// <typeparam name="T4">The type of the decuple's quaternary component.</typeparam>
        /// <typeparam name="T5">The type of the decuple's quinary component.</typeparam>
        /// <typeparam name="T6">The type of the decuple's senary component.</typeparam>
        /// <typeparam name="T7">The type of the decuple's septenary component.</typeparam>
        /// <typeparam name="T8">The type of the decuple's octonary component.</typeparam>
        /// <typeparam name="T9">The type of the decuple's nonary component.</typeparam>
        /// <typeparam name="T10">The type of the decuple's denary component.</typeparam>
        /// <param name="item1">The value of the decuple's primary component.</param>
        /// <param name="item2">The value of the decuple's secondary component.</param>
        /// <param name="item3">The value of the decuple's tertiary component.</param>
        /// <param name="item4">The value of the decuple's quaternary component.</param>
        /// <param name="item5">The value of the decuple's quinary component.</param>
        /// <param name="item6">The value of the decuple's senary component.</param>
        /// <param name="item7">The value of the decuple's septenary component.</param>
        /// <param name="item8">The value of the decuple's octonary component.</param>
        /// <param name="item9">The value of the decuple's nonary component.</param>
        /// <param name="item10">The value of the decuple's denary component.</param>
        /// <returns>A new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> decuple
        /// with the first through tenth components provided.</returns>
        [StaticContainerExtensionAttribute(typeof(Tuple))]
        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10>> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10>>(item1, item2, item3,
                item4, item5, item6,
                item7, new Tuple<T8, T9, T10>(item8, item9,
                item10));
        }

        /// <summary>
        /// Creates a new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> undecuple
        /// with the first through eleventh components provided.
        /// </summary>
        /// <typeparam name="T1">The type of the undecuple's primary component.</typeparam>
        /// <typeparam name="T2">The type of the undecuple's secondary component.</typeparam>
        /// <typeparam name="T3">The type of the undecuple's tertiary component.</typeparam>
        /// <typeparam name="T4">The type of the undecuple's quaternary component.</typeparam>
        /// <typeparam name="T5">The type of the undecuple's quinary component.</typeparam>
        /// <typeparam name="T6">The type of the undecuple's senary component.</typeparam>
        /// <typeparam name="T7">The type of the undecuple's septenary component.</typeparam>
        /// <typeparam name="T8">The type of the undecuple's octonary component.</typeparam>
        /// <typeparam name="T9">The type of the undecuple's nonary component.</typeparam>
        /// <typeparam name="T10">The type of the undecuple's denary component.</typeparam>
        /// <typeparam name="T11">The type of the undecuple's undenary component.</typeparam>
        /// <param name="item1">The value of the undecuple's primary component.</param>
        /// <param name="item2">The value of the undecuple's secondary component.</param>
        /// <param name="item3">The value of the undecuple's tertiary component.</param>
        /// <param name="item4">The value of the undecuple's quaternary component.</param>
        /// <param name="item5">The value of the undecuple's quinary component.</param>
        /// <param name="item6">The value of the undecuple's senary component.</param>
        /// <param name="item7">The value of the undecuple's septenary component.</param>
        /// <param name="item8">The value of the undecuple's octonary component.</param>
        /// <param name="item9">The value of the undecuple's nonary component.</param>
        /// <param name="item10">The value of the undecuple's denary component.</param>
        /// <param name="item11">The value of the undecuple's undenary component.</param>
        /// <returns>A new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> undecuple
        /// with the first through eleventh components provided.</returns>
        [StaticContainerExtensionAttribute(typeof(Tuple))]
        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11>> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11>>(item1, item2, item3,
                item4, item5, item6,
                item7, new Tuple<T8, T9, T10, T11>(item8, item9,
                item10, item11));
        }

        /// <summary>
        /// Creates a new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> duodecuple
        /// with the first through twelfth components provided.
        /// </summary>
        /// <typeparam name="T1">The type of the duodecuple's primary component.</typeparam>
        /// <typeparam name="T2">The type of the duodecuple's secondary component.</typeparam>
        /// <typeparam name="T3">The type of the duodecuple's tertiary component.</typeparam>
        /// <typeparam name="T4">The type of the duodecuple's quaternary component.</typeparam>
        /// <typeparam name="T5">The type of the duodecuple's quinary component.</typeparam>
        /// <typeparam name="T6">The type of the duodecuple's senary component.</typeparam>
        /// <typeparam name="T7">The type of the duodecuple's septenary component.</typeparam>
        /// <typeparam name="T8">The type of the duodecuple's octonary component.</typeparam>
        /// <typeparam name="T9">The type of the duodecuple's nonary component.</typeparam>
        /// <typeparam name="T10">The type of the duodecuple's denary component.</typeparam>
        /// <typeparam name="T11">The type of the duodecuple's undenary component.</typeparam>
        /// <typeparam name="T12">The type of the duodecuple's duodenary component.</typeparam>
        /// <param name="item1">The value of the duodecuple's primary component.</param>
        /// <param name="item2">The value of the duodecuple's secondary component.</param>
        /// <param name="item3">The value of the duodecuple's tertiary component.</param>
        /// <param name="item4">The value of the duodecuple's quaternary component.</param>
        /// <param name="item5">The value of the duodecuple's quinary component.</param>
        /// <param name="item6">The value of the duodecuple's senary component.</param>
        /// <param name="item7">The value of the duodecuple's septenary component.</param>
        /// <param name="item8">The value of the duodecuple's octonary component.</param>
        /// <param name="item9">The value of the duodecuple's nonary component.</param>
        /// <param name="item10">The value of the duodecuple's denary component.</param>
        /// <param name="item11">The value of the duodecuple's undenary component.</param>
        /// <param name="item12">The value of the duodecuple's duodenary component.</param>
        /// <returns>A new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> duodecuple
        /// with the first through twelfth components provided.</returns>
        [StaticContainerExtensionAttribute(typeof(Tuple))]
        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12>> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12>>(item1, item2, item3,
                item4, item5, item6,
                item7, new Tuple<T8, T9, T10, T11, T12>(item8, item9,
                item10, item11, item12));
        }

        /// <summary>
        /// Creates a new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> tredecuple
        /// with the first through thirteenth components provided.
        /// </summary>
        /// <typeparam name="T1">The type of the tredecuple's primary component.</typeparam>
        /// <typeparam name="T2">The type of the tredecuple's secondary component.</typeparam>
        /// <typeparam name="T3">The type of the tredecuple's tertiary component.</typeparam>
        /// <typeparam name="T4">The type of the tredecuple's quaternary component.</typeparam>
        /// <typeparam name="T5">The type of the tredecuple's quinary component.</typeparam>
        /// <typeparam name="T6">The type of the tredecuple's senary component.</typeparam>
        /// <typeparam name="T7">The type of the tredecuple's septenary component.</typeparam>
        /// <typeparam name="T8">The type of the tredecuple's octonary component.</typeparam>
        /// <typeparam name="T9">The type of the tredecuple's nonary component.</typeparam>
        /// <typeparam name="T10">The type of the tredecuple's denary component.</typeparam>
        /// <typeparam name="T11">The type of the tredecuple's undenary component.</typeparam>
        /// <typeparam name="T12">The type of the tredecuple's duodenary component.</typeparam>
        /// <typeparam name="T13">The type of the tredecuple's tredenary component.</typeparam>
        /// <param name="item1">The value of the tredecuple's primary component.</param>
        /// <param name="item2">The value of the tredecuple's secondary component.</param>
        /// <param name="item3">The value of the tredecuple's tertiary component.</param>
        /// <param name="item4">The value of the tredecuple's quaternary component.</param>
        /// <param name="item5">The value of the tredecuple's quinary component.</param>
        /// <param name="item6">The value of the tredecuple's senary component.</param>
        /// <param name="item7">The value of the tredecuple's septenary component.</param>
        /// <param name="item8">The value of the tredecuple's octonary component.</param>
        /// <param name="item9">The value of the tredecuple's nonary component.</param>
        /// <param name="item10">The value of the tredecuple's denary component.</param>
        /// <param name="item11">The value of the tredecuple's undenary component.</param>
        /// <param name="item12">The value of the tredecuple's duodenary component.</param>
        /// <param name="item13">The value of the tredecuple's tredenary component.</param>
        /// <returns>A new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> tredecuple
        /// with the first through thirteenth components provided.</returns>
        [StaticContainerExtensionAttribute(typeof(Tuple))]
        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13>> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13>>(item1, item2, item3,
                item4, item5, item6,
                item7, new Tuple<T8, T9, T10, T11, T12, T13>(item8, item9,
                item10, item11, item12,
                item13));
        }

        /// <summary>
        /// Creates a new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> quattuordecuple
        /// with the first through fourteenth components provided.
        /// </summary>
        /// <typeparam name="T1">The type of the quattuordecuple's primary component.</typeparam>
        /// <typeparam name="T2">The type of the quattuordecuple's secondary component.</typeparam>
        /// <typeparam name="T3">The type of the quattuordecuple's tertiary component.</typeparam>
        /// <typeparam name="T4">The type of the quattuordecuple's quaternary component.</typeparam>
        /// <typeparam name="T5">The type of the quattuordecuple's quinary component.</typeparam>
        /// <typeparam name="T6">The type of the quattuordecuple's senary component.</typeparam>
        /// <typeparam name="T7">The type of the quattuordecuple's septenary component.</typeparam>
        /// <typeparam name="T8">The type of the quattuordecuple's octonary component.</typeparam>
        /// <typeparam name="T9">The type of the quattuordecuple's nonary component.</typeparam>
        /// <typeparam name="T10">The type of the quattuordecuple's denary component.</typeparam>
        /// <typeparam name="T11">The type of the quattuordecuple's undenary component.</typeparam>
        /// <typeparam name="T12">The type of the quattuordecuple's duodenary component.</typeparam>
        /// <typeparam name="T13">The type of the quattuordecuple's tredenary component.</typeparam>
        /// <typeparam name="T14">The type of the quattuordecuple's quattuordenary component.</typeparam>
        /// <param name="item1">The value of the quattuordecuple's primary component.</param>
        /// <param name="item2">The value of the quattuordecuple's secondary component.</param>
        /// <param name="item3">The value of the quattuordecuple's tertiary component.</param>
        /// <param name="item4">The value of the quattuordecuple's quaternary component.</param>
        /// <param name="item5">The value of the quattuordecuple's quinary component.</param>
        /// <param name="item6">The value of the quattuordecuple's senary component.</param>
        /// <param name="item7">The value of the quattuordecuple's septenary component.</param>
        /// <param name="item8">The value of the quattuordecuple's octonary component.</param>
        /// <param name="item9">The value of the quattuordecuple's nonary component.</param>
        /// <param name="item10">The value of the quattuordecuple's denary component.</param>
        /// <param name="item11">The value of the quattuordecuple's undenary component.</param>
        /// <param name="item12">The value of the quattuordecuple's duodenary component.</param>
        /// <param name="item13">The value of the quattuordecuple's tredenary component.</param>
        /// <param name="item14">The value of the quattuordecuple's quattuordenary component.</param>
        /// <returns>A new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> quattuordecuple
        /// with the first through fourteenth components provided.</returns>
        [StaticContainerExtensionAttribute(typeof(Tuple))]
        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14>> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14>>(item1, item2, item3,
                item4, item5, item6,
                item7, new Tuple<T8, T9, T10, T11, T12, T13, T14>(item8, item9,
                item10, item11, item12,
                item13, item14));
        }

        /// <summary>
        /// Creates a new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> quindecuple
        /// with the first through fifteenth components provided.
        /// </summary>
        /// <typeparam name="T1">The type of the quindecuple's primary component.</typeparam>
        /// <typeparam name="T2">The type of the quindecuple's secondary component.</typeparam>
        /// <typeparam name="T3">The type of the quindecuple's tertiary component.</typeparam>
        /// <typeparam name="T4">The type of the quindecuple's quaternary component.</typeparam>
        /// <typeparam name="T5">The type of the quindecuple's quinary component.</typeparam>
        /// <typeparam name="T6">The type of the quindecuple's senary component.</typeparam>
        /// <typeparam name="T7">The type of the quindecuple's septenary component.</typeparam>
        /// <typeparam name="T8">The type of the quindecuple's octonary component.</typeparam>
        /// <typeparam name="T9">The type of the quindecuple's nonary component.</typeparam>
        /// <typeparam name="T10">The type of the quindecuple's denary component.</typeparam>
        /// <typeparam name="T11">The type of the quindecuple's undenary component.</typeparam>
        /// <typeparam name="T12">The type of the quindecuple's duodenary component.</typeparam>
        /// <typeparam name="T13">The type of the quindecuple's tredenary component.</typeparam>
        /// <typeparam name="T14">The type of the quindecuple's quattuordenary component.</typeparam>
        /// <typeparam name="T15">The type of the quindecuple's quindenary component.</typeparam>
        /// <param name="item1">The value of the quindecuple's primary component.</param>
        /// <param name="item2">The value of the quindecuple's secondary component.</param>
        /// <param name="item3">The value of the quindecuple's tertiary component.</param>
        /// <param name="item4">The value of the quindecuple's quaternary component.</param>
        /// <param name="item5">The value of the quindecuple's quinary component.</param>
        /// <param name="item6">The value of the quindecuple's senary component.</param>
        /// <param name="item7">The value of the quindecuple's septenary component.</param>
        /// <param name="item8">The value of the quindecuple's octonary component.</param>
        /// <param name="item9">The value of the quindecuple's nonary component.</param>
        /// <param name="item10">The value of the quindecuple's denary component.</param>
        /// <param name="item11">The value of the quindecuple's undenary component.</param>
        /// <param name="item12">The value of the quindecuple's duodenary component.</param>
        /// <param name="item13">The value of the quindecuple's tredenary component.</param>
        /// <param name="item14">The value of the quindecuple's quattuordenary component.</param>
        /// <param name="item15">The value of the quindecuple's quindenary component.</param>
        /// <returns>A new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> quindecuple
        /// with the first through fifteenth components provided.</returns>
        [StaticContainerExtensionAttribute(typeof(Tuple))]
        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15>>> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15>>>(item1, item2, item3,
                item4, item5, item6,
                item7, new Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15>>(item8, item9,
                item10, item11, item12,
                item13, item14, Tuple.Create(item15)));
        }

        /// <summary>
        /// Creates a new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> sexdecuple
        /// with the first through sixteenth components provided.
        /// </summary>
        /// <typeparam name="T1">The type of the sexdecuple's primary component.</typeparam>
        /// <typeparam name="T2">The type of the sexdecuple's secondary component.</typeparam>
        /// <typeparam name="T3">The type of the sexdecuple's tertiary component.</typeparam>
        /// <typeparam name="T4">The type of the sexdecuple's quaternary component.</typeparam>
        /// <typeparam name="T5">The type of the sexdecuple's quinary component.</typeparam>
        /// <typeparam name="T6">The type of the sexdecuple's senary component.</typeparam>
        /// <typeparam name="T7">The type of the sexdecuple's septenary component.</typeparam>
        /// <typeparam name="T8">The type of the sexdecuple's octonary component.</typeparam>
        /// <typeparam name="T9">The type of the sexdecuple's nonary component.</typeparam>
        /// <typeparam name="T10">The type of the sexdecuple's denary component.</typeparam>
        /// <typeparam name="T11">The type of the sexdecuple's undenary component.</typeparam>
        /// <typeparam name="T12">The type of the sexdecuple's duodenary component.</typeparam>
        /// <typeparam name="T13">The type of the sexdecuple's tredenary component.</typeparam>
        /// <typeparam name="T14">The type of the sexdecuple's quattuordenary component.</typeparam>
        /// <typeparam name="T15">The type of the sexdecuple's quindenary component.</typeparam>
        /// <typeparam name="T16">The type of the sexdecuple's sedenary component.</typeparam>
        /// <param name="item1">The value of the sexdecuple's primary component.</param>
        /// <param name="item2">The value of the sexdecuple's secondary component.</param>
        /// <param name="item3">The value of the sexdecuple's tertiary component.</param>
        /// <param name="item4">The value of the sexdecuple's quaternary component.</param>
        /// <param name="item5">The value of the sexdecuple's quinary component.</param>
        /// <param name="item6">The value of the sexdecuple's senary component.</param>
        /// <param name="item7">The value of the sexdecuple's septenary component.</param>
        /// <param name="item8">The value of the sexdecuple's octonary component.</param>
        /// <param name="item9">The value of the sexdecuple's nonary component.</param>
        /// <param name="item10">The value of the sexdecuple's denary component.</param>
        /// <param name="item11">The value of the sexdecuple's undenary component.</param>
        /// <param name="item12">The value of the sexdecuple's duodenary component.</param>
        /// <param name="item13">The value of the sexdecuple's tredenary component.</param>
        /// <param name="item14">The value of the sexdecuple's quattuordenary component.</param>
        /// <param name="item15">The value of the sexdecuple's quindenary component.</param>
        /// <param name="item16">The value of the sexdecuple's sedenary component.</param>
        /// <returns>A new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> sexdecuple
        /// with the first through sixteenth components provided.</returns>
        [StaticContainerExtensionAttribute(typeof(Tuple))]
        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16>>> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16>>>(item1, item2, item3,
                item4, item5, item6,
                item7, new Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16>>(item8, item9,
                item10, item11, item12,
                item13, item14, Tuple.Create(item15,
                item16)));
        }

        /// <summary>
        /// Creates a new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> septendecuple
        /// with the first through seventeenth components provided.
        /// </summary>
        /// <typeparam name="T1">The type of the septendecuple's primary component.</typeparam>
        /// <typeparam name="T2">The type of the septendecuple's secondary component.</typeparam>
        /// <typeparam name="T3">The type of the septendecuple's tertiary component.</typeparam>
        /// <typeparam name="T4">The type of the septendecuple's quaternary component.</typeparam>
        /// <typeparam name="T5">The type of the septendecuple's quinary component.</typeparam>
        /// <typeparam name="T6">The type of the septendecuple's senary component.</typeparam>
        /// <typeparam name="T7">The type of the septendecuple's septenary component.</typeparam>
        /// <typeparam name="T8">The type of the septendecuple's octonary component.</typeparam>
        /// <typeparam name="T9">The type of the septendecuple's nonary component.</typeparam>
        /// <typeparam name="T10">The type of the septendecuple's denary component.</typeparam>
        /// <typeparam name="T11">The type of the septendecuple's undenary component.</typeparam>
        /// <typeparam name="T12">The type of the septendecuple's duodenary component.</typeparam>
        /// <typeparam name="T13">The type of the septendecuple's tredenary component.</typeparam>
        /// <typeparam name="T14">The type of the septendecuple's quattuordenary component.</typeparam>
        /// <typeparam name="T15">The type of the septendecuple's quindenary component.</typeparam>
        /// <typeparam name="T16">The type of the septendecuple's sedenary component.</typeparam>
        /// <typeparam name="T17">The type of the septendecuple's septendenary component.</typeparam>
        /// <param name="item1">The value of the septendecuple's primary component.</param>
        /// <param name="item2">The value of the septendecuple's secondary component.</param>
        /// <param name="item3">The value of the septendecuple's tertiary component.</param>
        /// <param name="item4">The value of the septendecuple's quaternary component.</param>
        /// <param name="item5">The value of the septendecuple's quinary component.</param>
        /// <param name="item6">The value of the septendecuple's senary component.</param>
        /// <param name="item7">The value of the septendecuple's septenary component.</param>
        /// <param name="item8">The value of the septendecuple's octonary component.</param>
        /// <param name="item9">The value of the septendecuple's nonary component.</param>
        /// <param name="item10">The value of the septendecuple's denary component.</param>
        /// <param name="item11">The value of the septendecuple's undenary component.</param>
        /// <param name="item12">The value of the septendecuple's duodenary component.</param>
        /// <param name="item13">The value of the septendecuple's tredenary component.</param>
        /// <param name="item14">The value of the septendecuple's quattuordenary component.</param>
        /// <param name="item15">The value of the septendecuple's quindenary component.</param>
        /// <param name="item16">The value of the septendecuple's sedenary component.</param>
        /// <param name="item17">The value of the septendecuple's septendenary component.</param>
        /// <returns>A new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> septendecuple
        /// with the first through seventeenth components provided.</returns>
        [StaticContainerExtensionAttribute(typeof(Tuple))]
        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17>>> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, T17 item17)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17>>>(item1, item2, item3,
                item4, item5, item6,
                item7, new Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17>>(item8, item9,
                item10, item11, item12,
                item13, item14, Tuple.Create(item15,
                item16, item17)));
        }

        /// <summary>
        /// Creates a new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> octodecuple
        /// with the first through eighteenth components provided.
        /// </summary>
        /// <typeparam name="T1">The type of the octodecuple's primary component.</typeparam>
        /// <typeparam name="T2">The type of the octodecuple's secondary component.</typeparam>
        /// <typeparam name="T3">The type of the octodecuple's tertiary component.</typeparam>
        /// <typeparam name="T4">The type of the octodecuple's quaternary component.</typeparam>
        /// <typeparam name="T5">The type of the octodecuple's quinary component.</typeparam>
        /// <typeparam name="T6">The type of the octodecuple's senary component.</typeparam>
        /// <typeparam name="T7">The type of the octodecuple's septenary component.</typeparam>
        /// <typeparam name="T8">The type of the octodecuple's octonary component.</typeparam>
        /// <typeparam name="T9">The type of the octodecuple's nonary component.</typeparam>
        /// <typeparam name="T10">The type of the octodecuple's denary component.</typeparam>
        /// <typeparam name="T11">The type of the octodecuple's undenary component.</typeparam>
        /// <typeparam name="T12">The type of the octodecuple's duodenary component.</typeparam>
        /// <typeparam name="T13">The type of the octodecuple's tredenary component.</typeparam>
        /// <typeparam name="T14">The type of the octodecuple's quattuordenary component.</typeparam>
        /// <typeparam name="T15">The type of the octodecuple's quindenary component.</typeparam>
        /// <typeparam name="T16">The type of the octodecuple's sedenary component.</typeparam>
        /// <typeparam name="T17">The type of the octodecuple's septendenary component.</typeparam>
        /// <typeparam name="T18">The type of the octodecuple's octodenary component.</typeparam>
        /// <param name="item1">The value of the octodecuple's primary component.</param>
        /// <param name="item2">The value of the octodecuple's secondary component.</param>
        /// <param name="item3">The value of the octodecuple's tertiary component.</param>
        /// <param name="item4">The value of the octodecuple's quaternary component.</param>
        /// <param name="item5">The value of the octodecuple's quinary component.</param>
        /// <param name="item6">The value of the octodecuple's senary component.</param>
        /// <param name="item7">The value of the octodecuple's septenary component.</param>
        /// <param name="item8">The value of the octodecuple's octonary component.</param>
        /// <param name="item9">The value of the octodecuple's nonary component.</param>
        /// <param name="item10">The value of the octodecuple's denary component.</param>
        /// <param name="item11">The value of the octodecuple's undenary component.</param>
        /// <param name="item12">The value of the octodecuple's duodenary component.</param>
        /// <param name="item13">The value of the octodecuple's tredenary component.</param>
        /// <param name="item14">The value of the octodecuple's quattuordenary component.</param>
        /// <param name="item15">The value of the octodecuple's quindenary component.</param>
        /// <param name="item16">The value of the octodecuple's sedenary component.</param>
        /// <param name="item17">The value of the octodecuple's septendenary component.</param>
        /// <param name="item18">The value of the octodecuple's octodenary component.</param>
        /// <returns>A new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> octodecuple
        /// with the first through eighteenth components provided.</returns>
        [StaticContainerExtensionAttribute(typeof(Tuple))]
        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18>>> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, T17 item17, T18 item18)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18>>>(item1, item2, item3,
                item4, item5, item6,
                item7, new Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18>>(item8, item9,
                item10, item11, item12,
                item13, item14, Tuple.Create(item15,
                item16, item17, item18)));
        }

        /// <summary>
        /// Creates a new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> novemdecuple
        /// with the first through nineteenth components provided.
        /// </summary>
        /// <typeparam name="T1">The type of the novemdecuple's primary component.</typeparam>
        /// <typeparam name="T2">The type of the novemdecuple's secondary component.</typeparam>
        /// <typeparam name="T3">The type of the novemdecuple's tertiary component.</typeparam>
        /// <typeparam name="T4">The type of the novemdecuple's quaternary component.</typeparam>
        /// <typeparam name="T5">The type of the novemdecuple's quinary component.</typeparam>
        /// <typeparam name="T6">The type of the novemdecuple's senary component.</typeparam>
        /// <typeparam name="T7">The type of the novemdecuple's septenary component.</typeparam>
        /// <typeparam name="T8">The type of the novemdecuple's octonary component.</typeparam>
        /// <typeparam name="T9">The type of the novemdecuple's nonary component.</typeparam>
        /// <typeparam name="T10">The type of the novemdecuple's denary component.</typeparam>
        /// <typeparam name="T11">The type of the novemdecuple's undenary component.</typeparam>
        /// <typeparam name="T12">The type of the novemdecuple's duodenary component.</typeparam>
        /// <typeparam name="T13">The type of the novemdecuple's tredenary component.</typeparam>
        /// <typeparam name="T14">The type of the novemdecuple's quattuordenary component.</typeparam>
        /// <typeparam name="T15">The type of the novemdecuple's quindenary component.</typeparam>
        /// <typeparam name="T16">The type of the novemdecuple's sedenary component.</typeparam>
        /// <typeparam name="T17">The type of the novemdecuple's septendenary component.</typeparam>
        /// <typeparam name="T18">The type of the novemdecuple's octodenary component.</typeparam>
        /// <typeparam name="T19">The type of the novemdecuple's novemdenary component.</typeparam>
        /// <param name="item1">The value of the novemdecuple's primary component.</param>
        /// <param name="item2">The value of the novemdecuple's secondary component.</param>
        /// <param name="item3">The value of the novemdecuple's tertiary component.</param>
        /// <param name="item4">The value of the novemdecuple's quaternary component.</param>
        /// <param name="item5">The value of the novemdecuple's quinary component.</param>
        /// <param name="item6">The value of the novemdecuple's senary component.</param>
        /// <param name="item7">The value of the novemdecuple's septenary component.</param>
        /// <param name="item8">The value of the novemdecuple's octonary component.</param>
        /// <param name="item9">The value of the novemdecuple's nonary component.</param>
        /// <param name="item10">The value of the novemdecuple's denary component.</param>
        /// <param name="item11">The value of the novemdecuple's undenary component.</param>
        /// <param name="item12">The value of the novemdecuple's duodenary component.</param>
        /// <param name="item13">The value of the novemdecuple's tredenary component.</param>
        /// <param name="item14">The value of the novemdecuple's quattuordenary component.</param>
        /// <param name="item15">The value of the novemdecuple's quindenary component.</param>
        /// <param name="item16">The value of the novemdecuple's sedenary component.</param>
        /// <param name="item17">The value of the novemdecuple's septendenary component.</param>
        /// <param name="item18">The value of the novemdecuple's octodenary component.</param>
        /// <param name="item19">The value of the novemdecuple's novemdenary component.</param>
        /// <returns>A new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> novemdecuple
        /// with the first through nineteenth components provided.</returns>
        [StaticContainerExtensionAttribute(typeof(Tuple))]
        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19>>> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, T17 item17, T18 item18, T19 item19)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19>>>(item1, item2, item3,
                item4, item5, item6,
                item7, new Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19>>(item8, item9,
                item10, item11, item12,
                item13, item14, Tuple.Create(item15,
                item16, item17, item18,
                item19)));
        }

        /// <summary>
        /// Creates a new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> vigenuple
        /// with the first through twentieth components provided.
        /// </summary>
        /// <typeparam name="T1">The type of the vigenuple's primary component.</typeparam>
        /// <typeparam name="T2">The type of the vigenuple's secondary component.</typeparam>
        /// <typeparam name="T3">The type of the vigenuple's tertiary component.</typeparam>
        /// <typeparam name="T4">The type of the vigenuple's quaternary component.</typeparam>
        /// <typeparam name="T5">The type of the vigenuple's quinary component.</typeparam>
        /// <typeparam name="T6">The type of the vigenuple's senary component.</typeparam>
        /// <typeparam name="T7">The type of the vigenuple's septenary component.</typeparam>
        /// <typeparam name="T8">The type of the vigenuple's octonary component.</typeparam>
        /// <typeparam name="T9">The type of the vigenuple's nonary component.</typeparam>
        /// <typeparam name="T10">The type of the vigenuple's denary component.</typeparam>
        /// <typeparam name="T11">The type of the vigenuple's undenary component.</typeparam>
        /// <typeparam name="T12">The type of the vigenuple's duodenary component.</typeparam>
        /// <typeparam name="T13">The type of the vigenuple's tredenary component.</typeparam>
        /// <typeparam name="T14">The type of the vigenuple's quattuordenary component.</typeparam>
        /// <typeparam name="T15">The type of the vigenuple's quindenary component.</typeparam>
        /// <typeparam name="T16">The type of the vigenuple's sedenary component.</typeparam>
        /// <typeparam name="T17">The type of the vigenuple's septendenary component.</typeparam>
        /// <typeparam name="T18">The type of the vigenuple's octodenary component.</typeparam>
        /// <typeparam name="T19">The type of the vigenuple's novemdenary component.</typeparam>
        /// <typeparam name="T20">The type of the vigenuple's vigenary component.</typeparam>
        /// <param name="item1">The value of the vigenuple's primary component.</param>
        /// <param name="item2">The value of the vigenuple's secondary component.</param>
        /// <param name="item3">The value of the vigenuple's tertiary component.</param>
        /// <param name="item4">The value of the vigenuple's quaternary component.</param>
        /// <param name="item5">The value of the vigenuple's quinary component.</param>
        /// <param name="item6">The value of the vigenuple's senary component.</param>
        /// <param name="item7">The value of the vigenuple's septenary component.</param>
        /// <param name="item8">The value of the vigenuple's octonary component.</param>
        /// <param name="item9">The value of the vigenuple's nonary component.</param>
        /// <param name="item10">The value of the vigenuple's denary component.</param>
        /// <param name="item11">The value of the vigenuple's undenary component.</param>
        /// <param name="item12">The value of the vigenuple's duodenary component.</param>
        /// <param name="item13">The value of the vigenuple's tredenary component.</param>
        /// <param name="item14">The value of the vigenuple's quattuordenary component.</param>
        /// <param name="item15">The value of the vigenuple's quindenary component.</param>
        /// <param name="item16">The value of the vigenuple's sedenary component.</param>
        /// <param name="item17">The value of the vigenuple's septendenary component.</param>
        /// <param name="item18">The value of the vigenuple's octodenary component.</param>
        /// <param name="item19">The value of the vigenuple's novemdenary component.</param>
        /// <param name="item20">The value of the vigenuple's vigenary component.</param>
        /// <returns>A new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> vigenuple
        /// with the first through twentieth components provided.</returns>
        [StaticContainerExtensionAttribute(typeof(Tuple))]
        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20>>> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, T17 item17, T18 item18, T19 item19, T20 item20)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20>>>(item1, item2, item3,
                item4, item5, item6,
                item7, new Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20>>(item8, item9,
                item10, item11, item12,
                item13, item14, Tuple.Create(item15,
                item16, item17, item18,
                item19, item20)));
        }


        /// <summary>
        /// Creates a new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> unvigenuple
        /// with the first through twenty-first components provided.
        /// </summary>
        /// <typeparam name="T1">The type of the unvigenuple's primary component.</typeparam>
        /// <typeparam name="T2">The type of the unvigenuple's secondary component.</typeparam>
        /// <typeparam name="T3">The type of the unvigenuple's tertiary component.</typeparam>
        /// <typeparam name="T4">The type of the unvigenuple's quaternary component.</typeparam>
        /// <typeparam name="T5">The type of the unvigenuple's quinary component.</typeparam>
        /// <typeparam name="T6">The type of the unvigenuple's senary component.</typeparam>
        /// <typeparam name="T7">The type of the unvigenuple's septenary component.</typeparam>
        /// <typeparam name="T8">The type of the unvigenuple's octonary component.</typeparam>
        /// <typeparam name="T9">The type of the unvigenuple's nonary component.</typeparam>
        /// <typeparam name="T10">The type of the unvigenuple's denary component.</typeparam>
        /// <typeparam name="T11">The type of the unvigenuple's undenary component.</typeparam>
        /// <typeparam name="T12">The type of the unvigenuple's duodenary component.</typeparam>
        /// <typeparam name="T13">The type of the unvigenuple's tredenary component.</typeparam>
        /// <typeparam name="T14">The type of the unvigenuple's quattuordenary component.</typeparam>
        /// <typeparam name="T15">The type of the unvigenuple's quindenary component.</typeparam>
        /// <typeparam name="T16">The type of the unvigenuple's sedenary component.</typeparam>
        /// <typeparam name="T17">The type of the unvigenuple's septendenary component.</typeparam>
        /// <typeparam name="T18">The type of the unvigenuple's octodenary component.</typeparam>
        /// <typeparam name="T19">The type of the unvigenuple's novemdenary component.</typeparam>
        /// <typeparam name="T20">The type of the unvigenuple's vigenary component.</typeparam>
        /// <typeparam name="T21">The type of the unvigenuple's unvigenary component.</typeparam>
        /// <param name="item1">The value of the unvigenuple's primary component.</param>
        /// <param name="item2">The value of the unvigenuple's secondary component.</param>
        /// <param name="item3">The value of the unvigenuple's tertiary component.</param>
        /// <param name="item4">The value of the unvigenuple's quaternary component.</param>
        /// <param name="item5">The value of the unvigenuple's quinary component.</param>
        /// <param name="item6">The value of the unvigenuple's senary component.</param>
        /// <param name="item7">The value of the unvigenuple's septenary component.</param>
        /// <param name="item8">The value of the unvigenuple's octonary component.</param>
        /// <param name="item9">The value of the unvigenuple's nonary component.</param>
        /// <param name="item10">The value of the unvigenuple's denary component.</param>
        /// <param name="item11">The value of the unvigenuple's undenary component.</param>
        /// <param name="item12">The value of the unvigenuple's duodenary component.</param>
        /// <param name="item13">The value of the unvigenuple's tredenary component.</param>
        /// <param name="item14">The value of the unvigenuple's quattuordenary component.</param>
        /// <param name="item15">The value of the unvigenuple's quindenary component.</param>
        /// <param name="item16">The value of the unvigenuple's sedenary component.</param>
        /// <param name="item17">The value of the unvigenuple's septendenary component.</param>
        /// <param name="item18">The value of the unvigenuple's octodenary component.</param>
        /// <param name="item19">The value of the unvigenuple's novemdenary component.</param>
        /// <param name="item20">The value of the unvigenuple's vigenary component.</param>
        /// <param name="item21">The value of the unvigenuple's unvigenary component.</param>
        /// <returns>A new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> unvigenuple
        /// with the first through twenty-first components provided.</returns>
        [StaticContainerExtensionAttribute(typeof(Tuple))]
        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20, T21>>> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, T17 item17, T18 item18, T19 item19, T20 item20, T21 item21)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20, T21>>>(item1, item2, item3,
                item4, item5, item6,
                item7, new Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20, T21>>(item8, item9,
                item10, item11, item12,
                item13, item14, Tuple.Create(item15,
                item16, item17, item18,
                item19, item20, item21)));
        }
        #endregion // TupleHelper methods
        public static void Split<T1>(this Tuple<T1> target, out T1 item1)
        {
            item1 = target.Item1;
        }
        public static void Split<T1, T2>(this Tuple<T1, T2> target, out T1 item1, out T2 item2)
        {
            item1 = target.Item1;
            item2 = target.Item2;
        }
        public static void Split<T1, T2, T3>(this Tuple<T1, T2, T3> target, out T1 item1, out T2 item2, out  T3 item3)
        {
            item1 = target.Item1;
            item2 = target.Item2;
            item3 = target.Item3;
        }

        public static void Split<T1, T2, T3, T4>(this Tuple<T1, T2, T3, T4> target, out T1 item1, out T2 item2, out  T3 item3, out T4 item4)
        {
            item1 = target.Item1;
            item2 = target.Item2;
            item3 = target.Item3;
            item4 = target.Item4;
        }

    }

    // Module: RootModule
}
