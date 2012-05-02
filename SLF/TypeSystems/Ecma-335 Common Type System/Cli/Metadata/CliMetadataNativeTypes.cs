using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    /// <summary>
    /// The native types used
    /// </summary>
    public enum CliMetadataNativeTypes
    {
        /// <remarks>
/// Original name: ELEMENT_TYPE_END
/// </remarks>
ListEnd                = 0x00, // ELEMENT_TYPE_END
        /// <remarks>
        /// Original name: ELEMENT_TYPE_VOID
        /// </remarks>
        Void                   = 0x01, // ELEMENT_TYPE_VOID
        /// <summary>
        /// A boolean value.
        /// </summary>
        /// <remarks>
        /// Original name: ELEMENT_TYPE_BOOLEAN
        /// </remarks>
        Boolean                = 0x02, // ELEMENT_TYPE_BOOLEAN
        /// <summary>
        /// Represents a unicode character.
        /// </summary>
        /// <remarks>
        /// Original name: ELEMENT_TYPE_CHAR
        /// </remarks>
        Char                   = 0x03, // ELEMENT_TYPE_CHAR
        /// <summary>
        /// Represents a signed 8-bit integer.
        /// </summary>
        /// <remarks>
        /// Original name: ELEMENT_TYPE_I1
        /// </remarks>
        SByte                  = 0x04, // ELEMENT_TYPE_I1
        /// <summary>
        /// Represents an unsigned 8-bit integer.
        /// </summary>
        /// <remarks>
        /// Original name: ELEMENT_TYPE_U1
        /// </remarks>
        Byte                   = 0x05, // ELEMENT_TYPE_U1
        /// <summary>
        /// Represents a signed 16-bit integer.
        /// </summary>
        /// <remarks>
        /// Original name: ELEMENT_TYPE_I2
        /// </remarks>
        Int16                  = 0x06, // ELEMENT_TYPE_I2
        /// <summary>
        /// Represents an unsigned 16-bit integer.
        /// </summary>
        /// <remarks>
        /// Original name: ELEMENT_TYPE_U2
        /// </remarks>
        UInt16                 = 0x07, // ELEMENT_TYPE_U2
        /// <summary>
        /// Represents a signed 32 bit integer.
        /// </summary>
        /// <remarks>
        /// Original name: ELEMENT_TYPE_I4
        /// </remarks>
        Int32                  = 0x08, // ELEMENT_TYPE_I4
        /// <summary>
        /// Represents an unsigned 32-bit integer.
        /// </summary>
        /// <remarks>
        /// Original name: ELEMENT_TYPE_U4
        /// </remarks>
        UInt32                 = 0x09, // ELEMENT_TYPE_U4
        /// <remarks>
        /// Original name: ELEMENT_TYPE_I8
        /// </remarks>
        Int64                  = 0x0A, // ELEMENT_TYPE_I8
        /// <remarks>
        /// Original name: ELEMENT_TYPE_U8
        /// </remarks>
        UInt64                 = 0x0B, // ELEMENT_TYPE_U8
        /// <remarks>
        /// Original name: ELEMENT_TYPE_R4
        /// </remarks>
        Single                 = 0x0C, // ELEMENT_TYPE_R4 
        /// <remarks>
        /// Original name: ELEMENT_TYPE_R8
        /// </remarks>
        Double                 = 0x0D, // ELEMENT_TYPE_R8 
        /// <remarks>
        /// Original name: ELEMENT_TYPE_STRING
        /// </remarks>
        String                 = 0x0E, // ELEMENT_TYPE_STRING
        /// <remarks>
        /// Original name: ELEMENT_TYPE_PTR
        /// </remarks>
        Pointer                = 0x0F, // ELEMENT_TYPE_PTR
        /// <remarks>
        /// Original name: ELEMENT_TYPE_BYREF
        /// </remarks>
        ByRef                  = 0x10, // ELEMENT_TYPE_BYREF
        /// <remarks>
        /// Original name: ELEMENT_TYPE_VALUETYPE
        /// </remarks>
        ValueType              = 0x11, // ELEMENT_TYPE_VALUETYPE
        /// <remarks>
        /// Original name: ELEMENT_TYPE_CLASS
        /// </remarks>
        Class                  = 0x12, // ELEMENT_TYPE_CLASS
        /// <remarks>
        /// Original name: ELEMENT_TYPE_VAR
        /// </remarks>
        GenericTypeParameter   = 0x13, // ELEMENT_TYPE_VAR
        /// <remarks>
        /// Original name: ELEMENT_TYPE_ARRAY
        /// </remarks>
        Array                  = 0x14, // ELEMENT_TYPE_ARRAY
        /// <remarks>
        /// Original name: ELEMENT_TYPE_GENERICINST
        /// </remarks>
        GenericClosure         = 0x15, // ELEMENT_TYPE_GENERICINST
        /// <remarks>
        /// Original name: ELEMENT_TYPE_TYPEDBYREF
        /// </remarks>
        TypedByReference       = 0x16, // ELEMENT_TYPE_TYPEDBYREF
        /// <remarks>
        /// Original name: ELEMENT_TYPE_I
        /// </remarks>
        NativeInteger          = 0x18, // ELEMENT_TYPE_I
        /// <remarks>
        /// Original name: ELEMENT_TYPE_U
        /// </remarks>
        NativeUnsignedInteger  = 0x19, // ELEMENT_TYPE_U
        /// <remarks>
        /// Original name: ELEMENT_TYPE_FNPTR
        /// </remarks>
        FunctionPointer        = 0x1B, // ELEMENT_TYPE_FNPTR
        /// <remarks>
        /// Original name: ELEMENT_TYPE_OBJECT
        /// </remarks>
        Object                 = 0x1C, // ELEMENT_TYPE_OBJECT
        /// <remarks>
        /// Original name: ELEMENT_TYPE_SZARRAY
        /// </remarks>
        VectorArray            = 0x1D, // ELEMENT_TYPE_SZARRAY
        /// <remarks>
        /// Original name: ELEMENT_TYPE_MVAR
        /// </remarks>
        MethodGenericParameter = 0x1E, // ELEMENT_TYPE_MVAR
        /// <remarks>
        /// Original name: ELEMENT_TYPE_CMOD_REQD
        /// </remarks>
        RequiredModifier       = 0x1F, // ELEMENT_TYPE_CMOD_REQD
        /// <remarks>
        /// Original name: ELEMENT_TYPE_CMOD_OPT
        /// </remarks>
        OptionalModifier       = 0x20, // ELEMENT_TYPE_CMOD_OPT
        /// <remarks>
        /// Original name: ELEMENT_TYPE_INTERNAL
        /// </remarks>
        Internal               = 0x21, // ELEMENT_TYPE_INTERNAL
        /// <remarks>
        /// Original name: ELEMENT_TYPE_SENTINEL
        /// </remarks>
        Sentinel               = 0x41, // ELEMENT_TYPE_SENTINEL
        /// <remarks>
        /// Original name: ELEMENT_TYPE_PINNED
        /// </remarks>
        Pinned                 = 0x45, // ELEMENT_TYPE_PINNED
        /// <remarks>
        /// Original name: ELEMENT_TYPE_TYPE
        /// </remarks>
        Type                   = 0x50, // ELEMENT_TYPE_TYPE
        /// <remarks>
        /// Original name: ELEMENT_TYPE_SIGNAL_FIELD
        /// </remarks>
        FieldSignal            = 0x53, // ELEMENT_TYPE_SIGNAL_FIELD
        /// <remarks>
        /// Original name: ELEMENT_TYPE_SIGNAL_PROPERTY
        /// </remarks>
        PropertySignal         = 0x54, // ELEMENT_TYPE_SIGNAL_PROPERTY
        /// <remarks>
        /// Original name: ELEMENT_TYPE_SIGNAL_ENUM
        /// </remarks>
        EnumSignal             = 0x55, // ELEMENT_TYPE_SIGNAL_ENUM
    };
}
