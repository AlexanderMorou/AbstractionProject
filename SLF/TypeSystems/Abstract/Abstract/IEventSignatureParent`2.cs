using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
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
    /// <see cref="IEventSignatureMember{TEvent, TEventParent}"/>
    /// in the current implementation as only the signature
    /// of the <typeparamref name="TEvent"/>.</typeparam>
    /// <typeparam name="TEventParent">The <see cref="IType{TType}"/>
    /// which parents the <typeparamref name="TEvent"/>
    /// instances in the current implementation.</typeparam>
    public interface IEventSignatureParent<TEvent, TEventParent> :
        IEventSignatureParent<TEvent, IEventSignatureParameterMember<TEvent, TEventParent>, TEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParent>
    {
        /// <summary>
        /// Returns the <see cref="IEventSignatureMemberDictionary{TEvent, TEventParent}"/>
        /// associated to the current <typeparamref name="TEventParent"/>
        /// implementation.
        /// </summary>
        new IEventSignatureMemberDictionary<TEvent, TEventParent> Events { get; }
    }
}
