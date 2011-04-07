using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with the parent of a type.
    /// </summary>
    public interface ITypeParent
    {
        /// <summary>
        /// Returns the <see cref="IClassTypeDictionary"/> associated
        /// to the <see cref="ITypeParent"/>.
        /// </summary>
        IClassTypeDictionary Classes { get; }
        /// <summary>
        /// Returns the <see cref="IDelegateTypeDictionary"/> associated
        /// to the <see cref="ITypeParent"/>.
        /// </summary>
        IDelegateTypeDictionary Delegates { get; }
        /// <summary>
        /// Returns the <see cref="IEnumTypeDictionary"/> associated
        /// to the <see cref="ITypeParent"/>.
        /// </summary>
        IEnumTypeDictionary Enums { get; }
        /// <summary>
        /// Returns the <see cref="IInterfaceTypeDictionary"/> associated
        /// to the <see cref="ITypeParent"/>.
        /// </summary>
        IInterfaceTypeDictionary Interfaces { get; }
        /// <summary>
        /// Returns the <see cref="IStructTypeDictionary"/> associated
        /// to the <see cref="ITypeParent"/>.
        /// </summary>
        IStructTypeDictionary Structs { get; }
        /// <summary>
        /// Returns the <see cref="IFullTypeDictionary"/>  associated to
        /// the <see cref="ITypeParent"/>.
        /// </summary>
        IFullTypeDictionary Types { get; }
        /// <summary>
        /// Returns the assembly in which the type parent is contained.
        /// </summary>
        IAssembly Assembly { get; }
        /// <summary>
        /// Returns a series of string values which relate to the 
        /// identifiers contained within the <see cref="ITypeParent"/>
        /// </summary>
        IEnumerable<string> AggregateIdentifiers { get; }
    }
}
