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

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic
{
    /// <summary>
    /// Provides a series of values which relate to versions
    /// of the 
    /// <see cref="IVisualBasicLanguage">Visual Basic.NET language</see>.
    /// </summary>
    public enum VisualBasicVersion
    {
        /* 0000111000001 */
        /// <summary>
        /// First version of 
        /// <see cref="IVisualBasicLanguage">Visual Basic.NET</see>
        /// to contain generics.
        /// </summary>
        Version08 = 8,
        /* 0011001000010 */
        /// <summary>
        /// First version of 
        /// <see cref="IVisualBasicLanguage">Visual Basic.NET</see>
        /// to contain Language integrated query.
        /// </summary>
        Version09 = 9,
        /* 0101010000100 */
        /// <summary>
        /// First version of 
        /// <see cref="IVisualBasicLanguage">Visual Basic.NET</see>
        /// to contain uniform dynamic dispatch functionality.
        /// </summary>
        Version10 = 10,
        /* 0110100001000 */
        /// <summary>
        /// First version of 
        /// <see cref="IVisualBasicLanguage">Visual Basic.NET</see>
        /// to contain asynchronous methods.
        /// </summary>
        Version11 = 11,
        CurrentVersion = Version11,
    }
}
