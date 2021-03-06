﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    /// <summary>
    /// Provides a full member dictionary for a CLI member parent.
    /// </summary>
    internal partial class CliFullMemberDictionary :
        IFullDeclarationDictionary<IGeneralMemberUniqueIdentifier, IMember>,
        IFullMemberDictionary,
        IFullDeclarationDictionary
    {
        private CliMemberType[] memberTypes;
        private IMember[] members;
        private ICliMetadataTableRow[] memberSources;
        private IGeneralMemberUniqueIdentifier[] memberIdentifiers;
        private _ICliMemberParent parent;
        private KeysCollection keys;
        private ValuesCollection values;
        public CliFullMemberDictionary(_ICliMemberParent parent)
        {
            this.parent = parent;
            var typesAndSources = parent.MetadataEntry.GetMemberData().SplitSet();
            this.memberTypes = typesAndSources.Item1;
            this.memberSources = typesAndSources.Item2;
            this.members = new IMember[this.memberTypes.Length];
            this.memberIdentifiers = new IGeneralMemberUniqueIdentifier[this.memberTypes.Length];
        }

        internal int GetMetadataIndex(ICliMetadataTableRow metadataEntry)
        {
            if (memberSources == null)
                return -1;
            return this.memberSources.GetIndexOf(metadataEntry);
        }

        public IEnumerable<ISubordinateDictionary> Subordinates
        {
            get
            {
                ISubordinateDictionary current = null;
                if ((current = (this.parent.BinaryOperators)) != null)
                    yield return current;
                if ((current = (this.parent.Constructors)) != null)
                    yield return current;
                if ((current = (this.parent.Events)) != null)
                    yield return current;
                if ((current = (this.parent.Fields)) != null)
                    yield return current;
                if ((current = (this.parent.Indexers)) != null)
                    yield return current;
                if ((current = (this.parent.Methods)) != null)
                    yield return current;
                if ((current = (this.parent.Properties)) != null)
                    yield return current;
                if ((current = (this.parent.TypeCoercions)) != null)
                    yield return current;
                if ((current = (this.parent.UnaryOperators)) != null)
                    yield return current;
            }
        }

        public IControlledCollection<IGeneralMemberUniqueIdentifier> Keys
        {
            get
            {
                if (this.keys == null)
                    this.keys = new KeysCollection(this);
                return this.keys;
            }
        }

        public IControlledCollection<MasterDictionaryEntry<IMember>> Values
        {
            get
            {
                if (this.values == null)
                    this.values = new ValuesCollection(this);
                return this.values;
            }
        }

        public MasterDictionaryEntry<IMember> this[IGeneralMemberUniqueIdentifier key]
        {
            get {
                int index = this.Keys.IndexOf(key);
                if (index == -1)
                    throw new KeyNotFoundException();
                return this.Values[index];
            }
        }

        public bool ContainsKey(IGeneralMemberUniqueIdentifier key)
        {
            int firstUnloadedIndex = -1;
            for (int memberIndex = 0; memberIndex < this.Count; memberIndex++)
                if (this.memberIdentifiers[memberIndex] != null)
                {
                    if (key.Equals(this.memberIdentifiers[memberIndex]))
                        return true;
                }
                else if (firstUnloadedIndex == -1)
                    firstUnloadedIndex = memberIndex;
            if (firstUnloadedIndex == -1)
                return false;
            for (int memberIndex = firstUnloadedIndex; memberIndex < this.Count; memberIndex++)
            {
                if (this.memberIdentifiers[memberIndex] == null)
                {
                    this.CheckIdentifierAt(memberIndex);
                    if (key.Equals(this.memberIdentifiers[memberIndex]))
                        return true;
                }
            }
            return false;
        }

        public bool TryGetValue(IGeneralMemberUniqueIdentifier key, out MasterDictionaryEntry<IMember> value)
        {
            int index = this.Keys.IndexOf(key);
            if (index == -1)
            {
                value = default(MasterDictionaryEntry<IMember>);
                return false;
            }
            value = this.Values[index];
            return true;
        }

        public int Count
        {
            get { return this.members.Length; }
        }

        public bool Contains(KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>> item)
        {
            int index = this.Keys.IndexOf(item.Key);
            if (index == -1)
                return false;
            return this.Values[index].Equals(item.Value);
        }

        public void CopyTo(KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>>[] array, int arrayIndex = 0)
        {
            ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
            for (int memberIndex = 0; memberIndex < this.Count; memberIndex++)
                array[arrayIndex + memberIndex] = new KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>>(this.Keys[memberIndex], this.Values[memberIndex]);
        }

        public KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>> this[int index]
        {
            get { return new KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>>(this.Keys[index], this.Values[index]); }
        }

        public KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>>[] ToArray()
        {
            KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>>[] result = new KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>>[this.Count];
            this.CopyTo(result);
            return result;
        }

        public int IndexOf(KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>> element)
        {
            int keyIndex = this.Keys.IndexOf(element.Key);
            if (keyIndex > -1)
            {
                var valIndex = this.Values.IndexOf(element.Value);
                if (valIndex == keyIndex)
                    return valIndex;
            }
            return -1;
        }

        public IEnumerator<KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>>> GetEnumerator()
        {
            for (int memberIndex = 0; memberIndex < this.Count; memberIndex++)
                yield return new KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>>(this.Keys[memberIndex], this.Values[memberIndex]);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void CheckIdentifierAt(int memberIndex)
        {
            if (memberIndex < 0 || memberIndex >= this.Count)
                throw new ArgumentOutOfRangeException("memberIndex");
            if (this.memberIdentifiers[memberIndex] != null)
                return;
            switch (this.memberTypes[memberIndex])
            {
                case CliMemberType.BinaryOperator:
                    this.memberIdentifiers[memberIndex] = CliMemberExtensions.GetBinaryOperatorIdentifier((ICliMetadataMethodDefinitionTableRow)this.memberSources[memberIndex], (IType)this.parent, this.parent.IdentityManager);
                    break;
                case CliMemberType.Constructor:
                    var ctor = (ICliMetadataMethodDefinitionTableRow)this.memberSources[memberIndex];
                    if (ctor.Name == CliCommon.ConstructorStaticName &&
                        ctor.Parameters.Count == 0)
                        this.memberIdentifiers[memberIndex] = TypeSystemIdentifiers.GetCtorSignatureIdentifier();
                    else
                        this.memberIdentifiers[memberIndex] = CliMemberExtensions.GetCtorIdentifier(ctor, (IType)this.parent, this.parent.IdentityManager);
                    break;
                case CliMemberType.Event:
                    this.memberIdentifiers[memberIndex] = CliMemberExtensions.GetEventIdentifier((ICliMetadataEventTableRow)this.memberSources[memberIndex], (IType)this.parent, this.parent.IdentityManager);
                    break;
                case CliMemberType.Field:
                    this.memberIdentifiers[memberIndex] = (IGeneralMemberUniqueIdentifier)CliMemberExtensions.GetFieldIdentifier((ICliMetadataFieldTableRow)this.memberSources[memberIndex]);
                    break;
                case CliMemberType.Indexer:
                    this.memberIdentifiers[memberIndex] = CliMemberExtensions.GetIndexerIdentifier((ICliMetadataPropertyTableRow)this.memberSources[memberIndex], (IType)this.parent, this.parent.IdentityManager);
                    break;
                case CliMemberType.Method:
                    this.memberIdentifiers[memberIndex] = CliMemberExtensions.GetMethodIdentifier((ICliMetadataMethodDefinitionTableRow)this.memberSources[memberIndex], (IType)this.parent, this.parent.IdentityManager, () => (IMethodSignatureMember)this.Values[memberIndex].Entry);
                    break;
                case CliMemberType.Property:
                    this.memberIdentifiers[memberIndex] = (IGeneralMemberUniqueIdentifier)CliMemberExtensions.GetPropertyIdentifier((ICliMetadataPropertyTableRow)this.memberSources[memberIndex]);
                    break;
                case CliMemberType.TypeCoercionOperator:
                    this.memberIdentifiers[memberIndex] = CliMemberExtensions.GetTypeCoercionOperatorIdentifier((ICliMetadataMethodDefinitionTableRow)this.memberSources[memberIndex], (IType)this.parent, this.parent.IdentityManager);
                    break;
                case CliMemberType.UnaryOperator:
                    this.memberIdentifiers[memberIndex] = CliMemberExtensions.GetUnaryOperatorIdentifier((ICliMetadataMethodDefinitionTableRow)this.memberSources[memberIndex], (IType)this.parent, this.parent.IdentityManager);
                    break;
                default:
                    throw new InvalidOperationException("Member of invalid kind.");
            }
        }

        private void CheckItemAt(int memberIndex)
        {
            if (this.members[memberIndex] == null)
            {
                this.CheckIdentifierAt(memberIndex);
                this.members[memberIndex] = this.parent.CreateItem(this.memberTypes[memberIndex], this.memberSources[memberIndex], this.memberIdentifiers[memberIndex], memberIndex);
            }
        }

        internal IEnumerable<Tuple<int, TIdentifier>> ObtainSubset<TIdentifier, TMember>(CliMemberType kind)
            where TIdentifier :
                IMemberUniqueIdentifier
            where TMember :
                IDeclaration<TIdentifier>,
                IMember
        {
            for (int datumIndex = 0; datumIndex < this.Count; datumIndex++)
            {
                if (this.memberTypes[datumIndex] == kind)
                {
                    this.CheckIdentifierAt(datumIndex);
                    IMemberUniqueIdentifier currentId = this.memberIdentifiers[datumIndex];
                    if (currentId is TIdentifier)
                        yield return new Tuple<int, TIdentifier>(datumIndex, (TIdentifier)currentId);
                }
            }
        }


        IEnumerable IMasterDictionary.Subordinates
        {
            get { return this.Subordinates; }
        }

        object IControlledCollection.this[int index]
        {
            get { return new KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>>(this.Keys[index], this.Values[index]); }
        }

        void IControlledCollection.CopyTo(Array array, int arrayIndex)
        {
            ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
            for (int memberIndex = 0; memberIndex < this.Count; memberIndex++)
                array.SetValue(new KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>>(this.Keys[memberIndex], this.Values[memberIndex]), arrayIndex + memberIndex);
        }

        int IControlledCollection.IndexOf(object element)
        {
            if (element is KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>>)
                return this.IndexOf((KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>>)element);
            return -1;
        }

        bool IControlledCollection.Contains(object element)
        {
            if (element is KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>>)
                return this.Contains((KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>>)element);
            return false;
        }

        IControlledCollection IControlledDictionary.Keys { get { return (IControlledCollection)this.Keys; } }

        IControlledCollection IControlledDictionary.Values { get { return (IControlledCollection)this.Values; } }

        object IControlledDictionary.this[object index]
        {
            get
            {
                if (index is IGeneralMemberUniqueIdentifier)
                    return this[(IGeneralMemberUniqueIdentifier)index];
                throw new ArgumentOutOfRangeException("index");
            }
        }
        IDictionaryEnumerator IControlledDictionary.GetEnumerator() 
        {
            return new SimpleDictionaryEnumerator<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>>(this.GetEnumerator());
        }

        bool IControlledDictionary.ContainsKey(object key)
        {
            if (key is IGeneralMemberUniqueIdentifier)
                return this.ContainsKey((IGeneralMemberUniqueIdentifier)key);
            return false;
        }
    }
}
