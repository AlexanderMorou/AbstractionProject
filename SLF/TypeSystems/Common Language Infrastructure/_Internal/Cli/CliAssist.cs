using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Slf.Abstract;
/*----------------------------------------\
| Copyright © 2011 Allen Copeland Jr.     |
|-----------------------------------------|
| The Abstraction Project's code is prov- |
| -ided under a contract-release basis.   |
| DO NOT DISTRIBUTE and do not use beyond |
| the contract terms.                     |
\--------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    /// <summary>
    /// Defines the attribute usage from an <see cref="AttributeUsageAttribute"/>.
    /// </summary>
    internal struct AttributeUsage
    {
        public readonly bool AllowMultiple;
        public bool Inherited;
        public AttributeUsage(AttributeUsageAttribute attr)
        {
            this.AllowMultiple = attr.AllowMultiple;
            this.Inherited = attr.Inherited;
        }
    }
    internal static class CliAssist
    {

        private static Dictionary<Type, AttributeUsage> attributeUsageCache;

        static CliAssist()
        {
            attributeUsageCache = new Dictionary<Type, AttributeUsage>();
        }
        /// <summary>
        /// Obtains the usage information on an attribute.
        /// </summary>
        /// <param name="attr">The attribute to check the usage of.</param>
        /// <returns>An <see cref="AttributeUsage"/> structure defining the usage of an attribute.</returns>
        internal static AttributeUsage GetAttributeUsage(this Type attr)
        {
            if (attributeUsageCache.ContainsKey(attr))
                return attributeUsageCache[attr];
            else
            {
                AttributeUsage result;
                if (attr.IsDefined(typeof(AttributeUsageAttribute), true))
                {
                    Attribute[] allowMult = Tweaks.Cast<Attribute, object>(attr.GetCustomAttributes(typeof(AttributeUsageAttribute), true));
                    if (allowMult.Length > 0 && allowMult[0] is AttributeUsageAttribute)
                        result = new AttributeUsage((AttributeUsageAttribute)allowMult[0]);
                    else
                    {
                        result = new AttributeUsage();
                        result.Inherited = true;
                    }
                }
                else
                {
                    result = new AttributeUsage();
                    result.Inherited = true;
                }
                attributeUsageCache.Add(attr, result);
                return result;
            }
        }

        public static IEnumerable<string> GetAggregateIdentifiers(this INamespaceParent target)
        {
            return (target.Types as CompiledFullTypeDictionary).GetAggregateIdentifiers().Concat(
                from @namespace in target.Namespaces.Values
                select @namespace.Name).Distinct();
        }

        public static Dictionary<Attribute, Type> GetHierarchicalMap(Type t)
        {
            Dictionary<Attribute, Type> results = new Dictionary<Attribute, Type>();
            Type current = t;
            while (true)
            {
                if (current == null)
                    break;
                Attribute[] custAttrs = Tweaks.Cast<Attribute, object>(current.GetCustomAttributes(false));
                foreach (Attribute attr in custAttrs)
                {
                    AttributeUsage attrUsage = GetAttributeUsage(attr.GetType());
                    if (/* * 
                         * If the current element is the top-level in the hierarchy,
                         * all elements will be added.
                         * */
                        (current == t) ||

                        /* *
                         * Regardless of the current type, if there's multiple allowed
                         * and it's inherited: valid entry.
                         * */
                        (attrUsage.AllowMultiple
                      && attrUsage.Inherited) ||

                        /* *
                         * If multiples aren't allowed, and the attribute doesn't exist:
                         * valid entry.
                         * */
                        (attrUsage.Inherited
                            && (!attrUsage.AllowMultiple)
                            && (!results.Values.Contains(attr.GetType())))
                        )
                        results.Add(attr, current);
                }
                current = current.BaseType;
            }
            return results;
        }
    }
}
