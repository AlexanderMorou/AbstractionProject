using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal abstract class _EventSignatureMembersBase<TEvent, TEventParameter, TEventParent, TDictionary> :
        _SignatureMembersBase<IGeneralSignatureMemberUniqueIdentifier, TEvent, TEventParameter, TEventParent, TDictionary>,
        IEventSignatureMemberDictionary<TEvent, TEventParameter, TEventParent>,
        IEventSignatureMemberDictionary
        where TEvent :
            class,
            IEventSignatureMember<TEvent, TEventParameter, TEventParent>
        where TEventParameter :
            IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParameter, TEventParent>
        where TDictionary :
            class,
            IEventSignatureMemberDictionary<TEvent, TEventParameter, TEventParent>
    {
        internal _EventSignatureMembersBase(_FullMembersBase master, TDictionary originalSet, TEventParent parent)
            : base(master, originalSet, parent)
        {
        }


        #region IEventSignatureMemberDictionary<TEvent,TEventParameter,TEventParent> Members

        public TEvent Find(string eventName, IDelegateType searchCriteria)
        {
            return this.Find(searchCriteria).Filter(@event => @event.Name == eventName).Values.FirstOrDefault();
        }

        public IFilteredSignatureMemberDictionary<IGeneralSignatureMemberUniqueIdentifier, TEvent, TEventParameter, TEventParent> Find(IDelegateType searchCriteria)
        {
            return this.Find(true, searchCriteria.Parameters.ParameterTypes);
        }

        #endregion


        #region IEventSignatureMemberDictionary Members

        IFilteredSignatureMemberDictionary IEventSignatureMemberDictionary.Find(IDelegateType searchCriteria)
        {
            return (IFilteredSignatureMemberDictionary)this.Find(searchCriteria);
        }

        IEventSignatureMember IEventSignatureMemberDictionary.Find(string eventName, IDelegateType searchCriteria)
        {
            return this.Find(eventName, searchCriteria);
        }

        #endregion

    }
}
