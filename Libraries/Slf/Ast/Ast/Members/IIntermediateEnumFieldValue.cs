using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// The type of value the enum's field value contains.
    /// </summary>
    public enum EnumValueType
    {
        /// <summary>
        /// The field member's value is a constant literal.
        /// </summary>
        Constant,
        /// <summary>
        /// The field member's value is automatically generated.
        /// </summary>
        /// 
        Automatic,
        /// <summary>
        /// The field member's value is mixed with literals and 
        /// other constant values.
        /// </summary>
        Mixed,
    }
    /// <summary>
    /// Defines properties and methods for working with the value of a field
    /// defined on an enumeration type.
    /// </summary>
    public interface IIntermediateEnumFieldValue
    {
        /// <summary>
        /// Returns the <see cref="EnumValueType"/> for the current
        /// <see cref="IIntermediateEnumFieldValue"/>.
        /// </summary>
        EnumValueType ValueType { get; }
    }
}
