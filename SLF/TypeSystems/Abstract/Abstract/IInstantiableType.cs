using System.Collections.Generic;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a type that can be instantiated.
    /// </summary>
    public interface IInstantiableType :
        ICreatableParent,
        ICoercibleType,
        IEventParent,
        IFieldParent,
        IIndexerParent,
        IMethodParent,
        IPropertyParent,
        ITypeParent,
        IType
    {
        /// <summary>
        /// Returns a series of string values which relate to the 
        /// identifiers contained within the <see cref="IInstantiableType"/>
        /// </summary>
        new IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers { get; }
    }
}
