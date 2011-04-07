using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Oil;

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    public abstract class AggregateIdentityNode :
        IAggregateIdentityNode
    {
        private IAggregateIdentityNode parent;

        /// <summary>
        /// Focused at creating a parentless <see cref="AggregateIdentityNode"/>.
        /// </summary>
        internal AggregateIdentityNode()
        {
        }

        #region IAggregateIdentityNode Members
        /// <summary>
        /// Returns the name of the identity associated to the 
        /// node at the current level.
        /// </summary>
        public abstract string Name { get; }

        public IAggregateIdentityNode Parent
        {
            get { return this.parent; }
        }


        /// <summary>
        /// Returns the <see cref="String"/> which represents
        /// a fully qualified identifier of the identity, or
        /// a path to said identity.
        /// </summary>
        public string FullName
        {
            get
            {
                /* *
                 * Build a stack of the elements within the current path.
                 * */
                Stack<IAggregateIdentityNode> fullPath = new Stack<IAggregateIdentityNode>();
                for (IAggregateIdentityNode current = this; current != null; current = current.Parent)
                    fullPath.Push(current);
                /* *
                 * Pop each identity node from the stack and build a full path.
                 * */
                StringBuilder resultPath = new StringBuilder();
                bool first = false;
                while (fullPath.Count > 0)
                {
                    if (first)
                        first = false;
                    else
                        resultPath.Append(".");
                    resultPath.Append(fullPath.Pop().Name);
                }
                /* *
                 * Recursion might be more elegant via
                 * return Parent.FullName + this.Name; however,
                 * sometimes I prefer to avoid it on linear structures.
                 * */
                return resultPath.ToString();
            }
        }


        /// <summary>
        /// Returns the <see cref="AggregateIdentityKind"/> which denotes
        /// the kind of identity represented by the current <see cref="AggregateIdentityNode"/>.
        /// </summary>
        public abstract AggregateIdentityKind Kind { get; }

        #endregion

    }
}
