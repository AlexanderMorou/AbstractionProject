using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal partial class LockedEventSignatureMembersBase<TEvent, TEventSignatureParameter, TEventParent> :
        LockedSignatureMembersBase<IGeneralSignatureMemberUniqueIdentifier, TEvent, TEventSignatureParameter, TEventParent, EventInfo>,
        IEventSignatureMemberDictionary<TEvent, TEventSignatureParameter, TEventParent>,
        IEventSignatureMemberDictionary
        where TEvent :
            class,
            IEventSignatureMember<TEvent, TEventSignatureParameter, TEventParent>
        where TEventSignatureParameter :
            IEventSignatureParameterMember<TEvent, TEventSignatureParameter, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventSignatureParameter, TEventParent>
    {
        internal LockedEventSignatureMembersBase(LockedFullMembersBase master, TEventParent parent, EventInfo[] sourceData, Func<EventInfo, TEvent> fetchImpl)
            : base(master, parent, sourceData, fetchImpl, GetName)
        {
        }
        private static string GetName(EventInfo @event)
        {
            return @event.Name;
        }
        internal LockedEventSignatureMembersBase(LockedFullMembersBase master, TEventParent parent)
            : base(master, parent)
        {
        }

        protected override IGeneralSignatureMemberUniqueIdentifier FetchKey(EventInfo item)
        {
            return item.GetUniqueIdentifier();
        }

        #region IEventSignatureMemberDictionary<TEvent,TEventSignatureParameter,TEventParent> Members

        public TEvent Find(string eventName, IDelegateType searchCriteria)
        {
            return this.Find(searchCriteria).Filter(@event => @event.Name == eventName).Values.FirstOrDefault();
        }

        public IFilteredSignatureMemberDictionary<IGeneralSignatureMemberUniqueIdentifier, TEvent, TEventSignatureParameter, TEventParent> Find(IDelegateType searchCriteria)
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
