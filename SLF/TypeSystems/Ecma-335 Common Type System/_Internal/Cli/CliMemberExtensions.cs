﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Members;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using System.Diagnostics;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal static class CliMemberExtensions
    {
        public static IEnumerable<Tuple<CliMemberType, ICliMetadataTableRow>> GetMemberData(this ICliMetadataTypeDefinitionTableRow target)
        {
            IEnumerable<Tuple<CliMemberType, uint, ICliMetadataTableRow>> propertyData = null;
            List<uint> fullSemantics = new List<uint>();
            /* *
             * Build our own list of methods tied to semantics,
             * this is about 3 times faster than querying the full
             * set each time, but uses a little more memory.
             * */
            uint fieldCount = target.Fields == null ? 0 : (uint) target.Fields.Count;
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
                uint endIndex = (uint) (startIndex + properties.Count - 1);
                var subSemantics = (from s in target.MetadataRoot.PropertySemantics
                                    where ((startIndex <= s.AssociationIndex) && (s.AssociationIndex <= endIndex))
                                    select s).ToArray();
                fullSemantics.AddRange(subSemantics.Select(s => s.MethodIndex));
                propertyData = (from property in target.Properties
                                join semantics in subSemantics on property.Index equals semantics.AssociationIndex into methods
                                select new Tuple<CliMemberType, uint, ICliMetadataTableRow>(CliMemberType.Property, methods.Min() + fieldCount, property));
            }
            IEnumerable<Tuple<CliMemberType, uint, ICliMetadataTableRow>> @eventData = null;
            var events = target.Events;
            if (events == null || events.Count == 0)
                @eventData = new Tuple<CliMemberType, uint, ICliMetadataTableRow>[0];
            else
            {
                uint startIndex = events[0].Index;
                uint endIndex = (uint) (startIndex + events.Count - 1);
                var subSemantics = (from s in target.MetadataRoot.EventSemantics
                                    where ((startIndex <= s.AssociationIndex) && (s.AssociationIndex <= endIndex))
                                    select s);
                fullSemantics.AddRange(subSemantics.Select(s => s.MethodIndex));
                eventData = (from @event in target.Events
                             join semantics in subSemantics on @event.Index equals semantics.AssociationIndex into methods
                             select new Tuple<CliMemberType, uint, ICliMetadataTableRow>(CliMemberType.Event, methods.Min() + fieldCount, @event));
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
                                        (target.Methods.Except(from m in target.Methods
                                                               join semantics in fullSemantics on m.Index equals semantics
                                                               select m))
                                    select new Tuple<CliMemberType, uint, ICliMetadataTableRow>(DiscernMemberType(m), m.Index + fieldCount, m));
            /* *
             * Yield a full set of members, remembering their kind, and ordering them 
             * by a close approximate of their original order.
             * */
            return from m in
                       propertyData.Concat(eventData).Concat(remainingMethods).Concat(
                           from f in target.Fields
                           select new Tuple<CliMemberType, uint, ICliMetadataTableRow>(CliMemberType.Field, f.Index, f))
                   orderby m.Item2
                   select new Tuple<CliMemberType, ICliMetadataTableRow>(m.Item1, m.Item3);
        }

        private static uint Min(this IEnumerable<ICliMetadataMethodSemanticsTableRow> target)
        {
            uint result = 0;
            bool first=true;
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

        private static CliMemberType DiscernMemberType(ICliMetadataMethodDefinitionTableRow method)
        {
            if ((method.Flags & (MethodAttributes.SpecialName | MethodAttributes.RTSpecialName)) == (MethodAttributes.SpecialName | MethodAttributes.RTSpecialName))
            {
                if (method.Name == ".ctor" && (method.Flags & MethodAttributes.Static) != MethodAttributes.Static)
                    return CliMemberType.Constructor;
                else if (method.Name == ".cctor" && (method.Flags & MethodAttributes.Static) == MethodAttributes.Static)
                    return CliMemberType.Constructor;
            }
            else if ((method.Flags & (MethodAttributes.SpecialName | MethodAttributes.Static)) == (MethodAttributes.SpecialName | MethodAttributes.Static))
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

    }
}