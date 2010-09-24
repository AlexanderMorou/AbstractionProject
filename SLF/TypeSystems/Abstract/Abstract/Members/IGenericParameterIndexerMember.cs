using System;
using System.Collections.Generic;
using System.Text;
/*----------------------------------------\
| Copyright © 2010 Allen Copeland Jr.     |
|-----------------------------------------|
| The Abstraction Project's code is prov- |
| -ided under a contract-release basis.   |
| DO NOT DISTRIBUTE and do not use beyond |
| the contract terms.                     |
\--------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with an indexer
    /// defined on a generic parameter.
    /// </summary>
    /// <typeparam name="TGenericParameter">The type of <see cref="IGenericParameter{TGenericParameter}"/>
    /// the indexer member belongs to.</typeparam>
    public interface IGenericParameterIndexerMember<TGenericParameter> :
        IIndexerSignatureMember<IGenericParameterIndexerMember<TGenericParameter>, TGenericParameter>,
        IGenericParameterIndexerMember
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with an indexer
    /// defined on a generic parameter.
    /// </summary>
    public interface IGenericParameterIndexerMember :
        IIndexerSignatureMember
    {
    }
}
