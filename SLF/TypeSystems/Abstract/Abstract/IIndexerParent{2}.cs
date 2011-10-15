using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines generic properties and methods for working with the
    /// parent of an indexer member.
    /// </summary>
    /// <typeparam name="TIndexer">The type of indexer in the current implementation.</typeparam>
    /// <typeparam name="TIndexerParentIdentifier">The kind of identifier used to
    /// represent the parent uniquely from its siblings.</typeparam>
    /// <typeparam name="TIndexerParent">The type of <typeparamref name="TIndexer"/> parent
    /// in the current implementation.</typeparam>
    public interface IIndexerParent<TIndexer, TIndexerParent> :
        ISignatureParent<IGeneralSignatureMemberUniqueIdentifier, TIndexer, IIndexerParameterMember<TIndexer, TIndexerParent>, TIndexerParent>,
        IIndexerParent
        where TIndexer :
            IIndexerMember<TIndexer, TIndexerParent>
        where TIndexerParent :
            IIndexerParent<TIndexer, TIndexerParent>
    {
        /// <summary>
        /// Returns the <see cref="IIndexerMemberDictionary{TIndexer, TIndexerParent}"/>
        /// contained within the <see cref="IIndexerParent{TIndexer, TIndexerParent}"/>.
        /// </summary>
        new IIndexerMemberDictionary<TIndexer, TIndexerParent> Indexers { get; }
    }
}
