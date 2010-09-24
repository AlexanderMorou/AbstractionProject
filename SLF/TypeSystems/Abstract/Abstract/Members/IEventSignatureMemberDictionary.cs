using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a dictionary of event signature members.
    /// </summary>
    /// <typeparam name="TEvent">The type of event in the current implementation.</typeparam>
    /// <typeparam name="TEventParent">The type used as a parent of the <typeparamref name="TEvent"/>
    /// instances in the current implementation.</typeparam>
    public interface IEventSignatureMemberDictionary<TEvent, TEventParent> :
        IEventSignatureMemberDictionary<TEvent, IEventSignatureParameterMember<TEvent, TEventParent>, TEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParent>
    {
    }
    /// <summary>
    /// Defines generic properties and methods for working with a dictionary of event signature members.
    /// </summary>
    /// <typeparam name="TEvent">The type of event in the current implementation.</typeparam>
    /// <typeparam name="TEventParameter">The type of parameters used on the <typeparamref name="TEvent"/>
    /// instances in the abstract typs system.</typeparam>
    /// <typeparam name="TEventParent">The type used as a parent of the <typeparamref name="TEvent"/>
    /// instances in the current implementation.</typeparam>
    public interface IEventSignatureMemberDictionary<TEvent, TEventParameter, TEventParent> :
        ISignatureMemberDictionary<TEvent, TEventParameter, TEventParent>,
        IGroupedMemberDictionary<TEventParent, TEvent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParameter, TEventParent>
        where TEventParameter :
            IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParameter, TEventParent>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with a series of <see cref="IEventSignatureMember"/>
    /// instances.
    /// </summary>
    public interface IEventSignatureMemberDictionary :
        ISignatureMemberDictionary
    {
    }
}
