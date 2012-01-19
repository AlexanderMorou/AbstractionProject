using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
using System.Diagnostics.SymbolStore;
using AllenCopeland.Abstraction.Slf.Languages.CSharp;
using AllenCopeland.Abstraction.Slf.Languages.Cil;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Provides the globally unique identifiers for language vendors and
    /// services.
    /// </summary>
    public static class LanguageGuids
    {
        /// <summary>
        /// The <see cref="Guid"/> for the <see cref="CSharpLanguage">C&#9839; Language</see>.
        /// </summary>
        public static readonly Guid CSharp = SymLanguageType.CSharp;
        /// <summary>
        /// The <see cref="Guid"/> for the Visual Basic.NET Language.
        /// </summary>
        public static readonly Guid VisualBasic = SymLanguageType.Basic;
        /// <summary>
        /// The <see cref="Guid"/> for the <see cref="Cil.CommonIntermediateLanguage">Common Intermediate Language</see>.
        /// </summary>
        public static readonly Guid CommonIntermediateLanguage = SymLanguageType.ILAssembly;
        /// <summary>
        /// The <see cref="Guid"/> for the Objectified Intermediate Language Lexical Analysis program generator, aka: OILexer.
        /// </summary>
        public static readonly Guid Oilexer = new Guid(0xED13FCAD, 0xE20F, 0x4C81, 0xA6, 0xFE, 0xAF, 0xAD, 0xE2, 0x99, 0xB9, 0xC0);
        /// <summary>
        /// Globally unique identifiers for the constructor
        /// services which aid in creating intermediate types,
        /// assemblies and so on.
        /// </summary>
        public static class ConstructorServices
        {
            /// <summary>
            /// The <see cref="IIntermediateClassType"/> creation service guid.
            /// </summary>
            /// <remarks><para>Resulted services must implement <see cref="IIntermediateTypeCtorLanguageService{TType}"/> with TType as <see cref="IIntermediateClassType"/>.</para>
            /// <para>Guid = { 0x30846e79, 0x2d39, 0x4824, { 0xb9, 0xfe, 0x1d, 0x80, 0x19, 0x46, 0x3, 0x73 } }</para></remarks>
            public static readonly Guid IntermediateClassCreatorService = new Guid(0x30846e79, 0x2d39, 0x4824, 0xb9, 0xfe, 0x1d, 0x80, 0x19, 0x46, 0x3, 0x73);
            /// <summary>
            /// The <see cref="IIntermediateDelegateType"/> creation service guid.
            /// </summary>
            /// <remarks>Guid = { 0xef710ca1, 0x316d, 0x4719, { 0xab, 0x86, 0xd3, 0xbd, 0x8e, 0x3f, 0x58, 0xd1 } }</remarks>
            public static readonly Guid IntermediateDelegateCreatorService = new Guid(0xef710ca1, 0x316d, 0x4719, 0xab, 0x86, 0xd3, 0xbd, 0x8e, 0x3f, 0x58, 0xd1);
            /// <summary>
            /// The <see cref="IIntermediateEnumType"/> creation service guid.
            /// </summary>
            /// <remarks>Guid = { 0xb154963f, 0x2ac, 0x4ae7, { 0x80, 0xf, 0xfd, 0x37, 0xdf, 0x69, 0x72, 0x27 } }</remarks>
            public static readonly Guid IntermediateEnumCreatorService = new Guid(0xb154963f, 0x2ac, 0x4ae7, 0x80, 0xf, 0xfd, 0x37, 0xdf, 0x69, 0x72, 0x27);
            /// <summary>
            /// The <see cref="IIntermediateInterfaceType"/> creation service guid.
            /// </summary>
            /// <remarks>Guid = { 0x191763b, 0xee19, 0x4ef9, { 0x9f, 0x2f, 0xed, 0x11, 0x4b, 0xf3, 0x88, 0xaa } }</remarks>
            public static readonly Guid IntermediateInterfaceCreatorService = new Guid(0x191763b, 0xee19, 0x4ef9, 0x9f, 0x2f, 0xed, 0x11, 0x4b, 0xf3, 0x88, 0xaa);
            // {35A208DB-30B7-40C1-9840-60106B1879C8}
            /// <summary>
            /// The <see cref="IIntermediateStructType"/> creation service guid.
            /// </summary>
            /// <remarks>
            /// Guid = { 0x35a208db, 0x30b7, 0x40c1, { 0x98, 0x40, 0x60, 0x10, 0x6b, 0x18, 0x79, 0xc8 } }</remarks>
            public static readonly Guid IntermediateStructCreatorService = new Guid(0x35a208db, 0x30b7, 0x40c1, 0x98, 0x40, 0x60, 0x10, 0x6b, 0x18, 0x79, 0xc8);
            /// <summary>
            /// The <see cref="IIntermediateAssembly"/> creation service guid.
            /// </summary>
            /// <remarks>Guid = { 0xcfa52a0e, 0x3c49, 0x4f99, { 0x8d, 0x40, 0xdc, 0x24, 0x9c, 0xe9, 0x56, 0x7c } }</remarks>
            public static readonly Guid IntermediateAssemblyCreatorService = new Guid(0xcfa52a0e, 0x3c49, 0x4f99, 0x8d, 0x40, 0xdc, 0x24, 0x9c, 0xe9, 0x56, 0x7c);
        }

        /// <summary>
        /// Globally unique identifiers for the language vendors.
        /// </summary>
        public static class Vendors
        {
            /// <summary>
            /// The <see cref="IAllenCopelandLanguageVendor"/> guid.
            /// </summary>
            /// <remarks>Guid = { 0x6A9AFE0B, 0x5C4A, 0x4FE8, { 0x9F, 0x2B, 0x91, 0x43, 0x4C, 0xE1, 0x29, 0xEB } }</remarks>
            public static readonly Guid AllenCopeland = new Guid(0x6A9AFE0B, 0x5C4A, 0x4FE8, 0x9F, 0x2B, 0x91, 0x43, 0x4C, 0xE1, 0x29, 0xEB);
            /// <summary>
            /// The <see cref="IMicrosoftLanguageVendor"/> guid.
            /// </summary>
            /// <remarks>Guid = <see cref="SymLanguageVendor.Microsoft"/>.</remarks>
            public static readonly Guid Microsoft = SymLanguageVendor.Microsoft;
        }
    }
}
