using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public class IntermediateSignatureMemberMapping<
            TEvent, TEventSig, TIntermediateEvent, TIntermediateEventSig, 
            TIndexer, TIndexerSig, TIntermediateIndexer, TIntermediateIndexerSig,
            TMethod, TMethodSig, TIntermediateMethod, TIntermediateMethodSig,
            TProperty, TPropertySig, TIntermediateProperty, TIntermediatePropertySig, 
            TParent, TParentSig, TIntermediateParent, TIntermediateParentSig> :
        IIntermediateSignatureMemberMapping<
                TEvent, TEventSig, TIntermediateEvent, TIntermediateEventSig,
                TIndexer, TIndexerSig, TIntermediateIndexer, TIntermediateIndexerSig,
                TMethod, TMethodSig, TIntermediateMethod, TIntermediateMethodSig,
                TProperty, TPropertySig, TIntermediateProperty, TIntermediatePropertySig,
                TParent, TParentSig, TIntermediateParent, TIntermediateParentSig>
        where TEvent :
            IEventMember<TEvent, TParent>
        where TEventSig :
            IEventSignatureMember<TEventSig, TParentSig>
        where TIntermediateEvent :
            TEvent,
            IIntermediateEventMember<TEvent, TIntermediateEvent, TParent, TIntermediateParent>
        where TIntermediateEventSig :
            TEventSig,
            IIntermediateEventSignatureMember<TEventSig, TIntermediateEventSig, TParentSig, TIntermediateParentSig>
        where TIndexer :
            IIndexerMember<TIndexer, TParent>
        where TIndexerSig :
            IIndexerSignatureMember<TIndexerSig, TParentSig>
        where TIntermediateIndexer :
            TIndexer,
            IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TParent, TIntermediateParent>
        where TIntermediateIndexerSig :
            TIndexerSig,
            IIntermediateIndexerSignatureMember<TIndexerSig, TIntermediateIndexerSig, TParentSig, TIntermediateParentSig>
        where TMethod :
            IMethodMember<TMethod, TParent>,
            IExtendedInstanceMember
        where TMethodSig :
            IMethodSignatureMember<TMethodSig, TParentSig>
        where TIntermediateMethod :
            TMethod,
            IIntermediateMethodMember<TMethod, TIntermediateMethod, TParent, TIntermediateParent>
        where TIntermediateMethodSig :
            TMethodSig,
            IIntermediateMethodSignatureMember<TMethodSig, TIntermediateMethodSig, TParentSig, TIntermediateParentSig>
        where TProperty :
            IPropertyMember<TProperty, TParent>
        where TPropertySig :
            IPropertySignatureMember<TPropertySig, TParentSig>
        where TIntermediateProperty :
            TProperty,
            IIntermediatePropertyMember<TProperty, TIntermediateProperty, TParent, TIntermediateParent>
        where TIntermediatePropertySig :
            TPropertySig,
            IIntermediatePropertySignatureMember<TPropertySig, TIntermediatePropertySig, TParentSig, TIntermediateParentSig>
        where TParent :
            IEventParent<TEvent,TParent>,
            IIndexerParent<TIndexer, TParent>,
            IMethodParent<TMethod, TParent>,
            IPropertyParent<TProperty, TParent>
        where TParentSig :
            IEventSignatureParent<TEventSig, TParentSig>,
            IIndexerSignatureParent<TIndexerSig, TParentSig>,
            IMethodSignatureParent<TMethodSig, TParentSig>,
            IPropertySignatureParent<TPropertySig, TParentSig>
        where TIntermediateParent :
            IIntermediateEventParent<TEvent, TIntermediateEvent, TParent, TIntermediateParent>,
            IIntermediateIndexerParent<TIndexer, TIntermediateIndexer, TParent, TIntermediateParent>,
            IIntermediateMethodParent<TMethod, TIntermediateMethod, TParent, TIntermediateParent>,
            IIntermediatePropertyParent<TProperty, TIntermediateProperty, TParent, TIntermediateParent>,
            TParent
        where TIntermediateParentSig :
            IIntermediateEventSignatureParent<TEventSig, TIntermediateEventSig, TParentSig, TIntermediateParentSig>,
            IIntermediateIndexerSignatureParent<TIndexerSig, TIntermediateIndexerSig, TParentSig, TIntermediateParentSig>,
            IIntermediateMethodSignatureParent<TMethodSig, TIntermediateMethodSig, TParentSig, TIntermediateParentSig>,
            IIntermediatePropertySignatureParent<TPropertySig, TIntermediatePropertySig, TParentSig, TIntermediateParentSig>,
            TParentSig
    {
        #region IIntermediateSignatureMemberMapping<TEvent,TEventSig,TIntermediateEvent,TIntermediateEventSig,TIndexer,TIndexerSig,TIntermediateIndexer,TIntermediateIndexerSig,TMethod,TMethodSig,TIntermediateMethod,TIntermediateMethodSig,TProperty,TPropertySig,TIntermediateProperty,TIntermediatePropertySig,TParent,TParentSig,TIntermediateParent,TIntermediateParentSig> Members

        public IEnumerable<MemberMap<IGeneralMemberUniqueIdentifier, TIntermediateProperty, TIntermediatePropertySig>> IntermediateProperties
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<MemberMap<IGeneralGenericSignatureMemberUniqueIdentifier, TIntermediateMethod, TIntermediateMethodSig>> IntermediateMethods
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<MemberMap<IGeneralSignatureMemberUniqueIdentifier, TIntermediateIndexer, TIntermediateIndexerSig>> IntermediateIndexers
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<MemberMap<IGeneralSignatureMemberUniqueIdentifier, TIntermediateEvent, TIntermediateEventSig>> IntermediateEvents
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region ISignatureMemberMapping<TMethod,TMethodSig,TProperty,TPropertySig,TEvent,TEventSig,TIndexer,TIndexerSig,TParent,TParentSig> Members

        public IInterfaceType Target
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<MemberMap<IGeneralMemberUniqueIdentifier, TProperty, TPropertySig>> Properties
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<MemberMap<IGeneralGenericSignatureMemberUniqueIdentifier, TMethod, TMethodSig>> Methods
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<MemberMap<IGeneralSignatureMemberUniqueIdentifier, TIndexer, TIndexerSig>> Indexers
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<MemberMap<IGeneralSignatureMemberUniqueIdentifier, TEvent, TEventSig>> Events
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
