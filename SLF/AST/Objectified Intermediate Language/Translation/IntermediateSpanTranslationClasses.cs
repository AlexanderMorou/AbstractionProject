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

namespace AllenCopeland.Abstraction.Slf.Translation
{
    public enum IntermediateSpanTranslationClasses
    {
        /// <summary>
        /// No class has been entered as of yet.
        /// </summary>
        None,
        /// <summary>
        /// The current group represents a keyword
        /// or a series of keywords.
        /// </summary>
        Keyword,
        /// <summary>
        /// The current group represents an 
        /// identifier.
        /// </summary>
        Identifier,
        /// <summary>
        /// The current group represents a user type.
        /// </summary>
        UserType,
        /// <summary>
        /// The current group represents a delegate 
        /// type.
        /// </summary>
        UserDelegateType,
        /// <summary>
        /// The current group represents a user enum
        /// type.
        /// </summary>
        UserEnumType,
        /// <summary>
        /// The current group represents a user 
        /// interface type.
        /// </summary>
        UserInterfaceType,
        /// <summary>
        /// The current group represents a user 
        /// struct type.
        /// </summary>
        UserStructType,
        /// <summary>
        /// The current group represents an operator
        /// or series of operators..
        /// </summary>
        Operator,
        /// <summary>
        /// The current group represents a literal which
        /// has a numeric value.
        /// </summary>
        LiteralNumber,
        /// <summary>
        /// The current group represents a literal
        /// which consists of a sequence of unicode
        /// characters.
        /// </summary>
        LiteralString,
        /// <summary>
        /// The current group represents a literal
        /// which consists of a single unicode
        /// character.
        /// </summary>
        LiteralCharacter,
    }
}
