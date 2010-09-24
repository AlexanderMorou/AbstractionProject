using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public class FixedLengthMalleableExpressionCollection :
        IFixedLengthMalleableExpressionCollection
    {
        private IExpression[] data;

        public FixedLengthMalleableExpressionCollection(int count)
        {
            this.data = new IExpression[count];
        }

        public FixedLengthMalleableExpressionCollection(params IExpression[] expressions)
        {
            if (expressions == null)
                throw new ArgumentNullException("expressions");
            this.data = new IExpression[expressions.Length];
            expressions.CopyTo(this.data, 0);
        }

        #region IFixedLengthMalleableExpressionCollection Members

        public IExpression this[int index]
        {
            get
            {
                if (index < 0 || index > this.Count)
                    throw new ArgumentOutOfRangeException("index");
                return this.data[index];
            }
            set
            {
                if (index < 0 || index > this.Count)
                    throw new ArgumentOutOfRangeException("index");
                this.data[index] = value;
            }
        }

        #endregion

        #region IControlledStateCollection<IExpression> Members

        public int Count
        {
            get {
                if (this.data == null)
                    return 0;
                return this.data.Length;
            }
        }

        public bool Contains(IExpression item)
        {
            if (this.data == null)
                return false;
            foreach (var datum in data)
                if (datum == item)
                    return true;
            return false;
        }

        public void CopyTo(IExpression[] array, int arrayIndex)
        {
            if (this.data == null)
                return;
            this.data.CopyTo(array, arrayIndex);
        }

        public IExpression[] ToArray()
        {
            if (this.data == null)
                return new IExpression[0];
            IExpression[] result = new IExpression[this.data.Length];
            this.data.CopyTo(result, 0);
            return result;
        }

        #endregion

        #region IEnumerable<IExpression> Members

        public IEnumerator<IExpression> GetEnumerator()
        {
            if (this.data == null)
                yield break;
            foreach (var datum in this.data)
                yield return datum;
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IControlledStateCollection<IExpression> Members

        public int IndexOf(IExpression element)
        {
            int index = 0;
            if (this.data == null)
                return -1;
            foreach (var datum in data)
                if (datum == element)
                    return index;
                else
                    index++;
            return -1;
        }

        #endregion
    }
}
