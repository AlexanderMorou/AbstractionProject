using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf._Internal.CompilerServices
{
    /// <summary>
    /// The <see cref="ArgumentException"/> message.
    /// </summary>
    internal enum ExceptionMessageId
    {
        /// <summary>
        /// The provided constructor contains a different number of
        /// parameters than the Delegate type.
        /// </summary>
        DelegateTypeParameterMismatch,
        /// <summary>
        /// Provided type must be a generic parameter.
        /// </summary>
        TypeMustBeGenericParameter,
        /// <summary>
        /// Unknown argument identifier provided.
        /// </summary>
        UnknownArgument,
        /// <summary>
        /// Unknown argument message identifier provided.
        /// </summary>
        UnknownArgumentMessage,
    }
}
