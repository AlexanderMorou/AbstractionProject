using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
/*----------------------------------------\
| Copyright © 2011 Allen Copeland Jr.     |
|-----------------------------------------|
| The Abstraction Project's code is prov- |
| -ided under a contract-release basis.   |
| DO NOT DISTRIBUTE and do not use beyond |
| the contract terms.                     |
\--------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    /* *
     * Provides an empty locked custom attribute collection for 
     * array, byreference, nullable, and pointer types.
     * */
    internal class LockedEmptyCustomAttributeCollection : 
        ReadOnlyCollection<ICustomAttributeInstance>,
        ICustomAttributeCollection
    {
        /* *
         * Creates a new LockedEmptyCustomAttributeCollection instance with the parent
         * provided.
         * *
         * The parent is the simple type which needs the empty custom attribute collection.
         * */
        public LockedEmptyCustomAttributeCollection(ICustomAttributedDeclaration parent)
            : base()
        {
            this.Parent = parent;
        }

        #region ICustomAttributeCollection Members

        public bool Contains(IType attributeType)
        {
            return false;
        }

        #endregion


        #region IDisposable Members

        /* *
         * Disposes the LockedEmptyCustomAttributeCollection.
         * */
        public void Dispose()
        {
            this.Parent = null;
            this.baseList = null;
            GC.SuppressFinalize(this);
        }

        #endregion

        #region ICustomAttributeCollection Members

        public ICustomAttributedDeclaration Parent { get; private set; }

        #endregion

        #region ICustomAttributeCollection Members

        public ICustomAttributeInstance this[IType attributeType]
        {
            get { return null; }
        }

        #endregion

    }
}
