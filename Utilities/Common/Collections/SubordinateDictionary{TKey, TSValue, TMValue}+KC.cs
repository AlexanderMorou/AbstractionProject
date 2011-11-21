using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    partial class SubordinateDictionary<TSKey, TMKey, TSValue, TMValue>
        where TSKey :
            TMKey
        where TMValue :
            class
        where TSValue :
            TMValue
    {
        private new class KeysCollection :
            ControlledStateDictionary<TSKey, TSValue>.KeysCollection
        {
            private SubordinateDictionary<TSKey, TMKey, TSValue, TMValue> owner;
            public KeysCollection(SubordinateDictionary<TSKey, TMKey, TSValue, TMValue> owner)
                : base(owner)
            {
                this.owner = owner;
            }

            protected internal override void OnSetKey(int index, TSKey value)
            {
                int masterIndex = this.owner.Master.Keys.IndexOf(base[index]);
                if (masterIndex > -1)
                    this.owner.Master.Keys[masterIndex] = value;
                base.OnSetKey(index, value);
            }

            public override IEnumerator<TSKey> GetEnumerator()
            {
                return base.GetEnumerator();
            }

            public override void CopyTo(TSKey[] array, int arrayIndex)
            {
                base.CopyTo(array, arrayIndex);
            }

            public override bool Contains(TSKey item)
            {
                return base.Contains(item);
            }
        }
    }
}
