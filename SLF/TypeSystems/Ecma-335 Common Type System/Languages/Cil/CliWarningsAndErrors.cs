using AllenCopeland.Abstraction.Slf.Compilers;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public static readonly ICompilerReferenceError CliMetadata001 = new CompilerReferenceError("The module table shall contain one and only one row.", 0x100001);
        /// <summary>
        /// Provides the reference error for an error within <see cref="ICliMetadataRoot">metadata</see> that occurs
        /// when the <see cref="ICliMetadataResolutionScopeRow.NameIndex"/> references an empty string.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata002 = new CompilerReferenceError("Name shall index a non-empty string. This string should match exactly any corresponding ModuleRef.Name string that resolves to this module.", 0x100002);
        /// <summary>
        /// Provides the reference error for an error within <see cref="ICliMetadataRoot">metadata</see> that occurs when
        /// the module version identifier does not reference a valid entry within the <see cref="ICliMetadataRoot.GuidHeap"/>.
        /// </summary>
        public static readonly ICompilerReferenceError CliMetadata003 = new CompilerReferenceError("Mvid shall index a non-null Guid in the Guid heap.", 0x100003);

        public static readonly ICompilerReferenceError CliMetadata011a = new CompilerReferenceError("When resolution scope is null, there shall be an ExportedType table row for this type ({0}).", 0x10011A);
        public static readonly ICompilerReferenceError CliMetadata011b = new CompilerReferenceError("When resolution scope is a typeref token, if the current typeref is a nested type ({0}).", 0x10011B);
        public static readonly ICompilerReferenceError CliMetadata011c = new CompilerReferenceError("When resolution scope is a moduleref token, the target type ({0}) is defined in another module within the same assembly.", 0x10011C);
        public static readonly ICliReferenceWarning CliMetadata011d = new CliReferenceWarning("When resolution scope is a module token, the type referenced ({0}) should be defined within the current module; though, this should not occur in a CLI (\"Compressed Metadata\") module.", CliWarningLevel.CliWarning, 0x10011D);
        public static readonly ICompilerReferenceError CliMetadata011e = new CompilerReferenceError("When resolution scope is an assemblyref, the type referenced ({0}) should be defined within another assembly other than the current module's assembly.", 0x10011E);
        public static readonly ICompilerReferenceError CliMetadata011f = new CompilerReferenceError("When resolution scope is an assemblyref, the type referenced ({0}) should be defined within another assembly ({1}) which cannot be found.", 0x10011F);
        public static readonly ICompilerReferenceError CliMetadata012 = new CompilerReferenceError("Name shall index a non-empty string within the StringHeap.", 0x100012);
        public static readonly ICompilerReferenceError CliMetadata014 = new CompilerReferenceError("Namespace shall index a non-empty string if not null.", 0x100014);
        public static readonly ICliReferenceWarning CliMetadata015 = new CliReferenceWarning("Name shall be in proper CLS Identifier form.", CliWarningLevel.ClsWarning, 0x100014);

        public static readonly ICompilerReferenceError CliMetadata201 = new CompilerReferenceError("The assembly table shall contain zero or one row only.", 0x100201);
        public static readonly ICompilerReferenceError CliMetadata202 = new CompilerReferenceError("HashAlgId shall be one of the specified values.", 0x100202);
        public static readonly ICompilerReferenceError CliMetadata204 = new CompilerReferenceError("Flags shall have only those values set that are specified.", 0x100204);
        public static readonly ICompilerReferenceError CliMetadata206 = new CompilerReferenceError("Name shall index a non-empty string in the String Heap.", 0x100206);
        public static readonly ICompilerReferenceError CliMetadata209 = new CompilerReferenceError("The current culture '{0}' is invalid, ECMA 335 says that: if Culture is non-null, it shall index a single string from the list specified in ECMA-335: II.23.1.3", 0x100209);
    }
}
