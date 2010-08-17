using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    partial class TypeExtensions
    {
        internal static Dictionary<TypeCode, Dictionary<TypeCode, bool>> conversionInfo = GetConversionInfo();


        /// <summary>
        /// Checks to see if you can go <paramref name="from"/> one type <paramref name="to"/> another.
        /// </summary>
        /// <param name="from">The type to check conversion of.</param>
        /// <param name="to">The type to see if <paramref name="from"/> can go to.</param>
        /// <returns>True if <paramref name="from"/> can be cast/converted <paramref name="to"/>; otherwise false.</returns>
        public static bool CanConvertFrom(this Type from, Type to)
        {
            TypeCode fromTC = Type.GetTypeCode(from);
            TypeCode toTC = Type.GetTypeCode(to);
            try
            {
                if (fromTC != toTC)
                    return conversionInfo[fromTC][toTC];
                else if (fromTC == TypeCode.Object)
                    //ToDo: Add code here to use the Expression coersion members to find an
                    //implicit coercion from 'from' to 'to'.
                    return (to.IsAssignableFrom(from));
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }
        internal static bool IsSigned(this TypeCode tc)
        {
            switch (tc)
            {
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        private static Dictionary<TypeCode, Dictionary<TypeCode, bool>> GetConversionInfo()
        {
            Dictionary<TypeCode, Dictionary<TypeCode, bool>> conversionInfo = new Dictionary<TypeCode, Dictionary<TypeCode, bool>>();
            TypeCode[] supportedTypeCodes = new TypeCode[] { TypeCode.Byte, TypeCode.SByte, TypeCode.Single, TypeCode.Double, TypeCode.Char, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64 };
            foreach (TypeCode tc in supportedTypeCodes)
            {
                Dictionary<TypeCode, bool> current = new Dictionary<TypeCode, bool>();
                switch (tc)
                {
                    case TypeCode.Char:
                        current[TypeCode.UInt16] = true;
                        current[TypeCode.UInt32] = true;
                        current[TypeCode.UInt64] = true;
                        current[TypeCode.Int32] = true;
                        current[TypeCode.Int64] = true;
                        current[TypeCode.Single] = true;
                        current[TypeCode.Double] = true;
                        break;
                    case TypeCode.Byte:
                        current[TypeCode.Char] = true;
                        current[TypeCode.UInt16] = true;
                        current[TypeCode.UInt32] = true;
                        current[TypeCode.UInt64] = true;
                        goto case TypeCode.SByte;
                    case TypeCode.SByte:
                        current[TypeCode.Int16] = true;
                        current[TypeCode.Int32] = true;
                        current[TypeCode.Int64] = true;
                        current[TypeCode.Single] = true;
                        current[TypeCode.Double] = true;
                        break;
                    case TypeCode.UInt16:
                        current[TypeCode.UInt32] = true;
                        current[TypeCode.UInt64] = true;
                        goto case TypeCode.Int16;
                    case TypeCode.Int16:
                        current[TypeCode.Int32] = true;
                        current[TypeCode.Int64] = true;
                        current[TypeCode.Single] = true;
                        current[TypeCode.Double] = true;
                        break;
                    case TypeCode.UInt32:
                        current[TypeCode.UInt64] = true;
                        goto case TypeCode.Int32;
                    case TypeCode.Int32:
                        current[TypeCode.Int64] = true;
                        current[TypeCode.Single] = true;
                        current[TypeCode.Double] = true;
                        break;
                    case TypeCode.UInt64:
                    case TypeCode.Int64:
                        current[TypeCode.Single] = true;
                        current[TypeCode.Double] = true;
                        break;
                    case TypeCode.Single:
                        current[TypeCode.Double] = true;
                        break;
                }
                conversionInfo[tc] = current;
            }
            return conversionInfo;
        }

    }
}
