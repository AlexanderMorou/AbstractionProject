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
    /// class type declaration.
    /// </summary>
    public interface IIntermediateClassType :
        IIntermediateGenericType<IClassType, IIntermediateClassType>,
        IIntermediateInstantiableType
            <IClassCtorMember, IIntermediateClassCtorMember, IClassEventMember, IIntermediateClassEventMember, IClassFieldMember,
             IIntermediateClassFieldMember, IClassIndexerMember, IIntermediateClassIndexerMember, IClassMethodMember, 
             IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember,
             IClassType, IIntermediateClassType>,
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
        /// Returns the <see cref="ITypeCollection"/> which denotes what interfaces
        /// are to be implemented by the <see cref="IIntermediateClassType"/>.
        /// </summary>
        new ITypeCollection ImplementedInterfaces { get; }
    }
}
