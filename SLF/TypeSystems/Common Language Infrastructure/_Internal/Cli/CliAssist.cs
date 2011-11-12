using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities;
using System.Globalization;
/*----------------------------------------\
| Copyright © 2012 Allen Copeland Jr.     |
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
        internal static IGeneralSignatureMemberUniqueIdentifier GetUniqueIdentifier(this EventInfo member)
        {
            var signatureType = member.EventHandlerType.GetTypeReference<IDelegateUniqueIdentifier, IDelegateType>();
            return AstIdentifier.Signature(member.Name, (signatureType.Parameters.ParameterTypes).SinglePass());
        }
        internal static IGeneralSignatureMemberUniqueIdentifier GetIndexerUniqueIdentifier(this PropertyInfo property)
        {
            return AstIdentifier.Signature(property.Name, (from param in property.GetIndexParameters()
                                                           select param.ParameterType.GetTypeReference()).SinglePass());
        }

        internal static IGeneralSignatureMemberUniqueIdentifier GetUniqueIdentifier(this ConstructorInfo constructor)
        {
            if (constructor.IsStatic)
                return AstIdentifier.CtorSignature();
            else
                return AstIdentifier.CtorSignature((from param in constructor.GetParameters()
                                                    select param.ParameterType.GetTypeReference()).SinglePass());
        }

        internal static IGeneralMemberUniqueIdentifier GetUniqueIdentifier(this PropertyInfo property)
        {
            return AstIdentifier.Member(property.Name);
        }

        internal static IGeneralMemberUniqueIdentifier GetUniqueIdentifier(this FieldInfo field)
        {
            return AstIdentifier.Member(field.Name);
        }

        internal static IBinaryOperatorUniqueIdentifier GetBinaryOperatorUniqueIdentifier(this MethodInfo target)
        {
            BinaryOpCoercionContainingSide containingSide;
            CoercibleBinaryOperators _operator;
            IType otherSide = null;
            Type[] _params = target.GetParameters().OnAll(p => p.ParameterType).ToArray();
            if (_params.Length != 2)
                throw new InvalidOperationException("object in invalid state, member info not of proper kind.");
            Type pType = target.DeclaringType;
            if (_params[0] == pType)
                if (_params[1] == pType)
                    containingSide = BinaryOpCoercionContainingSide.Both;
                else
                {
                    containingSide = BinaryOpCoercionContainingSide.LeftSide;
                    otherSide = _params[1].GetTypeReference();
                }
            else if (_params[1] == pType)
            {
                containingSide = BinaryOpCoercionContainingSide.RightSide;
                otherSide = _params[0].GetTypeReference();
            }
            else
                throw new InvalidOperationException("object in invalid state, binary operation doesn't work on containing type.");
            string s = target.Name;
            //CLI operator overload names.
            switch (s)
            {
                case CLICommon.BinaryOperatorNames.Addition: //                '+'      - op_Addition
                    _operator = CoercibleBinaryOperators.Add;
                    break;
                case CLICommon.BinaryOperatorNames.BitwiseAnd: //          '&' or 'And' - op_BitwiseAnd
                    _operator = CoercibleBinaryOperators.BitwiseAnd;
                    break;
                case CLICommon.BinaryOperatorNames.BitwiseOr: //           '|' or "Or"  - op_BitwiseOr
                    _operator = CoercibleBinaryOperators.BitwiseOr;
                    break;
                case CLICommon.BinaryOperatorNames.Division: //                '/'      - op_Division
                    _operator = CoercibleBinaryOperators.Divide;
                    break;
                case CLICommon.BinaryOperatorNames.ExclusiveOr: //         '^' or 'XOr' - op_ExclusiveOr
                    _operator = CoercibleBinaryOperators.ExclusiveOr;
                    break;
                case CLICommon.BinaryOperatorNames.GreaterThan: //             '>'      - op_GreaterThan
                    _operator = CoercibleBinaryOperators.GreaterThan;
                    break;
                case CLICommon.BinaryOperatorNames.GreaterThanOrEqual: //  ">="         - op_GreaterThanOrEqual
                    _operator = CoercibleBinaryOperators.GreaterThanOrEqualTo;
                    break;
                case CLICommon.BinaryOperatorNames.Equality: //            "==" or '='  - op_Equality
                    _operator = CoercibleBinaryOperators.IsEqualTo;
                    break;
                case CLICommon.BinaryOperatorNames.Inequality: //          "!=" or "<>" - op_Inequality
                    _operator = CoercibleBinaryOperators.IsNotEqualTo;
                    break;
                case CLICommon.BinaryOperatorNames.LeftShift: //               "<<"     - op_LeftShift
                    _operator = CoercibleBinaryOperators.LeftShift;
                    break;
                case CLICommon.BinaryOperatorNames.LessThan: //                '<'      - op_LessThan
                    _operator = CoercibleBinaryOperators.LessThan;
                    break;
                case CLICommon.BinaryOperatorNames.LessThanOrEqual: //         "<="     - op_LessThanOrEqual
                    _operator = CoercibleBinaryOperators.LessThanOrEqualTo;
                    break;
                case CLICommon.BinaryOperatorNames.Modulus: //             '%' or "Mod" - op_Modulus
                    _operator = CoercibleBinaryOperators.Modulus;
                    break;
                case CLICommon.BinaryOperatorNames.Multiply: //                '*'      - op_Multiply
                    _operator = CoercibleBinaryOperators.Multiply;
                    break;
                case CLICommon.BinaryOperatorNames.RightShift: //             ">>"      - op_RightShift
                    _operator = CoercibleBinaryOperators.RightShift;
                    break;
                case CLICommon.BinaryOperatorNames.Subtraction: //             '-'      - op_Subtraction
                    _operator = CoercibleBinaryOperators.Subtract;
                    break;
                default:
                    throw new InvalidOperationException(string.Format("object in invalid state, binary operation ({0}) not supported.", s.Substring(3)));
            }
            return AstIdentifier.BinaryOperator(_operator, containingSide, otherSide);
        }

        internal static ITypeCoercionUniqueIdentifier GetTypeCoercionUniqueIdentifier(this MethodInfo target)
        {
            throw new NotImplementedException();
        }

        internal static IUnaryOperatorUniqueIdentifier GetUnaryOperatorUniqueIdentifier(this MethodInfo target)
        {
            throw new NotImplementedException();
        }

        internal static IGeneralTypeUniqueIdentifier GetUniqueIdentifier(this Type target)
        {
            if (target.IsEnum)
                return AstIdentifier.Type(target.Name);
            else
            {
                if (target.IsSubclassOf(typeof(Delegate)) && target != typeof(MulticastDelegate))
                    return AstIdentifier.Delegate(target.Name, target.GetGenericArguments().Length, target.GetTypeReference<IDelegateUniqueIdentifier, IDelegateType>().Parameters.ParameterTypes);
                else
                {
                    if (target.IsGenericParameter)
                        return AstIdentifier.GenericParameter(target.GenericParameterPosition);
                    int tpCnt = 0;
                    if (target.IsGenericType)
                    {
                        tpCnt = target.GetGenericArguments().Length;
                        if (target.DeclaringType != null && target.IsGenericType)
                            tpCnt -= target.DeclaringType.GetGenericArguments().Length;
                    }
                    return AstIdentifier.Type(target.Name, tpCnt);
                }
            }
        }

        internal static IGeneralGenericSignatureMemberUniqueIdentifier GetUniqueIdentifier(this MethodInfo target)
        {
            var q = target.ReturnType;
            var r = (from parameter in target.GetParameters()
                     select parameter.ParameterType.GetTypeReference()).SinglePass();
            int tpCnt = 0;
            if (target.IsGenericMethod)
                tpCnt = target.GetGenericArguments().Length;
            return AstIdentifier.GenericSignature(target.Name, tpCnt, r);
        }

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
                    Attribute[] allowMult = ArrayExtensions.Cast<Attribute, object>(attr.GetCustomAttributes(typeof(AttributeUsageAttribute), true));
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

        public static IEnumerable<IGeneralDeclarationUniqueIdentifier> GetAggregateIdentifiers(this INamespaceParent target)
        {
            return from identifier in
                       (target.Types as CompiledFullTypeDictionary).GetAggregateIdentifiers()
                       .Concat(target.Namespaces.Keys)
                   orderby identifier.Name ascending
                   select identifier;
        }

        public static Dictionary<Attribute, Type> GetHierarchicalMap(Type t)
        {
            Dictionary<Attribute, Type> results = new Dictionary<Attribute, Type>();
            Type current = t;
            while (true)
            {
                if (current == null)
                    break;
                Attribute[] custAttrs = ArrayExtensions.Cast<Attribute, object>(current.GetCustomAttributes(false));
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
