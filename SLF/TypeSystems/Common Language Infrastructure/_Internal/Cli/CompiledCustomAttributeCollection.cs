using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
/*----------------------------------------\
| Copyright © 2012 Allen Copeland Jr.     |
|-----------------------------------------|
| The Abstraction Project's code is prov- |
| -ided under a contract-release basis.   |
| DO NOT DISTRIBUTE and do not use beyond |
| the contract terms.                     |
\--------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CompiledCustomAttributeCollection :
        IReadOnlyCollection<ICustomAttributeInstance>,
        ICustomAttributeCollection,
        IReadOnlyCollection
    {
        private Attribute[] attributes;
        private ICustomAttributeInstance[] attributeWrappers;
        private ICustomAttributedDeclaration parent;
        private Func<bool, object[]> attributeObtainer;
        private ICustomAttributeInstance[] mruList;
        private const int mruListLength = 7;

        private Attribute[] Attributes
        {
            get
            {
                if (this.attributes == null)
                {
                    this.attributes = attributeObtainer(true).Cast<Attribute>().ToArray();
                    this.attributeObtainer = null;
                }
                return this.attributes;
            }
        }

        public void Dispose()
        {
            try
            {
                if (this.attributes != null)
                    this.attributes = null;
                else if (this.attributeObtainer != null)
                    this.attributeObtainer = null;
                this.mruList = null;
                this.parent = null;
                if (this.attributeWrappers != null)
                    foreach (var item in this.attributeWrappers)
                        item.Dispose();
                this.attributeWrappers = null;
            }
            finally
            {
                GC.SuppressFinalize(this);
            }
        }

        public CompiledCustomAttributeCollection(Func<bool, object[]> attributeObtainer)
        {
            this.attributeObtainer = attributeObtainer;
        }
        #region IControlledCollection<ICustomAttributeInstance> Members

        public int Count
        {
            get
            {
                if (this.Attributes != null)
                    return this.Attributes.Length;
                //Object is in an invalid state.
                throw new InvalidOperationException();
            }
        }

        public bool Contains(ICustomAttributeInstance item)
        {
            for (int i = 0; i < this.Count; i++)
                if (this.attributeWrappers[i] == item)
                    return true;
            /* *
             * CheckItemAt unnecessary, since the item they send in
             * would have to of been created by this class.
             * *
             * If it doesn't exist here, it's not the same.
             * */
            return false;
        }

        public void CopyTo(ICustomAttributeInstance[] array, int arrayIndex = 0)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            /* *
             * < 0 is a valid check since non-vector arrays are typed
             * elementType[*] not elementType[].
             * */
            if (arrayIndex < 0 || this.Count + arrayIndex > array.Length)
                throw new ArgumentOutOfRangeException("arrayIndex");
            for (int i = 0; i < this.Count; i++)
            {
                this.CheckItemAt(i);
                array[i + arrayIndex] = this.attributeWrappers[i];
            }
        }

        public ICustomAttributeInstance this[int index]
        {
            get
            {
                this.CheckItemAt(index);
                return this.attributeWrappers[index];
            }
        }

        public ICustomAttributeInstance[] ToArray()
        {
            ICustomAttributeInstance[] result = new ICustomAttributeInstance[this.Count];
            this.CopyTo(result, 0);
            return result;
        }

        #endregion

        private void CheckItemAt(int index)
        {
            if (this.attributeWrappers == null)
                this.attributeWrappers = new ICustomAttributeInstance[this.Count];
            if (this.attributeWrappers[index] == null)
                this.attributeWrappers[index] = new CompiledCustomAttributeInstance(Attributes[index], Parent);
        }

        #region IControlledCollection Members

        bool IControlledCollection.Contains(object item)
        {
            if (!(item is ICustomAttributeInstance))
                if (!(item is IType))
                    return false;
                else
                    return this.Contains((IType)(item));
            return this.Contains(((ICustomAttributeInstance)(item)));
        }

        void IControlledCollection.CopyTo(Array array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            /* *
             * < 0 is a valid check since non-vector arrays are typed
             * elementType[*] not elementType[].
             * */
            if (arrayIndex < 0 || this.Count + arrayIndex > array.Length)
                throw new ArgumentOutOfRangeException("arrayIndex");
            for (int i = 0; i < this.Count; i++)
            {
                this.CheckItemAt(i);
                array.SetValue(this.attributeWrappers[i], i + arrayIndex);
            }

        }

        object IControlledCollection.this[int index]
        {
            get { return this[index]; }
        }

        #endregion

        #region IEnumerable<ICustomAttributeInstance> Members

        public IEnumerator<ICustomAttributeInstance> GetEnumerator()
        {
            if (this.attributes == null ||
                this.attributes.Length == 0)
                yield break;
            for (int i = 0; i < this.Count; i++)
            {
                this.CheckItemAt(i);
                yield return this.attributeWrappers[i];
            }
            yield break;
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region ICustomAttributeCollection Members

        public bool Contains(IType attributeType)
        {
            for (int i = 0; i < this.Count; i++)
            {
                this.CheckItemAt(i);
                if (this.attributeWrappers[i].Type.Equals(attributeType))
                    return true;
                else if (attributeType.IsAssignableFrom(this.attributeWrappers[i].Type))
                    return true;
            }
            return false;
        }

        public ICustomAttributedDeclaration Parent
        {
            get { return this.parent; }
        }

        #endregion

        #region ICustomAttributeCollection Members

        public ICustomAttributeInstance this[IType attributeType]
        {
            get
            {
                ICustomAttributeInstance mruClose = null;
                if (this.mruList == null)
                    mruList = new ICustomAttributeInstance[mruListLength];
                for (int i = 0; i < mruListLength; i++)
                {
                    if (mruList[i] == null)
                        break;
                    if (mruList[i].Type == attributeType)
                    {
                        if (i == 0)
                            //Avoid unnecessary work.
                            return mruList[0];
                        /* *
                         * Use the close as a temporary variable...
                         * */
                        mruClose = mruList[i];
                        /* *
                         * Squeeze the item out of the place it's at...
                         * */
                        for (int j = i + 1; j < mruListLength; j++)
                            mruList[j - 1] = mruList[j];
                        /* *
                         * Shift them all down, and then set the first to the
                         * current.
                         * */
                        ShiftMRU();
                        return (mruList[0] = mruClose);
                    }
                    if (attributeType.IsAssignableFrom(mruList[i].Type) && mruClose == null)
                        mruClose = mruList[i];
                }
                ICustomAttributeInstance fullClose = null;
                for (int i = 0; i < this.Count; i++)
                {
                    var item = this[i];
                    if (item.Type == attributeType)
                    {
                        ShiftMRU();
                        mruList[0] = item;
                        return item;
                    }
                    else if (attributeType.IsAssignableFrom(item.Type) && fullClose == null)
                        fullClose = item;
                }
                //Potentially null.
                if (fullClose != null)
                {
                    if (mruClose != fullClose)
                    {
                        ShiftMRU();
                        mruList[0] = fullClose;
                    }
                }
                return fullClose ?? mruClose;
            }
        }

        private void ShiftMRU()
        {
            for (int i = mruListLength - 1; i >= 1; i--)
                mruList[i] = mruList[i - 1];
        }


        #endregion

        #region IControlledCollection<ICustomAttributeInstance> Members

        public int IndexOf(ICustomAttributeInstance element)
        {
            for (int i = 0; i < this.Count; i++)
                if (this.attributeWrappers[i] == element)
                    return i;
            return -1;
        }

        #endregion

        #region IControlledCollection Members


        int IControlledCollection.IndexOf(object element)
        {
            if (element is ICustomAttributeInstance)
                return this.IndexOf((ICustomAttributeInstance)element);
            return -1;
        }

        #endregion
    }
}
