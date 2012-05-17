using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliTypeDictionary<TTypeIdentifier, TType> :
        SubordinateDictionary<TTypeIdentifier, IGeneralTypeUniqueIdentifier, TType, IType>,
        IGroupedTypeDictionary<TTypeIdentifier, TType>
        where TTypeIdentifier :
            ITypeUniqueIdentifier,
            IGeneralTypeUniqueIdentifier
        where TType :
            IType<TTypeIdentifier, TType>
    {
        private int[] parentTypeSubset;
        private _ICliTypeParent owner;
        private CliManager manager;
        public CliTypeDictionary(CliTypeDictionary master, CliManager identityManager, _ICliTypeParent owningParent, int[] parentTypeSubset)
            : base(master)
        {
            this.manager = identityManager;
            this.owner = owningParent;
        }

        #region IDeclarationDictionary<TTypeIdentifier,TType> Members

        public int IndexOf(TType decl)
        {
            if (decl is ICliType)
            {
                var cliDecl = (ICliType) decl;
                var metadataRows = owner._Types;
                int index = metadataRows.GetIndexOf(cliDecl.Metadata);
                if (index != -1)
                {
                    for (int i = 0; i < parentTypeSubset.Length; i++)
                    {
                        if (parentTypeSubset[i] == index)
                            return i;
                    }
                }
            }
            return -1;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this.parentTypeSubset = null;
            this.manager = null;
            this.owner = null;
        }

        #endregion
    }

}
