using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
/*----------------------------------------\
| Copyright © 2010 Allen Copeland Jr.     |
|-----------------------------------------|
| The Abstraction Project's code is prov- |
| -ided under a contract-release basis.   |
| DO NOT DISTRIBUTE and do not use beyond |
| the contract terms.                     |
\--------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledAssembly
    {

        protected override ICustomAttributeCollection InitializeCustomAttributes()
        {
            this.initializedCustomAttributes = true;
            return new CustomAttributeCollection(this);
        }

        private partial class CustomAttributeCollection :
            ReadOnlyCollection<ICustomAttributeInstance>,
            ICustomAttributeCollection
        {
            private CompiledAssembly assembly;
            private ICustomAttributeInstance[] mruList;
            private const int mruListLength = 7;
            public CustomAttributeCollection(CompiledAssembly assembly)
            {
                this.assembly = assembly;
                object[] customAttrs = assembly.UnderlyingAssembly.GetCustomAttributes(false);
                foreach (Attribute attr in customAttrs)
                    this.baseCollection.Add(new CustomAttributeInstance(attr, this.assembly));
            }

            #region IDisposable Members

            /// <summary>
            /// Disposes the <see cref="CustomAttributeCollection"/>.
            /// </summary>
            public void Dispose()
            {
                for (int i = 0; i < this.Count; i++)
                    this[i].Dispose();
                this.baseCollection.Clear();
            }

            #endregion

            #region ICustomAttributeCollection Members

            public bool Contains(IType attributeType)
            {
                foreach (var item in this.baseCollection)
                    if (item.Type.Equals(attributeType))
                        return true;
                return false;
            }

            public ICustomAttributedDeclaration Parent
            {
                get { return this.assembly; }
            }

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

        }
    }
}
