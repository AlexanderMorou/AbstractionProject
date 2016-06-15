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
        private class ValuesCollection :
            IControlledCollection<TDeclaration>,
            IControlledCollection
        {
            private CliMetadataDrivenDictionary<TDeclarationIdentifier, TMetadata, TDeclaration> owner;
            public ValuesCollection(CliMetadataDrivenDictionary<TDeclarationIdentifier, TMetadata, TDeclaration> owner)
            {
                this.owner = owner;
            }

            #region IControlledCollection<TDeclaration> Members

            public int Count
            {
                get { return this.owner.Count; }
            }

            public bool Contains(TDeclaration item)
            {
                for (int i = 0; i < this.owner.Count; i++)
                {
                    this.owner.CheckItemAt(i);
                    if (this.owner.declarationData[i] == item)
                        return true;
                }
                return false;
            }

            public void CopyTo(TDeclaration[] array, int arrayIndex = 0)
            {
                for (int i = 0; i < this.owner.Count; i++)
                {
                    this.owner.CheckItemAt(i);
                    array[i + arrayIndex] = this.owner.declarationData[i];
                }
            }

            public TDeclaration this[int index]
            {
                get
                {
                    if (index < 0 || index >= this.Count)
                        throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
                    this.owner.CheckItemAt(index);
                    return this.owner.declarationData[index];
                }
            }

            public TDeclaration[] ToArray()
            {
                TDeclaration[] result = new TDeclaration[this.Count];
                this.CopyTo(result);
                return result;
            }

            public int IndexOf(TDeclaration element)
            {
                for (int i = 0; i < this.owner.Count; i++)
                {
                    this.owner.CheckItemAt(i);
                    if (this.owner.declarationData[i] == element)
                        return i;
                }
                return -1;
            }

            #endregion

            #region IEnumerable<TDeclaration> Members

            public IEnumerator<TDeclaration> GetEnumerator()
            {
                for (int i = 0; i < this.owner.Count; i++)
                {
                    this.owner.CheckItemAt(i);
                    yield return this.owner.declarationData[i];
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
                if (item is TDeclaration)
                    return this.Contains((TDeclaration) item);
                return false;
            }

            void IControlledCollection.CopyTo(Array array, int arrayIndex)
            {
                for (int i = 0; i < this.owner.Count; i++)
                {
                    this.owner.CheckItemAt(i);
                    array.SetValue(this.owner.declarationData[i], i + arrayIndex);
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
                if (element is TDeclaration)
                    return this.IndexOf((TDeclaration) element);
                return -1;
            }

            #endregion
        }
    }
}
