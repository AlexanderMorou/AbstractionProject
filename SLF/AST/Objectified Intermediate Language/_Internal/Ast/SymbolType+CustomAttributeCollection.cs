using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Ast
{
    partial class SymbolType
    {
        protected class CustomAttributeCollection :
            ReadOnlyCollection<ICustomAttributeInstance>,
            ICustomAttributeCollection
        {
            private ICustomAttributedDeclaration parent;
            public CustomAttributeCollection(ICustomAttributedDeclaration parent)
                : base()
            {
                this.parent = parent;
            }

            #region ICustomAttributeCollection Members

            public bool Contains(IType attributeType)
            {
                return false;
            }

            public ICustomAttributedDeclaration Parent
            {
                get { return this.parent; }
            }

            public ICustomAttributeInstance this[IType attributeType]
            {
                get { throw new ArgumentException("attributeType"); }
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
