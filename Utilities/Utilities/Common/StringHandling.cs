﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Common
{
    internal static class StringHandling
    {
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
                int elementLen = element.Length;
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
                    resultBuilder.AppendLine();
                    currentLength = element.Length;
                }
                else
                    currentLength = newLength;
                resultBuilder.Append(element);
            }
            return resultBuilder.ToString();
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
            {
                char[] r = new char[count];
                for (int i = 0; i < r.Length; i++)
                    r[i] = value;
                return new string(r);
            }
        }

        public static string Repeat(this string s, int length)
        {
            char[] result = new char[s.Length * length];
            for (int j = 0, k = 0; j < length; j++)
                for (int i = 0; i < s.Length; i++)
                    result[k++] = s[i];
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
        public static string FormatHexadecimal(this int number, int minPlaces)
        {
            string r = string.Format(CultureInfo.CurrentCulture, "{0:X}", number);
            return string.Format(CultureInfo.CurrentCulture, "{0}{1}", '0'.Repeat(minPlaces - r.Length), r);
        }

        public static string FormatHexadecimal(this byte[] array)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in array)
                sb.Append(((int)b).FormatHexadecimal(2));
            return sb.ToString();
        }

        public static byte[] ToByteArray(this string source)
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
        public static string GetRelativeRoot(this IEnumerable<string> files)
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
        public static string GetExtensionFromRelativeRoot(string path, string relativeRoot, string uptrail = @".\")
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

    }
}