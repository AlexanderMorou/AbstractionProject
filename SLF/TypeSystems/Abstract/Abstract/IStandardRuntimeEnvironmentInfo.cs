using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    public interface IStandardRuntimeEnvironmentInfo
    {
        /// <summary>
        /// Returns the <see cref="IAssemblyUniqueIdentifier"/> of
        /// the core library of the runtime environment
        /// represented by the <see cref="IStandardRuntimeEnvironmentInfo"/>.
        /// </summary>
        IAssemblyUniqueIdentifier CoreLibraryIdentifier { get; }
        /// <summary>
        /// Returns whether the core library identified by <see cref="CoreLibraryIdentifier"/>
        /// is present.
        /// </summary>
        /// <remarks>If false, the <see cref="CoreLibraryIdentifier"/>
        /// will return null, type identity resolution from it
        /// will be unavailable.</remarks>
        bool UseCoreLibrary { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> relative to the runtime's
        /// definition of the root of all array types.
        /// </summary>
        IGeneralTypeUniqueIdentifier ArrayType { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> relative to the runtime's
        /// definition of the root of all asynchronous tasks.
        /// </summary>
        IGeneralTypeUniqueIdentifier AsynchronousTask { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> relative to the runtime's
        /// definition of the root of all generic asynchronous tasks.
        /// </summary>
        IGeneralTypeUniqueIdentifier AsynchronousTaskOfT { get; }

        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> relative to the runtime's
        /// definition of a boolean value.
        /// </summary>
        IGeneralTypeUniqueIdentifier Boolean { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> relative to the runtime's
        /// definition of a decimal value.
        /// </summary>
        IGeneralTypeUniqueIdentifier Decimal { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> relative to the runtime's
        /// definition of a single-precision floating-point value.
        /// </summary>
        IGeneralTypeUniqueIdentifier Single { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> relative to the runtime's
        /// definition of a double-precision floating-point value.
        /// </summary>
        IGeneralTypeUniqueIdentifier Double { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> relative to the runtime's
        /// definition of a signed byte.
        /// </summary>
        IGeneralTypeUniqueIdentifier SByte { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> relative to the runtime's
        /// definition of an unsigned byte.
        /// </summary>
        IGeneralTypeUniqueIdentifier Byte { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> relative to the runtime's
        /// definition of a unicode character.
        /// </summary>
        IGeneralTypeUniqueIdentifier Char { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> relative to the runtime's
        /// definition of the root of the compiler generated metadatum.
        /// </summary>
        IGeneralTypeUniqueIdentifier CompilerGeneratedMetadatum { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> relative to the runtime's
        /// definition of the root of all delegate types
        /// </summary>
        IGeneralTypeUniqueIdentifier Delegate { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> relative to the runtime's
        /// definition of the root of all enumerations.
        /// </summary>
        IGeneralTypeUniqueIdentifier EnumBaseType { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> relative to the runtime's
        /// definition of a signed 16-bit integer.
        /// </summary>
        IGeneralTypeUniqueIdentifier Int16 { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> relative to the runtime's
        /// definition of an unsigned 16-bit integer.
        /// </summary>
        IGeneralTypeUniqueIdentifier UInt16 { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> relative to the runtime's
        /// definition of a signed 32-bit integer.
        /// </summary>
        IGeneralTypeUniqueIdentifier Int32 { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> relative to the runtime's
        /// definition of an unsigned 32-bit integer.
        /// </summary>
        IGeneralTypeUniqueIdentifier UInt32 { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> relative to the runtime's
        /// definition of a signed 64-bit integer.
        /// </summary>
        IGeneralTypeUniqueIdentifier Int64 { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> relative to the runtime's
        /// definition of an unsigned 64-bit integer.
        /// </summary>
        IGeneralTypeUniqueIdentifier UInt64 { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> relative to the runtime's
        /// definition of the root of all multicast delegate types.
        /// </summary>
        IGeneralTypeUniqueIdentifier MulticastDelegate { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> relative to the runtime's
        /// definition of the base type of a nullable type.
        /// </summary>
        IGeneralTypeUniqueIdentifier NullableBaseType { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> relative to the runtime's
        /// definition a nullable type.
        /// </summary>
        IGeneralTypeUniqueIdentifier NullableType { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> relative to the runtime's 
        /// definition of the root of all types.
        /// </summary>
        IGeneralTypeUniqueIdentifier RootType { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> relative to the runtime's
        /// definition of a series of unicode characters.
        /// </summary>
        IGeneralTypeUniqueIdentifier String { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> relative to the runtime's
        /// definition of the base of all value types.
        /// </summary>
        IGeneralTypeUniqueIdentifier ValueTypeBaseType { get; }
        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> relative to the runtime's
        /// definition of the void type.
        /// </summary>
        IGeneralTypeUniqueIdentifier VoidType { get; }
    }
}
