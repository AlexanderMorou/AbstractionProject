using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
//using AllenCopeland.Abstraction.Utilities.Tuples;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Common;

/*---------------------------------------------------------------------\
| Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal
{
    internal static class _CoreHelperMethods
    {
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
            var spaceStack = new Stack<INamespaceDeclaration>();
            for (var currentNamespace = @namespace; currentNamespace != null; currentNamespace = currentNamespace.Parent as INamespaceDeclaration)
                spaceStack.Push(currentNamespace);
            bool first = true;
            StringBuilder fullName = new StringBuilder();
            while (spaceStack.Count > 0)
            {
                if (first)
                    first = false;
                else
                    fullName.Append('.');
                fullName.Append(spaceStack.Pop().Name);
            }
            return fullName.ToString();
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
                var points = (from s in path.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries)
                                   select AstIdentifier.Declaration(s)).ToArray();
                foreach (var s in points)
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
                return target.Values[target.Keys.GetIndexOf(AstIdentifier.Declaration(path))];
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
            {
                var token = AstIdentifier.Declaration(s);
                if (s.IsEmptyOrNull())
                    if (ind == null)
                        if (target.ContainsKey(token))
                            ind = target[token];
                        else
                            return false;
                    else
                        if (ind.Namespaces.ContainsKey(token))
                            ind = ind.Namespaces[token];
                        else
                            return false;
                
            }
            return true;
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
