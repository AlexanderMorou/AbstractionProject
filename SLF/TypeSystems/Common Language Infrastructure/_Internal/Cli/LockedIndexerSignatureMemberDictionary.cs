using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class LockedIndexerSignatureMemberDictionary<TIndexer, TIndexerParent> :
        LockedGroupedMembersBase<TIndexerParent, IGeneralSignatureMemberUniqueIdentifier, TIndexer, PropertyInfo>,
        IIndexerSignatureMemberDictionary<TIndexer, TIndexerParent>,
        IIndexerSignatureMemberDictionary
        where TIndexer :
            class,
            IIndexerSignatureMember<TIndexer, TIndexerParent>
        where TIndexerParent :
            IIndexerSignatureParent<TIndexer, TIndexerParent>
    {
        internal LockedIndexerSignatureMemberDictionary(LockedFullMembersBase master, TIndexerParent parent, PropertyInfo[] properties, Func<PropertyInfo, TIndexer> fetchImpl)
            : base(master, parent, properties, fetchImpl, GetName)
        {
        }
        private static string GetName(PropertyInfo indexer)
        {
            return indexer.Name;
        }
        protected override IGeneralSignatureMemberUniqueIdentifier FetchKey(PropertyInfo item)
        {
            return item.GetIndexerUniqueIdentifier(((ICompiledType) this.Parent).Manager);
        }

        #region IIndexerSignatureMemberDictionary Members

        IIndexerSignatureParent IIndexerSignatureMemberDictionary.Parent
        {
            get { return base.Parent; }
        }

        #endregion

    }
}
