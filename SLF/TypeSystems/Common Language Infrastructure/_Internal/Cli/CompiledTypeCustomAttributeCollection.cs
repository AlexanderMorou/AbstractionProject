using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
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
    internal class CompiledTypeCustomAttributeCollection :
        IReadOnlyCollection<IMetadatum>,
        IMetadataCollection,
        IReadOnlyCollection
    {
        private ICompiledType parent;
        private Attribute[] attributes;
        private IMetadatum[] attributeWrappers;
        private Dictionary<Attribute, Type> parentRelationships;
        private IMetadatum[] mruList;
        private const int mruListLength = 7;

        private ICliManager manager;


        public CompiledTypeCustomAttributeCollection(ICompiledType parent)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");
            parentRelationships = CliAssist.GetHierarchicalMap(parent.UnderlyingSystemType);
            this.parent = parent;
            attributeWrappers = new IMetadatum[parentRelationships.Count];
        }
        
        #region IDisposable Members

        public void Dispose()
        {
            if (this.parentRelationships != null)
            {
                this.parentRelationships.Clear();
                this.parentRelationships = null;
            }
            this.parent = null;
            this.attributes = null;
        }

        #endregion

        #region IControlledCollection<IMetadatum> Members

        public int Count
        {
            get
            {
                if (this.attributes == null &&
                    this.parentRelationships != null)
                    return this.parentRelationships.Count;
                else if (this.attributes != null)
                    return this.attributes.Length;
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

        public int IndexOf(IMetadatum element)
        {
            for (int i = 0; i < this.Count; i++)
                if (this.attributeWrappers[i] == element)
                    return i;
            return -1;
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
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        private void CheckItemAt(int index)
        {
            if (this.attributeWrappers[index] == null)
            {
                if (this.attributes != null)
                    this.attributeWrappers[index] = new CompiledCustomAttributeInstance(this.attributes[index], this.parent, this.Parent.Manager);
                else if (this.parentRelationships != null)
                {
                    var item = this.parentRelationships.ElementAt(index);
                    this.attributeWrappers[index] = new CompiledCustomAttributeInstance(item.Key, this.parent.Manager.ObtainTypeReference(item.Value), this.Parent.Manager);
                }
                else
                    //Object in an invalid state, possibly disposed.
                    throw new InvalidOperationException();
            }
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

        int IControlledCollection.IndexOf(object element)
        {
            if (element is IMetadatum)
                return this.IndexOf((IMetadatum)element);
            return -1;
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
            }
            return false;
        }

        public ICompiledType Parent { get { return this.parent; } }

        IMetadataEntity IMetadataCollection.Parent
        {
            get { return this.Parent; }
        }

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
    }
}
