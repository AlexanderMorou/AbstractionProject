using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen;
using AllenCopeland.Abstraction.OldCodeGen.Types;
using System.Collections.ObjectModel;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.OldCodeGen.Types.Members;
using AllenCopeland.Abstraction.IO;
using AllenCopeland.Abstraction.OldCodeGen.Expression;
using AllenCopeland.Abstraction.OldCodeGen.Statements;
using System.CodeDom;
using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.OldCodeGen.Compiler;
using AllenCopeland.Abstraction.OldCodeGen.Translation;
using System.IO;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.OldCodeGen.FileModel;
using System.CodeDom.Compiler;
using TypeDefOrRef = AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataTypeDefOrRefTag;
using HasConstant = AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataHasConstantTag;
using TypeDefOrMethodDef = AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataTypeOrMethodDef;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
namespace CliMetadataReader
{
    class Program
    {
        static void Main(string[] args)
        {
            /* *
             * Classic example of 'Get it done'.  I hate this style of programming
             * but it's very effective for program generators.
             * */
            var uintDataType = new MetadataTableTypeDataType(typeof(uint).GetTypeReference());
            var ushortDataType = new MetadataTableTypeDataType(typeof(ushort).GetTypeReference());
            var typeAttributes = new MetadataTableTypeDataType(typeof(TypeAttributes).GetTypeReference(), typeof(uint).GetTypeReference());
            var fieldAttributes = new MetadataTableTypeDataType(typeof(FieldAttributes).GetTypeReference(), typeof(ushort).GetTypeReference());
            var methodImplementationDetails = new MetadataTableTypeDataType(typeof(MethodImplementationDetails).GetTypeReference(), typeof(ushort).GetTypeReference());
            var methodUsage = new MetadataTableTypeDataType(typeof(MethodUseDetails).GetTypeReference(), typeof(ushort).GetTypeReference());
            var parameterAttributes = new MetadataTableTypeDataType(typeof(ParameterAttributes).GetTypeReference(), typeof(ushort).GetTypeReference());
            var nativeTypes = new MetadataTableTypeDataType(typeof(CliMetadataNativeTypes).GetTypeReference(), typeof(ushort).GetTypeReference());
            var securityAction = new MetadataTableTypeDataType(typeof(SecurityAction).GetTypeReference(), typeof(ushort).GetTypeReference());
            var eventAttributes = new MetadataTableTypeDataType(typeof(EventAttributes).GetTypeReference(), typeof(ushort).GetTypeReference());
            var propertyAttributes = new MetadataTableTypeDataType(typeof(PropertyAttributes).GetTypeReference(), typeof(ushort).GetTypeReference());
            var methodSemanticsAttributes = new MetadataTableTypeDataType(typeof(MethodSemanticsAttributes).GetTypeReference(), typeof(ushort).GetTypeReference());
            var platformInvokeCharacterSet = new MetadataTableTypeDataType(typeof(PlatformInvokeCharacterSet).GetTypeReference(), typeof(byte).GetTypeReference());
            var platformInvokeCallingConvention = new MetadataTableTypeDataType(typeof(PlatformInvokeCallingConvention).GetTypeReference(), typeof(byte).GetTypeReference());
            var assemblyHashAlgorithm = new MetadataTableTypeDataType(typeof(AssemblyHashAlgorithm).GetTypeReference(), typeof(uint).GetTypeReference());
            var assemblyFlags = new MetadataTableTypeDataType(typeof(CliMetadataAssemblyFlags).GetTypeReference(), typeof(uint).GetTypeReference());
            var qwordVersion = new MetadataTableTypeDataType(typeof(QWordLongVersion).GetTypeReference(), true);
            var fileAttributes = new MetadataTableTypeDataType(typeof(CliMetadataFileAttributes).GetTypeReference(), typeof(uint).GetTypeReference());
            var genericParameterAttributes = new MetadataTableTypeDataType(typeof(GenericParameterAttributes).GetTypeReference(), typeof(ushort).GetTypeReference());

            var stringsHeap = new MetadataTableFieldHeapDataType(MetadataHeapTarget.StringsHeap);
            var blobHeap = new MetadataTableFieldHeapDataType(MetadataHeapTarget.BlobHeap);
            var guidHeap = new MetadataTableFieldHeapDataType(MetadataHeapTarget.GuidHeap);
            var userString = new MetadataTableFieldHeapDataType(MetadataHeapTarget.UserStringsHeap);
            var signatureKindsType = typeof(SignatureKinds).GetTypeReferenceExpression();
            var fieldSignatureBlobType = new MetadataTableBlobHeapDataType(signatureKindsType.GetField("FieldSig"), typeof(ICliMetadataFieldSignature).GetTypeReference());
            var methodSignatureBlobType = new MetadataTableBlobHeapDataType(signatureKindsType.GetField("MethodDefSig"), typeof(ICliMetadataMethodSignature).GetTypeReference());
            var methodRefSignatureBlobType = new MetadataTableBlobHeapDataType(signatureKindsType.GetField("MethodRefSig"), typeof(ICliMetadataMethodSignature).GetTypeReference());
            var memberRefSignatureBlobType = new MetadataTableBlobHeapDataType(signatureKindsType.GetField("MemberRefSig"), typeof(ICliMetadataMemberRefSignature).GetTypeReference());
            var standAloneSignatureBlobType = new MetadataTableBlobHeapDataType(signatureKindsType.GetField("StandaloneSignature"), typeof(ICliMetadataStandAloneSignature).GetTypeReference());
            var propertySignatureBlobType = new MetadataTableBlobHeapDataType(signatureKindsType.GetField("PropertySig"), typeof(ICliMetadataPropertySignature).GetTypeReference());
            var typeSpecBlobType = new MetadataTableBlobHeapDataType(signatureKindsType.GetField("TypeSpec"), typeof(ICliMetadataTypeSpecSignature).GetTypeReference());
            var methodSpecBlobType = new MetadataTableBlobHeapDataType(signatureKindsType.GetField("MethodSpec"), typeof(ICliMetadataMethodSpecSignature).GetTypeReference());


            var typeDefinitionOrReferenceEncoding = new MetadataTableEncoding<TypeDefOrRef>("TypeDefOrRef");
            var hasConstantEncoding = new MetadataTableEncoding<HasConstant>("HasConstant");
            var hasCustomAttributeEncoding = new MetadataTableEncoding<CliMetadataHasCustomAttributeTag>("HasCustomAttribute");
            var hasFieldMarshalEncoding = new MetadataTableEncoding<CliMetadataHasFieldMarshalTag>("HasFieldMarshal");
            var hasDeclaredSecurityEncoding = new MetadataTableEncoding<CliMetadataHasDeclSecurityTag>("HasDeclSecurity");
            var memberReferenceParentEncoding = new MetadataTableEncoding<CliMetadataMemberRefParentTag>("MemberRefParent");
            var hasSemanticsEncoding = new MetadataTableEncoding<CliMetadataHasSemanticsTag>("HasSemantics");
            var methodDefinitionOrReferenceEncoding = new MetadataTableEncoding<CliMetadataMethodDefOrRefTag>("MethodDefOrRef");
            var memberForwardedEncoding = new MetadataTableEncoding<CliMetadataMemberForwardedTag>("MemberForwarded");
            var implementationEncoding = new MetadataTableEncoding<CliMetadataImplementationTag>("Implementation");
            var customAttributeTypeEncoding = new MetadataTableEncoding<CliMetadataCustomAttributeTypeTag>("CustomAttributeType");
            var resolutionScopeEncoding = new MetadataTableEncoding<CliMetadataResolutionScopeTag>("ResolutionScope");
            var typeOrMethodDefinitionEncoding = new MetadataTableEncoding<CliMetadataTypeOrMethodDef>("TypeOrMethodDef");

            const string defaultNamespaceName = "AllenCopeland.Abstraction.Slf.Cli.Metadata";
            const string defaultInternalNamespaceName = "AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata";
            const string depreciatedMetadata = "This record should not be emitted into any PE image; however if present, it should be treated as if all its fields were zero. Supported to ensure proper reading of the metadata.";
            /* *
             * A few rules:
             * @s:Target; is the code generator's '<see cref="Target"/>' shortcut,
             * this is to ensure that you can embed '<' and '>' into comments while escaping them
             * properly.  Since it can't know the difference between when you want to escape and when you
             * don't, a secondary format was used.
             * */
            Dictionary<CliMetadataTableKinds, MetadataTable> tableLookup = new Dictionary<CliMetadataTableKinds, MetadataTable>();
            List<MetadataTable> tables = new List<MetadataTable>()
            {
                new MetadataTable("Module",                         0x00)
                {
                    new MetadataTableField("Generation",            ushortDataType, "The generation associated to the module", "A two-byte (2-byte) value, reserved, shall be zero."),
                    new MetadataTableField("Name",                  stringsHeap,    "The name of the module."),
                    new MetadataTableField("ModuleVersion",         guidHeap,       "A Guid used to distinguish between two versions of the same module."),
                    new MetadataTableField("EncId",                 guidHeap,       "An index into the Guid heap, reserved, shall be zero."),
                    new MetadataTableField("EncBaseId",             guidHeap,       "An index into the Guid heap, reserved, shall be zero.") 
                },
                new MetadataTable("TypeReference",                  0x01, "Defines the structure of a type reference, which identifies how to resolve the reference, its name, and namespace.", "ResolutionScope shall be exactly one of:@table;|-null -|- in this case there shall be a row in @s:CliMetadataExportedTypeTable; which should identify where the type is now defined.-|@/table;", "TypeRef")
                {
                    new MetadataTableEncodedField<CliMetadataResolutionScopeTag>("Source", resolutionScopeEncoding, "ResolutionScope", "The source of the type."),
                    new MetadataTableField("Name",                  stringsHeap,    "The name of the referenced type."),
                    new MetadataTableField("Namespace",             stringsHeap,    "The namespace of the referenced type."),
                },
                new MetadataTable("TypeDefinition",                 0x02, "Defines the information about the types within the image's metadata.")
                {
                    new MetadataTableField("TypeAttributes",        typeAttributes, "The @s:TypeAttributes; which denote information about the type's structure."),
                    new MetadataTableField("Name",                  stringsHeap,    "The name of the defined type."),
                    new MetadataTableField("Namespace",             stringsHeap,    "The namespace of the defined type."),
                    new MetadataTableEncodedField<TypeDefOrRef>("Extends", typeDefinitionOrReferenceEncoding, "ExtendsSource", "The @s:ICliMetadataTypeDefOrRefRow; from which the type derives."),
                    new MetadataTableField("FieldStart", () =>      tableLookup[CliMetadataTableKinds.Field], "The first field in the type.", "The set of fields defined by the type can be determined by the next row's first field."),
                    new MetadataTableField("MethodStart", () =>     tableLookup[CliMetadataTableKinds.MethodDefinition], "The first method in the type.", "The set of methods defined by the type can be determined by the next row's first method."),
                },
                new MetadataTable("Field",                          0x04, "Defines information about the image's fields.")
                {
                    new MetadataTableField("FieldAttributes",       fieldAttributes, "Conditional information about the field and its accessibility."),
                    new MetadataTableField("Name",                  stringsHeap, "The name of the field."),
                    new MetadataTableField("FieldType",             fieldSignatureBlobType, "The type of the field, in signature form."),
                },
                new MetadataTable("MethodDefinition",               0x06, "Defines information about the image's methods.")
                {
                    new MetadataTableField("RVA",                   uintDataType, "The relative virtual address of the method's body."),
                    new MetadataTableField("ImplementationDetails", methodImplementationDetails, "The conditional information about the method's implementation."),
                    new MetadataTableField("UsageDetails",          methodUsage, "Conditional information about the method, its accessibility, and vtable information."),
                    new MetadataTableField("Name",                  stringsHeap, "The name of the method."),
                    new MetadataTableField("Signature",             methodSignatureBlobType, "The signature of the method, that is: it's return type, parameter types, and potential generic calling convention."),
                    new MetadataTableField("ParameterStart", () =>  tableLookup[CliMetadataTableKinds.Parameter], "The parameters of the method."),
                },
                new MetadataTable("Parameter",                      0x08, "Defines information about the parameters defined within the image.")
                {
                    new MetadataTableField("Flags",                 parameterAttributes, "Conditional information about the parameter, whether it's optional, has marshaling applied to it, and/or the direction in which the parameter's data is coerced."),
                    new MetadataTableField("Sequence",              ushortDataType, "The ordinal index of the parameter within the sequence of parameters for a given method.", "Gaps in Sequence are allowed; however, the value of sequence from one parameter to the next must be in increasing value.  Parameter with sequence index zero refers to the method's return type."),
                    new MetadataTableField("Name",                  stringsHeap, "The name of the method.", "The name of a parameter can be null or non-null."),
                },
                new MetadataTable("InterfaceImpl",                  0x09, "Defines information about the interfaces implemented by the defined types of the image.")
                {
                    new MetadataTableField("Class", () =>           tableLookup[CliMetadataTableKinds.TypeDefinition], "The class which implements @s:Interface;."),
                    new MetadataTableEncodedField<TypeDefOrRef>("Interface", typeDefinitionOrReferenceEncoding, "InterfaceSource", "The interface implemented by @s:Class;."),
                },
                new MetadataTable("MemberReference",                0x0A, "Defines information about the members referenced by the metadata's method bodies.")
                {
                    new MetadataTableEncodedField<CliMetadataMemberRefParentTag>("Class", memberReferenceParentEncoding, "ClassSource"),
                    new MetadataTableField("Name",                  stringsHeap, "The name of the member reference."),
                    new MetadataTableField("Signature",             memberRefSignatureBlobType, ""),
                },
                new MetadataTable("Constant",                       0x0B, "Defines information about the constants within the image.", "The constants are defined and perhaps used within metadata; however, they are not referenceable through any IL instruction.  Compilers must fold the constants into the emitted IL.")
                {
                    new MetadataTableField("Type", nativeTypes),
                    new MetadataTableEncodedField<HasConstant>("Parent", hasConstantEncoding, "ParentSource"),
                    new MetadataTableField("Value",                 blobHeap),
                },
                new MetadataTable("CustomAttribute",                0x0C)
                {
                    new MetadataTableEncodedField<CliMetadataHasCustomAttributeTag>("Parent", hasCustomAttributeEncoding, "ParentSource"),
                    new MetadataTableEncodedField<CliMetadataCustomAttributeTypeTag>("Ctor", customAttributeTypeEncoding, "CtorSource"),
                    new MetadataTableField("Value",                 blobHeap),
                },
                new MetadataTable("FieldMarshal",                   0x0D)
                {
                    new MetadataTableEncodedField<CliMetadataHasFieldMarshalTag>("Parent", hasFieldMarshalEncoding, "ParentSource"),
                    new MetadataTableField("NativeType",            blobHeap),
                },
                new MetadataTable("DeclSecurity",                   0x0E)
                {
                    new MetadataTableField("Action",                securityAction),
                    new MetadataTableEncodedField<CliMetadataHasDeclSecurityTag>("Parent", hasDeclaredSecurityEncoding, "ParentSource"),
                    new MetadataTableField("PermissionSet", blobHeap)
                },
                new MetadataTable("ClassLayout",                    0x0F, "Defines information about the data size and packing size of a type.")
                {
                    new MetadataTableField("PackingSize",           ushortDataType),
                    new MetadataTableField("ClassSize",             uintDataType),
                    new MetadataTableField("Parent", () =>          tableLookup[CliMetadataTableKinds.TypeDefinition])
                },
                new MetadataTable("FieldLayout",                    0x10, "Defines the layout of the fields on an @s:LayoutKind.Explicit; layout type.")
                {
                    new MetadataTableField("Offset",                uintDataType),
                    new MetadataTableField("Field", () =>           tableLookup[CliMetadataTableKinds.Field]) { IndexSummary = "Returns the @s:UInt32; value which represents the field for which the layout exists." } 
                },
                new MetadataTable("StandAloneSig",                  0x11, "Defines the offset to a standalone signature.", "Used when a method is called by address, the standalone signature is pushed onto the stack and the call is made.")
                {
                    new MetadataTableField("Signature",             standAloneSignatureBlobType),
                },
                new MetadataTable("EventMap",                       0x12, "Defines the event mapping of a type defined within the module.")
                {
                    new MetadataTableField("Parent", () =>          tableLookup[CliMetadataTableKinds.TypeDefinition], "The target type definition which contains the series of events."),
                    new MetadataTableField("EventList", () =>       tableLookup[CliMetadataTableKinds.Event], "The first event of the @s:Parent;.", "The full list of events can be obtained by comparing the next entry's @s:EventList;.")
                },
                new MetadataTable("Event",                          0x14, "Defines information about an event.")
                {
                    new MetadataTableField("Flags",                 eventAttributes, "The @s:EventAttributes; which denote how the event is handled."),
                    new MetadataTableField("Name",                  stringsHeap,     "The name of the event."),
                    new MetadataTableEncodedField<TypeDefOrRef>("SignatureType", typeDefinitionOrReferenceEncoding, "SignatureSource", "The @s:ITypeDefOrRefRow; which is the delegate that acts as the event's signature."),
                },
                new MetadataTable("PropertyMap",                    0x15, "Defines the property mapping of a type defined within the module.")
                {
                    new MetadataTableField("Parent", () =>          tableLookup[CliMetadataTableKinds.TypeDefinition], "The target type definition which contains the series of properties."),
                    new MetadataTableField("PropertyList", () =>    tableLookup[CliMetadataTableKinds.Property], "The properties defined on the current type.")
                },
                new MetadataTable("Property",                       0x17, "Defines information about a property.")
                {
                    new MetadataTableField("Flags",                 propertyAttributes, "Conditional information about the property, such as special runtime handling semantics."),
                    new MetadataTableField("Name",                  stringsHeap, "The name of the property."),
                    new MetadataTableField("PropertyType",          propertySignatureBlobType, "The signature of the property."),
                },
                new MetadataTable("MethodSemantics",                0x18, "Defines semantic information about a method.")
                {
                    new MetadataTableField("Semantics",             methodSemanticsAttributes, "Whether the method belongs to a property or event."),
                    new MetadataTableField("Method", () =>          tableLookup[CliMetadataTableKinds.MethodDefinition], "The target method of the semantics."),
                    new MetadataTableEncodedField<CliMetadataHasSemanticsTag>("Association", hasSemanticsEncoding, "AssociationSource"),
                },
                new MetadataTable("MethodImpl",                     0x19)
                {
                    new MetadataTableField("Class", () =>           tableLookup[CliMetadataTableKinds.TypeDefinition]),
                    new MetadataTableEncodedField<CliMetadataMethodDefOrRefTag>("MethodBody", methodDefinitionOrReferenceEncoding, "MethodBodySource"),
                    new MetadataTableEncodedField<CliMetadataMethodDefOrRefTag>("MethodDeclaration", methodDefinitionOrReferenceEncoding, "MethodDeclarationSource"),
                },
                new MetadataTable("ModuleReference",                0x1A, "Defines information about imported modules.")
                {
                    new MetadataTableField("Name",                  stringsHeap, "The name of the imported module."),
                },
                new MetadataTable("TypeSpecification",              0x1B, "Defines information about a type specification.")
                {
                    new MetadataTableField("Signature",             typeSpecBlobType, "The signature of the type specification."),
                },
                new MetadataTable("ImportMap",                      0x1C)
                {
                    new MetadataTableField("MappingCharset",        platformInvokeCharacterSet),
                    new MetadataTableField("MappingCallingConvention", platformInvokeCallingConvention),
                    new MetadataTableEncodedField<CliMetadataMemberForwardedTag>("MemberForwarded", memberForwardedEncoding, "MemberForwardedSource"),
                    new MetadataTableField("ImportName",            stringsHeap),
                    new MetadataTableField("ImportScope", () =>     tableLookup[CliMetadataTableKinds.ModuleReference]),

                },
                new MetadataTable("FieldRelativeVirtualAddress",    0x1D, "Defines information about a field's relative virtual address.")
                {
                    new MetadataTableField("RVA",                   uintDataType, "Returns the @s:UInt32; value representing the relative virtual address for the field."),
                    new MetadataTableField("Field", () =>           tableLookup[CliMetadataTableKinds.Field]) { IndexSummary = "Returns the @s:UInt32; value which represents the field for which the rva exists." },
                },
                new MetadataTable("Assembly",                       0x20, "Defines the manifest of an assembly.", "There can be zero or one in a CLI conformant module.")
                {
                    new MetadataTableField("HashAlgorithmId",       assemblyHashAlgorithm),
                    new MetadataTableField("Version",               qwordVersion),
                    new MetadataTableField("Flags",                 assemblyFlags),
                    new MetadataTableField("PublicKey",             blobHeap),
                    new MetadataTableField("Name",                  stringsHeap),
                    new MetadataTableField("Culture",               stringsHeap),
                },
                new MetadataTable("AssemblyProcessor",              0x21, "Defines the processor of the target machine of the assembly.", depreciatedMetadata)
                {
                    new MetadataTableField("Processor", uintDataType)
                },
                new MetadataTable("AssemblyOS",                     0x22, "Defines the target operating system of the assembly.", depreciatedMetadata)
                {
                    new MetadataTableField("PlatformId",            uintDataType),
                    new MetadataTableField("MajorVersion",          uintDataType),
                    new MetadataTableField("MinorVersion",          uintDataType),
                },
                new MetadataTable("AssemblyReference",              0x23, "Defines the assembly references of a module through its manifest.")
                {
                    new MetadataTableField("Version",               qwordVersion),
                    new MetadataTableField("Flags",                 assemblyFlags),
                    new MetadataTableField("PublicKeyOrToken",      blobHeap),
                    new MetadataTableField("Name",                  stringsHeap),
                    new MetadataTableField("Culture",               stringsHeap),
                    new MetadataTableField("HashValue",             blobHeap),
                },
                new MetadataTable("AssemblyReferenceProcessor",     0x24, "Defines the processor of the target machine of the reference assembly.", depreciatedMetadata)
                {
                    new MetadataTableField("Processor",             uintDataType),
                    new MetadataTableField("AssemblyRef", () =>     tableLookup[CliMetadataTableKinds.AssemblyReference]),
                },
                new MetadataTable("AssemblyReferenceOS",            0x25, "Defines the target operating system of the assembly.", depreciatedMetadata)
                {
                    new MetadataTableField("PlatformId",            uintDataType),
                    new MetadataTableField("MajorVersion",          uintDataType),
                    new MetadataTableField("MinorVersion",          uintDataType),
                    new MetadataTableField("AssemblyRef", () =>     tableLookup[CliMetadataTableKinds.AssemblyReference]),
                },
                new MetadataTable("File",                           0x26, "Defines the external files associated to the assembly.", "Files associated to the assembly do not necessarily contain metadata themselves, as such, they will be marked as purely data files.")
                {
                    new MetadataTableField("Flags",                 fileAttributes),
                    new MetadataTableField("Name",                  stringsHeap),
                    new MetadataTableField("HashValue",             blobHeap),
                },
                new MetadataTable("ExportedType",                   0x27)
                {
                    new MetadataTableField("TypeAttributes",        typeAttributes),
                    new MetadataTableField("TypeDefIdentifier",     uintDataType),
                    new MetadataTableField("Name",                  stringsHeap),
                    new MetadataTableField("Namespace",             stringsHeap),
                    new MetadataTableEncodedField<CliMetadataImplementationTag>("Implementation", implementationEncoding, "ImplementationSource"),
                },
                new MetadataTable("ManifestResource",               0x28)
                {
                    new MetadataTableField("Offset",                uintDataType),
                    new MetadataTableField("Flags",                 uintDataType),
                    new MetadataTableField("Name",                  stringsHeap),
                    new MetadataTableEncodedField<CliMetadataImplementationTag>("Implementation", implementationEncoding, "ImplementationSource"),
                },
                new MetadataTable("NestedClass",                    0x29)
                {
                    new MetadataTableField("NestedClass", () =>     tableLookup[CliMetadataTableKinds.TypeDefinition]),
                    new MetadataTableField("EnclosingClass", () =>  tableLookup[CliMetadataTableKinds.TypeDefinition]),
                },
                new MetadataTable("GenericParameter",               0x2A)
                {
                    new MetadataTableField("Number",                ushortDataType),
                    new MetadataTableField("Flags",                 genericParameterAttributes),
                    new MetadataTableEncodedField<TypeDefOrMethodDef>("Owner", typeOrMethodDefinitionEncoding, "OwnerSource"),
                    new MetadataTableField("Name",                  stringsHeap),
                },
                new MetadataTable("MethodSpecification",            0x2B)
                {
                    new MetadataTableEncodedField<CliMetadataMethodDefOrRefTag>("Method", methodDefinitionOrReferenceEncoding, "MethodBodySource"),
                    new MetadataTableField("Instantiation",         methodSpecBlobType),
                },
                new MetadataTable("GenericParamConstraint",         0x2C)
                {
                    new MetadataTableField("Owner", () =>           tableLookup[CliMetadataTableKinds.GenericParameter]),
                    new MetadataTableEncodedField<TypeDefOrRef>("Constraint", typeDefinitionOrReferenceEncoding, "ConstraintSource"),
                },
            };

            foreach (var table in tables)
                tableLookup.Add(table.TableKind, table);

            var nestedClassEnclosingField = tableLookup[CliMetadataTableKinds.NestedClass]["EnclosingClass"];
            var nestedClassNestedField = tableLookup[CliMetadataTableKinds.NestedClass]["NestedClass"];

            nestedClassEnclosingField.ImportType = MetadataTableFieldImportKind.ManyToOneImport;
            nestedClassEnclosingField.SourceKind = MetadataTableFieldListSource.FieldRef;
            nestedClassEnclosingField.TargetField = nestedClassNestedField;
            nestedClassEnclosingField.TargetListTable = tableLookup[CliMetadataTableKinds.TypeDefinition];
            nestedClassEnclosingField.ImportName = "NestedClasses";
            nestedClassEnclosingField.ImportSummary = "Returns the nested types for the current type.";

            nestedClassNestedField.ImportType = MetadataTableFieldImportKind.TableReference;
            nestedClassNestedField.ImportName = "DeclaringType";
            nestedClassNestedField.TargetField = nestedClassEnclosingField;
            nestedClassNestedField.ImportSummary = "Returns the type which declares the current type.";
            nestedClassNestedField.ImportRemarks = "Can be null.";

            var methodEntriesField = tableLookup[CliMetadataTableKinds.TypeDefinition]["MethodStart"];
            methodEntriesField.ImportType = MetadataTableFieldImportKind.OneToSequentialMany;
            methodEntriesField.TargetListTable = tableLookup[CliMetadataTableKinds.MethodDefinition];
            methodEntriesField.ImportName = "Methods";
            methodEntriesField.ImportSummary = "Returns the methods for the current type.";

            var fieldEntriesField = tableLookup[CliMetadataTableKinds.TypeDefinition]["FieldStart"];
            fieldEntriesField.ImportType = MetadataTableFieldImportKind.OneToSequentialMany;
            fieldEntriesField.TargetListTable = tableLookup[CliMetadataTableKinds.Field];
            fieldEntriesField.ImportName = "Fields";
            fieldEntriesField.ImportSummary = "Returns the fields for the current type.";



            var parametersField = tableLookup[CliMetadataTableKinds.MethodDefinition]["ParameterStart"];
            parametersField.ImportType = MetadataTableFieldImportKind.OneToSequentialMany;
            parametersField.TargetListTable = tableLookup[CliMetadataTableKinds.Parameter];
            parametersField.ImportName = "Parameters";
            parametersField.ImportSummary = "Returns the parameters for the current method.";

            var eventListField = tableLookup[CliMetadataTableKinds.EventMap]["Parent"];
            eventListField.ImportType = MetadataTableFieldImportKind.OneToSequentialManyImported;
            eventListField.TargetListTable = tableLookup[CliMetadataTableKinds.EventMap];
            eventListField.TargetField = tableLookup[CliMetadataTableKinds.EventMap]["EventList"];
            eventListField.TargetListTable = tableLookup[CliMetadataTableKinds.Event];
            eventListField.ImportName = "Events";
            eventListField.ResultedListElementName = "Event";
            eventListField.ImportSummary = "Returns the events for the current type definition.";

            var propertyListField = tableLookup[CliMetadataTableKinds.PropertyMap]["Parent"];
            propertyListField.ImportType = MetadataTableFieldImportKind.OneToSequentialManyImported;
            propertyListField.TargetListTable = tableLookup[CliMetadataTableKinds.PropertyMap];
            propertyListField.TargetField = tableLookup[CliMetadataTableKinds.PropertyMap]["PropertyList"];
            propertyListField.TargetListTable = tableLookup[CliMetadataTableKinds.Property];
            propertyListField.ImportName = "Properties";
            propertyListField.ResultedListElementName = "Property";
            propertyListField.ImportSummary = "Returns the properties for the current type definition.";


            var genericParameterOwner = tableLookup[CliMetadataTableKinds.GenericParameter]["Owner"];
            genericParameterOwner.ImportType = MetadataTableFieldImportKind.ManyToOneImport;
            genericParameterOwner.SourceKind = MetadataTableFieldListSource.SourceTableRow;
            genericParameterOwner.ImportName = "TypeParameters";
            genericParameterOwner.ImportSummary = "Returns the type-parameters relative to the current row.";

            var customAttributeOwner = tableLookup[CliMetadataTableKinds.CustomAttribute]["Parent"];
            customAttributeOwner.ImportType = MetadataTableFieldImportKind.ManyToOneImport;
            customAttributeOwner.SourceKind = MetadataTableFieldListSource.SourceTableRow;
            customAttributeOwner.ImportName = "CustomAttributes";
            customAttributeOwner.ImportSummary = "Returns the set of custom metadata elements applied to the member.";


            var genericParameterConstraint = tableLookup[CliMetadataTableKinds.GenericParamConstraint]["Owner"];
            genericParameterConstraint.ImportType = MetadataTableFieldImportKind.ManyToOneImport;
            genericParameterConstraint.SourceKind = MetadataTableFieldListSource.SourceTableRow;
            genericParameterConstraint.ImportName = "Constraints";
            genericParameterConstraint.ImportSummary = "Returns the constraints relative to the current generic parameter.";

            var fieldLayout = tableLookup[CliMetadataTableKinds.FieldLayout]["Field"];
            fieldLayout.ImportType = MetadataTableFieldImportKind.TableReference;
            fieldLayout.ImportName = "Layout";
            fieldLayout.ImportSummary = "Returns the layout of the field which determines the byte offset of the field relative to the structure which contains it.";
            fieldLayout.ImportRemarks = "Can be null.";

            var fieldRVA = tableLookup[CliMetadataTableKinds.FieldRelativeVirtualAddress]["Field"];
            fieldRVA.ImportType = MetadataTableFieldImportKind.TableReference;
            fieldRVA.ImportName = "RVA";
            fieldRVA.ImportSummary = "Returns the relative virtual address for the field.";
            fieldRVA.ImportRemarks = "Usually null except for initialized and uninitialized '.data' fields which store sequential bytes of data within the application's memory space.  The data-types of such fields must have no private fields of their own and contain no reference type fields as they point into the GC Heap.";

            var classLayout = tableLookup[CliMetadataTableKinds.ClassLayout]["Parent"];
            classLayout.ImportType = MetadataTableFieldImportKind.TableReference;
            classLayout.ImportName = "Layout";
            classLayout.ImportSummary = "Returns the class layout information which determines the data and packing size of the type.";
            classLayout.ImportRemarks = "Can be null.";

            var methodSemanticsAssociation = tableLookup[CliMetadataTableKinds.MethodSemantics]["Association"];
            methodSemanticsAssociation.ImportType = MetadataTableFieldImportKind.ManyToOneImport;
            methodSemanticsAssociation.SourceKind = MetadataTableFieldListSource.SourceTableRow;
            methodSemanticsAssociation.TargetField = tableLookup[CliMetadataTableKinds.MethodSemantics]["Method"];
            methodSemanticsAssociation.ImportName = "Methods";
            methodSemanticsAssociation.ImportSummary = "Returns the methods with semantics relative to the current row.";

            tableLookup[CliMetadataTableKinds.FieldRelativeVirtualAddress].ShortName = "FieldRVA";
            tableLookup[CliMetadataTableKinds.AssemblyReference].ShortName = "AssemblyRef";
            tableLookup[CliMetadataTableKinds.AssemblyReferenceOS].ShortName = "AssemblyRefOS";
            tableLookup[CliMetadataTableKinds.AssemblyReferenceProcessor].ShortName = "AssemblyRefProcessor";

            var interfaceImpl = tableLookup[CliMetadataTableKinds.InterfaceImpl]["Class"];
            interfaceImpl.ImportType = MetadataTableFieldImportKind.ManyToOneImport;
            interfaceImpl.SourceKind = MetadataTableFieldListSource.SourceTableRow;
            interfaceImpl.TargetField = tableLookup[CliMetadataTableKinds.InterfaceImpl]["Interface"];
            interfaceImpl.Summary = "Returns the set of interfaces implemented by the class.";
            interfaceImpl.ImportName = "ImplementedInterfaces";

            var implMap = tableLookup[CliMetadataTableKinds.MethodImpl]["Class"];
            implMap.ImportName = "ImplementationMap";
            implMap.SourceKind = MetadataTableFieldListSource.SourceTableRow;
            implMap.ImportType = MetadataTableFieldImportKind.ManyToOneImport;
            implMap.ImportSummary = "Returns the set of implementation mappings related to a class' implemented interfaces.";


            tableLookup[CliMetadataTableKinds.Property].NeedsIndex = true;
            tableLookup[CliMetadataTableKinds.Event].NeedsIndex = true;
            tableLookup[CliMetadataTableKinds.EventMap].NeedsIndex = true;
            tableLookup[CliMetadataTableKinds.PropertyMap].NeedsIndex = true;
            tableLookup[CliMetadataTableKinds.MethodSemantics].NeedsIndex = true;
            tableLookup[CliMetadataTableKinds.MethodImpl].NeedsIndex = true;
            tableLookup[CliMetadataTableKinds.CustomAttribute].NeedsIndex = true;
            tableLookup[CliMetadataTableKinds.GenericParamConstraint].NeedsIndex = true;

            //var assemblyRefOS = tableLookup[CliMetadataTableKinds.AssemblyReferenceOS];
            //var assemblyRefOSAssemblyRef = assemblyRefOS["AssemblyRef"];
            //assemblyRefOSAssemblyRef.ImportName = "OperatingSystem";
            //assemblyRefOSAssemblyRef.ImportType = MetadataTableFieldImportKind.TableReference;
            //var assemblyRefProcessor = tableLookup[CliMetadataTableKinds.AssemblyReferenceProcessor];
            //var assemblyRefProcessorAssemblyRef = assemblyRefProcessor["AssemblyRef"];
            //assemblyRefProcessorAssemblyRef.ImportName = "Processor";
            //assemblyRefProcessorAssemblyRef.ImportType = MetadataTableFieldImportKind.TableReference;

            #region Encoding setup
            typeDefinitionOrReferenceEncoding.Add(TypeDefOrRef.TypeDefinition, tableLookup[CliMetadataTableKinds.TypeDefinition]);
            typeDefinitionOrReferenceEncoding.Add(TypeDefOrRef.TypeReference, tableLookup[CliMetadataTableKinds.TypeReference]);
            typeDefinitionOrReferenceEncoding.Add(TypeDefOrRef.TypeSpecification, tableLookup[CliMetadataTableKinds.TypeSpecification]);

            hasConstantEncoding.Add(HasConstant.Field, tableLookup[CliMetadataTableKinds.Field]);
            hasConstantEncoding.Add(HasConstant.Param, tableLookup[CliMetadataTableKinds.Parameter]);
            hasConstantEncoding.Add(HasConstant.Property, tableLookup[CliMetadataTableKinds.Property]);

            hasCustomAttributeEncoding.Add(CliMetadataHasCustomAttributeTag.Assembly, tableLookup[CliMetadataTableKinds.Assembly]);
            hasCustomAttributeEncoding.Add(CliMetadataHasCustomAttributeTag.AssemblyReference, tableLookup[CliMetadataTableKinds.AssemblyReference]);
            hasCustomAttributeEncoding.Add(CliMetadataHasCustomAttributeTag.Event, tableLookup[CliMetadataTableKinds.Event]);
            hasCustomAttributeEncoding.Add(CliMetadataHasCustomAttributeTag.ExportedType, tableLookup[CliMetadataTableKinds.ExportedType]);
            hasCustomAttributeEncoding.Add(CliMetadataHasCustomAttributeTag.Field, tableLookup[CliMetadataTableKinds.Field]);
            hasCustomAttributeEncoding.Add(CliMetadataHasCustomAttributeTag.File, tableLookup[CliMetadataTableKinds.File]);
            hasCustomAttributeEncoding.Add(CliMetadataHasCustomAttributeTag.GenericParam, tableLookup[CliMetadataTableKinds.GenericParameter]);
            hasCustomAttributeEncoding.Add(CliMetadataHasCustomAttributeTag.GenericParamConstraint, null);
            hasCustomAttributeEncoding.Add(CliMetadataHasCustomAttributeTag.InterfaceImpl, tableLookup[CliMetadataTableKinds.InterfaceImpl]);
            hasCustomAttributeEncoding.Add(CliMetadataHasCustomAttributeTag.ManifestResource, tableLookup[CliMetadataTableKinds.ManifestResource]);
            hasCustomAttributeEncoding.Add(CliMetadataHasCustomAttributeTag.MemberRef, tableLookup[CliMetadataTableKinds.MemberReference]);
            hasCustomAttributeEncoding.Add(CliMetadataHasCustomAttributeTag.MethodDefinition, tableLookup[CliMetadataTableKinds.MethodDefinition]);
            hasCustomAttributeEncoding.Add(CliMetadataHasCustomAttributeTag.MethodSpec, null);
            hasCustomAttributeEncoding.Add(CliMetadataHasCustomAttributeTag.Module, tableLookup[CliMetadataTableKinds.Module]);
            hasCustomAttributeEncoding.Add(CliMetadataHasCustomAttributeTag.ModuleReference, tableLookup[CliMetadataTableKinds.ModuleReference]);
            hasCustomAttributeEncoding.Add(CliMetadataHasCustomAttributeTag.Parameter, tableLookup[CliMetadataTableKinds.Parameter]);
            hasCustomAttributeEncoding.Add(CliMetadataHasCustomAttributeTag.Permission, tableLookup[CliMetadataTableKinds.DeclSecurity]);
            hasCustomAttributeEncoding.Add(CliMetadataHasCustomAttributeTag.Property, tableLookup[CliMetadataTableKinds.Property]);
            hasCustomAttributeEncoding.Add(CliMetadataHasCustomAttributeTag.StandAloneSig, tableLookup[CliMetadataTableKinds.StandAloneSig]);
            hasCustomAttributeEncoding.Add(CliMetadataHasCustomAttributeTag.TypeDefinition, tableLookup[CliMetadataTableKinds.TypeDefinition]);
            hasCustomAttributeEncoding.Add(CliMetadataHasCustomAttributeTag.TypeReference, tableLookup[CliMetadataTableKinds.TypeReference]);
            hasCustomAttributeEncoding.Add(CliMetadataHasCustomAttributeTag.TypeSpecification, tableLookup[CliMetadataTableKinds.TypeSpecification]);

            hasFieldMarshalEncoding.Add(CliMetadataHasFieldMarshalTag.Field, tableLookup[CliMetadataTableKinds.Field]);
            hasFieldMarshalEncoding.Add(CliMetadataHasFieldMarshalTag.Param, tableLookup[CliMetadataTableKinds.Parameter]);

            hasDeclaredSecurityEncoding.Add(CliMetadataHasDeclSecurityTag.Assembly, tableLookup[CliMetadataTableKinds.Assembly]);
            hasDeclaredSecurityEncoding.Add(CliMetadataHasDeclSecurityTag.MethodDefinition, tableLookup[CliMetadataTableKinds.MethodDefinition]);
            hasDeclaredSecurityEncoding.Add(CliMetadataHasDeclSecurityTag.TypeDefinition, tableLookup[CliMetadataTableKinds.TypeDefinition]);

            memberReferenceParentEncoding.Add(CliMetadataMemberRefParentTag.MethodDefinition, tableLookup[CliMetadataTableKinds.MethodDefinition]);
            memberReferenceParentEncoding.Add(CliMetadataMemberRefParentTag.ModuleReference, tableLookup[CliMetadataTableKinds.ModuleReference]);
            memberReferenceParentEncoding.Add(CliMetadataMemberRefParentTag.TypeDefinition, tableLookup[CliMetadataTableKinds.TypeDefinition]);
            memberReferenceParentEncoding.Add(CliMetadataMemberRefParentTag.TypeReference, tableLookup[CliMetadataTableKinds.TypeReference]);
            memberReferenceParentEncoding.Add(CliMetadataMemberRefParentTag.TypeSpecification,
                                          tableLookup[CliMetadataTableKinds.TypeSpecification]);

            hasSemanticsEncoding.Add(CliMetadataHasSemanticsTag.Event, tableLookup[CliMetadataTableKinds.Event]);
            hasSemanticsEncoding.Add(CliMetadataHasSemanticsTag.Property, tableLookup[CliMetadataTableKinds.Property]);

            methodDefinitionOrReferenceEncoding.Add(CliMetadataMethodDefOrRefTag.MemberRef, tableLookup[CliMetadataTableKinds.MemberReference]);
            methodDefinitionOrReferenceEncoding.Add(CliMetadataMethodDefOrRefTag.MethodDefinition, tableLookup[CliMetadataTableKinds.MethodDefinition]);

            memberForwardedEncoding.Add(CliMetadataMemberForwardedTag.Field, tableLookup[CliMetadataTableKinds.Field]);
            memberForwardedEncoding.Add(CliMetadataMemberForwardedTag.MethodDefinition, tableLookup[CliMetadataTableKinds.MethodDefinition]);

            implementationEncoding.Add(CliMetadataImplementationTag.AssemblyReference, tableLookup[CliMetadataTableKinds.AssemblyReference]);
            implementationEncoding.Add(CliMetadataImplementationTag.ExportedType, tableLookup[CliMetadataTableKinds.ExportedType]);
            implementationEncoding.Add(CliMetadataImplementationTag.File, tableLookup[CliMetadataTableKinds.File]);

            customAttributeTypeEncoding.Add(CliMetadataCustomAttributeTypeTag.MemberReference, tableLookup[CliMetadataTableKinds.MemberReference]);
            customAttributeTypeEncoding.Add(CliMetadataCustomAttributeTypeTag.MethodDefinition, tableLookup[CliMetadataTableKinds.MethodDefinition]);
            customAttributeTypeEncoding.Add(CliMetadataCustomAttributeTypeTag.NotUsed1, null);
            customAttributeTypeEncoding.Add(CliMetadataCustomAttributeTypeTag.NotUsed2, null);
            customAttributeTypeEncoding.Add(CliMetadataCustomAttributeTypeTag.NotUsed3, null);

            resolutionScopeEncoding.Add(CliMetadataResolutionScopeTag.AssemblyReference, tableLookup[CliMetadataTableKinds.AssemblyReference]);
            resolutionScopeEncoding.Add(CliMetadataResolutionScopeTag.Module, tableLookup[CliMetadataTableKinds.Module]);
            resolutionScopeEncoding.Add(CliMetadataResolutionScopeTag.ModuleReference, tableLookup[CliMetadataTableKinds.ModuleReference]);
            resolutionScopeEncoding.Add(CliMetadataResolutionScopeTag.TypeReference, tableLookup[CliMetadataTableKinds.TypeReference]);

            typeOrMethodDefinitionEncoding.Add(CliMetadataTypeOrMethodDef.MethodDefinition, tableLookup[CliMetadataTableKinds.MethodDefinition]);
            typeOrMethodDefinitionEncoding.Add(CliMetadataTypeOrMethodDef.TypeDefinition, tableLookup[CliMetadataTableKinds.TypeDefinition]);

            List<IMetadataTableFieldEncodingDataType> encodings =
                new List<IMetadataTableFieldEncodingDataType>() 
                { 
                    typeDefinitionOrReferenceEncoding,
                    hasConstantEncoding,
                    hasCustomAttributeEncoding,
                    hasFieldMarshalEncoding,
                    hasDeclaredSecurityEncoding,
                    memberReferenceParentEncoding,
                    hasSemanticsEncoding,
                    methodDefinitionOrReferenceEncoding,
                    memberForwardedEncoding,
                    implementationEncoding,
                    customAttributeTypeEncoding,
                    resolutionScopeEncoding,
                    typeOrMethodDefinitionEncoding,
                };

            #endregion

            var resultedProject = new IntermediateProject("CliMetadataReader", defaultNamespaceName);
            var defaultNamespace = resultedProject.DefaultNameSpace;
            var defaultInternalNamespace = resultedProject.NameSpaces.AddNew(defaultInternalNamespaceName);
            /* *
             * var constructOverrides = 
             *      CreateDualInterfaces(resultedProject, 
             *      typeof(CliMetadataRoot), typeof(CliMetadataTableStreamAndHeader), typeof(CliMetadataStringsHeaderAndHeap),
             *      typeof(CliMetadataBlobHeaderAndHeap), typeof(CliMetadataUserStringsHeaderAndHeap), typeof(CliMetadataGuidHeaderAndHeap));
             * */
            var metadataRoot = typeof(ICliMetadataRoot).GetTypeReference().TypeInstance;
            var mutableMetadataRoot = typeof(ICliMetadataMutableRoot).GetTypeReference().TypeInstance;
            var tablesStream = defaultInternalNamespace.Partials.AddNew().Classes.AddNew("CliMetadataTableStreamAndHeader");
            tablesStream.Partials.AddNew();
            tablesStream.BaseType = typeof(ControlledDictionary<CliMetadataTableKinds, ICliMetadataTable>).GetTypeReference();
            tablesStream.ImplementsList.Add(typeof(ICliMetadataTableStreamAndHeader));
            var reservedA = tablesStream.Fields.AddNew(new TypedName("reservedA", typeof(uint)));
            var schemataVersion = tablesStream.Fields.AddNew(new TypedName("schemataVersion", typeof(WordVersion)));
            var heapSizes = tablesStream.Fields.AddNew(new TypedName("heapSizes", typeof(CliMetadataHeapSizes)));
            var reservedB = tablesStream.Fields.AddNew(new TypedName("reservedB", typeof(byte)));
            var tablesPresent = tablesStream.Fields.AddNew(new TypedName("tablesPresent", typeof(CliMetadataTableKinds)));
            var sortedTables = tablesStream.Fields.AddNew(new TypedName("sortedTables", typeof(CliMetadataTableKinds)));

            var tablesStreamConstructor = CreateTablesStreamCtor(tablesStream);
            var tablesStreamReadMethod = tablesStream.Methods.AddNew(new TypedName("Read", typeof(void)));
            tablesStreamReadMethod.AccessLevel = DeclarationAccessLevel.Internal;
            var tablesStreamReadMethod_reader = tablesStreamReadMethod.Parameters.AddNew(new TypedName("reader", typeof(EndianAwareBinaryReader)));
            var tablesStreamReadMethod_metadataRoot = tablesStreamReadMethod.Parameters.AddNew(new TypedName("metadataRoot", metadataRoot));
            //var tableSubstream = tablesStreamReadMethod.Locals.AddNew(new TypedName("tableSubstream", typeof(Substream)));
            //tableSubstream.InitializationExpression = new CreateNewObjectExpression(tableSubstream.LocalType, tablesStreamReadMethod_reader.GetReference().GetProperty("BaseStream"), PrimitiveExpression.NumberZero, PrimitiveExpression.NumberZero, PrimitiveExpression.FalseValue);

            //var readNewReader = tablesStreamReadMethod.Locals.AddNew(new TypedName("tableSubstreamReader", typeof(EndianAwareBinaryReader)));
            //readNewReader.InitializationExpression = new CreateNewObjectExpression(readNewReader.LocalType, tableSubstream.GetReference(), typeof(Endianness).GetTypeReferenceExpression().GetField("LittleEndian"), PrimitiveExpression.FalseValue);
            tablesStreamReadMethod.Add(new CommentStatement("Programs are best suited to this kind of code generation.\r\nLots of interconnected relationships, and lots of room for\r\nhuman error.  Thus, why this generator was created."));
            tablesStreamReadMethod.Statements.Add(new CommentStatement("Reserved, always 0."));
            tablesStreamReadMethod.Assign(reservedA.GetReference(), tablesStreamReadMethod_reader.GetReference().GetMethod("ReadUInt32").Invoke());
            tablesStreamReadMethod.Statements.Add(new CommentStatement("Shall be 2.0."));
            tablesStreamReadMethod.CallMethod(schemataVersion.GetReference().GetMethod("Read").Invoke(tablesStreamReadMethod_reader.GetReference()));
            tablesStreamReadMethod.Statements.Add(new CommentStatement("Bit vector for heap sizes."));
            tablesStreamReadMethod.Assign(heapSizes.GetReference(), tablesStreamReadMethod_reader.GetReference().GetMethod("ReadByte").Invoke().Cast(typeof(CliMetadataHeapSizes)));
            tablesStreamReadMethod.Statements.Add(new CommentStatement("Reserved, always 1."));
            tablesStreamReadMethod.Assign(reservedB.GetReference(), tablesStreamReadMethod_reader.GetReference().GetMethod("ReadByte").Invoke());

            var stringHeapSize = tablesStreamReadMethod.Locals.AddNew(new TypedName("stringHeapSize", typeof(CliMetadataReferenceIndexSize)));
            var stringHeapExpression = new BinaryOperationExpression(new BinaryOperationExpression(heapSizes.GetReference(), CodeBinaryOperatorType.BitwiseAnd, typeof(CliMetadataHeapSizes).GetTypeReferenceExpression().GetField("StringStream")), CodeBinaryOperatorType.IdentityEquality, typeof(CliMetadataHeapSizes).GetTypeReferenceExpression().GetField("StringStream"));
            stringHeapSize.AutoDeclare = false;
            tablesStreamReadMethod.Statements.Add(stringHeapSize.GetDeclarationStatement());
            var stringHeapCondition = tablesStreamReadMethod.IfThen(stringHeapExpression);
            stringHeapCondition.Assign(stringHeapSize.GetReference(), typeof(CliMetadataReferenceIndexSize).GetTypeReferenceExpression().GetField("DWord"));
            stringHeapCondition.FalseBlock.Assign(stringHeapSize.GetReference(), typeof(CliMetadataReferenceIndexSize).GetTypeReferenceExpression().GetField("Word"));
            var blobHeapSize = tablesStreamReadMethod.Locals.AddNew(new TypedName("blobHeapSize", typeof(CliMetadataReferenceIndexSize)));
            blobHeapSize.AutoDeclare = false;
            tablesStreamReadMethod.Statements.Add(blobHeapSize.GetDeclarationStatement());
            var blobHeapExpression = new BinaryOperationExpression(new BinaryOperationExpression(heapSizes.GetReference(), CodeBinaryOperatorType.BitwiseAnd, typeof(CliMetadataHeapSizes).GetTypeReferenceExpression().GetField("BlobStream")), CodeBinaryOperatorType.IdentityEquality, typeof(CliMetadataHeapSizes).GetTypeReferenceExpression().GetField("BlobStream"));
            var blobHeapCondition = tablesStreamReadMethod.IfThen(blobHeapExpression);
            blobHeapCondition.Assign(blobHeapSize.GetReference(), typeof(CliMetadataReferenceIndexSize).GetTypeReferenceExpression().GetField("DWord"));
            blobHeapCondition.FalseBlock.Assign(blobHeapSize.GetReference(), typeof(CliMetadataReferenceIndexSize).GetTypeReferenceExpression().GetField("Word"));
            var guidHeapSize = tablesStreamReadMethod.Locals.AddNew(new TypedName("guidHeapSize", typeof(CliMetadataReferenceIndexSize)));
            var guidHeapExpression = new BinaryOperationExpression(new BinaryOperationExpression(heapSizes.GetReference(), CodeBinaryOperatorType.BitwiseAnd, typeof(CliMetadataHeapSizes).GetTypeReferenceExpression().GetField("GuidStream")), CodeBinaryOperatorType.IdentityEquality, typeof(CliMetadataHeapSizes).GetTypeReferenceExpression().GetField("GuidStream"));
            guidHeapSize.AutoDeclare = false;
            tablesStreamReadMethod.Statements.Add(guidHeapSize.GetDeclarationStatement());
            var guidHeapCondition = tablesStreamReadMethod.IfThen(guidHeapExpression);
            guidHeapCondition.Assign(guidHeapSize.GetReference(), typeof(CliMetadataReferenceIndexSize).GetTypeReferenceExpression().GetField("DWord"));
            guidHeapCondition.FalseBlock.Assign(guidHeapSize.GetReference(), typeof(CliMetadataReferenceIndexSize).GetTypeReferenceExpression().GetField("Word"));

            tablesStreamReadMethod.Assign(tablesPresent.GetReference(), tablesStreamReadMethod_reader.GetReference().GetMethod("ReadUInt64").Invoke().Cast(typeof(CliMetadataTableKinds)));
            tablesStreamReadMethod.Assign(sortedTables.GetReference(), tablesStreamReadMethod_reader.GetReference().GetMethod("ReadUInt64").Invoke().Cast(typeof(CliMetadataTableKinds)));
            var metadataValidationCheck = tablesStreamReadMethod.IfThen(new BinaryOperationExpression(new BinaryOperationExpression(tablesPresent.GetReference(), CodeBinaryOperatorType.BitwiseAnd, typeof(CliMetadataTableKinds).GetTypeReferenceExpression().GetField("SupportedMask").Compliment()).Cast(typeof(ulong)), CodeBinaryOperatorType.IdentityInequality, PrimitiveExpression.NumberZero));
            metadataValidationCheck.Add(new ThrowStatement(new CreateNewObjectExpression(typeof(BadImageFormatException).GetTypeReference(), new ExpressionCollection(new PrimitiveExpression("Unsupported metadata type.")))));

            Dictionary<MetadataHeapTarget, ITypeReference> heapTypeReferences = new Dictionary<MetadataHeapTarget, ITypeReference>()
            {
                {
                    MetadataHeapTarget.BlobHeap,
                    typeof(byte[]).GetTypeReference()
                },
                {
                    MetadataHeapTarget.GuidHeap,
                    typeof(Guid).GetTypeReference()
                },
                {
                    MetadataHeapTarget.StringsHeap,
                    typeof(string).GetTypeReference()
                },
                {
                    MetadataHeapTarget.UserStringsHeap,
                    typeof(string).GetTypeReference()
                },
            };

            Dictionary<MetadataHeapTarget, IMemberParentExpression> heapReferenceExpressions = new Dictionary<MetadataHeapTarget, IMemberParentExpression>()
            {
                {
                    MetadataHeapTarget.BlobHeap,
                    new ThisReferenceExpression().GetField("metadataRoot").GetProperty("BlobHeap")
                },
                {
                    MetadataHeapTarget.GuidHeap,
                    new ThisReferenceExpression().GetField("metadataRoot").GetProperty("GuidHeap")
                },
                {
                    MetadataHeapTarget.StringsHeap,
                    new ThisReferenceExpression().GetField("metadataRoot").GetProperty("StringsHeap")
                },
                {
                    MetadataHeapTarget.UserStringsHeap,
                    new ThisReferenceExpression().GetField("metadataRoot").GetProperty("UserStringsHeap")
                },
            };
            var referencesTo = (from t in tables
                                from t2 in tables
                                from f in t.Values
                                where f.DataType == t2
                                group new { Table = t, Field = f } by t2).ToDictionary(k => k.Key, v => v.ToArray());

            var orderedTables = from t in tables
                                orderby t.Name
                                select t;
            var tableNamespace = defaultNamespace.ChildSpaces.AddNew("Tables");
            var internalTableNamespace = defaultInternalNamespace.ChildSpaces.AddNew("Tables");
            int maxTableCount = tables[tables.Count - 1].Offset + 1;

            for (int i = 0; i < encodings.Count; i++)
            {
                var currentSizeLocal = encodings[i].WordSizeLocal = tablesStreamReadMethod.Locals.AddNew(new TypedName(string.Format("enc{0}", encodings[i].Name), typeof(CliMetadataReferenceIndexSize)));
                currentSizeLocal.AutoDeclare = false;
                currentSizeLocal.InitializationExpression = typeof(CliMetadataReferenceIndexSize).GetTypeReferenceExpression().GetField("Word");
                var currentEncodingInterface = encodings[i].EncodingCommonType = tableNamespace.Partials.AddNew().Interfaces.AddNew(string.Format("ICliMetadata{0}Row", encodings[i].Name));
                currentEncodingInterface.Summary = string.Format("Defines the umbrella interface for reference indexes encoded with @s:{0};.", ((IExternTypeReference)encodings[i].EncodingType).TypeInstance.Type.Name);
                currentEncodingInterface.AccessLevel = DeclarationAccessLevel.Public;
            }

            var typeGroupingQuery = (from t in tables
                                     let fieldQuery =
                                         (from f in t.Values
                                          where
                                           f.DataType is IMetadataTableFieldHeapDataType ||
                                           f.DataType is IMetadataTableFieldEncodingDataType ||
                                           f.DataType is MetadataTable
                                          orderby f.DataType.ToString()
                                          group f by f.DataType).ToDictionary(p => p.Key, p => p.ToArray())
                                     //where fieldQuery.Count > 0
                                     orderby fieldQuery.Count descending,
                                             t.Name ascending
                                     select new { Table = t, VariableTypes = fieldQuery }).ToArray();
            var tablesArray = tables.ToArray();
            foreach (var tableDataGroups in typeGroupingQuery)
            {
                MetadataTableStateMachineInfo stateMachine = new MetadataTableStateMachineInfo(tablesArray, tables.IndexOf(tableDataGroups.Table));
                tableDataGroups.Table.StateMachine = stateMachine;
                foreach (var dataGroup in tableDataGroups.VariableTypes)
                {
                    var dataTypeSection = stateMachine.Add(dataGroup.Key);
                    foreach (var field in dataGroup.Value)
                        dataTypeSection.Add(field);
                }
            }

            List<IStatementBlockLocalMember> encodingLocalsDefined = new List<IStatementBlockLocalMember>();
            var offsetLocal = tablesStreamReadMethod.Locals.AddNew(new TypedName("currentOffset", typeof(long)));

            foreach (var table in orderedTables)
            {

                var staticReference = (from r in referencesTo
                                       where r.Key == table
                                       where r.Value.Length > 0
                                       select r).Count() > 0;
                var encodingReference = (from e in encodings
                                         where e.Contains(table)
                                         select e).Count() > 0;
                var currentTableClass = internalTableNamespace.Partials.AddNew().Classes.AddNew(string.Format("CliMetadata{0}TableReader", table.ShortName));
                var currentMutableTableClass = internalTableNamespace.Partials.AddNew().Classes.AddNew(string.Format("CliMetadata{0}MutableTable", table.ShortName));
                currentTableClass.AccessLevel = DeclarationAccessLevel.Internal;
                currentTableClass.Remarks = table.Remarks;
                var currentTableLockedInterface = tableNamespace.Partials.AddNew().Interfaces.AddNew(string.Format("ICliMetadata{0}Table", table.ShortName));
                var currentTableMutableInterface = internalTableNamespace.Partials.AddNew().Interfaces.AddNew(string.Format("ICliMetadata{0}MutableTable", table.ShortName));
                if (!string.IsNullOrEmpty(table.Summary))
                {
                    currentTableClass.Summary = string.Format("Provides a table which {0}", lowerFirst(table.Summary));
                    currentTableLockedInterface.Summary = string.Format("Defines properties and methods for a table which {0}", lowerFirst(table.Summary));
                }
                var currentTableClassCtor = currentTableClass.Constructors.AddNew();
                currentTableClassCtor.AccessLevel = DeclarationAccessLevel.Public;
                var kindProperty = currentTableClass.Properties.AddNew(new TypedName("Kind", typeof(CliMetadataTableKinds)), true, false);
                table.TableKindExpression = typeof(CliMetadataTableKinds).GetTypeReferenceExpression().GetField(table.Name);
                kindProperty.Overrides = true;
                kindProperty.GetPart.Return(table.TableKindExpression);
                kindProperty.AccessLevel = DeclarationAccessLevel.Public;
                var currentLockedMetadataRoot = currentTableClass.Fields.AddNew(new TypedName("metadataRoot", metadataRoot));
                var currentMutableMetadataRoot = currentMutableTableClass.Fields.AddNew(new TypedName("metadataRoot", mutableMetadataRoot));
                var currentReader = currentTableClass.Fields.AddNew(new TypedName("reader", typeof(EndianAwareBinaryReader)));
                var currentSync = currentTableClass.Fields.AddNew(new TypedName("syncObject", typeof(object)));
                var currentStream = currentTableClass.Fields.AddNew(new TypedName("fStream", typeof(FileStream)));
                var currentRowCount = currentTableClass.Fields.AddNew(new TypedName("rowCount", typeof(uint)));
                table.RowCountField = currentRowCount;
                var currentTableClassCtor_metadataRoot = currentTableClassCtor.Parameters.AddNew(new TypedName("metadataRoot", metadataRoot));
                var currentTableClassCtor_readerTriplet = currentTableClassCtor.Parameters.AddNew(new TypedName("readerInfo", typeof(Tuple<object, FileStream, EndianAwareBinaryReader>)));
                //var currentTableClassCtor_reader = currentTableClassCtor.Parameters.AddNew(new TypedName("reader", typeof(EndianAwareBinaryReader)));
                var currentTableClassCtor_rowCount = currentTableClassCtor.Parameters.AddNew(new TypedName("rowCount", typeof(uint)));
                currentTableClassCtor.CascadeExpressionsTarget = ConstructorCascadeTarget.Base;
                currentTableClassCtor.CascadeMembers.Add(currentTableClassCtor_metadataRoot.GetReference());
                currentTableClassCtor.CascadeMembers.Add(currentTableClassCtor_rowCount.GetReference());
                currentTableClassCtor.Statements.Assign(currentLockedMetadataRoot.GetReference(), currentTableClassCtor_metadataRoot.GetReference());
                currentTableClassCtor.Statements.Assign(currentSync.GetReference(), currentTableClassCtor_readerTriplet.GetReference().GetProperty("Item1"));
                currentTableClassCtor.Statements.Assign(currentStream.GetReference(), currentTableClassCtor_readerTriplet.GetReference().GetProperty("Item2"));
                currentTableClassCtor.Statements.Assign(currentReader.GetReference(), currentTableClassCtor_readerTriplet.GetReference().GetProperty("Item3"));
                currentTableClassCtor.Statements.Assign(currentRowCount.GetReference(), currentTableClassCtor_rowCount.GetReference());
                table.SyncField = currentSync;
                table.StreamField = currentStream;
                table.ReaderField = currentReader;
                currentTableLockedInterface.AccessLevel = DeclarationAccessLevel.Public;

                table.LockedMetadataRootField = currentLockedMetadataRoot;
                table.MutableMetadataRootField = currentMutableMetadataRoot;
                var currentLockedTableRowClass = internalTableNamespace.Partials.AddNew().Classes.AddNew(string.Format("CliMetadata{0}LockedTableRow", table.ShortName));
                var currentMutableTableRowClass = internalTableNamespace.Partials.AddNew().Classes.AddNew(string.Format("CliMetadata{0}MutableTableRow", table.ShortName));
                var currentTableLockedRowInterface = tableNamespace.Partials.AddNew().Interfaces.AddNew(string.Format("{0}Row", currentTableLockedInterface.Name));
                var currentTableMutableRowInterface = internalTableNamespace.Partials.AddNew().Interfaces.AddNew(string.Format("{0}Row", currentTableMutableInterface.Name));
                currentTableMutableRowInterface.ImplementsList.Add(currentTableLockedRowInterface.GetTypeReference());
                currentTableLockedRowInterface.AccessLevel = DeclarationAccessLevel.Public;
                table.DeclaredLockedTableRowInterface = currentTableLockedRowInterface;
                table.DeclaredMutableTableRowInterface = currentTableMutableRowInterface;

                table.DeclaredLockedTableInterface = currentTableLockedInterface;
                table.DeclaredMutableTableInterface = currentTableMutableInterface;

                table.DeclaredLockedTableRowClass = currentLockedTableRowClass;
                table.DeclaredMutableTableRowClass = currentMutableTableRowClass;

                table.DeclaredLockedTableRowClass.ImplementsList.Add(table.DeclaredLockedTableRowInterface.GetTypeReference());
                table.DeclaredLockedTableInterface.ImplementsList.Add(typeof(IControlledCollection<>).GetTypeReference(new TypeReferenceCollection(table.DeclaredLockedTableRowInterface.GetTypeReference())));
                table.DeclaredMutableTableInterface.ImplementsList.Add(typeof(IControlledCollection<>).GetTypeReference(new TypeReferenceCollection(table.DeclaredMutableTableRowInterface.GetTypeReference())));

                var mutableTableAddMethod = table.DeclaredMutableTableInterface.Methods.AddNew(new TypedName("Add", typeof(void)), new TypedName(string.Format("{0}ToAdd", lowerFirst(table.ShortName)), table.DeclaredMutableTableRowInterface.GetTypeReference()));
                var mutableTableRemoveMethod = table.DeclaredMutableTableInterface.Methods.AddNew(new TypedName("Remove", typeof(void)), new TypedName(string.Format("{0}ToRemove", lowerFirst(table.ShortName)), table.DeclaredMutableTableRowInterface.GetTypeReference()));


                table.DeclaredLockedTableRowInterface.ImplementsList.Add(typeof(ICliMetadataTableRow));
                currentTableClass.BaseType = typeof(CliMetadataLazyTable<>).GetTypeReference(new TypeReferenceCollection(currentTableLockedRowInterface.GetTypeReference()));
                currentLockedTableRowClass.AccessLevel = DeclarationAccessLevel.Internal;
                if (!string.IsNullOrEmpty(table.Summary))
                {
                    currentLockedTableRowClass.Summary = string.Format("Provides a locked row class for a locked table which {0}", lowerFirst(table.Summary));
                    currentTableLockedRowInterface.Summary = string.Format("Defines properties and methods for a locked row in a table which {0}", lowerFirst(table.Summary));

                    currentMutableTableRowClass.Summary = string.Format("Provides a mutable row class for a mutable table which {0}", lowerFirst(table.Summary));
                    currentTableMutableRowInterface.Summary = string.Format("Defines properties and methods for a mutable row in a table which {0}", lowerFirst(table.Summary));
                }
                var currentTableRowCtor = currentLockedTableRowClass.Constructors.AddNew();
                table.DeclaredTableRowCtor = currentTableRowCtor;
                table.DeclaredTableRowCtor.AccessLevel = DeclarationAccessLevel.Internal;
                table.DeclaredTableClassCtor = currentTableClassCtor;
                table.DeclaredTableClass = currentTableClass;
                table.DeclaredTableClass.ImplementsList.Add(typeof(ICliMetadataTable).GetTypeReference());
                table.DeclaredTableClass.ImplementsList.Add(table.DeclaredLockedTableInterface.GetTypeReference());

                if (staticReference || encodingReference || table.NeedsIndex)
                {
                    if (staticReference)
                    {
                        var currentWordSizeLocal = table.WordSizeLocal = tablesStreamReadMethod.Locals.AddNew(new TypedName(string.Format("{0}Size", lowerFirst(table.ShortName)), typeof(CliMetadataReferenceIndexSize)));
                        currentWordSizeLocal.AutoDeclare = false;
                        currentWordSizeLocal.InitializationExpression = typeof(CliMetadataReferenceIndexSize).GetTypeReferenceExpression().GetField("Word");
                    }
                    var currentTableIndexField = table.DeclaredLockedTableRowClass.Fields.AddNew(new TypedName("index", typeof(uint)));
                    var currentTableIndexProperty = table.DeclaredLockedTableRowClass.Properties.AddNew(new TypedName("Index", typeof(uint)), true, false);
                    currentTableIndexProperty.AccessLevel = DeclarationAccessLevel.Public;
                    currentTableIndexProperty.Summary = string.Format("Returns the index of the row within the @s:{0}; since the rows from the containing table are referenced by other tables.", table.DeclaredTableClass.Name);
                    currentTableIndexField.Summary = string.Format("Data member for @s:{0};.", currentTableIndexProperty.Name);
                    currentTableIndexProperty.GetPart.Return(currentTableIndexField.GetReference());
                    var indexParam = table.DeclaredTableRowCtor.Parameters.AddNew(new TypedName("index", typeof(uint)));
                    table.DeclaredTableRowCtor.Statements.Assign(currentTableIndexField.GetReference(), indexParam.GetReference());
                }
                //else if (!encodingReference)
                //    Console.WriteLine("No reference to: {0}", table.ShortName);
            }
            foreach (var table in tableLookup.Values)
            {
                var currentClass = table.DeclaredTableClass;
                var currentInterface = table.DeclaredLockedTableInterface;
                var tableRefField = tablesStream.Fields.AddNew(new TypedName(string.Format("{0}Table", lowerFirst(table.ShortName)), currentClass.GetTypeReference()));
                var tableRefProperty = tablesStream.Properties.AddNew(new TypedName(string.Format("{0}Table", table.ShortName), currentInterface.GetTypeReference()), true, false);
                var tableRefNullCheck = tableRefProperty.GetPart.IfThen(new BinaryOperationExpression(tableRefField.GetReference(), CodeBinaryOperatorType.IdentityEquality, PrimitiveExpression.NullValue));
                var tableRefLocal = tableRefNullCheck.Locals.AddNew(new TypedName(tableRefField.Name, typeof(ICliMetadataTable)));
                var ifCondition = tableRefNullCheck.IfThen(new ThisReferenceExpression().GetMethod("TryGetValue").Invoke(table.TableKindExpression, new DirectionExpression(FieldDirection.Out, tableRefLocal.GetReference())));
                ifCondition.Assign(tableRefField.GetReference(), tableRefLocal.GetReference().Cast(currentClass.GetTypeReference()));
                tableRefField.Summary = string.Format("Data member for @s:{0};.", tableRefProperty.Name);
                tableRefProperty.Summary = string.Format("Returns the @S:{0}; for the module.", table.DeclaredTableClass.Name);
                tableRefProperty.Remarks = "May return null if the metadata is not present in the module.";
                tableRefProperty.GetPart.Return(tableRefField.GetReference());
                tableRefProperty.AccessLevel = DeclarationAccessLevel.Public;
                table.MetadataProperty = tableRefProperty;
                var lockedMetadataRootField = table.DeclaredLockedTableRowClass.Fields.AddNew(new TypedName("metadataRoot", metadataRoot));
                var mutableMetadataRootField = table.DeclaredMutableTableRowClass.Fields.AddNew(new TypedName("metadataRoot", mutableMetadataRoot));
                table.RowLockedMetadataRootField = lockedMetadataRootField;
                table.RowMutableMetadataRootField = mutableMetadataRootField;

                lockedMetadataRootField.AccessLevel = DeclarationAccessLevel.Private;
                var lockedMetadataRootProperty = table.DeclaredLockedTableRowClass.Properties.AddNew(new TypedName("MetadataRoot", metadataRoot), true, false);
                lockedMetadataRootProperty.AccessLevel = DeclarationAccessLevel.Public;
                lockedMetadataRootProperty.Summary = string.Format("Returns the root of the metadata from which the current @s:{0}; was derived.", table.DeclaredLockedTableRowClass.Name);
                lockedMetadataRootProperty.GetPart.Return(lockedMetadataRootField.GetReference());
                var mutableMetadataRootProperty = table.DeclaredMutableTableRowClass.Properties.AddNew(new TypedName("MetadataRoot", mutableMetadataRoot), true, false);
                mutableMetadataRootProperty.AccessLevel = DeclarationAccessLevel.Public;
                mutableMetadataRootProperty.Summary = string.Format("Returns the root of the metadata from which the current @s:{0}; was derived.", table.DeclaredLockedTableRowClass.Name);
                mutableMetadataRootProperty.GetPart.Return(mutableMetadataRootField.GetReference());
                table.RowLockedMetadataRootProperty = lockedMetadataRootProperty;
                table.RowMutableMetadataRootProperty = mutableMetadataRootProperty;
            }

            foreach (var table in tableLookup.Values)
            {
                var toStringMethod = table.DeclaredLockedTableRowClass.Methods.AddNew(new TypedName("ToString", typeof(string)));
                toStringMethod.AccessLevel = DeclarationAccessLevel.Public;
                toStringMethod.Overrides = true;
                toStringMethod.IsFinal = false;
                StringBuilder formatStringBuilder = new StringBuilder();
                formatStringBuilder.AppendFormat("{0}: ", table.ShortName);
                IExpressionCollection formatArgs = new ExpressionCollection();
                int formatIndex = 0;
                var currentTableRowClass = table.DeclaredLockedTableRowClass;
                var primitiveFormat = new PrimitiveExpression(string.Empty);
                formatArgs.Add(primitiveFormat);
                foreach (var field in table.Values)
                {
                    switch (field.DataType.DataKind)
                    {
                        case FieldDataKind.Encoding:
                            var encodingType = field.DataType as IMetadataTableFieldEncodingDataType;
                            var encodedField = (field as IMetadataTableEncodedField);
                            field.FieldReference = currentTableRowClass.Fields.AddNew(new TypedName(string.Format("{0}Index", lowerFirst(field.FieldName)), typeof(uint)));
                            field.PropertyReference = currentTableRowClass.Properties.AddNew(new TypedName(field.FieldName, encodingType.EncodingCommonType), true, false);
                            field.PropertyIndexReference = currentTableRowClass.Properties.AddNew(new TypedName(string.Format("{0}Index", field.FieldName), typeof(uint)), true, false);
                            field.PropertyIndexReference.Summary = string.Format("Returns the decoded index of the @s:{0}; relative to the appropriate table.", field.PropertyReference.Name);
                            encodedField.EncodedField = currentTableRowClass.Fields.AddNew(new TypedName(lowerFirst(encodedField.EncodingIdName), encodingType.EncodingType));
                            encodedField.EncodingProperty = currentTableRowClass.Properties.AddNew(new TypedName(encodedField.EncodingIdName, encodingType.EncodingType), true, false);
                            field.PropertyIndexReference.Remarks = string.Format("Refer to @s:{0}; to discern the proper table for @s:{1};", encodedField.EncodingProperty.Name, field.PropertyIndexReference.Name);
                            encodedField.EncodedField.AccessLevel = DeclarationAccessLevel.Private;
                            field.PropertyIndexReference.AccessLevel = DeclarationAccessLevel.Public;
                            encodedField.EncodingProperty.AccessLevel = DeclarationAccessLevel.Public;
                            encodedField.EncodingProperty.GetPart.Return(encodedField.EncodedField.GetReference());
                            var ifZeroSwitch = field.PropertyReference.GetPart.IfThen(new BinaryOperationExpression(field.FieldReference.GetReference(), CodeBinaryOperatorType.IdentityEquality, PrimitiveExpression.NumberZero));
                            ifZeroSwitch.Return(PrimitiveExpression.NullValue);
                            var kindSwitch = field.PropertyReference.GetPart.SelectCase(encodedField.EncodedField.GetReference());

                            foreach (var target in encodingType.Values)
                            {
                                if (target.Item2 == null)
                                    continue;
                                var currentCase = kindSwitch.Cases.AddNew(target.Item1);

                                currentCase.Return(target.Item2.MetadataProperty.GetReference(table.RowLockedMetadataRootField.GetReference().GetProperty("TableStream")).GetIndex(field.FieldReference.GetReference().Cast(typeof(int))));
                            }
                            string encodingTypeString = encodingType.EncodingType.TypeInstance.GetTypeName(new IntermediateCodeTranslatorOptions(true));
                            encodedField.EncodingProperty.Summary = string.Format("Returns the @s:{0}; which determines the table that @s:{1}; refers to.", encodingTypeString, field.PropertyIndexReference.Name);
                            StringBuilder encodingPropertyRemarksBuilder = new StringBuilder();
                            encodingPropertyRemarksBuilder.AppendFormat("@s:{0}; encoding @s:{1}; tables:", encodingTypeString, tablesStream.Name);
                            encodingPropertyRemarksBuilder.Append("@table;");
                            encodingPropertyRemarksBuilder.Append("|:-Encoding-|-TableStream Property-|");
                            foreach (var tableKind in encodingType.Values)
                                if (tableKind.Item2 != null)
                                    encodingPropertyRemarksBuilder.AppendFormat("|-@s:{0};-|-@s:{1};-| ", tableKind.Item1, typeof(CliMetadataTableStreamAndHeader).GetTypeReferenceExpression().GetProperty(tableKind.Item2.MetadataProperty.Name));
                            encodingPropertyRemarksBuilder.Append("@/table;");
                            encodedField.EncodingProperty.Remarks = encodingPropertyRemarksBuilder.ToString();
                            field.FieldReference.AccessLevel = DeclarationAccessLevel.Private;
                            field.PropertyReference.AccessLevel = DeclarationAccessLevel.Public;
                            field.PropertyReference.GetPart.Return(PrimitiveExpression.NullValue);
                            break;
                        case FieldDataKind.HeapIndex:
                            field.FieldReference = currentTableRowClass.Fields.AddNew(new TypedName(string.Format("{0}Index", lowerFirst(field.FieldName)), typeof(uint).GetTypeReference()));
                            var heapTarget = ((IMetadataTableFieldHeapDataType)field.DataType).Heap;
                            string heapPropertyName = null;
                            switch (heapTarget)
                            {
                                case MetadataHeapTarget.StringsHeap:
                                    heapPropertyName = "CliMetadataRoot.StringsHeap";
                                    break;
                                case MetadataHeapTarget.UserStringsHeap:
                                    heapPropertyName = "CliMetadataRoot.UserStringHeap";
                                    break;
                                case MetadataHeapTarget.BlobHeap:
                                    heapPropertyName = "CliMetadataRoot.BlobHeap";
                                    break;
                                case MetadataHeapTarget.GuidHeap:
                                    heapPropertyName = "CliMetadataRoot.GuidHeap";
                                    break;
                                default:
                                    break;
                            }

                            if (field.DataType is IMetadataTableBlobHeapDataType)
                            {
                                var signatureBlobType = field.DataType as IMetadataTableBlobHeapDataType;
                                field.PropertyReference = currentTableRowClass.Properties.AddNew(new TypedName(field.FieldName, signatureBlobType.SignatureType), true, false);
                                field.PropertyReference.AccessLevel = DeclarationAccessLevel.Public;
                                field.PropertyReference.GetPart.Return(heapReferenceExpressions[heapTarget].GetMethod("GetSignature", signatureBlobType.SignatureType).Invoke(signatureBlobType.SignatureKind, field.FieldReference.GetReference()));
                                field.FieldReference.AccessLevel = DeclarationAccessLevel.Private;
                                field.PropertyIndexReference = currentTableRowClass.Properties.AddNew(new TypedName(string.Format("{0}Index", field.FieldName), typeof(uint)), true, false);
                            }
                            else
                            {
                                field.PropertyReference = currentTableRowClass.Properties.AddNew(new TypedName(field.FieldName, heapTypeReferences[heapTarget]), true, false);
                                field.PropertyReference.AccessLevel = DeclarationAccessLevel.Public;
                                field.PropertyReference.GetPart.Return(heapReferenceExpressions[heapTarget].GetIndex(field.FieldReference.GetReference()));
                                field.FieldReference.AccessLevel = DeclarationAccessLevel.Private;
                                field.PropertyIndexReference = currentTableRowClass.Properties.AddNew(new TypedName(string.Format("{0}Index", field.FieldName), typeof(uint)), true, false);
                            }
                            field.PropertyIndexReference.Summary = string.Format("Returns the index onto the @s:{0}; from which @s:{1}; is derived.", heapPropertyName, field.PropertyReference.Name);
                            if (formatIndex > 0)
                                formatStringBuilder.Append(", ");
                            formatStringBuilder.AppendFormat("{0} = {{{1}}}", field.PropertyReference.Name, formatIndex++);
                            formatArgs.Add(field.PropertyReference.GetReference());
                            break;
                        case FieldDataKind.DataType:
                        case FieldDataKind.CastDataType:
                        case FieldDataKind.SelfsufficientDataType:
                            field.FieldReference = currentTableRowClass.Fields.AddNew(new TypedName(lowerFirst(field.FieldName), ((MetadataTableTypeDataType)field.DataType).DataType));
                            field.PropertyReference = currentTableRowClass.Properties.AddNew(new TypedName(field.FieldName, ((MetadataTableTypeDataType)field.DataType).DataType), true, false);
                            field.PropertyReference.AccessLevel = DeclarationAccessLevel.Public;
                            field.PropertyReference.GetPart.Return(field.FieldReference.GetReference());
                            field.FieldReference.AccessLevel = DeclarationAccessLevel.Private;
                            if (field.DataType.DataKind != FieldDataKind.CastDataType)
                            {
                                if (formatIndex > 0)
                                    formatStringBuilder.Append(", ");
                                formatStringBuilder.AppendFormat("{0} = {{{1}}}", field.PropertyReference.Name, formatIndex++);
                                formatArgs.Add(field.PropertyReference.GetReference());
                            }
                            break;
                        case FieldDataKind.TableReference:
                            var fieldTable = ((MetadataTable)field.DataType);
                            var fieldType = tableLookup[fieldTable.TableKind].DeclaredLockedTableRowInterface;
                            field.FieldReference = currentTableRowClass.Fields.AddNew(new TypedName(string.Format("{0}Index", lowerFirst(field.FieldName)), typeof(uint).GetTypeReference()));
                            field.PropertyIndexReference = currentTableRowClass.Properties.AddNew(new TypedName(string.Format("{0}Index", field.FieldName), typeof(uint)), true, false);
                            field.PropertyIndexReference.Summary = field.IndexSummary;
                            field.PropertyReference = currentTableRowClass.Properties.AddNew(new TypedName(field.FieldName, fieldType), true, false);
                            field.PropertyReference.AccessLevel = DeclarationAccessLevel.Public;

                            field.PropertyReference.GetPart.Return(fieldTable.MetadataProperty.GetReference(table.RowLockedMetadataRootField.GetReference().GetProperty("TableStream")).GetIndex(field.FieldReference.GetReference().Cast(typeof(int))));
                            field.FieldReference.AccessLevel = DeclarationAccessLevel.Private;
                            break;
                        default:
                            return;
                    }
                    if (field.PropertyIndexReference != null)
                    {
                        field.PropertyIndexReference.GetPart.Return(field.FieldReference.GetReference());
                        field.PropertyIndexReference.AccessLevel = DeclarationAccessLevel.Public;
                    }
                    if (field.Summary != null)
                        field.PropertyReference.Summary = string.Format("Returns {0}", lowerFirst(field.Summary));
                    field.PropertyReference.Remarks = field.Remarks;
                    field.FieldReference.Summary = string.Format("Data member for @s:{0};.", field.PropertyReference.Name);
                }
                if (formatArgs.Count > 1)
                {
                    primitiveFormat.Value = formatStringBuilder.ToString();
                    toStringMethod.Return(typeof(string).GetTypeReferenceExpression().GetMethod("Format").Invoke(formatArgs.ToArray()));
                }
                else
                    currentTableRowClass.Methods.Remove(toStringMethod);

            }


            var addQuery =
                from t in tables
                orderby t.TableKind
                let encodingQuery =
                    from e in encodings
                    where e.Contains(t)
                    select e
                select new { Table = t, TableKind = t.TableKindExpression, StateMachine = t.StateMachine, ShortName = t.ShortName, WordSizeLocal = t.WordSizeLocal, Encodings = encodingQuery.ToArray(), RowClass = t.DeclaredLockedTableRowClass, RowInterface = t.DeclaredLockedTableRowInterface, Constructor = (CreateConstructorDelegate)t.DeclaredTableClass.GetTypeReference().CreateNew };

            //tablesStreamReadMethod.Assign(readCounter.GetReference(), PrimitiveExpression.NumberZero);
            bool firstNoEncode = true,
                 firstTagDescription = true;
            foreach (var tableInfo in addQuery)
            {
                tableInfo.Table.TableKindExpression = tableInfo.TableKind;
                var staticReferences = ArrayExtensions.MergeArrays((from r in referencesTo
                                                                    where r.Key == tableInfo.Table
                                                                    where r.Value.Length > 0
                                                                    select r.Value).ToArray());
                var encodedReferences = (from e in encodings
                                         where e.Contains(tableInfo.Table)
                                         select e).ToArray();
                if (staticReferences.Length > 0 || encodedReferences.Length > 0)
                {
                    if (tableInfo.WordSizeLocal != null)
                        tablesStreamReadMethod.Statements.Add(tableInfo.WordSizeLocal.GetDeclarationStatement());
                    if (staticReferences.Length > 0)
                        if (encodedReferences.Length > 0)
                            tablesStreamReadMethod.Statements.Add(new CommentStatement(string.Format("{0} is referenced by the following fields: {1} The following encodings reference it as well:\r\n{2}", tableInfo.ShortName, string.Join(", ", from s in staticReferences
                                                                                                                                                                                                                                                   select string.Format("{0}.{1}", s.Table.ShortName, s.Field.FieldName)), string.Join(", ", from e in encodedReferences
                                                                                                                                                                                                                                                                                                                                             select e.Name))));
                        else
                            tablesStreamReadMethod.Statements.Add(new CommentStatement(string.Format("{0} is referenced by the following fields: {1}", tableInfo.ShortName, string.Join(", ", from s in staticReferences
                                                                                                                                                                                              select string.Format("{0}.{1}", s.Table.ShortName, s.Field.FieldName)))));
                    else
                        tablesStreamReadMethod.Statements.Add(new CommentStatement(string.Format("The following encodings reference {0}: {1}", tableInfo.ShortName, string.Join(", ", from e in encodedReferences
                                                                                                                                                                                      select e.Name))));
                }
                else
                {
                    if (firstNoEncode)
                    {
                        firstNoEncode = false;
                        tablesStreamReadMethod.Statements.Add(new CommentStatement(string.Format("{0} is not referenced by anything, so setup is much simpler.", tableInfo.ShortName)));
                    }
                    else
                        tablesStreamReadMethod.Statements.Add(new CommentStatement(string.Format("{0} is not referenced by anything.", tableInfo.ShortName)));
                }

                tableInfo.Table.PresenceCheckCondition = new BinaryOperationExpression(new BinaryOperationExpression(tablesPresent.GetReference(), CodeBinaryOperatorType.BitwiseAnd, tableInfo.TableKind), CodeBinaryOperatorType.IdentityEquality, tableInfo.TableKind);
                var tablePresenceCheck = tablesStreamReadMethod.IfThen(tableInfo.Table.PresenceCheckCondition);
                if (encodedReferences.Length > 0 ||
                    staticReferences.Length > 0)
                {
                    var countLocal = tablePresenceCheck.Locals.AddNew(new TypedName(string.Format("{0}Count", lowerFirst(tableInfo.ShortName)), typeof(uint)));
                    countLocal.InitializationExpression = tablesStreamReadMethod_reader.GetReference().GetMethod("ReadUInt32").Invoke();
                    if (tableInfo.WordSizeLocal != null)
                    {
                        var tableSizeCheck = tablePresenceCheck.IfThen(new BinaryOperationExpression(countLocal.GetReference(), CodeBinaryOperatorType.GreaterThan, typeof(ushort).GetTypeReferenceExpression().GetField("MaxValue")));
                        tableSizeCheck.Assign(tableInfo.WordSizeLocal.GetReference(), typeof(CliMetadataReferenceIndexSize).GetTypeReferenceExpression().GetField("DWord"));
                    }
                    foreach (var encoding in tableInfo.Encodings)
                    {
                        tableInfo.RowInterface.ImplementsList.Add(encoding.EncodingCommonType);

                        bool encodingAdded = true;
                        if (!encodingLocalsDefined.Contains(encoding.WordSizeLocal))
                        {
                            tablesStreamReadMethod.Statements.Insert(tablesStreamReadMethod.Statements.IndexOf(tablePresenceCheck), encoding.WordSizeLocal.GetDeclarationStatement());
                            encodingLocalsDefined.Add(encoding.WordSizeLocal);
                            encodingAdded = false;
                        }
                        //var encodingSizeCheck = tablePresenceCheck.IfThen(new BinaryOperationExpression(tableInfo.CountLocal.GetReference(), CodeBinaryOperatorType.GreaterThan, encoding.WordSizeLocal.GetReference()));
                        var aPart = new BinaryOperationExpression(encoding.WordSizeLocal.GetReference(), CodeBinaryOperatorType.IdentityEquality, typeof(CliMetadataReferenceIndexSize).GetTypeReferenceExpression().GetField("Word"));
                        var bPart = new BinaryOperationExpression(countLocal.GetReference(), CodeBinaryOperatorType.GreaterThan, new PrimitiveExpression(ushort.MaxValue >> encoding.BitEncodingSize));
                        if (!encodingAdded)
                        {
                            if (firstTagDescription)
                            {
                                firstTagDescription = false;
                                tablePresenceCheck.Statements.Add(new CommentStatement(string.Format("It takes {0} bit{3} to encode indices with the {1} tag, so if the count for any target exceeds 2^{2}, use a DWord.", encoding.BitEncodingSize, encoding.Name, 16 - encoding.BitEncodingSize, encoding.BitEncodingSize == 1 ? string.Empty : "s")));
                            }
                            else
                                tablePresenceCheck.Statements.Add(new CommentStatement(string.Format("{1} tags take {0} bit{3} to encode, so if count exceeds 2^{2}, use a DWord.", encoding.BitEncodingSize, encoding.Name, 16 - encoding.BitEncodingSize, encoding.BitEncodingSize == 1 ? string.Empty : "s")));

                        }
                        var encodingSizeCheck = tablePresenceCheck.IfThen(new BinaryOperationExpression(aPart, CodeBinaryOperatorType.BooleanAnd, bPart));
                        encodingSizeCheck.Assign(encoding.WordSizeLocal.GetReference(), typeof(CliMetadataReferenceIndexSize).GetTypeReferenceExpression().GetField("DWord"));
                    }
                    tablePresenceCheck.CallMethod(new ThisReferenceExpression().GetMethod("_Add").Invoke(tableInfo.TableKind, tableInfo.Constructor(tablesStreamReadMethod_metadataRoot.GetReference(), tablesStreamReadMethod_metadataRoot.GetReference().GetProperty("SourceImage").GetMethod("SecureReader").Invoke(), countLocal.GetReference())));
                }
                else
                    tablePresenceCheck.CallMethod(new ThisReferenceExpression().GetMethod("_Add").Invoke(tableInfo.TableKind, tableInfo.Constructor(tablesStreamReadMethod_metadataRoot.GetReference(), tablesStreamReadMethod_metadataRoot.GetReference().GetProperty("SourceImage").GetMethod("SecureReader").Invoke(), tablesStreamReadMethod_reader.GetReference().GetMethod("ReadUInt32").Invoke())));

                if (tableInfo.StateMachine != null)
                    tableInfo.StateMachine.CreateStateMachine(encodings, metadataRoot);

            }
            var specialDataTypeTargets =
                    (from t in tables
                     from f in t.Values
                     where f.DataType is IMetadataTableFieldHeapDataType ||
                           f.DataType is IMetadataTableFieldEncodingDataType
                     group new { Table = t, Field = f } by f.DataType).ToDictionary(p => p.Key, p => (from tf in p
                                                                                                      group tf.Field by tf.Table).ToDictionary(
                                                                                                        k => k.Key,
                                                                                                        v => v.ToList()
                                                                                                        ));


            offsetLocal.InitializationExpression = tablesStreamReadMethod_reader.GetReference().GetProperty("BaseStream").GetProperty("Position");//PrimitiveExpression.NumberZero;//tablesStreamReadMethod_reader.GetReference().GetProperty("BaseStream").GetProperty("Position");
            offsetLocal.AutoDeclare = false;
            tablesStreamReadMethod.Add(offsetLocal.GetDeclarationStatement());
            foreach (var table in tableLookup.Values)
            {
                var presenceCondition = table.PresenceCheckCondition;
                var presenceCheck = tablesStreamReadMethod.IfThen(presenceCondition);
                var currentTableRef = presenceCheck.Locals.AddNew(new TypedName(string.Format("current{0}", table.ShortName), table.DeclaredTableClass));

                currentTableRef.InitializationExpression = table.MetadataProperty.GetReference().Cast(table.DeclaredTableClass.GetTypeReference());
                //new ThisReferenceExpression().GetIndex(table.TableKindExpression).Cast(table.DeclaredTableClass.GetTypeReference());


                var initializeMethodRef = table.InitializeMethod.GetReference(currentTableRef.GetReference()).Invoke(offsetLocal.GetReference());
                foreach (var kind in table.StateMachine.Values)
                {
                    switch (kind.DataType.DataKind)
                    {
                        case FieldDataKind.Encoding:
                            var encoding = kind.DataType as IMetadataTableFieldEncodingDataType;
                            initializeMethodRef.ArgumentExpressions.Add(encoding.WordSizeLocal.GetReference());
                            break;
                        case FieldDataKind.HeapIndex:
                            var heapType = kind.DataType as IMetadataTableFieldHeapDataType;
                            switch (heapType.Heap)
                            {
                                case MetadataHeapTarget.StringsHeap:
                                    initializeMethodRef.ArgumentExpressions.Add(stringHeapSize.GetReference());
                                    break;
                                case MetadataHeapTarget.BlobHeap:
                                    initializeMethodRef.ArgumentExpressions.Add(blobHeapSize.GetReference());
                                    break;
                                case MetadataHeapTarget.GuidHeap:
                                    initializeMethodRef.ArgumentExpressions.Add(guidHeapSize.GetReference());
                                    break;
                            }
                            break;
                        case FieldDataKind.TableReference:
                            var dataTypeTable = kind.DataType as MetadataTable;
                            initializeMethodRef.ArgumentExpressions.Add(dataTypeTable.WordSizeLocal.GetReference());
                            break;
                        default:
                            break;
                    }
                }
                //foreach (var encoding in encodings)
                //    if (encoding.Contains(table))
                //        initializeMethodRef.ArgumentExpressions.Add(encoding.WordSizeLocal.GetReference());
                presenceCheck.CallMethod(initializeMethodRef);
                presenceCheck.Assign(offsetLocal.GetReference(), new BinaryOperationExpression(offsetLocal.GetReference(), CodeBinaryOperatorType.Add, table.LengthProperty.GetReference(currentTableRef.GetReference())));
            }

            //tablesStreamReadMethod.Assign(tableSubstream.GetReference().GetProperty("Offset"), new BinaryOperationExpression(tableSubstream.GetReference().GetProperty("Offset"), CodeBinaryOperatorType.Add, tablesStreamReadMethod_reader.GetReference().GetProperty("BaseStream").GetProperty("Position")));
            //tablesStreamReadMethod.CallMethod(tableSubstream.GetReference().GetMethod("SetLength").Invoke(offsetLocal.GetReference()));
            
            ITypeReferenceExpression semanticsExpression = typeof(MethodSemanticsAttributes).GetTypeReferenceExpression();
            IExpression propertySemanticsMask = new BinaryOperationExpression(new BinaryOperationExpression(semanticsExpression.GetField("Getter"), CodeBinaryOperatorType.BitwiseOr, semanticsExpression.GetField("Setter")), CodeBinaryOperatorType.BitwiseOr, semanticsExpression.GetField("Other"));
            IExpression eventSemanticsMask = new BinaryOperationExpression(new BinaryOperationExpression(new BinaryOperationExpression(semanticsExpression.GetField("AddOn"), CodeBinaryOperatorType.BitwiseOr, semanticsExpression.GetField("RemoveOn")), CodeBinaryOperatorType.BitwiseOr, semanticsExpression.GetField("Fire")), CodeBinaryOperatorType.BitwiseOr, semanticsExpression.GetField("Other"));
            IExpression semanticsMask = propertySemanticsMask;
            IExpression semanticsTarget = semanticsExpression.GetField("Getter");
            var methodSemanticsInterface = tableLookup[CliMetadataTableKinds.MethodSemantics].DeclaredLockedTableRowInterface;
            var methodRowInterface = tableLookup[CliMetadataTableKinds.MethodDefinition].DeclaredLockedTableRowInterface;
            var propertyClass = tableLookup[CliMetadataTableKinds.Property].DeclaredLockedTableRowClass;
            var eventClass = tableLookup[CliMetadataTableKinds.Event].DeclaredLockedTableRowClass;
            CreateSemanticsMethodProperty(propertyClass, methodRowInterface, methodSemanticsInterface, "GetMethod", propertySemanticsMask, semanticsExpression.GetField("Getter"), tableLookup[CliMetadataTableKinds.Property].DeclaredLockedTableRowClass.Properties["Methods"].GetReference());
            CreateSemanticsMethodProperty(propertyClass, methodRowInterface, methodSemanticsInterface, "SetMethod", propertySemanticsMask, semanticsExpression.GetField("Setter"), tableLookup[CliMetadataTableKinds.Property].DeclaredLockedTableRowClass.Properties["Methods"].GetReference());
            CreateSemanticsMethodProperty(eventClass, methodRowInterface, methodSemanticsInterface, "OnAdd", eventSemanticsMask, semanticsExpression.GetField("AddOn"), tableLookup[CliMetadataTableKinds.Event].DeclaredLockedTableRowClass.Properties["Methods"].GetReference());
            CreateSemanticsMethodProperty(eventClass, methodRowInterface, methodSemanticsInterface, "OnRemove", eventSemanticsMask, semanticsExpression.GetField("RemoveOn"), tableLookup[CliMetadataTableKinds.Property].DeclaredLockedTableRowClass.Properties["Methods"].GetReference());
            CreateSemanticsMethodProperty(eventClass, methodRowInterface, methodSemanticsInterface, "OnFire", eventSemanticsMask, semanticsExpression.GetField("Fire"), tableLookup[CliMetadataTableKinds.Property].DeclaredLockedTableRowClass.Properties["Methods"].GetReference());

            var methodDefinition = tableLookup[CliMetadataTableKinds.MethodDefinition];
            var rvaField = methodDefinition["RVA"].FieldReference;

            var bodyProp = methodDefinition.DeclaredLockedTableRowClass.Properties.AddNew(new TypedName("Body", typeof(ICliMetadataMethodBody)), true, false);
            var bodyPropField = methodDefinition.DeclaredLockedTableRowClass.Fields.AddNew(new TypedName("body", typeof(ICliMetadataMethodBody)));
            var bodyPropCheck = methodDefinition.DeclaredLockedTableRowClass.Fields.AddNew(new TypedName("checkedBody", typeof(bool)));
            var typeDefRow = tableLookup[CliMetadataTableKinds.TypeDefinition].DeclaredLockedTableRowClass;

            typeDefRow.ImplementsList.Add(typeof(_ICliTypeParent));
            bodyProp.AccessLevel = DeclarationAccessLevel.Public;
            var bodyCheckedCondition = bodyProp.GetPart.IfThen(bodyPropCheck.GetReference().LogicalNot());
            var rvaValidCondition = bodyCheckedCondition.IfThen(new BinaryOperationExpression(rvaField.GetReference(), CodeBinaryOperatorType.IdentityInequality, PrimitiveExpression.NumberZero));
            rvaValidCondition.Assign(bodyPropField.GetReference(), new CreateNewObjectExpression(typeof(CliMetadataMethodBody).GetTypeReference(), methodDefinition.RowLockedMetadataRootField.GetReference(), rvaField.GetReference()));
            bodyCheckedCondition.Assign(bodyPropCheck.GetReference(), PrimitiveExpression.TrueValue);
            bodyProp.GetPart.Return(bodyPropField.GetReference());

            foreach (var table in tables)
            {
                DuplicateMembers(table.DeclaredLockedTableRowClass, table.DeclaredLockedTableRowInterface, table.DeclaredTableClass, table.DeclaredLockedTableInterface);
                if (table.DeclaredLockedTableRowInterface.Properties.ContainsKey("Index"))
                {
                    table.DeclaredLockedTableRowInterface.Properties.Remove("Index");
                    table.DeclaredLockedTableRowInterface.ImplementsList.Add(typeof(ICliMetadataIndexedRow));
                }
                DuplicateMembers(table.DeclaredTableClass, table.DeclaredLockedTableInterface, table.DeclaredLockedTableRowClass, table.DeclaredLockedTableRowInterface);
                table.DeclaredLockedTableRowInterface.Properties.Remove(table.DeclaredLockedTableRowInterface.Properties["Size"]);
                table.DeclaredLockedTableInterface.Properties.Remove(table.DeclaredLockedTableInterface.Properties["Kind"]);
            }
            //var oldTableStreamAndHeader = (IInterfaceType) constructOverrides[typeof(CliMetadataTableStreamAndHeader)];
            //oldTableStreamAndHeader.ParentTarget.Interfaces.Remove(oldTableStreamAndHeader);
            //var newTableStreamAndHeader = oldTableStreamAndHeader.ParentTarget.Interfaces.AddNew(oldTableStreamAndHeader.Name);
            //DuplicateMembers(tablesStream, newTableStreamAndHeader, tablesStream, newTableStreamAndHeader);
            //tablesStream.ImplementsList.Add(newTableStreamAndHeader);
            //newTableStreamAndHeader.AccessLevel = DeclarationAccessLevel.Public;

            /* *
             * Time for some post-construct analysis on the resulted types.
             * *
             * On encoded table rows which share a common encoding type, let's find the points
             * where they contain identical properties, when every type in the encoding set
             * contains the property, lift it into the encoding interface and remove it
             * from the original interface.
             * */
            var encodingOverlapQuery =
                (from encoding in encodings
                 let leftTable = ((from leftExpTable in encoding.Values
                                   where leftExpTable.Item2 != null
                                   select leftExpTable).First()).Item2
                 where leftTable != null
                 from leftProperty in leftTable.DeclaredLockedTableRowInterface.Properties.Values
                 let otherQuery = (from rightExpTable in encoding.Values
                                   let rightTable = rightExpTable.Item2
                                   where rightTable != null && leftTable != rightTable
                                   from rightProperty in rightTable.DeclaredLockedTableRowInterface.Properties.Values
                                   where leftProperty.Name == rightProperty.Name &&
                                         leftProperty.PropertyType.Equals(rightProperty.PropertyType)
                                   select rightProperty).ToArray()
                 where otherQuery.Length == encoding.Count() - 1
                 select new { Encoding = encoding, Property = leftProperty, Set = otherQuery }).Distinct().ToArray();
            HashSet<IPropertySignatureMember> removedSet = new HashSet<IPropertySignatureMember>();
            foreach (var encodingAndProperty in encodingOverlapQuery)
            {
                var currentEncoding = encodingAndProperty.Encoding;
                var currentProperty = encodingAndProperty.Property;
                var commonProperty = currentEncoding.EncodingCommonType.Properties.AddNew(new TypedName(currentProperty.Name, currentProperty.PropertyType), true, false);
                bool isList = currentProperty.PropertyType.TypeInstance.Equals(typeof(IReadOnlyCollection<>).GetTypeReference().TypeInstance);
                commonProperty.Summary = string.Format("Returns the @s:{0};{3} {2} associated to the row encoded with @s:{1};.", currentProperty.PropertyType.TypeInstance.GetTypeName(new IntermediateCodeTranslatorOptions(true), true), currentEncoding.EncodingType.TypeInstance.GetTypeName(new IntermediateCodeTranslatorOptions(true)), lowerFirst(currentProperty.Name), isList ? " of" : string.Empty);
                foreach (var item in encodingAndProperty.Set.Concat(new[] { encodingAndProperty.Property }))
                {
                    /* *
                     * A little 'Ambiguity' check, if multiple encodings lifted
                     * the same property out, we re-add it to rows which had it removed
                     * to avoid an ambiguity when the property is accessed in code.
                     * */
                    if (!removedSet.Contains(item))
                    {
                        item.ParentTarget.Properties.Remove(item);
                        removedSet.Add(item);
                    }
                    else if (!item.ParentTarget.Properties.Values.Contains(item))
                    {
                        item.HidesPrevious = true;
                        item.ParentTarget.Properties.Add(item);
                    }
                }
            }



            foreach (var encoding in encodings)
            {
                var currentEncodingProperty = encoding.EncodingCommonType.Properties.AddNew(new TypedName(string.Format("{0}Encoding", encoding.Name), encoding.EncodingType), true, false);
                currentEncodingProperty.Summary = string.Format("Returns the source table from which the current @s:{0}; is derived.", encoding.EncodingCommonType.Name);
                foreach (var expTable in encoding.Values)
                {
                    if (expTable.Item2 == null)
                        continue;
                    var currentTableProperty = expTable.Item2.DeclaredLockedTableRowClass.Properties.AddNew(new TypedName(currentEncodingProperty.Name, currentEncodingProperty.PropertyType), true, false);
                    currentTableProperty.GetPart.Return(expTable.Item1);
                    currentTableProperty.PrivateImplementationTarget = encoding.EncodingCommonType.GetTypeReference();
                }
            }
            /* *
             * Hard-coded requirement.
             * */
            var _typesProp = typeDefRow.Properties.AddNew(new TypedName("_Types", typeof(IControlledCollection<>).GetTypeReference(new TypeReferenceCollection(tableLookup[CliMetadataTableKinds.TypeDefinition].DeclaredLockedTableRowInterface.GetTypeReference()))), true, false);
            _typesProp.GetPart.Return(new ThisReferenceExpression().GetProperty("NestedClasses"));
            _typesProp.PrivateImplementationTarget = typeof(_ICliTypeParent).GetTypeReference();
            resultedProject.GetRootDeclaration().DefaultNameSpace = resultedProject.NameSpaces.AddNew("AllenCopeland.Abstraction.Slf");

            /* *
             * In debug mode, write to HTML, otherwise write to CSharp files.
             * *
             * HTML made my development easier so I didn't have to copy the files into the project
             * to notice errors or browse internal member definitions.
             * */

#if CS
            WriteProject(resultedProject, Path.GetDirectoryName(GetTypeAssemblyCodeBase(typeof(Program))));
#else
            WriteProject(resultedProject, Path.GetDirectoryName(GetTypeAssemblyCodeBase(typeof(Program))), ".html", "&nbsp;&nbsp;&nbsp;&nbsp;", true);
#endif

        }

