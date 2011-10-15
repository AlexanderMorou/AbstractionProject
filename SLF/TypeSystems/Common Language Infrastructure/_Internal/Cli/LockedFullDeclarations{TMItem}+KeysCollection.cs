using System.Collections.Generic;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class LockedFullDeclarations<TMItemIdentifier, TMItem>
    {
        private class _KeysCollection :
            KeysCollection
        {
            private LockedFullDeclarations<TMItemIdentifier, TMItem> owner;
            private TMItemIdentifier[] dataCopy;
            public _KeysCollection(LockedFullDeclarations<TMItemIdentifier, TMItem> source)
                : base(source)
            {
                this.owner = source;
                this.dataCopy = new TMItemIdentifier[source.sourceData.Count];
            }

            protected override TMItemIdentifier OnGetKey(int index)
            {
                if (this.dataCopy[index] == null)
                    this.dataCopy[index] = this.owner.FetchKey(owner.sourceData[index]);
                return this.dataCopy[index];
            }

#if DEBUG
            /// <summary>
            /// Sets the <paramref name="value"/> at the
            /// <paramref name="index"/> provided.
            /// </summary>
            /// <param name="index">The <see cref="Int32"/> that represents the index of
            /// the element to set.</param>
            /// <param name="value">The <see cref="String"/> value to set it to.</param>
            /// <exception cref="System.NotSupportedException">thrown always, not supported
            /// in a locked declaration set.</exception>
#endif
            protected internal override sealed void OnSetKey(int index, TMItemIdentifier value)
            {
                throw new NotSupportedException();
            }

            public override int Count
            {
                get
                {
                    return this.dataCopy.Length;
                }
            }

            public override IEnumerator<TMItemIdentifier> GetEnumerator()
            {
                for (int i = 0; i < this.dataCopy.Length; i++)
                {
                    if (this.dataCopy[i] == null)
                        this.dataCopy[i] = this.owner.FetchKey(owner.sourceData[i]);
                    yield return this.dataCopy[i];
                }
                yield break;
            }

            public override TMItemIdentifier[] ToArray()
            {
                for (int i = 0; i < this.dataCopy.Length; i++)
                    if (this.dataCopy[i] == null)
                        this.dataCopy[i] = this.owner.FetchKey(owner.sourceData[i]);
                TMItemIdentifier[] dc = new TMItemIdentifier[this.dataCopy.Length];
                this.dataCopy.CopyTo(dc, 0);
                return dc;
            }
            
            public override bool Contains(TMItemIdentifier item)
            {
                bool containsUnloaded = false;
                foreach (TMItemIdentifier s in this.dataCopy)
                    if (s == null)
                    {
                        if (!containsUnloaded)
                            containsUnloaded = true;
                    }
                    else if (s.Equals(item))
                        return true;
                if (containsUnloaded)
                    for (int i = 0; i < this.dataCopy.Length; i++)
                        if (this.dataCopy[i] == null)
                        {
                            TMItemIdentifier r = this.owner.FetchKey(owner.sourceData[i]);
                            this.dataCopy[i] = r;
                            if (r.Equals(item))
                                return true;
                        }
                return false;
            }

            public new int IndexOf(TMItemIdentifier key)
            {
                int index = 0;
                bool containsUnloaded = false;
                foreach (TMItemIdentifier s in this.dataCopy)
                {
                    if (s == null)
                    {
                        if (!containsUnloaded)
                            containsUnloaded = true;
                    }
                    else if (s.Equals(key))
                        return index;
                    index++;
                }
                if (containsUnloaded)
                {
                    for (int i = 0; i < this.dataCopy.Length; i++)
                        if (this.dataCopy[i] == null)
                        {
                            TMItemIdentifier r = this.owner.FetchKey(owner.sourceData[i]);
                            this.dataCopy[i] = r;
                            if (r.Equals(key))
                                return i;
                        }
                }
                return -1;
            }

            internal void Dispose()
            {
                this.dataCopy = null;
                this.owner = null;
            }

            internal void SetRange(int p)
            {
                TMItemIdentifier[] r = new TMItemIdentifier[p];
                int l = p > this.Count ? this.Count : p;
                for (int i = 0; i < l; i++)
                    r[i] = this.dataCopy[i];
                this.dataCopy = r;
            }

        }
    }
}
