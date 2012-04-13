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
        /// Returns the <see cref="ITypeUniqueIdentifier"/> relative to the runtime's
        /// definition of the root of all array types.
        /// </summary>
        ITypeUniqueIdentifier ArrayType { get; }
        /// <summary>
        /// Returns the <see cref="ITypeUniqueIdentifier"/> relative to the runtime's
        /// definition of the root of all asynchronous tasks.
        /// </summary>
        ITypeUniqueIdentifier AsynchronousTask { get; }
        /// <summary>
        /// Returns the <see cref="ITypeUniqueIdentifier"/> relative to the runtime's
        /// definition of the root of all generic asynchronous tasks.
        /// </summary>
        ITypeUniqueIdentifier AsynchronousTaskOfT { get; }
        /// <summary>
        /// Returns the <see cref="ITypeUniqueIdentifier"/> relative to the runtime's
        /// definition of a signed byte.
        /// </summary>
        ITypeUniqueIdentifier SByte { get; }
        /// <summary>
        /// Returns the <see cref="ITypeUniqueIdentifier"/> relative to the runtime's
        /// definition of an unsigned byte.
        /// </summary>
        ITypeUniqueIdentifier Byte { get; }
        /// <summary>
        /// Returns the <see cref="ITypeUniqueIdentifier"/> relative to the runtime's
        /// definition of the root of the compiler generated metadatum.
        /// </summary>
        ITypeUniqueIdentifier CompilerGeneratedMetadatum { get; }
        /// <summary>
        /// Returns the <see cref="ITypeUniqueIdentifier"/> relative to the runtime's
        /// definition of the root of all delegate types
        /// </summary>
        ITypeUniqueIdentifier Delegate { get; }
        /// <summary>
        /// Returns the <see cref="ITypeUniqueIdentifier"/> relative to the runtime's
        /// definition of the root of all enumerations.
        /// </summary>
        ITypeUniqueIdentifier EnumBaseType { get; }
        /// <summary>
        /// Returns the <see cref="ITypeUniqueIdentifier"/> relative to the runtime's
        /// definition of a signed 16-bit integer.
        /// </summary>
        ITypeUniqueIdentifier Int16 { get; }
        /// <summary>
        /// Returns the <see cref="ITypeUniqueIdentifier"/> relative to the runtime's
        /// definition of an unsigned 16-bit integer.
        /// </summary>
        ITypeUniqueIdentifier UInt16 { get; }
        /// <summary>
        /// Returns the <see cref="ITypeUniqueIdentifier"/> relative to the runtime's
        /// definition of a signed 32-bit integer.
        /// </summary>
        ITypeUniqueIdentifier Int32 { get; }
        /// <summary>
        /// Returns the <see cref="ITypeUniqueIdentifier"/> relative to the runtime's
        /// definition of an unsigned 32-bit integer.
        /// </summary>
        ITypeUniqueIdentifier UInt32 { get; }
        /// <summary>
        /// Returns the <see cref="ITypeUniqueIdentifier"/> relative to the runtime's
        /// definition of a signed 64-bit integer.
        /// </summary>
        ITypeUniqueIdentifier Int64 { get; }
        /// <summary>
        /// Returns the <see cref="ITypeUniqueIdentifier"/> relative to the runtime's
        /// definition of an unsigned 64-bit integer.
        /// </summary>
        ITypeUniqueIdentifier UInt64 { get; }
        /// <summary>
        /// Returns the <see cref="ITypeUniqueIdentifier"/> relative to the runtime's
        /// definition of the root of all multicast delegate types.
        /// </summary>
        ITypeUniqueIdentifier MulticastDelegate { get; }
        /// <summary>
        /// Returns the <see cref="ITypeUniqueIdentifier"/> relative to the runtime's
        /// definition of the base type of a nullable type.
        /// </summary>
        ITypeUniqueIdentifier NullableBaseType { get; }
        /// <summary>
        /// Returns the <see cref="ITypeUniqueIdentifier"/> relative to the runtime's
        /// definition a nullable type.
        /// </summary>
        ITypeUniqueIdentifier NullableType { get; }
        /// <summary>
        /// Returns the <see cref="ITypeUniqueIdentifier"/> relative to the runtime's 
        /// definition of the root of all types.
        /// </summary>
        ITypeUniqueIdentifier RootType { get; }
        /// <summary>
        /// Returns the <see cref="ITypeUniqueIdentifier"/> relative to the runtime's
        /// definition of the base of all value types.
        /// </summary>
        ITypeUniqueIdentifier ValueTypeBaseType { get; }
        /// <summary>
        /// Returns the <see cref="ITypeUniqueIdentifier"/> relative to the runtime's
        /// definition of the void type.
        /// </summary>
        ITypeUniqueIdentifier VoidType { get; }
    }
}
