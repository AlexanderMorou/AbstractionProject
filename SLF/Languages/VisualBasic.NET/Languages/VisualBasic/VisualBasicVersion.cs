using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic
{
    /// <summary>
    /// Provides a series of values which relate to versions
    /// of the 
    /// <see cref="IVisualBasicLanguage">Visual Basic.NET language</see>.
    /// </summary>
    public enum VisualBasicVersion
    {
        /// <summary>
        /// First version of 
        /// <see cref="IVisualBasicLanguage">Visual Basic.NET</see>
        /// to contain generics.
        /// </summary>
        Version08 = 8,
        /// <summary>
        /// First version of 
        /// <see cref="IVisualBasicLanguage">Visual Basic.NET</see>
        /// to contain Language integrated query.
        /// </summary>
        Version09 = 9,
        /// <summary>
        /// First version of 
        /// <see cref="IVisualBasicLanguage">Visual Basic.NET</see>
        /// to contain uniform dynamic dispatch functionality.
        /// </summary>
        Version10 = 10,
        /// <summary>
        /// First version of 
        /// <see cref="IVisualBasicLanguage">Visual Basic.NET</see>
        /// to contain asynchronous methods.
        /// </summary>
        Version11 = 11,
        /// <summary>
        /// The current version of the Visual Basic Language.
        /// </summary>
        CurrentVersion = Version11,
    }
}
