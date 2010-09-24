using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Provides a series of custom attributes on a <see cref="ICustomAttributedDeclaration"/>.
    /// </summary>
    public interface ICustomAttributeCollection :
        IReadOnlyCollection<ICustomAttributeInstance>,
        IDisposable
    {
        /// <summary>
        /// Returns whether an attribute of the
        /// <paramref name="attributeType"/> provided is in the 
        /// <see cref="ICustomAttributeCollection"/>.
        /// </summary>
        /// <param name="attributeType">The type of attribute to check for.</param>
        /// <returns>true if an attribute of the <paramref name="attributeType"/>
        /// provided exists; false, otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="attributeType"/> is null.</exception>
        bool Contains(IType attributeType);

        /// <summary>
        /// Returns the <see cref="ICustomAttributedDeclaration"/> which contains
        /// the current <see cref="ICustomAttributeCollection"/>.
        /// </summary>
        ICustomAttributedDeclaration Parent { get; }
        /// <summary>
        /// Returns the <see cref="ICustomAttributeInstance"/> of the
        /// <paramref name="attributeType"/> provided.
        /// </summary>
        /// <param name="attributeType">The <see cref="IType"/>
        /// of the attribute to return the <see cref="ICustomAttributeInstance"/>
        /// of.</param>
        /// <returns>The <see cref="ICustomAttributeInstance"/> of the
        /// <paramref name="attributeType"/> provided.</returns>
        ICustomAttributeInstance this[IType attributeType] {get;}
    }
}
