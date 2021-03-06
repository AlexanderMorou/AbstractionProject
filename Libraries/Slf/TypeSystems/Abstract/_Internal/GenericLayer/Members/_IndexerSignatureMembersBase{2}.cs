﻿using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal abstract class _IndexerSignatureMembersBase<TIndexer, TIndexerParent> :
        _GroupedMembersBase<TIndexerParent, IGeneralSignatureMemberUniqueIdentifier, TIndexer, IIndexerSignatureMemberDictionary<TIndexer, TIndexerParent>>,
        IIndexerSignatureMemberDictionary<TIndexer, TIndexerParent>,
        IIndexerSignatureMemberDictionary
        where TIndexer :
            class,
            IIndexerSignatureMember<TIndexer, TIndexerParent>
        where TIndexerParent :
            class,
            IIndexerSignatureParent<TIndexer, TIndexerParent>
    {
        public _IndexerSignatureMembersBase(_FullMembersBase master, IIndexerSignatureMemberDictionary<TIndexer, TIndexerParent> originalSet, TIndexerParent parent)
            : base(master, originalSet, parent)
        {

        }

        #region IIndexerSignatureMemberDictionary Members

        IIndexerSignatureParent IIndexerSignatureMemberDictionary.Parent
        {
            get { return base.Parent; }
        }

        #endregion

    }
}
