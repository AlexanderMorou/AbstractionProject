using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for working with a custom attribute definition.
    /// </summary>
    public interface IMetadatumDefinition :
        IMetadatum
    {
        /// <summary>
        /// Returns/sets the <see cref="IType"/>
        /// which the <see cref="IMetadatumDefinition"/>
        /// defines.
        /// </summary>
        new IType Type { get; set; }
        /// <summary>
        /// The <see cref="IIntermediateMetadataEntity"/> on which the <see cref="IMetadatumDefinition"/> 
        /// was declared.
        /// </summary>
        new IIntermediateMetadataEntity DeclarationPoint { get; }
        /// <summary>
        /// Returns the <see cref="IMetadataDefinitionParameterCollection"/>
        /// which determines the constructor arguments and properties set on the
        /// <see cref="IMetadatumDefinition"/>.
        /// </summary>
        new IMetadataDefinitionParameterCollection Parameters { get; }
    }
}
