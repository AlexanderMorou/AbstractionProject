using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    public class MultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue, TPrevious, TTopLevel> :
        ControlledStateDictionary<TKey1, MultikeyedTreeLevel<TKey2, TKey3, TKey4, TKey5, TKey6, TValue, MultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue, TPrevious, TTopLevel>, TTopLevel>>,
        IMultikeyedTreeLevel<
            TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, 
        MultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue, TPrevious, TTopLevel>,
        MultikeyedTreeLevel<TKey2, TKey3, TKey4, TKey5, TKey6, TValue, MultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue, TPrevious, TTopLevel>, TTopLevel>,
        MultikeyedTreeLevel<TKey3, TKey4, TKey5, TKey6, TValue, MultikeyedTreeLevel<TKey2, TKey3, TKey4, TKey5, TKey6, TValue, MultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue, TPrevious, TTopLevel>, TTopLevel>, TTopLevel>,
        MultikeyedTreeLevel<TKey4, TKey5, TKey6, TValue, MultikeyedTreeLevel<TKey3, TKey4, TKey5, TKey6, TValue, MultikeyedTreeLevel<TKey2, TKey3, TKey4, TKey5, TKey6, TValue, MultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue, TPrevious, TTopLevel>, TTopLevel>, TTopLevel>, TTopLevel>,
        MultikeyedTreeLevel<TKey5, TKey6, TValue, MultikeyedTreeLevel<TKey4, TKey5, TKey6, TValue, MultikeyedTreeLevel<TKey3, TKey4, TKey5, TKey6, TValue, MultikeyedTreeLevel<TKey2, TKey3, TKey4, TKey5, TKey6, TValue, MultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue, TPrevious, TTopLevel>, TTopLevel>, TTopLevel>, TTopLevel>, TTopLevel>,
        MultikeyedTreeLevel<TKey6, TValue, MultikeyedTreeLevel<TKey5, TKey6, TValue, MultikeyedTreeLevel<TKey4, TKey5, TKey6, TValue, MultikeyedTreeLevel<TKey3, TKey4, TKey5, TKey6, TValue, MultikeyedTreeLevel<TKey2, TKey3, TKey4, TKey5, TKey6, TValue, MultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue, TPrevious, TTopLevel>, TTopLevel>, TTopLevel>, TTopLevel>, TTopLevel>, TTopLevel>, 
            TValue, TPrevious, TTopLevel>
        where TPrevious :
            IMultikeyedTreeLevel
        where TTopLevel :
            IMultikeyedTreeTopLevel
    {
        internal MultikeyedTreeLevel(TPrevious parent)
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


        #region IControlledStateMultikeyedTreeChildLevel Members

        IControlledStateMultikeyedTreeLevel IControlledStateMultikeyedTreeChildLevel.Previous
        {
            get { return this.Previous; }
        }

        #endregion

        #region IControlledStateMultikeyedTreeLevel Members

        public int Level
        {
            get { return Previous.Level + 1; }
        }

        IControlledStateMultikeyedTreeTopLevel IControlledStateMultikeyedTreeLevel.TopLevel
        {
            get { return this.TopLevel; }
        }

        #endregion

        #region IMultikeyedTreeChildLevel Members

        IMultikeyedTreeLevel IMultikeyedTreeChildLevel.Previous
        {
            get { return this.Previous; }
        }

        #endregion

        #region IMultikeyedTreeLevel Members

        IMultikeyedTreeTopLevel IMultikeyedTreeLevel.TopLevel
        {
            get { return this.TopLevel; }
        }

        #endregion
    }
    public class MultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TKey5, TValue, TPrevious, TTopLevel> :
        ControlledStateDictionary<TKey1, MultikeyedTreeLevel<TKey2, TKey3, TKey4, TKey5, TValue, MultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TKey5, TValue, TPrevious, TTopLevel>, TTopLevel>>,
        IMultikeyedTreeLevel<
            TKey1, TKey2, TKey3, TKey4, TKey5,
        MultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TKey5, TValue, TPrevious, TTopLevel>,
        MultikeyedTreeLevel<TKey2, TKey3, TKey4, TKey5, TValue, MultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TKey5, TValue, TPrevious, TTopLevel>, TTopLevel>,
        MultikeyedTreeLevel<TKey3, TKey4, TKey5, TValue, MultikeyedTreeLevel<TKey2, TKey3, TKey4, TKey5, TValue, MultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TKey5, TValue, TPrevious, TTopLevel>, TTopLevel>, TTopLevel>,
        MultikeyedTreeLevel<TKey4, TKey5, TValue, MultikeyedTreeLevel<TKey3, TKey4, TKey5, TValue, MultikeyedTreeLevel<TKey2, TKey3, TKey4, TKey5, TValue, MultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TKey5, TValue, TPrevious, TTopLevel>, TTopLevel>, TTopLevel>, TTopLevel>,
        MultikeyedTreeLevel<TKey5, TValue, MultikeyedTreeLevel<TKey4, TKey5, TValue, MultikeyedTreeLevel<TKey3, TKey4, TKey5, TValue, MultikeyedTreeLevel<TKey2, TKey3, TKey4, TKey5, TValue, MultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TKey5, TValue, TPrevious, TTopLevel>, TTopLevel>, TTopLevel>, TTopLevel>, TTopLevel>,
            TValue, TPrevious, TTopLevel>
        where TPrevious :
            IMultikeyedTreeLevel
        where TTopLevel :
            IMultikeyedTreeTopLevel
    {
        internal MultikeyedTreeLevel(TPrevious parent)
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


        #region IControlledStateMultikeyedTreeChildLevel Members

        IControlledStateMultikeyedTreeLevel IControlledStateMultikeyedTreeChildLevel.Previous
        {
            get { return this.Previous; }
        }

        #endregion

        #region IControlledStateMultikeyedTreeLevel Members

        public int Level
        {
            get { return Previous.Level + 1; }
        }

        IControlledStateMultikeyedTreeTopLevel IControlledStateMultikeyedTreeLevel.TopLevel
        {
            get { return this.TopLevel; }
        }

        #endregion

        #region IMultikeyedTreeChildLevel Members

        IMultikeyedTreeLevel IMultikeyedTreeChildLevel.Previous
        {
            get { return this.Previous; }
        }

        #endregion

        #region IMultikeyedTreeLevel Members

        IMultikeyedTreeTopLevel IMultikeyedTreeLevel.TopLevel
        {
            get { return this.TopLevel; }
        }

        #endregion
    }
    public class MultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TValue, TPrevious, TTopLevel> :
        ControlledStateDictionary<TKey1, MultikeyedTreeLevel<TKey2, TKey3, TKey4, TValue, MultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TValue, TPrevious, TTopLevel>, TTopLevel>>,
        IMultikeyedTreeLevel<
            TKey1, TKey2, TKey3, TKey4,
        MultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TValue, TPrevious, TTopLevel>,
        MultikeyedTreeLevel<TKey2, TKey3, TKey4, TValue, MultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TValue, TPrevious, TTopLevel>, TTopLevel>,
        MultikeyedTreeLevel<TKey3, TKey4, TValue, MultikeyedTreeLevel<TKey2, TKey3, TKey4, TValue, MultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TValue, TPrevious, TTopLevel>, TTopLevel>, TTopLevel>,
        MultikeyedTreeLevel<TKey4, TValue, MultikeyedTreeLevel<TKey3, TKey4, TValue, MultikeyedTreeLevel<TKey2, TKey3, TKey4, TValue, MultikeyedTreeLevel<TKey1, TKey2, TKey3, TKey4, TValue, TPrevious, TTopLevel>, TTopLevel>, TTopLevel>, TTopLevel>,
            TValue, TPrevious, TTopLevel>
        where TPrevious :
            IMultikeyedTreeLevel
        where TTopLevel :
            IMultikeyedTreeTopLevel
    {
        internal MultikeyedTreeLevel(TPrevious parent)
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


        #region IControlledStateMultikeyedTreeChildLevel Members

        IControlledStateMultikeyedTreeLevel IControlledStateMultikeyedTreeChildLevel.Previous
        {
            get { return this.Previous; }
        }

        #endregion

        #region IControlledStateMultikeyedTreeLevel Members

        public int Level
        {
            get { return Previous.Level + 1; }
        }

        IControlledStateMultikeyedTreeTopLevel IControlledStateMultikeyedTreeLevel.TopLevel
        {
            get { return this.TopLevel; }
        }

        #endregion

        #region IMultikeyedTreeChildLevel Members

        IMultikeyedTreeLevel IMultikeyedTreeChildLevel.Previous
        {
            get { return this.Previous; }
        }

        #endregion

        #region IMultikeyedTreeLevel Members

        IMultikeyedTreeTopLevel IMultikeyedTreeLevel.TopLevel
        {
            get { return this.TopLevel; }
        }

        #endregion
    }
    public class MultikeyedTreeLevel<TKey1, TKey2, TKey3, TValue, TPrevious, TTopLevel> :
        ControlledStateDictionary<TKey1, MultikeyedTreeLevel<TKey2, TKey3, TValue, MultikeyedTreeLevel<TKey1, TKey2, TKey3, TValue, TPrevious, TTopLevel>, TTopLevel>>,
        IMultikeyedTreeLevel<
            TKey1, TKey2, TKey3,
        MultikeyedTreeLevel<TKey1, TKey2, TKey3, TValue, TPrevious, TTopLevel>,
        MultikeyedTreeLevel<TKey2, TKey3, TValue, MultikeyedTreeLevel<TKey1, TKey2, TKey3, TValue, TPrevious, TTopLevel>, TTopLevel>,
        MultikeyedTreeLevel<TKey3, TValue, MultikeyedTreeLevel<TKey2, TKey3, TValue, MultikeyedTreeLevel<TKey1, TKey2, TKey3, TValue, TPrevious, TTopLevel>, TTopLevel>, TTopLevel>,
            TValue, TPrevious, TTopLevel>
        where TPrevious :
            IMultikeyedTreeLevel
        where TTopLevel :
            IMultikeyedTreeTopLevel
    {
        internal MultikeyedTreeLevel(TPrevious parent)
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


        #region IControlledStateMultikeyedTreeChildLevel Members

        IControlledStateMultikeyedTreeLevel IControlledStateMultikeyedTreeChildLevel.Previous
        {
            get { return this.Previous; }
        }

        #endregion

        #region IControlledStateMultikeyedTreeLevel Members

        public int Level
        {
            get { return Previous.Level + 1; }
        }

        IControlledStateMultikeyedTreeTopLevel IControlledStateMultikeyedTreeLevel.TopLevel
        {
            get { return this.TopLevel; }
        }

        #endregion

        #region IMultikeyedTreeChildLevel Members

        IMultikeyedTreeLevel IMultikeyedTreeChildLevel.Previous
        {
            get { return this.Previous; }
        }

        #endregion

        #region IMultikeyedTreeLevel Members

        IMultikeyedTreeTopLevel IMultikeyedTreeLevel.TopLevel
        {
            get { return this.TopLevel; }
        }

        #endregion
    }
    public class MultikeyedTreeLevel<TKey1, TKey2, TValue, TPrevious, TTopLevel> :
        ControlledStateDictionary<TKey1, MultikeyedTreeLevel<TKey2, TValue, MultikeyedTreeLevel<TKey1, TKey2, TValue, TPrevious, TTopLevel>, TTopLevel>>,
        IMultikeyedTreeLevel<
            TKey1, TKey2,
        MultikeyedTreeLevel<TKey1, TKey2, TValue, TPrevious, TTopLevel>,
        MultikeyedTreeLevel<TKey2, TValue, MultikeyedTreeLevel<TKey1, TKey2, TValue, TPrevious, TTopLevel>, TTopLevel>,
            TValue, TPrevious, TTopLevel>
        where TPrevious :
            IMultikeyedTreeLevel
        where TTopLevel :
            IMultikeyedTreeTopLevel
    {
        internal MultikeyedTreeLevel(TPrevious parent)
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


        #region IControlledStateMultikeyedTreeChildLevel Members

        IControlledStateMultikeyedTreeLevel IControlledStateMultikeyedTreeChildLevel.Previous
        {
            get { return this.Previous; }
        }

        #endregion

        #region IControlledStateMultikeyedTreeLevel Members

        public int Level
        {
            get { return Previous.Level + 1; }
        }

        IControlledStateMultikeyedTreeTopLevel IControlledStateMultikeyedTreeLevel.TopLevel
        {
            get { return this.TopLevel; }
        }

        #endregion

        #region IMultikeyedTreeChildLevel Members

        IMultikeyedTreeLevel IMultikeyedTreeChildLevel.Previous
        {
            get { return this.Previous; }
        }

        #endregion

        #region IMultikeyedTreeLevel Members

        IMultikeyedTreeTopLevel IMultikeyedTreeLevel.TopLevel
        {
            get { return this.TopLevel; }
        }

        #endregion
    }
    public class MultikeyedTreeLevel<TKey, TValue, TPrevious, TTopLevel> :
        ControlledStateDictionary<TKey, TValue>,
        IMultikeyedTreeLevel<TKey, TValue, TPrevious, TTopLevel>
        where TPrevious :
            IMultikeyedTreeLevel
        where TTopLevel :
            IMultikeyedTreeTopLevel
    {
        internal MultikeyedTreeLevel(TPrevious parent)
        {
            this.Previous = parent;
        }

        public TPrevious Previous { get; private set; }

        public TTopLevel TopLevel
        {
            get {
                if (Previous == null)
                    return default(TTopLevel);
                return (TTopLevel)Previous.TopLevel;
            }
        }


        #region IControlledStateMultikeyedTreeChildLevel Members

        IControlledStateMultikeyedTreeLevel IControlledStateMultikeyedTreeChildLevel.Previous
        {
            get { return this.Previous; }
        }

        #endregion

        #region IControlledStateMultikeyedTreeLevel Members

        public int Level
        {
            get { return Previous.Level + 1; }
        }

        IControlledStateMultikeyedTreeTopLevel IControlledStateMultikeyedTreeLevel.TopLevel
        {
            get { return this.TopLevel; }
        }

        #endregion

        #region IMultikeyedTreeChildLevel Members

        IMultikeyedTreeLevel IMultikeyedTreeChildLevel.Previous
        {
            get { return this.Previous; }
        }

        #endregion

        #region IMultikeyedTreeLevel Members

        IMultikeyedTreeTopLevel IMultikeyedTreeLevel.TopLevel
        {
            get { return this.TopLevel; }
        }

        #endregion
    }
}
