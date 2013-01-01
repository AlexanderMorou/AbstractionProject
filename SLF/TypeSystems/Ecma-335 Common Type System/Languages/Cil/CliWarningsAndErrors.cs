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
        public static readonly ICompilerReferenceError CliMetadata0202d = new CompilerReferenceError("If HasSecurity is set, then the type must contain at least one DeclSecurity row, or a custom attribute called SuppressedUnmanagedCodeSecurityAttribute.", 0x1000202D);
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

        public static readonly ICompilerReferenceError CliMetadata2001 = new CompilerReferenceError("The assembly table shall contain zero or one row only.", 0x1002001);
        public static readonly ICompilerReferenceError CliMetadata2002 = new CompilerReferenceError("HashAlgId shall be one of the specified values.", 0x1002002);
        public static readonly ICompilerReferenceError CliMetadata2004 = new CompilerReferenceError("Flags shall have only those values set that are specified.", 0x1002004);
        public static readonly ICompilerReferenceError CliMetadata2006 = new CompilerReferenceError("Name shall index a non-empty string in the String Heap.", 0x1002006);
        public static readonly ICompilerReferenceError CliMetadata2009 = new CompilerReferenceError("The current culture '{0}' is invalid, ECMA 335 says that: if Culture is non-null, it shall index a single string from the list specified in ECMA-335: II.23.1.3", 0x1002009);
    }
}
