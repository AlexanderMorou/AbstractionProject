using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for working with a series
    /// of custom attribute definition instances.
    /// </summary>
    public interface ICustomAttributeDefinitionCollection :
        IControlledStateCollection<ICustomAttributeDefinition>,
        IDisposable
    {
        /// <summary>
        /// Returns the <see cref="ICustomAttributeDefinitionCollectionSeries"/>
        /// which contains the <see cref="ICustomAttributeDefinitionCollection"/>.
        /// </summary>
        ICustomAttributeDefinitionCollectionSeries Parent { get; }
        /// <summary>
        /// Adds a <see cref="ICustomAttributeDefinition"/> with the
        /// <paramref name="values"/> provided.
        /// </summary>
        /// <param name="values">A series of varied type values and possible names along with the attribute's <see cref="IType"/>.</param>
        /// <returns>A <see cref="ICustomAttributeDefinition"/> instance with the
        /// <paramref name="values"/> provided.</returns>
        /// <exception cref="System.ArgumentException">thrown when the <paramref name="values"/> points to a compiled attribute type
        /// which has no public constructor that matches the values given, or a property referenced in the named value series did not exist.</exception>
        /// <exception cref="System.ArgumentNullException"><paramref name="values"/>' <see cref="CustomAttributeDefinition.ParameterValueCollection.AttributeType"/> is null.</exception>
        ICustomAttributeDefinition Add(CustomAttributeDefinition.ParameterValueCollection values);
        /// <summary>
        /// Returns whether the <see cref="ICustomAttributeDefinitionCollection"/> contains the
        /// an attribute of the <paramref name="attributeType"/> provided.
        /// </summary>
        /// <param name="attributeType">The <see cref="IType"/> of the attribute
        /// to search for.</param>
        /// <returns>true if a definition of an attribute is of or derived from
        /// the <paramref name="attributeType"/> provided.</returns>
        bool Contains(IType attributeType);
        /// <summary>
        /// Removes the <paramref name="customAttribute"/> defined within the
        /// <see cref="ICustomAttributeDefinitionCollection"/>.
        /// </summary>
        /// <param name="customAttribute">The <see cref="ICustomAttributeDefinition"/>
        /// to remove.</param>
        void Remove(ICustomAttributeDefinition customAttribute);
    }
}
