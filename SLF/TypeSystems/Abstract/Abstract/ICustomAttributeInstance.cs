using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a custom attribute defined
    /// on a custom attributed declaration.
    /// </summary>
    public interface ICustomAttributeInstance :
        IDisposable
    {
        /// <summary>
        /// Returns the <see cref="IType"/> the <see cref="ICustomAttributeInstance"/>
        /// is.
        /// </summary>
        IType Type { get; }
        /// <summary>
        /// The <see cref="Attribute"/> represented by the <see cref="ICustomAttributeInstance"/>.
        /// </summary>
        Attribute WrappedAttribute { get; }
        /// <summary>
        /// The <see cref="ICustomAttributedDeclaration"/> on which the <see cref="ICustomAttributeInstance"/> 
        /// was declared.
        /// </summary>
        ICustomAttributedDeclaration DeclarationPoint { get; }
    }
}
