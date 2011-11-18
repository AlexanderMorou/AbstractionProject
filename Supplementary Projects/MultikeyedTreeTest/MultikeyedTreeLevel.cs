using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Collections
{
#if MKD_SEVEN
    public class MultikeyedTreeLevel2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue, TPrevious, TTopLevel> :
        ControlledStateDictionary<TKey1, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TKey6, TValue, IMultikeyedTreeLevel2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue, TPrevious, TTopLevel>, TTopLevel>>,
        IMultikeyedTreeLevel2<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue, TPrevious, TTopLevel>
        where TPrevious :
            IMultikeyedTreeLevel2
        where TTopLevel :
            IMultikeyedTreeTopLevel2
    {
        internal MultikeyedTreeLevel2(TPrevious parent)
        {
            this.Previous = parent;
        }

        public TPrevious Previous { get; private set; }

        public TTopLevel TopLevel
        {
            get
            {
                if (Previous == null)
                    return default(TTopLevel);
                return (TTopLevel)Previous.TopLevel;
            }
        }

        #region IMultikeyedTreeChildLevel2 Members

        IMultikeyedTreeLevel2 IMultikeyedTreeChildLevel2.Previous
        {
            get { return this.Previous; }
        }

        #endregion

        #region IMultikeyedTreeLevel2 Members

        public int Level
        {
            get { return Previous.Level + 1; }
        }

        IMultikeyedTreeTopLevel2 IMultikeyedTreeLevel2.TopLevel
        {
            get { return this.TopLevel; }
        }

        #endregion
    }
#endif
#if MKD_SIX
    public class MultikeyedTreeLevel2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue, TPrevious, TTopLevel> :
        ControlledStateDictionary<TKey1, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TKey5, TValue, IMultikeyedTreeLevel2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue, TPrevious, TTopLevel>, TTopLevel>>,
        IMultikeyedTreeLevel2<TKey1, TKey2, TKey3, TKey4, TKey5, TValue, TPrevious, TTopLevel>
        where TPrevious :
            IMultikeyedTreeLevel2
        where TTopLevel :
            IMultikeyedTreeTopLevel2
    {
        internal MultikeyedTreeLevel2(TPrevious parent)
        {
            this.Previous = parent;
        }

        public TPrevious Previous { get; private set; }

        public TTopLevel TopLevel
        {
            get
            {
                if (Previous == null)
                    return default(TTopLevel);
                return (TTopLevel)Previous.TopLevel;
            }
        }

        #region IMultikeyedTreeChildLevel2 Members

        IMultikeyedTreeLevel2 IMultikeyedTreeChildLevel2.Previous
        {
            get { return this.Previous; }
        }

        #endregion

        #region IMultikeyedTreeLevel2 Members

        public int Level
        {
            get { return Previous.Level + 1; }
        }

        IMultikeyedTreeTopLevel2 IMultikeyedTreeLevel2.TopLevel
        {
            get { return this.TopLevel; }
        }

        #endregion
    }
#endif
#if MKD_FIVE
    public class MultikeyedTreeLevel2<TKey1, TKey2, TKey3, TKey4, TValue, TPrevious, TTopLevel> :
        ControlledStateDictionary<TKey1, IMultikeyedTreeLevel2<TKey2, TKey3, TKey4, TValue, IMultikeyedTreeLevel2<TKey1, TKey2, TKey3, TKey4, TValue, TPrevious, TTopLevel>, TTopLevel>>,
        IMultikeyedTreeLevel2<TKey1, TKey2, TKey3, TKey4, TValue, TPrevious, TTopLevel>
        where TPrevious :
            IMultikeyedTreeLevel2
        where TTopLevel :
            IMultikeyedTreeTopLevel2
    {
        internal MultikeyedTreeLevel2(TPrevious parent)
        {
            this.Previous = parent;
        }

        public TPrevious Previous { get; private set; }

        public TTopLevel TopLevel
        {
            get
            {
                if (Previous == null)
                    return default(TTopLevel);
                return (TTopLevel)Previous.TopLevel;
            }
        }

        #region IMultikeyedTreeChildLevel2 Members

        IMultikeyedTreeLevel2 IMultikeyedTreeChildLevel2.Previous
        {
            get { return this.Previous; }
        }

        #endregion

        #region IMultikeyedTreeLevel2 Members

        public int Level
        {
            get { return Previous.Level + 1; }
        }

        IMultikeyedTreeTopLevel2 IMultikeyedTreeLevel2.TopLevel
        {
            get { return this.TopLevel; }
        }

        #endregion
    }
#endif
#if MKD_FOUR
    public class MultikeyedTreeLevel2<TKey1, TKey2, TKey3, TValue, TPrevious, TTopLevel> :
        ControlledStateDictionary<TKey1, IMultikeyedTreeLevel2<TKey2, TKey3, TValue, IMultikeyedTreeLevel2<TKey1, TKey2, TKey3, TValue, TPrevious, TTopLevel>, TTopLevel>>,
        IMultikeyedTreeLevel2<TKey1, TKey2, TKey3, TValue, TPrevious, TTopLevel>
        where TPrevious :
            IMultikeyedTreeLevel2
        where TTopLevel :
            IMultikeyedTreeTopLevel2
    {
        internal MultikeyedTreeLevel2(TPrevious parent)
        {
            this.Previous = parent;
        }

        public TPrevious Previous { get; private set; }

        public TTopLevel TopLevel
        {
            get
            {
                if (Previous == null)
                    return default(TTopLevel);
                return (TTopLevel)Previous.TopLevel;
            }
        }

        #region IMultikeyedTreeChildLevel2 Members

        IMultikeyedTreeLevel2 IMultikeyedTreeChildLevel2.Previous
        {
            get { return this.Previous; }
        }

        #endregion

        #region IMultikeyedTreeLevel2 Members

        public int Level
        {
            get { return Previous.Level + 1; }
        }

        IMultikeyedTreeTopLevel2 IMultikeyedTreeLevel2.TopLevel
        {
            get { return this.TopLevel; }
        }

        #endregion
    }
#endif
    public class MultikeyedTreeLevel2<TKey1, TKey2, TValue, TPrevious, TTopLevel> :
        ControlledStateDictionary<TKey1, IMultikeyedTreeLevel2<TKey2, TValue, IMultikeyedTreeLevel2<TKey1, TKey2, TValue, TPrevious, TTopLevel>, TTopLevel>>,
        IMultikeyedTreeLevel2<TKey1, TKey2, TValue, TPrevious, TTopLevel>
        where TPrevious :
            IMultikeyedTreeLevel2
        where TTopLevel :
            IMultikeyedTreeTopLevel2
    {
        internal MultikeyedTreeLevel2(TPrevious parent)
        {
            this.Previous = parent;
        }

        public TPrevious Previous { get; private set; }

        public TTopLevel TopLevel
        {
            get
            {
                if (Previous == null)
                    return default(TTopLevel);
                return (TTopLevel)Previous.TopLevel;
            }
        }


        #region IMultikeyedTreeChildLevel2 Members
        IMultikeyedTreeLevel2 IMultikeyedTreeChildLevel2.Previous
        {
            get { return this.Previous; }
        }

        #endregion

        #region IMultikeyedTreeLevel2 Members

        public int Level
        {
            get { return Previous.Level + 1; }
        }

        IMultikeyedTreeTopLevel2 IMultikeyedTreeLevel2.TopLevel
        {
            get { return this.TopLevel; }
        }

        #endregion

        internal void Add(TKey1 key, IMultikeyedTreeLevel2<TKey2, TValue, IMultikeyedTreeLevel2<TKey1, TKey2, TValue, TPrevious, TTopLevel>, TTopLevel> value)
        {
            this._Add(key, value);
        }

        internal void Remove(TKey1 key1, TKey2 key2)
        {
            
        }

    }
    public class MultikeyedTreeLevel2<TKey, TValue, TPrevious, TTopLevel> :
        ControlledStateDictionary<TKey, TValue>,
        IMultikeyedTreeLevel2<TKey, TValue, TPrevious, TTopLevel>
        where TPrevious :
            IMultikeyedTreeLevel2
        where TTopLevel :
            IMultikeyedTreeTopLevel2
    {
        internal MultikeyedTreeLevel2(TPrevious parent)
        {
            this.Previous = parent;
        }

        public TPrevious Previous { get; private set; }

        public TTopLevel TopLevel
        {
            get
            {
                if (Previous == null)
                    return default(TTopLevel);
                return (TTopLevel)Previous.TopLevel;
            }
        }

        #region IMultikeyedTreeChildLevel2 Members

        IMultikeyedTreeLevel2 IMultikeyedTreeChildLevel2.Previous
        {
            get { return this.Previous; }
        }

        #endregion

        #region IMultikeyedTreeLevel2 Members

        public int Level
        {
            get { return Previous.Level + 1; }
        }

        IMultikeyedTreeTopLevel2 IMultikeyedTreeLevel2.TopLevel
        {
            get { return this.TopLevel; }
        }

        #endregion

        internal void Add(TKey key, TValue value)
        {
            this._Add(key, value);
        }

        internal void Remove(TKey key)
        {
            this._Remove(key);
        }
    }
}
