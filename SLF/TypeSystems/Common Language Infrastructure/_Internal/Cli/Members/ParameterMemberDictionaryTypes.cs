using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class ParameterMemberDictionaryTypes<TParent, TParameter> :
        ITypeCollectionBase
        where TParent :
            IParameterParent<TParent, TParameter>
        where TParameter :
            IParameterMember<TParent>
    {
        private IParameterMemberDictionary<TParent, TParameter> owner;

        public ParameterMemberDictionaryTypes(IParameterMemberDictionary<TParent, TParameter> owner)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");
            this.owner = owner;
        }

        #region ITypeCollectionBase Members

        public int IndexOf(IType item)
        {
            using (var enumerator = this.owner.Values.GetEnumerator())
                for (int i = 0; enumerator.MoveNext(); i++)
                    if (enumerator.Current.ParameterType == item)
                        return i;
            return -1;
        }

        #endregion

        #region IControlledStateCollection<IType> Members

        public int Count
        {
            get { return this.owner.Count; }
        }

        public bool Contains(IType item)
        {
            foreach (var parameter in this.owner.Values)
                if (item == parameter.ParameterType)
                    return true;
            return false;
        }

        public void CopyTo(IType[] array, int arrayIndex = 0)
        {
            if (arrayIndex < 0 || this.Count + arrayIndex > array.Length)
                throw new ArgumentOutOfRangeException("arrayIndex");
            if (this.Count + arrayIndex > array.Length)
                throw new ArgumentOutOfRangeException("arrayIndex");
            using (var enumerator = this.owner.Values.GetEnumerator())
                for (int i = 0; enumerator.MoveNext(); i++)
                    array[arrayIndex + i] = enumerator.Current.ParameterType;
        }

        public IType this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Count)
                    throw new ArgumentOutOfRangeException("index");
                return this.owner.Values[index].ParameterType;
            }
        }

        public IType[] ToArray()
        {
            int c = this.Count;
            IType[] result = new IType[c];
            this.CopyTo(result);
            return result;
        }

        #endregion

        #region IEnumerable<IType> Members

        public IEnumerator<IType> GetEnumerator()
        {
            foreach (var parameter in this.owner.Values)
                yield return parameter.ParameterType;
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IEquatable<ITypeCollectionBase> Members

        public bool Equals(ITypeCollectionBase other)
        {
            if (other == null)
                return false;
            if (object.ReferenceEquals(this, other))
                return true;
            if (other.Count != this.Count)
                return false;
            return this.SequenceEqual(other);
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj is ITypeCollectionBase)
                return this.Equals((ITypeCollectionBase)(obj));
            return false;
        }

        public override int GetHashCode()
        {
            return this.Count.GetHashCode();
        }
    }
}
