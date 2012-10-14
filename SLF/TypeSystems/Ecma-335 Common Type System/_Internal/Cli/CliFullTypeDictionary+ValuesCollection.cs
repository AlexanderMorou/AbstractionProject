using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CliFullTypeDictionary
    {
        private class ValuesCollection :
            IControlledCollection<MasterDictionaryEntry<IType>>
        {
            private CliFullTypeDictionary owner;

            public ValuesCollection(CliFullTypeDictionary owner)
            {
                this.owner = owner;
            }

            #region IControlledCollection<IType> Members

            public int Count
            {
                get { return this.owner.Count; }
            }

            public bool Contains(MasterDictionaryEntry<IType> item)
            {
                lock (this.owner.syncObject)
                {
                    int lastNull = -1;
                    for (int datumIndex = 0; datumIndex < this.Count; datumIndex++)
                    {
                        var currentType = this.owner.resultTypes[datumIndex];
                        if (currentType != null)
                        {
                            if (currentType.Equals(item.Entry))
                                return true;
                        }
                        else if (lastNull == -1)
                            lastNull = datumIndex;
                    }
                    if (lastNull == -1)
                        return false;
                    for (int datumIndex = lastNull; datumIndex < this.Count; datumIndex++)
                    {
                        var currentType = this.owner.resultTypes[datumIndex];
                        if (currentType != null)
                            continue;
                        this.owner.CheckItemAt(datumIndex);
                        if (this.owner.resultTypes[datumIndex].Equals(item.Entry))
                            return true;
                    }
                    return false;
                }
            }

            public void CopyTo(MasterDictionaryEntry<IType>[] array, int arrayIndex = 0)
            {
                ThrowHelper.CopyToCheck(array, arrayIndex, this.owner.Count);
                lock (this.owner.syncObject)
                {
                    for (int datumIndex = 0; datumIndex < this.owner.Count; datumIndex++)
                    {
                        this.owner.CheckItemAt(datumIndex);
                        array[arrayIndex + datumIndex] = new MasterDictionaryEntry<IType>(this.owner.GetSubordinate(this.owner.resultKinds[datumIndex]), this.owner.resultTypes[datumIndex]);
                    }
                }
            }

            public MasterDictionaryEntry<IType> this[int index]
            {
                get
                {
                    if (index < 0 || index >= this.owner.Count)
                        throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
                    lock (this.owner.syncObject)
                    {
                        this.owner.CheckItemAt(index);
                        return new MasterDictionaryEntry<IType>(this.owner.GetSubordinate(this.owner.resultKinds[index]), this.owner.resultTypes[index]);
                    }
                }
            }

            public MasterDictionaryEntry<IType>[] ToArray()
            {
                MasterDictionaryEntry<IType>[] result = new MasterDictionaryEntry<IType>[this.owner.Count];
                this.CopyTo(result);
                return result;
            }

            public int IndexOf(MasterDictionaryEntry<IType> element)
            {
                lock (this.owner.syncObject)
                {
                    int lastNull = -1;
                    for (int datumIndex = 0; datumIndex < this.Count; datumIndex++)
                    {
                        var currentType = this.owner.resultTypes[datumIndex];
                        if (currentType != null)
                        {
                            if (currentType.Equals(element.Entry))
                                return datumIndex;
                        }
                        else if (lastNull == -1)
                            lastNull = datumIndex;
                    }
                    if (lastNull == -1)
                        return -1;
                    for (int datumIndex = lastNull; datumIndex < this.Count; datumIndex++)
                    {
                        var currentType = this.owner.resultTypes[datumIndex];
                        if (currentType != null)
                            continue;
                        this.owner.CheckItemAt(datumIndex);
                        if (this.owner.resultTypes[datumIndex].Equals(element.Entry))
                            return datumIndex;
                    }
                    return -1;
                }
            }

            #endregion

            #region IEnumerable<IType> Members

            public IEnumerator<MasterDictionaryEntry<IType>> GetEnumerator()
            {
                for (int datumIndex = 0; datumIndex < this.owner.Count; datumIndex++)
                {
                    IType current = null;
                    TypeKind kind;
                    /* *
                     * States of the iterator should hit this, lock, and then unlock
                     * the sync object.  Needs done for each iteration.
                     * Preferred method vs arbitrarily long locks over the syncObject
                     * for the resulted iterator.
                     * */
                    lock (this.owner.syncObject)
                    {
                        current = this.owner.resultTypes[datumIndex];
                        if (current == null)
                            current = this.owner.ConstructItemAt(datumIndex);
                        kind = this.owner.resultKinds[datumIndex];
                    }
                    yield return new MasterDictionaryEntry<IType>(this.owner.GetSubordinate(kind), current);
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
