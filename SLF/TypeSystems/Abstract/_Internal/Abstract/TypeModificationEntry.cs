using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    internal class TypeModifierSetEntry :
        HashList<TypeModification>,
        ITypeModifierSetEntry
    {
        public TypeModifierSetEntry(IEnumerable<TypeModification> modifications)
            : base(modifications) { }

        #region ITypeModificationEntry Members

        public IEnumerable<bool> EnumerateRequirementState()
        {
            return from modification in this
                   select modification.IsRequiredType;
        }

        public IEnumerable<IType> EnumerateModifierTypes()
        {
            return from modification in this
                   select modification.ModifierType;
        }

        #endregion

        #region IEquatable<ITypeModificationEntry> Members

        public bool Equals(ITypeModifierSetEntry other)
        {
            if (other.Count != this.Count)
                return false;
            return ((ITypeModifierSetEntry) this).EnumerateRequirementState().SequenceEqual(other.EnumerateRequirementState()) &&
                   ((ITypeModifierSetEntry) this).EnumerateModifierTypes().SequenceEqual(other.EnumerateModifierTypes());
            return this.SequenceEqual(other);

            //if (other == null)
            //    return false;
            //if (other.Count != this.Count)
            //    return false;

            //TypeModification  thisCurrent = default(TypeModification),
            //                 otherCurrent = default(TypeModification);
            //bool                 thisNext = false, 
            //                    otherNext = false;
            //for (IEnumerator<TypeModification> thisEnum = this.GetEnumerator(), otherEnum = other.GetEnumerator(); (thisNext = thisEnum.MoveNext()) && (otherNext = otherEnum.MoveNext()); )
            //{
            //    thisCurrent = thisEnum.Current;
            //    otherCurrent = otherEnum.Current;
            //    if (!thisCurrent.Equals(otherCurrent))
            //        return false;
            //}
            ///* *
            // * The other entry's enumerator state machine
            // * yielded a different number of elements than is expected
            // * by the count specified.
            // * */
            //if (otherNext != thisNext)
            //    return false;
            //return true;
        }

        #endregion

        public override bool Equals(HashList<TypeModification> other)
        {
            if (other is ITypeModifierSetEntry)
                return this.Equals((ITypeModifierSetEntry) other);
            return base.Equals(other);
        }
    }
}
