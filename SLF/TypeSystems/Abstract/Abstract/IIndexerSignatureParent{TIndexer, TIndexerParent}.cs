using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines generic properties and methods for working with the
    /// parent of a series of indexer signature members.
    /// </summary>
    /// <typeparam name="TIndexer">The type of indexer member used in
    /// the abstact type system.</typeparam>
    /// <typeparam name="TIndexerParent">The type of indexer parent
    /// used in the abstact type system.</typeparam>
    public interface IIndexerSignatureParent<TIndexer, TIndexerParent> :
        ISignatureParent<IGeneralSignatureMemberUniqueIdentifier, TIndexer, IIndexerSignatureParameterMember<TIndexer, TIndexerParent>, TIndexerParent>,
        IIndexerSignatureParent
        where TIndexer :
            IIndexerSignatureMember<TIndexer, TIndexerParent>
        where TIndexerParent :
            IIndexerSignatureParent<TIndexer, TIndexerParent>
    {
        /// <summary>
        /// Returns the <see cref="IIndexerSignatureMemberDictionary{TIndexer, TIndexerParentIdentifier, TIndexerParent}"/>
        /// contained within the <see cref="IIndexerSignatureParent{TIndexer, TIndexerParentIdentifier, TIndexerParent}"/>.
        /// </summary>
        new IIndexerSignatureMemberDictionary<TIndexer, TIndexerParent> Indexers { get; }
    }
}
