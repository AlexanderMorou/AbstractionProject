using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a parameter
    /// of a constructor member.
    /// </summary>
    /// <typeparam name="TCtor">The type of <see cref="IConstructorMember{TCtor, TCtorParent}"/>
    /// in the current implementation.</typeparam>
    /// <typeparam name="TType">The type of <see cref="ICreatableParent{TCtor, TCtorParent}"/>
    /// that contains the <typeparamref name="TCtor"/> instances.</typeparam>
    public interface IConstructorParameterMember<TCtor, TType> :
        ISignatureParameterMember<IGeneralSignatureMemberUniqueIdentifier, TCtor, IConstructorParameterMember<TCtor, TType>, TType>
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TType :
            ICreatableParent<TCtor, TType>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with a parameter of a 
    /// constructor member.
    /// </summary>
    public interface IConstructorParameterMember :
        IParameterMember
    {

    }
}
