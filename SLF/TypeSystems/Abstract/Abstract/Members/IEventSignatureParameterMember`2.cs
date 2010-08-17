using System;
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
    /// Defines generic properties and methods for working with a parameter of an
    /// event signature member.
    /// </summary>
    /// <typeparam name="TEvent">The type of event in the current implementation.</typeparam>
    /// <typeparam name="TEventParent">The type used as a parent of the <typeparamref name="TEvent"/>
    /// instances in the current implementation.</typeparam>
    public interface IEventSignatureParameterMember<TEvent, TEventParent> :
        IEventSignatureParameterMember<TEvent, IEventSignatureParameterMember<TEvent, TEventParent>, TEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParent>
    {
    }
    /// <summary>
    /// Defines generic properties and methods for working with a parameter
    /// of an event signature member.
    /// </summary>
    /// <typeparam name="TEvent">The type of event in the current implementation.</typeparam>
    /// <typeparam name="TEventParameter">The type of parameters used on the <typeparamref name="TEvent"/>
    /// instances in the abstract typs system.</typeparam>
    /// <typeparam name="TEventParent">The type used as a parent of the <typeparamref name="TEvent"/>
    /// instances in the current implementation.</typeparam>
    public interface IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent> :
        ISignatureParameterMember<TEvent, TEventParameter, TEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParameter, TEventParent>
        where TEventParameter :
            IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParameter, TEventParent>
    {
    }
}
