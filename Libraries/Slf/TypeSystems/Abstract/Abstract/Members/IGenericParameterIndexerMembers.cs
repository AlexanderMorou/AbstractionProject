using System;
using System.Collections.Generic;
using System.Text;
/*----------------------------------------\
| Copyright © 2012 Allen Copeland Jr.     |
|-----------------------------------------|
| The Abstraction Project's code is prov- |
| -ided under a contract-release basis.   |
| DO NOT DISTRIBUTE and do not use beyond |
| the contract terms.                     |
\--------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a dictionary of indexer
    /// members defined on a generic parameter member.
    /// </summary>
    /// <typeparam name="TGenericParameter">The type of generic parameter member in the 
    /// abstract type system.</typeparam>
    public interface IGenericParameterIndexerMemberDictionary<TGenericParameter> :
        IIndexerSignatureMemberDictionary<IGenericParameterIndexerMember<TGenericParameter>, TGenericParameter>
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with a series of
    /// indexers defined on an <see cref="IGenericParameter"/>.
    /// </summary>
    public interface IGenericParameterIndexerMemberDictionary :
        IIndexerSignatureMemberDictionary
    {
        /// <summary>
        /// Returns the <see cref="IGenericParameter"/> which contains
        /// the <see cref="IGenericParameterIndexerMemberDictionary"/>.
        /// </summary>
        new IGenericParameter Parent { get; }
    }
}
