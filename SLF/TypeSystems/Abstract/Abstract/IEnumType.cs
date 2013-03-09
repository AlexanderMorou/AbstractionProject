using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
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
        SByte = PrimitiveType.SByte,
        /// <summary>
        /// The base-type for the enumeration is a byte.
        /// </summary>
        Byte = PrimitiveType.Byte,
        /// <summary>
        /// The base-type for the enumeration is a 
        /// signed 16-bit integer.
        /// </summary>
        Int16 = PrimitiveType.Int16,
        /// <summary>
        /// The base-type for the enumeration is an unsigned
        /// 16-bit integer.
        /// </summary>
        UInt16 = PrimitiveType.UInt16,
        /// <summary>
        /// The base-type for the enumeration is a signed
        /// 32-bit integer.
        /// </summary>
        Int32 = PrimitiveType.Int32,
        /// <summary>
        /// The base-type for the enumeration is an unsigned
        /// 32-bit integer.
        /// </summary>
        UInt32 = PrimitiveType.UInt32,
        /// <summary>
        /// The base-type for the enumeration is a signed
        /// 64-bit integer.
        /// </summary>
        Int64 = PrimitiveType.Int64,
        /// <summary>
        /// The base-type for the enumeration is an unsigned 
        /// 64-bit integer.
        /// </summary>
        UInt64 = PrimitiveType.UInt64,
        /// <summary>
        /// The base-type for the enumeration is a signed
        /// native integer.
        /// </summary>
        NativeInteger,
        /// <summary>
        /// The base-type for the enumeration is an unsigned
        /// native integer.
        /// </summary>
        NativeUnsignedInteger,
    }
    /// <summary>
    /// Defines properties and methods for working with an enumeration type which contains a series of 
    /// constant fields.
    /// </summary>
    public interface IEnumType :
        IType<IGeneralTypeUniqueIdentifier, IEnumType>,
        IFieldParent<IEnumFieldMember, IEnumType>
    {
        /// <summary>
        /// Returns the <see cref="PrimitiveType"/> which is represented by
        /// the <see cref="IEnumType"/>.
        /// </summary>
        EnumerationBaseType ValueType { get; }
    }
}