        private static IDictionary<Type, IType> CreateDualInterfaces(IIntermediateProject project, params Type[] targets)
        {
            var result = new Dictionary<Type, IType>();
            //Start by creating the replacement interfaces.
            foreach (var originType in targets)
            {
                INameSpaceDeclaration @namespace = null;

                if (!project.NameSpaceExists(originType.Namespace, ref @namespace))
                    @namespace = project.NameSpaces.AddNew(originType.Namespace);
                else
                    @namespace = @namespace.Partials.AddNew();
                IInterfaceType rInterface;
                result.Add(originType, rInterface = @namespace.Interfaces.AddNew(string.Format("I{0}", originType.Name)));
                rInterface.AccessLevel = DeclarationAccessLevel.Public;
            }
            IDictionary<Type, IType> gPLookup = new Dictionary<Type, IType>();
            foreach (var kvOI in result.ToArray())
            {
                var originType = kvOI.Key;
                var @interface = (IInterfaceType)kvOI.Value;
                var properties = originType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                var methods = (from m in originType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                               where (m.Attributes & MethodAttributes.RTSpecialName) != MethodAttributes.RTSpecialName &&
                                     (m.Attributes & MethodAttributes.SpecialName) != MethodAttributes.SpecialName

                               select m).ToArray();
                foreach (var propertyInfo in properties)
                {
                    var parameterInfos = propertyInfo.GetIndexParameters();
                    if (parameterInfos.Length > 0)
                    {
                        var parameterTypedNames = new TypedName[parameterInfos.Length];
                        for (int i = 0; i < parameterInfos.Length; i++)
                            parameterTypedNames[i] = new TypedName(parameterInfos[i].Name, ConstructLookup(parameterInfos[i].ParameterType, result));
                        @interface.Properties.AddNew(new TypedName(propertyInfo.Name, ConstructLookup(propertyInfo.PropertyType, result)), propertyInfo.CanRead, propertyInfo.CanWrite, parameterTypedNames);
                    }
                    else
                        @interface.Properties.AddNew(new TypedName(propertyInfo.Name, ConstructLookup(propertyInfo.PropertyType, result)), propertyInfo.CanRead, propertyInfo.CanWrite);
                }
                foreach (var methodInfo in methods)
                {
                    var parameterInfos = methodInfo.GetParameters();
                    var parameterTypedNames = new TypedName[parameterInfos.Length];
                    var genericParameterInfos = methodInfo.GetGenericArguments();
                    var genericParameterConstrainedNames = new TypeConstrainedName[genericParameterInfos.Length];
                    for (int i = 0; i < genericParameterInfos.Length; i++)
                    {
                        var constraintInfos = genericParameterInfos[i].GetGenericParameterConstraints();
                        var constraintReferences = new ITypeReference[constraintInfos.Length];

                        for (int j = 0; j < constraintInfos.Length; j++)
                        {
                            if (constraintInfos[j] == typeof(ValueType))
                                continue;
                            constraintReferences[j] = ConstructLookup(constraintInfos[j], result);
                        }

                        genericParameterConstrainedNames[i] = new TypeConstrainedName(genericParameterInfos[i].Name, (from c in constraintReferences
                                                                                                                      where c != null
                                                                                                                      select c).ToArray());
                    }
                    for (int i = 0; i < parameterInfos.Length; i++)
                        parameterTypedNames[i] = new TypedName(parameterInfos[i].Name, ConstructLookup(parameterInfos[i].ParameterType, result));
                    var rMethod = @interface.Methods.AddNew(new TypedName(methodInfo.Name, ConstructLookup(methodInfo.ReturnType, result)), parameterTypedNames, genericParameterConstrainedNames);
                    for (int i = 0; i < genericParameterInfos.Length; i++)
                        gPLookup.Add(genericParameterInfos[i], rMethod.TypeParameters[i]);
                    foreach (var parameter in rMethod.Parameters.Values)
                    {
                        var pType = GPReplacement(gPLookup, parameter.ParameterType);
                        if (pType != parameter.ParameterType)
                            parameter.ParameterType = pType;
                    }
                    var rType = GPReplacement(gPLookup, rMethod.ReturnType);
                    if (rMethod.ReturnType != rType)
                        rMethod.ReturnType = rType;
                }
            }

            return result;
        }

