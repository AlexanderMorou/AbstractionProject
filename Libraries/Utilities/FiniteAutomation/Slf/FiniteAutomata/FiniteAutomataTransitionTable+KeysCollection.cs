﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.FiniteAutomata
{
    partial class FiniteAutomataTransitionTable<TCheck, TState, TNodeTarget>
    {
        /// <summary>
        /// Provides a key collection for the transition table.
        /// </summary>
        protected class KeysCollection :
            IControlledCollection<TCheck>
        {
            private FiniteAutomataTransitionTable<TCheck, TState, TNodeTarget> owner;
            public KeysCollection(FiniteAutomataTransitionTable<TCheck, TState, TNodeTarget> owner)
            {
                this.owner = owner;
            }

            #region IControlledCollection<TCheck> Members

            public int Count
            {
                get { return this.owner.backup.Count; }
            }

            public bool Contains(TCheck item)
            {
                return this.owner.backup.ContainsKey(item);
            }

            public void CopyTo(TCheck[] array, int arrayIndex = 0)
            {
                this.owner.backup.Keys.CopyTo(array, arrayIndex);
            }

            public TCheck this[int index]
            {
                get {
                    if (index < 0 || index >= this.Count)
                        throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
                    int i = 0;
                    foreach (var key in this.owner.backup.Keys)
                        if (i++ == index)
                            return key;
                    return default(TCheck);
                }
            }

            public TCheck[] ToArray()
            {
                TCheck[] result = new TCheck[this.Count];
                this.CopyTo(result, 0);
                return result;
            }

            #endregion

            #region IEnumerable<TCheck> Members

            public IEnumerator<TCheck> GetEnumerator()
            {
                return this.owner.backup.Keys.GetEnumerator();
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion

            #region IControlledCollection<TCheck> Members


            public int IndexOf(TCheck element)
            {
                int index = -1;
                int i = 0;
                foreach (var currentElement in this.owner.backup.Keys)
                    if (currentElement.Equals(element))
                    {
                        index = i;
                        break;
                    }
                    else
                        i++;
                return index;
            }

            #endregion
        }
    }
}
