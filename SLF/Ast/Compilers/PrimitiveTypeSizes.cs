using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// The sizes of the primitive data types within the intermediate model.
    /// </summary>
    public enum PrimitiveTypeSizes
    {
        /// <summary>
        /// The size of the 8-bit signed integer.
        /// </summary>
        Int8Size = sizeof(sbyte),
        /// <summary>
        /// The size of the 16-bit signed integer.
        /// </summary>
        Int16Size = sizeof(short),
        /// <summary>
        /// The size of the 32-bit signed integer.
        /// </summary>
        Int32Size = sizeof(int),
        /// <summary>
        /// The size of the 64-bit signed integer.
        /// </summary>
        Int64Size = sizeof(long),
        /// <summary>
        /// The size of the 8-bit unsigned integer.
        /// </summary>
        UInt8Size = sizeof(byte),
        /// <summary>
        /// The size of the 16-bit unsigned integer used to
        /// represent unicode characters.
        /// </summary>
        Char = sizeof(char),
        /// <summary>
        /// The size of the 16-bit unsigned integer.
        /// </summary>
        UInt16Size = sizeof(ushort),
        /// <summary>
        /// The size of the 32-bit unsigned integer.
        /// </summary>
        UInt32Size = sizeof(uint),
        /// <summary>
        /// The size of the 64-bit unsigned integer.
        /// </summary>
        UInt64Size = sizeof(ulong),
        /// <summary>
        /// The size of the single-precision floating point
        /// number.
        /// </summary>
        SingleSize = sizeof(float),
        /// <summary>
        /// The size of the double-precision floating point
        /// number.
        /// </summary>
        DoubleSize = sizeof(double),
        /// <summary>
        /// The size of the decimal data type.
        /// </summary>
        DecimalSize = sizeof(decimal),
#if x86
        /// <summary>
        /// The size of the platform's unsigned native integer.
        /// </summary>
        NativeUIntSize = UInt32Size,
        /// <summary>
        /// The size of the platform's native integer.
        /// </summary>
        NativeIntSize = Int32Size,
#elif x64
        /// <summary>
        /// The size of the platform's native integer.
        /// </summary>
        NativeUIntSize = UInt64Size,
        /// <summary>
        /// The size of the platform's unsigned native integer.
        /// </summary>
        NativeIntSize = Int64Size,
#endif
    }
}
