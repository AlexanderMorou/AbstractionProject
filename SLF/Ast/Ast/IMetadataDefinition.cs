using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for working with a series
    /// of custom attribute definition instances.
    /// </summary>
    public interface IMetadataDefinition :
        IControlledCollection<IMetadatumDefinition>,
        IDisposable
    {
        /// <summary>
        /// Returns the <see cref="IMetadataDefinitionCollection"/>
        /// which contains the <see cref="IMetadataDefinition"/>.
        /// </summary>
        IMetadataDefinitionCollection Parent { get; }
        /// <summary>
        /// Adds a <see cref="IMetadatumDefinition"/> with the
        /// <paramref name="values"/> provided.
        /// </summary>
        /// <param name="values">A series of varied type values and possible names along with the attribute's <see cref="IType"/>.</param>
        /// <returns>A <see cref="IMetadatumDefinition"/> instance with the
        /// <paramref name="values"/> provided.</returns>
        /// <exception cref="System.ArgumentException">thrown when the <paramref name="values"/> points to a compiled attribute type
        /// which has no public constructor that matches the values given, or a property referenced in the named value series did not exist.</exception>
        /// <exception cref="System.ArgumentNullException"><paramref name="values"/>' <see cref="MetadatumDefinitionParameterValueCollection.AttributeType"/> is null.</exception>
        IMetadatumDefinition Add(MetadatumDefinitionParameterValueCollection values);
        /// <summary>
        /// Returns whether the <see cref="IMetadataDefinition"/> contains the
        /// an attribute of the <paramref name="metadatumType"/> provided.
        /// </summary>
        /// <param name="metadatumType">The <see cref="IType"/> of the attribute
        /// to search for.</param>
        /// <returns>true if a definition of an attribute is of or derived from
        /// the <paramref name="metadatumType"/> provided.</returns>
        bool Contains(IType metadatumType);
        /// <summary>
        /// Removes the <paramref name="customAttribute"/> defined within the
        /// <see cref="IMetadataDefinition"/>.
        /// </summary>
        /// <param name="customAttribute">The <see cref="IMetadatumDefinition"/>
        /// to remove.</param>
        void Remove(IMetadatumDefinition customAttribute);
    }
}
