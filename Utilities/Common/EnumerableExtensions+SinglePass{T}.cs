using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace AllenCopeland.Abstraction.Utilities
{
    partial class EnumerableExtensions
    {
        private class SinglePassEnumerable<T> :
            IEnumerable<T>,
            IEnumerator<T>
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
            public SinglePassEnumerable(IEnumerable<T> target)
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
            }

            #region IEnumerable<T> Members

            public IEnumerator<T> GetEnumerator()
            {
                switch (this.state)
                {
                    case StateInitial:
                        this.itemCopy = new List<T>();
                        this.state = StateEnumerator;
                        this.enumerator = this.original.GetEnumerator();
                        return this;
                    case StateEnumerator:
                        return new SinglePassEnumerable<T>(this.original).GetEnumerator();
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
                if (this.state == StateFinished)
                {
                    this.original = null;
                    if (this.enumerator != null)
                    {
                        this.enumerator.Dispose();
                        this.enumerator = null;
                    }
                    this._current = default(T);
                }
                else
                    this.Reset();
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
                        break;
                    case StateFinished:
                        this.enumerator = this.original.GetEnumerator();
                        break;
                }
            }

            #endregion
        }
    }
}
