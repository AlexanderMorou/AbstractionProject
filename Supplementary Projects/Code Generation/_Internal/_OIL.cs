#if x64
using SlotType = System.UInt64;
#elif x86
using SlotType = System.UInt32;
#endif
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.CSharp;
using AllenCopeland.Abstraction.OldCodeGen;
using AllenCopeland.Abstraction.OldCodeGen.Types;
using AllenCopeland.Abstraction.OldCodeGen.Utilities.Collections;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;


namespace AllenCopeland.Abstraction.OldCodeGen._Internal
{
    internal static partial class _OIL
    {
        public static string FixedJoinSeries(this string[] series, string jointElement, int maxWidth = 80)
        {
            int jointElementLength = jointElement.Length;
            int seriesMax = 0;
            if (series.Length > 0)
                seriesMax = series.Max(element => element.Length);
            int maxLengthAllowed = Math.Max(seriesMax, maxWidth) + jointElementLength;
            StringBuilder resultBuilder = new StringBuilder();
            bool firstElement = true;
            int currentLength = 0;
            int maxActual = maxLengthAllowed - jointElement.Length;
            foreach (var element in series)
            {
                if (firstElement)
                    firstElement = false;
                else
                {
                    resultBuilder.Append(jointElement);
                    currentLength += jointElementLength;
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
                        if (encodeSpaces)
                            sb.Append("&nbsp;");
                        else
                            sb.Append(" ");
                        break;
                    case '\t':

                    default:
                        if (c <= 0xFF)
                            sb.Append(c);
                        else
                            sb.AppendFormat("&#{0:000#};", (int)c);
                        break;
                }
            }
            return sb.ToString();
        }

        public static string Repeat(this char c, int length)
        {
            char[] result = new char[length];
            for (int i = 0; i < result.Length; i++)
                result[i] = c;
            return new string(result);
        }
        public static string Repeat(this string s, int length)
        {
            char[] result = new char[s.Length * length];
            for (int j = 0, k = 0; j < length; j++)
                for (int i = 0; i < s.Length; i++)
                    result[k++] = s[i];
            return new string(result);
        }

        internal unsafe static SlotType[] ObtainFiniteSeries(this BitArray characters, int FullSetLength)
        {
            if (characters == null)
                throw new ArgumentNullException("characters");
            else if (characters.Length > FullSetLength + 1)
                throw new ArgumentOutOfRangeException("characters");

            int[] values = new int[(int)Math.Ceiling(((double)(characters.Length)) / 32D)];
            characters.CopyTo(values, 0);
#if x86
            uint[] values2 = new uint[values.Length];
            for (int i = 0; i < values2.Length; i++)
                values2[i] = unchecked((uint)values[i]);
#elif x64
            ulong[] values2 = new ulong[(int)Math.Ceiling(((double)(values.Length)) / 2)];
            fixed (SlotType* values2ptr = values2)
            {
                uint* v2p = (uint*)values2ptr;
                fixed (int* valuesPtr = values)
                {
                    uint* vp = (uint*)valuesPtr;
                    for (int i = 0; i < values.Length; i++)
                    {
                        *v2p = *vp;
                        v2p++;
                        vp++;
                    }
                }
            }
#endif
            return values2;
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
