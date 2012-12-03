using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CliFullMemberDictionary
    {
        private class KeysCollection :
            IControlledCollection<IGeneralMemberUniqueIdentifier>,
            IControlledCollection
        {
            private CliFullMemberDictionary owner;

            public KeysCollection(CliFullMemberDictionary owner)
            {
                this.owner = owner;
            }

            public int Count
            {
                get { return this.owner.Count; }
            }

            public bool Contains(IGeneralMemberUniqueIdentifier item)
            {
                int firstUnloadedIndex = -1;
                for (int memberIndex = 0; memberIndex < this.Count; memberIndex++)
                    if (this.owner.memberIdentifiers[memberIndex] != null)
                    {
                        if (item.Equals(this.owner.memberIdentifiers[memberIndex]))
                            return true;
                    }
                    else if (firstUnloadedIndex == -1)
                        firstUnloadedIndex = memberIndex;
                if (firstUnloadedIndex == -1)
                    return false;
                for (int memberIndex = firstUnloadedIndex; memberIndex < this.Count; memberIndex++)
                {
                    if (this.owner.memberIdentifiers[memberIndex] == null)
                    {
                        this.owner.CheckIdentifierAt(memberIndex);
                        if (item.Equals(this.owner.memberIdentifiers[memberIndex]))
                            return true;
                    }
                }
                return false;
            }

            public void CopyTo(IGeneralMemberUniqueIdentifier[] array, int arrayIndex = 0)
            {
                for (int memberIndex = 0; memberIndex < this.Count; memberIndex++)
                    if (this.owner.memberIdentifiers[memberIndex] == null)
                        this.owner.CheckIdentifierAt(memberIndex);
                this.owner.memberIdentifiers.CopyTo(array, 0);
            }

            public IGeneralMemberUniqueIdentifier this[int index]
            {
                get {
                    if (index < 0 || index >= this.Count)
                        throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
                    if (this.owner.memberIdentifiers[index] == null)
                        this.owner.CheckIdentifierAt(index);
                    return this.owner.memberIdentifiers[index];
                }
            }

            public IGeneralMemberUniqueIdentifier[] ToArray()
            {
                for (int memberIndex = 0; memberIndex < this.Count; memberIndex++)
                    if (this.owner.memberIdentifiers[memberIndex] == null)
                        this.owner.CheckIdentifierAt(memberIndex);
                IGeneralMemberUniqueIdentifier[] result = new IGeneralMemberUniqueIdentifier[this.Count];
                this.owner.memberIdentifiers.CopyTo(result, 0);
                return result;
            }

            public int IndexOf(IGeneralMemberUniqueIdentifier element)
            {
                int firstUnloadedIndex = -1;
                for (int memberIndex = 0; memberIndex < this.Count; memberIndex++)
                    if (this.owner.memberIdentifiers[memberIndex] != null)
                    {
                        if (element.Equals(this.owner.memberIdentifiers[memberIndex]))
                            return memberIndex;
                    }
                    else if (firstUnloadedIndex == -1)
                        firstUnloadedIndex = memberIndex;
                if (firstUnloadedIndex == -1)
                    return -1;
                for (int memberIndex = firstUnloadedIndex; memberIndex < this.Count; memberIndex++)
                {
                    if (this.owner.memberIdentifiers[memberIndex] == null)
                    {
                        this.owner.CheckIdentifierAt(memberIndex);
                        if (element.Equals(this.owner.memberIdentifiers[memberIndex]))
                            return memberIndex;
                    }
                }
                return -1;
            }

            public IEnumerator<IGeneralMemberUniqueIdentifier> GetEnumerator()
            {
                for (int memberIndex = 0; memberIndex < this.Count; memberIndex++)
                {
                    if (this.owner.memberIdentifiers[memberIndex] == null)
                        this.owner.CheckIdentifierAt(memberIndex);
                    yield return this.owner.memberIdentifiers[memberIndex];
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            bool IControlledCollection.Contains(object element)
            {
                if (element is IGeneralMemberUniqueIdentifier)
                    return this.Contains((IGeneralMemberUniqueIdentifier)element);
                return false;
            }

            void IControlledCollection.CopyTo(Array array, int arrayIndex = 0)
            {
                ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
                for (int memberIndex = 0; memberIndex < this.Count; memberIndex++)
                    this.owner.CheckItemAt(memberIndex);
                this.owner.members.CopyTo(array, arrayIndex);
            }

            object IControlledCollection.this[int index]
            {
                get { return this[index]; }
            }

            int IControlledCollection.IndexOf(object element)
            {
                if (element is IGeneralMemberUniqueIdentifier)
                    return this.IndexOf((IGeneralMemberUniqueIdentifier)element);
                return -1;
            }

        }
    }
}
