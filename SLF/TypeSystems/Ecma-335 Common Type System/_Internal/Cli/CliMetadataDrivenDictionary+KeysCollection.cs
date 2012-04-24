using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CliMetadataDrivenDictionary<TDeclarationIdentifier, TMetadata, TDeclaration>
    {

        private class KeysCollection :
            IControlledCollection<TDeclarationIdentifier>,
            IControlledCollection
        {
            private CliMetadataDrivenDictionary<TDeclarationIdentifier, TMetadata, TDeclaration> owner;
            public KeysCollection(CliMetadataDrivenDictionary<TDeclarationIdentifier, TMetadata, TDeclaration> owner)
            {
                this.owner = owner;
            }

            #region IControlledCollection<TDeclarationIdentifier> Members

            public int Count
            {
                get { return this.owner.Count; }
            }

            public bool Contains(TDeclarationIdentifier item)
            {
                for (int i = 0; i < this.owner.Count; i++)
                {
                    this.owner.CheckItemAt(i);
                    if (this.owner.declarationData[i].UniqueIdentifier.Equals(item))
                        return true;
                }
                return false;
            }

            public void CopyTo(TDeclarationIdentifier[] array, int arrayIndex = 0)
            {
                for (int i = 0; i < this.owner.Count; i++)
                {
                    this.owner.CheckItemAt(i);
                    array[i + arrayIndex] = (TDeclarationIdentifier)this.owner.declarationData[i].UniqueIdentifier;
                }
            }

            public TDeclarationIdentifier this[int index]
            {
                get
                {
                    if (index < 0 || index >= this.Count)
                        throw new ArgumentOutOfRangeException("index");
                    this.owner.CheckItemAt(index);
                    return (TDeclarationIdentifier) this.owner.declarationData[index].UniqueIdentifier;
                }
            }

            public TDeclarationIdentifier[] ToArray()
            {
                TDeclarationIdentifier[] result = new TDeclarationIdentifier[this.Count];
                this.CopyTo(result);
                return result;
            }

            public int IndexOf(TDeclarationIdentifier element)
            {
                for (int i = 0; i < this.owner.Count; i++)
                {
                    this.owner.CheckItemAt(i);
                    if (this.owner.declarationData[i].UniqueIdentifier.Equals(element))
                        return i;
                }
                return -1;
            }

            #endregion

            #region IEnumerable<TDeclarationIdentifier> Members

            public IEnumerator<TDeclarationIdentifier> GetEnumerator()
            {
                for (int i = 0; i < this.owner.Count; i++)
                {
                    this.owner.CheckItemAt(i);
                    yield return (TDeclarationIdentifier) this.owner.declarationData[i].UniqueIdentifier;
                }
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion

            #region IControlledCollection Members


            bool IControlledCollection.Contains(object item)
            {
                if (item is TDeclarationIdentifier)
                    return this.Contains((TDeclarationIdentifier) item);
                return false;
            }

            void IControlledCollection.CopyTo(Array array, int arrayIndex = 0)
            {
                for (int i = 0; i < this.owner.Count; i++)
                {
                    this.owner.CheckItemAt(i);
                    array.SetValue(this.owner.declarationData[i].UniqueIdentifier, i + arrayIndex);
                }
            }

            object IControlledCollection.this[int index]
            {
                get
                {
                    return this[index];
                }
            }

            int IControlledCollection.IndexOf(object element)
            {
                if (element is TDeclarationIdentifier)
                    return this.IndexOf((TDeclarationIdentifier) element);
                return -1;
            }

            #endregion
        }
    }
}
