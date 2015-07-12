using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal abstract partial class _EventSignatureMemberBase<TEvent, TEventParent> :
        _EventSignatureMemberBase<TEvent, IEventSignatureParameterMember<TEvent, TEventParent>, TEventParent>,
        IEventSignatureMember<TEvent, TEventParent>
        where TEvent :
            class,
            IEventSignatureMember<TEvent, TEventParent>
        where TEventParent :
            class,
            IEventSignatureParent<TEvent, TEventParent>
    {
        protected _EventSignatureMemberBase(TEvent original, TEventParent adjustedParent)
            : base(original, adjustedParent)
        { 
        }

        protected override IParameterMemberDictionary<TEvent, IEventSignatureParameterMember<TEvent, TEventParent>> InitializeParameters()
        {
            return new _Parameters(this.Original.Parameters, this);
        }

    }
}
