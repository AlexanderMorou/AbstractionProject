using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
/*---------------------------------------------------------------------\
| Copyright © 2011 Allen Copeland Jr.                                  |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal abstract class _IndexerMembersBase<TIndexer, TIndexerParent> :
        _GroupedMembersBase<TIndexerParent, IGeneralSignatureMemberUniqueIdentifier, TIndexer, IIndexerMemberDictionary<TIndexer, TIndexerParent>>,
        IIndexerMemberDictionary<TIndexer, TIndexerParent>,
        IIndexerMemberDictionary
        where TIndexer :
            class,
            IIndexerMember<TIndexer, TIndexerParent>
        where TIndexerParent :
            class,
            IIndexerParent<TIndexer, TIndexerParent>
    {
        public _IndexerMembersBase(_FullMembersBase master, IIndexerMemberDictionary<TIndexer, TIndexerParent> originalSet, TIndexerParent parent)
            : base(master, originalSet, parent)
        {

        }

    }
}
