using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    public enum RuntimeCoreType
    {
        /// <summary>
        /// The definition of the root of all array types.
        /// </summary>
        ArrayType,
        /// <summary>
        /// The definition of the root of all asynchronous tasks.
        /// </summary>
        AsynchronousTask,
        /// <summary>
        /// The definition of the root of all generic asynchronous tasks.
        /// </summary>
        AsynchronousTaskOfT,
        /// <summary>
        /// The definition of a boolean value.
        /// </summary>
        Boolean,
        /// <summary>
        /// The definition of a decimal value.
        /// </summary>
        Decimal,
        /// <summary>
        /// The definition of a single-precision floating-point value.
        /// </summary>
        Single,
        /// <summary>
        /// The definition of a double-precision floating-point value.
        /// </summary>
        Double,
        /// <summary>
        /// The definition of a signed byte.
        /// </summary>
        SByte,
        /// <summary>
        /// The definition of an unsigned byte.
        /// </summary>
        Byte,
        /// <summary>
        /// The definition of a unicode character.
        /// </summary>
        Char,
        /// <summary>
        /// The definition of the root of the compiler generated metadatum.
        /// </summary>
        CompilerGeneratedMetadatum,
        /// <summary>
        /// The definition of the root of all delegate types
        /// </summary>
        Delegate,
        /// <summary>
        /// The definition of the root of all enumerations.
        /// </summary>
        EnumBaseType,
        /// <summary>
        /// The definition of a signed 16-bit integer.
        /// </summary>
        Int16,
        /// <summary>
        /// The definition of an unsigned 16-bit integer.
        /// </summary>
        UInt16,
        /// <summary>
        /// The definition of a signed 32-bit integer.
        /// </summary>
        Int32,
        /// <summary>
        /// The definition of an unsigned 32-bit integer.
        /// </summary>
        UInt32,
        /// <summary>
        /// The definition of a signed 64-bit integer.
        /// </summary>
        Int64,
        /// <summary>
        /// The definition of an unsigned 64-bit integer.
        /// </summary>
        UInt64,
        /// <summary>
        /// The definition of the root of all multicast delegate types.
        /// </summary>
        MulticastDelegate,
        /// <summary>
        /// The definition of the base type of a nullable type.
        /// </summary>
        NullableBaseType,
        /// <summary>
        /// The definition a nullable type.
        /// </summary>
        NullableType,
        /// <summary>
        /// The  
        /// definition of the root of all types.
        /// </summary>
        RootType,
        /// <summary>
        /// The definition of a series of unicode characters.
        /// </summary>
        String,
        /// <summary>
        /// The definition of the base of all value types.
        /// </summary>
        ValueTypeBaseType,
        /// <summary>
        /// The definition of the void type.
        /// </summary>
        VoidType,
    }
}
