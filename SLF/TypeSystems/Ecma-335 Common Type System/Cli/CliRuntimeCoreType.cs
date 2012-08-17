using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    public enum CliRuntimeCoreType
    {
        /// <summary>
        /// The definition of the root of all asynchronous tasks.
        /// </summary>
        AsynchronousTask,
        /// <summary>
        /// The definition of the root of all generic asynchronous tasks.
        /// </summary>
        AsynchronousTaskOfT,
        /// <summary>
        /// The definition of the root of the compiler generated metadatum.
        /// </summary>
        CompilerGeneratedMetadatum,
        /// <summary>
        /// The definition of the root of all delegate types.
        /// </summary>
        Delegate,
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
        /// The definition of the parameter array metadatum to be applied
        /// to the final parameter of a method which signifies that
        /// the method accepts multiple arguments as one in place of 
        /// the array.
        /// </summary>
        ParamArrayMetadatum,
        /// <summary>
        /// The definition of the root metadatum type.
        /// </summary>
        RootMetadatum,
    }
}
