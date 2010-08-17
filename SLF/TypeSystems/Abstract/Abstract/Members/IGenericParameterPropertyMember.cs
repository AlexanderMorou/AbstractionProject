using System;
using System.Collections.Generic;
using System.Text;
/*----------------------------------------\
| Copyright © 2009 Allen Copeland Jr.     |
|-----------------------------------------|
| The Abstraction Project's code is prov- |
| -ided under a contract-release basis.   |
| DO NOT DISTRIBUTE and do not use beyond |
| the contract terms.                     |
\--------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a property of a
    /// generic parameter member.
    /// </summary>
    /// <typeparam name="TGenericParameter">The type of generic parameter in the
    /// abstract type system.</typeparam>
    public interface IGenericParameterPropertyMember<TGenericParameter> :
        IPropertySignatureMember<IGenericParameterPropertyMember<TGenericParameter>, TGenericParameter>,
        IGenericParameterPropertyMember
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with a property member of a generic parameter.
    /// </summary>
    /// <remarks>Generic parameter members are all signature based.</remarks>
    public interface IGenericParameterPropertyMember :
        IPropertySignatureMember
    {
    }
}
