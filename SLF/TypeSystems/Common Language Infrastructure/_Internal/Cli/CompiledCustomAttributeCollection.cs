using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli;
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
        IReadOnlyCollection<IMetadatum>,
        IMetadataCollection,
        IReadOnlyCollection
    {
        private Attribute[] attributes;
        private IMetadatum[] attributeWrappers;
        private IMetadataEntity parent;
        private Func<bool, object[]> attributeObtainer;
        private IMetadatum[] mruList;
        private const int mruListLength = 7;

        private ICliManager manager;

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

        public ICliManager Manager { get { return this.manager; } }

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

        public CompiledCustomAttributeCollection(Func<bool, object[]> attributeObtainer, ICliManager manager)
        {
            this.attributeObtainer = attributeObtainer;
            this.manager = manager;
        }
        #region IControlledCollection<IMetadatum> Members

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

        public bool Contains(IMetadatum item)
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

        public void CopyTo(IMetadatum[] array, int arrayIndex = 0)
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

        public IMetadatum this[int index]
        {
            get
            {
                this.CheckItemAt(index);
                return this.attributeWrappers[index];
            }
        }

        public IMetadatum[] ToArray()
        {
            IMetadatum[] result = new IMetadatum[this.Count];
            this.CopyTo(result, 0);
            return result;
        }

        #endregion

        private void CheckItemAt(int index)
        {
            if (this.attributeWrappers == null)
                this.attributeWrappers = new IMetadatum[this.Count];
            if (this.attributeWrappers[index] == null)
                this.attributeWrappers[index] = new CompiledCustomAttributeInstance(Attributes[index], Parent, this.Manager);
        }

        #region IControlledCollection Members

        bool IControlledCollection.Contains(object item)
        {
            if (!(item is IMetadatum))
                if (!(item is IType))
                    return false;
                else
                    return this.Contains((IType)(item));
            return this.Contains(((IMetadatum)(item)));
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

        #region IEnumerable<IMetadatum> Members

        public IEnumerator<IMetadatum> GetEnumerator()
        {
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

        #region IMetadataCollection Members

        public bool Contains(IType metadatumType)
        {
            for (int i = 0; i < this.Count; i++)
            {
                this.CheckItemAt(i);
                if (this.attributeWrappers[i].Type.Equals(metadatumType))
                    return true;
                else if (metadatumType.IsAssignableFrom(this.attributeWrappers[i].Type))
                    return true;
            }
            return false;
        }

        public IMetadataEntity Parent
        {
            get { return this.parent; }
        }

        #endregion

        #region IMetadataCollection Members

        public IMetadatum this[IType metadatumType]
        {
            get
            {
                IMetadatum mruClose = null;
                if (this.mruList == null)
                    mruList = new IMetadatum[mruListLength];
                for (int i = 0; i < mruListLength; i++)
                {
                    if (mruList[i] == null)
                        break;
                    if (mruList[i].Type == metadatumType)
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
                    if (metadatumType.IsAssignableFrom(mruList[i].Type) && mruClose == null)
                        mruClose = mruList[i];
                }
                IMetadatum fullClose = null;
                for (int i = 0; i < this.Count; i++)
                {
                    var item = this[i];
                    if (item.Type == metadatumType)
                    {
                        ShiftMRU();
                        mruList[0] = item;
                        return item;
                    }
                    else if (metadatumType.IsAssignableFrom(item.Type) && fullClose == null)
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

        #region IControlledCollection<IMetadatum> Members

        public int IndexOf(IMetadatum element)
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
            if (element is IMetadatum)
                return this.IndexOf((IMetadatum)element);
            return -1;
        }

        #endregion
    }
}
