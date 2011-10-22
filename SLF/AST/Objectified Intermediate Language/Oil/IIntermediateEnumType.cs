using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// The base-type for an enumeration type.
    /// </summary>
    public enum EnumerationBaseType
    {
        /// <summary>
        /// The base-type for the enumeration is the default.
        /// </summary>
        /// <remarks>
        /// Actual data-type is resolved at compile time
        /// based upon the langauge.
        /// </remarks>
        Default,
        /// <summary>
        /// The base-type for the enumeration is a signed byte.
        /// </summary>
        SByte = TypeCode.SByte,
        /// <summary>
        /// The base-type for the enumeration is a byte.
        /// </summary>
        Byte = TypeCode.Byte,
        /// <summary>
        /// The base-type for the enumeration is a 
        /// signed 16-bit integer.
        /// </summary>
        Int16 = TypeCode.Int16,
        /// <summary>
        /// The base-type for the enumeration is an unsigned
        /// 16-bit integer.
        /// </summary>
        UInt16 = TypeCode.UInt16,
        /// <summary>
        /// The base-type for the enumeration is a signed
        /// 32-bit integer.
        /// </summary>
        Int32 = TypeCode.Int32,
        /// <summary>
        /// The base-type for the enumeration is an unsigned
        /// 32-bit integer.
        /// </summary>
        UInt32 = TypeCode.UInt32,
        /// <summary>
        /// The base-type for the enumeration is a signed
        /// 64-bit integer.
        /// </summary>
        Int64 = TypeCode.Int64,
        /// <summary>
        /// The base-type for the enumeration is an unsigned 
        /// 64-bit integer.
        /// </summary>
        UInt64 = TypeCode.UInt64,
    }

    /// <summary>
    /// Defines properties and methods for working with an intermediate
    /// enumeration which defines a series of constant fields of a 
    /// specified common data type.
    /// </summary>
    public interface IIntermediateEnumType :
        IIntermediateMemberParent,
        IIntermediateType,
        IFieldParent<IEnumFieldMember, IEnumType>,
        IEnumType
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateEnumFieldMemberDictionary"/>
        /// for the current <see cref="IIntermediateEnumType"/>.
        /// </summary>
        new IIntermediateEnumFieldMemberDictionary Fields { get; }
        /// <summary>
        /// Returns/sets the <see cref="EnumerationBaseType"/> for the 
        /// <see cref="IIntermediateEnumType"/>.
        /// </summary>
        new EnumerationBaseType BaseType { get; set; }
        /// <summary>
        /// Returns the <see cref="IIntermediateAssembly"/> in which
        /// the <see cref="IIntermediateEnumType"/> is declared
        /// </summary>
        new IIntermediateAssembly Assembly { get; }
    }
}
