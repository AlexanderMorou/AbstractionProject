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
    /// Defines properties and methods for working with an intermediate 
    /// class type declaration.
    /// </summary>
    public interface IIntermediateClassType :
        IIntermediateInstantiableType
            <IClassCtorMember, IIntermediateClassCtorMember, IClassEventMember, IIntermediateClassEventMember, IClassFieldMember,
             IIntermediateClassFieldMember, IClassIndexerMember, IIntermediateClassIndexerMember, IClassMethodMember, 
             IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember,
             IGeneralGenericTypeUniqueIdentifier, IClassType, IIntermediateClassType>,
        IIntermediateGenericType<IGeneralGenericTypeUniqueIdentifier, IClassType, IIntermediateClassType>,
        IClassType
    {
        /// <summary>
        /// Returns/sets the <see cref="SpecialClassModifier"/>
        /// applied to the <see cref="IIntermediateClassType"/>.
        /// </summary>
        new SpecialClassModifier SpecialModifier { get; set; }

        /// <summary>
        /// Returns/sets the type from which the current <see cref="IIntermediateClassType"/>
        /// derives.
        /// </summary>
        /// <exception cref="System.ArgumentException">thrown when the type
        /// provided contains the current type in its inheritance chain.</exception>
        new IClassType BaseType { get; set; }

        /// <summary>
        /// Returns the <see cref="IIntermediateAssembly"/> in which
        /// the <see cref="IIntermediateClassType"/> is declared
        /// </summary>
        new IIntermediateAssembly Assembly { get; }

        /// <summary>
        /// Returns the <see cref="IIntermediateInstantiableTypeImplementedInterfaces{TEvent, TIntermediateEvent, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TParent, TIntermediateParent}"/>
        /// which represents the interfaces implemented by the current 
        /// <see cref="IIntermediateClassType"/>.
        /// </summary>
        new IIntermediateInstantiableTypeImplementedInterfaces<IClassEventMember, IIntermediateClassEventMember, IClassIndexerMember, IIntermediateClassIndexerMember, 
                                                               IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember,
                                                               IClassType, IIntermediateClassType> ImplementedInterfaces { get; }

        /// <summary>
        /// Returns the <see cref="IIntermediateClassCtorMember"/> which denotes the
        /// constructor member that contains no parameters.
        /// </summary>
        IIntermediateClassCtorMember DefaultConstructor { get; }
    }
}
