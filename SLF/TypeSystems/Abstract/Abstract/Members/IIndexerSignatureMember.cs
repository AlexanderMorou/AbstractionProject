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
    /// Defines generic properties and methods for working with the signature of an indexer.
    /// </summary>
    /// <typeparam name="TIndexer">The type of indexer member used in the abstact type system.</typeparam>
    /// <typeparam name="TIndexerParent">The type of indexer parent used in the abstact type system.</typeparam>
    public interface IIndexerSignatureMember<TIndexer, TIndexerParent> :
        ISignatureMember<TIndexer, IIndexerSignatureParameterMember<TIndexer, TIndexerParent>, TIndexerParent>,
        IPropertySignatureMember,
        IMember
        where TIndexer :
            IIndexerSignatureMember<TIndexer, TIndexerParent>
        where TIndexerParent :
            IIndexerSignatureParent<TIndexer, TIndexerParent>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with the signature of an indexer.
    /// </summary>
    public interface IIndexerSignatureMember :
        ISignatureMember,
        IPropertySignatureMember
    {
    }
}
