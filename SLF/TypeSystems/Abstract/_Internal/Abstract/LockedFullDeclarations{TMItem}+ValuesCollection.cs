﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections.ObjectModel;
using System.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    partial class LockedFullDeclarations<TMItem>
    {
        private class _ValuesCollection :
            ICollection<MasterDictionaryEntry<TMItem>>
        {
            private LockedFullDeclarations<TMItem> source;
            private List<MasterDictionaryEntry<TMItem>?> dataCopy;
            public _ValuesCollection(LockedFullDeclarations<TMItem> source)
            {
                this.source = source;
                this.dataCopy = new List<MasterDictionaryEntry<TMItem>?>(source.sourceData.Count);
                for (int i = 0; i < this.source.sourceData.Count; i++)
                    this.dataCopy.Add(null);
            }
            public MasterDictionaryEntry<TMItem> this[int index]
            {
                get
                {
                    if (this.dataCopy[index] == null)
                        this.dataCopy[index] = this.source.Fetch(source.sourceData[index]);
                    return this.dataCopy[index].Value;
                }
            }
            public int Count
            {
                get
                {
                    return this.dataCopy.Count;
                }
            }

            public IEnumerator<MasterDictionaryEntry<TMItem>> GetEnumerator()
            {
                for (int i = 0; i < this.dataCopy.Capacity; i++)
                {
                    if (this.dataCopy[i] == null)
                        this.dataCopy[i] = this.source.Fetch(source.sourceData[i]);
                    yield return this.dataCopy[i].Value;
                }
                yield break;
            }

            public MasterDictionaryEntry<TMItem>[] ToArray()
            {
                for (int i = 0; i < this.dataCopy.Count; i++)
                    if (this.dataCopy[i] == null)
                        this.dataCopy[i] = this.source.Fetch(source.sourceData[i]);
                MasterDictionaryEntry<TMItem>[] dc = new MasterDictionaryEntry<TMItem>[this.dataCopy.Count];
                for (int i = 0; i < this.dataCopy.Count; i++)
                    dc[i] = this.dataCopy[i].Value;
                return dc;
            }

            public bool Contains(MasterDictionaryEntry<TMItem> item)
            {
                bool containsUnloaded = false;
                foreach (var s in this.dataCopy)
                    if (s == null)
                        containsUnloaded = true;
                    else if (s == item)
                        return true;
                if (containsUnloaded)
                    for (int i = 0; i < this.dataCopy.Count; i++)
                        if (dataCopy[i] == null)
                        {
                            MasterDictionaryEntry<TMItem> r = this.source.Fetch(source.sourceData[i]);
                            this.dataCopy[i] = r;
                            if (object.ReferenceEquals(r, item))
                                return true;
                        }
                return false;
            }

            internal void Dispose()
            {
                for (int i = 0; i < this.dataCopy.Count; i++)
                    if (!object.ReferenceEquals(this.dataCopy[i], null))
                    {
                        this.dataCopy[i].Value.Entry.Dispose();
                        this.dataCopy[i] = null;
                    }
                this.dataCopy = null;
                this.source = null;
            }

            internal void SetRange(int p)
            {

                int d = p > this.dataCopy.Count ? p - this.dataCopy.Count : this.dataCopy.Count - p;
                if (d > 0)
                    for (int i = 0; i < d; i++)
                        this.dataCopy.Add(null);
                else
                {
                    int lI = this.Count+d;
                    for (int i = 0; i < -d; i++)
                        this.dataCopy.RemoveAt(lI);
                }
                this.dataCopy.Capacity = p;
            }

            #region ICollection<MasterDictionaryEntry<TMItem>> Members

            public void Add(MasterDictionaryEntry<TMItem> item)
            {
                throw new NotSupportedException();
            }

            public void Clear()
            {
                throw new NotSupportedException();
            }

            public void CopyTo(MasterDictionaryEntry<TMItem>[] array, int arrayIndex)
            {
                for (int i = 0; i < this.dataCopy.Count; i++)
                    if (this.dataCopy[i] == null)
                        array[i + arrayIndex] = (this.dataCopy[i] = this.source.Fetch(source.sourceData[i])).Value;
                    else
                        array[i + arrayIndex] = this.dataCopy[i].Value;
            }

            public bool IsReadOnly
            {
                get { return true; }
            }

            public bool Remove(MasterDictionaryEntry<TMItem> item)
            {
                throw new NotSupportedException();
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