        private static ITypeReference GPReplacement(IDictionary<Type, IType> gPLookup, ITypeReference pType)
        {
            IType tInst = pType.TypeInstance;
            if (tInst is IExternType)
            {
                var extType = (IExternType)tInst;
                var underlyingType = extType.Type;
                if (underlyingType.IsGenericParameter)
                    pType = ConstructLookup(underlyingType, gPLookup);
            }
            return pType;
        }

        private static ITypeReference ConstructLookup(Type sourceType, IDictionary<Type, IType> replacements)
        {
            return replacements.ContainsKey(sourceType) ? replacements[sourceType].GetTypeReference() : sourceType.GetTypeReference();
        }

        private static IInterfaceType CreateDualInterface(IIntermediateProject project, Type originType, string overriddenName = null)
        {
            if (overriddenName == null)
                overriddenName = string.Format("I{0}", originType.Name);
            var @namespace = project.NameSpaces.AddNew(originType.Namespace);
            var @interface = @namespace.Interfaces.AddNew(overriddenName);
            var properties = originType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            var methods = originType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (var propertyInfo in properties)
            {
                @interface.Properties.AddNew(new TypedName());
            }
            return @interface;
        }

        private static void CreateSemanticsMethodProperty(IClassType target, IInterfaceType methodRowInterface, IInterfaceType methodSemanticsRowInterface, string memberName, IExpression semanticsMask, IExpression semanticsTarget, IMemberParentExpression methodsSource)
        {
            var semanticsMethodProperty = target.Properties.AddNew(new TypedName(memberName, methodRowInterface.GetTypeReference()), true, false);
            var semanticsMethodCheckField = target.Fields.AddNew(new TypedName(string.Format("checked{0}", memberName), typeof(bool)));
            var semanticsMethodField = target.Fields.AddNew(new TypedName(lowerFirst(memberName), semanticsMethodProperty.PropertyType));
            var semanticsMethodCheck = semanticsMethodProperty.GetPart.IfThen(semanticsMethodCheckField.GetReference().LogicalNot());
            var semanticsMethodCheckIterator = semanticsMethodCheck.Enumerate(methodsSource, methodSemanticsRowInterface.GetTypeReference());
            semanticsMethodCheck.Assign(semanticsMethodCheckField.GetReference(), PrimitiveExpression.TrueValue);
            semanticsMethodCheckIterator.CurrentMember.Name = "semanticsMethod";
            var getterMethodIteratorCheck = semanticsMethodCheckIterator.IfThen(new BinaryOperationExpression(new BinaryOperationExpression(semanticsMethodCheckIterator.CurrentMember.GetReference().GetProperty("Semantics"), CodeBinaryOperatorType.BitwiseAnd, semanticsMask), CodeBinaryOperatorType.IdentityEquality, semanticsTarget));
            getterMethodIteratorCheck.Assign(semanticsMethodField.GetReference(), semanticsMethodCheckIterator.CurrentMember.GetReference().GetProperty("Method"));
            semanticsMethodProperty.GetPart.Return(semanticsMethodField.GetReference());
            semanticsMethodProperty.AccessLevel = DeclarationAccessLevel.Public;
        }



