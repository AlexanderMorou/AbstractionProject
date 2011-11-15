using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Collections
{
#if REQUIRE_NGEN
    /// <summary>
    /// Defines properties and methods for working with a controlled-state multikeyed tree.
    /// </summary>
    /// <typeparam name="TKey1">The type of the first key of the first level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TKey2">The type of the second key of the second level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TKey3">The type of the third key of the third level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TKey4">The type of the fourthkey of the fourth level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TKey5">The type of the fifth key of the fifth level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TKey6">The type of the sixth key of the sixth level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TKey7">The type of the seventh key of the last level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TValue">The type of the value held on the last
    /// level of the controlled-state multikeyed tree.</typeparam>
    public interface IControlledStateMultikeyedTree<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TTop, TLevel6, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue> :
        IControlledStateDictionary<TKey1, TLevel6>,
        IControlledStateMultikeyedTreeTopLevel
        where TTop :
            IControlledStateMultikeyedTree<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TTop, TLevel6, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue>
        where TLevel6 :
            IControlledStateMultikeyedTreeLevel<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TLevel6, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TTop, TTop>
        where TLevel5 :
            IControlledStateMultikeyedTreeLevel<TKey3, TKey4, TKey5, TKey6, TKey7, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TLevel6, TTop>
        where TLevel4 :
            IControlledStateMultikeyedTreeLevel<TKey4, TKey5, TKey6, TKey7, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TLevel5, TTop>
        where TLevel3 :
            IControlledStateMultikeyedTreeLevel<TKey5, TKey6, TKey7, TLevel3, TLevel2, TLevel1, TValue, TLevel4, TTop>
        where TLevel2 :
            IControlledStateMultikeyedTreeLevel<TKey6, TKey7, TLevel2, TLevel1, TValue, TLevel3, TTop>
        where TLevel1 :
            IControlledStateMultikeyedTreeLevel<TKey7, TValue, TLevel2, TTop>
    {
        /// <summary>
        /// Determines whether the keys provided 
        /// exist within the <see cref="IControlledStateMultikeyedTree{TKey1, TKey2, TValue}"/>.
        /// </summary>
        /// <param name="key1">The <typeparamref name="TKey1"/> value which denotes the first key 
        /// to check for.</param>
        /// <param name="key2">The <typeparamref name="TKey2"/> value which denotes the second key 
        /// to check for.</param>
        /// <param name="key3">The <typeparamref name="TKey3"/> value which denotes the third key 
        /// to check for.</param>
        /// <param name="key4">The <typeparamref name="TKey4"/> value which denotes the fourth key 
        /// to check for.</param>
        /// <param name="key5">The <typeparamref name="TKey5"/> value which denotes the fifth key 
        /// to check for.</param>
        /// <param name="key6">The <typeparamref name="TKey6"/> value which denotes the sixth key 
        /// to check for.</param>
        /// <param name="key7">The <typeparamref name="TKey7"/> value which denotes the last key 
        /// to check for.</param>
        /// <returns>A <see cref="Int32"/> value denoting
        /// the number of keys that were contained ranging from 0 to 7.</returns>
        int ContainsKeys(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7);
        /// <summary>
        /// Obtains the <typeparamref name="TValue"/> at the
        /// <paramref name="key1"/>, <paramref name="key2"/>, <paramref name="key3"/>
        /// <paramref name="key4"/>,<paramref name="key5"/>,
        /// <paramref name="key6"/> and <paramref name="key7"/>provided.
        /// </summary>
        /// <param name="key1">The <typeparamref name="TKey1"/> value which denotes the first key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <param name="key2">The <typeparamref name="TKey2"/> value which denotes the second key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <param name="key3">The <typeparamref name="TKey3"/> value which denotes the third key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <param name="key4">The <typeparamref name="TKey4"/> value which denotes the fourth key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <param name="key5">The <typeparamref name="TKey5"/> value which denotes the fifth key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <param name="key6">The <typeparamref name="TKey6"/> value which denotes the sixth key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <param name="key7">The <typeparamref name="TKey7"/> value which denotes the last key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <returns>The <typeparamref name="TValue"/> that resides at the location
        /// denoted by <paramref name="key1"/> and <paramref name="key2"/>.</returns>
        TValue this[TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7] { get; }
        /// <summary>
        /// Attempts to get the <typeparamref name="TValue"/> at the
        /// <paramref name="key1"/>, <paramref name="key2"/>, <paramref name="key3"/>
        /// <paramref name="key4"/>,<paramref name="key5"/>,
        /// <paramref name="key6"/> and <paramref name="key7"/>provided.
        /// </summary>
        /// <param name="key1">The <typeparamref name="TKey1"/> value which denotes the first key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="key2">The <typeparamref name="TKey2"/> value which denotes the second key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="key3">The <typeparamref name="TKey3"/> value which denotes the third key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="key4">The <typeparamref name="TKey4"/> value which denotes the fourth key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="key5">The <typeparamref name="TKey5"/> value which denotes the fifth key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="key6">The <typeparamref name="TKey6"/> value which denotes the sixth key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="key7">The <typeparamref name="TKey7"/> value which denotes the last key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="value">The <typeparamref name="TValue"/> instance which
        /// will receive the value if it is present in the 
        /// <see cref="IControlledStateMultikeyedTree{TKey1, TKey2, TValue}"/>.</param>
        /// <returns>true if <paramref name="value"/> contains the 
        /// element at the location denoted by <paramref name="key1"/> and 
        /// <paramref name="key2"/>; false, otherwise.</returns>
        bool TryGetValue(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, out TValue value);
    }
    /// <summary>
    /// Defines properties and methods for working with a controlled-state multikeyed tree.
    /// </summary>
    /// <typeparam name="TKey1">The type of the first key of the first level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TKey2">The type of the second key of the second level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TKey3">The type of the third key of the third level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TKey4">The type of the fourthkey of the fourth level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TKey5">The type of the fifth key of the fifth level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TKey6">The type of the sixth key of the last level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TValue">The type of the value held on the last
    /// level of the controlled-state multikeyed tree.</typeparam>
    public interface IControlledStateMultikeyedTree<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TTop, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue> :
        IControlledStateDictionary<TKey1, TLevel5>,
        IControlledStateMultikeyedTreeTopLevel
        where TTop :
            IControlledStateMultikeyedTree<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TTop, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue>
        where TLevel5 :
            IControlledStateMultikeyedTreeLevel<TKey2, TKey3, TKey4, TKey5, TKey6, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TTop, TTop>
        where TLevel4 :
            IControlledStateMultikeyedTreeLevel<TKey3, TKey4, TKey5, TKey6, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TLevel5, TTop>
        where TLevel3 :
            IControlledStateMultikeyedTreeLevel<TKey4, TKey5, TKey6, TLevel3, TLevel2, TLevel1, TValue, TLevel4, TTop>
        where TLevel2 :
            IControlledStateMultikeyedTreeLevel<TKey5, TKey6, TLevel2, TLevel1, TValue, TLevel3, TTop>
        where TLevel1 :
            IControlledStateMultikeyedTreeLevel<TKey6, TValue, TLevel2, TTop>
    {
        /// <summary>
        /// Determines whether the keys provided 
        /// exist within the <see cref="IControlledStateMultikeyedTree{TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue}"/>.
        /// </summary>
        /// <param name="key1">The <typeparamref name="TKey1"/> value which denotes the first key 
        /// to check for.</param>
        /// <param name="key2">The <typeparamref name="TKey2"/> value which denotes the second key 
        /// to check for.</param>
        /// <param name="key3">The <typeparamref name="TKey3"/> value which denotes the third key 
        /// to check for.</param>
        /// <param name="key4">The <typeparamref name="TKey4"/> value which denotes the fourth key 
        /// to check for.</param>
        /// <param name="key5">The <typeparamref name="TKey5"/> value which denotes the fifth key 
        /// to check for.</param>
        /// <param name="key6">The <typeparamref name="TKey6"/> value which denotes the last key 
        /// to check for.</param>
        /// <returns>A <see cref="Int32"/> value denoting
        /// the number of keys that were contained ranging from 0 to 6.</returns>
        int ContainsKeys(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6);
        /// <summary>
        /// Obtains the <typeparamref name="TValue"/> at the
        /// <paramref name="key1"/>, <paramref name="key2"/>, <paramref name="key3"/>
        /// <paramref name="key4"/>,<paramref name="key5"/> and
        /// <paramref name="key6"/> provided.
        /// </summary>
        /// <param name="key1">The <typeparamref name="TKey1"/> value which denotes the first key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <param name="key2">The <typeparamref name="TKey2"/> value which denotes the second key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <param name="key3">The <typeparamref name="TKey3"/> value which denotes the third key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <param name="key4">The <typeparamref name="TKey4"/> value which denotes the fourth key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <param name="key5">The <typeparamref name="TKey5"/> value which denotes the fifth key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <param name="key6">The <typeparamref name="TKey6"/> value which denotes the last key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <returns>The <typeparamref name="TValue"/> that resides at the location
        /// denoted by <paramref name="key1"/> and <paramref name="key2"/>.</returns>
        TValue this[TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6] { get; }
        /// <summary>
        /// Attempts to get the <typeparamref name="TValue"/> at the
        /// <paramref name="key1"/>, <paramref name="key2"/>, <paramref name="key3"/>
        /// <paramref name="key4"/>,<paramref name="key5"/> and
        /// <paramref name="key6"/> provided.
        /// </summary>
        /// <param name="key1">The <typeparamref name="TKey1"/> value which denotes the first key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="key2">The <typeparamref name="TKey2"/> value which denotes the second key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="key3">The <typeparamref name="TKey3"/> value which denotes the third key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="key4">The <typeparamref name="TKey4"/> value which denotes the fourth key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="key5">The <typeparamref name="TKey5"/> value which denotes the fifth key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="key6">The <typeparamref name="TKey6"/> value which denotes the last key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="value">The <typeparamref name="TValue"/> instance which
        /// will receive the value if it is present in the 
        /// <see cref="IControlledStateMultikeyedTree{TKey1, TKey2, TValue}"/>.</param>
        /// <returns>true if <paramref name="value"/> contains the 
        /// element at the location denoted by <paramref name="key1"/> and 
        /// <paramref name="key2"/>; false, otherwise.</returns>
        bool TryGetValue(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, out TValue value);
    }
    /// <summary>
    /// Defines properties and methods for working with a controlled-state multikeyed tree.
    /// </summary>
    /// <typeparam name="TKey1">The type of the first key of the first level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TKey2">The type of the second key of the second level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TKey3">The type of the third key of the third level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TKey4">The type of the fourthkey of the fourth level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TKey5">The type of the fifth key of the fifth level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TValue">The type of the value held on the last
    /// level of the controlled-state multikeyed tree.</typeparam>
    public interface IControlledStateMultikeyedTree<TKey1, TKey2, TKey3, TKey4, TKey5, TTop, TLevel4, TLevel3, TLevel2, TLevel1, TValue> :
        IControlledStateDictionary<TKey1, TLevel4>,
        IControlledStateMultikeyedTreeTopLevel
        where TTop :
            IControlledStateMultikeyedTree<TKey1, TKey2, TKey3, TKey4, TKey5, TTop, TLevel4, TLevel3, TLevel2, TLevel1, TValue>
        where TLevel4 :
            IControlledStateMultikeyedTreeLevel<TKey2, TKey3, TKey4, TKey5, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TTop, TTop>
        where TLevel3 :
            IControlledStateMultikeyedTreeLevel<TKey3, TKey4, TKey5, TLevel3, TLevel2, TLevel1, TValue, TLevel4, TTop>
        where TLevel2 :
            IControlledStateMultikeyedTreeLevel<TKey4, TKey5, TLevel2, TLevel1, TValue, TLevel3, TTop>
        where TLevel1 :
            IControlledStateMultikeyedTreeLevel<TKey5, TValue, TLevel2, TTop>
    {
        /// <summary>
        /// Determines whether the keys provided 
        /// exist within the <see cref="IControlledStateMultikeyedTree{TKey1, TKey2, TKey3, TKey4, TKey5, TValue}"/>.
        /// </summary>
        /// <param name="key1">The <typeparamref name="TKey1"/> value which denotes the first key 
        /// to check for.</param>
        /// <param name="key2">The <typeparamref name="TKey2"/> value which denotes the second key 
        /// to check for.</param>
        /// <param name="key3">The <typeparamref name="TKey3"/> value which denotes the third key 
        /// to check for.</param>
        /// <param name="key4">The <typeparamref name="TKey4"/> value which denotes the fourth key 
        /// to check for.</param>
        /// <param name="key5">The <typeparamref name="TKey5"/> value which denotes the first key 
        /// to check for.</param>
        /// <returns>A <see cref="Int32"/> value denoting
        /// the number of keys that were contained ranging from 0 to 5.</returns>
        int ContainsKeys(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5);
        /// <summary>
        /// Obtains the <typeparamref name="TValue"/> at the
        /// <paramref name="key1"/>, <paramref name="key2"/>, <paramref name="key3"/>
        /// <paramref name="key4"/> and <paramref name="key5"/> provided.
        /// </summary>
        /// <param name="key1">The <typeparamref name="TKey1"/> value which denotes the first key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <param name="key2">The <typeparamref name="TKey2"/> value which denotes the second key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <param name="key3">The <typeparamref name="TKey3"/> value which denotes the third key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <param name="key4">The <typeparamref name="TKey4"/> value which denotes the fourth key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <param name="key5">The <typeparamref name="TKey5"/> value which denotes the last key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <returns>The <typeparamref name="TValue"/> that resides at the location
        /// denoted by <paramref name="key1"/> and <paramref name="key2"/>.</returns>
        TValue this[TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5] { get; }
        /// <summary>
        /// Attempts to get the <typeparamref name="TValue"/> at the
        /// <paramref name="key1"/>, <paramref name="key2"/>, <paramref name="key3"/>
        /// <paramref name="key4"/> and <paramref name="key5"/> provided.
        /// </summary>
        /// <param name="key1">The <typeparamref name="TKey1"/> value which denotes the first key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="key2">The <typeparamref name="TKey2"/> value which denotes the second key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="key3">The <typeparamref name="TKey3"/> value which denotes the third key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="key4">The <typeparamref name="TKey4"/> value which denotes the fourth key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="key5">The <typeparamref name="TKey5"/> value which denotes the first key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="value">The <typeparamref name="TValue"/> instance which
        /// will receive the value if it is present in the 
        /// <see cref="IControlledStateMultikeyedTree{TKey1, TKey2, TKey3, TKey4, TKey5, TValue}"/>.</param>
        /// <returns>true if <paramref name="value"/> contains the 
        /// element at the location denoted by <paramref name="key1"/> and 
        /// <paramref name="key2"/>; false, otherwise.</returns>
        bool TryGetValue(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, out TValue value);
    }
    /// <summary>
    /// Defines properties and methods for working with a controlled-state multikeyed tree.
    /// </summary>
    /// <typeparam name="TKey1">The type of the first key of the first level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TKey2">The type of the second key of the second level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TKey3">The type of the third key of the third level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TKey4">The type of the fourthkey of the fourth level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TValue">The type of the value held on the last
    /// level of the controlled-state multikeyed tree.</typeparam>
    public interface IControlledStateMultikeyedTree<TKey1, TKey2, TKey3, TKey4, TTop, TLevel3, TLevel2, TLevel1, TValue> :
        IControlledStateDictionary<TKey1, TLevel3>,
        IControlledStateMultikeyedTreeTopLevel
        where TTop :
            IControlledStateMultikeyedTree<TKey1, TKey2, TKey3, TKey4, TTop, TLevel3, TLevel2, TLevel1, TValue>
        where TLevel3 :
            IControlledStateMultikeyedTreeLevel<TKey2, TKey3, TKey4, TLevel3, TLevel2, TLevel1, TValue, TTop, TTop>
        where TLevel2 :
            IControlledStateMultikeyedTreeLevel<TKey3, TKey4, TLevel2, TLevel1, TValue, TLevel3, TTop>
        where TLevel1 :
            IControlledStateMultikeyedTreeLevel<TKey4, TValue, TLevel2, TTop>
    {
        /// <summary>
        /// Determines whether the keys provided 
        /// exist within the <see cref="IControlledStateMultikeyedTree{TKey1, TKey2, TKey3, TKey4, TValue}"/>.
        /// </summary>
        /// <param name="key1">The <typeparamref name="TKey1"/> value which denotes the first key 
        /// to check for.</param>
        /// <param name="key2">The <typeparamref name="TKey2"/> value which denotes the second key 
        /// to check for.</param>
        /// <param name="key3">The <typeparamref name="TKey3"/> value which denotes the third key 
        /// to check for.</param>
        /// <param name="key4">The <typeparamref name="TKey4"/> value which denotes the last key 
        /// to check for.</param>
        /// <returns>A <see cref="Int32"/> value denoting
        /// the number of keys that were contained ranging from 0 to 4.</returns>
        int ContainsKeys(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4);
        /// <summary>
        /// Obtains the <typeparamref name="TValue"/> at the
        /// <paramref name="key1"/>, <paramref name="key2"/>,
        /// <paramref name="key3"/> and <paramref name="key4"/>
        /// provided.
        /// </summary>
        /// <param name="key1">The <typeparamref name="TKey1"/> value which denotes the first key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <param name="key2">The <typeparamref name="TKey2"/> value which denotes the second key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <param name="key3">The <typeparamref name="TKey3"/> value which denotes the third key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <param name="key4">The <typeparamref name="TKey4"/> value which denotes the last key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <returns>The <typeparamref name="TValue"/> that resides at the location
        /// denoted by <paramref name="key1"/>, <paramref name="key2"/>,
        /// <paramref name="key3"/> and <paramref name="key4"/>.</returns>
        TValue this[TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4] { get; }
        /// <summary>
        /// Attempts to get the <typeparamref name="TValue"/> at the
        /// <paramref name="key1"/>, <paramref name="key2"/>,
        /// <paramref name="key3"/> and <paramref name="key4"/>
        /// provided.
        /// </summary>
        /// <param name="key1">The <typeparamref name="TKey1"/> value which denotes the first key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="key2">The <typeparamref name="TKey2"/> value which denotes the second key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="key3">The <typeparamref name="TKey3"/> value which denotes the third key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="key4">The <typeparamref name="TKey4"/> value which denotes the last key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="value">The <typeparamref name="TValue"/> instance which
        /// will receive the value if it is present in the 
        /// <see cref="IControlledStateMultikeyedTree{TKey1, TKey2, TKey3, TKey4, TValue}"/>.</param>
        /// <returns>true if <paramref name="value"/> contains the 
        /// element at the location denoted by <paramref name="key1"/>,
        /// <paramref name="key2"/>, <paramref name="key3"/> and
        /// <paramref name="key4"/>; false, otherwise.</returns>
        bool TryGetValue(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, out TValue value);
    }
#endif
    /// <summary>
    /// Defines properties and methods for working with a controlled-state multikeyed tree.
    /// </summary>
    /// <typeparam name="TKey1">The type of the first key of the first level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TKey2">The type of the second key of the second level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TKey3">The type of the third key of the third level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TValue">The type of the value held on the last
    /// level of the controlled-state multikeyed tree.</typeparam>
    public interface IControlledStateMultikeyedTree<TKey1, TKey2, TKey3, TTop, TLevel2, TLevel1, TValue> :
        IControlledStateDictionary<TKey1, TLevel2>,
        IControlledStateMultikeyedTreeTopLevel
        where TTop :
            IControlledStateMultikeyedTree<TKey1, TKey2, TKey3, TTop, TLevel2, TLevel1, TValue>
        where TLevel2 :
            IControlledStateMultikeyedTreeLevel<TKey2, TKey3, TLevel2, TLevel1, TValue, TTop, TTop>
        where TLevel1 :
            IControlledStateMultikeyedTreeLevel<TKey3, TValue, TLevel2, TTop>
    {
        /// <summary>
        /// Determines whether the keys provided 
        /// exist within the <see cref="IControlledStateMultikeyedTree{TKey1, TKey2, TKey3, TValue}"/>.
        /// </summary>
        /// <param name="key1">The <typeparamref name="TKey1"/> value which denotes the first key 
        /// to check for.</param>
        /// <param name="key2">The <typeparamref name="TKey2"/> value which denotes the second key 
        /// to check for.</param>
        /// <param name="key3">The <typeparamref name="TKey3"/> value which denotes the last key 
        /// to check for.</param>
        /// <returns>A <see cref="Int32"/> value denoting
        /// the number of keys that were contained ranging from 0 to 3.</returns>
        int ContainsKeys(TKey1 key1, TKey2 key2, TKey3 key3);
        /// <summary>
        /// Obtains the <typeparamref name="TValue"/> at the
        /// <paramref name="key1"/>, <paramref name="key2"/>
        /// and <paramref name="key3"/> provided.
        /// </summary>
        /// <param name="key1">The <typeparamref name="TKey1"/> value which denotes the first key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <param name="key2">The <typeparamref name="TKey2"/> value which denotes the second key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <param name="key3">The <typeparamref name="TKey3"/> value which denotes the last key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <returns>The <typeparamref name="TValue"/> that resides at the location
        /// denoted by <paramref name="key1"/> and <paramref name="key2"/>.</returns>
        TValue this[TKey1 key1, TKey2 key2, TKey3 key3] { get; }
        /// <summary>
        /// Attempts to get the <typeparamref name="TValue"/> at the
        /// <paramref name="key1"/>, <paramref name="key2"/>
        /// and <paramref name="key3"/> provided.
        /// </summary>
        /// <param name="key1">The <typeparamref name="TKey1"/> value which denotes the first key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="key2">The <typeparamref name="TKey2"/> value which denotes the second key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="key3">The <typeparamref name="TKey3"/> value which denotes the last key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="value">The <typeparamref name="TValue"/> instance which
        /// will receive the value if it is present in the 
        /// <see cref="IControlledStateMultikeyedTree{TKey1, TKey2, TKey3, TValue}"/>.</param>
        /// <returns>true if <paramref name="value"/> contains the 
        /// element at the location denoted by <paramref name="key1"/>, <paramref name="key2"/> 
        /// <paramref name="key3"/>; false, otherwise.</returns>
        bool TryGetValue(TKey1 key1, TKey2 key2, TKey3 key3, out TValue value);
    }
    /// <summary>
    /// Defines properties and methods for working with a controlled-state multikeyed tree.
    /// </summary>
    /// <typeparam name="TKey1">The type of the first key of the first level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TKey2">The type of the second key of the last level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TValue">The type of the value held on the last
    /// level of the controlled-state multikeyed tree.</typeparam>
    public interface IControlledStateMultikeyedTree<TKey1, TKey2, TTop, TLevel1, TValue> :
        IControlledStateDictionary<TKey1, TLevel1>,
        IControlledStateMultikeyedTreeTopLevel
        where TTop :
            IControlledStateMultikeyedTree<TKey1, TKey2, TTop, TLevel1, TValue>
        where TLevel1 :
            IControlledStateMultikeyedTreeLevel<TKey2, TValue, TTop, TTop>
    {
        /// <summary>
        /// Determines whether the keys provided 
        /// exist within the <see cref="IControlledStateMultikeyedTree{TKey1, TKey2, TValue}"/>.
        /// </summary>
        /// <param name="key1">The <typeparamref name="TKey1"/> value which denotes the first key 
        /// to check for.</param>
        /// <param name="key2">The <typeparamref name="TKey2"/> value which denotes the last key 
        /// to check for.</param>
        /// <returns>A <see cref="Int32"/> value denoting
        /// the number of keys that were contained ranging from 0 to 2.</returns>
        int ContainsKeys(TKey1 key1, TKey2 key2);
        /// <summary>
        /// Obtains the <typeparamref name="TValue"/> at the
        /// <paramref name="key1"/> and <paramref name="key2"/> provided.
        /// </summary>
        /// <param name="key1">The <typeparamref name="TKey1"/> value which denotes the first key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <param name="key2">The <typeparamref name="TKey2"/> value which denotes the last key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <returns>The <typeparamref name="TValue"/> that resides at the location
        /// denoted by <paramref name="key1"/> and <paramref name="key2"/>.</returns>
        TValue this[TKey1 key1, TKey2 key2] { get; }
        /// <summary>
        /// Attempts to get the <typeparamref name="TValue"/> at the
        /// <paramref name="key1"/> and <paramref name="key2"/> provided.
        /// </summary>
        /// <param name="key1">The <typeparamref name="TKey1"/> value which denotes the first key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="key2">The <typeparamref name="TKey2"/> value which denotes the last key 
        /// of the <typeparamref name="TValue"/> to attempt to retrieve.</param>
        /// <param name="value">The <typeparamref name="TValue"/> instance which
        /// will receive the value if it is present in the 
        /// <see cref="IControlledStateMultikeyedTree{TKey1, TKey2, TValue}"/>.</param>
        /// <returns>true if <paramref name="value"/> contains the 
        /// element at the location denoted by <paramref name="key1"/> and 
        /// <paramref name="key2"/>; false, otherwise.</returns>
        bool TryGetValue(TKey1 key1, TKey2 key2, out TValue value);
    }
    public interface IControlledStateMultikeyedTreeTopLevel :
        IControlledStateMultikeyedTreeLevel
    {
    }
}
