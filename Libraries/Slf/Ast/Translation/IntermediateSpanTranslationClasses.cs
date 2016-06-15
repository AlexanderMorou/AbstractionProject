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

namespace AllenCopeland.Abstraction.Slf.Translation
{
    /// <summary>
    /// Denotes the kind of span within the scope of a given written 
    /// term.
    /// </summary>
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
        /// or series of operators.
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
        /// which consists of a sequence of unicode
        /// characters in its alternate form.
        /// </summary>
        LiteralStringAlternate,
        /// <summary>
        /// The current group represents a string of 
        /// characters which adds context to the current scope.
        /// </summary>
        Comment,
        /// <summary>
        /// The current group represents a literal
        /// which consists of a single unicode
        /// character.
        /// </summary>
        LiteralCharacter,
        /// <summary>
        /// The current group represents a term which is
        /// the name of a constructor.
        /// </summary>
        ConstructorName,
        /// <summary>
        /// The current group represents a term which is
        /// a reference to a method.
        /// </summary>
        MethodReference,
        /// <summary>
        /// The current group represents a term which is
        /// a reference to a parameter.
        /// </summary>
        ParameterReference,
        /// <summary>
        /// The current group represents a term which is
        /// a reference to a property.
        /// </summary>
        PropertyReference,
        /// <summary>
        /// The current group represents a term which is
        /// a reference to an event.
        /// </summary>
        EventReference,
        /// <summary>
        /// The current group represents a term which is
        /// a reference to an indexer.
        /// </summary>
        IndexerReference,
        /// <summary>
        /// The current group represents a term which is
        /// a reference to a field.
        /// </summary>
        FieldReference,
        /// <summary>
        /// The current group represents a term which is
        /// a reference to a local variable.
        /// </summary>
        LocalReference,
        /// <summary>
        /// The group represents a term which is a
        /// symbol, whose identity has not yet been resolved.
        /// </summary>
        Symbol,
        /// <summary>
        /// The group represents a term which is a label.
        /// </summary>
        Label,
        GenericParameterReference,
    }

}
