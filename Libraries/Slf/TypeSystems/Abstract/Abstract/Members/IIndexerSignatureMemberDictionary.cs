using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a series of indexer signatures.
    /// </summary>
    /// <typeparam name="TIndexer">The type of indexer in the current implementation.</typeparam>
    /// <typeparam name="TIndexerParent">The type of indexer parent in the current implementation.</typeparam>
    public interface IIndexerSignatureMemberDictionary<TIndexer, TIndexerParent> :
        IGroupedMemberDictionary<TIndexerParent, IGeneralSignatureMemberUniqueIdentifier, TIndexer>
        where TIndexer :
            IIndexerSignatureMember<TIndexer, TIndexerParent>
        where TIndexerParent :
            IIndexerSignatureParent<TIndexer, TIndexerParent>
    {

    }
    /// <summary>
    /// Defines properties and methods for working with a series of indexer signatures
    /// </summary>
    public interface IIndexerSignatureMemberDictionary :
        IGroupedMemberDictionary
    {
        /// <summary>
        /// Returns the <see cref="IIndexerParent"/> which 
        /// owns the <see cref="IIndexerMember"/> series.
        /// </summary>
        new IIndexerSignatureParent Parent { get; }
    }
}
