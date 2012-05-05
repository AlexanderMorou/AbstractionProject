using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// The primitive types represented by the abstraction framework.
    /// </summary>
    public enum PrimitiveType
    {
        /// <summary>
        /// A boolean publicKey type.
        /// </summary>
        Boolean,
        /// <summary>
        /// A byte publicKey type.
        /// </summary>
        Byte,
        /// <summary>
        /// A signed byte publicKey type.
        /// </summary>
        SByte,
        /// <summary>
        /// A signed 16-bit value.
        /// </summary>
        Int16,
        /// <summary>
        /// An unsigned
        /// 16-bit value.
        /// </summary>
        UInt16,
        /// <summary>
        /// Asigned 32-bit value.
        /// </summary>
        Int32,
        /// <summary>
        /// An unsigned 32-bit value.
        /// </summary>
        UInt32,
        /// <summary>
        /// A signed 64-bit value.
        /// </summary>
        Int64,
        /// <summary>
        /// An unsigned 64-bit value.
        /// </summary>
        UInt64,
        /// <summary>
        /// A decimal value.
        /// </summary>
        Decimal,
        /// <summary>
        /// A single precision floating point value.
        /// </summary>
        Float,
        /// <summary>
        /// A double precision floating point value.
        /// </summary>
        Double,
        /// <summary>
        /// A unicode character.
        /// </summary>
        Char,
        /// <summary>
        /// A string of characters.
        /// </summary>
        String,
        /// <summary>
        /// A primitive null value.
        /// </summary>
        Null
    }
}
