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
    /// Defines properties and methods for working with the parameter
    /// of an indexer signature member.
    /// </summary>
    /// <typeparam name="TIndexer">The type of indexer in the current implementation.</typeparam>
    /// <typeparam name="TIndexerParent">The type of indexer parent in the abstract
    /// type system.</typeparam>
    public interface IIndexerSignatureParameterMember<TIndexer, TIndexerParent> :
        ISignatureParameterMember<IGeneralSignatureMemberUniqueIdentifier, TIndexer, IIndexerSignatureParameterMember<TIndexer, TIndexerParent>, TIndexerParent>
        where TIndexer :
            IIndexerSignatureMember<TIndexer, TIndexerParent>
        where TIndexerParent :
            IIndexerSignatureParent<TIndexer, TIndexerParent>
    {
    }
}
