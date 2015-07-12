using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for a type-strict interface member mapping.
    /// </summary>
    /// <remarks>A vigenuple of type-parameters is required to create a generic closure.</remarks>
    /// <typeparam name="TEvent">The type of event represented by the mapping.</typeparam>
    /// <typeparam name="TIntermediateEvent">The type of intermediate event represented by the mapping.</typeparam>
    /// <typeparam name="TIndexer">The type of indexer represented by the mapping.</typeparam>
    /// <typeparam name="TIntermediateIndexer">The type of intermediate indexer member represented by the mapping.</typeparam>
    /// <typeparam name="TMethod">The type of method member represented by the mapping.</typeparam>
    /// <typeparam name="TIntermediateMethod">The type of intermediate method member represented by the mapping.</typeparam>
    /// <typeparam name="TProperty">The type of property member represented by the mapping.</typeparam>
    /// <typeparam name="TIntermediateProperty">The type of intermediate property member represented by the mapping.</typeparam>
    /// <typeparam name="TParent">The base type of the member parent represented by the mapping.</typeparam>
    /// <typeparam name="TIntermediateParent">The type of intermediate member parent represented by the mapping.</typeparam>
    public interface IIntermediateInterfaceMemberMapping<
            TEvent, TIntermediateEvent, 
            TIndexer, TIntermediateIndexer,
            TMethod, TIntermediateMethod,
            TProperty, TIntermediateProperty, 
            TParent, TIntermediateParent> :
        IInterfaceMemberMapping<TMethod, TProperty, TEvent, TIndexer, TParent>
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
        /// <summary>
        /// Returns the list of implemented properties with
        /// a link back to the original interface member.
        /// </summary>
        IEnumerable<MemberMap<IGeneralMemberUniqueIdentifier, TIntermediateProperty, IInterfacePropertyMember>> IntermediateProperties { get; }
        /// <summary>
        /// Returns the list of implemented methods
        /// with a link back to the original interface 
        /// member.
        /// </summary>
        IEnumerable<MemberMap<IGeneralGenericSignatureMemberUniqueIdentifier, TIntermediateMethod, IInterfaceMethodMember>> IntermediateMethods { get; }
        /// <summary>
        /// Returns the list of implemented indexers with
        /// a link back to the original interface member.
        /// </summary>
        IEnumerable<MemberMap<IGeneralSignatureMemberUniqueIdentifier, TIntermediateIndexer, IInterfaceIndexerMember>> IntermediateIndexers { get; }
        /// <summary>
        /// Returns the list of implemented indexers with
        /// a link back to the original interface member.
        /// </summary>
        IEnumerable<MemberMap<IGeneralSignatureMemberUniqueIdentifier, TIntermediateEvent, IInterfaceEventMember>> IntermediateEvents { get; }
    }
}
