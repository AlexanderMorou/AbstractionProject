using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    public partial class LockedLinkedList<T> :
        ILockedLinkedList<T>
    {
        private Node[] series;

        public ILockedLinkedListNode<T> First
        {
            get { return series.FirstOrDefault(); }
        }

        public ILockedLinkedListNode<T> Last
        {
            get { return series.LastOrDefault(); }
        }

        public int IndexOf(ILockedLinkedListNode<T> node)
        {
            for (int i = 0; i < this.Count; i++)
                if (this.series[i] == node)
                    return i;
            return -1;
        }

        public IEnumerator<ILockedLinkedListNode<T>> GetEnumerator()
        {
            foreach (var node in this.series)
                yield return node;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Count
        {
            get { return this.series.Length; }
        }

        public LockedLinkedList(IEnumerable<T> nodeItems)
        {
            var copy = nodeItems.ToArray();
            this.series = new Node[copy.Length];
            for (int i = 0; i < copy.Length; i++)
            {
                this.series[i] = new Node(this, i, copy[i]);
            }
        }
    }
}
