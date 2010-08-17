using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines generic properties and methods for working with
    /// the parent of a series of <typeparamref name="TEvent"/>
    /// instances as a <typeparamref name="TEventParent"/>.
    /// </summary>
    /// <typeparam name="TEvent">The type of 
    /// <see cref="IEventSignatureMember{TEvent, TEventParameter, TEventParent}"/>
    /// in the current implementation.</typeparam>
    /// <typeparam name="TEventParameter">The type of 
    /// <see cref="IEventSignatureParameterMember{TEvent, TEventParameter, TEventParent}"/>
    /// in the current implementation that acts as a parameter
    /// of <typeparamref name="TEvent"/> instances.</typeparam>
    /// <typeparam name="TEventParent">The <see cref="IType{TType}"/>
    /// which parents the <typeparamref name="TEvent"/>
    /// instances in the current implementation.</typeparam>
    public interface IEventSignatureParent<TEvent, TEventParameter, TEventParent> :
        ISignatureParent<TEvent, TEventParameter, TEventParent>,
        IType<TEventParent>,
        IEventSignatureParent
        where TEvent :
            IEventSignatureMember<TEvent, TEventParameter, TEventParent>
        where TEventParameter :
            IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParameter, TEventParent>
    {
        /// <summary>
        /// Returns the <see cref="IEventSignatureMemberDictionary{TEvent, TEventParameter, TEventParent}"/>
        /// associated to the current <typeparamref name="TEventParent"/>
        /// implementation.
        /// </summary>
        new IEventSignatureMemberDictionary<TEvent, TEventParameter, TEventParent> Events { get; }
    }
}
