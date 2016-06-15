using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Tuples
{
    /// <summary>
    /// Denotes the selection from the <see cref="SelectiveTuple{T1, T2}"/>.
    /// </summary>
    public enum TuplePairSelection 
    {
        /// <summary>
        /// The active element is the <see cref="SelectiveTuple{T1, T2}.Item1"/> on the 
        /// <see cref="SelectiveTuple{T1, T2}"/>.
        /// </summary>
        Primary,
        /// <summary>
        /// The active element is the <see cref="SelectiveTuple{T1, T2}.Item2"/> on the 
        /// <see cref="SelectiveTuple{T1, T2}"/>.
        /// </summary>
        Secondary,
    }
    public class SelectiveTuple<T1, T2>
    {
        /// <summary>
        /// Returns the <typeparamref name="T1"/> value which denotes
        /// the value of the <see cref="SelectiveTuple{T1, T2}"/>.
        /// </summary>
        public T1 Item1 { get; private set; }
        /// <summary>
        /// Returns the <typeparamref name="T2"/> value which denotes
        /// the value of the <see cref="SelectiveTuple{T1, T2}"/>.
        /// </summary>
        public T2 Item2 { get; private set; }
        /// <summary>
        /// Returns the <see cref="TuplePairSelection"/> which denotes
        /// the value to yield from the <see cref="SelectiveTuple{T1, T2}"/>.
        /// </summary>
        public TuplePairSelection Selection { get; private set; }

        public SelectiveTuple(T1 item1)
        {
            this.Item1 = item1;
            this.Selection = TuplePairSelection.Primary;
        }

        public SelectiveTuple(T2 item2)
        {
            this.Item2 = item2;
            this.Selection = TuplePairSelection.Secondary;
        }

        /// <summary>
        /// Visits the appropriate visitor based off of the current
        /// <see cref="SelectiveTuple{T1, T2}.Selection"/>.
        /// </summary>
        /// <param name="primaryVisitor">The <see cref="Action{T}"/> which is used when
        /// the <see cref="Item1"/> is the selection.</param>
        /// <param name="secondaryVisitor">The <see cref="Action{T}"/> which is used when
        /// the <see cref="Item2"/> is the selection.</param>
        /// <param name="defaultVisitor">The <see cref="Action"/> which is used when
        /// the <see cref="SelectiveTuple{T1, T2}"/> is in an invalid state.</param>
        public void Visit(Action<T1> primaryVisitor, Action<T2> secondaryVisitor, Action defaultVisitor = null)
        {
            switch (Selection)
            {
                case TuplePairSelection.Primary:
                    if (primaryVisitor == null)
                        throw new ArgumentNullException("primaryVisitor");
                    primaryVisitor(this.Item1);
                    break;
                case TuplePairSelection.Secondary:
                    if (secondaryVisitor == null)
                        throw new ArgumentNullException("secondaryVisitor");
                    secondaryVisitor(this.Item2);
                    break;
                default:
                    if (defaultVisitor != null)
                        defaultVisitor();
                    return;
            }
        }
    }
}
