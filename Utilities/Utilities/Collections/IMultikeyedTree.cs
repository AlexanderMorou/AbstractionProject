using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Collections
{
#if REQUIRE_NGEN
    /// <summary>
    /// Defines properties and methods for working with a multikeyed tree.
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
    /// level of the multikeyed tree.</typeparam>
    public interface IMultikeyedTree<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TTop, TLevel6, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue> :
        IMultikeyedTreeTopLevel,
        IControlledStateMultikeyedTree<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TTop, TLevel6, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue>
        where TTop :
            IMultikeyedTree<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TTop, TLevel6, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue>
        where TLevel6 :
            IMultikeyedTreeLevel<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TLevel6, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TTop, TTop>
        where TLevel5 :
            IMultikeyedTreeLevel<TKey3, TKey4, TKey5, TKey6, TKey7, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TLevel6, TTop>
        where TLevel4 :
            IMultikeyedTreeLevel<TKey4, TKey5, TKey6, TKey7, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TLevel5, TTop>
        where TLevel3 :
            IMultikeyedTreeLevel<TKey5, TKey6, TKey7, TLevel3, TLevel2, TLevel1, TValue, TLevel4, TTop>
        where TLevel2 :
            IMultikeyedTreeLevel<TKey6, TKey7, TLevel2, TLevel1, TValue, TLevel3, TTop>
        where TLevel1 :
            IMultikeyedTreeLevel<TKey7, TValue, TLevel2, TTop>
    {
        /// <summary>
        /// Adds the <paramref name="value"/> at keys
        /// <paramref name="key1"/>, <paramref name="key2"/>,
        /// <paramref name="key3"/>, <paramref name="key4"/>,
        /// <paramref name="key5"/>, <paramref name="key6"/> and
        /// <paramref name="key7"/>.
        /// </summary>
        /// <param name="key1">The first key of the item to add.</param>
        /// <param name="key2">The second key of the item to add.</param>
        /// <param name="key3">The third key of the item to add.</param>
        /// <param name="key4">The fourth key of the item to add.</param>
        /// <param name="key5">The fifth key of the item to add.</param>
        /// <param name="key6">The sixth key of the item to add.</param>
        /// <param name="key7">The seventh key of the item to add.</param>
        /// <param name="value">The <typeparamref name="TValue"/> of the
        /// item to add.</param>
        void Add(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TValue value);
        /// <summary>
        /// Removes the item by the 
        /// <paramref name="key1"/>, <paramref name="key2"/>,
        /// <paramref name="key3"/>, <paramref name="key4"/>,
        /// <paramref name="key5"/>, <paramref name="key6"/> and
        /// <paramref name="key7"/>.
        /// </summary>
        /// <param name="key1">The first key of the item to remove.</param>
        /// <param name="key2">The second key of the item to remove.</param>
        /// <param name="key3">The third key of the item to remove.</param>
        /// <param name="key4">The fourth key of the item to remove.</param>
        /// <param name="key5">The fifth key of the item to remove.</param>
        /// <param name="key6">The sixth key of the item to remove.</param>
        /// <param name="key7">The seventh key of the item to remove.</param>
        /// <returns>true if the item was present and removed; false otherwise.</returns>
        bool Remove(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7);
        /// <summary>
        /// Clears the <see cref="IMultikeyedTree{TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue}"/>
        /// of all its elements.
        /// </summary>
        void Clear();
        /// <summary>
        /// Returns/sets the <typeparamref name="TValue"/> at the
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
        new TValue this[TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7] { get; set; }

    }
    /// <summary>
    /// Defines properties and methods for working with a multikeyed tree.
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
    /// level of the multikeyed tree.</typeparam>
    public interface IMultikeyedTree<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TTop, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue> :
        IMultikeyedTreeTopLevel,
        IControlledStateMultikeyedTree<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TTop, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue>
        where TTop :
            IMultikeyedTree<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TTop, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue>
        where TLevel5 :
            IMultikeyedTreeLevel<TKey2, TKey3, TKey4, TKey5, TKey6, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TTop, TTop>
        where TLevel4 :
            IMultikeyedTreeLevel<TKey3, TKey4, TKey5, TKey6, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TLevel5, TTop>
        where TLevel3 :
            IMultikeyedTreeLevel<TKey4, TKey5, TKey6, TLevel3, TLevel2, TLevel1, TValue, TLevel4, TTop>
        where TLevel2 :
            IMultikeyedTreeLevel<TKey5, TKey6, TLevel2, TLevel1, TValue, TLevel3, TTop>
        where TLevel1 :
            IMultikeyedTreeLevel<TKey6, TValue, TLevel2, TTop>
    {
        /// <summary>
        /// Adds the <paramref name="value"/> at keys
        /// <paramref name="key1"/>, <paramref name="key2"/>,
        /// <paramref name="key3"/>, <paramref name="key4"/>,
        /// <paramref name="key5"/>, and <paramref name="key6"/>.
        /// </summary>
        /// <param name="key1">The first key of the item to add.</param>
        /// <param name="key2">The second key of the item to add.</param>
        /// <param name="key3">The third key of the item to add.</param>
        /// <param name="key4">The fourth key of the item to add.</param>
        /// <param name="key5">The fifth key of the item to add.</param>
        /// <param name="key6">The sixth key of the item to add.</param>
        /// <param name="value">The <typeparamref name="TValue"/> of the
        /// item to add.</param>
        void Add(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TValue value);
        /// <summary>
        /// Removes the item by the 
        /// <paramref name="key1"/>, <paramref name="key2"/>,
        /// <paramref name="key3"/>, <paramref name="key4"/>,
        /// <paramref name="key5"/>, and <paramref name="key6"/>.
        /// </summary>
        /// <param name="key1">The first key of the item to remove.</param>
        /// <param name="key2">The second key of the item to remove.</param>
        /// <param name="key3">The third key of the item to remove.</param>
        /// <param name="key4">The fourth key of the item to remove.</param>
        /// <param name="key5">The fifth key of the item to remove.</param>
        /// <param name="key6">The sixth key of the item to remove.</param>
        /// <returns>true if the item was present and removed; false otherwise.</returns>
        bool Remove(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6);
        /// <summary>
        /// Clears the <see cref="IMultikeyedTree{TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue}"/>
        /// of all its elements.
        /// </summary>
        void Clear();
        /// <summary>
        /// Returns/sets the <typeparamref name="TValue"/> at the
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
        new TValue this[TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6] { get; set; }
    }
    /// <summary>
    /// Defines properties and methods for working with a multikeyed tree.
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
    /// level of the multikeyed tree.</typeparam>
    public interface IMultikeyedTree<TKey1, TKey2, TKey3, TKey4, TKey5, TTop, TLevel4, TLevel3, TLevel2, TLevel1, TValue> :
        IMultikeyedTreeTopLevel,
        IControlledStateMultikeyedTree<TKey1, TKey2, TKey3, TKey4, TKey5, TTop, TLevel4, TLevel3, TLevel2, TLevel1, TValue>
        where TTop :
            IMultikeyedTree<TKey1, TKey2, TKey3, TKey4, TKey5, TTop, TLevel4, TLevel3, TLevel2, TLevel1, TValue>
        where TLevel4 :
            IMultikeyedTreeLevel<TKey2, TKey3, TKey4, TKey5, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TTop, TTop>
        where TLevel3 :
            IMultikeyedTreeLevel<TKey3, TKey4, TKey5, TLevel3, TLevel2, TLevel1, TValue, TLevel4, TTop>
        where TLevel2 :
            IMultikeyedTreeLevel<TKey4, TKey5, TLevel2, TLevel1, TValue, TLevel3, TTop>
        where TLevel1 :
            IMultikeyedTreeLevel<TKey5, TValue, TLevel2, TTop>
    {
        /// <summary>
        /// Adds the <paramref name="value"/> at keys
        /// <paramref name="key1"/>, <paramref name="key2"/>,
        /// <paramref name="key3"/>, <paramref name="key4"/> and
        /// <paramref name="key5"/>.
        /// </summary>
        /// <param name="key1">The first key of the item to add.</param>
        /// <param name="key2">The second key of the item to add.</param>
        /// <param name="key3">The third key of the item to add.</param>
        /// <param name="key4">The fourth key of the item to add.</param>
        /// <param name="key5">The fifth key of the item to add.</param>
        /// <param name="value">The <typeparamref name="TValue"/> of the
        /// item to add.</param>
        void Add(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TValue value);
        /// <summary>
        /// Removes the item by the 
        /// <paramref name="key1"/>, <paramref name="key2"/>,
        /// <paramref name="key3"/>, <paramref name="key4"/> and
        /// <paramref name="key5"/>.
        /// </summary>
        /// <param name="key1">The first key of the item to remove.</param>
        /// <param name="key2">The second key of the item to remove.</param>
        /// <param name="key3">The third key of the item to remove.</param>
        /// <param name="key4">The fourth key of the item to remove.</param>
        /// <param name="key5">The fifth key of the item to remove.</param>
        /// <returns>true if the item was present and removed; false otherwise.</returns>
        bool Remove(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5);
        /// <summary>
        /// Clears the <see cref="IMultikeyedTree{TKey1, TKey2, TKey3, TKey4, TKey5, TValue}"/>
        /// of all its elements.
        /// </summary>
        void Clear();
        /// <summary>
        /// Returns/sets the <typeparamref name="TValue"/> at the
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
        new TValue this[TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5] { get; set; }
    }
    /// <summary>
    /// Defines properties and methods for working with a multikeyed tree.
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
    /// level of the multikeyed tree.</typeparam>
    public interface IMultikeyedTree<TKey1, TKey2, TKey3, TKey4, TTop, TLevel3, TLevel2, TLevel1, TValue> :
        IMultikeyedTreeTopLevel,
        IControlledStateMultikeyedTree<TKey1, TKey2, TKey3, TKey4, TTop, TLevel3, TLevel2, TLevel1, TValue>
        where TTop :
            IMultikeyedTree<TKey1, TKey2, TKey3, TKey4, TTop, TLevel3, TLevel2, TLevel1, TValue>
        where TLevel3 :
            IMultikeyedTreeLevel<TKey2, TKey3, TKey4, TLevel3, TLevel2, TLevel1, TValue, TTop, TTop>
        where TLevel2 :
            IMultikeyedTreeLevel<TKey3, TKey4, TLevel2, TLevel1, TValue, TLevel3, TTop>
        where TLevel1 :
            IMultikeyedTreeLevel<TKey4, TValue, TLevel2, TTop>
    {
        /// <summary>
        /// Adds the <paramref name="value"/> at keys
        /// <paramref name="key1"/>, <paramref name="key2"/>,
        /// <paramref name="key3"/>, and <paramref name="key4"/>.
        /// </summary>
        /// <param name="key1">The first key of the item to add.</param>
        /// <param name="key2">The second key of the item to add.</param>
        /// <param name="key3">The third key of the item to add.</param>
        /// <param name="key4">The fourth key of the item to add.</param>
        /// <param name="value">The <typeparamref name="TValue"/> of the
        /// item to add.</param>
        void Add(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TValue value);
        /// <summary>
        /// Removes the item by the 
        /// <paramref name="key1"/>, <paramref name="key2"/>,
        /// <paramref name="key3"/>, and <paramref name="key4"/>.
        /// </summary>
        /// <param name="key1">The first key of the item to remove.</param>
        /// <param name="key2">The second key of the item to remove.</param>
        /// <param name="key3">The third key of the item to remove.</param>
        /// <param name="key4">The fourth key of the item to remove.</param>
        /// <returns>true if the item was present and removed; false otherwise.</returns>
        bool Remove(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4);
        /// <summary>
        /// Clears the <see cref="IMultikeyedTree{TKey1, TKey2, TKey3, TKey4, TValue}"/>
        /// of all its elements.
        /// </summary>
        void Clear();
        /// <summary>
        /// Returns/sets the <typeparamref name="TValue"/> at the
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
        new TValue this[TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4] { get; set; }
    }
#endif
    /// <summary>
    /// Defines properties and methods for working with a multikeyed tree.
    /// </summary>
    /// <typeparam name="TKey1">The type of the first key of the first level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TKey2">The type of the second key of the second level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TKey3">The type of the third key of the third level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TValue">The type of the value held on the last
    /// level of the multikeyed tree.</typeparam>
    public interface IMultikeyedTree<TKey1, TKey2, TKey3, TTop, TLevel2, TLevel1, TValue> :
        IMultikeyedTreeTopLevel,
        IControlledStateMultikeyedTree<TKey1, TKey2, TKey3, TTop, TLevel2, TLevel1, TValue>
        where TTop :
            IMultikeyedTree<TKey1, TKey2, TKey3, TTop, TLevel2, TLevel1, TValue>
        where TLevel2 :
            IMultikeyedTreeLevel<TKey2, TKey3, TLevel2, TLevel1, TValue, TTop, TTop>
        where TLevel1 :
            IMultikeyedTreeLevel<TKey3, TValue, TLevel2, TTop>
    {
        /// <summary>
        /// Adds the <paramref name="value"/> at keys
        /// <paramref name="key1"/>, <paramref name="key2"/>,
        /// <paramref name="key3"/>, and <paramref name="key4"/>.
        /// </summary>
        /// <param name="key1">The first key of the item to add.</param>
        /// <param name="key2">The second key of the item to add.</param>
        /// <param name="key3">The third key of the item to add.</param>
        /// <param name="value">The <typeparamref name="TValue"/> of the
        /// item to add.</param>
        void Add(TKey1 key1, TKey2 key2, TKey3 key3, TValue value);
        /// <summary>
        /// Removes the item by the 
        /// <paramref name="key1"/>, <paramref name="key2"/>,
        /// <paramref name="key3"/>, and <paramref name="key4"/>.
        /// </summary>
        /// <param name="key1">The first key of the item to remove.</param>
        /// <param name="key2">The second key of the item to remove.</param>
        /// <param name="key3">The third key of the item to remove.</param>
        /// <returns>true if the item was present and removed; false otherwise.</returns>
        bool Remove(TKey1 key1, TKey2 key2, TKey3 key3);
        /// <summary>
        /// Clears the <see cref="IMultikeyedTree{TKey1, TKey2, TKey3, TValue}"/>
        /// of all its elements.
        /// </summary>
        void Clear();
        /// <summary>
        /// Returns/sets the <typeparamref name="TValue"/> at the
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
        new TValue this[TKey1 key1, TKey2 key2, TKey3 key3] { get; set; }
    }
    /// <summary>
    /// Defines properties and methods for working with a multikeyed tree.
    /// </summary>
    /// <typeparam name="TKey1">The type of the first key of the first level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TKey2">The type of the second key of the last level
    /// of the multikeyed tree.</typeparam>
    /// <typeparam name="TValue">The type of the value held on the last
    /// level of the multikeyed tree.</typeparam>
    public interface IMultikeyedTree<TKey1, TKey2, TTop, TLevel1, TValue> :
        IMultikeyedTreeTopLevel,
        IControlledStateMultikeyedTree<TKey1, TKey2, TTop, TLevel1, TValue>
        where TTop :
            IMultikeyedTree<TKey1, TKey2, TTop, TLevel1, TValue>
        where TLevel1 :
            IMultikeyedTreeLevel<TKey2, TValue, TTop, TTop>
    {
        /// <summary>
        /// Adds the <paramref name="value"/> at keys
        /// <paramref name="key1"/> and <paramref name="key2"/>.
        /// </summary>
        /// <param name="key1">The first key of the item to add.</param>
        /// <param name="key2">The second key of the item to add.</param>
        /// <param name="value">The <typeparamref name="TValue"/> of the
        /// item to add.</param>
        void Add(TKey1 key1, TKey2 key2, TValue value);
        /// <summary>
        /// Removes the item by the 
        /// <paramref name="key1"/>, <paramref name="key2"/>,
        /// <paramref name="key3"/>, and <paramref name="key4"/>.
        /// </summary>
        /// <param name="key1">The first key of the item to remove.</param>
        /// <param name="key2">The second key of the item to remove.</param>
        /// <returns>true if the item was present and removed; false otherwise.</returns>
        bool Remove(TKey1 key1, TKey2 key2);
        /// <summary>
        /// Clears the <see cref="IMultikeyedTree{TKey1, TKey2, TValue}"/>
        /// of all its elements.
        /// </summary>
        void Clear();
        /// <summary>
        /// Returns/sets the <typeparamref name="TValue"/> at the
        /// <paramref name="key1"/> and <paramref name="key2"/> provided.
        /// </summary>
        /// <param name="key1">The <typeparamref name="TKey1"/> value which denotes the first key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <param name="key2">The <typeparamref name="TKey2"/> value which denotes the last key 
        /// of the <typeparamref name="TValue"/> to retrieve.</param>
        /// <returns>The <typeparamref name="TValue"/> that resides at the location
        /// denoted by <paramref name="key1"/> and <paramref name="key2"/>.</returns>
        TValue this[TKey1 key1, TKey2 key2] { get; set; }
    }
    public interface IMultikeyedTreeTopLevel :
        IMultikeyedTreeLevel,
        IControlledStateMultikeyedTreeTopLevel
    {

    }
}
