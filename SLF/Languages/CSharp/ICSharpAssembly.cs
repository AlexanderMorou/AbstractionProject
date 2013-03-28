using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Parsers;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Languages;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp
{
    /// <summary>
    /// Defines properties and methods for working with a C&#9839; Assembly.
    /// </summary>
    public interface ICSharpAssembly :
        IVersionedIntermediateAssembly<ICSharpLanguage, ICSharpProvider, CSharpLanguageVersion>
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateCliManager"/> used to 
        /// handle identities.
        /// </summary>
        IIntermediateCliManager IdentityManager { get; }
    }
}
