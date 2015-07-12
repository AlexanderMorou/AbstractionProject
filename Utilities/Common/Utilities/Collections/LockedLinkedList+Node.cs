using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    partial class LockedLinkedList<T>
    {
        private class Node :
            ILockedLinkedListNode<T>
        {
            private LockedLinkedList<T> owner;
            public ILockedLinkedListNode<T> First
            {
                get { return this.owner.First; }
            }

            public ILockedLinkedListNode<T> Last
            {
                get { return this.owner.Last; }
            }

            public ILockedLinkedListNode<T> Previous
            {
                get
                {
                    if (this.Index == 0)
                        return null;
                    return this.owner.series[Index - 1];
                }
            }

            public ILockedLinkedListNode<T> Next
            {
                get {
                    if (this.Index == this.owner.Count - 1)
                        return null;
                    return this.owner.series[this.Index + 1];
                }
            }

            public T Element { get; private set; }

            public int Index { get; private set; }

            public Node(LockedLinkedList<T> owner, int index, T element)
            {
                this.owner = owner;
                this.Element = element;
                this.Index = index;
            }
        }
    }
}
