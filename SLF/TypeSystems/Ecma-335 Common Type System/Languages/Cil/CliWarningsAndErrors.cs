using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Compilers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages.Cil
{
    public static class CliWarningsAndErrors
    {
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.Module"/> table:
        /// The module table shall contain one and only one row.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0001 = new CompilerReferenceError("The module table shall contain one and only one row.", 0x1000001);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.Module"/> table:
        /// Name shall index a non-empty string. This string should match exactly
        /// any corresponding ModuleRef.Name string that resolves to this module.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0002 = new CompilerReferenceError("Name shall index a non-empty string. This string should match exactly any corresponding ModuleRef.Name string that resolves to this module.", 0x1000002);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.Module"/> table:
        /// Mvid shall index a non-null Guid in the Guid heap.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0003 = new CompilerReferenceError("Mvid shall index a non-null Guid in the Guid heap.", 0x1000003);

        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeReference"/>
        /// table: When resolution scope is null, there shall be an ExportedType
        /// table row for this type (TYPE).
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0101a = new CompilerReferenceError("When resolution scope is null, there shall be an ExportedType table row for this type ({0}).", 0x100101a);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeReference"/>
        /// table: When resolution scope is a typeref token, if the current typeref
        /// is a nested type (TYPE).
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0101b = new CompilerReferenceError("When resolution scope is a typeref token, if the current typeref is a nested type ({0}).", 0x100101b);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeReference"/>
        /// table: When resolution scope is a moduleref token, the target type
        /// (TYPE) is defined in another module within the same assembly.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0101c = new CompilerReferenceError("When resolution scope is a moduleref token, the target type ({0}) is defined in another module within the same assembly.", 0x100101c);
        /// <summary>
        /// Provides a reference warning for a ECMA-335 metadata model warning
        /// which occurs within the <see cref="CliMetadataTableKinds.TypeReference"/>
        /// table: When resolution scope is a module token, the type referenced
        /// (TYPE_REFERENCE) should be defined within the current module; though, this should
        /// not occur in a CLI ("Compressed Metadata") module.
        /// </summary>
        public static readonly ICliReferenceWarning CliMetadata0101d = new CliReferenceWarning("When resolution scope is a module token, the type referenced ({0}) should be defined within the current module; though, this should not occur in a CLI (\"Compressed Metadata\") module.", CliWarningLevel.CliWarning, 0x10011d);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeReference"/>
        /// table: When resolution scope is an assemblyref, the type referenced
        /// (TYPE_REFERENCE) should be defined within another assembly other than the current
        /// module's assembly.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0101e = new CompilerReferenceError("When resolution scope is an assemblyref, the type referenced ({0}) should be defined within another assembly other than the current module's assembly.", 0x100101e);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeReference"/>
        /// table: When resolution scope is an assemblyref, the type referenced
        /// (TYPE_REFERENCE) should be defined within another assembly (ASSEMBLY_REFERENCE) which cannot
        /// be found.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0101f = new CompilerReferenceError("When resolution scope is an assemblyref, the type referenced ({0}) should be defined within another assembly ({1}) which cannot be found.", 0x100101f);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeReference"/>
        /// table: Name shall index a non-empty string within the StringHeap.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0102 = new CompilerReferenceError("Name shall index a non-empty string within the StringHeap.", 0x1000102);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeReference"/>
        /// table: Namespace shall index a non-empty string if not null.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0104 = new CompilerReferenceError("Namespace shall index a non-empty string if not null.", 0x1000104);
        /// <summary>
        /// Provides a reference warning for a ECMA-335 metadata model warning
        /// which occurs within the <see cref="CliMetadataTableKinds.TypeReference"/>
        /// table: Name shall be in proper CLS Identifier form.
        /// </summary>
        public static readonly ICliReferenceWarning CliMetadata0105 = new CliReferenceWarning("Name shall be in proper CLS Identifier form.", CliWarningLevel.ClsWarning, 0x1000105);

        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: Flags must contain only those values specified by System.Reflection.TypeAttributes
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0202a = new CompilerReferenceError("Flags must contain only those values specified by System.Reflection.TypeAttributes", 0x1000202a);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: Sequential Layout and Explicit Layout cannot be set together.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0202b = new CompilerReferenceError("Sequential Layout and Explicit Layout cannot be set together.", 0x1000202b);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: UnicodeClass and AutoClass flags cannot be set together.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0202c = new CompilerReferenceError("UnicodeClass and AutoClass flags cannot be set together.", 0x1000202c);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: If HasSecurity is set, then the type must contain at least one
        /// DeclSecurity row, or a custom attribute called SuppressUnmanagedCodeSecurityAttribute.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0202d = new CompilerReferenceError("If HasSecurity is set, then the type must contain at least one DeclSecurity row, or a custom attribute called SuppressUnmanagedCodeSecurityAttribute.", 0x1000202d);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: If this type owns one (or more) DeclSecurity rows, then the
        /// HasSecurity flag must be set.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0202e = new CompilerReferenceError("If this type owns one (or more) DeclSecurity rows, then the HasSecurity flag must be set.", 0x1000202e);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: If this type has a custom attribute called SuppressUnmanagedCodeSecurityAttribute,
        /// then the HasSecurity flag must be set.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0202f = new CompilerReferenceError("If this type has a custom attribute called SuppressUnmanagedCodeSecurityAttribute, then the HasSecurity flag must be set.", 0x1000202f);
        /// <summary>
        /// Provides a reference warning for a ECMA-335 metadata model warning
        /// which occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: Interfaces are allowed to have the HasSecurity flag set; however,
        /// the security system ignores any permission requests attached to that
        /// interface.
        /// </summary>
        public static readonly ICliReferenceWarning CliMetadata0202g = new CliReferenceWarning("Interfaces are allowed to have the HasSecurity flag set; however, the security system ignores any permission requests attached to that interface.", CliWarningLevel.CliWarning, 0x10020210);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: Name shall index a non-empty string in the String Heap.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0203 = new CompilerReferenceError("Name shall index a non-empty string in the String Heap.", 0x1000203);
        /// <summary>
        /// Provides a reference warning for a ECMA-335 metadata model warning
        /// which occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: Name shall be a valid CLS identifier.
        /// </summary>
        public static readonly ICliReferenceWarning CliMetadata0204 = new CliReferenceWarning("Name shall be a valid CLS identifier.", CliWarningLevel.ClsWarning, 0x1000204);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: If non-null, Namespace shall index a non-empty string in the
        /// String Heap.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0206 = new CompilerReferenceError("If non-null, Namespace shall index a non-empty string in the String Heap.", 0x1000206);
        /// <summary>
        /// Provides a reference warning for a ECMA-335 metadata model warning
        /// which occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: If non-null, the Namespace string should be a valid CLS identifier.
        /// </summary>
        public static readonly ICliReferenceWarning CliMetadata0207 = new CliReferenceWarning("If non-null, the Namespace string should be a valid CLS identifier.", CliWarningLevel.ClsWarning, 0x1000207);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: Every class (with exception to System.Object and the special
        /// class &lt;Module&gt;) shall extend one, and only one, other Class - so Extends
        /// is for a Class shall be non-null.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0208 = new CompilerReferenceError("Every class (with exception to System.Object and the special class <Module>) shall extend one, and only one, other Class - so Extends is for a Class shall be non-null.", 0x1000208);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: System.Object must have an extends value of null.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0209 = new CompilerReferenceError("System.Object must have an extends value of null.", 0x1000209);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: System.ValueType must have an extends value of System.Object
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0210 = new CompilerReferenceError("System.ValueType must have an extends value of System.Object", 0x1000210);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: With exception of System.Object and the special class &lt;Module&gt;,
        /// for any Class, extends shall be a valid row in the TypeDef, TypeRef,
        /// or TypeSpec table, where valid means 1 &lt;= row &lt;= rowCount.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0211a = new CompilerReferenceError("With exception of System.Object and the special class &lt;Module&gt;, for any Class, extends shall be a valid row in the TypeDef, TypeRef, or TypeSpec table, where valid means 1 <= row <= rowCount.", 0x1000211a);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: The Extends target for the type shall be a class, not an interface,
        /// nor a ValueType.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0211b = new CompilerReferenceError("The Extends target for the type shall be a class, not an interface, nor a ValueType.", 0x1000211b);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: The Extends target for the type cannot have its Sealed flag
        /// set.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0211c = new CompilerReferenceError("The Extends target for the type cannot have its Sealed flag set.", 0x1000211c);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: A class shall not extend itself, or any of its children (i.e.,
        /// its derived classes), because this will introduce loops in the hierarchy
        /// tree.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0212 = new CompilerReferenceError("A class shall not extend itself, or any of its children (i.e., its derived classes), because this will introduce loops in the hierarchy tree.", 0x1000212);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: The Extends target for an interface must be null, as interfaces
        /// do not derive any other type.  Interfaces can implement other interfaces;
        /// however, recall that this is handled the InterfaceImpl table, rather
        /// than the extends column.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0213 = new CompilerReferenceError("The Extends target for an interface must be null, as interfaces do not derive any other type.  Interfaces can implement other interfaces; however, recall that this is handled the InterfaceImpl table, rather than the extends column.", 0x1000213);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: A ValueType shall have a non-zero size, either by defining at
        /// least one field, or by providing a non-zero ClassSize.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0216 = new CompilerReferenceError("A ValueType shall have a non-zero size, either by defining at least one field, or by providing a non-zero ClassSize.", 0x1000216);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: If FieldList is non-null, it shall index a valid row in the
        /// Field table, where valid means 1 <= row <= rowCount + 1
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0217 = new CompilerReferenceError("If FieldList is non-null, it shall index a valid row in the Field table, where valid means 1 &lt;= row &lt;= rowCount + 1", 0x1000217);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: The runtime size of a ValueType shall not exceed 1 MByte (0x100000
        /// bytes.)
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0220 = new CompilerReferenceError("The runtime size of a ValueType shall not exceed 1 MByte (0x100000 bytes.)", 0x1000220);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: If MethodList is non-null, it shall index a valid row in the
        /// MethodDef table, where valid means 1 &lt;= row &lt;= rowCount + 1
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0221 = new CompilerReferenceError("If MethodList is non-null, it shall index a valid row in the MethodDef table, where valid means 1 <= row <= rowCount + 1", 0x1000221);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: Classes with methods containing the abstract flag shall have
        /// their Abstract flag set.  This also applies if inherited Abstract methods
        /// are not overridden.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0222 = new CompilerReferenceError("Classes with methods containing the abstract flag shall have their Abstract flag set.  This also applies if inherited Abstract methods are not overridden.", 0x1000222);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: Interfaces shall have the Abstract flag set.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0223 = new CompilerReferenceError("Interfaces shall have the Abstract flag set.", 0x1000223);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: Interfaces can contain static fields, but not instance fields.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0226 = new CompilerReferenceError("Interfaces can contain static fields, but not instance fields.", 0x1000226);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: An interface cannot be sealed.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0227 = new CompilerReferenceError("An interface cannot be sealed.", 0x1000227);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: Methods on an interface must be marked abstract.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0228 = new CompilerReferenceError("Methods on an interface must be marked abstract.", 0x1000228);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: No two types with the same Namespace and Name can be present,
        /// unless the type is nested.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0229 = new CompilerReferenceError("No two types with the same Namespace and Name can be present, unless the type is nested.", 0x1000229);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: Nested types cannot have duplicate row entries in the type definition
        /// table.  Identical entries are based off of Namespace, Name, and the
        /// owner target within the NestedClass table.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0230 = new CompilerReferenceError("Nested types cannot have duplicate row entries in the type definition table.  Identical entries are based off of Namespace, Name, and the owner target within the NestedClass table.", 0x1000230);
        /// <summary>
        /// Provides a reference warning for a ECMA-335 metadata model warning
        /// which occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: There shall be no Namespace + Name duplicate entries for non-nested
        /// types when compared using CLS conflicting-identifier-rules.
        /// </summary>
        public static readonly ICliReferenceWarning CliMetadata0231 = new CliReferenceWarning("There shall be no Namespace + Name duplicate entries for non-nested types when compared using CLS conflicting-identifier-rules.", CliWarningLevel.ClsWarning, 0x1000231);
        /// <summary>
        /// Provides a reference warning for a ECMA-335 metadata model warning
        /// which occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: There shall be no Namespace + Name duplicate entries for nested
        /// types, with the same parent, when compared using CLS conflicting-identifier-rules.
        /// </summary>
        public static readonly ICliReferenceWarning CliMetadata0232 = new CliReferenceWarning("There shall be no Namespace + Name duplicate entries for nested types, with the same parent, when compared using CLS conflicting-identifier-rules.", CliWarningLevel.ClsWarning, 0x1000231);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: If the type extends System.Enum the Sealed flag shall be set.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0233a = new CompilerReferenceError("If the type extends System.Enum the Sealed flag shall be set.", 0x1000233a);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: If the type extends System.Enum there shall not be any methods
        /// defined upon the type.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0233b = new CompilerReferenceError("If the type extends System.Enum there shall not be any methods defined upon the type.", 0x1000233b);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: If the type extends System.Enum there shall be no InterfaceImpl
        /// table rows for this type.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0233c = new CompilerReferenceError("If the type extends System.Enum there shall be no InterfaceImpl table rows for this type.", 0x1000233c);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: If the type extends System.Enum there shall be no properties.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0233d = new CompilerReferenceError("If the type extends System.Enum there shall be no properties.", 0x1000233d);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: If the type extends System.Enum there shall be no events.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0233e = new CompilerReferenceError("If the type extends System.Enum there shall be no events.", 0x1000233e);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: If the type extends System.Enum, all static fields shall have
        /// their Literal flag set.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0233f = new CompilerReferenceError("If the type extends System.Enum, all static fields shall have their Literal flag set.", 0x1000233f);
        /// <summary>
        /// Provides a reference warning for a ECMA-335 metadata model warning
        /// which occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: If the type extends System.Enum, there shall be one or more
        /// static, literal fields, each of which have the same type as the Enum.
        /// </summary>
        public static readonly ICliReferenceWarning CliMetadata0233g = new CliReferenceWarning("If the type extends System.Enum, there shall be one or more static, literal fields, each of which have the same type as the Enum.", CliWarningLevel.ClsWarning, 0x10023310);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: If the type extends System.Enum, there shall be exactly one
        /// instance field of a built-in integer type. 
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0233h = new CompilerReferenceError("If the type extends System.Enum, there shall be exactly one instance field of a built-in integer type.", 0x10023311);
        /// <summary>
        /// Provides a reference warning for a ECMA-335 metadata model warning
        /// which occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: If the type extends System.Enum, the Name string of the instance
        /// field shall be "value__", the field shall be marked RTSpecialName,
        /// and that field shall have one of the CLS integer types.
        /// </summary>
        public static readonly ICliReferenceWarning CliMetadata0233i = new CliReferenceWarning("If the type extends System.Enum, the Name string of the instance field shall be \"value__\", the field shall be marked RTSpecialName, and that field shall have one of the CLS integer types.", CliWarningLevel.ClsWarning, 0x10023312);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: If the type extends System.Enum, there shall be no static fields
        /// unless they have their Literal flag set.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0233j = new CompilerReferenceError("If the type extends System.Enum, there shall be no static fields unless they have their Literal flag set.", 0x10023313);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: A nested type shall own exactly one row within the nested class
        /// table as the nested class; that is: a nested type shall have exactly
        /// one parent.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0234 = new CompilerReferenceError("A nested type shall own exactly one row within the nested class table as the nested class; that is: a nested type shall have exactly one parent.", 0x1000234);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.TypeDefinition"/>
        /// table: A ValueType shall be sealed
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0235 = new CompilerReferenceError("A ValueType shall be sealed", 0x1000235);

        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.Field"/> table:
        /// Each row shall have one, and only one, owner row in the TypeDef table.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0402 = new CompilerReferenceError("Each row shall have one, and only one, owner row in the TypeDef table.", 0x1000402);
        /// <summary>
        /// Provides a reference warning for a ECMA-335 metadata model warning
        /// which occurs within the <see cref="CliMetadataTableKinds.Field"/> table:
        /// The owner row in the TypeDef table shall not be an interface
        /// </summary>
        public static readonly ICliReferenceWarning CliMetadata0403 = new CliReferenceWarning("The owner row in the TypeDef table shall not be an interface", CliWarningLevel.ClsWarning, 0x1000403);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.Field"/> table:
        /// The fields Flags shall have only those values defined within FieldAttributes
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0404 = new CompilerReferenceError("The fields Flags shall have only those values defined within FieldAttributes", 0x1000404);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.Field"/> table:
        /// The subset of the flags on the field relative to the FieldAccessMask
        /// shall contain precisely one of: { CompilerControlled, Private, FamANDAssem,
        /// Assembly, Familiy, FamOrAssem, or Public }
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0405 = new CompilerReferenceError("The subset of the flags on the field relative to the FieldAccessMask shall contain precisely one of: { CompilerControlled, Private, FamANDAssem, Assembly, Familiy, FamOrAssem, or Public }", 0x1000405);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.Field"/> table:
        /// The fields flags can set either or neither of Literal or InitOnly,
        /// but not both.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0406 = new CompilerReferenceError("The fields flags can set either or neither of Literal or InitOnly, but not both.", 0x1000406);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.Field"/> table:
        /// If the Literal flag is set then Static shall also be set.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0407 = new CompilerReferenceError("If the Literal flag is set then Static shall also be set.", 0x1000407);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.Field"/> table:
        /// If the RTSpecialName flag is set then SpecialName shall also be set
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0408 = new CompilerReferenceError("If the RTSpecialName flag is set then SpecialName shall also be set", 0x1000408);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.Field"/> table:
        /// If the HasFieldMarshal flag is set, then this row shall own exactly
        /// one row in the FieldMarshal table.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0409 = new CompilerReferenceError("If the HasFieldMarshal flag is set, then this row shall own exactly one row in the FieldMarshal table.", 0x1000409);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.Field"/> table:
        /// If the HasDefault flag is set then this row shall own exactly one row
        /// in the Constant table
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0410 = new CompilerReferenceError("If the HasDefault flag is set then this row shall own exactly one row in the Constant table", 0x1000410);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.Field"/> table:
        /// If the HasFieldRVA flag is set, then this row shall own exactly one
        /// row in the Field RVA table
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0411 = new CompilerReferenceError("If the HasFieldRVA flag is set, then this row shall own exactly one row in the Field RVA table", 0x1000411);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.Field"/> table:
        /// Name shall index a non-empty string in the String heap.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0412 = new CompilerReferenceError("Name shall index a non-empty string in the String heap.", 0x1000412);
        /// <summary>
        /// Provides a reference warning for a ECMA-335 metadata model warning
        /// which occurs within the <see cref="CliMetadataTableKinds.Field"/> table:
        /// The fields Name string shall be a valid CLS identifier.
        /// </summary>
        public static readonly ICliReferenceWarning CliMetadata0413 = new CliReferenceWarning("The fields Name string shall be a valid CLS identifier.", CliWarningLevel.ClsWarning, 0x1000413);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.Field"/> table:
        /// The fields Signature shall index a valid field signature in the Blob
        /// heap.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0414 = new CompilerReferenceError("The fields Signature shall index a valid field signature in the Blob heap.", 0x1000414);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.Field"/> table:
        /// If the field is a global field, then the Static flag shall be set.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0416a = new CompilerReferenceError("If the field is a global field, then the Static flag shall be set.", 0x1000416a);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.Field"/> table:
        /// If the field is a global field, then the subset of the flags on the
        /// field relative to the FieldAccessMask shall be exactly one of: { Public,
        /// CompilerControlled or Private }
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0416b = new CompilerReferenceError("If the field is a global field, then the subset of the flags on the field relative to the FieldAccessMask shall be exactly one of: { Public, CompilerControlled or Private }", 0x1000416b);
        /// <summary>
        /// Provides a reference warning for a ECMA-335 metadata model warning
        /// which occurs within the <see cref="CliMetadataTableKinds.Field"/> table:
        /// Globally scoped fields are not valid relative to CLS compliance.
        /// </summary>
        public static readonly ICliReferenceWarning CliMetadata0416c = new CliReferenceWarning("Globally scoped fields are not valid relative to CLS compliance.", CliWarningLevel.ClsWarning, 0x1000416c);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.Field"/> table:
        /// There shall be no duplicate rows in the Field table, based upon Owner,
        /// Name and Signature as the defining characteristic.  If the ComiplerControlled
        /// flag is set, this duplicate checking is omitted.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0417 = new CompilerReferenceError("There shall be no duplicate rows in the Field table, based upon Owner, Name and Signature as the defining characteristic.  If the ComiplerControlled flag is set, this duplicate checking is omitted.", 0x1000417);
        /// <summary>
        /// Provides a reference warning for a ECMA-335 metadata model warning
        /// which occurs within the <see cref="CliMetadataTableKinds.Field"/> table:
        /// There shall be no duplicate rows in the Field table, based upon owner
        /// and Name; however, if the CompilerControlled flag is set this check
        /// is omitted.
        /// </summary>
        public static readonly ICliReferenceWarning CliMetadata0418 = new CliReferenceWarning("There shall be no duplicate rows in the Field table, based upon owner and Name; however, if the CompilerControlled flag is set this check is omitted.", CliWarningLevel.ClsWarning, 0x1000418);

        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.Assembly"/> table:
        /// The assembly table shall contain zero or one row only.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata2001 = new CompilerReferenceError("The assembly table shall contain zero or one row only.", 0x1002001);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.Assembly"/> table:
        /// HashAlgId shall be one of the specified values.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata2002 = new CompilerReferenceError("HashAlgId shall be one of the specified values.", 0x1002002);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.Assembly"/> table:
        /// Flags shall have only those values set that are specified.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata2004 = new CompilerReferenceError("Flags shall have only those values set that are specified.", 0x1002004);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.Assembly"/> table:
        /// Name shall index a non-empty string in the String Heap.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata2006 = new CompilerReferenceError("Name shall index a non-empty string in the String Heap.", 0x1002006);
        /// <summary>
        /// Provides a reference error for a ECMA-335 metadata model error which
        /// occurs within the <see cref="CliMetadataTableKinds.Assembly"/> table:
        /// The current culture 'CULTURE' is invalid, ECMA 335 says that: if Culture
        /// is non-null, it shall index a single string from the list specified
        /// in ECMA-335: II.23.1.3
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata2009 = new CompilerReferenceError("The current culture '{0}' is invalid, ECMA 335 says that: if Culture is non-null, it shall index a single string from the list specified in ECMA-335: II.23.1.3", 0x1002009);
        

    }
}
