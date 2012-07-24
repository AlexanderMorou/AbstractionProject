using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal partial class CliFullTypeDictionary :
        IFullDeclarationDictionary<IGeneralTypeUniqueIdentifier, IType>,
        IFullTypeDictionary
    {
        private _ICliTypeParent parent;
        private ICliMetadataTypeDefinitionTableRow[] typeDefinitions;
        private IType[] resultTypes;
        private TypeKind[] resultKinds;
        private IGeneralTypeUniqueIdentifier[] resultKeys;
        private object syncObject = new object();

        private KeysCollection keys;
        private ValuesCollection values;

        public CliFullTypeDictionary(IReadOnlyCollection<ICliMetadataTypeDefinitionTableRow> typeDefinitions, _ICliTypeParent parent)
        {
            if (typeDefinitions is IArrayReadOnlyCollection<ICliMetadataTypeDefinitionTableRow>)
            {
                var arrayTypeDefinitions = (IArrayReadOnlyCollection<ICliMetadataTypeDefinitionTableRow>) typeDefinitions;
                if (arrayTypeDefinitions.IsAggregate)
                    this.typeDefinitions = arrayTypeDefinitions.AggregateArrays.ConcatinateSeries();
                else
                    this.typeDefinitions = arrayTypeDefinitions.InternalArray;
            }
            else if (typeDefinitions != null)
                this.typeDefinitions = typeDefinitions.ToArray();
            else
                this.typeDefinitions = new ICliMetadataTypeDefinitionTableRow[0];
            int len = this.typeDefinitions.Length;
            this.resultTypes = new IType[len];
            this.resultKinds = new TypeKind[len];
            this.resultKeys = new IGeneralTypeUniqueIdentifier[len];
            this.parent = parent;
        }

        #region IMasterDictionary<IGeneralTypeUniqueIdentifier,IType> Members

        public IEnumerable<ISubordinateDictionary> Subordinates
        {
            get
            {
                yield return GetSubordinate(TypeKind.Class);
                yield return GetSubordinate(TypeKind.Delegate);
                yield return GetSubordinate(TypeKind.Enumeration);
                yield return GetSubordinate(TypeKind.Interface);
                yield return GetSubordinate(TypeKind.Struct);
            }
        }

        #endregion

        #region IControlledDictionary<IGeneralTypeUniqueIdentifier,MasterDictionaryEntry<IType>> Members

        public IControlledCollection<IGeneralTypeUniqueIdentifier> Keys
        {
            get
            {
                if (this.keys == null)
                    this.keys = new KeysCollection(owner: this);
                return this.keys;
            }
        }

        public IControlledCollection<MasterDictionaryEntry<IType>> Values
        {
            get
            {
                if (this.values == null)
                    this.values = new CliFullTypeDictionary.ValuesCollection(owner: this);
                return this.values;
            }
        }

        public MasterDictionaryEntry<IType> this[IGeneralTypeUniqueIdentifier key]
        {
            get
            {
                lock (syncObject)
                {
                    int lastNull = -1;
                    for (int datumIndex = 0; datumIndex < this.Count; datumIndex++)
                    {
                        var currentKey = this.resultKeys[datumIndex];
                        if (currentKey != null)
                        {
                            if (currentKey.Equals(key))
                            {
                                this.CheckItemAt(datumIndex);
                                return new MasterDictionaryEntry<IType>(this.GetSubordinate(this.resultKinds[datumIndex]), this.resultTypes[datumIndex]);
                            }
                        }
                        else if (lastNull == -1)
                            lastNull = datumIndex;
                    }
                    /* *
                     * No element was null, therefore nothing needs loaded,
                     * if that's the case, all items have been checked.
                     * */
                    if (lastNull == -1)
                        throw new KeyNotFoundException();
                    for (int datumIndex = lastNull; datumIndex < this.Count; datumIndex++)
                    {
                        var currentKey = this.resultKeys[datumIndex];
                        if (currentKey != null)
                            continue;
                        this.CheckKindAt(datumIndex);
                        if (this.resultKeys[datumIndex].Equals(key))
                        {
                            this.CheckItemAt(datumIndex);
                            return new MasterDictionaryEntry<IType>(this.GetSubordinate(this.resultKinds[datumIndex]), this.resultTypes[datumIndex]);
                        }
                    }
                    throw new KeyNotFoundException();
                }
            }
        }

        public bool ContainsKey(IGeneralTypeUniqueIdentifier key)
        {
            lock (syncObject)
            {
                int lastNull = -1;
                for (int datumIndex = 0; datumIndex < this.Count; datumIndex++)
                {
                    var currentKey = this.resultKeys[datumIndex];
                    if (currentKey != null)
                    {
                        if (currentKey.Equals(key))
                            return true;
                    }
                    else if (lastNull == -1)
                        lastNull = datumIndex;
                }
                if (lastNull == -1)
                    return false;
                for (int datumIndex = lastNull; datumIndex < this.Count; datumIndex++)
                {
                    var currentKey = this.resultKeys[datumIndex];
                    if (currentKey != null)
                        continue;
                    this.CheckKindAt(datumIndex);
                    if (this.resultKeys[datumIndex].Equals(key))
                        return true;
                }
                return false;
            }
        }

        public bool TryGetValue(IGeneralTypeUniqueIdentifier key, out MasterDictionaryEntry<IType> value)
        {
            lock (syncObject)
            {
                int lastNull = -1;
                for (int datumIndex = 0; datumIndex < this.Count; datumIndex++)
                {
                    var currentKey = this.resultKeys[datumIndex];
                    if (currentKey != null)
                    {
                        if (currentKey.Equals(key))
                        {
                            this.CheckItemAt(datumIndex);
                            value = new MasterDictionaryEntry<IType>(this.GetSubordinate(this.resultKinds[datumIndex]), this.resultTypes[datumIndex]);
                            return true;
                        }
                    }
                    else if (lastNull == -1)
                        lastNull = datumIndex;
                }
                /* *
                 * No element was null, therefore nothing needs loaded,
                 * if that's the case, all items have been checked.
                 * */
                if (lastNull == -1)
                {
                    value = default(MasterDictionaryEntry<IType>);
                    return false;
                }
                for (int datumIndex = lastNull; datumIndex < this.Count; datumIndex++)
                {
                    var currentKey = this.resultKeys[datumIndex];
                    if (currentKey != null)
                        continue;
                    this.CheckKindAt(datumIndex);
                    if (this.resultKeys[datumIndex].Equals(key))
                    {
                        this.CheckItemAt(datumIndex);
                        value = new MasterDictionaryEntry<IType>(this.GetSubordinate(this.resultKinds[datumIndex]), this.resultTypes[datumIndex]);
                        return true;
                    }
                }
                value = default(MasterDictionaryEntry<IType>);
                return false;
            }
        }

        #endregion

        #region IControlledCollection<KeyValuePair<IGeneralTypeUniqueIdentifier,MasterDictionaryEntry<IType>>> Members

        public int Count
        {
            get { return this.typeDefinitions.Length; }
        }

        public bool Contains(KeyValuePair<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IType>> item)
        {
            MasterDictionaryEntry<IType> dummy;
            if (this.TryGetValue(item.Key, out dummy))
                return item.Value.Equals(dummy);
            return false;
        }

        public void CopyTo(KeyValuePair<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IType>>[] array, int arrayIndex = 0)
        {
            ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
            for (int datumIndex = 0; datumIndex < this.Count; datumIndex++)
            {
                this.CheckItemAt(datumIndex);
                array[datumIndex + arrayIndex] = new KeyValuePair<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IType>>(this.resultKeys[datumIndex], new MasterDictionaryEntry<IType>(this.GetSubordinate(this.resultKinds[datumIndex]), this.resultTypes[datumIndex]));
            }
        }

        public KeyValuePair<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IType>> this[int index]
        {
            get { throw new NotImplementedException(); }
        }

        public KeyValuePair<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IType>>[] ToArray()
        {
            throw new NotImplementedException();
        }

        int IControlledCollection<KeyValuePair<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IType>>>.IndexOf(KeyValuePair<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IType>> element)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<KeyValuePair<IGeneralTypeUniqueIdentifier,MasterDictionaryEntry<IType>>> Members

        public IEnumerator<KeyValuePair<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IType>>> GetEnumerator()
        {
            for (int datumIndex = 0; datumIndex < this.Count; datumIndex++)
            {
                this.CheckItemAt(datumIndex);
                yield return new KeyValuePair<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IType>>(this.resultKeys[datumIndex], new MasterDictionaryEntry<IType>(this.GetSubordinate(this.resultKinds[datumIndex]), this.resultTypes[datumIndex]));
            }
        }

        private ICliMetadataTypeDefinitionTableRow CheckKindAt(int datumIndex)
        {
            lock (this.syncObject)
            {
                if (this.resultKeys[datumIndex] == null)
                    return DiscernKindAt(datumIndex);
                return this.typeDefinitions[datumIndex];
            }
        }

        private void CheckItemAt(int datumIndex)
        {
            lock (this.syncObject)
            {
                if (this.resultTypes[datumIndex] == null)
                    ConstructItemAt(datumIndex);
            }
        }

        private IType ConstructItemAt(int datumIndex)
        {
            lock (this.syncObject)
            {
                var current = this.CheckKindAt(datumIndex);
                return this.resultTypes[datumIndex] = parent.Manager.ObtainTypeReference(current);
            }
        }

        private ISubordinateDictionary GetSubordinate(TypeKind kind)
        {
            ISubordinateDictionary subordinate = null;
            switch (kind)
            {
                case TypeKind.Class:
                    subordinate = (ISubordinateDictionary) this.parent.Classes;
                    break;
                case TypeKind.Delegate:
                    subordinate = (ISubordinateDictionary) this.parent.Delegates;
                    break;
                case TypeKind.Enumeration:
                    subordinate = (ISubordinateDictionary) this.parent.Enums;
                    break;
                case TypeKind.Interface:
                    subordinate = (ISubordinateDictionary) this.parent.Interfaces;
                    break;
                case TypeKind.Struct:
                    subordinate = (ISubordinateDictionary) this.parent.Structs;
                    break;
            }
            return subordinate;
        }

        private ICliMetadataTypeDefinitionTableRow DiscernKindAt(int datumIndex)
        {
            var current = this.typeDefinitions[datumIndex];
            TypeKind typeKind = current.DetermineTypeKind(this.parent.Manager);
            this.resultKeys[datumIndex] = CliCommon.GetUniqueIdentifier(current, parent.Manager);
            this.resultKinds[datumIndex] = typeKind;
            return current;
        }

        #endregion

        internal IEnumerable<Tuple<int, TIdentifier>> ObtainSubset<TIdentifier, TType>(TypeKind kind)
            where TIdentifier :
                ITypeUniqueIdentifier
            where TType :
                IType<TIdentifier, TType>
        {
            for (int datumIndex = 0; datumIndex < this.Count; datumIndex++)
            {
                this.CheckKindAt(datumIndex);
                IGeneralTypeUniqueIdentifier currentId = null;
                if (this.resultKinds[datumIndex] == kind &&
                    (currentId = this.resultKeys[datumIndex]) is TIdentifier)
                    yield return new Tuple<int, TIdentifier>(datumIndex, (TIdentifier) currentId);
            }
        }

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IFullTypeDictionary Members

        public IType[] GetTypesByName(string name)
        {
            return (from t in this.Keys
                    where t.Name == name
                    select this[t].Entry).ToArray();
        }

        #endregion
    }
}
