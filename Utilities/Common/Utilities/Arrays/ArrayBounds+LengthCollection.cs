using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Arrays
{
    partial class ArrayBounds
    {
        private class LengthCollection :
            IControlledCollection<int>
        {
            private ArrayBounds owner;

            internal LengthCollection(ArrayBounds owner) 
            {
                this.owner = owner; 
            }

            #region IControlledCollection<int> Members

            public int Count
            {
                get { return this.owner.Rank; }
            }

            public bool Contains(int item)
            {
                for (int i = 0; i < this.owner.Rank; i++) 
                    if (this.owner._upperBounds[i] - (this.owner._lowerBounds[i] - 1) == item)
                        return true;
                return false;
            }

            public void CopyTo(int[] array, int arrayIndex = 0)
            {
                ThrowHelper.CopyToCheck(array, arrayIndex, this.owner.Rank);
                for (int i = 0; i < this.owner.Rank; i++)
                    array[arrayIndex + i] = this.owner._upperBounds[i] - (this.owner._lowerBounds[i] - 1);
            }

            public int this[int index]
            {
                get {
                    return this.owner._upperBounds[index] - (this.owner._lowerBounds[index] - 1);
                }
            }

            public int[] ToArray()
            {
                int[] result = new int[this.owner.Rank];
                this.CopyTo(result);
                return result;
            }

            public int IndexOf(int element)
            {
                for (int i = 0; i < this.owner.Rank; i++)
                    if (this.owner._upperBounds[i] - (this.owner._lowerBounds[i] - 1) == element)
                        return i;
                return -1;
            }

            #endregion

            #region IEnumerable<int> Members

            public IEnumerator<int> GetEnumerator()
            {
                for (int i = 0; i < this.owner.Rank; i++)
                    yield return this.owner._upperBounds[i] - (this.owner._lowerBounds[i] - 1);
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion
        }

    }
}
