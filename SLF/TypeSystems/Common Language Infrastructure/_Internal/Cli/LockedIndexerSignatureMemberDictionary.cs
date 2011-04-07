using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class LockedIndexerSignatureMemberDictionary<TIndexer, TIndexerParent> :
        LockedGroupedMembersBase<TIndexerParent, TIndexer, PropertyInfo>,
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
        protected override string FetchKey(PropertyInfo item)
        {
            return item.GetUniqueIdentifier();
        }


        #region IIndexerSignatureMemberDictionary Members

        IIndexerSignatureParent IIndexerSignatureMemberDictionary.Parent
        {
            get { return base.Parent; }
        }

        #endregion

    }
}
