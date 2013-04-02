using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Ast
{
    partial class SymbolType
    {
        protected class CustomAttributeCollection :
            ReadOnlyCollection<IMetadatum>,
            IMetadataCollection
        {
            private IMetadataEntity parent;
            public CustomAttributeCollection(IMetadataEntity parent)
                : base()
            {
                this.parent = parent;
            }

            #region IMetadataCollection Members

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
                get { throw new ArgumentException("metadatumType"); }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                this.parent = null;
            }

            #endregion
        }
    }
}
