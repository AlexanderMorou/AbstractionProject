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
    /// of an event member.
    /// </summary>
    /// <typeparam name="TEvent">The type of 
    /// <see cref="IEventMember{TEvent, TEventParent}"/>
    /// in the current implementation.</typeparam>
    /// <typeparam name="TEventParentIdentifier">The kind of unique identifier used 
    /// to differentiate between the event parent and its siblings.</typeparam>
    /// <typeparam name="TEventParent">The <see cref="IType{TTypeIdentifier, TType}"/>
    /// which parents the <typeparamref name="TEvent"/>
    /// instances in the current implementation.</typeparam>
    public interface IEventParameterMember<TEvent, TEventParent> :
        IEventSignatureParameterMember<TEvent, IEventParameterMember<TEvent, TEventParent>, TEventParent>
        where TEvent :
            IEventMember<TEvent, TEventParent>
        where TEventParent :
            IEventParent<TEvent, TEventParent>
    {
    }
}
