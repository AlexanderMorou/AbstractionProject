using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for working with an intermediate
    /// class's implemented interfaces.
    /// </summary>
    /// <remarks>Used to mask the length of the <see cref="IIntermediateInstantiableTypeImplementedInterfaces{TEvent, TIntermediateEvent, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TParent, TIntermediateParent}"/>.</remarks>
    public interface IIntermediateClassImplementedInterfaces :
        IIntermediateInstantiableTypeImplementedInterfaces<IClassEventMember, IIntermediateClassEventMember, IClassIndexerMember, IIntermediateClassIndexerMember,
                                                           IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember,
                                                           IClassType, IIntermediateClassType>
    {
        /// <summary>
        /// Obtains a <see cref="IIntermediateClassInterfaceMapping"/> for
        /// the <paramref name="interface"/> provided.
        /// </summary>
        /// <param name="interface">The <see cref="IInterfaceType"/>
        /// to implement.</param>
        /// <param name="insertPlaceholders">Whether or not to insert placeholders
        /// for the members not implicitly implemented.</param>
        /// <returns>A <see cref="IIntermediateClassInterfaceMapping"/>
        /// for the <paramref name="interface"/> provided.</returns>
        new IIntermediateClassInterfaceMapping ImplementInterface(IInterfaceType @interface, bool insertPlaceholders = false);

        /// <summary>
        /// Obtains the <see cref="IIntermediateClassInterfaceMapping"/>
        /// </summary>
        /// <param name="interface"></param>
        /// <returns></returns>
        new IIntermediateClassInterfaceMapping this[IInterfaceType @interface] { get; }
    }
}
