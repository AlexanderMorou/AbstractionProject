using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal partial class CliModifiedType
    {
        private class ModifierCollection :
            IReadOnlyCollection<TypeModification>,
            ITypeModifierSetEntry
        {
            private IReadOnlyCollection<ICliMetadataCustomModifierSignature> dataSource;
            private IType[] dataCopy;
            private _ICliManager manager;
            public ModifierCollection(IReadOnlyCollection<ICliMetadataCustomModifierSignature> modifierCollection)
            {
                this.dataSource = modifierCollection;
                this.dataCopy = new IType[modifierCollection.Count];
            }

            #region IControlledCollection<TypeModification> Members

            public int Count
            {
                get
                {
                    return this.dataCopy.Length;
                }
            }

            public bool Contains(TypeModification item)
            {
                int firstNull = -1;
                for (int datumIndex = 0; datumIndex < this.dataCopy.Length; datumIndex++)
                {
                    var current = this.dataSource[datumIndex];
                    if (current.Required != item.IsRequiredType)
                    {
                        if (this.dataCopy[datumIndex] != null && firstNull == -1)
                            firstNull = datumIndex;
                        continue;
                    }
                    if (this.dataCopy[datumIndex] != null)
                    {
                        if (this.dataCopy[datumIndex].Equals(item.ModifierType))
                            return true;
                    }
                    else if (firstNull == -1)
                        firstNull = datumIndex;
                }
                /* *
                 * All elements were available, none matched, so exit.
                 * */
                if (firstNull == -1)
                    return false;
                for (int datumIndex = firstNull; datumIndex < this.Count; datumIndex++)
                {
                    var current = this.dataSource[datumIndex];
                    if (!CheckItemAt(datumIndex) && this.dataCopy[datumIndex].Equals(item.ModifierType))
                        return true;
                }
                return false;
            }

            private bool CheckItemAt(int datumIndex)
            {
                if (this.dataCopy[datumIndex] == null)
                {
                    this.InitializeItemAt(datumIndex);
                    return false;
                }
                return true;
            }

            private void InitializeItemAt(int datumIndex)
            {
                this.dataCopy[datumIndex] = this.manager.ObtainTypeReference(dataSource[datumIndex].ModifierType);
            }

            public void CopyTo(TypeModification[] array, int arrayIndex = 0)
            {
                ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
                for (int datumIndex = 0; datumIndex < this.Count; datumIndex++)
                {
                    this.CheckItemAt(datumIndex);
                    array[arrayIndex + datumIndex] = new TypeModification(this.dataCopy[datumIndex], this.dataSource[datumIndex].Required);
                }
            }

            public TypeModification this[int index]
            {
                get {
                    if (index < 0 || index >= this.Count)
                        throw new ArgumentOutOfRangeException("index");
                    this.CheckItemAt(index);
                    return new TypeModification(this.dataCopy[index], this.dataSource[index].Required);
                }
            }

            public TypeModification[] ToArray()
            {
                TypeModification[] result = new TypeModification[this.Count];
                this.CopyTo(result);
                return result;
            }

            public int IndexOf(TypeModification item)
            {
                int firstNull = -1;
                for (int datumIndex = 0; datumIndex < this.dataCopy.Length; datumIndex++)
                {
                    var current = this.dataSource[datumIndex];
                    if (current.Required != item.IsRequiredType)
                    {
                        if (this.dataCopy[datumIndex] != null && firstNull == -1)
                            firstNull = datumIndex;
                        continue;
                    }
                    if (this.dataCopy[datumIndex] != null)
                    {
                        if (this.dataCopy[datumIndex].Equals(item.ModifierType))
                            return datumIndex;
                    }
                    else if (firstNull == -1)
                        firstNull = datumIndex;
                }
                /* *
                 * All elements were available, none matched, so exit.
                 * */
                if (firstNull == -1)
                    return -1;
                for (int datumIndex = firstNull; datumIndex < this.Count; datumIndex++)
                {
                    var current = this.dataSource[datumIndex];
                    if (!CheckItemAt(datumIndex) && this.dataCopy[datumIndex].Equals(item.ModifierType))
                        return datumIndex;
                }
                return -1;
            }

            #endregion

            #region IEnumerable<TypeModification> Members

            public IEnumerator<TypeModification> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion


            #region ITypeModifierSetEntry Members

            IEnumerable<bool> ITypeModifierSetEntry.EnumerateRequirementState()
            {
                return from entry in this.dataSource
                       select entry.Required;
            }

            IEnumerable<IType> ITypeModifierSetEntry.EnumerateModifierTypes()
            {
                for (int datumIndex = 0; datumIndex < this.Count; datumIndex++)
                {
                    var current = this.dataSource[datumIndex];
                    this.CheckItemAt(datumIndex);
                    yield return this.dataCopy[datumIndex];
                }
            }

            #endregion

            public IEnumerable<IType> RequiredModifiers
            {
                get
                {
                    return GetModifiersOf(true);
                }
            }
            public IEnumerable<IType> OptionalModifiers
            {
                get
                {
                    return GetModifiersOf(false);
                }
            }

            private IEnumerable<IType> GetModifiersOf(bool required)
            {
                for (int datumIndex = 0; datumIndex < this.Count; datumIndex++)
                {
                    if (this.dataSource[datumIndex].Required == required)
                    {
                        this.CheckItemAt(datumIndex);
                        yield return this.dataCopy[datumIndex];
                    }
                }
            }


            #region IEquatable<ITypeModifierSetEntry> Members

            bool IEquatable<ITypeModifierSetEntry>.Equals(ITypeModifierSetEntry other)
            {
                if (other.Count != this.Count)
                    return false;
                return ((ITypeModifierSetEntry) this).EnumerateRequirementState().SequenceEqual(other.EnumerateRequirementState()) &&
                       ((ITypeModifierSetEntry) this).EnumerateModifierTypes().SequenceEqual(other.EnumerateModifierTypes());
            }

            #endregion
        }
    }
}
