using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public interface IIntermediateInstantiableTypeImplementedInterfaces<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType> :
        IControlledStateDictionary<IType, IIntermediateInterfaceMemberMapping<TEvent, IInterfaceEventMember, TIntermediateEvent,IIntermediateInterfaceEventMember, TIndexer, IInterfaceIndexerMember, TIntermediateIndexer, IIntermediateInterfaceIndexerMember, TMethod, IInterfaceMethodMember, TIntermediateMethod, IIntermediateInterfaceMethodMember, TProperty, IInterfacePropertyMember, TIntermediateProperty, IIntermediateInterfacePropertyMember, TType, IInterfaceType, TIntermediateType, IIntermediateInterfaceType>>,
        ITypeCollectionBase
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TIntermediateCtor :
            TCtor,
            IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
        where TEvent :
            IEventMember<TEvent, TType>
        where TIntermediateEvent :
            TEvent,
            IIntermediateEventMember<TEvent, TIntermediateEvent, TType, TIntermediateType>
        where TField :
            IFieldMember<TField, TType>,
            IInstanceMember
        where TIntermediateField :
            TField,
            IIntermediateFieldMember<TField, TIntermediateField, TType, TIntermediateType>
        where TIndexer :
            IIndexerMember<TIndexer, TType>
        where TIntermediateIndexer :
            TIndexer,
            IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TType, TIntermediateType>
        where TMethod :
            IMethodMember<TMethod, TType>,
            IExtendedInstanceMember
        where TIntermediateMethod :
            TMethod,
            IIntermediateMethodMember<TMethod, TIntermediateMethod, TType, TIntermediateType>
        where TProperty :
            IPropertyMember<TProperty, TType>
        where TIntermediateProperty :
            TProperty,
            IIntermediatePropertyMember<TProperty, TIntermediateProperty, TType, TIntermediateType>
        where TType :
            IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType>
        where TIntermediateType :
            TType,
            IIntermediateInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType>
    {

    }
}