        private static void DuplicateMembers(IClassType source, IInterfaceType destination, IClassType tableClass, IInterfaceType tableInterface)
        {
            if (string.IsNullOrEmpty(destination.Summary))
                destination.Summary = source.Summary;
            if (string.IsNullOrEmpty(destination.Remarks))
                destination.Remarks = source.Remarks;
            foreach (var method in source.Methods.Values)
                if (method.AccessLevel != DeclarationAccessLevel.Public ||
                    method.Name == "ToString")
                    continue;
                else
                    DuplicateComments(destination.Methods.AddNew(new TypedName(method.Name, method.ReturnType), (from p in method.Parameters.Values
                                                                                                                 select new TypedName(p.Name, p.ParameterType)).ToArray()), method, tableClass, tableInterface);
            foreach (var property in source.Properties.Values)
                if (property.AccessLevel != DeclarationAccessLevel.Public)
                    continue;
                else if (property is IIndexerMember)
                    DuplicateComments(destination.Properties.AddNew(new TypedName(property.Name, property.PropertyType), property.HasGet, property.HasSet, (from p in ((IIndexerMember)property).Parameters.Values
                                                                                                                                                            select new TypedName(p.Name, p.ParameterType)).ToArray()), property, tableClass, tableInterface);
                else
                    DuplicateComments(destination.Properties.AddNew(new TypedName(property.Name, property.PropertyType), property.HasGet, property.HasSet), property, tableClass, tableInterface);

        }

