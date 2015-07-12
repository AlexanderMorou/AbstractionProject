using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with the parameter of an intermediate
    /// indexer signature member.
    /// </summary>
    /// <typeparam name="TIndexer">The type of indexer signature in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateIndexer">The type of indexer signature in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TIndexerParent">The type of indexer signature parent in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateIndexerParent">The type of indexer signature parent in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateIndexerSignatureParameterMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> :
        IIntermediateSignatureParameterMember<IGeneralSignatureMemberUniqueIdentifier, TIndexer, TIntermediateIndexer, IIndexerSignatureParameterMember<TIndexer, TIndexerParent>, IIntermediateIndexerSignatureParameterMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>, TIndexerParent, TIntermediateIndexerParent>,
        IIndexerSignatureParameterMember<TIndexer, TIndexerParent>
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
}
