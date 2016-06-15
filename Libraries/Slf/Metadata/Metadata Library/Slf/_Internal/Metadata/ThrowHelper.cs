using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Metadata.Properties;

namespace AllenCopeland.Abstraction.Slf._Internal.Metadata
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

        public static string GetExceptionMessage(ExceptionMessageId message, params string[] replacements)
        {
            switch (message)
            {
                case  ExceptionMessageId.GenericParameterFailureException:
                    return string.Format(Resources.AE_GenericParameterFailureException, replacements);
                case ExceptionMessageId.TypeParameterInfoError:
                    return string.Format(Resources.AE_TypeParameterInfoError, replacements);
                case ExceptionMessageId.TypeMustBeCompilerGenerated:
                    return string.Format(Resources.AE_TypeMustBeCompilerGenerated, replacements);
                case ExceptionMessageId.TypeMustBeGenericChild:
                    return string.Format(Resources.AE_TypeMustBeGenericChild, replacements);
                case ExceptionMessageId.TypeMustBeStaticClass:
                    return string.Format(Resources.AE_TypeMustBeStaticClass, replacements);
                case ExceptionMessageId.TypeNotGeneric:
                    return string.Format(Resources.AE_TypeNotGeneric, replacements);
                case ExceptionMessageId.UnknownArgument:
                    return string.Format(Resources.AE_UnknownArgument, replacements);
                case ExceptionMessageId.UnknownArgumentMessage:
                    return string.Format(Resources.AE_UnknownArgumentMessage, replacements);
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
                case ArgumentWithException.genericLocalType:
                    return "structuralTypeInfo";
                case ArgumentWithException.globalMemberType:
                    return "globalMemberType";
                case ArgumentWithException.message:
                    return "message";
                default:
                    throw ObtainArgumentException(ArgumentWithException.argument, ExceptionMessageId.UnknownArgument);
            }
        }
    }
}