        public static void DuplicateComments(IAutoCommentMember dest, IAutoCommentMember source, IClassType tableClass, IInterfaceType tableInterface)
        {
            if (source.Summary != null)
                dest.Summary = source.Summary.Replace(string.Format("@s:{0};", source.ParentTarget.Name), string.Format("@s:{0};", dest.ParentTarget.Name)).Replace(string.Format("@s:{0};", tableClass.Name), string.Format("@s:{0};", tableInterface.Name));
            if (source.Remarks != null)
                dest.Remarks = source.Remarks.Replace(string.Format("@s:{0};", source.ParentTarget.Name), string.Format("@s:{0};", dest.ParentTarget.Name)).Replace(string.Format("@s:{0};", tableClass.Name), string.Format("@s:{0};", tableInterface.Name));
            //#if CS
            //            source.Summary = null;
            //            dest.Summary = null;
            //#endif
        }

        public static IShiftExpression MakeLeftShiftCall(IExpression value, IExpression shift)
        {
            return new ShiftExpression() { RightSide = shift, LeftSide = value, Direction = ShiftDirection.Left };
        }

        public static IShiftExpression MakeRightShiftCall(IExpression value, IExpression shift)
        {
            return new ShiftExpression() { RightSide = shift, LeftSide = value, Direction = ShiftDirection.Right };
            // typeof(NumericCommon).GetTypeReferenceExpression().GetMethod("RightShift").Invoke(value, shift);
        }

