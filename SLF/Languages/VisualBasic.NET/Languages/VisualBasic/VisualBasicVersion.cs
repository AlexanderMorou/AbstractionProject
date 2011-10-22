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
    [Flags]
    public enum VisualBasicVersion
    {
        /* 0000111000001 */
        /// <summary>
        /// First version of 
        /// <see cref="IVisualBasicLanguage">Visual Basic.NET</see>
        /// to contain generics.
        /// </summary>
        Version08 = 0x1c1,
        /* 0011001000010 */
        /// <summary>
        /// First version of 
        /// <see cref="IVisualBasicLanguage">Visual Basic.NET</see>
        /// to contain Language integrated query.
        /// </summary>
        Version09 = 0x642,
        /* 0101010000100 */
        /// <summary>
        /// First version of 
        /// <see cref="IVisualBasicLanguage">Visual Basic.NET</see>
        /// to contain uniform dynamic dispatch functionality.
        /// </summary>
        Version10 = 0xa84,
        /* 0110100001000 */
        /// <summary>
        /// First version of 
        /// <see cref="IVisualBasicLanguage">Visual Basic.NET</see>
        /// to contain asynchronous methods.
        /// </summary>
        Version11 = 0xd08,
        /* 1000000010000 */
        /// <summary>
        /// The version of the 
        /// <see cref="IVisualBasicLanguage">Visual Basic.NET</see>
        /// which contains a minimal subset of functionality
        /// of the VB Language embedded within the resulting assembly.
        /// </summary>
        /// <remarks>Do not use with <see cref="VersionRegular"/>.</remarks>
        VersionCore = 0x1010,
        /* 1000000100000 */
        /// <summary>
        /// The version of the 
        /// <see cref="IVisualBasicLanguage">Visual Basic.NET</see>
        /// which references the visual basic runtime
        /// (Microsoft.VisualBasic.dll).
        /// </summary>
        /// <remarks>Do not use with <see cref="VersionCore"/>.</remarks>
        VersionRegular = 0x1020,
    }
}
