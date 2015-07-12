using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
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
        private class _ValuesCollection :
            ValuesCollection
        {
            internal ControlledDictionary<TDeclarationSpecific, TDeclarationSpecific> values;
            private _DeclarationsBase<TDeclarationIdentifier, TDeclaration, TDeclarationSpecificIdentifier, TDeclarationSpecific, TOriginalContainer, TDictionary> ParentTypes { get; set; }
            internal _ValuesCollection(_DeclarationsBase<TDeclarationIdentifier, TDeclaration, TDeclarationSpecificIdentifier, TDeclarationSpecific, TOriginalContainer, TDictionary> parentTypes)
                : base(parentTypes)
            {
                this.ParentTypes = parentTypes;
                this.values = new ControlledDictionary<TDeclarationSpecific, TDeclarationSpecific>();
            }

            public override bool Contains(TDeclarationSpecific value)
            {
                if (this.values.Values.Contains(value))
                    return true;
                foreach (var item in this.ParentTypes.Original.Values)
                {
                    if (CheckItemAt(item) && this.values[item] == value)
                        return true;
                }
                return false;
            }

            private bool CheckItemAt(TDeclarationSpecific item)
            {
                if (this.values.ContainsKey(item))
                    return false;
                else
                {
                    this.values._Add(item,this.ParentTypes.GetWrapper(item, this.ParentTypes.Parent));
                    item.Disposed += item_Disposed;
                    return true;
                }
            }

            void item_Disposed(object sender, EventArgs e)
            {
                if (!(sender is TDeclarationSpecific))
                    return;
                if (this.values.ContainsKey(((TDeclarationSpecific)(sender))))
                {
                    ((TDeclarationSpecific)(sender)).Disposed -= item_Disposed;
                    this.values[(TDeclarationSpecific)sender].Dispose();
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

            public override void CopyTo(TDeclarationSpecific[] array, int arrayIndex)
            {
                this.ToArray().CopyTo(array, arrayIndex);
            }

            protected override TDeclarationSpecific OnGetValue(int index)
            {
                if (index < 0 || index >= this.Count)
                    throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
                if (this.values.Count <= index)
                {
                    int oldCount = this.values.Count;
                    for (int i = oldCount; i <= index; i++)
                        this.CheckItemAt(this.ParentTypes.Original.Values[i]);
                }
                return this.values.Values[index];
            }

            public override TDeclarationSpecific[] ToArray()
            {
                return Enumerable.ToArray(this);
            }

            public override IEnumerator<TDeclarationSpecific> GetEnumerator()
            {
                foreach (var item in this.ParentTypes.Original.Values)
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