        internal static string lowerFirst(string value)
        {
            char[] result = value.ToCharArray();
            int index = 0;
            while (index < result.Length && Char.IsUpper(result[index]))
                result[index] = char.ToLower(result[index++]);
            return new string(result, 0, result.Length);
        }

        private static int IndexItem(Dictionary<IMetadataTableFieldDataType, int> series, IMetadataTableFieldDataType dataType, ref int index)
        {
            if (!series.ContainsKey(dataType))
                series.Add(dataType, index++);
            return series[dataType];
        }

        delegate ICreateNewObjectExpression CreateConstructorDelegate(params IExpression[] parameters);

        private static IConstructorMember CreateTablesStreamCtor(IClassType tablesStream)
        {
            var ctor = tablesStream.Constructors.AddNew();
            var originalHeader = ctor.Parameters.AddNew(new TypedName("originalHeader", typeof(CliMetadataStreamHeader)));
            var data = ctor.Statements.Locals.AddNew(new TypedName("data", typeof(Tuple<uint, uint, string>)));
            ctor.AccessLevel = DeclarationAccessLevel.Internal;
            data.AutoDeclare = true;
            data.InitializationExpression = originalHeader.GetReference().GetMethod("GetData").Invoke();
            ctor.Statements.Assign(tablesStream.GetThisExpression().GetProperty("Offset"), data.GetReference().GetProperty("Item1"));
            ctor.Statements.Assign(tablesStream.GetThisExpression().GetProperty("Size"), data.GetReference().GetProperty("Item2"));
            ctor.Statements.Assign(tablesStream.GetThisExpression().GetProperty("Name"), data.GetReference().GetProperty("Item3"));
            return ctor;
        }

