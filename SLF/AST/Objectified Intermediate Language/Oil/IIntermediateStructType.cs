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
    /// Defines properties and methods for working with an intermediate
    /// data structure type declaration.
    /// </summary>
    public interface IIntermediateStructType :
        IIntermediateInstantiableType<IGeneralGenericTypeUniqueIdentifier, IStructCtorMember, IIntermediateStructCtorMember, IStructEventMember, 
            IIntermediateStructEventMember, IStructFieldMember, IIntermediateStructFieldMember, 
            IStructIndexerMember, IIntermediateStructIndexerMember, IStructMethodMember, 
            IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember, 
            IStructType, IIntermediateStructType>,
        IIntermediateGenericType<IGeneralGenericTypeUniqueIdentifier, IStructType, IIntermediateStructType>,
        IStructType
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateAssembly"/> in which
        /// the <see cref="IIntermediateStructType"/> is declared
        /// </summary>
        new IIntermediateAssembly Assembly { get; }

        /// <summary>
        /// Returns the <see cref="IIntermediateInstantiableTypeImplementedInterfaces{TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType}"/>
        /// which represents the interfaces implemented by the current 
        /// <see cref="IIntermediateStructType"/>.
        /// </summary>
        new IIntermediateInstantiableTypeImplementedInterfaces<IStructCtorMember, IIntermediateStructCtorMember, IStructEventMember,
                                                               IIntermediateStructEventMember, IStructFieldMember, IIntermediateStructFieldMember,
                                                               IStructIndexerMember, IIntermediateStructIndexerMember, IStructMethodMember,
                                                               IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember,
                                                               IStructType, IIntermediateStructType> ImplementedInterfaces { get; }
    }
}
