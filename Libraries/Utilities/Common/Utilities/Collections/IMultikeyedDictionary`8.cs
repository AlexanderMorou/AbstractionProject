 /* -----------------------------------------------------------\
 |  This code was generated by Allen Copeland's Abstraction.   |
 |  Version: 1.0.0.11553                                       |
 |-------------------------------------------------------------|
 |  To ensure the code works properly,                         |
 |  please do not make any changes to the file.                |
 |-------------------------------------------------------------|
 |  The specific language is C# (Runtime version: v4.0.30319)  |
 |  Sub-tool Name: Abstraction's Old C♯ Code Translator        |
 |  Sub-tool Version: 1.0.0.11553                              |
 \----------------------------------------------------------- */
using System;
using System.Collections.Generic;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Utilities.Collections
{
#if MKD_SEVEN
    // Module: RootModule
    /// <summary>
    /// Defines properties and methods for working with a multiple keyed dictionary with seven keys.
    /// </summary>
    /// <typeparam name="TKey1">The type of the first key of the multiple key dictionary.</typeparam>
    /// <typeparam name="TKey2">The type of the second key of the multiple key dictionary.</typeparam>
    /// <typeparam name="TKey3">The type of the third key of the multiple key dictionary.</typeparam>
    /// <typeparam name="TKey4">The type of the fourth key of the multiple key dictionary.</typeparam>
    /// <typeparam name="TKey5">The type of the fifth key of the multiple key dictionary.</typeparam>
    /// <typeparam name="TKey6">The type of the sixth key of the multiple key dictionary.</typeparam>
    /// <typeparam name="TKey7">The type of the seventh key of the multiple key dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the value in the multiple key dictionary.</typeparam>
    public interface IMultikeyedDictionary<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue> :
        IEnumerable<KeysValuePair<MultikeyedDictionaryKeys<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7>, TValue>>
    {
        #region IMultikeyedDictionary properties
        /// <summary>
        /// Returns the number of elements within the <see cref="IMultikeyedDictionary{TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue}"/>.
        /// </summary>
        int Count{ get; }
        
        /// <summary>
        /// Returns/sets the value with the keys <paramref name="key1"/>, <paramref name="key2"/>, <paramref name="key3"/>, <paramref name="key4"/>, <paramref name="key5"/>, <paramref name="key6"/>, <paramref name="key7"/>.
        /// </summary>
        /// <param name="key1">The <typeparamref name="TKey1"/> which
        /// is the first key of the element to set/retrieve.</param>
        /// <param name="key2">The <typeparamref name="TKey2"/> which
        /// is the second key of the element to set/retrieve.</param>
        /// <param name="key3">The <typeparamref name="TKey3"/> which
        /// is the third key of the element to set/retrieve.</param>
        /// <param name="key4">The <typeparamref name="TKey4"/> which
        /// is the fourth key of the element to set/retrieve.</param>
        /// <param name="key5">The <typeparamref name="TKey5"/> which
        /// is the fifth key of the element to set/retrieve.</param>
        /// <param name="key6">The <typeparamref name="TKey6"/> which
        /// is the sixth key of the element to set/retrieve.</param>
        /// <param name="key7">The <typeparamref name="TKey7"/> which
        /// is the seventh key of the element to set/retrieve.</param>
        TValue this[TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7]
        { get; set; }
        
        /// <summary>
        /// Obtains the <see cref="KeysValuePair{TKeys, TValue}"/> at
        /// the <paramref name="index"/> provided.
        /// </summary>
        /// <param name="index">The <see cref="Int32"/> value which denotes
        /// the zero-based index of the <see cref="KeysValuePair{TKeys, TValue}"/>
        /// to retrieve.</param>
        KeysValuePair<MultikeyedDictionaryKeys<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7>, TValue> this[int index]
        { get; }
        #endregion // IMultikeyedDictionary properties
        #region IMultikeyedDictionary methods
        /// <summary>
        /// Adds the <paramref name="value"/> with the keys
        /// <paramref name="key1"/>, <paramref name="key2"/>, <paramref name="key3"/>, <paramref name="key4"/>, <paramref name="key5"/>, <paramref name="key6"/>, <paramref name="key7"/>.
        /// </summary>
        /// <param name="key1">The <typeparamref name="TKey1"/> which
        /// is the first key of the <paramref name="value"/> to add.</param>
        /// <param name="key2">The <typeparamref name="TKey2"/> which
        /// is the second key of the <paramref name="value"/> to add.</param>
        /// <param name="key3">The <typeparamref name="TKey3"/> which
        /// is the third key of the <paramref name="value"/> to add.</param>
        /// <param name="key4">The <typeparamref name="TKey4"/> which
        /// is the fourth key of the <paramref name="value"/> to add.</param>
        /// <param name="key5">The <typeparamref name="TKey5"/> which
        /// is the fifth key of the <paramref name="value"/> to add.</param>
        /// <param name="key6">The <typeparamref name="TKey6"/> which
        /// is the sixth key of the <paramref name="value"/> to add.</param>
        /// <param name="key7">The <typeparamref name="TKey7"/> which
        /// is the seventh key of the <paramref name="value"/> to add.</param>
        /// <param name="value">The <typeparamref name="value"/> to add to the <see cref="IMultikeyedDictionary{TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue}"/>.</param>
        void Add(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TValue value);
        
        /// <summary>
        /// Removes the value with the keys <paramref name="key1"/>, <paramref name="key2"/>, <paramref name="key3"/>, <paramref name="key4"/>, <paramref name="key5"/>, <paramref name="key6"/>, <paramref name="key7"/>.
        /// </summary>
        /// <param name="key1">The <typeparamref name="TKey1"/> which
        /// is the first key of the element to remove.</param>
        /// <param name="key2">The <typeparamref name="TKey2"/> which
        /// is the second key of the element to remove.</param>
        /// <param name="key3">The <typeparamref name="TKey3"/> which
        /// is the third key of the element to remove.</param>
        /// <param name="key4">The <typeparamref name="TKey4"/> which
        /// is the fourth key of the element to remove.</param>
        /// <param name="key5">The <typeparamref name="TKey5"/> which
        /// is the fifth key of the element to remove.</param>
        /// <param name="key6">The <typeparamref name="TKey6"/> which
        /// is the sixth key of the element to remove.</param>
        /// <param name="key7">The <typeparamref name="TKey7"/> which
        /// is the seventh key of the element to remove.</param>
        bool Remove(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7);
        
        /// <summary>
        /// Attempts to add the <paramref name="value"/> with the keys
        /// <paramref name="key1"/>, <paramref name="key2"/>, <paramref name="key3"/>, <paramref name="key4"/>, <paramref name="key5"/>, <paramref name="key6"/>, <paramref name="key7"/>.
        /// </summary>
        /// <param name="key1">The <typeparamref name="TKey1"/> which
        /// is the first key of the <paramref name="value"/> to attempt to add.</param>
        /// <param name="key2">The <typeparamref name="TKey2"/> which
        /// is the second key of the <paramref name="value"/> to attempt to add.</param>
        /// <param name="key3">The <typeparamref name="TKey3"/> which
        /// is the third key of the <paramref name="value"/> to attempt to add.</param>
        /// <param name="key4">The <typeparamref name="TKey4"/> which
        /// is the fourth key of the <paramref name="value"/> to attempt to add.</param>
        /// <param name="key5">The <typeparamref name="TKey5"/> which
        /// is the fifth key of the <paramref name="value"/> to attempt to add.</param>
        /// <param name="key6">The <typeparamref name="TKey6"/> which
        /// is the sixth key of the <paramref name="value"/> to attempt to add.</param>
        /// <param name="key7">The <typeparamref name="TKey7"/> which
        /// is the seventh key of the <paramref name="value"/> to attempt to add.</param>
        /// <param name="value">The <typeparamref name="value"/> to attempt to add to the <see cref="IMultikeyedDictionary{TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue}"/>.</param>
        bool TryAdd(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TValue value);
        
        /// <summary>
        /// Attempts to retrieve the <paramref name="value"/> with the
        /// keys <paramref name="key1"/>, <paramref name="key2"/>, <paramref name="key3"/>, <paramref name="key4"/>, <paramref name="key5"/>, <paramref name="key6"/>, <paramref name="key7"/>.
        /// </summary>
        /// <param name="key1">The <typeparamref name="TKey1"/> which
        /// is the first key of the <paramref name="value"/> to attempt to retrieve.</param>
        /// <param name="key2">The <typeparamref name="TKey2"/> which
        /// is the second key of the <paramref name="value"/> to attempt to retrieve.</param>
        /// <param name="key3">The <typeparamref name="TKey3"/> which
        /// is the third key of the <paramref name="value"/> to attempt to retrieve.</param>
        /// <param name="key4">The <typeparamref name="TKey4"/> which
        /// is the fourth key of the <paramref name="value"/> to attempt to retrieve.</param>
        /// <param name="key5">The <typeparamref name="TKey5"/> which
        /// is the fifth key of the <paramref name="value"/> to attempt to retrieve.</param>
        /// <param name="key6">The <typeparamref name="TKey6"/> which
        /// is the sixth key of the <paramref name="value"/> to attempt to retrieve.</param>
        /// <param name="key7">The <typeparamref name="TKey7"/> which
        /// is the seventh key of the <paramref name="value"/> to attempt to retrieve.</param>
        /// <param name="value">The <typeparamref name="value"/> to attempt to
        /// retrieve from the <see cref="IMultikeyedDictionary{TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue}"/>.</param>
        bool TryGetValue(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, out TValue value);
        #endregion // IMultikeyedDictionary methods
    }
#endif
}
 /* -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------\
 |  This file took 00:00:00.0012439 to generate.                                                                                                                                                                                                                                             |
 |  Date generated: 11/24/2011 3:33:02 AM                                                                                                                                                                                                                                                    |
 |  There were 21 types used by this file                                                                                                                                                                                                                                                    |
 |  Key1, Key1, Key2, Key2, Key3, Key3, Key4, Key4, Key5, Key5, Key6, Key6, Key7, Key7, Value, Abstraction.Utilities.Collections.MultikeyedDictionaryKeys`7[[TKey1],[TKey2],[TKey3],[TKey4],[TKey5],[TKey6],[TKey7]]<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7>,TValue>,                     |
 |  MultikeyedDictionaryKeys`7[[TKey1],[TKey2],[TKey3],[TKey4],[TKey5],[TKey6],[TKey7]]<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7>, Boolean, Int32, Void,                                                                                                                                    |
 |  IEnumerable`1<AllenCopeland.Abstraction.Utilities.Collections.KeysValuePair`2[[TKeys],[TValue]]<AllenCopeland.Abstraction.Utilities.Collections.MultikeyedDictionaryKeys`7[[TKey1],[TKey2],[TKey3],[TKey4],[TKey5],[TKey6],[TKey7]]<TKey1,TKey2,TKey3,TKey4,TKey5,TKey6,TKey7>,TValue>>  |
 |-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
 |  There were 1 assemblies referenced:                                                                                                                                                                                                                                                      |
 |  mscorlib                                                                                                                                                                                                                                                                                 |
 \----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- */