        public static void WriteProject(IIntermediateProject project, string targetDirectory, string fileExtension = ".cs", string tabString = "    ", bool htmlExportMode = false)
        {
            //Create the translator to use.
            CSharpCodeTranslator translator = new CSharpCodeTranslator();

            if (htmlExportMode)
            {
                var projectLineCounts = new Dictionary<IIntermediateProject, int>();
                translator.Options = new IntermediateCodeTranslatorOptions(true, IntermediateCodeTranslator.HTMLFormatter)
                {
                    GetFileNameOf =
                        (type) => ProjectTranslator.GetFileNameFor((IDeclaredType)type, targetDirectory, project, fileExtension, translator.Options, true),
                    GetLineNumber = p =>
                    {
                        if (!projectLineCounts.ContainsKey(p))
                            projectLineCounts.Add(p, 0);
                        return ++projectLineCounts[p];
                    }
                };
            }
            else
                translator.Options = new IntermediateCodeTranslatorOptions(true);
            translator.Options.SuppressTailGenerationMessage = true;
            translator.Options.AutoComments = true;
            translator.Options.AllowRegions = false;
            translator.Options.AutoRegions = AutoRegionAreas.None;
            TemporaryDirectory td;
            TempFileCollection tfc;
            List<string> files;
            Stack<IIntermediateProject> partialCompletions;
            Dictionary<IIntermediateModule, List<string>> moduleFiles;
            ProjectTranslator.WriteProject(project, translator, targetDirectory, out td, out tfc, out files, out partialCompletions, out moduleFiles, true, true, fileExtension, true, tabString);
        }


        private static string GetTypeAssemblyCodeBase(Type type)
        {
            var m = string.Join(string.Empty, from c in type.Assembly.CodeBase
                                              select c != '#' ? c.ToString() : Uri.HexEscape(c));
            var path3 = new Uri(m).LocalPath;
            return path3;
        }

    }
}
