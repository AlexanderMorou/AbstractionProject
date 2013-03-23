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
        private class ValuesCollection :
            IControlledCollection<MasterDictionaryEntry<IMember>>,
            IControlledCollection
        {
            private CliFullMemberDictionary owner;

            public ValuesCollection(CliFullMemberDictionary owner)
            {
                this.owner = owner;
            }

            public int Count
            {
                get
                {
                    return this.owner.members.Length;
                }
            }

            public bool Contains(MasterDictionaryEntry<IMember> item)
            {
                /* *
                 * Unlike the keys, which can come from about anywhere,
                 * the members initialization are controlled through this framework,
                 * if it isn't loaded yet, it isn't contained here.
                 * */
                for (int memberIndex = 0; memberIndex < this.Count; memberIndex++)
                {
                    if (this.owner.members[memberIndex] != null)
                        switch (this.owner.memberTypes[memberIndex])
                        {
                            case CliMemberType.BinaryOperator:
                                if (item.Subordinate != this.owner.parent.BinaryOperators)
                                    continue;
                                break;
                            case CliMemberType.Constructor:
                                if (item.Subordinate != this.owner.parent.Constructors)
                                    continue;
                                break;
                            case CliMemberType.Event:
                                if (item.Subordinate != this.owner.parent.Events)
                                    continue;
                                break;
                            case CliMemberType.Field:
                                if (item.Subordinate != this.owner.parent.Fields)
                                    continue;
                                break;
                            case CliMemberType.Method:
                                if (item.Subordinate != this.owner.parent.Methods)
                                    continue;
                                break;
                            case CliMemberType.Property:
                                if (item.Subordinate != this.owner.parent.Properties)
                                    continue;
                                break;
                            case CliMemberType.TypeCoercionOperator:
                                if (item.Subordinate != this.owner.parent.TypeCoercions)
                                    continue;
                                break;
                            case CliMemberType.UnaryOperator:
                                if (item.Subordinate != this.owner.parent.UnaryOperators)
                                    continue;
                                break;
                            default:
                                continue;
                        }
                    if (item.Entry == this.owner.members[memberIndex])
                        return true;
                }
                return false;
            }

            public void CopyTo(MasterDictionaryEntry<IMember>[] array, int arrayIndex = 0)
            {
                ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
                for (int memberIndex = 0; memberIndex < this.Count; memberIndex++)
                    this.owner.CheckItemAt(memberIndex);
                this.owner.members.CopyTo(array, arrayIndex);
            }

            public MasterDictionaryEntry<IMember> this[int index]
            {
                get
                {
                    if (this.owner.members[index] == null)
                        this.owner.CheckItemAt(index);
                    switch (this.owner.memberTypes[index])
                    {
                        case CliMemberType.BinaryOperator:
                            return new MasterDictionaryEntry<IMember>(this.owner.parent.BinaryOperators, this.owner.members[index]);
                        case CliMemberType.Constructor:
                            return new MasterDictionaryEntry<IMember>(this.owner.parent.Constructors, this.owner.members[index]);
                        case CliMemberType.Indexer:
                            return new MasterDictionaryEntry<IMember>(this.owner.parent.Indexers, this.owner.members[index]);
                        case CliMemberType.Event:
                            return new MasterDictionaryEntry<IMember>(this.owner.parent.Events, this.owner.members[index]);
                        case CliMemberType.Field:
                            return new MasterDictionaryEntry<IMember>(this.owner.parent.Fields, this.owner.members[index]);
                        case CliMemberType.Method:
                            return new MasterDictionaryEntry<IMember>(this.owner.parent.Methods, this.owner.members[index]);
                        case CliMemberType.Property:
                            return new MasterDictionaryEntry<IMember>(this.owner.parent.Properties, this.owner.members[index]);
                        case CliMemberType.TypeCoercionOperator:
                            return new MasterDictionaryEntry<IMember>(this.owner.parent.TypeCoercions, this.owner.members[index]);
                        case CliMemberType.UnaryOperator:
                            return new MasterDictionaryEntry<IMember>(this.owner.parent.UnaryOperators, this.owner.members[index]);
                    }
                    throw new InvalidOperationException();
                }
            }

            public MasterDictionaryEntry<IMember>[] ToArray()
            {
                MasterDictionaryEntry<IMember>[] result = new MasterDictionaryEntry<IMember>[this.Count];
                this.CopyTo(result);
                return result;
            }

            public int IndexOf(MasterDictionaryEntry<IMember> element)
            {
                for (int memberIndex = 0; memberIndex < this.Count; memberIndex++)
                {
                    if (this.owner.members[memberIndex] != null)
                    {
                        switch (this.owner.memberTypes[memberIndex])
                        {
                            case CliMemberType.BinaryOperator:
                                if (element.Subordinate != this.owner.parent.BinaryOperators)
                                    continue;
                                break;
                            case CliMemberType.Constructor:
                                if (element.Subordinate != this.owner.parent.Constructors)
                                    continue;
                                break;
                            case CliMemberType.Event:
                                if (element.Subordinate != this.owner.parent.Events)
                                    continue;
                                break;
                            case CliMemberType.Field:
                                if (element.Subordinate != this.owner.parent.Fields)
                                    continue;
                                break;
                            case CliMemberType.Method:
                                if (element.Subordinate != this.owner.parent.Methods)
                                    continue;
                                break;
                            case CliMemberType.Property:
                                if (element.Subordinate != this.owner.parent.Properties)
                                    continue;
                                break;
                            case CliMemberType.TypeCoercionOperator:
                                if (element.Subordinate != this.owner.parent.TypeCoercions)
                                    continue;
                                break;
                            case CliMemberType.UnaryOperator:
                                if (element.Subordinate != this.owner.parent.UnaryOperators)
                                    continue;
                                break;
                            default:
                                continue;
                        }
                        if (element.Entry == this.owner.members[memberIndex])
                            return memberIndex;
                    }
                }
                return -1;
            }

            public IEnumerator<MasterDictionaryEntry<IMember>> GetEnumerator()
            {
                ISubordinateDictionary bops = null,
                                       ctors = null,
                                       evts = null,
                                       fields = null,
                                       methods = null,
                                       props = null,
                                       typeCs = null,
                                       unops = null;
                for (int memberIndex = 0; memberIndex < this.Count; memberIndex++)
                {
                    if (this.owner.members[memberIndex] == null)
                        this.owner.CheckItemAt(memberIndex);
                    switch (this.owner.memberTypes[memberIndex])
                    {
                        case CliMemberType.BinaryOperator:
                            yield return new MasterDictionaryEntry<IMember>(bops ?? (bops = this.owner.parent.BinaryOperators), this.owner.members[memberIndex]);
                            break;
                        case CliMemberType.Constructor:
                            yield return new MasterDictionaryEntry<IMember>(ctors ?? (ctors = this.owner.parent.Constructors), this.owner.members[memberIndex]);
                            break;
                        case CliMemberType.Event:
                            yield return new MasterDictionaryEntry<IMember>(evts ?? (evts = this.owner.parent.Events), this.owner.members[memberIndex]);
                            break;
                        case CliMemberType.Field:
                            yield return new MasterDictionaryEntry<IMember>(fields ?? (fields = this.owner.parent.Fields), this.owner.members[memberIndex]);
                            break;
                        case CliMemberType.Method:
                            yield return new MasterDictionaryEntry<IMember>(methods ?? (methods = this.owner.parent.Methods), this.owner.members[memberIndex]);
                            break;
                        case CliMemberType.Property:
                            yield return new MasterDictionaryEntry<IMember>(props ?? (props = this.owner.parent.Properties), this.owner.members[memberIndex]);
                            break;
                        case CliMemberType.TypeCoercionOperator:
                            yield return new MasterDictionaryEntry<IMember>(typeCs ?? (typeCs = this.owner.parent.TypeCoercions), this.owner.members[memberIndex]);
                            break;
                        case CliMemberType.UnaryOperator:
                            yield return new MasterDictionaryEntry<IMember>(unops ?? (unops = this.owner.parent.UnaryOperators), this.owner.members[memberIndex]);
                            break;
                    }
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }


            bool IControlledCollection.Contains(object element)
            {
                if (element is MasterDictionaryEntry<IMember>)
                    return this.Contains((MasterDictionaryEntry<IMember>)element);
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
                if (element is MasterDictionaryEntry<IMember>)
                    return this.IndexOf((MasterDictionaryEntry<IMember>)element);
                return -1;
            }
        }
    }
}
