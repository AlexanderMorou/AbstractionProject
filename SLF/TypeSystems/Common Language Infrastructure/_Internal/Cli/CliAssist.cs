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
    internal struct MetadatumUsage
    {
        public readonly bool AllowMultiple;
        public bool Inherited;
        public MetadatumUsage(AttributeUsageAttribute attr)
        {
            this.AllowMultiple = attr.AllowMultiple;
            this.Inherited = attr.Inherited;
        }
    }
    internal static class CliAssist
    {
        internal static IGeneralSignatureMemberUniqueIdentifier GetUniqueIdentifier(this EventInfo member, ICliManager manager)
        {
            var signatureType = (IDelegateType) manager.ObtainTypeReference(member.EventHandlerType);
            if (signatureType == null)
                return null;
            var parameters = (signatureType.Parameters.ParameterTypes).SinglePass();
            return AstIdentifier.Signature(member.Name, parameters);
        }
        internal static IGeneralSignatureMemberUniqueIdentifier GetIndexerUniqueIdentifier(this PropertyInfo property, ICliManager manager)
        {
            return AstIdentifier.Signature(property.Name, (from param in property.GetIndexParameters()
                                                           select manager.ObtainTypeReference(param.ParameterType)).SinglePass());
        }

        internal static IGeneralSignatureMemberUniqueIdentifier GetUniqueIdentifier(this ConstructorInfo constructor, ICliManager manager)
        {
            if (constructor.IsStatic)
                return AstIdentifier.CtorSignature();
            else
                return AstIdentifier.CtorSignature((from param in constructor.GetParameters()
                                                    select manager.ObtainTypeReference(param.ParameterType)).SinglePass());
        }

        internal static IGeneralMemberUniqueIdentifier GetUniqueIdentifier(this PropertyInfo property)
        {
            return AstIdentifier.Member(property.Name);
        }

        internal static IGeneralMemberUniqueIdentifier GetUniqueIdentifier(this FieldInfo field)
        {
            return AstIdentifier.Member(field.Name);
        }

        internal static IBinaryOperatorUniqueIdentifier GetBinaryOperatorUniqueIdentifier(this MethodInfo target, ICliManager manager)
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
                    otherSide = manager.ObtainTypeReference(_params[1]);
                }
            else if (_params[1] == pType)
            {
                containingSide = BinaryOpCoercionContainingSide.RightSide;
                otherSide = manager.ObtainTypeReference(_params[0]);
            }
            else
                throw new InvalidOperationException("object in invalid state, binary operation doesn't work on containing type.");
            string s = target.Name;
            //CLI operator overload names.
            switch (s)
            {
                case CliCommon.BinaryOperatorNames.Addition: //                '+'      - op_Addition
                    _operator = CoercibleBinaryOperators.Add;
                    break;
                case CliCommon.BinaryOperatorNames.BitwiseAnd: //          '&' or 'And' - op_BitwiseAnd
                    _operator = CoercibleBinaryOperators.BitwiseAnd;
                    break;
                case CliCommon.BinaryOperatorNames.BitwiseOr: //           '|' or "Or"  - op_BitwiseOr
                    _operator = CoercibleBinaryOperators.BitwiseOr;
                    break;
                case CliCommon.BinaryOperatorNames.Division: //                '/'      - op_Division
                    _operator = CoercibleBinaryOperators.Divide;
                    break;
                case CliCommon.BinaryOperatorNames.ExclusiveOr: //         '^' or 'XOr' - op_ExclusiveOr
                    _operator = CoercibleBinaryOperators.ExclusiveOr;
                    break;
                case CliCommon.BinaryOperatorNames.GreaterThan: //             '>'      - op_GreaterThan
                    _operator = CoercibleBinaryOperators.GreaterThan;
                    break;
                case CliCommon.BinaryOperatorNames.GreaterThanOrEqual: //  ">="         - op_GreaterThanOrEqual
                    _operator = CoercibleBinaryOperators.GreaterThanOrEqualTo;
                    break;
                case CliCommon.BinaryOperatorNames.Equality: //            "==" or '='  - op_Equality
                    _operator = CoercibleBinaryOperators.IsEqualTo;
                    break;
                case CliCommon.BinaryOperatorNames.Inequality: //          "!=" or "<>" - op_Inequality
                    _operator = CoercibleBinaryOperators.IsNotEqualTo;
                    break;
                case CliCommon.BinaryOperatorNames.LeftShift: //               "<<"     - op_LeftShift
                    _operator = CoercibleBinaryOperators.LeftShift;
                    break;
                case CliCommon.BinaryOperatorNames.LessThan: //                '<'      - op_LessThan
                    _operator = CoercibleBinaryOperators.LessThan;
                    break;
                case CliCommon.BinaryOperatorNames.LessThanOrEqual: //         "<="     - op_LessThanOrEqual
                    _operator = CoercibleBinaryOperators.LessThanOrEqualTo;
                    break;
                case CliCommon.BinaryOperatorNames.Modulus: //             '%' or "Mod" - op_Modulus
                    _operator = CoercibleBinaryOperators.Modulus;
                    break;
                case CliCommon.BinaryOperatorNames.Multiply: //                '*'      - op_Multiply
                    _operator = CoercibleBinaryOperators.Multiply;
                    break;
                case CliCommon.BinaryOperatorNames.RightShift: //             ">>"      - op_RightShift
                    _operator = CoercibleBinaryOperators.RightShift;
                    break;
                case CliCommon.BinaryOperatorNames.Subtraction: //             '-'      - op_Subtraction
                    _operator = CoercibleBinaryOperators.Subtract;
                    break;
                default:
                    throw new InvalidOperationException(string.Format("object in invalid state, binary operation ({0}) not supported.", s.Substring(3)));
            }
            return AstIdentifier.BinaryOperator(_operator, containingSide, otherSide);
        }

        internal static ITypeCoercionUniqueIdentifier GetTypeCoercionUniqueIdentifier(this MethodInfo target, ICliManager manager)
        {
            IType coercionType = null;
            TypeConversionDirection direction;
            var declaringType = target.DeclaringType;
            var firstParam = target.GetParameters().First();
            if (declaringType == target.ReturnType)
            {
                coercionType = manager.ObtainTypeReference(firstParam.ParameterType);
                direction = TypeConversionDirection.ToContainingType;
            }
            else
            {
                coercionType = manager.ObtainTypeReference(target.ReturnType);
                direction = TypeConversionDirection.FromContainingType;
            }
            return AstIdentifier.TypeOperator(target.GetTypeCoercionRequirement(), direction, coercionType);
        }

        internal static TypeConversionRequirement GetTypeCoercionRequirement(this MethodInfo target)
        {
            if (target != null &&
               !string.IsNullOrEmpty(target.Name))
                switch (target.Name)
                {
                    case CliCommon.TypeCoercionNames.Explicit:
                        return TypeConversionRequirement.Explicit;
                    case CliCommon.TypeCoercionNames.Implicit:
                        return TypeConversionRequirement.Implicit;
                }
            throw new InvalidOperationException();
        }

        internal static IUnaryOperatorUniqueIdentifier GetUnaryOperatorUniqueIdentifier(this MethodInfo target)
        {
            switch (target.Name)
            {
                case CliCommon.UnaryOperatorNames.Plus:
                    return AstIdentifier.UnaryOperator(CoercibleUnaryOperators.Plus);
                case CliCommon.UnaryOperatorNames.Negation:
                    return AstIdentifier.UnaryOperator(CoercibleUnaryOperators.Negation);
                case CliCommon.UnaryOperatorNames.False:
                    return AstIdentifier.UnaryOperator(CoercibleUnaryOperators.EvaluatesToFalse);
                case CliCommon.UnaryOperatorNames.True:
                    return AstIdentifier.UnaryOperator(CoercibleUnaryOperators.EvaluatesToTrue);
                case CliCommon.UnaryOperatorNames.LogicalNot:
                    return AstIdentifier.UnaryOperator(CoercibleUnaryOperators.LogicalInvert);
                case CliCommon.UnaryOperatorNames.OnesComplement:
                    return AstIdentifier.UnaryOperator(CoercibleUnaryOperators.Complement);
                case CliCommon.UnaryOperatorNames.Increment:
                    return AstIdentifier.UnaryOperator(CoercibleUnaryOperators.Increment);
                case CliCommon.UnaryOperatorNames.Decrement:
                    return AstIdentifier.UnaryOperator(CoercibleUnaryOperators.Decrement);
                default:
                    if (target.Name.Length < 3)
                        throw new InvalidOperationException(string.Format("object in invalid state, unary operation ({0}) not supported.", target.Name));
                    else
                        throw new InvalidOperationException(string.Format("object in invalid state, unary operation ({0}) not supported.", target.Name.Substring(3)));
            }
        }

        internal static IGeneralTypeUniqueIdentifier GetUniqueIdentifier(this Type target, ICliManager manager)
        {
            if (target.IsEnum)
                return AstIdentifier.Type(target.Name);
            else
            {
                if (target.IsSubclassOf(typeof(Delegate)) && target != typeof(MulticastDelegate))
                    return AstIdentifier.Delegate(target.Name, target.GetGenericArguments().Length, ((IDelegateType)manager.ObtainTypeReference(target)).Parameters.ParameterTypes.SinglePass());
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
                        string nameModifier = string.Format("`{0}", tpCnt);
                        string name = target.Name;
                        if (tpCnt > 0 && name.Length >= nameModifier.Length)
                            name = name.Substring(0, name.Length - nameModifier.Length);
                        return AstIdentifier.Type(name, tpCnt);
                    }
                    return AstIdentifier.Type(target.Name, tpCnt);
                }
            }
        }

        internal static IGeneralGenericSignatureMemberUniqueIdentifier GetUniqueIdentifier(this MethodInfo target, ICliManager manager)
        {
            var q = target.ReturnType;
            var r = (from parameter in target.GetParameters()
                     select manager.ObtainTypeReference(parameter.ParameterType)).ToArray();
            int tpCnt = 0;
            if (target.IsGenericMethod)
                tpCnt = target.GetGenericArguments().Length;
            return AstIdentifier.GenericSignature(target.Name, tpCnt, r);
        }

        private static Dictionary<Type, MetadatumUsage> attributeUsageCache;

        static CliAssist()
        {
            attributeUsageCache = new Dictionary<Type, MetadatumUsage>();
        }
        /// <summary>
        /// Obtains the usage information on an attribute.
        /// </summary>
        /// <param name="attr">The attribute to check the usage of.</param>
        /// <returns>An <see cref="MetadatumUsage"/> structure defining the usage of an attribute.</returns>
        internal static MetadatumUsage GetAttributeUsage(this Type attr)
        {
            if (attributeUsageCache.ContainsKey(attr))
                return attributeUsageCache[attr];
            else
            {
                MetadatumUsage result;
                if (attr.IsDefined(typeof(AttributeUsageAttribute), true))
                {
                    Attribute[] allowMult = ArrayExtensions.Cast<Attribute, object>(attr.GetCustomAttributes(typeof(AttributeUsageAttribute), true));
                    if (allowMult.Length > 0 && allowMult[0] is AttributeUsageAttribute)
                        result = new MetadatumUsage((AttributeUsageAttribute)allowMult[0]);
                    else
                    {
                        result = new MetadatumUsage();
                        result.Inherited = true;
                    }
                }
                else
                {
                    result = new MetadatumUsage();
                    result.Inherited = true;
                }
                attributeUsageCache.Add(attr, result);
                return result;
            }
        }

        internal static MetadatumUsage GetAttributeUsage(this IType attr, ICliManager manager)
        {
            MetadatumUsage result;
            if (attr.IsDefined(manager.ObtainTypeReference(typeof(AttributeUsageAttribute)), true))
            {
                IMetadatum allowMult = attr.CustomAttributes[manager.ObtainTypeReference(typeof(AttributeUsageAttribute))];
                if (allowMult != null)
                    result = new MetadatumUsage(allowMult.WrappedAttribute as AttributeUsageAttribute);
                else
                    result = new MetadatumUsage(new AttributeUsageAttribute(AttributeTargets.All) { Inherited = true });
            }
            else
                result = new MetadatumUsage(new AttributeUsageAttribute(AttributeTargets.All) { Inherited = true });
            return result;
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
                    MetadatumUsage attrUsage = GetAttributeUsage(attr.GetType());
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


        public static IClassMethodMember ObtainPreviousDefinition(this IClassMethodMember target)
        {
            for (IClassType p = target.Parent.BaseType; p != null; p = p.BaseType)
                foreach (var methodMember in p.Methods.Values)
                    if (methodMember.Name != target.Name ||
                        methodMember.Parameters.Count != target.Parameters.Count ||
                        methodMember.IsGenericConstruct != target.IsGenericConstruct ||
                        target.IsGenericConstruct && methodMember.TypeParameters.Count != target.TypeParameters.Count)
                        continue;
                    else
                    {
                        bool match = true;
                        /* *
                         * For the sake of this find operation,
                         * the type-parameters will be mostly ignored to 
                         * ensure that the base declaration is found.
                         * *
                         * If the signature matches, it's a valid override;
                         * however, if the constraints upon the type-parameter
                         * don't match, then that's the compiler's domain to
                         * notify.
                         * */
                        for (int i = 0; i < target.Parameters.Count; i++)
                        {
                            /* *
                             * Var variable declaration is helpful for 
                             * cases like this.
                             * */
                            var targetParam = methodMember.Parameters.Values[i];
                            var sourceParam = target.Parameters.Values[i];
                            if (targetParam.Direction != sourceParam.Direction)
                            {
                                match = false;
                                break;
                            }
                            else if (targetParam.ParameterType.IsGenericTypeParameter)
                            {
                                /* *
                                 * Rewrite this code so that when the source parameter
                                 * is a generic parameter, and its parent is the enclosing
                                 * type, that the newly declared version is equal to the
                                 * generic parameter defined in the inheritance chain.
                                 * */
                                if (!sourceParam.ParameterType.IsGenericTypeParameter)
                                {
                                    match = false;
                                    break;
                                }

                                IGenericParameter sourceTParam = (IGenericParameter)sourceParam.ParameterType,
                                                  targetTParam = (IGenericParameter)targetParam.ParameterType;
                                if (targetTParam.Parent == methodMember)
                                {
                                    if (sourceTParam.Parent != target)
                                    {
                                        match = false;
                                        break;
                                    }
                                    match = sourceTParam.Position == targetTParam.Position;
                                }
                                else if (targetTParam.Parent == target.Parent)
                                {
                                    if (sourceTParam.Parent != target.Parent)
                                    {
                                        match = false;
                                        break;
                                    }
                                    match = sourceTParam.Position == targetTParam.Position;
                                }
                            }
                            else
                                match = targetParam.ParameterType.Equals(sourceParam.ParameterType);
                            if (!match)
                                break;
                        }
                        if (match)
                            return methodMember;
                    }
            throw new InvalidOperationException("match not found");
        }
    }
}
