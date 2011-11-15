using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    public interface IControlledStateMultikeyedTreeLevel
    {
        /// <summary>
        /// Returns the <see cref="Int32"/> value denoting
        /// the level of the tree.
        /// </summary>
        int Level { get; }
        /// <summary>
        /// Returns the toplevel section of the multikeyed tree.
        /// </summary>
        IControlledStateMultikeyedTreeTopLevel TopLevel { get; }
    }
    public interface IControlledStateMultikeyedTreeChildLevel :
        IControlledStateMultikeyedTreeLevel
    {
        /// <summary>
        /// Returns the <see cref="IControlledStateMultikeyedTreeLevel"/> that
        /// sits a level above the current point in the hierarchy.
        /// </summary>
        IControlledStateMultikeyedTreeLevel Previous { get; }
    }
    public interface IControlledStateMultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TLevel6, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TPrevious, TTopLevel> :
        IControlledStateDictionary<TKey1, TLevel5>,
        IControlledStateMultikeyedTreeChildLevel
        where TLevel6 :
            IControlledStateMultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TLevel6, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TPrevious, TTopLevel>
        where TLevel5 :
            IControlledStateMultikeyedTreeLevel<TKey2, TKey3, TKey4, TKey5, TKey6, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TLevel6, TTopLevel>
        where TLevel4 :
            IControlledStateMultikeyedTreeLevel<TKey3, TKey4, TKey5, TKey6, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TLevel5, TTopLevel>
        where TLevel3 :
            IControlledStateMultikeyedTreeLevel<TKey4, TKey5, TKey6, TLevel3, TLevel2, TLevel1, TValue, TLevel4, TTopLevel>
        where TLevel2 :
            IControlledStateMultikeyedTreeLevel<TKey5, TKey6, TLevel2, TLevel1, TValue, TLevel3, TTopLevel>
        where TLevel1 :
            IControlledStateMultikeyedTreeLevel<TKey6, TValue, TLevel2, TTopLevel>
        where TPrevious :
            IControlledStateMultikeyedTreeLevel
        where TTopLevel :
            IControlledStateMultikeyedTreeTopLevel
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

    public interface IControlledStateMultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TKey5, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TPrevious, TTopLevel> :
        IControlledStateDictionary<TKey1, TLevel4>,
        IControlledStateMultikeyedTreeChildLevel
        where TLevel5 :
            IControlledStateMultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TKey5, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TPrevious, TTopLevel>
        where TLevel4 :
            IControlledStateMultikeyedTreeLevel<TKey2, TKey3, TKey4, TKey5, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TLevel5, TTopLevel>
        where TLevel3 :
            IControlledStateMultikeyedTreeLevel<TKey3, TKey4, TKey5, TLevel3, TLevel2, TLevel1, TValue, TLevel4, TTopLevel>
        where TLevel2 :
            IControlledStateMultikeyedTreeLevel<TKey4, TKey5, TLevel2, TLevel1, TValue, TLevel3, TTopLevel>
        where TLevel1 :
            IControlledStateMultikeyedTreeLevel<TKey5, TValue, TLevel2, TTopLevel>
        where TPrevious :
            IControlledStateMultikeyedTreeLevel
        where TTopLevel :
            IControlledStateMultikeyedTreeTopLevel
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

    public interface IControlledStateMultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TPrevious, TTopLevel> :
        IControlledStateDictionary<TKey1, TLevel3>,
        IControlledStateMultikeyedTreeChildLevel
        where TLevel4 :
            IControlledStateMultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TPrevious, TTopLevel>
        where TLevel3 :
            IControlledStateMultikeyedTreeLevel<TKey2, TKey3, TKey4, TLevel3, TLevel2, TLevel1, TValue, TLevel4, TTopLevel>
        where TLevel2 :
            IControlledStateMultikeyedTreeLevel<TKey3, TKey4, TLevel2, TLevel1, TValue, TLevel3, TTopLevel>
        where TLevel1 :
            IControlledStateMultikeyedTreeLevel<TKey4, TValue, TLevel2, TTopLevel>
        where TPrevious :
            IControlledStateMultikeyedTreeLevel
        where TTopLevel :
            IControlledStateMultikeyedTreeTopLevel
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
    public interface IControlledStateMultikeyedTreeLevel<TKey1, TKey2, TKey3, TLevel3, TLevel2, TLevel1, TValue, TPrevious, TTopLevel> :
        IControlledStateDictionary<TKey1, TLevel2>,
        IControlledStateMultikeyedTreeChildLevel
        where TLevel3 :
            IControlledStateMultikeyedTreeLevel<TKey1, TKey2, TKey3, TLevel3, TLevel2, TLevel1, TValue, TPrevious, TTopLevel>
        where TLevel2 :
            IControlledStateMultikeyedTreeLevel<TKey2, TKey3, TLevel2, TLevel1, TValue, TLevel3, TTopLevel>
        where TLevel1 :
            IControlledStateMultikeyedTreeLevel<TKey3, TValue, TLevel2, TTopLevel>
        where TPrevious :
            IControlledStateMultikeyedTreeLevel
        where TTopLevel :
            IControlledStateMultikeyedTreeTopLevel
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
    public interface IControlledStateMultikeyedTreeLevel<TKey1, TKey2, TLevel2, TLevel1, TValue, TPrevious, TTopLevel> :
        IControlledStateDictionary<TKey1, TLevel1>,
        IControlledStateMultikeyedTreeChildLevel
        where TLevel2 :
            IControlledStateMultikeyedTreeLevel<TKey1, TKey2, TLevel2, TLevel1, TValue, TPrevious, TTopLevel>
        where TLevel1 :
            IControlledStateMultikeyedTreeLevel<TKey2, TValue, TLevel2, TTopLevel>
        where TPrevious :
            IControlledStateMultikeyedTreeLevel
        where TTopLevel :
            IControlledStateMultikeyedTreeTopLevel
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
    public interface IControlledStateMultikeyedTreeLevel<TKey, TValue, TPrevious, TTopLevel> :
        IControlledStateDictionary<TKey, TValue>,
        IControlledStateMultikeyedTreeChildLevel
        where TPrevious :
            IControlledStateMultikeyedTreeLevel
        where TTopLevel :
            IControlledStateMultikeyedTreeTopLevel
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
}
