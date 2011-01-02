using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Common;
//using AllenCopeland.Abstraction.Utilities.Tuples;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System.Globalization;

 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal
{
    internal static class _CoreHelperMethods
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

        internal static string CSharpToString(this IType refType)
        {
            switch (refType.ElementClassification)
            {
                case TypeElementClassification.None:
                case TypeElementClassification.GenericTypeDefinition:
                    StringBuilder sb = new StringBuilder();
                    if (refType.DeclaringType != null && !refType.IsGenericTypeParameter)
                    {
                        sb.Append(refType.DeclaringType.CSharpToString());
                        sb.Append(".");
                    }
                    sb.Append(refType.Name);
                    if (refType.IsGenericConstruct && refType is IGenericType)
                    {
                        int declTGenParamCount = ((refType.DeclaringType == null) || (!(refType.DeclaringType.IsGenericConstruct)) || (!(refType.DeclaringType is IGenericType))) ? 0 : ((IGenericType)(refType.DeclaringType)).GenericParameters.Count,
                            gPIndex = 0;
                        if (declTGenParamCount != ((IGenericType)(refType)).GenericParameters.Count)
                        {
                            sb.Append("<");
                            bool first = true;
                            foreach (IType gP in ((IGenericType)(refType)).GenericParameters)
                            {
                                //gPIndex;
                                if (gPIndex++ >= declTGenParamCount)
                                {
                                    if (first)
                                        first = false;
                                    else
                                        sb.Append(", ");
                                    sb.Append(gP.CSharpToString());
                                }
                            }
                            sb.Append(">");
                        }
                    }
                    return sb.ToString();
                case TypeElementClassification.Array:
                    if (refType is IArrayType)
                    {
                        var topArray = refType as IArrayType;
                        Stack<int> arrayRanks = new Stack<int>();
                        for (; topArray != null; )
                        {
                            arrayRanks.Push(topArray.ArrayRank);
                            refType = topArray.ElementType;
                            if (refType is IArrayType)
                                topArray = refType as IArrayType;
                            else
                                break;
                        }
                        string ranks = string.Empty;
                        //Reverse.
                        arrayRanks = new Stack<int>(arrayRanks);
                        while (arrayRanks.Count > 0)
                            ranks += '[' + ','.Repeat(arrayRanks.Pop() - 1) + ']';

                        return string.Format("{0}{1}", refType.CSharpToString(), ranks);
                    }
                    else
                        return string.Format("{0}[?,...]", refType.ElementType.CSharpToString());
                case TypeElementClassification.Nullable:
                    return string.Format("{0}?", refType.ElementType.CSharpToString());
                case TypeElementClassification.Pointer:
                    return string.Format("{0}*", refType.ElementType.CSharpToString());
                case TypeElementClassification.Reference:
                    return string.Format("{0}&", refType.ElementType.CSharpToString());
            }
            return null;
        }

        internal static string EscapeStringOrCharCILAndCS(this string toEscape, bool isString = true)
        {
            StringBuilder sb = new StringBuilder((int)((float)(toEscape.Length + 8) * 1.1));
            if (isString)
                sb.Append(@"""");
            else
                sb.Append("'");
            for (int i = 0; i < toEscape.Length; i++)
            {
                char c = toEscape[i];
                //bool b = (i == (toEscape.Length - 1));
                switch (c)
                {
                    case '"':
                        if (!isString)
                            goto default;
                        sb.Append(@"\""");
                        break;
                    case '\'':
                        if (isString)
                            goto default;
                        sb.Append(@"\'");
                        break;
                    case '\\':
                        sb.Append(@"\\");
                        break;
                    case '\r':
                        sb.Append(@"\r");
                        break;
                    case '\n':
                        sb.Append(@"\n");
                        break;
                    case '\0':
                        sb.Append(@"\0");
                        break;
                    case '\x85':
                        sb.Append("\\x85");
                        break;
                    default:
                        if (c > 255)
                        {
                            var baseHexVal = string.Format("{0:x}", (int)(c));
                            while (baseHexVal.Length < 4)
                                baseHexVal = "0" + baseHexVal;
                            sb.AppendFormat("\\u{0}", baseHexVal);
                        }
                        else
                            sb.Append(c);
                        break;
                }
            }
            if (isString)
                sb.Append(@"""");
            else
                sb.Append("'");
            return sb.ToString();
        }
        /// <summary>
        /// Obtains the <see cref="String"/> representing the 
        /// full name of the <paramref name="namespace"/> provided.
        /// </summary>
        /// <param name="namespace">The <see cref="INamespaceDeclaration"/> to obtain
        /// the full name of.</param>
        /// <returns>A <see cref="String"/> value representing the full name 
        /// of the given namespace.</returns>
        internal static string GetFullName(this INamespaceDeclaration @namespace)
        {
            var u = @namespace;
            var k = new List<INamespaceDeclaration>();
            while (u != null)
            {
                k.Add(u);
                if (u.Parent is INamespaceDeclaration)
                    u = (INamespaceDeclaration)u.Parent;
                else
                    break;
            }
            k.Reverse();
            var first = true;
            var r = new StringBuilder();
            foreach (var v in k)
            {
                if (first)
                    first = false;
                else
                    r.Append('.');
                r.Append(v.Name);
            }
            return r.ToString();
        }

        /// <summary>
        /// Gets the namespace of the given key provided the <paramref name="path"/> 
        /// to the namespace.
        /// </summary>
        /// <param name="target">The <see cref="INamespaceDictionary"/> which
        /// contain the namespace hierarchy.</param>
        /// <param name="path">The <see cref="String"/> representing the relative path to follow
        /// to obtain the namespace declaration.</param>
        /// <returns>A new <see cref="INamespaceDeclaration"/> relative to the <paramref name="path"/>
        /// provided.</returns>
        public static INamespaceDeclaration GetThis(this INamespaceDictionary target, string path)
        {
            if (path.Contains('.'))
            {
                INamespaceDeclaration insd = null;
                string[] points = path.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in points)
                {
                    //The first is the dictionary calling this method.
                    if (insd == null)
                        insd = target.Values[target.Keys.GetIndexOf(s)];
                    else
                        insd = insd.Namespaces[s];
                }
                return insd;
            }
            else
                return target.Values[target.Keys.GetIndexOf(path)];
        }

        /// <summary>
        /// Determines whether the <paramref name="series"/> contains
        /// enough space to hold the <paramref name="numberOfNew"/> elements 
        /// relative to the actual <paramref name="itemCount"/> and the
        /// array <see cref="Array.Length"/>.
        /// </summary>
        /// <typeparam name="T">The type of element
        /// within the series.</typeparam>
        /// <param name="series">The series of <typeparamref name="T"/>
        /// elements to work with.</param>
        /// <param name="itemCount">The number of items actually contained within
        /// the <paramref name="series"/>.</param>
        /// <param name="numberOfNew">The <see cref="Int32"/> value representing
        /// the number of elements to introduce into the <paramref name="series"/>.</param>
        /// <returns>An array of <typeparamref name="T"/> elements representing
        /// the new buffer.</returns>
        internal static T[] EnsureSpaceExists<T>(this T[] series, int itemCount, int numberOfNew)
        {
            if (series == null)
            {
                var result = new T[(itemCount + numberOfNew) * 2];
                return result;
            }
            else if (series.Length < itemCount + numberOfNew)
            {
                int newLength = series.Length * 2;
                if (newLength < itemCount + numberOfNew)
                    /* *
                     * Ensures the next growth isn't the next item entered.
                     * */
                    newLength = (itemCount + numberOfNew) * 2;
                var copy = new T[newLength];
                for (int i = 0; i < series.Length; i++)
                    copy[i] = series[i];
                return copy;
            }
            return series;
        }

        public static bool PathExists(this INamespaceDictionary target, string path)
        {
            var paths = path.Split('.');
            INamespaceDeclaration ind = null;
            /* *
             * Iterate through each element in the 
             * namespace path until an element is 
             * not found or every element has been
             * iterated through.
             * */
            foreach (var s in paths)
                if (s.IsEmptyOrNull())
                    if (ind == null)
                        if (target.ContainsKey(s))
                            ind = target[s];
                        else
                            return false;
                    else
                        if (ind.Namespaces.ContainsKey(s))
                            ind = ind.Namespaces[s];
                        else
                            return false;
            return true;
        }

        internal static string GetUniqueIdentifier<TSignatureParameter, TSignature, TSignatureParent>(this IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent> target)
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
        {
            var q = target.ReturnType;
            var r = target.Parameters.Values.OnAll(t => t.ParameterType).ToCollection();
            ITypeCollectionBase s = null;
            if (target.IsGenericConstruct)
                s = target.GenericParameters;
            return string.Format("{0} {1}{2}({3})", q.FullName, target.Name, target.IsGenericConstruct ? string.Format(CultureInfo.CurrentCulture, "<{0}>",String.Join(", ", s.OnAll(t => t.Name).ToArray())) : string.Empty, string.Join(", ", r.OnAll(t => t.IsGenericTypeParameter ? t.Name : t.FullName).ToArray()));
        }

        public static TypedNameSeries ToSeries(this IEnumerable<TypedName> target)
        {
            TypedNameSeries tns = new TypedNameSeries();
            foreach (TypedName item in target)
                tns.Add(item);
            return tns;
        }

    }
}
