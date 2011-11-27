using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for working with an intermediate
    /// struct's implemented interfaces.
    /// </summary>
    /// <remarks>Used to mask the length of the <see cref="IIntermediateInstantiableTypeImplementedInterfaces{TEvent, TIntermediateEvent, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TParent, TIntermediateParent}"/>.</remarks>
    public interface IIntermediateStructImplementedInterfaces :
        IIntermediateInstantiableTypeImplementedInterfaces<IStructEventMember, IIntermediateStructEventMember, IStructIndexerMember, IIntermediateStructIndexerMember,
                                                           IStructMethodMember, IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember,
                                                           IStructType, IIntermediateStructType>
    {
        /// <summary>
        /// Obtains a <see cref="IIntermediateStructInterfaceMapping"/> for
        /// the <paramref name="interface"/> provided.
        /// </summary>
        /// <param name="interface">The <see cref="IInterfaceType"/>
        /// to implement.</param>
        /// <param name="insertPlaceholders">Whether or not to insert placeholders
        /// for the members not implicitly implemented.</param>
        /// <returns>A <see cref="IIntermediateStructInterfaceMapping"/>
        /// for the <paramref name="interface"/> provided.</returns>
        new IIntermediateStructInterfaceMapping ImplementInterface(IInterfaceType @interface, bool insertPlaceholders = false);

        /// <summary>
        /// Obtains the <see cref="IIntermediateStructInterfaceMapping"/>
        /// </summary>
        /// <param name="interface"></param>
        /// <returns></returns>
        new IIntermediateStructInterfaceMapping this[IInterfaceType @interface] { get; }
    }
}
