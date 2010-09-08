using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    partial class _DeclarationsBase<TDeclaration, TDeclarationSpecific, TOriginalContainer, TDictionary>
        where TDeclaration :
            class,
            IDeclaration
        where TDeclarationSpecific :
            class,
            TDeclaration
        where TDictionary :
            class,
            IDeclarationDictionary<TDeclarationSpecific>
    {
        private class _ValuesCollection :
            ValuesCollection
        {
            internal Dictionary<TDeclarationSpecific, TDeclarationSpecific> values;
            private _DeclarationsBase<TDeclaration, TDeclarationSpecific, TOriginalContainer, TDictionary> ParentTypes { get; set; }
            internal _ValuesCollection(_DeclarationsBase<TDeclaration, TDeclarationSpecific, TOriginalContainer, TDictionary> parentTypes)
                : base(parentTypes)
            {
                this.ParentTypes = parentTypes;
                this.values = new Dictionary<TDeclarationSpecific, TDeclarationSpecific>();
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
                    this.values.Add(item,this.ParentTypes.GetWrapper(item, this.ParentTypes.Parent));
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
                    this.values.Remove(((TDeclarationSpecific)(sender)));
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

            protected override TDeclarationSpecific OnGetThis(int index)
            {
                if (index < 0 || index >= this.Count)
                    throw new ArgumentOutOfRangeException("index");
                if (this.values.Count <= index)
                {
                    int oldCount = this.values.Count;
                    for (int i = oldCount; i <= index; i++)
                        this.CheckItemAt(this.ParentTypes.Original.Values[i]);
                }
                return this.values.Values.Take(index, 1).First();
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
                this.values.Clear();
            }
        }
    }
}
