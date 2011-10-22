using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines generic properties and methods for working with a type that can be instantiated.
    /// </summary>
    /// <typeparam name="TCtor">The type used for the constructors in the current implementation.</typeparam>
    /// <typeparam name="TEvent">The type used for the events in the current implementation.</typeparam>
    /// <typeparam name="TField">The type used for the fields in the current implementation.</typeparam>
    /// <typeparam name="TIndexer">The type used for the indexers in the current implementation.</typeparam>
    /// <typeparam name="TMethod">The type used for the methods in the current implementation.</typeparam>
    /// <typeparam name="TProperty">The type used for the properties in the current implementation.</typeparam>
    /// 
    /// <typeparam name="TType">The <see cref="IInstantiableType{TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TTypeIdentifier, TType}"/> 
    /// in the implementation.</typeparam>
    public interface IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TTypeIdentifier, TType> :
        ICreatableParent<TCtor, TType>,
        ICoercibleType<TTypeIdentifier, TType>,
        IFieldParent<TField, TType>,
        IEventParent<TEvent, TType>,
        IMethodParent<TMethod, TType>,
        IIndexerParent<TIndexer, TType>,
        IPropertyParent<TProperty, TType>,
        IType<TTypeIdentifier, TType>,
        IInstantiableType
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TEvent :
            IEventMember<TEvent, TType>
        where TField :
            IFieldMember<TField, TType>,
            IInstanceMember
        where TIndexer :
            IIndexerMember<TIndexer, TType>
        where TMethod :
            IMethodMember<TMethod, TType>,
            IExtendedInstanceMember
        where TProperty :
            IPropertyMember<TProperty, TType>
        where TTypeIdentifier :
            ITypeUniqueIdentifier<TTypeIdentifier>
        where TType :
            IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TTypeIdentifier, TType>
    {
        /// <summary>
        /// Obtains a <see cref="IInterfaceMemberMapping{TMethod, TMethodSig, TProperty, TPropertySig, TEvent, TEventSig, TIndexer, TIndexerSig, TParent, TParentSig}"/> 
        /// related to the <paramref name="type"/> provided.
        /// </summary>
        /// <param name="type">The <see cref="IInterfaceType"/> 
        /// to obtain the map of.</param>
        /// <returns>A <see cref="IInterfaceMemberMapping{TMethod, TMethodSig, TProperty, TPropertySig, TEvent, TEventSig, TIndexer, TIndexerSig, TParent, TParentSig}"/> relative
        /// to the properties and methods implemented
        /// by the <typeparamref name="TType"/> with regards
        /// to <paramref name="type"/>.</returns>
        IInterfaceMemberMapping<
            TMethod, IInterfaceMethodMember, 
            TProperty, IInterfacePropertyMember,
            TEvent, IInterfaceEventMember, 
            TIndexer, IInterfaceIndexerMember,
            TType, IInterfaceType> GetInterfaceMap(IInterfaceType type);
    }
}