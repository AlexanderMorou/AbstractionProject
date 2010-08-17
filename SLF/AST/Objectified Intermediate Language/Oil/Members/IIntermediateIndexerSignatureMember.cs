using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with an intermediate
    /// indexer signature member.
    /// </summary>
    /// <typeparam name="TIndexer">The type of indexer signature in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateIndexer">The type of indexer signature in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TIndexerParent">The type of indexer signature parent in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateIndexerParent">The type of indexer signature parent in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> :
        IIntermediateSignatureMember<TIndexer, TIntermediateIndexer, IIndexerSignatureParameterMember<TIndexer, TIndexerParent>, IIntermediateIndexerSignatureParameterMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>, TIndexerParent, TIntermediateIndexerParent>,
        IIntermediateIndexerSignatureMember,
        IIndexerSignatureMember<TIndexer, TIndexerParent>
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
    }

    /// <summary>
    /// Defines properties and methods for working with an intermediate
    /// indexer signature member.
    /// </summary>
    public interface IIntermediateIndexerSignatureMember :
        IIntermediateSignatureMember,
        IIntermediatePropertySignatureMember,
        IIndexerSignatureMember
    {
    }
}
