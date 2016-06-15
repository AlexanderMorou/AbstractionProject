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

        public virtual void AddRange(IEnumerable<IType> types)
        {
            foreach (var element in types)
                this.Add(element);
        }

        IType IControlledCollection<IType>.this[int index]
        {
            get
            {
                return base.baseList[index];
            }
        }

        IType ITypeCollection.this[int index]
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

        public new virtual T this[int index]
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

        public virtual void Insert(int index, IType item)
        {
            if (item is T)
                base.baseList.Insert(index, (T)item);
            else
                throw new ArgumentException(string.Format("Must reference an '{0}' for implementations.", typeof(T).FullName), "item");
        }

        public virtual void RemoveAt(int index)
        {
            base.baseList.RemoveAt(index);
        }

        public virtual void RemoveRange(int index, int count)
        {
            if (index + count > this.Count)
                throw new ArgumentOutOfRangeException("index");
            for (int secondIndex = index; secondIndex < index + count; secondIndex++)
                base.baseList.RemoveAt(index);
        }

        public virtual void Add(IType item)
        {
            if (item is T)
                base.baseList.Add((T)item);
            else
                throw new ArgumentException(string.Format("Must reference an '{0}' for implementations.", typeof(T).FullName), "item");
        }

        public virtual bool Remove(IType item)
        {
            if (item is T)
                return base.baseList.Remove((T)item);
            return false;
        }

        public virtual void Clear()
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

    internal class TypeCollectionWithEvents<T> :
        TypeCollection<T>,
        IDisposable
        where T :
            IType
    {
        private EventHandler globalDelta;
        public TypeCollectionWithEvents() { }
        public TypeCollectionWithEvents(EventHandler globalDelta)
        {
            if (globalDelta != null)
            {
                this.ItemAdded += globalDelta;
                this.ItemsAdded += globalDelta;
                this.ItemChanged += globalDelta;
                this.ItemRemoved += globalDelta;
                this.ItemsRemoved += globalDelta;
                this.Cleared += globalDelta;
                this.globalDelta = globalDelta;
            }
        }
        public event EventHandler ItemAdded;
        public event EventHandler ItemsAdded;
        public event EventHandler ItemChanged;
        public event EventHandler ItemRemoved;
        public event EventHandler ItemsRemoved;
        public event EventHandler Cleared;
        private bool suspendSingleEvent = false;
        protected virtual void OnItemAdded()
        {
            if (suspendSingleEvent)
                return;
            var itemAdded = this.ItemAdded;
            if (itemAdded != null)
                itemAdded(this, EventArgs.Empty);
        }

        protected virtual void OnItemsAdded()
        {
            var itemsAdded = this.ItemsAdded;
            if (itemsAdded != null)
                itemsAdded(this, EventArgs.Empty);
        }

        protected virtual void OnItemChanged()
        {
            var itemChanged = this.ItemChanged;
            if (itemChanged != null)
                itemChanged(this, EventArgs.Empty);
        }

        protected virtual void OnItemRemoved()
        {
            if (suspendSingleEvent)
                return;
            var itemRemoved = this.ItemRemoved;
            if (itemRemoved != null)
                itemRemoved(this, EventArgs.Empty);
        }

        protected virtual void OnItemsRemoved()
        {
            var itemsRemoved = this.ItemsRemoved;
            if (itemsRemoved != null)
                itemsRemoved(this, EventArgs.Empty);
        }

        protected virtual void OnCleared()
        {
            var cleared = this.Cleared;
            if (cleared != null)
                cleared(this, EventArgs.Empty);
        }

        public override void Add(IType item)
        {
            base.Add(item);
            this.OnItemAdded();
        }

        public override void Clear()
        {
            base.Clear();
            this.OnCleared();
        }

        public override T this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                base[index] = value;
                this.OnItemChanged();
            }
        }

        public override bool Remove(IType item)
        {
            bool result = base.Remove(item);
            if (result)
                this.OnItemRemoved();
            return result;
        }

        public override void RemoveAt(int index)
        {
            base.RemoveAt(index);
            this.OnItemRemoved();
        }

        public override void RemoveRange(int index, int count)
        {
            this.suspendSingleEvent = true;
            try
            {
                base.RemoveRange(index, count);
                this.OnItemsRemoved();
            }
            finally
            {
                this.suspendSingleEvent = false;
            }
        }

        public override void Insert(int index, IType item)
        {
            base.Insert(index, item);
            this.OnItemAdded();
        }

        public override void AddRange(IEnumerable<IType> types)
        {
            this.suspendSingleEvent = true;
            try
            {
                base.AddRange(types);
            }
            finally
            {
                this.suspendSingleEvent = false;
            }
        }


        public void Dispose()
        {
            if (this.globalDelta != null)
            {
                this.ItemAdded -= globalDelta;
                this.ItemsAdded -= globalDelta;
                this.ItemChanged -= globalDelta;
                this.ItemRemoved -= globalDelta;
                this.ItemsRemoved -= globalDelta;
                this.Cleared -= globalDelta;
            }
        }
    }
}
