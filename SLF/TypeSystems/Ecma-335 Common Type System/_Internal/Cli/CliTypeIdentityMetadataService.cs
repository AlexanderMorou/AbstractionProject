using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CliManager
    {
        internal class MetadataService :
            ITypeIdentityMetadataService
        {
            private CliManager manager;

            public MetadataService(CliManager manager) { this.manager = manager; }

            public ITypeIdentityManager IdentityManager
            {
                get { return this.manager; }
            }

            public bool IsMetadatumInheritable(IType metadatum)
            {
                return GetMetadatumInfo(metadatum).Inheritable;
            }

            public bool IsMetadatumRepeatable(IType metadatum)
            {
                return GetMetadatumInfo(metadatum).Repeatable;
            }

            public TypeIsMetadata IsMetadatum(IType metadatum)
            {
                if (metadatum == null)
                    throw new ArgumentNullException("metadatum");
                var metadataIdentifier = this.manager.RuntimeEnvironment.GetCoreIdentifier(CliRuntimeCoreType.RootMetadatum);
                IType rootMetadatum;
                var assembly = metadatum.Assembly;
                rootMetadatum = this.manager.ObtainTypeReference(this.manager.RuntimeEnvironment.GetCoreIdentifier(CliRuntimeCoreType.RootMetadatum), assembly);
                if (!rootMetadatum.IsAssignableFrom(metadatum))
                {
                    if (metadatum.Type == TypeKind.Interface)
                        return TypeIsMetadata.Interface;
                    return TypeIsMetadata.No;
                }
                else
                    return TypeIsMetadata.Yes;
            }

            public MetadatumInfo GetMetadatumInfo(IType metadatum)
            {
                TypeIsMetadata isMetadata = IsMetadatum(metadatum);
                if (isMetadata == TypeIsMetadata.No)
                    return new MetadatumInfo(isMetadata, false, false, MetadatumTargets.Class);
                var assembly = metadatum.Assembly;
                var attributeUsageMetadatumType = this.manager.ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System", "AttributeUsageAttribute"), assembly);
                if (metadatum.IsDefined(attributeUsageMetadatumType))
                {
                    var attrUsageMetadatum = metadatum.Metadata[attributeUsageMetadatumType];
                    var validOn = attrUsageMetadatum.Parameters.FirstOrDefault();
                    AttributeTargets targets = AttributeTargets.Class;
                    if (validOn != null)
                        targets = (AttributeTargets)(int)validOn.Item2;
                    var inheritableParameter = (from n in attrUsageMetadatum.NamedParameters
                                                where n.Item2 == "Inherited"
                                                select n).FirstOrDefault();
                    var allowMultiple = (from n in attrUsageMetadatum.NamedParameters
                                                where n.Item2 == "AllowMultiple"
                                                select n).FirstOrDefault();
                    
                    return new MetadatumInfo(isMetadata, allowMultiple == null ? false : (bool)allowMultiple.Item3, inheritableParameter == null ? true : (bool)inheritableParameter.Item3, TransposeTargets(targets));
                }
                else
                    return new MetadatumInfo(isMetadata, false, true, MetadatumTargets.Class);
            }

            private static MetadatumTargets TransposeTargets(AttributeTargets targets)
            {
                MetadatumTargets result = MetadatumTargets.None;
                if ((targets & AttributeTargets.Assembly) == AttributeTargets.Assembly)
                    result |= MetadatumTargets.Assembly;
                if ((targets & AttributeTargets.Class) == AttributeTargets.Class)
                    result |= MetadatumTargets.Class;
                if ((targets & AttributeTargets.Constructor) == AttributeTargets.Constructor)
                    result |= MetadatumTargets.Constructor;
                if ((targets & AttributeTargets.Delegate) == AttributeTargets.Delegate)
                    result |= MetadatumTargets.Delegate;
                if ((targets & AttributeTargets.Enum) == AttributeTargets.Enum)
                    result |= MetadatumTargets.Enum;
                if ((targets & AttributeTargets.Event) == AttributeTargets.Event)
                    result |= MetadatumTargets.Event;
                if ((targets & AttributeTargets.Field) == AttributeTargets.Field)
                    result |= MetadatumTargets.Field;
                if ((targets & AttributeTargets.GenericParameter) == AttributeTargets.GenericParameter)
                    result |= MetadatumTargets.GenericParameter;
                if ((targets & AttributeTargets.Interface) == AttributeTargets.Interface)
                    result |= MetadatumTargets.Interface;
                if ((targets & AttributeTargets.Method) == AttributeTargets.Method)
                    result |= MetadatumTargets.Method;
                if ((targets & AttributeTargets.Module) == AttributeTargets.Module)
                    result |= MetadatumTargets.Module;
                if ((targets & AttributeTargets.Parameter) == AttributeTargets.Parameter)
                    result |= MetadatumTargets.Parameter;
                if ((targets & AttributeTargets.Property) == AttributeTargets.Property)
                    result |= MetadatumTargets.Property | MetadatumTargets.Indexer;
                if ((targets & AttributeTargets.ReturnValue) == AttributeTargets.ReturnValue)
                    result |= MetadatumTargets.ReturnValue;
                if ((targets & AttributeTargets.Struct) == AttributeTargets.Struct)
                    result |= MetadatumTargets.Struct;
                return result;
            }
        }
    }
}
