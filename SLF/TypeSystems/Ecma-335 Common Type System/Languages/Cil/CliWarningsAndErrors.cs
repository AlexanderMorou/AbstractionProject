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
        /// Provides the reference error for an error within <see cref="ICliMetadataRoot">metadata</see> that occurs 
        /// when there is more than one module or zero module entries within the
        /// modules table.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0001 = new CompilerReferenceError("The module table shall contain one and only one row.", 0x1000001);
        /// <summary>
        /// Provides the reference error for an error within <see cref="ICliMetadataRoot">metadata</see> that occurs
        /// when the <see cref="ICliMetadataResolutionScopeRow.NameIndex"/> references an empty string.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0002 = new CompilerReferenceError("Name shall index a non-empty string. This string should match exactly any corresponding ModuleRef.Name string that resolves to this module.", 0x1000002);
        /// <summary>
        /// Provides the reference error for an error within <see cref="ICliMetadataRoot">metadata</see> that occurs when
        /// the module version identifier does not reference a valid entry within the <see cref="ICliMetadataRoot.GuidHeap"/>.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata0003 = new CompilerReferenceError("Mvid shall index a non-null Guid in the Guid heap.", 0x1000003);

        public static readonly ICompilerReferenceError CliMetadata0101a = new CompilerReferenceError("When resolution scope is null, there shall be an ExportedType table row for this type ({0}).", 0x100101A);
        public static readonly ICompilerReferenceError CliMetadata0101b = new CompilerReferenceError("When resolution scope is a typeref token, if the current typeref is a nested type ({0}).", 0x100101B);
        public static readonly ICompilerReferenceError CliMetadata0101c = new CompilerReferenceError("When resolution scope is a moduleref token, the target type ({0}) is defined in another module within the same assembly.", 0x100101C);
        public static readonly ICliReferenceWarning CliMetadata0101d = new CliReferenceWarning("When resolution scope is a module token, the type referenced ({0}) should be defined within the current module; though, this should not occur in a CLI (\"Compressed Metadata\") module.", CliWarningLevel.CliWarning, 0x10011D);
        public static readonly ICompilerReferenceError CliMetadata0101e = new CompilerReferenceError("When resolution scope is an assemblyref, the type referenced ({0}) should be defined within another assembly other than the current module's assembly.", 0x100101E);
        public static readonly ICompilerReferenceError CliMetadata0101f = new CompilerReferenceError("When resolution scope is an assemblyref, the type referenced ({0}) should be defined within another assembly ({1}) which cannot be found.", 0x100101F);
        public static readonly ICompilerReferenceError CliMetadata0102 = new CompilerReferenceError("Name shall index a non-empty string within the StringHeap.", 0x1000102);
        public static readonly ICompilerReferenceError CliMetadata0104 = new CompilerReferenceError("Namespace shall index a non-empty string if not null.", 0x1000104);
        public static readonly ICliReferenceWarning CliMetadata0105 = new CliReferenceWarning("Name shall be in proper CLS Identifier form.", CliWarningLevel.ClsWarning, 0x1000105);

        public static readonly ICompilerReferenceError CliMetadata0202a = new CompilerReferenceError("Flags must contain only those values specified by System.Reflection.TypeAttributes", 0x1000202A);
        public static readonly ICompilerReferenceError CliMetadata0202b = new CompilerReferenceError("Sequential Layout and Explicit Layout cannot be set together.", 0x1000202B);
        public static readonly ICompilerReferenceError CliMetadata0202c = new CompilerReferenceError("UnicodeClass and AutoClass flags cannot be set together.", 0x1000202C);
        public static readonly ICompilerReferenceError CliMetadata0202d = new CompilerReferenceError("If HasSecurity is set, then the type must contain at least one DeclSecurity row, or a custom attribute called SuppressUnmanagedCodeSecurityAttribute.", 0x1000202D);
        public static readonly ICompilerReferenceError CliMetadata0202e = new CompilerReferenceError("If this type owns one (or more) DeclSecurity rows, then the HasSecurity flag must be set.", 0x1000202E);
        public static readonly ICompilerReferenceError CliMetadata0202f = new CompilerReferenceError("If this type has a custom attribute called SuppressUnmanagedCodeSecurityAttribute, then the HasSecurity flag must be set.", 0x1000202F);
        public static readonly ICliReferenceWarning CliMetadata0202g = new CliReferenceWarning("Interfaces are allowed to have the HasSecurity flag set; however, the security system ignores any permission requests attached to that interface.", CliWarningLevel.CliWarning, 0x10020210);
        public static readonly ICompilerReferenceError CliMetadata0203 = new CompilerReferenceError("Name shall index a non-empty string in the String Heap.", 0x1000203);
        public static readonly ICliReferenceWarning CliMetadata0204 = new CliReferenceWarning("Name shall be a valid CLS identifier.", CliWarningLevel.ClsWarning, 0x1000204);
        public static readonly ICompilerReferenceError CliMetadata0206 = new CompilerReferenceError("If non-null, Namespace shall index a non-empty string in the String Heap.", 0x1000206);
        public static readonly ICliReferenceWarning CliMetadata0207 = new CliReferenceWarning("If non-null, the Namespace string should be a valid CLS identifier.", CliWarningLevel.ClsWarning, 0x1000207);
        public static readonly ICompilerReferenceError CliMetadata0208 = new CompilerReferenceError("Every class (with exception to System.Object and the special class <Module>) shall extend one, and only one, other Class - so Extends is for a Class shall be non-null.", 0x1000208);
        public static readonly ICompilerReferenceError CliMetadata0209 = new CompilerReferenceError("System.Object must have an extends value of null.", 0x1000209);
        public static readonly ICompilerReferenceError CliMetadata0210 = new CompilerReferenceError("System.ValueType must have an extends value of System.Object", 0x1000210);
        public static readonly ICompilerReferenceError CliMetadata0211a = new CompilerReferenceError("With exception of System.Object and the special class <Module>, for any Class, extends shall be a valid row in the TypeDef, TypeRef, or TypeSpec table, where valid means 1 <= row <= rowCount.", 0x1000211A);
        public static readonly ICompilerReferenceError CliMetadata0211b = new CompilerReferenceError("The Extends target for the type shall be a class, not an interface, nor a ValueType.", 0x1000211B);
        public static readonly ICompilerReferenceError CliMetadata0211c = new CompilerReferenceError("The Extends target for the type cannot have its Sealed flag set.", 0x1000211C);
        public static readonly ICompilerReferenceError CliMetadata0212 = new CompilerReferenceError("A class shall not extend itself, or any of its children (i.e., its derived classes), because this will introduce loops in the hierarchy tree.", 0x1000212);
        public static readonly ICompilerReferenceError CliMetadata0213 = new CompilerReferenceError("The Extends target for an interface must be null, as interfaces do not derive any other type.  Interfaces can implement other interfaces; however, recall that this is handled the InterfaceImpl table, rather than the extends column.", 0x1000213);
        public static readonly ICompilerReferenceError CliMetadata0216 = new CompilerReferenceError("A ValueType shall have a non-zero size, either by defining at least one field, or by providing a non-zero ClassSize.", 0x1000216);
        public static readonly ICompilerReferenceError CliMetadata0217 = new CompilerReferenceError("If FieldList is non-null, it shall index a valid row in the Field table, where valid means 1 <= row <= rowCount + 1", 0x1000217);
        public static readonly ICompilerReferenceError CliMetadata0220 = new CompilerReferenceError("The runtime size of a ValueType shall not exceed 1 MByte (0x100000 bytes.)", 0x1000220);
        public static readonly ICompilerReferenceError CliMetadata0221 = new CompilerReferenceError("If MethodList is non-null, it shall index a valid row in the MethodDef table, where valid means 1 <= row <= rowCount + 1", 0x1000221);
        public static readonly ICompilerReferenceError CliMetadata0222 = new CompilerReferenceError("Classes with methods containing the abstract flag shall have their Abstract flag set.  This also applies if inherited Abstract methods are not overridden.", 0x1000222);
        public static readonly ICompilerReferenceError CliMetadata0223 = new CompilerReferenceError("Interfaces shall have the Abstract flag set.", 0x1000223);
        public static readonly ICompilerReferenceError CliMetadata0226 = new CompilerReferenceError("Interfaces can contain static fields, but not instance fields.", 0x1000226);
        public static readonly ICompilerReferenceError CliMetadata0227 = new CompilerReferenceError("An interface cannot be sealed.", 0x1000227);
        public static readonly ICompilerReferenceError CliMetadata0228 = new CompilerReferenceError("Methods on an interface must be marked abstract.", 0x1000228);
        public static readonly ICompilerReferenceError CliMetadata0229 = new CompilerReferenceError("No two types with the same Namespace and Name can be present, unless the type is nested.", 0x1000229);
        public static readonly ICompilerReferenceError CliMetadata0230 = new CompilerReferenceError("Nested types cannot have duplicate row entries in the type definition table.  Identical entries are based off of Namespace, Name, and the owner target within the NestedClass table.", 0x1000230);
        public static readonly ICliReferenceWarning CliMetadata0231 = new CliReferenceWarning("There shall be no Namespace + Name duplicate entries for non-nested types when compared using CLS conflicting-identifier-rules.", CliWarningLevel.ClsWarning, 0x1000231);
        public static readonly ICliReferenceWarning CliMetadata0232 = new CliReferenceWarning("There shall be no Namespace + Name duplicate entries for nested types, with the same parent, when compared using CLS conflicting-identifier-rules.", CliWarningLevel.ClsWarning, 0x1000231);
        public static readonly ICompilerReferenceError CliMetadata0233a = new CompilerReferenceError("If the type extends System.Enum the Sealed flag shall be set.", 0x1000233A);
        public static readonly ICompilerReferenceError CliMetadata0233b = new CompilerReferenceError("If the type extends System.Enum there shall not be any methods defined upon the type.", 0x1000233B);
        public static readonly ICompilerReferenceError CliMetadata0233c = new CompilerReferenceError("If the type extends System.Enum there shall be no InterfaceImpl table rows for this type.", 0x1000233C);
        public static readonly ICompilerReferenceError CliMetadata0233d = new CompilerReferenceError("If the type extends System.Enum there shall be no properties.", 0x1000233D);
        public static readonly ICompilerReferenceError CliMetadata0233e = new CompilerReferenceError("If the type extends System.Enum there shall be no events.", 0x1000233E);
        public static readonly ICompilerReferenceError CliMetadata0233f = new CompilerReferenceError("If the type extends System.Enum, all static fields shall have their Literal flag set.", 0x1000233F);
        public static readonly ICliReferenceWarning CliMetadata0233g = new CliReferenceWarning("If the type extends System.Enum, there shall be one or more static, literal fields, each of which have the same type as the Enum.", CliWarningLevel.ClsWarning, 0x10023310);
        public static readonly ICompilerReferenceError CliMetadata0233h = new CompilerReferenceError("If the type extends System.Enum, there shall be exactly one instance field of a built-in integer type.", 0x10023311);
        public static readonly ICliReferenceWarning CliMetadata0233i = new CliReferenceWarning("If the type extends System.Enum, the Name string of the instance field shall be \"value__\", the field shall be marked RTSpecialName, and that field shall have one of the CLS integer types.", CliWarningLevel.ClsWarning, 0x10023312);
        public static readonly ICompilerReferenceError CliMetadata0233j = new CompilerReferenceError("If the type extends System.Enum, there shall be no static fields unless they have their Literal flag set.", 0x10023313);
        public static readonly ICompilerReferenceError CliMetadata0234 = new CompilerReferenceError("A nested type shall own exactly one row within the nested class table as the nested class; that is: a nested type shall have exactly one parent.", 0x1000234);
        public static readonly ICompilerReferenceError CliMetadata0235 = new CompilerReferenceError("A ValueType shall be sealed", 0x1000235);


        public static readonly ICompilerReferenceError CliMetadata0402 = new CompilerReferenceError("Each row shall have one, and only one, owner row in the TypeDef table.", 0x1000402);
        public static readonly ICliReferenceWarning CliMetadata0403 = new CliReferenceWarning("The owner row in the TypeDef table shall not be an interface", CliWarningLevel.ClsWarning, 0x1000403);
        public static readonly ICompilerReferenceError CliMetadata0404 = new CompilerReferenceError("The fields Flags shall have only those values defined within FieldAttributes", 0x1000404);
        public static readonly ICompilerReferenceError CliMetadata0405 = new CompilerReferenceError("The subset of the flags on the field relative to the FieldAccessMask shall contain precisely one of: { CompilerControlled, Private, FamANDAssem, Assembly, Familiy, FamOrAssem, or Public }", 0x1000405);
        public static readonly ICompilerReferenceError CliMetadata0406 = new CompilerReferenceError("The fields flags can set either or neither of Literal or InitOnly, but not both.", 0x1000406);
        public static readonly ICompilerReferenceError CliMetadata0407 = new CompilerReferenceError("If the Literal flag is set then Static shall also be set.", 0x1000407);
        public static readonly ICompilerReferenceError CliMetadata0408 = new CompilerReferenceError("If the RTSpecialName flag is set then SpecialName shall also be set", 0x1000408);
        public static readonly ICompilerReferenceError CliMetadata0409 = new CompilerReferenceError("If the HasFieldMarshal flag is set, then this row shall own exactly one row in the FieldMarshal table.", 0x1000409);
        public static readonly ICompilerReferenceError CliMetadata0410 = new CompilerReferenceError("If the HasDefault flag is set then this row shall own exactly one row in the Constant table", 0x1000410);
        public static readonly ICompilerReferenceError CliMetadata0411 = new CompilerReferenceError("If the HasFieldRVA flag is set, then this row shall own exactly one row in the Field RVA table", 0x1000411);
        public static readonly ICompilerReferenceError CliMetadata0412 = new CompilerReferenceError("Name shall index a non-empty string in the String heap.", 0x1000412);
        public static readonly ICliReferenceWarning CliMetadata0413 = new CliReferenceWarning("The fields Name string shall be a valid CLS identifier.", CliWarningLevel.ClsWarning, 0x1000413);
        public static readonly ICompilerReferenceError CliMetadata0414 = new CompilerReferenceError("The fields Signature shall index a valid field signature in the Blob heap.", 0x1000414);
        public static readonly ICompilerReferenceError CliMetadata0416a = new CompilerReferenceError("If the field is a global field, then the Static flag shall be set.", 0x1000416A);
        public static readonly ICompilerReferenceError CliMetadata0416b = new CompilerReferenceError("If the field is a global field, then the subset of the flags on the field relative to the FieldAccessMask shall be exactly one of: { Public, CompilerControlled or Private }", 0x1000416b);
        public static readonly ICliReferenceWarning CliMetadata0416c = new CliReferenceWarning("Globally scoped fields are not valid relative to CLS compliance.", CliWarningLevel.ClsWarning, 0x1000416C);
        public static readonly ICompilerReferenceError CliMetadata0417 = new CompilerReferenceError("There shall be no duplicate rows in the Field table, based upon Owner, Name and Signature as the defining characteristic.  If the ComiplerControlled flag is set, this duplicate checking is omitted.", 0x1000417);
        public static readonly ICliReferenceWarning CliMetadata0418 = new CliReferenceWarning("There shall be no duplicate rows in the Field table, based upon owner and Name; however, if the CompilerControlled flag is set this check is omitted.", CliWarningLevel.ClsWarning, 0x1000418);

        public static readonly ICompilerReferenceError CliMetadata2001 = new CompilerReferenceError("The assembly table shall contain zero or one row only.", 0x1002001);
        public static readonly ICompilerReferenceError CliMetadata2002 = new CompilerReferenceError("HashAlgId shall be one of the specified values.", 0x1002002);
        public static readonly ICompilerReferenceError CliMetadata2004 = new CompilerReferenceError("Flags shall have only those values set that are specified.", 0x1002004);
        public static readonly ICompilerReferenceError CliMetadata2006 = new CompilerReferenceError("Name shall index a non-empty string in the String Heap.", 0x1002006);
        public static readonly ICompilerReferenceError CliMetadata2009 = new CompilerReferenceError("The current culture '{0}' is invalid, ECMA 335 says that: if Culture is non-null, it shall index a single string from the list specified in ECMA-335: II.23.1.3", 0x1002009);
        

    }
}
