using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Utilities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CliManager
    {
        internal class MetadataService :
            IIdentityMetadataService
        {
            private CliManager provider;

            public MetadataService(CliManager provider) 
            {
                this.provider = provider;
            }

            /// <summary>
            /// Returns the <see cref="IIdentityManager"/> associated to the
            /// <see cref="IIdentityMetadataService"/>.
            /// </summary>
            public IIdentityManager IdentityManager
            {
                get { return this.provider; }
            }

            /// <summary>
            /// Returns whether the <paramref name="metadatumType"/>
            /// is inheritable through the type hierarchy.
            /// </summary>
            /// <param name="metadatumType">The <see cref="IType"/>
            /// to check inheritability as a metadatumType.</param>
            /// <returns>true if the <paramref name="metadatumType"/>
            /// is inheritable through the type hierarchy; false, otherwise.
            /// </returns>
            public bool IsMetadatumInheritable(IType metadatumType)
            {
                return GetMetadatumInfo(metadatumType).Inheritable;
            }

            /// <summary>
            /// Returns whether the <paramref name="metadatumType"/>
            /// can be repeated on the target multiple times.
            /// </summary>
            /// <param name="metadatumType">The <see cref="IType"/> to check 
            /// for repeatability as a metadatumType.</param>
            /// <returns>true, if the <paramref name="metadatumType"/> is able to be repeated
            /// on the target; false, otherwise.</returns>
            public bool IsMetadatumRepeatable(IType metadatumType)
            {
                return GetMetadatumInfo(metadatumType).Repeatable;
            }

            /// <summary>
            /// Returns whether the <paramref name="metadatumType"/> is actually
            /// a metadatumType, or whether it groups metadata since the type is an 
            /// interface.
            /// </summary>
            /// <param name="metadatumType">The <see cref="IType"/>
            /// to check for metadatumType status.</param>
            /// <returns>A <see cref="TypeMetadatumRepresentation"/> instance which denotes
            /// the <paramref name="metadatumType"/>'s validity as proper metadata, whether
            /// it's purely an interface, or both.</returns>
            /// <remarks>Depending on the underlying infrastructure,
            /// the type may be an interface and a metadatumType type.</remarks>
            public TypeMetadatumRepresentation GetTypeMetadatumRepresentation(IType metadatumType)
            {
                if (metadatumType == null)
                    throw new ArgumentNullException("metadatumType");
                var metadataIdentifier = this.provider.RuntimeEnvironment.GetCoreIdentifier(CliRuntimeCoreType.RootMetadatum);
                IType rootMetadatum;
                var assembly = metadatumType.Assembly;
                rootMetadatum = this.provider.ObtainTypeReference(this.provider.RuntimeEnvironment.GetCoreIdentifier(CliRuntimeCoreType.RootMetadatum), assembly);
                if (!rootMetadatum.IsAssignableFrom(metadatumType))
                {
                    if (metadatumType.Type == TypeKind.Interface)
                        return TypeMetadatumRepresentation.GroupsMetadata;
                    return TypeMetadatumRepresentation.IsNotMetadata;
                }
                else
                    return TypeMetadatumRepresentation.IsMetadata;
            }

            /// <summary>
            /// Returns a <see cref="MetadatumInfo"/> structure which denotes whether
            /// the <paramref name="metadatumType"/> is valid for metadata,
            /// whether it is inheritable through the type hierarchy, and whether
            /// multiple instances can be present on a target.
            /// </summary>
            /// <param name="metadatumType">The <see cref="IType"/>
            /// to check for metadatumType status, inheritability, and repeatability.</param>
            /// <returns>A <see cref="MetadatumInfo"/> instance which denotes
            /// the <paramref name="metadatumType"/>'s validity as proper metadata, inheritability
            /// of the metadata, and whether it can be repeated.</returns>
            /// <remarks>Depending on the underlying infrastructure,
            /// the type may be an interface and a metadatumType type.</remarks>
            public MetadatumInfo GetMetadatumInfo(IType metadatumType)
            {
                TypeMetadatumRepresentation isMetadata = GetTypeMetadatumRepresentation(metadatumType);
                if (isMetadata == TypeMetadatumRepresentation.IsNotMetadata)
                    return new MetadatumInfo(isMetadata, false, false, MetadatumTargets.Class);
                var assembly = metadatumType.Assembly;
                var attributeUsageMetadatumType = this.provider.ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System", "AttributeUsageAttribute"), assembly);
                if (metadatumType.IsDefined(attributeUsageMetadatumType))
                {
                    var attrUsageMetadatum = metadatumType.Metadata[attributeUsageMetadatumType];
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

            public IIdentityManager Provider
            {
                get { return this.provider; }
            }

            IServiceProvider<IIdentityService> IService<IIdentityService>.Provider
            {
                get { return this.Provider; }
            }

            public Guid ServiceGuid
            {
                get { return IdentityServiceGuids.MetadatumService; }
            }
        }
    }
}
