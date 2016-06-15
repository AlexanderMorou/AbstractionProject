using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Cli.Metadata
{
    public interface ICliMetadataMutableTableStreamAndHeader :
        ICliMetadataTableStreamAndHeader
    {
        /// <summary>
        /// Returns the <see cref="ICliMetadataModuleMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataModuleMutableTable ModuleTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataTypeRefMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataTypeRefMutableTable TypeRefTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataTypeDefinitionMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataTypeDefinitionMutableTable TypeDefinitionTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataFieldMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataFieldMutableTable FieldTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataMethodDefinitionMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataMethodDefinitionMutableTable MethodDefinitionTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataParameterMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataParameterMutableTable ParameterTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataInterfaceImplMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataInterfaceImplMutableTable InterfaceImplTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataMemberReferenceMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataMemberReferenceMutableTable MemberReferenceTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataConstantMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataConstantMutableTable ConstantTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataCustomAttributeMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataCustomAttributeMutableTable CustomAttributeTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataFieldMarshalMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataFieldMarshalMutableTable FieldMarshalTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataDeclSecurityMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataDeclSecurityMutableTable DeclSecurityTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataClassLayoutMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataClassLayoutMutableTable ClassLayoutTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataFieldLayoutMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataFieldLayoutMutableTable FieldLayoutTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataStandAloneSigMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataStandAloneSigMutableTable StandAloneSigTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataEventMapMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataEventMapMutableTable EventMapTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataEventMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataEventMutableTable EventTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataPropertyMapMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataPropertyMapMutableTable PropertyMapTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataPropertyMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataPropertyMutableTable PropertyTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataMethodSemanticsMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataMethodSemanticsMutableTable MethodSemanticsTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataMethodImplMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataMethodImplMutableTable MethodImplTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataModuleReferenceMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataModuleReferenceMutableTable ModuleReferenceTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataTypeSpecificationMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataTypeSpecificationMutableTable TypeSpecificationTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataImportMapMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataImportMapMutableTable ImportMapTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataFieldRVAMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataFieldRVAMutableTable FieldRVATable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataAssemblyMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataAssemblyMutableTable AssemblyTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataAssemblyProcessorMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataAssemblyProcessorMutableTable AssemblyProcessorTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataAssemblyOSMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataAssemblyOSMutableTable AssemblyOSTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataAssemblyRefMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataAssemblyRefMutableTable AssemblyRefTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataAssemblyRefProcessorMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataAssemblyRefProcessorMutableTable AssemblyRefProcessorTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataAssemblyRefOSMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataAssemblyRefOSMutableTable AssemblyRefOSTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataFileMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataFileMutableTable FileTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataExportedTypeMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataExportedTypeMutableTable ExportedTypeTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataManifestResourceMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataManifestResourceMutableTable ManifestResourceTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataNestedClassMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataNestedClassMutableTable NestedClassTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataGenericParameterMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataGenericParameterMutableTable GenericParameterTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataMethodSpecificationMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataMethodSpecificationMutableTable MethodSpecificationTable { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataGenericParamConstraintMutableTable"/> for the module.
        /// </summary>
        new ICliMetadataGenericParamConstraintMutableTable GenericParamConstraintTable { get; }

    }
}
