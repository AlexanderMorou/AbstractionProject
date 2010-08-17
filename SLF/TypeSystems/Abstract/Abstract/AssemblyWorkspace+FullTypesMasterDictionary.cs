using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    partial class AssemblyWorkspace
    {
        /// <summary>
        /// Provides a full types dictionary for an assembly workspace.
        /// </summary>
        protected class FullTypesMasterDictionary :
            MasterDictionaryBase<string, IType>,
            IFullTypeDictionary,
            IDisposable
        {
            /// <summary>
            /// Data member which relates back to the parent which contains the types in the 
            /// unified context.
            /// </summary>
            private ITypeParent owner;
            /// <summary>
            /// Data member for which relates to the original data sources from which the data
            /// is pulled.
            /// </summary>
            private ITypeParent[] sources;

            /// <summary>
            /// Creates a new <see cref="FullTypesMasterDictionary"/> instance with the
            /// <paramref name="owner"/> and <paramref name="sources"/> provided.
            /// </summary>
            /// <param name="owner"></param>
            /// <param name="sources"></param>
            public FullTypesMasterDictionary(ITypeParent owner, ITypeParent[] sources)
            {
                this.owner = owner;
                this.sources = sources;
            }

            #region IDisposable Members

            /// <summary>
            /// Disposes the current FullTypesMasterDictionary instance.
            /// </summary>
            public void Dispose()
            {
                this.sources = null;
                this.owner = null;
            }

            #endregion
        }
    }
}
