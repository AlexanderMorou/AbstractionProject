using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    public enum CliMetadataTypeDefOrRefTag
    {
        TypeDefinition = 0x0,
        TypeReference = 0x1,
        TypeSpecification = 0x2,
        ShiftSize = 0x2,
    }
    public enum CliMetadataHasConstantTag
    {
        Field,
        Param,
        Property,
        ShiftSize = 0x2,
    }
    public enum CliMetadataHasCustomAttributeTag
    {
        MethodDefinition,
        Field,
        TypeReference,
        TypeDefinition,
        Parameter,
        InterfaceImpl,
        MemberRef,
        Module,
        Permission,
        Property,
        Event,
        StandAloneSig,
        ModuleReference,
        TypeSpecification,
        Assembly,
        AssemblyReference,
        File,
        ExportedType,
        ManifestResource,
        GenericParam,
        GenericParamConstraint,
        MethodSpec,
        ShiftSize = 0x5,
    }

    public enum CliMetadataHasFieldMarshalTag
    {
        Field,
        Param,
        ShiftSize = 0x2,
    }

    public enum CliMetadataHasDeclSecurityTag
    {
        TypeDefinition,
        MethodDefinition,
        Assembly,
        ShiftSize = 0x2,
    }
    public enum CliMetadataMemberRefParentTag
    {
        TypeDefinition,
        TypeReference,
        ModuleReference,
        MethodDefinition,
        TypeSpecification,
        ShiftSize = 0x3,
    }

    public enum CliMetadataHasSemanticsTag
    {
        Event,
        Property,
        ShiftSize = 0x1,
    }

    public enum CliMetadataMethodDefOrRefTag
    {
        MethodDefinition,
        MemberRef,
        ShiftSize = 0x1,
    }
    public enum CliMetadataMemberForwardedTag
    {
        Field,
        MethodDefinition,
        ShiftSize = 0x1,
    }

    public enum CliMetadataImplementationTag
    {
        File,
        AssemblyReference,
        ExportedType,
        ShiftSize = 0x2,
    }
    public enum CliMetadataCustomAttributeTypeTag
    {
        NotUsed1,
        NotUsed2,
        MethodDefinition,
        MemberReference,
        NotUsed3,
        ShiftSize = 0x3,
    }
    public enum CliMetadataResolutionScopeTag
    {
        Module,
        ModuleReference,
        AssemblyReference,
        TypeReference,
        ShiftSize = 0x2,
    }

    public enum CliMetadataTypeOrMethodDef
    {
        TypeDefinition,
        MethodDefinition,
        ShiftSize = 0x1,
    }
}
