using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    partial class _GroupedMasterBase<TDeclaration>
    {
        private class _DictionaryEnumerator :
            IDictionaryEnumerator
        {
            private _GroupedMasterBase<TDeclaration> master;
            private IEnumerator<KeyValuePair<string, MasterDictionaryEntry<TDeclaration>>> enumerator;
            public _DictionaryEnumerator(_GroupedMasterBase<TDeclaration> master)
            {
                this.master = master;
            }

            #region IDictionaryEnumerator Members

            public DictionaryEntry Entry
            {
                get {
                    if (this.enumerator == null)
                        return default(DictionaryEntry);
                    return new DictionaryEntry(this.Key, this.Value);
                }
            }

            public object Key
            {
                get {
                    if (this.enumerator == null)
                        return null;
                    return this.enumerator.Current.Key;
                }
            }

            public object Value
            {
                get
                {
                    if (this.enumerator == null)
                        return null;
                    return this.enumerator.Current.Value;
                }
            }

            #endregion

            #region IEnumerator Members

            public object Current
            {
                get { return this.enumerator.Current; }
            }

            public bool MoveNext()
            {
                if (this.enumerator == null)
                    this.enumerator = this.master.GetEnumerator();
                return this.enumerator.MoveNext();
            }

            public void Reset()
            {
                if (this.enumerator != null)
                {
                    enumerator.Dispose();
                    this.enumerator = null;
                }
            }

            #endregion
        }
    }
}
