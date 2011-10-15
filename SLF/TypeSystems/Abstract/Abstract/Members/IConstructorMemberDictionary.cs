using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a series of <typeparamref name="TCtor"/> instances
    /// contained within a <typeparamref name="TCtorParent"/> instance.
    /// </summary>
    /// <typeparam name="TCtor">The type of <see cref="IConstructorMember{TCtor, TCtorParent}"/>
    /// in the current implementation.</typeparam>
    /// <typeparam name="TCtorParent">The type of <see cref="ICreatableParent{TCtor, TCtorParent}"/>
    /// that contains the <typeparamref name="TCtor"/> instances.</typeparam>
    public interface IConstructorMemberDictionary<TCtor, TCtorParent> :
        ISignatureMemberDictionary<IGeneralSignatureMemberUniqueIdentifier, TCtor, IConstructorParameterMember<TCtor, TCtorParent>, TCtorParent>,
        IGroupedMemberDictionary<TCtorParent, IGeneralSignatureMemberUniqueIdentifier, TCtor>
        where TCtor :
            IConstructorMember<TCtor, TCtorParent>
        where TCtorParent :
            ICreatableParent<TCtor, TCtorParent>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with a series of <see cref="IConstructorMember"/> instances.
    /// </summary>
    public interface IConstructorMemberDictionary :
        ISignatureMemberDictionary,
        IGroupedMemberDictionary
    {

    }
}
