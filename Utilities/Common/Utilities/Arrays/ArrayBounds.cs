using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Arrays
{
    public partial class ArrayBounds
    {
        private int[] _lowerBounds;
        private int[] _upperBounds;
        private IControlledCollection<int> lowerBounds;
        private IControlledCollection<int> upperBounds;
        private LengthCollection lengths;

        public IControlledCollection<int> Lengths 
        { 
            get 
            {
                return this.lengths ?? (this.lengths = new LengthCollection(this)); 
            }
        }

        public IControlledCollection<int> LowerBounds
        {
            get
            {
                if (this.lowerBounds == null)
                    this.lowerBounds = new ArrayReadOnlyCollection<int>(this._lowerBounds);
                return this.lowerBounds;
            }
        }

        public IControlledCollection<int> UpperBounds
        {
            get
            {
                if (this.upperBounds == null)
                    this.upperBounds = new ArrayReadOnlyCollection<int>(this._upperBounds);
                return this.upperBounds;
            }
        }

        internal ArrayBounds(Array a)
        {
            this.Rank = a.Rank;
            this._lowerBounds = new int[this.Rank];
            this._upperBounds = new int[this.Rank];
            for (int i = 0; i < this.Rank; i++)
            {
                this._lowerBounds[i] = a.GetLowerBound(i);
                this._upperBounds[i] = a.GetUpperBound(i);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is ArrayBounds)
                return this.Equals((ArrayBounds)obj);
            return false;
        }

        public static bool operator ==(ArrayBounds left, ArrayBounds right)
        {
            if (left == null && right == null)
                return true;
            if (left != null && right == null)
                return false;
            if (left == null && right != null)
                return false;
            if (object.ReferenceEquals(left, right))
                return true;
            return left.Equals(right);
        }

        public static bool operator !=(ArrayBounds left, ArrayBounds right)
        {
            if (left == null && right == null)
                return false;
            if (left != null && right == null)
                return true;
            if (left == null && right != null)
                return true;
            if (object.ReferenceEquals(left, right))
                return false;
            return left.NotEquals(right);
        }

        public bool NotEquals(ArrayBounds other)
        {
            if (other == null)
                return true;
            if ((other._lowerBounds == null ^
                 this._lowerBounds == null))
                return true;
            if ((other._upperBounds == null ^
                 this._upperBounds == null))
                return true;
            if (this._lowerBounds == null ||
                this._upperBounds == null)
                return true;
            if (other._upperBounds.Length != this._upperBounds.Length ||
                other._lowerBounds.Length != this._lowerBounds.Length)
                return true;
            int[] tLB = this._lowerBounds,
                  oLB = other._lowerBounds,
                  tUB = this._upperBounds,
                  oUB = other._upperBounds;
            for (int i = 0; i < tLB.Length; i++)
                if (tLB[i] != oLB[i])
                    return true;
            for (int i = 0; i < tUB.Length; i++)
                if (tUB[i] != oUB[i])
                    return true;
            return false;
        }

        public bool Equals(ArrayBounds other)
        {
            if (other == null)
                return false;
            if ((other._lowerBounds == null ^
                 this._lowerBounds == null))
                return false;
            if ((other._upperBounds == null ^
                 this._upperBounds == null))
                return false;
            if (this._lowerBounds == null ||
                this._upperBounds == null)
                return false;
            if (other._upperBounds.Length != this._upperBounds.Length ||
                other._lowerBounds.Length != this._lowerBounds.Length)
                return false;
            int[] tLB = this._lowerBounds,
                  oLB = other._lowerBounds,
                  tUB = this._upperBounds,
                  oUB = other._upperBounds;
            for (int i = 0; i < tLB.Length; i++)
                if (tLB[i] != oLB[i])
                    return false;
            for (int i = 0; i < tUB.Length; i++)
                if (tUB[i] != oUB[i])
                    return false;
            return true;
        }

        public int Rank { get; private set; }

    }
}
