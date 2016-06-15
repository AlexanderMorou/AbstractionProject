using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
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

        public static string LowerFirstCharacter(this string value)
        {
            if (Char.IsUpper(value[0]))
                return string.Format("{0}{1}", Char.ToLower(value[0]), value.Substring(1));
            return value;
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
            string r = string.Format(CultureInfo.CurrentCulture, "{0:x}", number);
            return string.Format(CultureInfo.CurrentCulture, "{0}{1}", '0'.Repeat(minPlaces - r.Length), r);
        }

        public static string FormatHexadecimal(this byte[] array)
        {
            if (array == null)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (byte b in array)
                sb.Append(((int)b).FormatHexadecimal(2));
            return sb.ToString();
        }

        public static byte[] FromFormatHexadecimal(string text)
        {
            if (text.Length % 2 == 1)
                throw new ArgumentException("text must be presented as two characters per byte.", "text");
            byte[] result = new byte[text.Length / 2];
            /* Perform initial data validation scan. */
            foreach (var @char in text)
                switch (@char)
                {
                    case '0':case '1':case '2':case '3':case '4':case '5':case '6':case '7':case '8':case '9':
                    case 'A':case 'B':case 'C':case 'D':case 'E':case 'F':
                    case 'a':case 'b':case 'c':case 'd':case 'e':case 'f':
                        continue;
                    default:
                        throw new ArgumentOutOfRangeException("text", "Non hexadecimal value encountered.");
                }
            for (int byteIndex = 0; byteIndex < result.Length; byteIndex++)
                result[byteIndex] = Convert.ToByte(text.Substring((byteIndex * 2), 2), 16);
            return result;
        }

        public static byte[] ToByteArray(this string source)
        {
            byte[] b              = new byte[source.Length * 2];
            const int lb          = 0x00FF;
            const int hb          = 0xFF00;
            const int byteBitSize = 8;
            for (int i = 0, i2 = 0; i < source.Length; i++, i2 += 2)
            {
                ushort cur = source[i];
                b[i2]      = (byte)((cur & hb) >> byteBitSize);
                b[i2 + 1]  = (byte)(cur & lb);
            }
            return b;
        }

        public static string FromByteArray(this byte[] source)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (source.Length % 2 != 0)
                throw new ArgumentOutOfRangeException("source", "source must be a set of bytes evenly divisible by two.");
            char[] result = new char[source.Length / 2];
            for (int i = 0, j = 0; i < result.Length; i++, j += 2)
                result[i] = (char)((int)source[j + 1] | (((int)source[j]) << 8));
            return new string(result);
        }

        public static string GetRelativeRoot(this IEnumerable<string> files, string directorySeparator = @"\")
        {
            var longestString = (from f in files
                                 let ePath = directorySeparator == @"\" ? Path.GetDirectoryName(f) : f
                                 orderby ePath.Length descending
                                 select ePath).FirstOrDefault();
            if (longestString == null)
                return null;
            var parts = longestString.Split(new string[] { directorySeparator }, StringSplitOptions.RemoveEmptyEntries);

            string relativeRoot = null;
            for (int i = 0; i < parts.Length; i++)
            {
                string currentRoot = string.Join(directorySeparator, parts, 0, parts.Length - i);
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

        public static int LeftDiff(this string target, string other)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (other == null)
                throw new ArgumentNullException("other");
            int minLength = Math.Min(target.Length, other.Length);
            for (int i = 0; i < minLength; i++)
                if (target[i] != other[i])
                    return i;
            return minLength;
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
            target.Matches(text);
            return target.Match(text).AsEnumerable();
        }

        public static IEnumerable<CurrentPreviousPair> IndicesOf(this string target, string search)
        {
            int last = 0;
        nextIndex:
            int current = target.IndexOf(search, last);
            yield return new CurrentPreviousPair(last, current);
            if (current != -1)
            {
                last = current + search.Length;
                goto nextIndex;
            }
        }

        public static string HtmlEncode(this string toEncode, bool encodeSpaces = true)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in toEncode)
            {
                switch (c)
                {
                    case '<':
                        sb.Append("&lt;");
                        break;
                    case '>':
                        sb.Append("&gt;");
                        break;
                    case '&':
                        sb.Append("&amp;");
                        break;
                    case ' ':
                    case '\xA0':
                        if (encodeSpaces)
                            sb.Append("&nbsp;");
                        else
                            sb.Append(c);
                        break;
                    default:
                        if (c <= 127)
                            sb.Append(c);
                        else
                            sb.AppendFormat("&#x{0:X};", (int)c);
                        break;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Returns the number of occurrences of <paramref name="substring"/> within the <paramref name="original"/> <see cref="String"/> provided.
        /// </summary>
        /// <param name="original">The <see cref="String"/> value to perform the count on.</param>
        /// <param name="substring">The <see cref="String"/> to count the occurrences of.</param>
        /// <returns>A <see cref="Int32"/> value of the number of occurrences of <paramref name="substring"/> within the <paramref name="original"/>
        /// <see cref="String"/> provided.</returns>
        public static int CountOccurrences(this string original, string substring)
        {
            if (string.IsNullOrEmpty(substring))
                return 0;
            if (substring.Length == 1) //Corner Case
                return CountOccurrences(original, substring[0]);
            if (string.IsNullOrEmpty(original) ||
                substring.Length > original.Length)
                return 0;
            int substringCount = 0;
            for (int charIndex = 0; charIndex < original.Length; charIndex++)
            {
                for (int subCharIndex = 0, secondaryCharIndex = charIndex; subCharIndex < substring.Length && secondaryCharIndex < original.Length; subCharIndex++, secondaryCharIndex++)
                {
                    if (substring[subCharIndex] != original[secondaryCharIndex])
                        goto continueOuter;
                }
                charIndex += substring.Length - 1;
                substringCount++;
                if (charIndex + substring.Length >= original.Length)
                    break;
            continueOuter:
                ;
            }
            return substringCount;
        }

        public static int CountOccurrences(this string original, char @char)
        {
            if (string.IsNullOrEmpty(original))
                return 0;
            int occurrenceCount = 0;
            for (int charIndex = 0; charIndex < original.Length; charIndex++)
                if (@char == original[charIndex])
                    occurrenceCount++;
            return occurrenceCount;
        }

        public static IEnumerable<string> ConditionalSplit(this string target, DetermineSplitState stateHandler)
        {
            if (stateHandler == null)
                throw new ArgumentNullException("stateHandler");
            if (target == null)
                yield break;
            if (target.Length == 0)
                yield break;
            int currentSectionStart = 0;
            int charIndex = 0;

            GetCharFromCurrentPosition peeker =
                (i) =>
                {
                    if (charIndex + i < 0 || charIndex + i >= target.Length)
                        return null;
                    return target[charIndex + i];
                };
            for (; charIndex < target.Length; charIndex++)
            {
                switch (stateHandler(target[charIndex], peeker))
                {
                    case SpecialSplitDetail.DoNothing:
                        break;
                    case SpecialSplitDetail.BreakRemoveCurrent:
                        yield return target.Substring(currentSectionStart, charIndex - currentSectionStart);
                        currentSectionStart = charIndex + 1;
                        break;
                    case SpecialSplitDetail.Break:
                        yield return target.Substring(currentSectionStart, charIndex - currentSectionStart + 1);
                        currentSectionStart = charIndex + 1;
                        break;
                    default:
                        throw new InvalidOperationException("Invalid split option provided.");
                }
            }
            if (currentSectionStart != target.Length)
                yield return target.Substring(currentSectionStart);
        }

        public static string[] TitleCaseIdentifierSplit(this string target)
        {
            return target.ConditionalSplit((c, s) =>
            {
                if (Char.IsLower(c) || c == '.' || char.IsNumber(c))
                {
                    var advance = s(1);
                    if (advance != null && char.IsUpper(advance.Value) || advance == null)
                        if (c == '.')
                            return SpecialSplitDetail.BreakRemoveCurrent;
                        else
                            return SpecialSplitDetail.Break;
                }
                else if (char.IsUpper(c))
                {
                    var a1  = s(1);
                    var a2  = s(2);
                    if (a1 != null && a2 != null)
                        if (char.IsUpper(a1.Value))
                            if (char.IsLower(a2.Value) || a2.Value == '.')
                                return SpecialSplitDetail.Break;
                }
                return SpecialSplitDetail.DoNothing;
            }).ToArray();
        }

        public static string GenerateTitleCaseSpacedIdentifier(this string target)
        {
            return string.Join(" ", target.TitleCaseIdentifierSplit());
        }
    }

    public delegate char? GetCharFromCurrentPosition(int index);
    public delegate SpecialSplitDetail DetermineSplitState(char currentCharacter, GetCharFromCurrentPosition peeker);
    public enum SpecialSplitDetail
    {
        DoNothing,
        Break,
        BreakRemoveCurrent,
    }
}
