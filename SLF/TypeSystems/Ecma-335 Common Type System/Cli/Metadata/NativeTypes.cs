using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    /// <summary>
    /// The native types used
    /// </summary>
    public enum NativeTypes
    {
        ListEnd                = 0x00, // ELEMENT_TYPE_END
        Void                   = 0x01, // ELEMENT_TYPE_VOID
        /// <summary>
        /// A boolean value.
        /// </summary>
        Boolean                = 0x02, // ELEMENT_TYPE_BOOLEAN
        Char                   = 0x03, // ELEMENT_TYPE_CHAR
        /// <summary>
        /// A signed 8-bit integer
        /// </summary>
        SByte                  = 0x04, // ELEMENT_TYPE_I1
        Byte                   = 0x05, // ELEMENT_TYPE_U1
        Int16                  = 0x06, // ELEMENT_TYPE_I2
        UInt16                 = 0x07, // ELEMENT_TYPE_U2
        Int32                  = 0x08, // ELEMENT_TYPE_I4
        UInt32                 = 0x09, // ELEMENT_TYPE_U4
        Int64                  = 0x0A, // ELEMENT_TYPE_I8
        UInt64                 = 0x0B, // ELEMENT_TYPE_U8
        Single                 = 0x0C, // ELEMENT_TYPE_R4 
        Double                 = 0x0D, // ELEMENT_TYPE_R8 
        String                 = 0x0E, // ELEMENT_TYPE_STRING
        Pointer                = 0x0F, // ELEMENT_TYPE_PTR
        ByRef                  = 0x10, // ELEMENT_TYPE_BYREF
        ValueType              = 0x11, // ELEMENT_TYPE_VALUETYPE
        Class                  = 0x12, // ELEMENT_TYPE_CLASS
        GenericTypeParameter   = 0x13, // ELEMENT_TYPE_VAR
        Array                  = 0x14, // ELEMENT_TYPE_ARRAY
        GenericClosure         = 0x15, // ELEMENT_TYPE_GENERICINST
        TypedByReference       = 0x16, // ELEMENT_TYPE_TYPEDBYREF
        NativeInteger          = 0x18, // ELEMENT_TYPE_I
        NativeUnsignedInteger  = 0x19, // ELEMENT_TYPE_U
        FunctionPointer        = 0x1B, // ELEMENT_TYPE_FNPTR
        Object                 = 0x1C, // ELEMENT_TYPE_OBJECT
        VectorArray            = 0x1D, // ELEMENT_TYPE_SZARRAY
        MethodGenericParameter = 0x1E, // ELEMENT_TYPE_MVAR
        RequiredModifier       = 0x1F, // ELEMENT_TYPE_CMOD_REQD
        OptionalModifier       = 0x20, // ELEMENT_TYPE_CMOD_OPT
        Internal               = 0x21, // ELEMENT_TYPE_INTERNAL
        Sentinel               = 0x41, // ELEMENT_TYPE_SENTINEL
        Pinned                 = 0x45, // ELEMENT_TYPE_PINNED
        Type                   = 0x50, // ELEMENT_TYPE_TYPE
        FieldSignal            = 0x53, // ELEMENT_TYPE_SIGNAL_FIELD
        PropertySignal         = 0x54, // ELEMENT_TYPE_SIGNAL_PROPERTY
        EnumSignal             = 0x55, // ELEMENT_TYPE_SIGNAL_ENUM
    };
}
