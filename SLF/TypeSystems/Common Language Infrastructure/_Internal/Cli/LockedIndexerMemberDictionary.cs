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
    internal class LockedIndexerMemberDictionary<TIndexer, TIndexerParent> :
        LockedGroupedMembersBase<TIndexerParent, TIndexer, PropertyInfo>,
        IIndexerMemberDictionary<TIndexer, TIndexerParent>,
        IIndexerMemberDictionary
        where TIndexer :
            class,
            IIndexerMember<TIndexer, TIndexerParent>
        where TIndexerParent :
            IIndexerParent<TIndexer, TIndexerParent>
    {
        internal LockedIndexerMemberDictionary(LockedFullMembersBase master, TIndexerParent parent, PropertyInfo[] properties, Func<PropertyInfo, TIndexer> fetchImpl)
            : base(master, parent, properties, fetchImpl)
        {
        }

        protected override string FetchKey(PropertyInfo item)
        {
            return item.GetUniqueIdentifier();
        }

    }
}
