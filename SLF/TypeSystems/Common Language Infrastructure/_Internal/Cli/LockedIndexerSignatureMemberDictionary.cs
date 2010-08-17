using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

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
            : base(master, parent, properties, fetchImpl)
        {
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
