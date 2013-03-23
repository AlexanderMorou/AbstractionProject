using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    partial class CliDependentParameterMemberDictionary<TParent, TParameter>
    {
        private class ValuesCollection :
            IControlledCollection<TParameter>
        {
            private CliDependentParameterMemberDictionary<TParent, TParameter> owner;
            public ValuesCollection(CliDependentParameterMemberDictionary<TParent, TParameter> owner)
            {
                this.owner = owner;
            }

            public int Count
            {
                get { return this.owner.duplicates.Length; }
            }

            public bool Contains(TParameter item)
            {
                int c;
                lock (this.owner.syncObject)
                    c = this.owner.duplicates.Length;
                for (int i = 0; i < c; i++)
                {
                    TParameter current;
                    lock (this.owner.syncObject)
                        current = this.owner.duplicates[i];
                    if (current == this.owner.duplicates[i])
                        return true;
                }
                return false;
            }

            public void CopyTo(TParameter[] array, int arrayIndex = 0)
            {
                int c;
                lock (this.owner.syncObject)
                    c = this.owner.duplicates.Length;
                ThrowHelper.CopyToCheck(array, arrayIndex, c);
                for (int i = 0; i < c; i++)
                {
                    this.owner.CheckItemAt(i);
                    TParameter current;
                    lock (this.owner.syncObject)
                        current = this.owner.duplicates[i];
                    array[arrayIndex + i] = current;
                }
            }

            public TParameter this[int index]
            {
                get
                {
                    if (index < 0 || index >= this.Count)
                        throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
                    this.owner.CheckItemAt(index);
                    TParameter current;
                    lock (this.owner.syncObject)
                        current = this.owner.duplicates[index];
                    return current;
                }
            }

            public TParameter[] ToArray()
            {
                TParameter[] result = new TParameter[this.Count];
                this.CopyTo(result);
                return result;
            }

            public int IndexOf(TParameter element)
            {
                int c;
                lock (this.owner.syncObject)
                    c = this.owner.duplicates.Length;
                for (int i = 0; i < c; i++)
                {
                    TParameter current;
                    lock (this.owner.syncObject)
                        current = this.owner.duplicates[i];
                    if (current == element)
                        return i;
                }
                return -1;
            }

            public IEnumerator<TParameter> GetEnumerator()
            {
                int c;
                lock (this.owner.syncObject)
                    c = this.owner.duplicates.Length;
                for (int i = 0; i < c; i++)
                {
                    this.owner.CheckItemAt(i);
                    TParameter currentParameter;
                    lock (this.owner.syncObject)
                        currentParameter = this.owner.duplicates[i];
                    yield return currentParameter;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }
    }
}
