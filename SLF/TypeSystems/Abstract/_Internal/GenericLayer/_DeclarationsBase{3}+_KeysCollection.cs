using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    partial class _DeclarationsBase<TDeclarationIdentifier, TDeclaration, TDeclarationSpecificIdentifier, TDeclarationSpecific, TOriginalContainer, TDictionary>
        where TDeclarationIdentifier :
            IDeclarationUniqueIdentifier
        where TDeclaration :
            class,
            IDeclaration
        where TDeclarationSpecificIdentifier :
            IDeclarationUniqueIdentifier,
            TDeclarationIdentifier
        where TDeclarationSpecific :
            class,
            IDeclaration<TDeclarationSpecificIdentifier>,
            TDeclaration
        where TDictionary :
            class,
            IDeclarationDictionary<TDeclarationSpecificIdentifier, TDeclarationSpecific>
    {
        private class _KeysCollection :
            KeysCollection
        {
            private _DeclarationsBase<TDeclarationIdentifier, TDeclaration, TDeclarationSpecificIdentifier, TDeclarationSpecific, TOriginalContainer, TDictionary> ParentTypes { get; set; }
            private ControlledDictionary<TDeclarationSpecific, TDeclarationSpecificIdentifier> values;
            internal _KeysCollection(_DeclarationsBase<TDeclarationIdentifier, TDeclaration, TDeclarationSpecificIdentifier, TDeclarationSpecific, TOriginalContainer, TDictionary> parentTypes)
                : base(parentTypes)
            {
                this.values = new ControlledDictionary<TDeclarationSpecific, TDeclarationSpecificIdentifier>();
                this.ParentTypes = parentTypes;
            }

            public override bool Contains(TDeclarationSpecificIdentifier key)
            {
                foreach (var item in this.ParentTypes.Values)
                {
                    CheckItemAt(item);
                    if (item.UniqueIdentifier.Equals(key))
                        return true;
                }
                return false;
            }

            private void CheckItemAt(TDeclarationSpecific item)
            {
                if (!this.values.ContainsKey(item))
                {
                    item.Disposed += item_Disposed;
                    this.values._Add(item, this.ParentTypes.Values[this.values.Count].UniqueIdentifier);
                }
            }

            void item_Disposed(object sender, EventArgs e)
            {
                if (!(sender is TDeclarationSpecific))
                    return;
                if (this.values.ContainsKey(((TDeclarationSpecific)(sender))))
                {
                    ((TDeclarationSpecific)(sender)).Disposed -= item_Disposed;
                    this.values._Remove(((TDeclarationSpecific)(sender)));
                }
            }

            public override int Count
            {
                get
                {
                    return this.ParentTypes.Count;
                }
            }

            public override int IndexOf(TDeclarationSpecificIdentifier key)
            {
                int index = 0;
                foreach (var element in this.values.Values)
                    if (element.Equals(key))
                        return index;
                    else
                        index++;
                return -1;
            }

            public override void CopyTo(TDeclarationSpecificIdentifier[] array, int arrayIndex)
            {
                this.ToArray().CopyTo(array, arrayIndex);
            }

            protected override TDeclarationSpecificIdentifier OnGetKey(int index)
            {
                if (index < 0 || index >= this.Count)
                    throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
                if (this.values.Count <= index)
                {
                    int oldCount = this.values.Count;
                    for (int i = oldCount; i <= index; i++)
                        this.CheckItemAt(this.ParentTypes.Values[i]);
                }
                return this.values.Values[index];
            }

            public override TDeclarationSpecificIdentifier[] ToArray()
            {
                return Enumerable.ToArray(this);
            }

            public override IEnumerator<TDeclarationSpecificIdentifier> GetEnumerator()
            {
                foreach (var item in this.ParentTypes.Values)
                {
                    this.CheckItemAt(item);
                    yield return this.values[item];
                }
                yield break;
            }

            internal void Dispose()
            {
                this.values._Clear();
            }
        }

    }
}
