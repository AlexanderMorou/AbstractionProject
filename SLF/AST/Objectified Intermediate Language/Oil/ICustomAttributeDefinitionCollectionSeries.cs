using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for working with a series
    /// of defined attribute definition collections.
    /// </summary>
    public interface ICustomAttributeDefinitionCollectionSeries :
        IControlledStateCollection<ICustomAttributeDefinitionCollection>,
        IDisposable
    {
        /// <summary>
        /// Adds a blank <see cref="ICustomAttributeDefinitionCollection"/> to the 
        /// <see cref="ICustomAttributeDefinitionCollectionSeries"/> for expansion.
        /// </summary>
        /// <returns>A new, empty, <see cref="ICustomAttributeDefinitionCollection"/>.</returns>
        ICustomAttributeDefinitionCollection Add();
        /// <summary>
        /// Adds a series of <see cref="ICustomAttributeDefinition"/> instances
        /// to the <see cref="ICustomAttributeDefinitionCollectionSeries"/> and returns the resulted
        /// container <see cref="ICustomAttributeDefinitionCollection"/>.
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        ICustomAttributeDefinitionCollection Add(params ICustomAttributeDefinition[] attributes);
        /// <summary>
        /// Adds a series of <see cref="ICustomAttributeDefinition"/> instances
        /// based upon the <paramref name="attributeData"/>
        /// </summary>
        /// <param name="attributeData">
        /// The array of <see cref="CustomAttributeDefinition.ParameterValueCollection"/>
        /// which designates the types of the attributes and the named and unnamed parameters.
        /// </param>
        /// <returns></returns>
        ICustomAttributeDefinitionCollection Add(params CustomAttributeDefinition.ParameterValueCollection[] attributeData);
        /// <summary>
        /// Returns the <see cref="IIntermediateCustomAttributedDeclaration"/>
        /// which contains the <see cref="ICustomAttributeDefinitionCollectionSeries"/>.
        /// </summary>
        IIntermediateCustomAttributedDeclaration Parent { get; }
        /// <summary>
        /// Flattens the <see cref="ICustomAttributeDefinitionCollectionSeries"/> into a single 
        /// array of <see cref="ICustomAttributeDefinition"/>.
        /// </summary>
        /// <returns>An array of <see cref="ICustomAttributeDefinition"/> elements contained in all of the
        /// <see cref="ICustomAttributeDefinitionCollection"/>
        /// elements in the current <see cref="ICustomAttributeDefinitionCollectionSeries"/></returns>
        ICustomAttributeDefinition[] Flatten();
        /// <summary>
        /// Obtains the <see cref="ICustomAttributeDefinition"/> which the <paramref name="attributeType"/> 
        /// </summary>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        ICustomAttributeDefinition this[IType attributeType] { get; }
        /// <summary>
        /// Returns whether the <see cref="ICustomAttributeDefinitionCollectionSeries"/>
        /// contains an attribute definition of the specified <paramref name="attributeType"/>.
        /// </summary>
        /// <param name="attributeType">The <see cref="IType"/> of the attribute to scan for.</param>
        /// <returns>true if there is an instance with the <paramref name="attributeType"/> of the one specified,
        /// or if an element exists derived of the specified type; false, otherwise.</returns>
        bool Contains(IType attributeType);
        /// <summary>
        /// Returns the full count of <see cref="ICustomAttributeDefinition"/> elements
        /// contained within all <see cref="ICustomAttributeDefinitionCollection"/>
        /// in the current <see cref="ICustomAttributeDefinitionCollectionSeries"/>.
        /// </summary>
        int FullCount { get; }
        /// <summary>
        /// Obtains an attribute instance for an attribute definition 
        /// in the <see cref="ICustomAttributeDefinitionCollectionSeries"/>;
        /// if it is defined, the first instance's wrapper is returned.
        /// </summary>
        /// <typeparam name="TAttribute">The type of <see cref="Attribute"/>
        /// to retrieve, this only applies to fully quantified attributes, not close matches.</typeparam>
        /// <returns>An instance of <typeparamref name="TAttribute"/> if an instance of it is defined
        /// on the <see cref="ICustomAttributeDefinitionCollection"/>.</returns>
        TAttribute GetAttributeInstance<TAttribute>()
            where TAttribute :
                Attribute;
        /// <summary>
        /// Removes the <paramref name="customAttribute"/> defined on the
        /// <see cref="Parent"/>.
        /// </summary>
        /// <param name="customAttribute">The <see cref="ICustomAttributeDefinition"/>
        /// to remove.</param>
        void Remove(ICustomAttributeDefinition customAttribute);

        /// <summary>
        /// Removes the <paramref name="series"/> provided, given they are 
        /// defined on the <see cref="Parent"/>.
        /// </summary>
        /// <param name="series">The <see cref="ICustomAttributeDefinition"/>
        /// series to remove.</param>
        void RemoveSet(IEnumerable<ICustomAttributeDefinition> series);
    }
}
