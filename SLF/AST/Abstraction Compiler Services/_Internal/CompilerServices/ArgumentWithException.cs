using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf._Internal.CompilerServices
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
        /// Parameter 'ctor'.
        /// </summary>
        ctor,
        /// <summary>
        /// Parameter 'message'.
        /// </summary>
        message,
        /// <summary>
        /// Parameter 'parameter'.
        /// </summary>
        parameter,
    }
}
