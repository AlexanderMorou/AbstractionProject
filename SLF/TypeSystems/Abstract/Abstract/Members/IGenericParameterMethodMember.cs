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
    /// Defines generic properties and methods for working with a method defined on a 
    /// generic parameter member.
    /// </summary>
    /// <typeparam name="TGenericParameter">The type of generic parameter member in the current implementation.</typeparam>
    public interface IGenericParameterMethodMember<TGenericParameter> :
        IMethodSignatureMember<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>,
        IGenericParameterMethodMember
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
    {

    }
    /// <summary>
    /// Defines properties and methods for working with a generic parameter's 
    /// method member.
    /// </summary>
    public interface IGenericParameterMethodMember :
        IMethodSignatureMember
    {
    }
}
