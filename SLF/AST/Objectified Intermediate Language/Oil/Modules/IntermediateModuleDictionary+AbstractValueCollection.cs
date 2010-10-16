using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using System.Collections;

namespace AllenCopeland.Abstraction.Slf.Oil.Modules
{
    partial class IntermediateModuleDictionary
    {
        private sealed class AbstractValueCollection :
            IControlledStateCollection<IModule>
        {
            private IntermediateModuleDictionary owner;

            public AbstractValueCollection(IntermediateModuleDictionary owner)
            {
                this.owner = owner;
            }

            #region IControlledStateCollection<IModule> Members

            public int Count
            {
                get { return this.owner.Count; }
            }

            public bool Contains(IModule item)
            {
                if (item is IIntermediateModule)
                    return this.owner.Values.Contains(((IIntermediateModule)(item)));
                return false;
            }

            public void CopyTo(IModule[] array, int arrayIndex = 0)
            {
                if (arrayIndex < 0 || arrayIndex >= array.Length)
                    throw new ArgumentOutOfRangeException("arrayIndex");
                if (this.Count + arrayIndex > array.Length)
                    throw new ArgumentException("array");
                for (int i = 0; i < this.Count; i++)
                    array[i+arrayIndex] = this.owner.Values[i];
            }

            public IModule this[int index]
            {
                get { return this.owner.Values[index]; }
            }

            public IModule[] ToArray()
            {
                IModule[] result = new IModule[this.Count];
                this.CopyTo(result);
                return result;
            }

            public int IndexOf(IModule element)
            {
                if (element is IIntermediateModule)
                    return this.owner.Values.IndexOf((IIntermediateModule)element);
                return -1;
            }

            #endregion

            #region IEnumerable<IModule> Members

            public IEnumerator<IModule> GetEnumerator()
            {
                foreach (var module in this.owner.Values)
                    yield return module;
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion
        }
    }
}
