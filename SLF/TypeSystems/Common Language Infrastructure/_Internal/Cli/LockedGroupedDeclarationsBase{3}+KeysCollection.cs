using System.Collections.Generic;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class LockedGroupedDeclarationsBase<TItemIdentifier, TItem, TMItemIdentifier, TMItem, TSourceItem>
    {
        private class _KeysCollection :
            KeysCollection
        {
            private LockedGroupedDeclarationsBase<TItemIdentifier, TItem, TMItemIdentifier, TMItem, TSourceItem> source;
            private TItemIdentifier[] dataCopy;
            public _KeysCollection(LockedGroupedDeclarationsBase<TItemIdentifier, TItem, TMItemIdentifier, TMItem, TSourceItem> source)
                : base(source)
            {
                this.source = source;
                this.dataCopy = new TItemIdentifier[source.sourceData.Length];
            }
      
            protected override TItemIdentifier OnGetKey(int index)
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

            public override IEnumerator<TItemIdentifier> GetEnumerator()
            {
                for (int i = 0; i < this.dataCopy.Length; i++)
                {
                    if (this.dataCopy[i] == null)
                        this.dataCopy[i] = this.source.FetchKey(source.sourceData[i]);
                    yield return this.dataCopy[i];
                }
                yield break;
            }

            public override TItemIdentifier[] ToArray()
            {
                for (int i = 0; i < this.dataCopy.Length; i++)
                    if (this.dataCopy[i] == null)
                        this.dataCopy[i] = this.source.FetchKey(source.sourceData[i]);
                TItemIdentifier[] dc = new TItemIdentifier[this.dataCopy.Length];
                this.dataCopy.CopyTo(dc, 0);
                return dc;
            }

            public override bool Contains(TItemIdentifier item)
            {
                bool containsUnloaded = false;
                foreach (TItemIdentifier s in this.dataCopy)
                    if (s == null)
                        containsUnloaded = true;
                    else if (s.Equals(item))
                        return true;
                if (containsUnloaded)
                    for (int i = 0; i < this.dataCopy.Length; i++)
                        if (this.dataCopy[i] == null)
                        {
                            TItemIdentifier r = this.source.FetchKey(source.sourceData[i]);
                            this.dataCopy[i] = r;
                            if (r.Equals(item))
                                return true;
                        }
                return false;
            }

            public new int IndexOf(TItemIdentifier key)
            {
                int index = 0;
                bool containsUnloaded = false;
                foreach (TItemIdentifier s in this.dataCopy)
                {
                    if (s == null)
                        containsUnloaded = true;
                    else if (s.Equals(key))
                        return index;
                    index++;
                }
                /* *
                 * Iterate through the elements twice, the first
                 * check is to perform a light check of the elements
                 * in case the item is already present; the second check
                 * is to load those not present as of yet.
                 * */
                if (containsUnloaded)
                    for (int i = 0; i < this.dataCopy.Length; i++)
                        /* *
                         * If the element isn't null,
                         * it was checked above.
                         * */
                        if (this.dataCopy[i] == null)
                        {
                            TItemIdentifier r = this.source.FetchKey(source.sourceData[i]);
                            this.dataCopy[i] = r;
                            if (r.Equals(key))
                                return i;
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
