using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf._Internal.Metadata
{
    /// <summary>
    /// The argument which caused the <see cref="ArgumentException"/>
    /// or <see cref="ArgumentOutOfRangeException"/>.
    /// </summary>
    internal enum ArgumentWithException
    {
        /// <summary>
        /// Parameter 'argument'.
        /// </summary>
        argument,
        /// <summary>
        /// Parameter 'globalMemberType'.
        /// </summary>
        globalMemberType,
        /// <summary>
        /// Parameter 'structuralTypeInfo'.
        /// </summary>
        genericLocalType,
        /// <summary>
        /// Parameter 'message'.
        /// </summary>
        message,
    }
}
