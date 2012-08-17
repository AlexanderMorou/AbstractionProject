using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
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
            return (from e in this.Values
                    where e.Name == eventName
                    where e.SignatureSource == EventSignatureSource.Delegate &&
                          e.SignatureType == searchCriteria ||
                          e.SignatureSource == EventSignatureSource.Declared &&
                          e.Parameters.ParameterTypes.SequenceEqual(searchCriteria.Parameters.ParameterTypes)
                    select e).FirstOrDefault();
        }

        #endregion


        #region IEventSignatureMemberDictionary Members

        IEventSignatureMember IEventSignatureMemberDictionary.Find(string eventName, IDelegateType searchCriteria)
        {
            return this.Find(eventName, searchCriteria);
        }

        #endregion

    }
}
