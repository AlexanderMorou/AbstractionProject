﻿using System.Collections.Generic;
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
        private class _ValuesCollection :
            ValuesCollection
        {
            private LockedGroupedDeclarationsBase<TItemIdentifier, TItem, TMItemIdentifier, TMItem, TSourceItem> source;
            internal TItem[] dataCopy;
            public _ValuesCollection(LockedGroupedDeclarationsBase<TItemIdentifier, TItem, TMItemIdentifier, TMItem, TSourceItem> source)
                : base(source)
            {
                this.source = source;
                this.dataCopy = new TItem[source.sourceData.Length];
            }

            protected override TItem OnGetValue(int index)
            {
                if (this.dataCopy[index] == null)
                    this.dataCopy[index] = this.source.Fetch(source.sourceData[index]);
                return this.dataCopy[index];
            }

            public override int Count
            {
                get
                {
                    return this.dataCopy.Length;
                }
            }

            public override IEnumerator<TItem> GetEnumerator()
            {
                for (int i = 0; i < this.dataCopy.Length; i++)
                {
                    if (this.dataCopy[i] == null)
                        this.dataCopy[i] = this.source.Fetch(source.sourceData[i]);
                    yield return this.dataCopy[i];
                }
                yield break;
            }

            public override TItem[] ToArray()
            {
                for (int i = 0; i < this.dataCopy.Length; i++)
                    if (this.dataCopy[i] == null)
                        this.dataCopy[i] = this.source.Fetch(source.sourceData[i]);
                TItem[] dc = new TItem[this.dataCopy.Length];
                this.dataCopy.CopyTo(dc, 0);
                return dc;
            }

            public override bool Contains(TItem item)
            {
                foreach (TItem s in this.dataCopy)
                    if (object.ReferenceEquals(s, null))
                        continue;
                    else if (object.ReferenceEquals(s, item))
                        return true;
                /* *
                 * Unlike the string keys, the values are 
                 * instantiated by this implementation, thus
                 * they are not able to be passed in as an instance
                 * since it would have not been created
                 * yet should the above test fail.
                 * */
                return false;
            }


            internal void Dispose()
            {
                for (int i = 0; i < this.dataCopy.Length; i++)
                    if (this.dataCopy[i] != null)
                    {
                        this.dataCopy[i].Dispose();
                        this.dataCopy[i] = null;
                    }
                this.dataCopy = null;
                this.source = null;
            }
        }
    }
}