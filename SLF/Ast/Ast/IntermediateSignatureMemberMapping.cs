using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public class IntermediateSignatureMemberMapping<
            TEvent, TIntermediateEvent, 
            TIndexer, TIntermediateIndexer,
            TMethod, TIntermediateMethod,
            TProperty, TIntermediateProperty, 
            TParent, TIntermediateParent> :
        IIntermediateInterfaceMemberMapping<
                TEvent, TIntermediateEvent,
                TIndexer, TIntermediateIndexer,
                TMethod, TIntermediateMethod,
                TProperty, TIntermediateProperty,
                TParent, TIntermediateParent>
        where TEvent :
            IEventMember<TEvent, TParent>
        where TIntermediateEvent :
            TEvent,
            IIntermediateEventMember<TEvent, TIntermediateEvent, TParent, TIntermediateParent>
        where TIndexer :
            IIndexerMember<TIndexer, TParent>
        where TIntermediateIndexer :
            TIndexer,
            IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TParent, TIntermediateParent>
        where TMethod :
            IMethodMember<TMethod, TParent>,
            IExtendedInstanceMember
        where TIntermediateMethod :
            TMethod,
            IIntermediateMethodMember<TMethod, TIntermediateMethod, TParent, TIntermediateParent>
        where TProperty :
            IPropertyMember<TProperty, TParent>
        where TIntermediateProperty :
            TProperty,
            IIntermediatePropertyMember<TProperty, TIntermediateProperty, TParent, TIntermediateParent>
        where TParent :
            IEventParent<TEvent,TParent>,
            IIndexerParent<TIndexer, TParent>,
            IMethodParent<TMethod, TParent>,
            IPropertyParent<TProperty, TParent>
        where TIntermediateParent :
            IIntermediateEventParent<TEvent, TIntermediateEvent, TParent, TIntermediateParent>,
            IIntermediateIndexerParent<TIndexer, TIntermediateIndexer, TParent, TIntermediateParent>,
            IIntermediateMethodParent<TMethod, TIntermediateMethod, TParent, TIntermediateParent>,
            IIntermediatePropertyParent<TProperty, TIntermediateProperty, TParent, TIntermediateParent>,
            TParent
    {
        #region IIntermediateInterfaceMemberMapping<TEvent,TIntermediateEvent,TIndexer,TIntermediateIndexer,TMethod,TIntermediateMethod,TProperty,TIntermediateProperty,TParent,TIntermediateParent> Members

        public IEnumerable<MemberMap<IGeneralMemberUniqueIdentifier, TIntermediateProperty, IInterfacePropertyMember>> IntermediateProperties
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<MemberMap<IGeneralGenericSignatureMemberUniqueIdentifier, TIntermediateMethod, IInterfaceMethodMember>> IntermediateMethods
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<MemberMap<IGeneralSignatureMemberUniqueIdentifier, TIntermediateIndexer, IInterfaceIndexerMember>> IntermediateIndexers
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<MemberMap<IGeneralSignatureMemberUniqueIdentifier, TIntermediateEvent, IInterfaceEventMember>> IntermediateEvents
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IInterfaceMemberMapping<TMethod,TMethodSig,TProperty,TPropertySig,TEvent,TEventSig,TIndexer,TIndexerSig,TParent,TParentSig> Members

        public IInterfaceType Target
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<MemberMap<IGeneralMemberUniqueIdentifier, TProperty, IInterfacePropertyMember>> Properties
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<MemberMap<IGeneralGenericSignatureMemberUniqueIdentifier, TMethod, IInterfaceMethodMember>> Methods
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<MemberMap<IGeneralSignatureMemberUniqueIdentifier, TIndexer, IInterfaceIndexerMember>> Indexers
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<MemberMap<IGeneralSignatureMemberUniqueIdentifier, TEvent, IInterfaceEventMember>> Events
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
