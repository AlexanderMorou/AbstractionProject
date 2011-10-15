using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for a type-strict interface member mapping.
    /// </summary>
    /// <typeparam name="TEvent">The type of event represented by the mapping.</typeparam>
    /// <typeparam name="TEventSig">The type of event signature represented by the mapping.</typeparam>
    /// <typeparam name="TIntermediateEvent">The type of intermediate event represented by the mapping.</typeparam>
    /// <typeparam name="TIntermediateEventSig">The type of intermediate event signature represented by the mapping.</typeparam>
    /// <typeparam name="TIndexer">The type of indexer represented by the mapping.</typeparam>
    /// <typeparam name="TIndexerSig">The type of indexer signature represented by the mapping.</typeparam>
    /// <typeparam name="TIntermediateIndexer">The type of intermediate indexer member represented by the mapping.</typeparam>
    /// <typeparam name="TIntermediateIndexerSig">The type of intermediate indexer signature member represented by the mapping.</typeparam>
    /// <typeparam name="TMethod">The type of method member represented by the mapping.</typeparam>
    /// <typeparam name="TMethodSig">The type of method signature member represented by the mapping.</typeparam>
    /// <typeparam name="TIntermediateMethod">The type of intermediate method member represented by the mapping.</typeparam>
    /// <typeparam name="TIntermediateMethodSig">The type of intermediate method signature member represented by the mapping.</typeparam>
    /// <typeparam name="TProperty">The type of property member represented by the mapping.</typeparam>
    /// <typeparam name="TPropertySig">The type of property signature member represented by the mapping.</typeparam>
    /// <typeparam name="TIntermediateProperty">The type of intermediate property member represented by the mapping.</typeparam>
    /// <typeparam name="TIntermediatePropertySig">The type of intermediate property signature member represented by the mapping.</typeparam>
    /// <typeparam name="TParent">The base type of the member parent represented by the mapping.</typeparam>
    /// <typeparam name="TParentSig">The base type of the member signature parent represented by the mapping.</typeparam>
    /// <typeparam name="TIntermediateParent">The type of intermediate member parent represented by the mapping.</typeparam>
    /// <typeparam name="TIntermediateParentSig">The type of intermediate member signature parent represented by the mapping.</typeparam>
    public interface IIntermediateInterfaceMemberMapping<
            TEvent, TEventSig, TIntermediateEvent, TIntermediateEventSig, 
            TIndexer, TIndexerSig, TIntermediateIndexer, TIntermediateIndexerSig,
            TMethod, TMethodSig, TIntermediateMethod, TIntermediateMethodSig,
            TProperty, TPropertySig, TIntermediateProperty, TIntermediatePropertySig, 
            TParent, TParentSig, TIntermediateParent, TIntermediateParentSig> :
        IInterfaceMemberMapping<TMethod, TMethodSig, TProperty, TPropertySig, TEvent, TEventSig, TIndexer, TIndexerSig, TParent, TParentSig>
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
            TParent,
            IIntermediateEventParent<TEvent, TIntermediateEvent, TParent, TIntermediateParent>,
            IIntermediateIndexerParent<TIndexer, TIntermediateIndexer, TParent, TIntermediateParent>,
            IIntermediateMethodParent<TMethod, TIntermediateMethod, TParent, TIntermediateParent>,
            IIntermediatePropertyParent<TProperty, TIntermediateProperty, TParent, TIntermediateParent>
        where TIntermediateParentSig :
            TParentSig,
            IIntermediateEventSignatureParent<TEventSig, TIntermediateEventSig, TParentSig, TIntermediateParentSig>,
            IIntermediateIndexerSignatureParent<TIndexerSig, TIntermediateIndexerSig, TParentSig, TIntermediateParentSig>,
            IIntermediateMethodSignatureParent<TMethodSig, TIntermediateMethodSig, TParentSig, TIntermediateParentSig>,
            IIntermediatePropertySignatureParent<TPropertySig, TIntermediatePropertySig, TParentSig, TIntermediateParentSig>
    {
        /// <summary>
        /// Returns the list of implemented properties with
        /// a link back to the original interface member.
        /// </summary>
        IEnumerable<MemberMap<TIntermediateProperty, TIntermediatePropertySig>> IntermediateProperties { get; }
        /// <summary>
        /// Returns the list of implemented methods
        /// with a link back to the original interface 
        /// member.
        /// </summary>
        IEnumerable<MemberMap<TIntermediateMethod, TIntermediateMethodSig>> IntermediateMethods { get; }
        /// <summary>
        /// Returns the list of implemented indexers with
        /// a link back to the original interface member.
        /// </summary>
        IEnumerable<MemberMap<TIntermediateIndexer, TIntermediateIndexerSig>> IntermediateIndexers { get; }
        /// <summary>
        /// Returns the list of implemented indexers with
        /// a link back to the original interface member.
        /// </summary>
        IEnumerable<MemberMap<TIntermediateEvent, TIntermediateEventSig>> IntermediateEvents { get; }
    }
}
