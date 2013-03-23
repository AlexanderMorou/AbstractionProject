using AllenCopeland.Abstraction.Slf.Abstract;
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
    internal abstract partial class CliDependentParameterMemberDictionary<TParent, TParameter> :
        IParameterMemberDictionary<TParent, TParameter>
        where TParent :
            IParameterParent<TParent, TParameter>
        where TParameter :
            class,
            IParameterMember<TParent>
    {
        private TParent parent;
        private IDelegateType delegateType;
        private TParameter[] duplicates;
        private object syncObject;
        private ValuesCollection values;
        public CliDependentParameterMemberDictionary(TParent parent, IDelegateType delegateType)
        {
            this.syncObject = new object();
            this.parent = parent;
            this.delegateType = delegateType;
            this.duplicates = new TParameter[delegateType.Parameters.Count];
        }

        public IControlledTypeCollection ParameterTypes
        {
            get { return this.delegateType.Parameters.ParameterTypes; }
        }

        public TParent Parent
        {
            get { return this.parent; }
        }

        public int IndexOf(TParameter decl)
        {
            return this.Values.IndexOf(decl);
        }

        public IControlledCollection<IGeneralMemberUniqueIdentifier> Keys
        {
            get { return this.delegateType.Parameters.Keys; }
        }

        public IControlledCollection<TParameter> Values
        {
            get {
                if (this.values == null)
                    this.values = new ValuesCollection(this);
                return this.values;
            }
        }

        public TParameter this[IGeneralMemberUniqueIdentifier key]
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
            return this.delegateType.Parameters.ContainsKey(key);
        }

        public bool TryGetValue(IGeneralMemberUniqueIdentifier key, out TParameter value)
        {
            int index = this.Keys.IndexOf(key);
            if (index == -1)
            {
                value = null;
                return false;
            }
            value = this.Values[index];
            return true;
        }

        public int Count
        {
            get { 
                lock (this.syncObject)
                    return this.duplicates.Length; }
        }

        public bool Contains(KeyValuePair<IGeneralMemberUniqueIdentifier, TParameter> item)
        {
            int index = this.Keys.IndexOf(item.Key);
            if (index == -1)
                return false;
            return this.Values[index].Equals(item.Value);
        }

        public void CopyTo(KeyValuePair<IGeneralMemberUniqueIdentifier, TParameter>[] array, int arrayIndex = 0)
        {
            ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
            for (int i = 0; i < this.Count; i++)
                array[i + arrayIndex] = new KeyValuePair<IGeneralMemberUniqueIdentifier, TParameter>(this.Keys[i], this.Values[i]);
        }

        public KeyValuePair<IGeneralMemberUniqueIdentifier, TParameter> this[int index]
        {
            get
            {
                return new KeyValuePair<IGeneralMemberUniqueIdentifier, TParameter>(this.Keys[index], this.Values[index]);
            }
        }

        public KeyValuePair<IGeneralMemberUniqueIdentifier, TParameter>[] ToArray()
        {
            KeyValuePair<IGeneralMemberUniqueIdentifier, TParameter>[] result = new KeyValuePair<IGeneralMemberUniqueIdentifier, TParameter>[this.Count];
            this.CopyTo(result);
            return result;
        }

        public int IndexOf(KeyValuePair<IGeneralMemberUniqueIdentifier, TParameter> element)
        {
            int index = this.Keys.IndexOf(element.Key);
            if (index == -1)
                return -1;
            if (this.Values[index].Equals(element.Value))
                return index;
            return -1;
        }

        public IEnumerator<KeyValuePair<IGeneralMemberUniqueIdentifier, TParameter>> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
                yield return new KeyValuePair<IGeneralMemberUniqueIdentifier, TParameter>(this.Keys[i], this.Values[i]);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Dispose()
        {
            lock (this.syncObject)
            {
                for (int i = 0; i < this.Count; i++)
                    if (this.duplicates[i] != null)
                        this.duplicates[i].Dispose();
                this.duplicates = null;
            }
        }

        internal void CheckItemAt(int index)
        {
            TParameter currentElement;
            lock (syncObject)
                currentElement = this.duplicates[index];
            if (currentElement == null)
            {
                currentElement = this.GetParameter(this.delegateType.Parameters.Values[index]);
                lock (syncObject)
                    this.duplicates[index] = currentElement;
            }
        }

        protected abstract TParameter GetParameter(IDelegateTypeParameterMember delegateTypeParameterMember);

    }
}
