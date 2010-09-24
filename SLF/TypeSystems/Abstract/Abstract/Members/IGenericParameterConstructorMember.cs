using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Defines properties and methods for working with the constructor of a
    /// generic parameter.
    /// </summary>
    /// <typeparam name="TGenericParameter">The type of generic parameter that contains the 
    /// <see cref="IGenericParameterConstructorMember{TGenericParameter}"/></typeparam>
    public interface IGenericParameterConstructorMember<TGenericParameter> :
        IConstructorMember<IGenericParameterConstructorMember<TGenericParameter>, TGenericParameter>,
        IGenericParameterConstructorMember
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with the constructor of an
    /// <see cref="IGenericParameter"/>.
    /// </summary>
    public interface IGenericParameterConstructorMember :
        IConstructorMember
    {
    }
}
