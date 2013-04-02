using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for working with a series
    /// of defined attribute definition collections.
    /// </summary>
    public interface IMetadataDefinitionCollection :
        IControlledCollection<IMetadataDefinition>,
        IDisposable
    {
        /// <summary>
        /// Returns the <see cref="ITypeIdentityManager"/> which manages the 
        /// identity of types within the current context.
        /// </summary>
        ITypeIdentityManager IdentityManager { get; }
        /// <summary>
        /// Adds a blank <see cref="IMetadataDefinition"/> to the 
        /// <see cref="IMetadataDefinitionCollection"/> for expansion.
        /// </summary>
        /// <returns>A new, empty, <see cref="IMetadataDefinition"/>.</returns>
        IMetadataDefinition Add();
        /// <summary>
        /// Adds a series of <see cref="IMetadatumDefinition"/> instances
        /// to the <see cref="IMetadataDefinitionCollection"/> and returns the resulted
        /// container <see cref="IMetadataDefinition"/>.
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        IMetadataDefinition Add(params IMetadatumDefinition[] attributes);
        /// <summary>
        /// Adds a series of <see cref="IMetadatumDefinition"/> instances
        /// based upon the <paramref name="attributeData"/>
        /// </summary>
        /// <param name="attributeData">
        /// The array of <see cref="MetadatumDefinitionParameterValueCollection"/>
        /// which designates the types of the attributes and the named and unnamed parameters.
        /// </param>
        /// <returns></returns>
        IMetadataDefinition Add(params MetadatumDefinitionParameterValueCollection[] attributeData);
        /// <summary>
        /// Returns the <see cref="IIntermediateMetadataEntity"/>
        /// which contains the <see cref="IMetadataDefinitionCollection"/>.
        /// </summary>
        IIntermediateMetadataEntity Parent { get; }
        /// <summary>
        /// Flattens the <see cref="IMetadataDefinitionCollection"/> into a single 
        /// array of <see cref="IMetadatumDefinition"/>.
        /// </summary>
        /// <returns>An array of <see cref="IMetadatumDefinition"/> elements contained in all of the
        /// <see cref="IMetadataDefinition"/>
        /// elements in the current <see cref="IMetadataDefinitionCollection"/></returns>
        IMetadatumDefinition[] Flatten();
        /// <summary>
        /// Obtains the <see cref="IMetadatumDefinition"/> which the <paramref name="metadatumType"/> 
        /// </summary>
        /// <param name="metadatumType"></param>
        /// <returns></returns>
        IMetadatumDefinition this[IType metadatumType] { get; }
        /// <summary>
        /// Returns whether the <see cref="IMetadataDefinitionCollection"/>
        /// contains an attribute definition of the specified <paramref name="metadatumType"/>.
        /// </summary>
        /// <param name="metadatumType">The <see cref="IType"/> of the attribute to scan for.</param>
        /// <returns>true if there is an instance with the <paramref name="metadatumType"/> of the one specified,
        /// or if an element exists derived of the specified type; false, otherwise.</returns>
        bool Contains(IType metadatumType);
        /// <summary>
        /// Returns the full count of <see cref="IMetadatumDefinition"/> elements
        /// contained within all <see cref="IMetadataDefinition"/>
        /// in the current <see cref="IMetadataDefinitionCollection"/>.
        /// </summary>
        int FullCount { get; }
        /// <summary>
        /// Removes the <paramref name="customAttribute"/> defined on the
        /// <see cref="Parent"/>.
        /// </summary>
        /// <param name="customAttribute">The <see cref="IMetadatumDefinition"/>
        /// to remove.</param>
        void Remove(IMetadatumDefinition customAttribute);

        /// <summary>
        /// Removes the <paramref name="series"/> provided, given they are 
        /// defined on the <see cref="Parent"/>.
        /// </summary>
        /// <param name="series">The <see cref="IMetadatumDefinition"/>
        /// series to remove.</param>
        void RemoveSet(IEnumerable<IMetadatumDefinition> series);
    }
}
