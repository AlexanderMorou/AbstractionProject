using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Collections
{
#if MKD_SEVEN
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
    public interface IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue> :
        IControlledStateDictionary<TKey1, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue>>>,
        IMultikeyedTreeTopLevel2
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
        /// Clears the <see cref="IMultikeyedTree2{TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue}"/>
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
    public interface IMultikeyedTreeLevel2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue, TPrevious, TTopLevel> :
        IControlledStateDictionary<TKey1, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue, TPrevious, TTopLevel>, TTopLevel>>,
        IMultikeyedTreeChildLevel2
        where TPrevious :
            IMultikeyedTreeLevel2
        where TTopLevel :
            IMultikeyedTreeTopLevel2
    {
        /// <summary>
        /// Returns the <typeparamref name="TPrevious"/> that
        /// sits a level above the current point in the hierarchy.
        /// </summary>
        new TPrevious Previous { get; }
        /// <summary>
        /// Returns the <typeparamref name="TTopLevel"/> section 
        /// of the multikeyed tree.
        /// </summary>
        new TTopLevel TopLevel { get; }
    }
#endif
#if MKD_SIX
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
    public interface IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue> :
        IControlledStateDictionary<TKey1, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue>>>,
        IMultikeyedTreeTopLevel2
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
        /// Clears the <see cref="IMultikeyedTree2{TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue}"/>
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
    public interface IMultikeyedTreeLevel2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue, TPrevious, TTopLevel> :
        IControlledStateDictionary<TKey1, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue, TPrevious, TTopLevel>, TTopLevel>>,
        IMultikeyedTreeChildLevel2
        where TPrevious :
            IMultikeyedTreeLevel2
        where TTopLevel :
            IMultikeyedTreeTopLevel2
    {
        /// <summary>
        /// Returns the <typeparamref name="TPrevious"/> that
        /// sits a level above the current point in the hierarchy.
        /// </summary>
        new TPrevious Previous { get; }
        /// <summary>
        /// Returns the <typeparamref name="TTopLevel"/> section 
        /// of the multikeyed tree.
        /// </summary>
        new TTopLevel TopLevel { get; }
    }
#endif
#if MKD_FIVE
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
    public interface IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue> :
        IControlledStateDictionary<TKey1, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>>>,
        IMultikeyedTreeTopLevel2
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
        /// Clears the <see cref="IMultikeyedTree2{TKey1, TKey2, TKey3, TKey4, TKey5, TValue}"/>
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
    public interface IMultikeyedTreeLevel2<TKey1, TKey2, TKey3, TKey4, TValue, TPrevious, TTopLevel> :
        IControlledStateDictionary<TKey1, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TValue, IMultikeyedTreeLevel2<TKey1, TKey2, TKey3, TKey4, TValue, TPrevious, TTopLevel>, TTopLevel>>,
        IMultikeyedTreeChildLevel2
        where TPrevious :
            IMultikeyedTreeLevel2
        where TTopLevel :
            IMultikeyedTreeTopLevel2
    {
        /// <summary>
        /// Returns the <typeparamref name="TPrevious"/> that
        /// sits a level above the current point in the hierarchy.
        /// </summary>
        new TPrevious Previous { get; }
        /// <summary>
        /// Returns the <typeparamref name="TTopLevel"/> section 
        /// of the multikeyed tree.
        /// </summary>
        new TTopLevel TopLevel { get; }
    }
#endif
#if MKD_FOUR
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
    public interface IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue> :
        IControlledStateDictionary<TKey1, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TKey4, TValue>>>,
        IMultikeyedTreeTopLevel2
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
        /// Clears the <see cref="IMultikeyedTree2{TKey1, TKey2, TKey3, TKey4, TValue}"/>
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
    public interface IMultikeyedTreeLevel2<TKey1, TKey2, TKey3, TValue, TPrevious, TTopLevel> :
        IControlledStateDictionary<TKey1, IMultikeyedTreeLevel2<TKey2, TKey3, TValue, IMultikeyedTreeLevel2<TKey1, TKey2, TKey3, TValue, TPrevious, TTopLevel>, TTopLevel>>,
        IMultikeyedTreeChildLevel2
        where TPrevious :
            IMultikeyedTreeLevel2
        where TTopLevel :
            IMultikeyedTreeTopLevel2
    {
        /// <summary>
        /// Returns the <typeparamref name="TPrevious"/> that
        /// sits a level above the current point in the hierarchy.
        /// </summary>
        new TPrevious Previous { get; }
        /// <summary>
        /// Returns the <typeparamref name="TTopLevel"/> section 
        /// of the multikeyed tree.
        /// </summary>
        new TTopLevel TopLevel { get; }
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
    public interface IMultikeyedTree2<TKey1, TKey2, TKey3, TValue> :
        IControlledStateDictionary<TKey1, IMultikeyedTreeLevel2<TKey2, TKey3, TValue, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>, IMultikeyedTree2<TKey1, TKey2, TKey3, TValue>>>,
        IMultikeyedTreeTopLevel2
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
        /// Clears the <see cref="IMultikeyedTree2{TKey1, TKey2, TKey3, TValue}"/>
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
    public interface IMultikeyedTree2<TKey1, TKey2, TValue> :
        IControlledStateDictionary<TKey1, IMultikeyedTreeLevel2<TKey2, TValue,IMultikeyedTree2<TKey1, TKey2, TValue>,IMultikeyedTree2<TKey1, TKey2, TValue>>>,
        IMultikeyedTreeTopLevel2
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
        /// Clears the <see cref="IMultikeyedTree2{TKey1, TKey2, TValue}"/>
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
    public interface IMultikeyedTreeTopLevel2 :
        IMultikeyedTreeLevel2
    {

    }
#if MKD_FIVE
#endif
    public interface IMultikeyedTreeLevel2<TKey1, TKey2, TValue, TPrevious, TTopLevel> :
        IControlledStateDictionary<TKey1, IMultikeyedTreeLevel2<TKey2, TValue, IMultikeyedTreeLevel2<TKey1, TKey2, TValue, TPrevious, TTopLevel>, TTopLevel>>,
        IMultikeyedTreeChildLevel2
        where TPrevious :
            IMultikeyedTreeLevel2
        where TTopLevel :
            IMultikeyedTreeTopLevel2
    {
        /// <summary>
        /// Returns the <typeparamref name="TPrevious"/> that
        /// sits a level above the current point in the hierarchy.
        /// </summary>
        new TPrevious Previous { get; }
        /// <summary>
        /// Returns the <typeparamref name="TTopLevel"/> section 
        /// of the multikeyed tree.
        /// </summary>
        new TTopLevel TopLevel { get; }
    }
    public interface IMultikeyedTreeLevel2<TKey, TValue, TPrevious, TTopLevel> :
        IControlledStateDictionary<TKey, TValue>,
        IMultikeyedTreeChildLevel2
        where TPrevious :
            IMultikeyedTreeLevel2
        where TTopLevel :
            IMultikeyedTreeTopLevel2
    {
        /// <summary>
        /// Returns the <typeparamref name="TPrevious"/> that
        /// sits a level above the current point in the hierarchy.
        /// </summary>
        new TPrevious Previous { get; }
        /// <summary>
        /// Returns the <typeparamref name="TTopLevel"/> section 
        /// of the multikeyed tree.
        /// </summary>
        new TTopLevel TopLevel { get; }
    }

    public interface IMultikeyedTreeLevel2
    {
        /// <summary>
        /// Returns the toplevel section of the multikeyed tree.
        /// </summary>
        new IMultikeyedTreeTopLevel2 TopLevel { get; }
        /// <summary>
        /// Returns the <see cref="Int32"/> value denoting the level 
        /// on which the <see cref="IMultikeyedTreeLevel2"/> is.
        /// </summary>
        int Level { get; }
    }
    public interface IMultikeyedTreeChildLevel2 :
        IMultikeyedTreeLevel2
    {
        /// <summary>
        /// Returns the <see cref="IMultikeyedTreeLevel2"/> that
        /// sits a level above the current point in the hierarchy.
        /// </summary>
        new IMultikeyedTreeLevel2 Previous { get; }
    }

}
