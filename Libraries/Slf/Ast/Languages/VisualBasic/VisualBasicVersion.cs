using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic
{
    /// <summary>
    /// Provides a series of values which relate to versions
    /// of the Visual Basic language.
    /// </summary>
    public enum VisualBasicVersion
    {
        /// <summary>
        /// The first version of Visual Basic,
        /// but the seventh version of Visual Basic.
        /// </summary>
        Version07 = 7,
        /// <summary>
        /// The second version of Visual Basic,
        /// but version 7.1 of Visual Basic.
        /// </summary>
        Version07_1 = 8,
        /// <summary>
        /// The first version of Visual Basic
        /// to contain generics.
        /// </summary>
        Version08 = 9,
        /// <summary>
        /// First version of Visual Basic
        /// to contain Language integrated query.
        /// </summary>
        Version09 = 10,
        /// <summary>
        /// The first version of Visual Basic
        /// to contain uniform dynamic dispatch functionality.
        /// </summary>
        Version10 = 11,
        /// <summary>
        /// The first version of Visual Basic
        /// to contain asynchronous methods.
        /// </summary>
        Version11 = 12,
        Version12 = 13,
        /// <summary>
        /// The current version of the Visual Basic Language.
        /// </summary>
        CurrentVersion = Version12,
    }
}
