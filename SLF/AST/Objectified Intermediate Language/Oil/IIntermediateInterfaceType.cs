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
    /// Defines properties and methods for working with an 
    /// intermediate interface type.
    /// </summary>
    public interface IIntermediateInterfaceType :
        IIntermediateMethodSignatureParent<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType>,
        IIntermediatePropertySignatureParentType<IInterfacePropertyMember, IIntermediateInterfacePropertyMember, IInterfaceType, IIntermediateInterfaceType>,
        IIntermediateEventSignatureParent<IInterfaceEventMember, IIntermediateInterfaceEventMember, IInterfaceType, IIntermediateInterfaceType>,
        IIntermediateIndexerSignatureParent<IInterfaceIndexerMember, IIntermediateInterfaceIndexerMember, IInterfaceType, IIntermediateInterfaceType>,
        IIntermediateGenericType<IInterfaceType, IIntermediateInterfaceType>,
        IIntermediateSegmentableType<IInterfaceType, IIntermediateInterfaceType>,
        IIntermediateTypeParent,
        IInterfaceType
    {
        /// <summary>
        /// Returns the classes associated
        /// to the <see cref="IIntermediateTypeParent"/>.
        /// </summary>
        /// <remarks>
        /// Not all languages support interfaces
        /// with nested types.
        /// </remarks>
        new IIntermediateClassTypeDictionary Classes { get; }
        /// <summary>
        /// Returns the delegates associated
        /// to the <see cref="IIntermediateTypeParent"/>.
        /// </summary>
        /// <remarks>
        /// Not all languages support interfaces
        /// with nested types.
        /// </remarks>
        new IIntermediateDelegateTypeDictionary Delegates { get; }
        /// <summary>
        /// Returns the enumerations associated
        /// to the <see cref="IIntermediateTypeParent"/>.
        /// </summary>
        /// <remarks>
        /// Not all languages support interfaces
        /// with nested types.
        /// </remarks>
        new IIntermediateEnumTypeDictionary Enums { get; }
        /// <summary>
        /// Returns the interfaces associated
        /// to the <see cref="IIntermediateTypeParent"/>.
        /// </summary>
        /// <remarks>
        /// Not all languages support interfaces
        /// with nested types.
        /// </remarks>
        new IIntermediateInterfaceTypeDictionary Interfaces { get; }
        /// <summary>
        /// Returns the data structures associated
        /// to the <see cref="IIntermediateTypeParent"/>.
        /// </summary>
        /// <remarks>
        /// Not all languages support interfaces
        /// with nested types.
        /// </remarks>
        new IIntermediateStructTypeDictionary Structs { get; }
        /// <summary>
        /// Returns the full set of types associated to
        /// the <see cref="IIntermediateTypeParent"/>.
        /// </summary>
        /// <remarks>
        /// Not all languages support interfaces
        /// with nested types.
        /// </remarks>
        new IIntermediateFullTypeDictionary Types { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateAssembly"/> in which the
        /// <see cref="IIntermediateTypeParent"/> is defined.
        /// </summary>
        /// <remarks>
        /// Not all languages support interfaces
        /// with nested types.
        /// </remarks>
        new IIntermediateAssembly Assembly { get; }
    }
    /* *
     *    /// <summary>
     *    /// Returns the <see cref="ITypeCollection"/> of
     *    /// <see cref="IType"/>s which the 
     *    /// <see cref="IIntermediateInterfaceType"/> implements.
     *    /// </summary>
     *    ITypeCollection ImplementedInterfaces { get; }
     * */
}
