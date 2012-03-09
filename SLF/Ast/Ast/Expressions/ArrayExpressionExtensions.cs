using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public static class ArrayExpressionExtensions
    {

        public static Tuple<bool, int[], IExpression[]> BreakdownExpression(this ICreateArrayDetailExpression createArrayExpression, Predicate<IExpression> validator)
        {
            Queue<ICreateArrayNestedDetailExpression> currentLevelNestedDetails = new Queue<ICreateArrayNestedDetailExpression>();
            currentLevelNestedDetails.Enqueue(createArrayExpression);
            List<IExpression> elements = new List<IExpression>();
            List<int> dimensions = new List<int>();
            dimensions.Add(createArrayExpression.Details.Count);
        checkCurrentLevel:
            int dimensionElementCount = -1;
            Queue<ICreateArrayNestedDetailExpression> nextLevelNestedDetails = new Queue<ICreateArrayNestedDetailExpression>();
            while (currentLevelNestedDetails.Count > 0)
            {
                var current = currentLevelNestedDetails.Dequeue();
                int subCount = dimensionElementCount;
                foreach (var element in current.Details)
                {
                    if (element is ICreateArrayNestedDetailExpression)
                    {
                        var detailElement = (ICreateArrayNestedDetailExpression)element;
                        if (subCount > 0)
                        {
                            if (detailElement.Details.Count != subCount)
                                return new Tuple<bool, int[], IExpression[]>(false, null, null);
                        }
                        else
                        {
                            subCount = detailElement.Details.Count;
                            if (dimensionElementCount == -1)
                                dimensionElementCount = subCount;
                        }
                        nextLevelNestedDetails.Enqueue(detailElement);
                    }
                    else if (subCount != -1)
                        /* *
                         * Contains a mix of nested detail elements as well
                         * as normal elements.
                         * */
                        return new Tuple<bool, int[], IExpression[]>(false, null, null);
                    else
                    {
                        /* *
                         * Check to see if the element is an integral data type.
                         * */
                        if (!validator(element))
                            return new Tuple<bool, int[], IExpression[]>(false, null, null);
                        else
                            elements.Add(element);
                    }
                }
            }
            if (dimensionElementCount != -1)
                dimensions.Add(dimensionElementCount);
            if (nextLevelNestedDetails.Count > 0)
            {
                currentLevelNestedDetails = nextLevelNestedDetails;
                goto checkCurrentLevel;
            }
            return Tuple.Create(true, dimensions.ToArray(), elements.ToArray());
        }

        public static byte[] ConvertToByteArray(this ICreateArrayDetailExpression createArrayExpression)
        {
            var expressionDetails = createArrayExpression.BreakdownExpression(e => e is IPrimitiveExpression);
            if (expressionDetails.Item1)
            {
                PrimitiveType? primitiveType = null;
                bool inconsistent = false;
                foreach (IPrimitiveExpression expression in expressionDetails.Item3)
                    if (primitiveType == null)
                        primitiveType = expression.PrimitiveType;
                    else if (primitiveType.Value != expression.PrimitiveType)
                    {
                        if (primitiveType.Value == PrimitiveType.String &&
                            expression.PrimitiveType == PrimitiveType.Null)
                            continue;
                        else if (primitiveType.Value == PrimitiveType.Null &&
                            expression.PrimitiveType == PrimitiveType.String)
                            primitiveType = PrimitiveType.String;
                        else
                            inconsistent = true;
                    }
                /* *
                 * Either contained no primitives or contained nothing
                 * but null values.
                 * */
                if (primitiveType == null || primitiveType.Value == PrimitiveType.Null)
                {
                    if (createArrayExpression.ArrayType == CommonTypeRefs.String)
                    {
                        /* *
                         * ToDo: Add code here to serialize a series of null strings.
                         * */
                    }
                }
                else if (!inconsistent)
                {
                    switch (primitiveType.Value)
                    {
                        case PrimitiveType.Boolean:
                            return Convert(expressionDetails.Item3.Cast<IPrimitiveExpression<bool>>().ToArray());
                        case PrimitiveType.Byte:
                            return Convert(expressionDetails.Item3.Cast<IPrimitiveExpression<byte>>().ToArray());
                        case PrimitiveType.SByte:
                            return Convert(expressionDetails.Item3.Cast<IPrimitiveExpression<sbyte>>().ToArray());
                        case PrimitiveType.Int16:
                            return Convert(expressionDetails.Item3.Cast<IPrimitiveExpression<short>>().ToArray());
                        case PrimitiveType.UInt16:
                            return Convert(expressionDetails.Item3.Cast<IPrimitiveExpression<ushort>>().ToArray());
                        case PrimitiveType.Int32:
                            return Convert(expressionDetails.Item3.Cast<IPrimitiveExpression<int>>().ToArray());
                        case PrimitiveType.UInt32:
                            return Convert(expressionDetails.Item3.Cast<IPrimitiveExpression<uint>>().ToArray());
                        case PrimitiveType.Int64:
                            return Convert(expressionDetails.Item3.Cast<IPrimitiveExpression<long>>().ToArray());
                        case PrimitiveType.UInt64:
                            return Convert(expressionDetails.Item3.Cast<IPrimitiveExpression<ulong>>().ToArray());
                        case PrimitiveType.Decimal:
                            return Convert(expressionDetails.Item3.Cast<IPrimitiveExpression<decimal>>().ToArray());
                        case PrimitiveType.Float:
                            return Convert(expressionDetails.Item3.Cast<IPrimitiveExpression<float>>().ToArray());
                        case PrimitiveType.Double:
                            return Convert(expressionDetails.Item3.Cast<IPrimitiveExpression<double>>().ToArray());
                        case PrimitiveType.Char:
                            return Convert(expressionDetails.Item3.Cast<IPrimitiveExpression<char>>().ToArray());
                        case PrimitiveType.String:
                            /* *
                             * ToDo: Add code here to serialize a set of strings.
                             * */
                            break;
                    }
                }
            }
            return null;
        }

        private static byte[] Convert(IPrimitiveExpression<bool>[] set)
        {

            var result = new byte[(set.Length + 7) / 8];
            for (int i = 0; i < set.Length; i++)
                if (set[i].Value)
                    result[i / 8] |= (byte)(1 << i % 8);
            return result;
        }
        private static byte[] Convert(IPrimitiveExpression<char>[] set)
        {
            var result = new byte[set.Length * sizeof(char)];
            for (int i = 0, offI = 0; i < set.Length; i++, offI += sizeof(char))
            {
                ushort current = (ushort)set[i].Value;
                result[offI] = (byte)(current & 0xFF);
                result[offI + 1] = (byte)((current & 0xFF00) >> 8);
            }
            return result;
        }

        private static byte[] Convert(IPrimitiveExpression<byte>[] set)
        {
            var result = new byte[set.Length];
            for (int i = 0; i < set.Length; i++)
                result[i] = set[i].Value;
            return null;
        }
        private static byte[] Convert(IPrimitiveExpression<sbyte>[] set)
        {
            var result = new byte[set.Length * sizeof(sbyte)];
            for (int i = 0; i < set.Length; i++)
                result[i] = (byte)set[i].Value;
            return result;
        }
        private static byte[] Convert(IPrimitiveExpression<ushort>[] set)
        {
            var result = new byte[set.Length * sizeof(ushort)];
            for (int i = 0, offI = 0; i < set.Length; i++, offI += sizeof(ushort))
            {
                ushort current = set[i].Value;
                result[offI] = (byte)(current & 0xFF);
                result[offI + 1] = (byte)((current & 0xFF00) >> 8);
            }
            return result;
        }
        private static byte[] Convert(IPrimitiveExpression<short>[] set)
        {
            var result = new byte[set.Length * sizeof(short)];
            for (int i = 0, offI = 0; i < set.Length; i++, offI += sizeof(short))
            {
                short current = set[i].Value;
                result[offI] = (byte)(current & 0xFF);
                result[offI + 1] = (byte)((current & 0xFF00) >> 8);
            }
            return result;
        }
        private static byte[] Convert(IPrimitiveExpression<uint>[] set)
        {
            var result = new byte[set.Length * sizeof(uint)];
            for (int i = 0, offI = 0; i < set.Length; i++, offI += sizeof(uint))
            {
                uint current = set[i].Value;
                result[offI] = (byte)(current & 0xFF);
                result[offI + 1] = (byte)((current & 0xFF00) >> 8);
                result[offI + 2] = (byte)((current & 0xFF0000) >> 16);
                result[offI + 3] = (byte)((current & 0xFF000000) >> 24);
            }
            return result;
        }
        private static byte[] Convert(IPrimitiveExpression<int>[] set)
        {
            var result = new byte[set.Length * sizeof(int)];
            for (int i = 0, offI = 0; i < set.Length; i++, offI += sizeof(int))
            {
                int current = set[i].Value;
                result[offI] = (byte)(current & 0xFF);
                result[offI + 1] = (byte)((current & 0xFF00) >> 8);
                result[offI + 2] = (byte)((current & 0xFF0000) >> 16);
                result[offI + 3] = (byte)((current & 0xFF000000) >> 24);
            }
            return result;
        }
        private static byte[] Convert(IPrimitiveExpression<ulong>[] set)
        {
            var result = new byte[set.Length * sizeof(ulong)];
            for (int i = 0, offI = 0; i < set.Length; i++, offI += sizeof(ulong))
            {
                ulong current = set[i].Value;
                result[offI] =  (byte)(current & 0xFFUL);
                result[offI + 1] = (byte)((current & 0xFF00UL) >> 8);
                result[offI + 2] = (byte)((current & 0xFF0000UL) >> 16);
                result[offI + 3] = (byte)((current & 0xFF000000UL) >> 24);
                result[offI + 4] = (byte)((current & 0xFF00000000UL) >> 32);
                result[offI + 5] = (byte)((current & 0xFF0000000000UL) >> 40);
                result[offI + 6] = (byte)((current & 0xFF000000000000UL) >> 48);
                result[offI + 7] = (byte)((current & 0xFF00000000000000UL) >> 56);
            }
            return result;
        }
        private static byte[] Convert(IPrimitiveExpression<long>[] set)
        {
            var result = new byte[set.Length * sizeof(long)];
            for (int i = 0, offI = 0; i < set.Length; i++, offI += sizeof(long))
            {
                long current = set[i].Value;
                result[offI] =  (byte)(current & 0xFFL);
                result[offI + 1] = (byte)((current & 0xFF00L) >> 8);
                result[offI + 2] = (byte)((current & 0xFF0000L) >> 16);
                result[offI + 3] = (byte)((current & 0xFF000000L) >> 24);
                result[offI + 4] = (byte)((current & 0xFF00000000L) >> 32);
                result[offI + 5] = (byte)((current & 0xFF0000000000L) >> 40);
                result[offI + 6] = (byte)((current & 0xFF000000000000L) >> 48);
                result[offI + 7] = (byte)((((ulong)current) & 0xFF00000000000000UL) >> 56);
            }
            return result;
        }
        private static byte[] Convert(IPrimitiveExpression<float>[] set)
        {
            var result = new byte[set.Length * sizeof(float)];
            for (int i = 0, offI = 0; i < set.Length; i++, offI += sizeof(float))
            {
                uint current = (uint)BitConverter.DoubleToInt64Bits(set[i].Value);
                result[offI] = (byte)(current & 0xFF);
                result[offI + 1] = (byte)((current & 0xFF00) >> 8);
                result[offI + 2] = (byte)((current & 0xFF0000) >> 16);
                result[offI + 3] = (byte)((current & 0xFF000000) >> 24);
            }
            return result;
        }
        private static byte[] Convert(IPrimitiveExpression<double>[] set)
        {
            var result = new byte[set.Length * sizeof(double)];
            for (int i = 0, offI = 0; i < set.Length; i++, offI += sizeof(double))
            {
                ulong current = (ulong)BitConverter.DoubleToInt64Bits(set[i].Value);
                result[offI + 1] = (byte)((current & 0xFF00UL) >> 8);
                result[offI + 2] = (byte)((current & 0xFF0000UL) >> 16);
                result[offI + 3] = (byte)((current & 0xFF000000UL) >> 24);
                result[offI + 4] = (byte)((current & 0xFF00000000UL) >> 32);
                result[offI + 5] = (byte)((current & 0xFF0000000000UL) >> 40);
                result[offI + 6] = (byte)((current & 0xFF000000000000UL) >> 48);
                result[offI + 7] = (byte)((current & 0xFF00000000000000UL) >> 56);
                result[offI] = (byte)(current & 0xFFUL);
            }
            return result;
        }
        private static byte[] Convert(IPrimitiveExpression<decimal>[] set)
        {
            var result = new byte[set.Length * sizeof(decimal)];
            for (int i = 0, offI = 0; i < set.Length; i++, offI += sizeof(decimal))
            {
                int[] bits = Decimal.GetBits(set[i].Value);
                ulong first = (((ulong)bits[3]) << 32) | (uint)bits[2];
                ulong second = (((ulong)bits[1]) << 32) | (uint)bits[0];
                result[offI] = (byte)(second & 0xFFUL);
                result[offI + 01] = (byte)((first & 0xFF00UL) >> 8);
                result[offI + 02] = (byte)((first & 0xFF0000UL) >> 16);
                result[offI + 03] = (byte)((first & 0xFF000000UL) >> 24);
                result[offI + 04] = (byte)((first & 0xFF00000000UL) >> 32);
                result[offI + 05] = (byte)((first & 0xFF0000000000UL) >> 40);
                result[offI + 06] = (byte)((first & 0xFF000000000000UL) >> 48);
                result[offI + 07] = (byte)((first & 0xFF00000000000000UL) >> 56);
                result[offI + 08] = (byte)(second & 0xFFUL);
                result[offI + 09] = (byte)((second & 0xFF00UL) >> 8);
                result[offI + 10] = (byte)((second & 0xFF0000UL) >> 16);
                result[offI + 11] = (byte)((second & 0xFF000000UL) >> 24);
                result[offI + 12] = (byte)((second & 0xFF00000000UL) >> 32);
                result[offI + 13] = (byte)((second & 0xFF0000000000UL) >> 40);
                result[offI + 14] = (byte)((second & 0xFF000000000000UL) >> 48);
                result[offI + 15] = (byte)((second & 0xFF00000000000000UL) >> 56);
            }
            return result;
        }
        #region Array ToExpression
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The zero-indexed target <see cref="Int32"/> vector array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this int[] target)
        {
            return target.ToExpression<int>(ExpressionExtensions.ToPrimitive);
        }

        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="Int32"/> two-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this int[,] target)
        {
            return target.ToExpression<int>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="Int32"/> three-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this int[, ,] target)
        {
            return target.ToExpression<int>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="Int32"/> four-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this int[, , ,] target)
        {
            return target.ToExpression<int>(ExpressionExtensions.ToPrimitive);
        }

        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The zero-indexed target <see cref="Byte"/> vector array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this byte[] target)
        {
            return target.ToExpression<byte>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="Byte"/> two-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this byte[,] target)
        {
            return target.ToExpression<byte>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="Byte"/> three-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this byte[, ,] target)
        {
            return target.ToExpression<byte>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="Byte"/> four-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this byte[, , ,] target)
        {
            return target.ToExpression<byte>(ExpressionExtensions.ToPrimitive);
        }

        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The zero-indexed target <see cref="UInt32"/> vector array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this uint[] target)
        {
            return target.ToExpression<uint>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="UInt32"/> two-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this uint[,] target)
        {
            return target.ToExpression<uint>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="UInt32"/> three-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this uint[, ,] target)
        {
            return target.ToExpression<uint>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="UInt32"/> four-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this uint[, , ,] target)
        {
            return target.ToExpression<uint>(ExpressionExtensions.ToPrimitive);
        }

        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The zero-indexed target <see cref="SByte"/> vector array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this sbyte[] target)
        {
            return target.ToExpression<sbyte>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="SByte"/> two-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this sbyte[,] target)
        {
            return target.ToExpression<sbyte>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="SByte"/> three-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this sbyte[, ,] target)
        {
            return target.ToExpression<sbyte>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="SByte"/> four-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this sbyte[, , ,] target)
        {
            return target.ToExpression<sbyte>(ExpressionExtensions.ToPrimitive);
        }

        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The zero-indexed target <see cref="Int16"/> vector array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this short[] target)
        {
            return target.ToExpression<short>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="Int16"/> two-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this short[,] target)
        {
            return target.ToExpression<short>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="Int16"/> three-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this short[, ,] target)
        {
            return target.ToExpression<short>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="Int16"/> four-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this short[, , ,] target)
        {
            return target.ToExpression<short>(ExpressionExtensions.ToPrimitive);
        }

        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The zero-indexed target <see cref="UInt16"/> vector array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this ushort[] target)
        {
            return target.ToExpression<ushort>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="UInt16"/> two-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this ushort[,] target)
        {
            return target.ToExpression<ushort>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="UInt16"/> three-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this ushort[, ,] target)
        {
            return target.ToExpression<ushort>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="UInt16"/> four-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this ushort[, , ,] target)
        {
            return target.ToExpression<ushort>(ExpressionExtensions.ToPrimitive);
        }

        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The zero-indexed target <see cref="Int64"/> vector array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this long[] target)
        {
            return target.ToExpression<long>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="Int64"/> two-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this long[,] target)
        {
            return target.ToExpression<long>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="Int64"/> three-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this long[, ,] target)
        {
            return target.ToExpression<long>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="Int64"/> four-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this long[, , ,] target)
        {
            return target.ToExpression<long>(ExpressionExtensions.ToPrimitive);
        }

        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The zero-indexed target <see cref="UInt64"/> vector array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this ulong[] target)
        {
            return target.ToExpression<ulong>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="UInt64"/> two-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this ulong[,] target)
        {
            return target.ToExpression<ulong>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="UInt64"/> three-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this ulong[, ,] target)
        {
            return target.ToExpression<ulong>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="UInt64"/> four-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this ulong[, , ,] target)
        {
            return target.ToExpression<ulong>(ExpressionExtensions.ToPrimitive);
        }

        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The zero-indexed target <see cref="Single"/> vector array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this float[] target)
        {
            return target.ToExpression<float>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="Single"/> two-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this float[,] target)
        {
            return target.ToExpression<float>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="Single"/> three-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this float[, ,] target)
        {
            return target.ToExpression<float>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="Single"/> four-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this float[, , ,] target)
        {
            return target.ToExpression<float>(ExpressionExtensions.ToPrimitive);
        }

        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The zero-indexed target <see cref="Double"/> vector array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this double[] target)
        {
            return target.ToExpression<double>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="Double"/> two-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this double[,] target)
        {
            return target.ToExpression<double>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="Double"/> three-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this double[, ,] target)
        {
            return target.ToExpression<double>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="Double"/> four-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this double[, , ,] target)
        {
            return target.ToExpression<double>(ExpressionExtensions.ToPrimitive);
        }

        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The zero-indexed target <see cref="Decimal"/> vector array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this decimal[] target)
        {
            return target.ToExpression<decimal>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="Decimal"/> two-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this decimal[,] target)
        {
            return target.ToExpression<decimal>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="Decimal"/> three-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this decimal[, ,] target)
        {
            return target.ToExpression<decimal>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="Decimal"/> four-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this decimal[, , ,] target)
        {
            return target.ToExpression<decimal>(ExpressionExtensions.ToPrimitive);
        }

        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The zero-indexed target <see cref="String"/> vector array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this string[] target)
        {
            return target.ToExpression<string>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="String"/> two-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this string[,] target)
        {
            return target.ToExpression<string>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="String"/> three-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this string[, ,] target)
        {
            return target.ToExpression<string>(ExpressionExtensions.ToPrimitive);
        }
        /// <summary>
        /// Converts the <paramref name="target"/> array into a <see cref="ICreateArrayDetailExpression"/>.
        /// </summary>
        /// <param name="target">The target <see cref="String"/> four-dimensional array to convert.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents
        /// the <paramref name="target"/> array.</returns>
        public static ICreateArrayDetailExpression ToExpression(this string[, , ,] target)
        {
            return target.ToExpression<string>(ExpressionExtensions.ToPrimitive);
        }
        #endregion

        /// <summary>
        /// Converts the <paramref name="target"/> <see cref="Array"/> into a <see cref="ICreateArrayDetailExpression"/>
        /// with the <paramref name="expressionConverter"/> provided.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the <paramref name="target"/> <see cref="Array"/>.</typeparam>
        /// <param name="target">The <see cref="Array"/> to convert.</param>
        /// <param name="expressionConverter">The <see cref="Func{T, TResult}"/> which converts the elements of 
        /// <typeparamref name="T"/> into <see cref="IExpression"/> instances.</param>
        /// <returns>A new <see cref="ICreateArrayDetailExpression"/> that represents the
        /// <paramref name="target"/> <see cref="Array"/>.</returns>
        public static ICreateArrayDetailExpression ToExpression<T>(this Array target, Func<T, IExpression> expressionConverter)
        {
            /* *
             * Instantiate a create array expression with item
             * details.
             * */
            int arrayRank = target.Rank;
            var result = new MalleableCreateArrayDetailExpression(typeof(T).GetTypeReference(), arrayRank);
            /* *
             * Since the sizes are available, provide them
             * to the array detail.
             * */
            result.Sizes.AddRange(from arrayDimension in arrayRank.Range()
                                  select target.GetLength(arrayDimension).ToPrimitive());
            /* *
             * Setup a series of nested detail expressions which will
             * be used to provide information about the dimensions
             * of the array.
             * *
             * Since they're cleaned out after spill-over, a single
             * dimension will do, the first item is the result.
             * */
            IMalleableCreateArrayNestedDetailExpression[] nestedDetails = new IMalleableCreateArrayNestedDetailExpression[arrayRank];
            nestedDetails[0] = result;
            for (int i = 1; i < arrayRank; i++)
                nestedDetails[i] = new MalleableCreateArrayNestedDetailExpression();
            int topRankIndex = arrayRank - 1;
            var topLevel = nestedDetails[topRankIndex];
            /* *
             * The bounds increment lambda handles dimension spillover;
             * that is, when a rank's highest element is reached,
             * it's set back to one, when this occurs, the current definition
             * of the dimension is complete, and the next needs started.
             * */
            Action<int, bool> boundsLambda = (rank, isIterationComplete) =>
            {
                if (rank > 0)
                {
                    nestedDetails[rank - 1].Details.Add(nestedDetails[rank]);
                    if (!isIterationComplete)
                        nestedDetails[rank] = new MalleableCreateArrayNestedDetailExpression();
                    else
                        nestedDetails[rank] = null;
                    if (rank == topRankIndex)
                        topLevel = nestedDetails[topRankIndex];
                }
            };

            /* *
             * This part is easy, on the top level nested details
             * add the item at the current indices set. 
             * *
             * The bounds lambda handles divisioning.
             * */
            foreach (var indices in target.Iterate(onBoundsIncrement: boundsLambda))
                topLevel.Details.Add(expressionConverter((T)target.GetValue(indices)));
            return result;
        }
    }
}
