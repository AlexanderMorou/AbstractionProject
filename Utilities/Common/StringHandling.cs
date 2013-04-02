using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction
{
    /// <summary>
    /// Provides a few basic string handling functions.
    /// </summary>
    public static class StringHandling
    {
        /// <summary>
        /// Indicates whether the <paramref name="value"/> provided
        /// is <see cref="String.Empty"/> or null.
        /// </summary>
        /// <param name="value">The <see cref="String"/> to check</param>
        /// <returns>true if <paramref name="value"/> is null
        /// or if it is <see cref="String.Empty"/>.</returns>
        internal static bool IsEmptyOrNull(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Joins a <paramref name="series"/> of <see cref="String"/> values with the 
        /// <paramref name="separator"/> provided, appending a <paramref name="sectionSeparator"/>
        /// when <paramref name="maxWidth"/> is reached for that section.
        /// </summary>
        /// <param name="series">A series of <see cref="String"/> values to be 
        /// joined together delimited by the <paramref name="separator"/>
        /// provided.</param>
        /// <param name="separator">The <see cref="String"/> value which
        /// should delimit the elements within <paramref name="series"/>.</param>
        /// <param name="maxWidth">The <see cref="Int32"/> value which denotes how 
        /// many characters should be allowed, at maximum, per section.</param>
        /// <returns>A <see cref="String"/> value which represents the elements of the 
        /// <paramref name="series"/> delimited by the <paramref name="separator"/>
        /// provided with a <paramref name="maxWidth"/> specified per section.</returns>
        /// <remarks>If the length of an element of the <paramref name="series"/>
        /// provided is greater than <paramref name="maxWidth"/>, the maximum width
        /// is adjusted.</remarks>
        /// <exception cref="System.ArgumentNullException">thrown when an element of <paramref name="series"/>
        /// is null; or <paramref name="series"/>, or <paramref name="separator"/> is null.</exception>
        public static string FixedJoin(this string[] series, string separator, string sectionSeparator, int maxWidth = 80)
        {
            if (series == null)
                throw new ArgumentNullException("series");
            if (separator == null)
                throw new ArgumentNullException("separator");
            int separatorLength = separator.Length;
            int maxElementLength = 0;
            for (int i = 0; i < series.Length; i++)
            {
                var element = series[i];
                if (element == null)
                    throw new ArgumentNullException("series");
                int elementLen = element.Length + separator.Length;
                if (elementLen > maxElementLength)
                    maxElementLength = elementLen;
            }

            int maxLengthAllowed = Math.Max(maxElementLength, maxWidth) + separatorLength;
            StringBuilder resultBuilder = new StringBuilder();
            bool firstElement = true;
            int currentLength = 0;
            int maxActual = maxLengthAllowed - separator.Length;
            foreach (var element in series)
            {
                if (firstElement)
                    firstElement = false;
                else
                {
                    resultBuilder.Append(separator);
                    currentLength += separatorLength;
                }
                int newLength = currentLength + element.Length;
                if (newLength >= maxActual)
                {
                    resultBuilder.Append(sectionSeparator);
                    currentLength = element.Length;
                }
                else
                    currentLength = newLength;
                resultBuilder.Append(element);
            }
            return resultBuilder.ToString();
        }

        /// <summary>
        /// Joins a <paramref name="series"/> of <see cref="String"/> values with the 
        /// <paramref name="separator"/> provided, appending a new line prior to the 
        /// <paramref name="maxWidth"/> being reached.
        /// </summary>
        /// <param name="series">A series of <see cref="String"/> values to be 
        /// joined together delimited by the <paramref name="separator"/>
        /// provided.</param>
        /// <param name="separator">The <see cref="String"/> value which
        /// should delimit the elements within <paramref name="series"/>.</param>
        /// <param name="maxWidth">The <see cref="Int32"/> value which denotes how 
        /// many characters should be allowed, at maximum, per line.</param>
        /// <returns>A <see cref="String"/> value which represents the elements of the 
        /// <paramref name="series"/> delimited by the <paramref name="separator"/>
        /// provided with a <paramref name="maxWidth"/> specified per line.</returns>
        /// <remarks>If the length of an element of the <paramref name="series"/>
        /// provided is greater than <paramref name="maxWidth"/>, the maximum width
        /// is adjusted.</remarks>
        /// <exception cref="System.ArgumentNullException">thrown when an element of <paramref name="series"/>
        /// is null; or <paramref name="series"/>, or <paramref name="separator"/> is null.</exception>
        public static string FixedJoin(this string[] series, string separator, int maxWidth = 80)
        {
            return series.FixedJoin(separator, Environment.NewLine, maxWidth);
        }

        /// <summary>
        /// Repeats the <paramref name="value"/> provided until <paramref name="count"/> is reached.
        /// </summary>
        /// <param name="value">The <see cref="System.Char"/> to repeat.</param>
        /// <param name="count">The number of times to repeat <paramref name="value"/>.</param>
        /// <returns>A <see cref="System.String"/> of the repeated <paramref name="value"/>.</returns>
        public static string Repeat(this char value, int count)
        {
            if (count <= 0)
                return string.Empty;
            else
                return new string(value, count);
        }

        public static string Repeat(this string s, int times)
        {
            char[] result = new char[s.Length * times];
            for (int j = 0, k = 0; j < times; k += s.Length, j++)
                s.CopyTo(0, result, k, s.Length);
            return new string(result);
        }
        /// <summary>
        /// Formats a <paramref name="number"/> in hexadecimal format with the 
        /// <paramref name="minPlaces"/> specified.
        /// </summary>
        /// <param name="number">The number to format as a hexadecimal value.</param>
        /// <param name="minPlaces">The minimum number of places the hexadecimal needs to have.</param>
        /// <returns>A <see cref="System.String"/> of a <paramref name="number"/> formatted into a 
        /// hexadecimal value.</returns>
        internal static string FormatHexadecimal(this int number, int minPlaces)
        {
            string r = string.Format(CultureInfo.CurrentCulture, "{0:x}", number);
            return string.Format(CultureInfo.CurrentCulture, "{0}{1}", '0'.Repeat(minPlaces - r.Length), r);
        }

        internal static string FormatHexadecimal(this byte[] array)
        {
            if (array == null)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (byte b in array)
                sb.Append(((int)b).FormatHexadecimal(2));
            return sb.ToString();
        }

        internal static byte[] ToByteArray(this string source)
        {
            byte[] b = new byte[source.Length * 2];
            const int lb = 0x00FF;
            const int hb = 0xFF00;
            const int byteBitSize = 8;
            for (int i = 0, i2 = 0; i < source.Length; i++, i2 += 2)
            {
                ushort cur = source[i];
                b[i2] = (byte)((cur & hb) >> byteBitSize);
                b[i2 + 1] = (byte)(cur & lb);
            }
            return b;
        }

        internal static string GetRelativeRoot(this IEnumerable<string> files)
        {
            var parts = (from f in files
                         let ePath = Path.GetDirectoryName(f)
                         orderby ePath.Length descending
                         select ePath).First().Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);

            string relativeRoot = null;
            for (int i = 0; i < parts.Length; i++)
            {
                string currentRoot = string.Join(@"\", parts, 0, parts.Length - i);
                if (files.All(p => p.Contains(currentRoot)))
                {
                    relativeRoot = currentRoot;
                    break;
                }
            }
            return relativeRoot;
        }
        internal static string GetExtensionFromRelativeRoot(string path, string relativeRoot, string uptrail = @".\")
        {
            string rPath = null;
            if (relativeRoot != null)
            {
                if (path == relativeRoot)
                    rPath = uptrail;
                else
                    rPath = uptrail + path.Substring(relativeRoot.Length + 1);
            }
            else
                rPath = path;
            return rPath;
        }
        /// <summary>
        /// Reverses the <paramref name="target"/> <see cref="String"/>.
        /// </summary>
        /// <param name="target">The <see cref="String"/> value to reverse.</param>
        /// <returns>A new <see cref="String"/> with the characters in the reverse
        /// order in which they started.</returns>
        public unsafe static string Reverse(this string target)
        {
            /* *
             * Wrote this for stackoverflow.
             * Figured I might as well put it here.
             * *
             * First unsafe method ftw.
             * */
            if (target == null || target.Length <= 1)
                return target;
            int len = target.Length;
            char* items = stackalloc char[len];
            fixed (char* originalPtr = target)
            {
                for (int i = 0, j = len; i <= --j; i++)
                {
                    items[i] = originalPtr[j];
                    items[j] = originalPtr[i];
                }
            }
            return new string(items, 0, len);
        }

        /// <summary>
        /// Breaks up a string into its individual prongs of varying <paramref name="lengths"/>.
        /// </summary>
        /// <param name="target">The target <see cref="String"/> to break up.</param>
        /// <param name="lengths">The <see cref="Int32"/> array  
        /// which determine the individual prong lengths.</param>
        /// <returns>An array of <see cref="String"/> values which represent
        /// the branches of the original <paramref name="target"/> <see cref="String"/>.</returns>
        public static string[] Polyfurcate(this string target, params int[] lengths)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (lengths == null)
                throw new ArgumentNullException("lengths");
            int len = 0;

            for (int i = 0; i < lengths.Length; len += lengths[i++])
                ;
            if (len != target.Length)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.target, ExceptionMessageId.SourceStringInvalid, ThrowHelper.GetArgumentName(ArgumentWithException.target));
            string[] result = new string[lengths.Length];
            for (int i = 0, position = 0; i < lengths.Length; )
            {
                int length;
                char[] current = new char[length = lengths[i]];
                for (int j = 0; j < length; position++)
                    current[j++] = target[position];
                result[i++] = new string(current);
            }
            return result;
        }

        private static IEnumerable<Match> AsEnumerable(this Match target)
        {
            while (target != null && target.Success)
            {
                yield return target;
                target = target.NextMatch();
            }
        }

        public static IEnumerable<Match> MatchSet(this Regex target, string text)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (text == null)
                throw new ArgumentNullException("text");
            return target.Match(text).AsEnumerable();
        }
    }
}
