using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    public interface IStandardRuntimeEnvironmentInfo
    {
        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> of the <paramref name="coreType"/>
        /// provided.
        /// </summary>
        /// <param name="coreType">The <see cref="RuntimeCoreType"/>
        /// to obtain the <see cref="IGeneralTypeUniqueIdentifier"/> of.</param>
        /// <returns>A <see cref="IGeneralTypeUniqueIdentifier"/> relative to the 
        /// <paramref name="coreType"/> provided.</returns>
        IGeneralTypeUniqueIdentifier GetCoreIdentifier(RuntimeCoreType coreType);
        /// <summary>
        /// Returns the series of <see cref="IType"/>s implemented by the
        /// <paramref name="coreType"/>
        /// </summary>
        /// <param name="coreType">The <see cref="RuntimeCoreType"/>
        /// to request the implemented interfaces of.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> of the interfaces
        /// implemented by <paramref name="coreType"/>.</returns>
        IEnumerable<IType> GetCoreTypeInterfaces(RuntimeCoreType coreType);
        /// <summary>
        /// Returns the definition of the root of all array types.
        /// </summary>
        IGeneralTypeUniqueIdentifier ArrayType { get; }
        /// <summary>
        /// Returns the definition of a boolean value.
        /// </summary>
        IGeneralTypeUniqueIdentifier Boolean { get; }
        /// <summary>
        /// Returns the definition of a decimal value.
        /// </summary>
        IGeneralTypeUniqueIdentifier Decimal { get; }
        /// <summary>
        /// Returns the definition of a single-precision floating-point value.
        /// </summary>
        IGeneralTypeUniqueIdentifier Single { get; }
        /// <summary>
        /// Returns the definition of a double-precision floating-point value.
        /// </summary>
        IGeneralTypeUniqueIdentifier Double { get; }
        /// <summary>
        /// Returns the definition of a signed byte.
        /// </summary>
        IGeneralTypeUniqueIdentifier SByte { get; }
        /// <summary>
        /// Returns the definition of an unsigned byte.
        /// </summary>
        IGeneralTypeUniqueIdentifier Byte { get; }
        /// <summary>
        /// Returns the definition of a unicode character.
        /// </summary>
        IGeneralTypeUniqueIdentifier Char { get; }
        /// <summary>
        /// Returns the definition of the root of all enumerations.
        /// </summary>
        IGeneralTypeUniqueIdentifier RootEnum { get; }
        /// <summary>
        /// Returns the definition of a signed 16-bit integer.
        /// </summary>
        IGeneralTypeUniqueIdentifier Int16 { get; }
        /// <summary>
        /// Returns the definition of an unsigned 16-bit integer.
        /// </summary>
        IGeneralTypeUniqueIdentifier UInt16 { get; }
        /// <summary>
        /// Returns the definition of a signed 32-bit integer.
        /// </summary>
        IGeneralTypeUniqueIdentifier Int32 { get; }
        /// <summary>
        /// Returns the definition of an unsigned 32-bit integer.
        /// </summary>
        IGeneralTypeUniqueIdentifier UInt32 { get; }
        /// <summary>
        /// Returns the definition of a signed 64-bit integer.
        /// </summary>
        IGeneralTypeUniqueIdentifier Int64 { get; }
        /// <summary>
        /// Returns the definition of an unsigned 64-bit integer.
        /// </summary>
        IGeneralTypeUniqueIdentifier UInt64 { get; }
        /// <summary>
        /// Returns the  
        /// Returns the definition of the root of all types.
        /// </summary>
        IGeneralTypeUniqueIdentifier RootType { get; }
        /// <summary>
        /// Returns the definition of a series of unicode characters.
        /// </summary>
        IGeneralTypeUniqueIdentifier String { get; }
        /// <summary>
        /// Returns the definition of the base of all value types.
        /// </summary>
        IGeneralTypeUniqueIdentifier RootStruct { get; }
        /// <summary>
        /// Returns the definition of the void type.
        /// </summary>
        IGeneralTypeUniqueIdentifier VoidType { get; }
        /// <summary>
        /// Returns the definition of the Type type.
        /// </summary>
        IGeneralTypeUniqueIdentifier TypeType { get; }

    }
}
