using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    /// <summary>
    /// Defines a series of fields which represent the table specifier for a given 
    /// encoded index of a type reference, specification, or definition.
    /// </summary>
    public enum CliMetadataTypeDefOrRefTag
    {
        /// <summary>
        /// Denotes that the reference is a type definition.
        /// </summary>
        TypeDefinition = 0x0,
        /// <summary>
        /// Denotes that the reference is a type reference into
        /// another assembly.
        /// </summary>
        TypeReference = 0x1,
        /// <summary>
        /// Denotes that the reference is a constructed type which
        /// is an array of another type, a generic instance, or other
        /// variation.
        /// </summary>
        TypeSpecification = 0x2,
        /// <summary>
        /// Denotes the number of bits required to encode the table specifier of the
        /// <see cref="CliMetadataTypeDefOrRefTag"/>.
        /// </summary>
        ShiftSize = 0x2,
    }
    /// <summary>
    /// Defines a series of fields which represent the table specifier for a given
    /// encoded index of a constant.
    /// </summary>
    public enum CliMetadataHasConstantTag
    {
        /// <summary>
        /// Denotes that the member containing the constant is a
        /// field.
        /// </summary>
        Field,
        /// <summary>
        /// Denotes that the member containing the constant is a 
        /// parameter.
        /// </summary>
        Param,
        /// <summary>
        /// Denotes that the member containing the constant is a 
        /// property.
        /// </summary>
        Property,
        /// <summary>
        /// Denotes the number of bits required to encode the table specifier of the
        /// <see cref="CliMetadataHasConstantTag"/>.
        /// </summary>
        ShiftSize = 0x2,
    }
    /// <summary>
    /// Defines a series of fields which represent the table specifier for a given
    /// encoded index of a custom attribute.
    /// </summary>
    public enum CliMetadataHasCustomAttributeTag
    {
        /// <summary>
        /// Denotes that the custom attribute is for a method definition.
        /// </summary>
        MethodDefinition,
        /// <summary>
        /// Denotes that the custom attribute is for a field.
        /// </summary>
        Field,
        /// <summary>
        /// Denotes that the custom attribute is for a type reference.
        /// </summary>
        TypeReference,
        /// <summary>
        /// Denotes that the custom attribute is for a type definition.
        /// </summary>
        TypeDefinition,
        /// <summary>
        /// Denotes that the custom attribute is for a parameter.
        /// </summary>
        Parameter,
        /// <summary>
        /// Denotes that the custom attribute is for a interface implementation reference.
        /// </summary>
        InterfaceImpl,
        /// <summary>
        /// Denotes that the custom attribute is for a member reference.
        /// </summary>
        MemberRef,
        /// <summary>
        /// Denotes that the custom attribute is for a module definition.
        /// </summary>
        Module,
        /// <summary>
        /// Denotes that the custom attribute is for a permission.
        /// </summary>
        Permission,
        /// <summary>
        /// Denotes that the custom attribute is for a property.
        /// </summary>
        Property,
        /// <summary>
        /// Denotes that the custom attribute is for an event.
        /// </summary>
        Event,
        /// <summary>
        /// Denotes that the custom attribute is for a stand alone signature.
        /// </summary>
        StandAloneSig,
        /// <summary>
        /// Denotes that the custom attribute is for a module reference.
        /// </summary>
        ModuleReference,
        /// <summary>
        /// Denotes that the custom attribute is for a type specification.
        /// </summary>
        TypeSpecification,
        /// <summary>
        /// Denotes that the custom attribute is for an assembly.
        /// </summary>
        Assembly,
        /// <summary>
        /// Denotes that the custom attribute is for an assembly reference.
        /// </summary>
        AssemblyReference,
        /// <summary>
        /// Denotes that the custom attribute is for a file.
        /// </summary>
        File,
        /// <summary>
        /// Denotes that the custom attribute is for an exported type.
        /// </summary>
        ExportedType,
        /// <summary>
        /// Denotes that the custom attribute is for a manifest resource.
        /// </summary>
        ManifestResource,
        /// <summary>
        /// Denotes that the custom attribute is for a generic parameter.
        /// </summary>
        GenericParam,
        /// <summary>
        /// Denotes that the custom attribute is for a generic paramter's constraint.
        /// </summary>
        GenericParamConstraint,
        /// <summary>
        /// Denotes that the custom attribute is for a method specification.
        /// </summary>
        MethodSpec,
        /// <summary>
        /// Denotes the number of bits required to encode the table specifier of the
        /// <see cref="CliMetadataHasCustomAttributeTag"/>.
        /// </summary>
        ShiftSize = 0x5,
    }
    /// <summary>
    /// 
    /// </summary>
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
