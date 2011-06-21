using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
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
        Version08,
        /// <summary>
        /// First version of 
        /// <see cref="IVisualBasicLanguage">Visual Basic.NET</see>
        /// to contain Language integrated query.
        /// </summary>
        Version09,
        /// <summary>
        /// First version of 
        /// <see cref="IVisualBasicLanguage">Visual Basic.NET</see>
        /// to contain uniform dynamic dispatch functionality.
        /// </summary>
        Version10,
        /// <summary>
        /// First version of 
        /// <see cref="IVisualBasicLanguage">Visual Basic.NET</see>
        /// to contain asynchronous methods.
        /// </summary>
        Version11,
    }
}
