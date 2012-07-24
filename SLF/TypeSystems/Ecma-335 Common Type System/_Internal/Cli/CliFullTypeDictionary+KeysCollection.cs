using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CliFullTypeDictionary
    {
        private class KeysCollection :
            IControlledCollection<IGeneralTypeUniqueIdentifier>
        {
            private CliFullTypeDictionary owner;

            public KeysCollection(CliFullTypeDictionary owner)
            {
                this.owner = owner;
            }

            #region IControlledCollection<IGeneralTypeUniqueIdentifier> Members

            public int Count
            {
                get { return this.owner.Count; }
            }

            public bool Contains(IGeneralTypeUniqueIdentifier item)
            {
                lock (this.owner.syncObject)
                {
                    int lastNull = -1;
                    for (int datumIndex = 0; datumIndex < this.Count; datumIndex++)
                    {
                        var currentKey = this.owner.resultKeys[datumIndex];
                        if (currentKey != null)
                        {
                            if (currentKey.Equals(item))
                                return true;
                        }
                        else if (lastNull == -1)
                            lastNull = datumIndex;
                    }
                    if (lastNull == -1)
                        return false;
                    for (int datumIndex = lastNull; datumIndex < this.Count; datumIndex++)
                    {
                        var currentKey = this.owner.resultKeys[datumIndex];
                        if (currentKey != null)
                            continue;
                        this.owner.CheckKindAt(datumIndex);
                        if (this.owner.resultKeys[datumIndex].Equals(item))
                            return true;
                    }
                    return false;
                }
            }

            public void CopyTo(IGeneralTypeUniqueIdentifier[] array, int arrayIndex = 0)
            {
                ThrowHelper.CopyToCheck(array, arrayIndex, this.owner.Count);
                for (int datumIndex = 0; datumIndex < this.owner.Count; datumIndex++)
                    this.owner.CheckKindAt(datumIndex);
                this.owner.resultKeys.CopyTo(array, arrayIndex);
            }

            public IGeneralTypeUniqueIdentifier this[int index]
            {
                get
                {
                    if (index < 0 || index >= this.owner.Count)
                        throw new ArgumentOutOfRangeException("index");
                    this.owner.CheckKindAt(index);
                    return this.owner.resultKeys[index];
                }
            }

            public IGeneralTypeUniqueIdentifier[] ToArray()
            {
                IGeneralTypeUniqueIdentifier[] result = new IGeneralTypeUniqueIdentifier[this.owner.Count];
                this.CopyTo(result);
                return result;
            }

            public int IndexOf(IGeneralTypeUniqueIdentifier element)
            {
                lock (this.owner.syncObject)
                {
                    int lastNull = -1;
                    for (int datumIndex = 0; datumIndex < this.Count; datumIndex++)
                    {
                        var currentKey = this.owner.resultKeys[datumIndex];
                        if (currentKey != null)
                        {
                            if (currentKey.Equals(element))
                                return datumIndex;
                        }
                        else if (lastNull == -1)
                            lastNull = datumIndex;
                    }
                    if (lastNull == -1)
                        return -1;
                    for (int datumIndex = lastNull; datumIndex < this.Count; datumIndex++)
                    {
                        var currentKey = this.owner.resultKeys[datumIndex];
                        if (currentKey != null)
                            continue;
                        this.owner.CheckKindAt(datumIndex);
                        if (this.owner.resultKeys[datumIndex].Equals(element))
                            return datumIndex;
                    }
                    return -1;
                }
            }

            #endregion

            #region IEnumerable<IGeneralTypeUniqueIdentifier> Members

            public IEnumerator<IGeneralTypeUniqueIdentifier> GetEnumerator()
            {
                for (int datumIndex = 0; datumIndex < this.owner.Count; datumIndex++)
                {
                    IGeneralTypeUniqueIdentifier key = null;
                    lock (this.owner.syncObject)
                    {
                        key = this.owner.resultKeys[datumIndex];
                        if (key == null)
                        {
                            this.owner.CheckKindAt(datumIndex);
                            key = this.owner.resultKeys[datumIndex];
                        }
                    }
                    yield return key;
                }
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
