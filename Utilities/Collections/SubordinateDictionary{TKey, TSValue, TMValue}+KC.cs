using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            public override TKey this[int index]
            {
                get
                {
                    this.owner.RefreshCheck();
                    return base[index];
                }
                internal protected set
                {
                    base[index] = value;
                }
            }

            public override IEnumerator<TKey> GetEnumerator()
            {
                this.owner.RefreshCheck();
                return base.GetEnumerator();
            }

            public override void CopyTo(TKey[] array, int arrayIndex)
            {
                this.owner.RefreshCheck();
                base.CopyTo(array, arrayIndex);
            }

            public override bool Contains(TKey item)
            {
                this.owner.RefreshCheck();
                return base.Contains(item);
            }
        }
    }
}
