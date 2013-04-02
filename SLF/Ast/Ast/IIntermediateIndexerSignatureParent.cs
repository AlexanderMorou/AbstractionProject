using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;

 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines generic properties and methods for working with the parent of a series of
    /// indexer signatures.
    /// </summary>
    /// <typeparam name="TIndexer">The type of indexer signature in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateIndexer">The type of indexer signature in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TIndexerParent">The type of indexer signature parent in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateIndexerParent">The type of indexer signature parent in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateIndexerSignatureParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> :
        IIntermediateSignatureParent<IGeneralSignatureMemberUniqueIdentifier, TIndexer, TIntermediateIndexer, IIndexerSignatureParameterMember<TIndexer, TIndexerParent>, IIntermediateIndexerSignatureParameterMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>, TIndexerParent, TIntermediateIndexerParent>,
        IIndexerSignatureParent<TIndexer, TIndexerParent>,
        IIntermediateIndexerSignatureParent
        where TIndexer :
            IIndexerSignatureMember<TIndexer, TIndexerParent>
        where TIntermediateIndexer :
            TIndexer,
            IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
        where TIndexerParent :
            IIndexerSignatureParent<TIndexer, TIndexerParent>
        where TIntermediateIndexerParent :
            TIndexerParent,
            IIntermediateIndexerSignatureParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateIndexerSignatureMemberDictionary{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent}"/>
        /// contained within the <see cref="IIntermediateIndexerSignatureParent{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent}"/>.
        /// </summary>
        new IIntermediateIndexerSignatureMemberDictionary<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> Indexers { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with the parent of a series of
    /// indexer signatures.
    /// </summary>
    public interface IIntermediateIndexerSignatureParent:
        IIndexerSignatureParent
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateIndexerSignatureMemberDictionary"/>
        /// contained within the <see cref="IIntermediateIndexerSignatureParent"/>.
        /// </summary>
        new IIntermediateIndexerSignatureMemberDictionary Indexers { get; }
    }
}
