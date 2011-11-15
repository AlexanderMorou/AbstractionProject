using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public interface IIntermediateInstantiableTypeImplementedInterfaces<TEvent, TIntermediateEvent, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType> :
        IControlledStateDictionary<IInterfaceType, IIntermediateInterfaceMemberMapping<TEvent, TIntermediateEvent, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType>>,
        ITypeCollectionBase
        where TEvent :
            IEventMember<TEvent, TType>
        where TIntermediateEvent :
            IIntermediateEventMember<TEvent, TIntermediateEvent, TType, TIntermediateType>,
            TEvent
        where TIndexer :
            IIndexerMember<TIndexer, TType>
        where TIntermediateIndexer :
            IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TType, TIntermediateType>,
            TIndexer
        where TMethod :
            IMethodMember<TMethod, TType>,
            IExtendedInstanceMember
        where TIntermediateMethod :
            IIntermediateMethodMember<TMethod, TIntermediateMethod, TType, TIntermediateType>,
            TMethod
        where TProperty :
            IPropertyMember<TProperty, TType>
        where TIntermediateProperty :
            IIntermediatePropertyMember<TProperty, TIntermediateProperty, TType, TIntermediateType>,
            TProperty
        where TType :
            IEventParent<TEvent, TType>,
            IIndexerParent<TIndexer, TType>,
            IMethodParent<TMethod, TType>,
            IPropertyParent<TProperty, TType>
            //IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TTypeIdentifier, TType>
        where TIntermediateType :
            IIntermediateEventParent<TEvent, TIntermediateEvent, TType, TIntermediateType>,
            IIntermediateIndexerParent<TIndexer, TIntermediateIndexer, TType, TIntermediateType>,
            IIntermediateMethodParent<TMethod, TIntermediateMethod, TType, TIntermediateType>,
            IIntermediatePropertyParent<TProperty, TIntermediateProperty, TType, TIntermediateType>,
            TType
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="interface"></param>
        /// <param name="insertPlaceholders"></param>
        /// <returns></returns>
        IIntermediateInterfaceMemberMapping<TEvent, TIntermediateEvent, 
                                            TIndexer, TIntermediateIndexer, 
                                            TMethod, TIntermediateMethod, 
                                            TProperty, TIntermediateProperty,
                                            TType, TIntermediateType> ImplementInterface(IInterfaceType @interface, bool insertPlaceholders = false);
        /// <summary>
        /// Implements an interface quickly versus enumerating through
        /// its elements and inserting placeholders.
        /// </summary>
        /// <param name="interface">The <see cref="IInterfaceType"/>
        /// to implement.</param>
        void ImplementInterfaceQuick(IInterfaceType @interface);
    }
}
