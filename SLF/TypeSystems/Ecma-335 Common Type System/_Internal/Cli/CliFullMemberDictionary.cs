using System;
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
    internal partial class CliFullMemberDictionary :
        IFullDeclarationDictionary<IGeneralMemberUniqueIdentifier, IMember>,
        IFullMemberDictionary
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

        public IEnumerable<ISubordinateDictionary> Subordinates
        {
            get
            {
                yield return this.parent.BinaryOperators;
                yield return this.parent.Constructors;
                yield return this.parent.Events;
                yield return this.parent.Fields;
                yield return this.parent.Methods;
                yield return this.parent.Properties;
                yield return this.parent.TypeCoercions;
                yield return this.parent.UnaryOperators;
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
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return this.members.Length; }
        }

        public bool Contains(KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>>[] array, int arrayIndex = 0)
        {
            throw new NotImplementedException();
        }

        public KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>> this[int index]
        {
            get { throw new NotImplementedException(); }
        }

        public KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>>[] ToArray()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>> element)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private void CheckIdentifierAt(int memberIndex)
        {

        }

        private void CheckItemAt(int memberIndex)
        {
            if (this.members[memberIndex] == null)
                this.members[memberIndex] = this.parent.CreateItem(this.memberTypes[memberIndex], this.memberSources[memberIndex]);
        }
    }
}
