using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Tuples
{
    /// <summary>
    /// Denotes the selection from the <see cref="SelectiveTuple{T1, T2, T3}"/>.
    /// </summary>
    public enum TupleTrioSelection 
    {
        /// <summary>
        /// The active element is the <see cref="SelectiveTuple{T1, T2, T3}.Item1"/> on the 
        /// <see cref="SelectiveTuple{T1, T2, T3}"/>.
        /// </summary>
        Primary,
        /// <summary>
        /// The active element is the <see cref="SelectiveTuple{T1, T2, T3}.Item2"/> on the 
        /// <see cref="SelectiveTuple{T1, T2, T3}"/>.
        /// </summary>
        Secondary,
        /// <summary>
        /// The active element is the <see cref="SelectiveTuple{T1, T2, T3}.Item3"/> on the 
        /// <see cref="SelectiveTuple{T1, T2, T3}"/>.
        /// </summary>
        Tertiary,
    }
    public class SelectiveTuple<T1, T2, T3>
    {
        /// <summary>
        /// Returns the <typeparamref name="T1"/> value which denotes
        /// the primary value of the <see cref="SelectiveTuple{T1, T2, T3}"/>.
        /// </summary>
        public T1 Item1 { get; private set; }
        /// <summary>
        /// Returns the <typeparamref name="T2"/> value which denotes
        /// the secondary value of the <see cref="SelectiveTuple{T1, T2, T3}"/>.
        /// </summary>
        public T2 Item2 { get; private set; }
        /// <summary>
        /// Returns the <typeparamref name="T3"/> value which denotes
        /// the tertiary value of the <see cref="SelectiveTuple{T1, T2, T3}"/>.
        /// </summary>
        public T3 Item3 { get; private set; }
        /// <summary>
        /// Returns the <see cref="TupleTrioSelection"/> which denotes
        /// the value to yield from the <see cref="SelectiveTuple{T1, T2, T3}"/>.
        /// </summary>
        public TupleTrioSelection Selection { get; private set; }

        public SelectiveTuple(T1 item1)
        {
            this.Item1 = item1;
            this.Selection = TupleTrioSelection.Primary;
        }

        public SelectiveTuple(T2 item2)
        {
            this.Item2 = item2;
            this.Selection = TupleTrioSelection.Secondary;
        }


        public SelectiveTuple(T3 item3)
        {
            this.Item3 = item3;
            this.Selection = TupleTrioSelection.Tertiary;
        }

        public SelectiveTuple(T1 item1, T2 item2, T3 item3, TupleTrioSelection selection)
        {
            switch (selection)
            {
                case TupleTrioSelection.Primary:
                    this.Item1 = item1;
                    break;
                case TupleTrioSelection.Secondary:
                    this.Item2 = item2;
                    break;
                case TupleTrioSelection.Tertiary:
                    this.Item3 = item3;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("selection");
            }
        }
        /// <summary>
        /// Visits the appropriate visitor based off of the current
        /// <see cref="SelectiveTuple{T1, T2, T3}.Selection"/>.
        /// </summary>
        /// <param name="primaryVisitor">The <see cref="Action{T}"/> which is used when
        /// the <see cref="Item1"/> is the selection.</param>
        /// <param name="secondaryVisitor">The <see cref="Action{T}"/> which is used when
        /// the <see cref="Item2"/> is the selection.</param>
        /// <param name="tertiaryVisitor">The <see cref="Action{T}"/> which is used when
        /// the <see cref="Item3"/> is the selection.</param>
        /// <param name="defaultVisitor">The <see cref="Action"/> which is used when
        /// the <see cref="SelectiveTuple{T1, T2, T3}"/> is in an invalid state.</param>
        public void Visit(Action<T1> primaryVisitor, Action<T2> secondaryVisitor, Action<T3> tertiaryVisitor, Action defaultVisitor = null)
        {
            switch (Selection)
            {
                case TupleTrioSelection.Primary:
                    if (primaryVisitor == null)
                        throw new ArgumentNullException("primaryVisitor");
                    primaryVisitor(this.Item1);
                    break;
                case TupleTrioSelection.Secondary:
                    if (secondaryVisitor == null)
                        throw new ArgumentNullException("secondaryVisitor");
                    secondaryVisitor(this.Item2);
                    break;
                case TupleTrioSelection.Tertiary:
                    if (tertiaryVisitor == null)
                        throw new ArgumentNullException("secondVisitor");
                    tertiaryVisitor(this.Item3);
                    break;
                default:
                    if (defaultVisitor != null)
                        defaultVisitor();
                    return;
            }
        }
    }
}
