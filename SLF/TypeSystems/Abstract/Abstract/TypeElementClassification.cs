using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// The type of classification given to the <see cref="IType.ElementType"/>.
    /// </summary>
    public enum TypeElementClassification
    {
        /// <summary>
        /// The <see cref="IType"/> has no special element classification.
        /// </summary>
        None,
        /// <summary>
        /// The <see cref="IType"/> represents an array with <see cref="IType.ElementType"/> as
        /// the element type of the array.
        /// </summary>
        Array,
        /// <summary>
        /// The <see cref="IType"/> represents a nullable type with <see cref="IType.ElementType"/> as
        /// the normal form of the type.
        /// </summary>
        Nullable,
        /// <summary>
        /// The <see cref="IType"/> represents a pointer type with <see cref="IType.ElementType"/> as
        /// the normal form of the type.
        /// </summary>
        Pointer,
        /// <summary>
        /// The <see cref="IType"/> represents a by-reference type with <see cref="IType.ElementType"/> as
        /// the normal form of the type.
        /// </summary>
        Reference,
        /// <summary>
        /// The <see cref="IType.ElementType"/> represents the original form of the <see cref="IType"/>
        /// as an open generic wherein the current <see cref="IType"/> is a closed-form generic.
        /// </summary>
        GenericTypeDefinition,
    }
}
