using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Collections
{
#if REQUIRE_NGEN
    public interface IMultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TLevel6, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TPrevious, TTopLevel> :
        IControlledStateMultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TLevel6, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TPrevious, TTopLevel>,
        IMultikeyedTreeChildLevel
        where TLevel6 :
            IMultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TLevel6, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TPrevious, TTopLevel>
        where TLevel5 :
            IMultikeyedTreeLevel<TKey2, TKey3, TKey4, TKey5, TKey6, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TLevel6, TTopLevel>
        where TLevel4 :
            IMultikeyedTreeLevel<TKey3, TKey4, TKey5, TKey6, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TLevel5, TTopLevel>
        where TLevel3 :
            IMultikeyedTreeLevel<TKey4, TKey5, TKey6, TLevel3, TLevel2, TLevel1, TValue, TLevel4, TTopLevel>
        where TLevel2 :
            IMultikeyedTreeLevel<TKey5, TKey6, TLevel2, TLevel1, TValue, TLevel3, TTopLevel>
        where TLevel1 :
            IMultikeyedTreeLevel<TKey6, TValue, TLevel2, TTopLevel>
        where TPrevious :
            IMultikeyedTreeLevel
        where TTopLevel :
            IMultikeyedTreeTopLevel
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

    public interface IMultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TKey5, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TPrevious, TTopLevel> :
        IControlledStateMultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TKey5, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TPrevious, TTopLevel>,
        IMultikeyedTreeChildLevel
        where TLevel5 :
            IMultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TKey5, TLevel5, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TPrevious, TTopLevel>
        where TLevel4 :
            IMultikeyedTreeLevel<TKey2, TKey3, TKey4, TKey5, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TLevel5, TTopLevel>
        where TLevel3 :
            IMultikeyedTreeLevel<TKey3, TKey4, TKey5, TLevel3, TLevel2, TLevel1, TValue, TLevel4, TTopLevel>
        where TLevel2 :
            IMultikeyedTreeLevel<TKey4, TKey5, TLevel2, TLevel1, TValue, TLevel3, TTopLevel>
        where TLevel1 :
            IMultikeyedTreeLevel<TKey5, TValue, TLevel2, TTopLevel>
        where TPrevious :
            IMultikeyedTreeLevel
        where TTopLevel :
            IMultikeyedTreeTopLevel
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

    public interface IMultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TPrevious, TTopLevel> :
        IControlledStateMultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TPrevious, TTopLevel>,
        IMultikeyedTreeChildLevel
        where TLevel4 :
            IMultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TLevel4, TLevel3, TLevel2, TLevel1, TValue, TPrevious, TTopLevel>
        where TLevel3 :
            IMultikeyedTreeLevel<TKey2, TKey3, TKey4, TLevel3, TLevel2, TLevel1, TValue, TLevel4, TTopLevel>
        where TLevel2 :
            IMultikeyedTreeLevel<TKey3, TKey4, TLevel2, TLevel1, TValue, TLevel3, TTopLevel>
        where TLevel1 :
            IMultikeyedTreeLevel<TKey4, TValue, TLevel2, TTopLevel>
        where TPrevious :
            IMultikeyedTreeLevel
        where TTopLevel :
            IMultikeyedTreeTopLevel
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
    public interface IMultikeyedTreeLevel<TKey1, TKey2, TKey3, TLevel3, TLevel2, TLevel1, TValue, TPrevious, TTopLevel> :
        IControlledStateMultikeyedTreeLevel<TKey1, TKey2, TKey3, TLevel3, TLevel2, TLevel1, TValue, TPrevious, TTopLevel>,
        IMultikeyedTreeChildLevel
        where TLevel3 :
            IMultikeyedTreeLevel<TKey1, TKey2, TKey3, TLevel3, TLevel2, TLevel1, TValue, TPrevious, TTopLevel>
        where TLevel2 :
            IMultikeyedTreeLevel<TKey2, TKey3, TLevel2, TLevel1, TValue, TLevel3, TTopLevel>
        where TLevel1 :
            IMultikeyedTreeLevel<TKey3, TValue, TLevel2, TTopLevel>
        where TPrevious :
            IMultikeyedTreeLevel
        where TTopLevel :
            IMultikeyedTreeTopLevel
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
    public interface IMultikeyedTreeLevel<TKey1, TKey2, TLevel2, TLevel1, TValue, TPrevious, TTopLevel> :
        IControlledStateMultikeyedTreeLevel<TKey1, TKey2, TLevel2, TLevel1, TValue, TPrevious, TTopLevel>,
        IMultikeyedTreeChildLevel
        where TLevel2 :
            IMultikeyedTreeLevel<TKey1, TKey2, TLevel2, TLevel1, TValue, TPrevious, TTopLevel>
        where TLevel1 :
            IMultikeyedTreeLevel<TKey2, TValue, TLevel2, TTopLevel>
        where TPrevious :
            IMultikeyedTreeLevel
        where TTopLevel :
            IMultikeyedTreeTopLevel
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
    public interface IMultikeyedTreeLevel<TKey, TValue, TPrevious, TTopLevel> :
        IControlledStateMultikeyedTreeLevel<TKey, TValue, TPrevious, TTopLevel>,
        IMultikeyedTreeChildLevel
        where TPrevious :
            IMultikeyedTreeLevel
        where TTopLevel :
            IMultikeyedTreeTopLevel
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

    public interface IMultikeyedTreeLevel :
        IControlledStateMultikeyedTreeLevel
    {
        /// <summary>
        /// Returns the toplevel section of the multikeyed tree.
        /// </summary>
        new IMultikeyedTreeTopLevel TopLevel { get; }
    }
    public interface IMultikeyedTreeChildLevel :
        IMultikeyedTreeLevel,
        IControlledStateMultikeyedTreeLevel
    {
        /// <summary>
        /// Returns the <see cref="IMultikeyedTreeLevel"/> that
        /// sits a level above the current point in the hierarchy.
        /// </summary>
        new IMultikeyedTreeLevel Previous { get; }
    }

}
