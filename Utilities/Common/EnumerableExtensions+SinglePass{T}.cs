using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities
{
    partial class EnumerableExtensions
    {
        private class SinglePassEnumerable<T> :
            IEnumerable<T>,
            IEnumerator<T>,
            IDisposable
        {
            private T _current;
            private IEnumerable<T> original;
            private IEnumerator<T> enumerator;
            private IEnumerable<T> finished;
            private List<T> itemCopy;
            private int state = StateInitial;
            private const int StateInitial = 0;
            private const int StateEnumerator = 1;
            private const int StateFinished = 2;
            private const int StateDisposed = 3;
            private SinglePassEnumerable<T> originalInst;
            public SinglePassEnumerable(IEnumerable<T> target, SinglePassEnumerable<T> originalInst = null)
            {
                var singlePassTarget = target as SinglePassEnumerable<T>;
                if (singlePassTarget != null)
                    if (singlePassTarget.state == StateFinished)
                    {
                        this.state = StateFinished;
                        this.finished = singlePassTarget.finished;
                    }
                    else
                        this.original = singlePassTarget.original;
                else
                    this.original = target;
                this.originalInst = originalInst ?? this;
            }

            #region IEnumerable<T> Members

            public IEnumerator<T> GetEnumerator()
            {
                switch (this.state)
                {
                    case StateInitial:
                        if (this.originalInst == this)
                            goto case StateEnumerator;
                        this.itemCopy = new List<T>();
                        this.state = StateEnumerator;
                        this.enumerator = this.original.GetEnumerator();
                        return this;
                    case StateEnumerator:
                        return new SinglePassEnumerable<T>(this.original, this.originalInst).GetEnumerator();
                    case StateFinished:
                        return this.finished.GetEnumerator();
                }
                throw new InvalidOperationException();
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion

            #region IEnumerator<T> Members

            public T Current
            {
                get { return this._current; }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                DisposeInternal();
            }

            private void DisposeInternal()
            {
                this.original = null;
                if (this.enumerator != null)
                {
                    this.enumerator.Dispose();
                    this.enumerator = null;
                }
                this._current = default(T);
                this.itemCopy = null;
                this.finished = null;
                this.state = StateDisposed;
            }

            #endregion

            #region IEnumerator Members

            object IEnumerator.Current
            {
                get { return this.Current; }
            }

            public bool MoveNext()
            {
                if (this.enumerator == null)
                    return false;
                bool b = this.enumerator.MoveNext();
                if (b)
                {
                    this._current = this.enumerator.Current;
                    this.itemCopy.Add(_current);
                }
                else
                {
                    this.state = StateFinished;
                    this.finished = itemCopy;
                    if (this.originalInst != null &&
                        this.originalInst != this && 
                        this.originalInst.state != StateFinished &&
                        this.originalInst.state != StateDisposed)
                    {
                        this.originalInst.state = StateFinished;
                        this.originalInst.finished = this.finished;
                    }
                }
                return b;
            }

            public void Reset()
            {
                switch (this.state)
                {
                    case StateEnumerator:
                        this.itemCopy.Clear();
                        this.enumerator = this.original.GetEnumerator();
                        this._current = default(T);
                        break;
                    case StateFinished:
                        this.enumerator = this.original.GetEnumerator();
                        this._current = default(T);
                        break;
                }
            }

            #endregion
        }
    }
}
