using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using System.Diagnostics;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal static class CliMemberExtensions
    {
        public static IEnumerable<Tuple<CliMemberType, ICliMetadataTableRow>> GetMemberData2(this ICliMetadataTypeDefinitionTableRow target)
        {
            var fields = target.Fields;
            var methods = target.Methods;
            var properties = target.Properties;
            var events = target.Events;
            var methodQuery = from m in methods
                              select new Tuple<CliMemberType, ICliMetadataTableRow>(CliMemberType.Method, m);
            var propertyQuery = from m in properties
                                select new Tuple<CliMemberType, ICliMetadataTableRow>(CliMemberType.Property, m);
            var eventQuery = from m in properties
                             select new Tuple<CliMemberType, ICliMetadataTableRow>(CliMemberType.Property, m);
            IEnumerable<Tuple<CliMemberType, ICliMetadataTableRow>> result = (fields == null) ? null : from f in fields
                                                                                                       select new Tuple<CliMemberType, ICliMetadataTableRow>(CliMemberType.Field, f);
            if (result == null && methods != null)
                result = methodQuery;
            else if (methods != null)
                result = result.Concat(methodQuery);
            if (result == null && properties != null)
                result = propertyQuery;
            else if (properties != null)
                result = result.Concat(propertyQuery);
            if (result == null && events != null)
                result = eventQuery;
            else if (events != null)
                result = result.Concat(eventQuery);
            return result ?? new Tuple<CliMemberType, ICliMetadataTableRow>[0];
            //return target.Methods.Concat<ICliMetadataTableRow>(target.Properties.Concat<ICliMetadataTableRow>(target.Events.Concat<ICliMetadataTableRow>(target.Fields)));
        }

        public static IEnumerable<Tuple<CliMemberType, ICliMetadataTableRow>> GetMemberData(this ICliMetadataTypeDefinitionTableRow target)
        {
            if (target.Name == "AstIdentifier")
            {
                Console.WriteLine();
            }
            IEnumerable<Tuple<CliMemberType, uint, ICliMetadataTableRow>> propertyData = null;
            /* *
             * Build our own list of methods tied to semantics,
             * this is about 3 times faster than querying the full
             * set each time, but uses a little more memory.
             * */
            List<uint> fullSemantics = new List<uint>();
            uint fieldCount = target.Fields == null ? 0 : (uint)target.Fields.Count;
            var properties = target.Properties;
            /* *
             * Construct the property and event sets remembering the 
             * lowest order method associated to each.
             * *
             * This is for proper member ordering, fields are always first, because there's no context
             * information to order them relative to the original source, as fields don't 
             * associate to methods like events and properties do.
             * */
            if (properties == null || properties.Count == 0)
                propertyData = new Tuple<CliMemberType, uint, ICliMetadataTableRow>[0];
            else
            {
                uint startIndex = properties[0].Index;
                uint endIndex = (uint)(startIndex + properties.Count - 1);
                //var subSemantics = (from s in target.MetadataRoot.PropertySemantics
                //                    where ((startIndex <= s.AssociationIndex) && (s.AssociationIndex <= endIndex))
                //                    select s).ToArray();
                List<ICliMetadataMethodSemanticsTableRow> subSemantics = new List<ICliMetadataMethodSemanticsTableRow>();

                foreach (var propertySemantic in target.MetadataRoot.PropertySemantics)
                    if (startIndex <= propertySemantic.AssociationIndex && propertySemantic.AssociationIndex <= endIndex)
                    {
                        subSemantics.Add(propertySemantic);
                        fullSemantics.Add(propertySemantic.MethodIndex);
                    }
                propertyData = (from property in target.Properties
                                join semantics in subSemantics on property.Index equals semantics.AssociationIndex into methods
                                let minSemantics = methods.MinSemantics()
                                let elementType = (minSemantics.Semantics == MethodSemanticsAttributes.Getter ?
                                                       minSemantics.Method.Parameters.Count > 0 :
                                                       minSemantics.Method.Parameters.Count > 1) ? CliMemberType.Indexer : CliMemberType.Property
                                select new Tuple<CliMemberType, uint, ICliMetadataTableRow>(elementType, minSemantics.MethodIndex + fieldCount + target.FieldStartIndex, property));
            }
            IEnumerable<Tuple<CliMemberType, uint, ICliMetadataTableRow>> @eventData = null;
            var events = target.Events;
            if (events == null || events.Count == 0)
                @eventData = new Tuple<CliMemberType, uint, ICliMetadataTableRow>[0];
            else
            {
                uint startIndex = events[0].Index;
                uint endIndex = (uint)(startIndex + events.Count - 1);
                List<ICliMetadataMethodSemanticsTableRow> subSemantics = new List<ICliMetadataMethodSemanticsTableRow>();
                foreach (var eventSemantic in target.MetadataRoot.EventSemantics)
                    if (startIndex <= eventSemantic.AssociationIndex && eventSemantic.AssociationIndex <= endIndex)
                    {
                        subSemantics.Add(eventSemantic);
                        fullSemantics.Add(eventSemantic.MethodIndex);
                    }
                eventData = (from @event in target.Events
                             join semantics in subSemantics on @event.Index equals semantics.AssociationIndex into methods
                             select new Tuple<CliMemberType, uint, ICliMetadataTableRow>(CliMemberType.Event, methods.Min() + fieldCount + target.FieldStartIndex, @event));
            }
            /* *
             * Using the indices gathered from the events and properties, 
             * select difference from the type's overall method set.
             * *
             * Then distinguish each variation of method as a constructor (.ctor, .cctor),
             * binary operator (op_Addition, et al), unary operator (op_Decrement, et al),
             * or expression type coercion operator (op_Implicit, op_Explicit)
             * */

            var remainingMethods = (from m in
                                        from method in target.Methods
                                        where !fullSemantics.Contains(method.Index)
                                        select method
                                    //(target.Methods.Except(from m in target.Methods
                                    //                       join semantics in fullSemantics on m.Index equals semantics
                                    //                       select m))
                                    select new Tuple<CliMemberType, uint, ICliMetadataTableRow>(DiscernMemberType(m), m.Index + fieldCount + target.FieldStartIndex, m));
            /* *
             * Yield a full set of members, remembering their kind, and ordering them 
             * by a close approximate of their original order.
             * */
            IEnumerable<Tuple<CliMemberType, ICliMetadataTableRow>> result;
            if (target.Fields == null)
                result = from m in propertyData.Concat(eventData).Concat(remainingMethods).ToArray()
                         orderby m.Item2
                         select new Tuple<CliMemberType, ICliMetadataTableRow>(m.Item1, m.Item3);
            else
                result = from m in
                             propertyData.Concat(eventData).Concat(remainingMethods).Concat(
                                 from f in target.Fields
                                 select new Tuple<CliMemberType, uint, ICliMetadataTableRow>(CliMemberType.Field, f.Index, f)).ToArray()
                         orderby m.Item2
                         select new Tuple<CliMemberType, ICliMetadataTableRow>(m.Item1, m.Item3);
            return result;
        }

        private static uint Min(this IEnumerable<ICliMetadataMethodSemanticsTableRow> target)
        {
            uint result = 0;
            bool first = true;
            foreach (var element in target)
            {
                if (first)
                {
                    result = element.MethodIndex;
                    first = false;
                }
                else if (result > element.MethodIndex)
                    result = element.MethodIndex;
            }
            return result;
        }

        private static ICliMetadataMethodSemanticsTableRow MinSemantics(this IEnumerable<ICliMetadataMethodSemanticsTableRow> target)
        {
            ICliMetadataMethodSemanticsTableRow result = null;
            bool first = true;
            foreach (var element in target)
            {
                if (first)
                {
                    result = element;
                    first = false;
                }
                else if (result.MethodIndex > element.MethodIndex)
                    result = element;
            }
            return result;
        }

        private static CliMemberType DiscernMemberType(ICliMetadataMethodDefinitionTableRow method)
        {
            MethodUseFlags usageFlags = method.UsageDetails.UsageFlags;
            if ((usageFlags & (MethodUseFlags.SpecialName | MethodUseFlags.RTSpecialName)) == (MethodUseFlags.SpecialName | MethodUseFlags.RTSpecialName))
            {
                if (method.Name == CliCommon.ConstructorName && (usageFlags & MethodUseFlags.Static) != MethodUseFlags.Static)
                    return CliMemberType.Constructor;
                else if (method.Name == CliCommon.ConstructorStaticName && (usageFlags & MethodUseFlags.Static) == MethodUseFlags.Static)
                    return CliMemberType.Constructor;
            }
            else if ((usageFlags & (MethodUseFlags.SpecialName | MethodUseFlags.Static)) == (MethodUseFlags.SpecialName | MethodUseFlags.Static))
            {
                switch (method.Name)
                {
                    case CliCommon.BinaryOperatorNames.Addition:
                    case CliCommon.BinaryOperatorNames.BitwiseAnd:
                    case CliCommon.BinaryOperatorNames.BitwiseOr:
                    case CliCommon.BinaryOperatorNames.Division:
                    case CliCommon.BinaryOperatorNames.Equality:
                    case CliCommon.BinaryOperatorNames.ExclusiveOr:
                    case CliCommon.BinaryOperatorNames.GreaterThan:
                    case CliCommon.BinaryOperatorNames.GreaterThanOrEqual:
                    case CliCommon.BinaryOperatorNames.Inequality:
                    case CliCommon.BinaryOperatorNames.LeftShift:
                    case CliCommon.BinaryOperatorNames.LessThan:
                    case CliCommon.BinaryOperatorNames.LessThanOrEqual:
                    case CliCommon.BinaryOperatorNames.Modulus:
                    case CliCommon.BinaryOperatorNames.Multiply:
                    case CliCommon.BinaryOperatorNames.RightShift:
                    case CliCommon.BinaryOperatorNames.Subtraction:
                        if (method.Parameters.Count == 2)
                            return CliMemberType.BinaryOperator;
                        break;
                    case CliCommon.TypeCoercionNames.Implicit:
                    case CliCommon.TypeCoercionNames.Explicit:
                        if (method.Parameters.Count == 1)
                            return CliMemberType.TypeCoercionOperator;
                        break;
                    case CliCommon.UnaryOperatorNames.Decrement:
                    case CliCommon.UnaryOperatorNames.False:
                    case CliCommon.UnaryOperatorNames.Increment:
                    case CliCommon.UnaryOperatorNames.LogicalNot:
                    case CliCommon.UnaryOperatorNames.Negation:
                    case CliCommon.UnaryOperatorNames.OnesComplement:
                    case CliCommon.UnaryOperatorNames.Plus:
                    case CliCommon.UnaryOperatorNames.True:
                        if (method.Parameters.Count == 1)
                            return CliMemberType.UnaryOperator;
                        break;
                }
            }
            return CliMemberType.Method;
        }


        internal static IBinaryOperatorUniqueIdentifier GetBinaryOperatorIdentifier(ICliMetadataMethodDefinitionTableRow methodDef, IType owner, _ICliManager manager)
        {
            var left = manager.ObtainTypeReference(methodDef.Signature.Parameters[0].ParameterType, owner, null);
            var right = manager.ObtainTypeReference(methodDef.Signature.Parameters[1].ParameterType, owner, null);
            BinaryOpCoercionContainingSide containingSide =
                (left == owner && right == owner) ? BinaryOpCoercionContainingSide.Both :
                    left == owner ? BinaryOpCoercionContainingSide.LeftSide :
                    right == owner ? BinaryOpCoercionContainingSide.RightSide : BinaryOpCoercionContainingSide.Invalid;
            IType otherSide = containingSide == BinaryOpCoercionContainingSide.LeftSide ?
                right : containingSide == BinaryOpCoercionContainingSide.RightSide ?
                left : owner;
            switch (methodDef.Name)
            {
                case CliCommon.BinaryOperatorNames.Addition:
                    return AstIdentifier.GetBinaryOperatorIdentifier(CoercibleBinaryOperators.Add, containingSide, otherSide);
                case CliCommon.BinaryOperatorNames.BitwiseAnd:
                    return AstIdentifier.GetBinaryOperatorIdentifier(CoercibleBinaryOperators.BitwiseAnd, containingSide, otherSide);
                case CliCommon.BinaryOperatorNames.BitwiseOr:
                    return AstIdentifier.GetBinaryOperatorIdentifier(CoercibleBinaryOperators.BitwiseOr, containingSide, otherSide);
                case CliCommon.BinaryOperatorNames.Division:
                    return AstIdentifier.GetBinaryOperatorIdentifier(CoercibleBinaryOperators.Divide, containingSide, otherSide);
                case CliCommon.BinaryOperatorNames.Equality:
                    return AstIdentifier.GetBinaryOperatorIdentifier(CoercibleBinaryOperators.IsEqualTo, containingSide, otherSide);
                case CliCommon.BinaryOperatorNames.ExclusiveOr:
                    return AstIdentifier.GetBinaryOperatorIdentifier(CoercibleBinaryOperators.ExclusiveOr, containingSide, otherSide);
                case CliCommon.BinaryOperatorNames.GreaterThan:
                    return AstIdentifier.GetBinaryOperatorIdentifier(CoercibleBinaryOperators.GreaterThan, containingSide, otherSide);
                case CliCommon.BinaryOperatorNames.GreaterThanOrEqual:
                    return AstIdentifier.GetBinaryOperatorIdentifier(CoercibleBinaryOperators.GreaterThanOrEqualTo, containingSide, otherSide);
                case CliCommon.BinaryOperatorNames.Inequality:
                    return AstIdentifier.GetBinaryOperatorIdentifier(CoercibleBinaryOperators.IsNotEqualTo, containingSide, otherSide);
                case CliCommon.BinaryOperatorNames.LeftShift:
                    return AstIdentifier.GetBinaryOperatorIdentifier(CoercibleBinaryOperators.LeftShift, containingSide, otherSide);
                case CliCommon.BinaryOperatorNames.LessThan:
                    return AstIdentifier.GetBinaryOperatorIdentifier(CoercibleBinaryOperators.LessThan, containingSide, otherSide);
                case CliCommon.BinaryOperatorNames.LessThanOrEqual:
                    return AstIdentifier.GetBinaryOperatorIdentifier(CoercibleBinaryOperators.LessThanOrEqualTo, containingSide, otherSide);
                case CliCommon.BinaryOperatorNames.Modulus:
                    return AstIdentifier.GetBinaryOperatorIdentifier(CoercibleBinaryOperators.Modulus, containingSide, otherSide);
                case CliCommon.BinaryOperatorNames.Multiply:
                    return AstIdentifier.GetBinaryOperatorIdentifier(CoercibleBinaryOperators.Multiply, containingSide, otherSide);
                case CliCommon.BinaryOperatorNames.RightShift:
                    return AstIdentifier.GetBinaryOperatorIdentifier(CoercibleBinaryOperators.RightShift, containingSide, otherSide);
                case CliCommon.BinaryOperatorNames.Subtraction:
                    return AstIdentifier.GetBinaryOperatorIdentifier(CoercibleBinaryOperators.Subtract, containingSide, otherSide);
                default:
                    throw new InvalidOperationException();
            }
        }

        internal static ISignatureMemberUniqueIdentifier GetCtorIdentifier(ICliMetadataMethodDefinitionTableRow methodDef, IType owner, _ICliManager manager)
        {
            return AstIdentifier.GetCtorSignatureIdentifier((from p in methodDef.Signature.Parameters
                                                             select manager.ObtainTypeReference(p.ParameterType, owner, null)).SinglePass());
        }

        internal static ISignatureMemberUniqueIdentifier GetEventIdentifier(ICliMetadataEventTableRow eventDef, IType owner, _ICliManager manager)
        {
            return AstIdentifier.GetSignatureIdentifier(eventDef.Name, (from p in ((IDelegateType)manager.ObtainTypeReference(eventDef.SignatureType, owner, null)).Parameters.Values
                                                                        select p.ParameterType).SinglePass());
        }

        internal static IMemberUniqueIdentifier GetFieldIdentifier(ICliMetadataFieldTableRow fieldDef)
        {
            return AstIdentifier.GetMemberIdentifier(fieldDef.Name);
        }

        internal static IGeneralSignatureMemberUniqueIdentifier GetIndexerIdentifier(ICliMetadataPropertyTableRow indexerDef, IType owner, _ICliManager manager)
        {
            ICliMetadataMethodDefinitionTableRow targetMethod = null;
            bool knockOffLast = false;
            var getMethod = targetMethod = indexerDef.GetMethod;
            if (getMethod == null)
            {
                targetMethod = indexerDef.SetMethod;
                if (targetMethod == null)
                {
                    var semanticMethod = indexerDef.Methods.FirstOrDefault();
                    if (semanticMethod != null)
                        targetMethod = semanticMethod.Method;
                }
                else
                    knockOffLast = true;
            }
            if (targetMethod != null)
                return AstIdentifier.GetSignatureIdentifier(indexerDef.Name, (from p in (knockOffLast ? targetMethod.Signature.Parameters.Take(targetMethod.Signature.Parameters.Count - 1) : targetMethod.Signature.Parameters)
                                                                              select manager.ObtainTypeReference(p.ParameterType, owner, null)).SinglePass());
            else
                return AstIdentifier.GetSignatureIdentifier(indexerDef.Name);
        }

        internal static IGeneralGenericSignatureMemberUniqueIdentifier GetMethodIdentifier(ICliMetadataMethodDefinitionTableRow methodDef, IType owner, _ICliManager manager, Func<IMethodSignatureMember> memberGetter)
        {
            bool typeParamCheck = methodDef.MetadataRoot.TableStream.GenericParameterTable != null;
            if (typeParamCheck)
                return AstIdentifier.GetGenericSignatureIdentifier(methodDef.Name, methodDef.TypeParameters.Count, (from p in methodDef.Signature.Parameters
                                                                                                                    select manager.ObtainTypeReference(p.ParameterType, owner, memberGetter())).SinglePass());
            else
                return AstIdentifier.GetGenericSignatureIdentifier(methodDef.Name, 0, (from p in methodDef.Signature.Parameters
                                                                                       select manager.ObtainTypeReference(p.ParameterType, owner, memberGetter())).SinglePass());
        }


        internal static IMemberUniqueIdentifier GetPropertyIdentifier(ICliMetadataPropertyTableRow propertyDef)
        {
            return AstIdentifier.GetMemberIdentifier(propertyDef.Name);
        }

        internal static IGeneralMemberUniqueIdentifier GetTypeCoercionOperatorIdentifier(ICliMetadataMethodDefinitionTableRow methodDef, IType owner, _ICliManager manager)
        {
            var coercionParameter = methodDef.Signature.Parameters.FirstOrDefault();
            if (coercionParameter == null)
                return null;
            var coercionType = manager.ObtainTypeReference(coercionParameter.ParameterType, owner, null);
            var returnType = manager.ObtainTypeReference(methodDef.Signature.ReturnType, owner, null);
            if (coercionType == null || returnType == null)
                return null;
            var genericOwner = owner as IGenericType;
            TypeConversionRequirement requirement = (methodDef.Name == CliCommon.TypeCoercionNames.Implicit) ? TypeConversionRequirement.Implicit : (methodDef.Name == CliCommon.TypeCoercionNames.Explicit) ? TypeConversionRequirement.Explicit : TypeConversionRequirement.Unknown;
            if (genericOwner == null)
                goto nonGenericResolution;
            else
            {
                if (owner.IsGenericConstruct)
                {
                    if (TypesAreEquivalent(owner, coercionType))
                        return AstIdentifier.GetTypeOperatorFromIdentifier(requirement, returnType);
                    else if (TypesAreEquivalent(owner, returnType))
                        return AstIdentifier.GetTypeOperatorToIdentifier(requirement, coercionType);
                    else
                        goto nonGenericResolution;
                }
                else
                    goto nonGenericResolution;
            }
        nonGenericResolution:
            if (owner == coercionType)
                return AstIdentifier.GetTypeOperatorFromIdentifier(requirement, returnType);
            else if (owner == returnType)
                return AstIdentifier.GetTypeOperatorToIdentifier(requirement, coercionType);
            else
                return null;
        }

        private static bool TypesAreEquivalent(IType source, IType alternate)
        {
            if (source == alternate)
                return true;
            else if (source is IGenericType &&
                alternate is IGenericType)
            {
                var genericSource = (IGenericType)source;
                var genericAlternate = (IGenericType)alternate;
                if (genericSource.IsGenericConstruct && genericAlternate.IsGenericConstruct)
                {
                    if (genericAlternate.IsGenericDefinition ||
                        genericAlternate.ElementClassification != TypeElementClassification.GenericTypeDefinition)
                        return false;
                    if (genericAlternate.ElementType == genericSource)
                        return genericAlternate.GenericParameters.SequenceEqual(genericSource.GenericParameters);
                }
            }
            return false;
        }

        internal static IUnaryOperatorUniqueIdentifier GetUnaryOperatorIdentifier(ICliMetadataMethodDefinitionTableRow methodDef, IType owner, _ICliManager manager)
        {
            switch (methodDef.Name)
            {
                case CliCommon.UnaryOperatorNames.Decrement:
                    return AstIdentifier.GetUnaryOperatorIdentifier(CoercibleUnaryOperators.Decrement);
                case CliCommon.UnaryOperatorNames.False:
                    return AstIdentifier.GetUnaryOperatorIdentifier(CoercibleUnaryOperators.EvaluatesToFalse);
                case CliCommon.UnaryOperatorNames.Increment:
                    return AstIdentifier.GetUnaryOperatorIdentifier(CoercibleUnaryOperators.Increment);
                case CliCommon.UnaryOperatorNames.LogicalNot:
                    return AstIdentifier.GetUnaryOperatorIdentifier(CoercibleUnaryOperators.LogicalInvert);
                case CliCommon.UnaryOperatorNames.Negation:
                    return AstIdentifier.GetUnaryOperatorIdentifier(CoercibleUnaryOperators.Negation);
                case CliCommon.UnaryOperatorNames.OnesComplement:
                    return AstIdentifier.GetUnaryOperatorIdentifier(CoercibleUnaryOperators.Complement);
                case CliCommon.UnaryOperatorNames.Plus:
                    return AstIdentifier.GetUnaryOperatorIdentifier(CoercibleUnaryOperators.Plus);
                case CliCommon.UnaryOperatorNames.True:
                    return AstIdentifier.GetUnaryOperatorIdentifier(CoercibleUnaryOperators.EvaluatesToTrue);
                default:
                    throw new InvalidOperationException();
            }
        }

        internal static uint GetEventDelegateMethod(ICliMetadataEventTableRow metadataEntry, _ICliManager manager)
        {
            var signatureType = manager.ObtainTypeReference(metadataEntry.SignatureType);
            if (!(signatureType is IDelegateType))
                throw new BadImageFormatException("Event must reference a delegate type to properly function.");
            var delegateType = (ICliDelegateType)signatureType;
            return delegateType.InvokeMethodIndex;
        }
        internal static ICliMetadataRoot GetEventDelegateMetadataRoot(ICliMetadataEventTableRow metadataEntry, _ICliManager manager)
        {
            return manager.ResolveScope(metadataEntry.SignatureType).MetadataRoot;
        }

        internal static bool IsLastParams<TParent, TParameter>(this IParameterParent<TParent, TParameter> parent, ICliAssembly assembly, _ICliManager manager)
            where TParent :
                IParameterParent<TParent, TParameter>
            where TParameter :
                class,
                IParameterMember<TParent>
        {
            var @params = parent.Parameters;
            var lastParam = @params.Count == 0 ? (TParameter)null : @params[@params.Keys[@params.Count - 1]];
            if (lastParam == null)
                return false;
            return lastParam.IsDefined(manager.ObtainTypeReference(manager.RuntimeEnvironment.ParamArrayMetadatum));
        }

        internal static AccessLevelModifiers ObtainAccessLevelModifiers(this IEnumerable<ICliMetadataMethodDefinitionTableRow> methods)
        {
            AccessLevelModifiers resultModifiers = AccessLevelModifiers.PrivateScope;
            foreach (var method in methods)
            {
                AccessLevelModifiers currentModifiers;
                switch (method.UsageDetails.Accessibility)
                {
                    case MethodMemberAccessibility.Private:
                        currentModifiers = AccessLevelModifiers.Private;
                        break;
                    case MethodMemberAccessibility.FamilyAndAssembly:
                        currentModifiers = AccessLevelModifiers.ProtectedAndInternal;
                        break;
                    case MethodMemberAccessibility.Assembly:
                        currentModifiers = AccessLevelModifiers.Internal;
                        break;
                    case MethodMemberAccessibility.Family:
                        currentModifiers = AccessLevelModifiers.Protected;
                        break;
                    case MethodMemberAccessibility.FamilyOrAssembly:
                        currentModifiers = AccessLevelModifiers.ProtectedOrInternal;
                        break;
                    case MethodMemberAccessibility.Public:
                        currentModifiers = AccessLevelModifiers.Public;
                        break;
                    default:
                        currentModifiers = AccessLevelModifiers.PrivateScope;
                        break;
                }
                if (resultModifiers.CompareTo(currentModifiers) < 0)
                    resultModifiers = currentModifiers;
            }
            return resultModifiers;
        }

        internal static AccessLevelModifiers ObtainAccessLevelModifiers(this IEnumerable<ICliMetadataMethodSemanticsTableRow> methods)
        {
            return (from semantics in methods
                    select semantics.Method).ObtainAccessLevelModifiers();
        }
    }
}
