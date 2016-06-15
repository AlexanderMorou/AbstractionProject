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
    /// Defines generic properties and methods for working with an indexer parameter member.
    /// </summary>
    /// <typeparam name="TIndexer">The type of indexer member used in the abstact type system.</typeparam>
    /// <typeparam name="TIndexerParent">The type of indexer parent used in the abstact type system.</typeparam>
    public interface IIndexerParameterMember<TIndexer, TIndexerParent> :
        ISignatureParameterMember<IGeneralSignatureMemberUniqueIdentifier, TIndexer, IIndexerParameterMember<TIndexer, TIndexerParent>, TIndexerParent>
        where TIndexer :
            IIndexerMember<TIndexer, TIndexerParent>
        where TIndexerParent :
            IIndexerParent<TIndexer, TIndexerParent>
    {

    }
}
