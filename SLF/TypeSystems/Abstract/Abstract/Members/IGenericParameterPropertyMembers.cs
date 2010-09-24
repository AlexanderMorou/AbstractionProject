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
    /// Defines properties and methods for working with a dictionary of generic
    /// parameter members.
    /// </summary>
    /// <typeparam name="TGenericParameter">The t ype of generic parameter in the abstract
    /// type system.</typeparam>
    public interface IGenericParameterPropertyMemberDictionary<TGenericParameter> :
        IPropertySignatureMemberDictionary<IGenericParameterPropertyMember<TGenericParameter>, TGenericParameter>
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with the properties
    /// defined on an <see cref="IGenericParameter"/>.
    /// </summary>
    public interface IGenericParameterPropertyMemberDictionary :
        IPropertySignatureMemberDictionary
    {
        /// <summary>
        /// Returns the <see cref="IGenericParameter"/> which contains
        /// the <see cref="IGenericParameterPropertyMemberDictionary"/>.
        /// </summary>
        new IGenericParameter Parent { get; }
    }
}