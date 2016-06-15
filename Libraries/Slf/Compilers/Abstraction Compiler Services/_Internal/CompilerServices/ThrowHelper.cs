using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.CompilerServices.Properties;
using System.Globalization;

namespace AllenCopeland.Abstraction.Slf._Internal.CompilerServices
{
    internal static class ThrowHelper
    {
        private static readonly string[] emptyReplacements = new string[0];

        public static NotSupportedException ObtainNotSupportedException(ExceptionMessageId message, string replacement)
        {
            return new NotSupportedException(GetExceptionMessage(message, new[] { replacement }));
        }

        public static NotSupportedException ObtainNotSupportedException(ExceptionMessageId message, string replacement1, string replacement2)
        {
            return new NotSupportedException(GetExceptionMessage(message, new[] { replacement1, replacement2 }));
        }

        public static NotSupportedException ObtainNotSupportedException(ExceptionMessageId message, string replacement1, string replacement2, string replacement3)
        {
            return new NotSupportedException(GetExceptionMessage(message, new[] { replacement1, replacement2, replacement3 }));
        }

        public static NotSupportedException ObtainNotSupportedException(ExceptionMessageId message, params string[] replacements)
        {
            return new NotSupportedException(GetExceptionMessage(message, replacements ?? emptyReplacements));
        }

        public static ArgumentOutOfRangeException ObtainArgumentOutOfRangeException(ArgumentWithException argument)
        {
            return new ArgumentOutOfRangeException(GetArgumentName(argument));
        }

        public static ArgumentOutOfRangeException ObtainArgumentOutOfRangeException(ArgumentWithException argument, ExceptionMessageId message)
        {
            return ObtainArgumentOutOfRangeException(argument, message, emptyReplacements);
        }
        public static ArgumentOutOfRangeException ObtainArgumentOutOfRangeException(ArgumentWithException argument, ExceptionMessageId message, params string[] replacements)
        {
            return new ArgumentOutOfRangeException(GetArgumentName(argument), GetExceptionMessage(message, replacements ?? emptyReplacements));
        }

        public static ArgumentException ObtainArgumentException(ArgumentWithException argument, ExceptionMessageId message)
        {
            return ObtainArgumentException(argument, message, emptyReplacements);
        }
        public static ArgumentException ObtainArgumentException(ArgumentWithException argument, ExceptionMessageId message, string replacement)
        {
            return ObtainArgumentException(argument, message, new[] { replacement });
        }

        public static ArgumentException ObtainArgumentException(ArgumentWithException argument, ExceptionMessageId message, string replacement1, string replacement2)
        {
            return ObtainArgumentException(argument, message, new[] { replacement1, replacement2 });
        }

        public static ArgumentException ObtainArgumentException(ArgumentWithException argument, ExceptionMessageId message, string replacement1, string replacement2, string replacement3)
        {
            return ObtainArgumentException(argument, message, new[] { replacement1, replacement2, replacement3 });
        }

        public static ArgumentException ObtainArgumentException(ArgumentWithException argument, ExceptionMessageId message, params string[] replacements)
        {
            return new ArgumentException(GetExceptionMessage(message, replacements ?? emptyReplacements), GetArgumentName(argument));
        }

        private static string GetExceptionMessage(ExceptionMessageId message, params string[] replacements)
        {
            switch (message)
            {
                case ExceptionMessageId.DelegateTypeParameterMismatch:
                    return string.Format(CultureInfo.InvariantCulture, Resources.AE_DelegateTypeParameterMismatch, replacements);
                case ExceptionMessageId.TypeMustBeGenericParameter:
                    return string.Format(CultureInfo.InvariantCulture, Resources.AE_TypeMustBeGenericParameter, replacements);
                case ExceptionMessageId.UnknownArgument:
                    return string.Format(CultureInfo.InvariantCulture, Resources.AE_UnknownArgument, replacements);
                case ExceptionMessageId.UnknownArgumentMessage:
                    return string.Format(CultureInfo.InvariantCulture, Resources.AE_UnknownArgumentMessage, replacements);
                default:
                    throw ObtainArgumentException(ArgumentWithException.message, ExceptionMessageId.UnknownArgumentMessage);
            }
        }

        /// <summary>
        /// Obtains the <see cref="String"/> name associated to the 
        /// <paramref name="argument"/> provided.
        /// </summary>
        /// <param name="argument">The <see cref="ArgumentWithException"/> enumeration value
        /// which determines which name to retrieve.</param>
        /// <returns>A <see cref="String"/> associated to the 
        /// name of the <paramref name="argument"/> provided.</returns>
        internal static string GetArgumentName(ArgumentWithException argument)
        {
            switch (argument)
            {
                case ArgumentWithException.argument:
                    return "argument";
                case ArgumentWithException.ctor:
                    return "ctor";
                case ArgumentWithException.message:
                    return "message";
                case ArgumentWithException.parameter:
                    return "parameter";
                default:
                    throw ObtainArgumentException(ArgumentWithException.argument, ExceptionMessageId.UnknownArgument);
            }
        }
    }
}
