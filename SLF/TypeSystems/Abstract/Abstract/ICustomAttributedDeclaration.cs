using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a declaration
    /// that contains custom attributes.
    /// </summary>
    public interface ICustomAttributedDeclaration :
        IDeclaration
    {
        /// <summary>
        /// Returns the <see cref="ICustomAttributeCollection"/> 
        /// associated to the <see cref="ICustomAttributedDeclaration"/>.
        /// </summary>
        ICustomAttributeCollection CustomAttributes { get; }
        /// <summary>
        /// Determines whether the <paramref name="attributeType"/> 
        /// is defined on the current 
        /// <see cref="ICustomAttributedDeclaration"/>.
        /// </summary>
        /// <param name="attributeType">The <see cref="IType"/> of
        /// the attribute to check the presence of.</param>
        /// <returns>true if an attribute of the given 
        /// <paramref name="attributeType"/> is defined
        /// on the current <see cref="ICustomAttributedDeclaration"/>.
        /// </returns>
        bool IsDefined(IType attributeType);
    }
}
