using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class LockedEmptyMetadataCollection :
        ReadOnlyCollection<IMetadatum>,
        IMetadataCollection
    {
        private IMetadataEntity parent;

        public LockedEmptyMetadataCollection(IMetadataEntity parent)
        {
            this.parent = parent;
        }


        public bool Contains(IType metadatumType)
        {
            return false;
        }

        public IMetadataEntity Parent
        {
            get { return this.parent; }
        }

        public IMetadatum this[IType metadatumType]
        {
            get { return null; }
        }

        public void Dispose()
        {
            this.parent = null;
            base.baseList = null;
        }
    }
}
