﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
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
    /// <typeparam name="TCtorParent">The type of <see cref="ICreatableType{TCtor, TType}"/>
    /// that contains the <typeparamref name="TCtor"/> instances.</typeparam>
    public interface IConstructorMemberDictionary<TCtor, TCtorParent> :
        ISignatureMemberDictionary<TCtor, IConstructorParameterMember<TCtor, TCtorParent>, TCtorParent>,
        IGroupedMemberDictionary<TCtorParent, TCtor>
        where TCtor :
            IConstructorMember<TCtor, TCtorParent>
        where TCtorParent :
            ICreatableType<TCtor, TCtorParent>
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