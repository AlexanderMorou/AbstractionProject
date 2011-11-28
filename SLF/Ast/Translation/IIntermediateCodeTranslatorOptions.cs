using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Translation
{
    [Flags]
    public enum TranslationOrderKind
    {
        /// <summary>
        /// The order of the elements is supposed to be
        /// alphabetic.
        /// </summary>
        /* 01001 */
        Alphabetic = 0x9,
        /// <summary>
        /// The order of the elements is supposed to be 
        /// by the order specified.
        /// </summary>
        /* 10010 */
        Specific = 0x12,
        /* 11100 */
        /// <summary>
        /// The order of the elements is supposed
        /// to be by the order the elements were inserted.
        /// </summary>
        Verbatim = 0x1c,
    }
    public enum DeclarationTranslationOrder
    {
        BinaryOperatorCoercions,
        Classes,
        Constructors,
        Delegates,
        Enums,
        Events,
        Fields,
        Indexers,
        Interfaces,
        Methods,
        Properties,
        Structs,
        TypeCoercions,
        UnaryOperatorCoercions,
    }
    public interface IIntermediateCodeTranslatorOptions
    {
        /// <summary>
        /// Returns/sets the method used to determine how the
        /// elements are translated.
        /// </summary>
        TranslationOrderKind ElementOrderingMethod { get; set; }
        /// <summary>
        /// Returns/sets whether the code translator will extend the parent
        /// instance's scope to include the namespace of the type involved
        /// based off of the explicit types involved.
        /// </summary>
        bool AutoScope { get; set; }
        /// <summary>
        /// Returns/sets whether the code translator allows partial
        /// instances to be written to file, or whether the full code is
        /// written in one file.
        /// </summary>
        bool AllowPartials { get; set; }
    }
}
