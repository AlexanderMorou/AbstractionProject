﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Describes the root types understood by most systems targeted by this framework.
    /// </summary>
    public enum RuntimeCoreType
    {
        /// <summary>
        /// The type described doesn't match any listed.
        /// </summary>
        None,
        /// <summary>
        /// The definition of the root of all array types.
        /// </summary>
        Array,
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
        /// The definition of the root of all enumerations.
        /// </summary>
        RootEnum,
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
        RootStruct,
        /// <summary>
        /// The definition of the void type.
        /// </summary>
        VoidType,
        /// <summary>
        /// The definition of the Type type.
        /// </summary>
        Type,
    }
}
