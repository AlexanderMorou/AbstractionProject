using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
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
    /// of custom attributes.
    /// </summary>
    public interface ICustomAttributeDefinition :
        ICustomAttributeInstance
    {
        /// <summary>
        /// Returns/sets the <see cref="IType"/>
        /// which the <see cref="ICustomAttributeDefinition"/>
        /// defines.
        /// </summary>
        new IType Type { get; set; }
        /// <summary>
        /// The <see cref="IIntermediateCustomAttributedDeclaration"/> on which the <see cref="ICustomAttributeDefinition"/> 
        /// was declared.
        /// </summary>
        new IIntermediateCustomAttributedDeclaration DeclarationPoint { get; }
        /// <summary>
        /// Returns the <see cref="ICustomAttributeDefinitionParameterCollection"/>
        /// which determines the constructor arguments and properties set on the
        /// <see cref="ICustomAttributeDefinition"/>.
        /// </summary>
        ICustomAttributeDefinitionParameterCollection Parameters { get; }
    }
}
