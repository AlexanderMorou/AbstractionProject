using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    partial class SubordinateDictionary<TKey, TSValue, TMValue>
        where TMValue :
            class
        where TSValue :
            TMValue
    {
        private new class KeysCollection :
            ControlledStateDictionary<TKey, TSValue>.KeysCollection
        {
            private SubordinateDictionary<TKey, TSValue, TMValue> owner;
            public KeysCollection(SubordinateDictionary<TKey, TSValue, TMValue> owner)
                : base(owner)
            {
                this.owner = owner;
            }

            protected internal override void OnSetKey(int index, TKey value)
            {
                int masterIndex = this.owner.Master.Keys.IndexOf(base[index]);
                if (masterIndex > -1)
                    this.owner.Master.Keys[masterIndex] = value;
                base[index] = value;
            }

            public override IEnumerator<TKey> GetEnumerator()
            {
                return base.GetEnumerator();
            }

            public override void CopyTo(TKey[] array, int arrayIndex)
            {
                base.CopyTo(array, arrayIndex);
            }

            public override bool Contains(TKey item)
            {
                return base.Contains(item);
            }
        }
    }
}
