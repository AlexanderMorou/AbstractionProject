using System.Collections.Generic;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    partial class LockedGroupedDeclarationsBase<TItem, TMItem, TSourceItem>
    {
        private class _KeysCollection :
            ControlledStateDictionary<string, TItem>.KeysCollection
        {
            private LockedGroupedDeclarationsBase<TItem, TMItem, TSourceItem> source;
            private string[] dataCopy;
            public _KeysCollection(LockedGroupedDeclarationsBase<TItem, TMItem, TSourceItem> source)
                : base(source)
            {
                this.source = source;
                this.dataCopy = new string[source.sourceData.Length];
            }
      
            protected override string OnGetKey(int index)
            {
                if (this.dataCopy[index] == null)
                    this.dataCopy[index] = this.source.FetchKey(source.sourceData[index]);
                return this.dataCopy[index];
            }

            public override int Count
            {
                get
                {
                    return this.dataCopy.Length;
                }
            }

            public override IEnumerator<string> GetEnumerator()
            {
                for (int i = 0; i < this.dataCopy.Length; i++)
                {
                    if (this.dataCopy[i] == null)
                        this.dataCopy[i] = this.source.FetchKey(source.sourceData[i]);
                    yield return this.dataCopy[i];
                }
                yield break;
            }

            public override string[] ToArray()
            {
                for (int i = 0; i < this.dataCopy.Length; i++)
                    if (this.dataCopy[i] == null)
                        this.dataCopy[i] = this.source.FetchKey(source.sourceData[i]);
                string[] dc = new string[this.dataCopy.Length];
                this.dataCopy.CopyTo(dc, 0);
                return dc;
            }

            public override bool Contains(string item)
            {
                bool containsUnloaded = false;
                foreach (string s in this.dataCopy)
                    if (s == null)
                        containsUnloaded = true;
                    else if (s == item)
                        return true;
                if (containsUnloaded)
                    for (int i = 0; i < this.dataCopy.Length; i++)
                        if (this.dataCopy[i] == null)
                        {
                            string r = this.source.FetchKey(source.sourceData[i]);
                            this.dataCopy[i] = r;
                            if (r == item)
                                return true;
                        }
                return false;
            }

            public new int IndexOf(string key)
            {
                int index = 0;
                bool containsUnloaded = false;
                foreach (string s in this.dataCopy)
                {
                    if (s == null)
                        containsUnloaded = true;
                    else if (s == key)
                        return index;
                    index++;
                }
                if (containsUnloaded)
                {
                    for (int i = 0; i < this.dataCopy.Length; i++)
                        if (this.dataCopy[i] == null)
                        {
                            string r = this.source.FetchKey(source.sourceData[i]);
                            this.dataCopy[i] = r;
                            if (r == key)
                                return i;
                        }
                }
                return -1;
            }

            internal void Dispose()
            {
                this.dataCopy = null;
                this.source = null;
            }
        }
    }
}
