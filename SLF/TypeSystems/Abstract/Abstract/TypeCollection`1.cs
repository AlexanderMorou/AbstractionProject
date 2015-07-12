using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    public class TypeCollection<T> :
        ControlledCollection<T>,
        ITypeCollection
        where T :
            IType
    {

        public void AddRange(IType[] types)
        {
            this.AddRange((IEnumerable<IType>)types);
        }

        public void AddRange(IEnumerable<IType> types)
        {
            foreach (var element in types)
                this.Add(element);
        }

        public new IType this[int index]
        {
            get
            {
                return base.baseList[index];
            }
            set
            {
                if (value is T)
                    base.baseList[index] = (T)value;
                else
                    throw new ArgumentException(string.Format("Must reference an '{0}' for implementations.", typeof(T).FullName), "value");
            }
        }

        public void Insert(int index, IType item)
        {
            if (item is T)
                base.baseList.Insert(index, (T)item);
            else
                throw new ArgumentException(string.Format("Must reference an '{0}' for implementations.", typeof(T).FullName), "item");
        }

        public void RemoveAt(int index)
        {
            base.baseList.RemoveAt(index);
        }

        public void RemoveRange(int index, int count)
        {
            if (index + count > this.Count)
                throw new ArgumentOutOfRangeException("index");
            for (int secondIndex = index; secondIndex < index + count; secondIndex++)
                base.baseList.RemoveAt(index);
        }

        public void Add(IType item)
        {
            if (item is T)
                base.baseList.Add((T)item);
            else
                throw new ArgumentException(string.Format("Must reference an '{0}' for implementations.", typeof(T).FullName), "item");
        }

        public bool Remove(IType item)
        {
            if (item is T)
                return base.baseList.Remove((T)item);
            return false;
        }

        public void Clear()
        {
            base.baseList.Clear();
        }

        public int IndexOf(IType type)
        {
            if (type is T)
                return base.baseList.IndexOf((T)type);
            return -1;
        }


        public bool Contains(IType item)
        {
            if (item is T)
                return base.baseList.Contains((T)item);
            return false;
        }

        public void CopyTo(IType[] array, int arrayIndex = 0)
        {
            ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
            for (int index = 0; index < this.Count; index++)
                array[arrayIndex + index] = this.baseList[index];
        }

        public new IType[] ToArray()
        {
            IType[] result = new IType[this.Count];
            this.CopyTo(result, 0);
            return result;
        }

        public new IEnumerator<IType> GetEnumerator()
        {
            foreach (var element in base.baseList)
                yield return element;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool Equals(IControlledTypeCollection other)
        {
            return object.ReferenceEquals(this, other);
        }
    }
}
