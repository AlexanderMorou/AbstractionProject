using System;
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

        public static IEnumerable<CliPropertyData> GetPropertyData(this ICliMetadataTypeDefinitionTableRow target)
        {
            var properties = target.Properties;
            if (properties.Count == 0)
                return new CliPropertyData[0];
            uint startIndex = properties[0].Index;
            uint endIndex = (uint) (startIndex + properties.Count - 1);
            var subSemantics = (from s in target.MetadataRoot.PropertySemantics
                                where ((s.AssociationIndex >= startIndex) && (s.AssociationIndex <= endIndex))
                                select s);
            return (from property in target.Properties
                    join semantics in subSemantics on property.Index equals semantics.AssociationIndex
                    join m in target.Methods on semantics.MethodIndex equals m.Index
                    group new { Method = m, Semantics = semantics } by property).Select(dataSet =>
                         {
                             ICliMetadataMethodDefinitionTableRow getter = null;
                             ICliMetadataMethodDefinitionTableRow setter = null;
                             IList<ICliMetadataMethodDefinitionTableRow> other = null;
                             foreach (var current in dataSet)
                                 switch (current.Semantics.Semantics)
                                 {
                                     case MethodSemanticsAttributes.Setter:
                                         setter = current.Method;
                                         break;
                                     case MethodSemanticsAttributes.Getter:
                                         getter = current.Method;
                                         break;
                                     case MethodSemanticsAttributes.Other:
                                         if (other == null)
                                             other = new List<ICliMetadataMethodDefinitionTableRow>();
                                         other.Add(current.Method);
                                         break;
                                 }
                             return new CliPropertyData(dataSet.Key, getter, setter, other == null ? new ICliMetadataMethodDefinitionTableRow[0] : other.ToArray());
                         }).OrderBy(p=>p.TokenIndex);
        }

        public static IEnumerable<CliEventData> GetEventData(this ICliMetadataTypeDefinitionTableRow target)
        {
            var events = target.Events;
            if (events.Count == 0)
                return new CliEventData[0];
            uint startIndex = events[0].Index;
            uint endIndex = (uint) (startIndex + events.Count - 1);
            var subSemantics = (from s in target.MetadataRoot.EventSemantics
                                where ((s.AssociationIndex >= startIndex) && (s.AssociationIndex <= endIndex))
                                select s);
            return (from @event in target.Events
                    join semantics in subSemantics on @event.Index equals semantics.AssociationIndex
                    join m in target.Methods on semantics.MethodIndex equals m.Index
                    group new { Method = m, Semantics = semantics } by @event).Select(dataSet =>
                    {
                        ICliMetadataMethodDefinitionTableRow onAddMethod = null;
                        ICliMetadataMethodDefinitionTableRow onFireMethod = null;
                        ICliMetadataMethodDefinitionTableRow onRemoveMethod = null;
                        IList<ICliMetadataMethodDefinitionTableRow> other = null;
                        foreach (var current in dataSet)
                            switch (current.Semantics.Semantics)
                            {
                                case MethodSemanticsAttributes.AddOn:
                                    onAddMethod = current.Method;
                                    break;
                                case MethodSemanticsAttributes.Fire:
                                    onFireMethod = current.Method;
                                    break;
                                case MethodSemanticsAttributes.RemoveOn:
                                    onRemoveMethod = current.Method;
                                    break;
                                case MethodSemanticsAttributes.Other:
                                    if (other == null)
                                        other = new List<ICliMetadataMethodDefinitionTableRow>();
                                    other.Add(current.Method);
                                    break;
                            }
                        return new CliEventData(dataSet.Key, onAddMethod, onFireMethod, onRemoveMethod, other == null ? new ICliMetadataMethodDefinitionTableRow[0] : other.ToArray());
                    }).OrderBy(p => p.TokenIndex);
        }

        public static IEnumerable<ICliMemberData> GetMemberData(this ICliMetadataTypeDefinitionTableRow target)
        {
            CliPropertyData[] propertyData = null;
            var properties = target.Properties;
            if (properties.Count == 0)
                propertyData = new CliPropertyData[0];
            else
            {
                uint startIndex = properties[0].Index;
                uint endIndex = (uint) (startIndex + properties.Count - 1);
                var subSemantics = (from s in target.MetadataRoot.PropertySemantics
                                    where ((s.AssociationIndex >= startIndex) && (s.AssociationIndex <= endIndex))
                                    select s);
                propertyData = (from property in target.Properties
                                join semantics in subSemantics on property.Index equals semantics.AssociationIndex
                                join m in target.Methods on semantics.MethodIndex equals m.Index
                                group new { Method = m, Semantics = semantics } by property).Select(dataSet =>
                        {
                            ICliMetadataMethodDefinitionTableRow getter = null;
                            ICliMetadataMethodDefinitionTableRow setter = null;
                            IList<ICliMetadataMethodDefinitionTableRow> other = null;
                            foreach (var current in dataSet)
                                switch (current.Semantics.Semantics)
                                {
                                    case MethodSemanticsAttributes.Setter:
                                        setter = current.Method;
                                        break;
                                    case MethodSemanticsAttributes.Getter:
                                        getter = current.Method;
                                        break;
                                    case MethodSemanticsAttributes.Other:
                                        if (other == null)
                                            other = new List<ICliMetadataMethodDefinitionTableRow>();
                                        other.Add(current.Method);
                                        break;
                                }
                            return new CliPropertyData(dataSet.Key, getter, setter, other == null ? new ICliMetadataMethodDefinitionTableRow[0] : other.ToArray());
                        }).ToArray();
            }
            CliEventData[] @eventData = null;
            var events = target.Properties;
            if (events.Count == 0)
                @eventData = new CliEventData[0];
            else
            {
                uint startIndex = events[0].Index;
                uint endIndex = (uint) (startIndex + events.Count - 1);
                var subSemantics = (from s in target.MetadataRoot.EventSemantics
                                    where ((s.AssociationIndex >= startIndex) && (s.AssociationIndex <= endIndex))
                                    select s);
                @eventData = (from @event in target.Events
                              join semantics in subSemantics on @event.Index equals semantics.AssociationIndex
                              join m in target.Methods on semantics.MethodIndex equals m.Index
                              group new { Method = m, Semantics = semantics } by @event).Select(dataSet =>
                              {
                                  ICliMetadataMethodDefinitionTableRow onAddMethod = null;
                                  ICliMetadataMethodDefinitionTableRow onFireMethod = null;
                                  ICliMetadataMethodDefinitionTableRow onRemoveMethod = null;
                                  IList<ICliMetadataMethodDefinitionTableRow> other = null;
                                  foreach (var current in dataSet)
                                      switch (current.Semantics.Semantics)
                                      {
                                          case MethodSemanticsAttributes.AddOn:
                                              onAddMethod = current.Method;
                                              break;
                                          case MethodSemanticsAttributes.Fire:
                                              onFireMethod = current.Method;
                                              break;
                                          case MethodSemanticsAttributes.RemoveOn:
                                              onRemoveMethod = current.Method;
                                              break;
                                          case MethodSemanticsAttributes.Other:
                                              if (other == null)
                                                  other = new List<ICliMetadataMethodDefinitionTableRow>();
                                              other.Add(current.Method);
                                              break;
                                      }
                                  return new CliEventData(dataSet.Key, onAddMethod, onFireMethod, onRemoveMethod, other == null ? new ICliMetadataMethodDefinitionTableRow[0] : other.ToArray());
                              }).ToArray();
            }
            var remainingMethods = (from m in
                                        (target.Methods.Except(from m in target.Methods
                                                               join semantics in
                                                                   (from s in target.MetadataRoot.TableStream.MethodSemanticsTable
                                                                    where s.MethodIndex >= target.MethodStartIndex && s.MethodIndex <= target.MethodStartIndex + target.Methods.Count - 1
                                                                    select s) on m.Index equals semantics.MethodIndex
                                                               select m))
                                    select new CliMethodTypeData(DiscernMemberType(m), m)).ToArray();

            return from m in propertyData.Concat<ICliMemberData>(eventData).Concat(remainingMethods)
                       orderby m.TokenIndex
                   select m;
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
