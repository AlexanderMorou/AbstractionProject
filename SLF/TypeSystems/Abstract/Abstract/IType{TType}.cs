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
    /// Defines generic properties and methods for working with a the definition of a type.
    /// </summary>
    /// <typeparam name="TType">The current type of <see cref="IType{TTypeIdentifier, TType}"/>.</typeparam>
    public interface IType<TIdentifier, TType> :
        IDeclaration<TIdentifier>,
        IType
        where TIdentifier :
            ITypeUniqueIdentifier<TIdentifier>
        where TType :
            IType<TIdentifier, TType>
    {
        /// <summary>
        /// Returns the element type of special classification types.
        /// </summary>
        new TType ElementType { get; }

        /// <summary>
        /// Returns the unique identifier for the current
        /// <see cref="IType{TTypeIdentifier, TType}"/> in its general case form.
        /// </summary>
        /// <remarks>Used to differentiate the ambiguity between
        /// <see cref="IDeclaration{TIdentifier}.UniqueIdentifier"/> and 
        /// <see cref="IType.UniqueIdentifier"/>.</remarks>
        new TIdentifier UniqueIdentifier { get; }
    }
}
