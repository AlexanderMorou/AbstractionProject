using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables
{
    /// <summary>
    /// Defines the kinds of tables within CLI metadata.
    /// </summary>
    /// <remarks>
    /// <list type="definition">
    /// <item><term><see cref="CliMetadataTableKinds.Assembly"/></term><description>
    /// Metadata Validation Rules (as per ECMA-335 22.2):
    /// <list type="table">
    /// <listheader><term>Category</term>
    /// <description>Description</description>
    /// </listheader>
    /// <item><term><see cref="MetadataValidationRuleCategory.Error"/>
    /// </term><description>Assembly shall contain zero or one row.
    /// </description></item>
    /// <item><term><see cref="MetadataValidationRuleCategory.Error"/>
    /// </term><description>
    /// <see cref="ICliMetadataAssemblyTableRow.HashAlgorithmIdentifier"/> shall
    /// be one of the specified values within
    /// <see cref="AssemblyHashAlgorithm"/>.
    /// </description></item>
    /// <item><term><see cref="MetadataValidationRuleCategory.None"/>
    /// </term><description><see cref="ICliMetadataAssemblyTableRow.MajorVersion"/>, 
    /// <see cref="ICliMetadataAssemblyTableRow.MinorVersion"/> and 
    /// <see cref="ICliMetadataAssemblyTableRow.Revision"/> can each have any value.
    /// </description></item>
    /// <item><term><see cref="MetadataValidationRuleCategory.Error"/>
    /// </term><description><see cref="ICliMetadataAssemblyTableRow.Flags"/>
    /// shall have only those values as defined in
    /// <see cref="CliMetadataAssemblyFlags"/>.
    /// </description></item>
    /// <item><term><see cref="MetadataValidationRuleCategory.Error"/>
    /// </term><description><see cref="ICliMetadataAssemblyTableRow.NameIndex"/>
    /// shall index a non-empty string in the String heap.
    /// </description></item>
    /// <item><term><see cref="MetadataValidationRuleCategory.None"/>
    /// </term><description>The string indexed by <see cref="ICliMetadataAssemblyTableRow.NameIndex"/>
    /// can be of unlimited length.</description></item>
    /// <item><term><see cref="MetadataValidationRuleCategory.None"/>
    /// </term><description><see cref="ICliMetadataAssemblyTableRow.CultureIndex"/>
    /// can be null or non-null.</description></item>
    /// <item><term><see cref="MetadataValidationRuleCategory.Error"/>
    /// </term><description>If <see cref="ICliMetadataAssemblyTableRow.CultureIndex"/>
    /// is non-null it shall index a string from <see cref="ValidCultureIdentifiers"/>.</description></item>
    /// </list></description></item>
    /// </list>
    /// </remarks>
    [Flags]
    public enum CliMetadataTableKinds :
        ulong
    {
        /// <summary>
        /// The assembly table contains zero or one row describing 
        /// meta-data about the active assembly.
        /// </summary>
        Assembly                        = 1UL << 0x20,
        /// <summary>
        /// The AssemblyOS describes information about the operating system
        /// on which the program originated.
        /// </summary>
        /// <remarks>
        /// <para>This record should not be emitted into any PE File.
        /// However, if present in a PE file, it shall be treated
        /// as if all of its fields were zero.</para>
        /// <para>It shall be ignored by the CLI</para>
        /// <para></para></remarks>
        AssemblyOS                      = 1UL << 0x22,
        /// <summary>
        /// The AssemblyProcessor provides a constant relative to the 
        /// processor that the assembly targets.
        /// </summary>
        /// <remarks>
        /// <para>This record should not be emitted into any PE File.
        /// However, if present in a PE file, it shall be treated
        /// as if all of its fields were zero.</para>
        /// <para>It shall be ignored by the CLI</para>
        /// <para></para></remarks>
        AssemblyProcessor               = 1UL << 0x21,
        /// <summary>
        /// The assembly table contains zero or one row describing 
        /// meta-data about the active assembly.
        /// </summary>
        /// <remarks>Metadata Validation Rules (as per ECMA-335 22.2):
        /// <list type="table">
        /// <listheader><term>Category</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item><term><see cref="MetadataValidationRuleCategory.Error"/>
        /// </term><description>
        /// <see cref="ICliMetadataAssemblyRefTableRow.HashAlgorithmIdentifier"/> shall
        /// be one of the specified values within
        /// <see cref="AssemblyHashAlgorithm"/>.
        /// </description></item>
        /// <item><term><see cref="MetadataValidationRuleCategory.None"/>
        /// </term><description><see cref="ICliMetadataAssemblyRefTableRow.MajorVersion"/>, 
        /// <see cref="ICliMetadataAssemblyRefTableRow.MinorVersion"/> and 
        /// <see cref="ICliMetadataAssemblyRefTableRow.Revision"/> can each have any value.
        /// </description></item>
        /// <item><term><see cref="MetadataValidationRuleCategory.Error"/>
        /// </term><description><see cref="ICliMetadataAssemblyRefTableRow.Flags"/>
        /// shall have only those values as defined in
        /// <see cref="CliMetadataAssemblyFlags"/>.
        /// </description></item>
        /// <item><term><see cref="MetadataValidationRuleCategory.Error"/>
        /// </term><description><see cref="ICliMetadataAssemblyRefTableRow.NameIndex"/>
        /// shall index a non-empty string in the String heap.
        /// </description></item>
        /// <item><term><see cref="MetadataValidationRuleCategory.None"/>
        /// </term><description>The string indexed by <see cref="ICliMetadataAssemblyRefTableRow.NameIndex"/>
        /// can be of unlimited length.</description></item>
        /// <item><term><see cref="MetadataValidationRuleCategory.None"/>
        /// </term><description><see cref="ICliMetadataAssemblyRefTableRow.CultureIndex"/>
        /// can be null or non-null.</description></item>
        /// <item><term><see cref="MetadataValidationRuleCategory.Error"/>
        /// </term><description>If <see cref="ICliMetadataAssemblyRefTableRow.CultureIndex"/>
        /// is non-null it shall index a string from <see cref="ValidCultureIdentifiers"/>.
        /// </description></item>
        /// </list></remarks>
        AssemblyReference               = 1UL << 0x23,
        /// <summary>
        /// The AssemblyReferenceOS describes information about the
        /// operating system on which the reference program originated.
        /// </summary>
        /// <remarks>
        /// <para>This record should not be emitted into any PE File.
        /// However, if present in a PE file, it shall be treated
        /// as if all of its fields were zero.</para>
        /// <para>It shall be ignored by the CLI</para>
        /// <para></para></remarks>
        AssemblyReferenceOS             = 1UL << 0x25,
        /// <summary>
        /// The AssemblyReferenceProcessor provides a constant relative
        /// to the processor that the reference assembly targets.
        /// </summary>
        /// <remarks>
        /// <para>This record should not be emitted into any PE File.
        /// However, if present in a PE file, it shall be treated
        /// as if all of its fields were zero.</para>
        /// <para>It shall be ignored by the CLI</para>
        /// <para></para></remarks>
        AssemblyReferenceProcessor      = 1UL << 0x24,
        /// <summary>
        /// ClassLayout describes the memory footprint and packing
        /// size associated to a given reference or value type.
        /// </summary>
        /// <remarks><para>
        /// Metadata Validation Rules (as per ECMA-335 22.8):</para>
        /// <list type="table">
        /// <listheader><term>Category</term><description>
        /// Description</description></listheader>
        /// <item><term><see cref="MetadataValidationRuleCategory.None"/></term>
        /// <description><para>The <see cref="ICliMetadataClassLayoutTable"/>
        /// can contain zero or more rows.</para>
        /// <para>A class size of zero indicates that the original declaration
        /// of the class contained no size information, this is valid and means
        /// the CLI shall calculate the proper size based off of the
        /// type's field types, while taking into account packing size.</para></description></item>
        /// <item><term><see cref="MetadataValidationRuleCategory.Error"/></term>
        /// <description><see cref="ICliMetadataClassLayoutTableRow.ParentIndex"/> shall
        /// reference a valid row in the <see cref="ICliMetadataClassLayoutTable"/>,
        /// corresponding to a class or valuetype.</description></item>
        /// <item><term><see cref="MetadataValidationRuleCategory.Error"/></term>
        /// <description><para>The Class or ValueType indexed by 
        /// <see cref="ICliMetadataClassLayoutTableRow.ParentIndex"/> shall
        /// have a layout kind that is either 
        /// <see cref="LayoutKind.Explicit"/> or 
        /// <see cref="LayoutKind.Sequential"/>.</para>
        /// <para>Thus, no Class nor ValueType with
        /// <see cref="LayoutKind.Auto"/> shall own any rows in the
        /// <see cref="ICliMetadataClassLayoutTable"/>.</para></description></item>
        /// </list>
        /// </remarks>
        ClassLayout                     = 1UL << 0x0F,
        /// <summary>
        /// The constant table defines compile-time constants for fields, parameters,
        /// and properties.
        /// </summary>
        /// <remarks>
        /// <para>Metadata Validation Rules (as per ECMA-335 22.9):</para>
        /// <list type="table"><listheader><term>Category</term><description>Description</description></listheader>
        /// <item><term><see cref="MetadataValidationRuleCategory.Error"/></term><description>
        /// <para><see cref="ICliMetadataConstantTableRow.Type"/> shall be exactly one of:</para><list type="number">
        /// <item><term><see cref="CliMetadataNativeTypes.Boolean"/></term></item>
        /// <item><term><see cref="CliMetadataNativeTypes.Byte"/></term></item>
        /// <item><term><see cref="CliMetadataNativeTypes.SByte"/></term></item>
        /// <item><term><see cref="CliMetadataNativeTypes.Int16"/></term></item>
        /// <item><term><see cref="CliMetadataNativeTypes.UInt16"/></term></item>
        /// <item><term><see cref="CliMetadataNativeTypes.Int32"/></term></item>
        /// <item><term><see cref="CliMetadataNativeTypes.UInt32"/></term></item>
        /// <item><term><see cref="CliMetadataNativeTypes.Int64"/></term></item>
        /// <item><term><see cref="CliMetadataNativeTypes.UInt64"/></term></item>
        /// <item><term><see cref="CliMetadataNativeTypes.Single"/></term></item>
        /// <item><term><see cref="CliMetadataNativeTypes.Double"/></term></item>
        /// <item><term><see cref="CliMetadataNativeTypes.Char"/></term></item>
        /// <item><term><see cref="CliMetadataNativeTypes.String"/></term></item>
        /// <item><term><see cref="CliMetadataNativeTypes.Class"/></term></item>
        /// </list></description></item>
        /// <item><term><see cref="MetadataValidationRuleCategory.Cls"/></term>
        /// <description><see cref="ICliMetadataConstantTableRow.Type"/> shall not be any of:
        /// <list type="number">
        /// <item><term><see cref="CliMetadataNativeTypes.SByte"/></term></item>
        /// <item><term><see cref="CliMetadataNativeTypes.UInt16"/></term></item>
        /// <item><term><see cref="CliMetadataNativeTypes.UInt32"/></term></item>
        /// <item><term><see cref="CliMetadataNativeTypes.UInt64"/></term></item>
        /// </list></description></item><item>
        /// <term><see cref="MetadataValidationRuleCategory.Error"/></term>
        /// <description><see cref="ICliMetadataConstantTableRow.ParentIndex"/> shall index a valid
        /// row in the <see cref="ICliMetadataFieldTable"/>, <see cref="ICliMetadataPropertyTable"/> or
        /// the <see cref="ICliMetadataParameterTable"/> via its <see cref="ICliMetadataConstantTableRow.ParentSource"/>.
        /// </description>
        /// </item>
        /// <item><term><see cref="MetadataValidationRuleCategory.Error"/></term>
        /// <description>There shall be no duplicate rows based upon a combination of <see cref="ICliMetadataConstantTableRow.ParentIndex"/> and
        /// <see cref="ICliMetadataConstantTableRow.ParentSource"/>.</description></item>
        /// <item><term><see cref="MetadataValidationRuleCategory.Cls"/></term><description><see cref="ICliMetadataConstantTableRow.Type"/> shall
        /// match exactly as the declared type of the <see cref="ICliMetadataFieldTableRow"/>, <see cref="ICliMetadataPropertyTableRow"/> or
        /// the <see cref="ICliMetadataParameterTableRow"/> defined by the combination of <see cref="ICliMetadataConstantTableRow.ParentIndex"/> and
        /// <see cref="ICliMetadataConstantTableRow.ParentSource"/> (in the case where the parent is an enum, it shall match exactly
        /// the underlying type of that enum.)</description></item></list></remarks>
        Constant                        = 1UL << 0x0B,
        CustomAttribute                 = 1UL << 0x0C,
        DeclSecurity                    = 1UL << 0x0E,
        EventMap                        = 1UL << 0x12,
        Event                           = 1UL << 0x14,
        ExportedType                    = 1UL << 0x27,
        Field                           = 1UL << 0x04,
        FieldLayout                     = 1UL << 0x10,
        FieldMarshal                    = 1UL << 0x0D,
        FieldRelativeVirtualAddress     = 1UL << 0x1D,
        File                            = 1UL << 0x26,
        GenericParameter                = 1UL << 0x2A,
        GenericParamConstraint          = 1UL << 0x2C,
        ImportMap                       = 1UL << 0x1C,
        InterfaceImpl                   = 1UL << 0x09,
        ManifestResource                = 1UL << 0x28,
        MemberReference                 = 1UL << 0x0A,
        MethodDefinition                = 1UL << 0x06,
        MethodImpl                      = 1UL << 0x19,
        MethodSemantics                 = 1UL << 0x18,
        MethodSpecification             = 1UL << 0x2B,
        Module                          = 1UL << 0x00,
        ModuleReference                 = 1UL << 0x1A,
        NestedClass                     = 1UL << 0x29,
        Parameter                       = 1UL << 0x08,
        Property                        = 1UL << 0x17,
        PropertyMap                     = 1UL << 0x15,
        StandAloneSig                   = 1UL << 0x11,
        TypeDefinition                  = 1UL << 0x02,
        TypeReference                   = 1UL << 0x01,
        TypeSpecification               = 1UL << 0x1B,
        CustomAttributeTypeTag_Unused1  = 1UL << 0x3D,
        CustomAttributeTypeTag_Unused2  = 1UL << 0x3E,
        CustomAttributeTypeTag_Unused3  = 1UL << 0x3F,
        SupportedMask                   = Assembly | AssemblyOS | AssemblyProcessor | AssemblyReference | AssemblyReferenceOS | AssemblyReferenceProcessor |
                                          ClassLayout | Constant | CustomAttribute | DeclSecurity | EventMap | Event | ExportedType | Field | FieldLayout |
                                          FieldMarshal | FieldRelativeVirtualAddress | File | GenericParameter | GenericParamConstraint | ImportMap |
                                          InterfaceImpl | ManifestResource | MemberReference | MethodDefinition | MethodImpl | MethodSemantics |
                                          MethodSpecification | Module | ModuleReference | NestedClass | Parameter | Property | PropertyMap | StandAloneSig |
                                          TypeDefinition | TypeReference | TypeSpecification,
    }
}
