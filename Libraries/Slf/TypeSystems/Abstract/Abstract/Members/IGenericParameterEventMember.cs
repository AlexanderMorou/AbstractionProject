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
    /// Defines generic properties and methods for working with an event defined
    /// on a generic parameter member.
    /// </summary>
    /// <typeparam name="TGenericParameter">The type of generic parameter member in the abstract
    /// type system.</typeparam>
    public interface IGenericParameterEventMember<TGenericParameter> :
        IEventSignatureMember<IGenericParameterEventMember<TGenericParameter>, TGenericParameter>,
        IGenericParameterEventMember
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with an event defined on a generic parameter member.
    /// </summary>
    public interface IGenericParameterEventMember :
        IEventSignatureMember
    {
        /// <summary>
        /// Returns the <see cref="IGenericParameter"/> in which the 
        /// current <see cref="IGenericParameterEventMember"/> belongs.
        /// </summary>
        new IGenericParameter Parent { get; }
    }
}
