using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliMetadataCollection :
        IMetadataCollection
    {
        private object syncObject = new object();
        private IMetadatum[] metadataSet;
        private IControlledCollection<ICliMetadataCustomAttributeTableRow> sourceData;
        IMetadataEntity declarationPoint;
        private _ICliManager identityManager;
        internal CliMetadataCollection(IControlledCollection<ICliMetadataCustomAttributeTableRow> sourceData, IMetadataEntity declarationPoint, _ICliManager identityManager)
        {
            this.declarationPoint = declarationPoint;
            this.sourceData = sourceData;
            this.metadataSet = new IMetadatum[this.Count];
            this.identityManager = identityManager;
        }

        private void CheckItemAt(int index)
        {
            lock (syncObject)
                if (this.metadataSet[index] == null)
                    this.metadataSet[index] = this.CreateMetadatum(this.sourceData[index]);
        }

        private IMetadatum CreateMetadatum(ICliMetadataCustomAttributeTableRow metadataEntry)
        {
            return new CliMetadatum(metadataEntry, this.identityManager, this.declarationPoint);
        }

        public bool Contains(IType metadatumType)
        {
            for (int metadatumIndex = 0; metadatumIndex < this.Count; metadatumIndex++)
            {
                lock (syncObject)
                {
                    this.CheckItemAt(metadatumIndex);
                    if (this.metadataSet[metadatumIndex].Type == metadatumType)
                        return true;
                }
            }
            return false;
        }

        public IMetadataEntity Parent
        {
            get { return this.declarationPoint; }
        }

        public IMetadatum this[IType metadatumType]
        {
            get {
                for (int metadatumIndex = 0; metadatumIndex < this.Count; metadatumIndex++)
                {
                    lock (syncObject)
                    {
                        this.CheckItemAt(metadatumIndex);
                        if (this.metadataSet[metadatumIndex].Type == metadatumType)
                            return this.metadataSet[metadatumIndex];
                    }
                }
                return null;
            }
        }

        public int Count
        {
            get { return this.sourceData.Count; }
        }

        public bool Contains(IMetadatum item)
        {
            lock (syncObject)
                return this.metadataSet.Contains(item);
        }

        public void CopyTo(IMetadatum[] array, int arrayIndex = 0)
        {
            ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
            for (int metadatumIndex = 0; metadatumIndex < this.Count; metadatumIndex++)
            {
                lock (syncObject)
                {
                    this.CheckItemAt(metadatumIndex);
                    array[arrayIndex + metadatumIndex] = this.metadataSet[metadatumIndex];
                }
            }
        }

        public IMetadatum this[int index]
        {
            get {
                if (index < 0 || index >= this.Count)
                    throw new ArgumentOutOfRangeException("index");
                lock (syncObject)
                {
                    this.CheckItemAt(index);
                    return this.metadataSet[index];
                }
            }
        }

        public IMetadatum[] ToArray()
        {
            IMetadatum[] result = new IMetadatum[this.Count];
            this.CopyTo(result);
            return result;
        }

        public int IndexOf(IMetadatum element)
        {
            lock (syncObject)
            {
                for (int metadatumIndex = 0; metadatumIndex < this.Count; metadatumIndex++)
                    if (this.metadataSet[metadatumIndex] == element)
                        return metadatumIndex;
            }
            return -1;
        }

        public IEnumerator<IMetadatum> GetEnumerator()
        {
            for (int metadatumIndex = 0; metadatumIndex < this.Count; metadatumIndex++)
            {
                IMetadatum current = null;
                lock (syncObject)
                {
                    this.CheckItemAt(metadatumIndex);
                    current = this.metadataSet[metadatumIndex];
                }
                yield return current;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Dispose()
        {
            lock (this.syncObject)
            {
                for (int metadatumIndex = 0, count = this.Count; metadatumIndex < count; metadatumIndex++)
                {
                    var current = this.metadataSet[metadatumIndex];
                    if (current != null)
                        current.Dispose();
                }
            }
            this.metadataSet = null;
            this.sourceData = null;
        }
    }
}
