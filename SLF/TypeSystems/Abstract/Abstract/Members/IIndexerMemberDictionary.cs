using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a series of
    /// indexers.
    /// </summary>
    /// <typeparam name="TIndexer">The type of indexer in the current implementation.</typeparam>
    /// <typeparam name="TIndexerParent">The type of indexer parent in the current implementation.</typeparam>
    public interface IIndexerMemberDictionary<TIndexer, TIndexerParent> :
        IGroupedMemberDictionary<TIndexerParent, TIndexer>
        where TIndexer :
            IIndexerMember<TIndexer, TIndexerParent>
        where TIndexerParent :
            IIndexerParent<TIndexer, TIndexerParent>
    {

    }

    /// <summary>
    /// Defines properties and methods for working with a series of
    /// indexers.
    /// </summary>
    public interface IIndexerMemberDictionary :
        IGroupedMemberDictionary
    {
    }
}
