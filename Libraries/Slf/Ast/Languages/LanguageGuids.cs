using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
using System.Diagnostics.SymbolStore;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Provides the globally unique identifiers for language vendors and
    /// services.
    /// </summary>
    public static class LanguageGuids
    {
        /// <summary>
        /// The <see cref="Guid"/> for the C&#9839; Language.
        /// </summary>
        public static readonly Guid CSharp = SymLanguageType.CSharp;
        /// <summary>
        /// The <see cref="Guid"/> for the Visual Basic Language.
        /// </summary>
        public static readonly Guid VisualBasic = SymLanguageType.Basic;
        /// <summary>
        /// The <see cref="Guid"/> for the Common Intermediate Language.
        /// </summary>
        public static readonly Guid CommonIntermediateLanguage = SymLanguageType.ILAssembly;
        /// <summary>
        /// The <see cref="Guid"/> for the Objectified Intermediate Language Lexical Analysis program generator, aka: OILexer.
        /// </summary>
        public static readonly Guid Oilexer = new Guid(0xED13FCAD, 0xE20F, 0x4C81, 0xA6, 0xFE, 0xAF, 0xAD, 0xE2, 0x99, 0xB9, 0xC0);
        /// <summary>
        /// The <see cref="Guid"/> for the Toy language, aka: T*y++.
        /// </summary>
        public static readonly Guid ToySharp = new Guid(0x48E70036, 0xB1C8, 0x456C, 0xBB, 0xCD, 0xED, 0x5E, 0x3B, 0x5D, 0x48, 0x9D);

        /// <summary>
        /// Globally unique identifiers for the constructor
        /// services which aid in creating intermediate types,
        /// assemblies and so on.
        /// </summary>
        public static class Services
        {
            /// <summary>
            /// A service which aids in determining whether a method return type or 
            /// a set of methods follows the asynchronous pattern.
            /// </summary>
            /// <remarks>Guid = {0x270ef8ab, 0xf881, 0x4687, { 0xa7, 0xa2, 0x32, 0xaa, 0x66, 0x9b, 0xd2, 0x91 } }</remarks>
            public static readonly Guid AsyncQueryService                   = new Guid(0x270ef8ab, 0xf881, 0x4687, 0xa7, 0xa2, 0x32, 0xaa, 0x66, 0x9b, 0xd2, 0x91);
            /// <summary>
            /// A service which aids in marshaling metadatum definitions.
            /// </summary>
            /// <remarks>Guid = { 0xe701b831, 0x5a52, 0x47c1, { 0xa0, 0xf5, 0xa9, 0xb8, 0xb2, 0x7e, 0x55, 0xd3 } }</remarks>
            public static readonly Guid MetadatumMarshalService             = AbstractGateway.MetadatumMarshalServiceGuid;

            /// <summary>A service which aids in manipulating expressions.</summary>
            public static readonly Guid ExpressionService                   = new Guid(0xEAF216F7, 0x9B05, 0x4F67, 0xAA, 0x30, 0xFC, 0x44, 0x37, 0x73, 0xB3, 0x62);

            public static class ClassServices
            {
                /// <summary>
                /// The <see cref="IIntermediateClassType"/> creation service guid.
                /// </summary>
                /// <remarks><para>Resulted services must implement <see cref="IIntermediateTypeCtorLanguageService{TType}"/> with TType as <see cref="IIntermediateClassType"/>.</para>
                /// <para>Guid = { 0x30846e79, 0x2d39, 0x4824, { 0xb9, 0xfe, 0x1d, 0x80, 0x19, 0x46, 0x3, 0x73 } }</para></remarks>
                public static readonly Guid ClassCreatorService = new Guid(0x30846e79, 0x2d39, 0x4824, 0xb9, 0xfe, 0x1d, 0x80, 0x19, 0x46, 0x3, 0x73);
                /// <summary>
                /// The <see cref="IIntermediateBinaryOperatorCoercionMember"/> creation service guid for classes.
                /// </summary>
                /// <remarks>Guid = { 0xaec251bd, 0x9087, 0x43c6, { 0xa3, 0xf7, 0x82, 0x37, 0x72, 0x10, 0x73, 0x90 } }</remarks>
                public static readonly Guid BinaryOperatorCreatorService = new Guid(0xaec251bd, 0x9087, 0x43c6, 0xa3, 0xf7, 0x82, 0x37, 0x72, 0x10, 0x73, 0x90);
                /// <summary>
                /// The <see cref="IIntermediateClassCtorMember"/> creation service guid.
                /// </summary>
                /// <remarks>Guid = { 0x5b86bee, 0xf8ee, 0x4a46, { 0x8f, 0x10, 0xa4, 0xa1, 0xfa, 0x35, 0x6a, 0xec } }</remarks>
                public static readonly Guid ConstructorCreatorService = new Guid(0x5b86bee, 0xf8ee, 0x4a46, 0x8f, 0x10, 0xa4, 0xa1, 0xfa, 0x35, 0x6a, 0xec);
                /// <summary>
                /// The <see cref="IIntermediateClassEventMember"/> creation service guid.
                /// </summary>
                /// <remarks>Guid = { 0x31b8689c, 0xa850, 0x4bb7, { 0xad, 0xbc, 0x26, 0x43, 0x56, 0x35, 0xc5, 0x64 } }</remarks>
                public static readonly Guid EventCreatorService = new Guid(0x31b8689c, 0xa850, 0x4bb7, 0xad, 0xbc, 0x26, 0x43, 0x56, 0x35, 0xc5, 0x64);
                /// <summary>
                /// The <see cref="IIntermediateClassFieldMember"/> creation service guid.
                /// </summary>
                /// <remarks>Guid = { 0x592b375a, 0xb37f, 0x4891, { 0x8e, 0xa8, 0x59, 0x8d, 0x42, 0xcf, 0x20, 0x1c } }</remarks>
                public static readonly Guid FieldCreatorService = new Guid(0x592b375a, 0xb37f, 0x4891, 0x8e, 0xa8, 0x59, 0x8d, 0x42, 0xcf, 0x20, 0x1c);
                /// <summary>
                /// The <see cref="IIntermediateClassIndexerMember"/> creation service guid.
                /// </summary>
                /// <remarks>Guid = { 0x21de70ce, 0x9db4, 0x4089, { 0x94, 0xda, 0xdf, 0x81, 0x2d, 0x64, 0x93, 0x5b } }</remarks>
                public static readonly Guid IndexerCreatorService = new Guid(0x21de70ce, 0x9db4, 0x4089, 0x94, 0xda, 0xdf, 0x81, 0x2d, 0x64, 0x93, 0x5b);
                /// <summary>
                /// The <see cref="IIntermediateClassMethodMember"/> creation service guid.
                /// </summary>
                /// <remarks>Guid = { 0x9896a55c, 0x1b5f, 0x4a3d, { 0xb6, 0x79, 0xb8, 0xf6, 0x92, 0x59, 0x34, 0x6a } }</remarks>
                public static readonly Guid MethodCreatorService = new Guid(0x9896a55c, 0x1b5f, 0x4a3d, 0xb6, 0x79, 0xb8, 0xf6, 0x92, 0x59, 0x34, 0x6a);
                /// <summary>
                /// The <see cref="IIntermediateClassPropertyMember"/> creation service guid.
                /// </summary>
                /// <remarks>Guid = { 0x7a53b06a, 0xeaa5, 0x4784, { 0xb1, 0x52, 0x76, 0x55, 0xc, 0x7f, 0x9a, 0x24 } }</remarks>
                public static readonly Guid PropertyCreatorService = new Guid(0x7a53b06a, 0xeaa5, 0x4784, 0xb1, 0x52, 0x76, 0x55, 0xc, 0x7f, 0x9a, 0x24);
                /// <summary>
                /// The <see cref="IIntermediateTypeCoercionMember"/> creation service guid for classes.
                /// </summary>
                /// <remarks>Guid = { 0x852f17c1, 0x11ed, 0x4a6d, { 0xb5, 0xd9, 0xc4, 0x3c, 0x58, 0xfe, 0x7, 0xc } }</remarks>
                public static readonly Guid TypeCoercionCreatorService = new Guid(0x852f17c1, 0x11ed, 0x4a6d, 0xb5, 0xd9, 0xc4, 0x3c, 0x58, 0xfe, 0x7, 0xc);
                /// <summary>
                /// The <see cref="IIntermediateUnaryOperatorCoercionMember"/> creation service guid for classes.
                /// </summary>
                /// <remarks>Guid = { 0x3fd5d84d, 0x169a, 0x4373, { 0x8a, 0xfb, 0xae, 0x2a, 0x48, 0x73, 0x19, 0xcf } }</remarks>
                public static readonly Guid UnaryOperatorCreatorService = new Guid(0x3fd5d84d, 0x169a, 0x4373, 0x8a, 0xfb, 0xae, 0x2a, 0x48, 0x73, 0x19, 0xcf);
            }

            public static class StructServices
            {
                /// <summary>
                /// The <see cref="IIntermediateStructType"/> creation service guid.
                /// </summary>
                /// <remarks>
                /// Guid = { 0x35a208db, 0x30b7, 0x40c1, { 0x98, 0x40, 0x60, 0x10, 0x6b, 0x18, 0x79, 0xc8 } }</remarks>
                public static readonly Guid StructCreatorService = new Guid(0x35a208db, 0x30b7, 0x40c1, 0x98, 0x40, 0x60, 0x10, 0x6b, 0x18, 0x79, 0xc8);
                /// <summary>
                /// The <see cref="IIntermediateBinaryOperatorCoercionMember"/> creation service guid for structs.
                /// </summary>
                /// <remarks>Guid = { 0x84a6a3cc, 0x841d, 0x42b7, { 0x8f, 0xf2, 0x29, 0x50, 0xe8, 0xe0, 0xe9, 0x78 } }</remarks>
                public static readonly Guid BinaryOperatorCreatorService = new Guid(0x84a6a3cc, 0x841d, 0x42b7, 0x8f, 0xf2, 0x29, 0x50, 0xe8, 0xe0, 0xe9, 0x78);
                /// <summary>
                /// The <see cref="IIntermediateStructCtorMember"/> creation service guid.
                /// </summary>
                /// <remarks>Guid = { 0x1d9a61f7, 0x10e8, 0x4801, { 0x8e, 0x9, 0x9c, 0xd2, 0x59, 0xc5, 0x94, 0x63 } }</remarks>
                public static readonly Guid ConstructorCreatorService = new Guid(0x1d9a61f7, 0x10e8, 0x4801, 0x8e, 0x9, 0x9c, 0xd2, 0x59, 0xc5, 0x94, 0x63);
                /// <summary>
                /// The <see cref="IIntermediateStructEventMember"/> creation service guid.
                /// </summary>
                /// <remarks>Guid = { 0x41ae0fdb, 0x65b7, 0x4546, { 0x99, 0x39, 0x57, 0x40, 0x45, 0xb9, 0x6, 0x4f } }</remarks>
                public static readonly Guid EventCreatorService = new Guid(0x41ae0fdb, 0x65b7, 0x4546, 0x99, 0x39, 0x57, 0x40, 0x45, 0xb9, 0x6, 0x4f);
                /// <summary>
                /// The <see cref="IIntermediateStructFieldMember"/> creation service guid.
                /// </summary>
                /// <remarks>Guid = { 0xeea703b2, 0xc98c, 0x49bf, { 0x84, 0xdb, 0xce, 0x44, 0x82, 0x8e, 0xe3, 0x71 } }</remarks>
                public static readonly Guid FieldCreatorService = new Guid(0xeea703b2, 0xc98c, 0x49bf, 0x84, 0xdb, 0xce, 0x44, 0x82, 0x8e, 0xe3, 0x71);
                /// <summary>
                /// The <see cref="IIntermediateStructIndexerMember"/> creation service guid.
                /// </summary>
                /// <remarks>Guid = { 0x581846cb, 0x8f36, { 0x4fc1, 0xa9, 0xd9, 0x4f, 0x34, 0x15, 0x9f, 0x29, 0xd7 } }</remarks>
                public static readonly Guid IndexerCreatorService = new Guid(0x581846cb, 0x8f36, 0x4fc1, 0xa9, 0xd9, 0x4f, 0x34, 0x15, 0x9f, 0x29, 0xd7);
                /// <summary>
                /// The <see cref="IIntermediateStructMethodMember"/> creation service guid.
                /// </summary>
                /// <remarks>Guid = { 0xab45f2b5, 0x42ce, 0x46a8, { 0xb8, 0xe8, 0x7f, 0xa0, 0xae, 0xba, 0xf8, 0x30 } }</remarks>
                public static readonly Guid MethodCreatorService = new Guid(0xab45f2b5, 0x42ce, 0x46a8, 0xb8, 0xe8, 0x7f, 0xa0, 0xae, 0xba, 0xf8, 0x30);
                /// <summary>
                /// The <see cref="IIntermediateStructPropertyMember"/> creation service guid.
                /// </summary>
                /// <remarks>Guid = { 0x5d4593fc, 0x7c2f, 0x4e21, { 0x8a, 0xad, 0x45, 0x74, 0x13, 0xd0, 0xf0, 0x28 } }</remarks>
                public static readonly Guid PropertyCreatorService = new Guid(0x5d4593fc, 0x7c2f, 0x4e21, 0x8a, 0xad, 0x45, 0x74, 0x13, 0xd0, 0xf0, 0x28);
                /// <summary>
                /// The <see cref="IIntermediateTypeCoercionMember"/> creation service guid for structs.
                /// </summary>
                /// <remarks>Guid = { 0xa4f69721, 0xa381, 0x46b5, { 0xb2, 0xbb, 0x5b, 0x29, 0xb5, 0x2f, 0xc0, 0xba } }</remarks>
                public static readonly Guid TypeCoercionCreatorService = new Guid(0xa4f69721, 0xa381, 0x46b5, 0xb2, 0xbb, 0x5b, 0x29, 0xb5, 0x2f, 0xc0, 0xba);
                /// <summary>
                /// The <see cref="IIntermediateUnaryOperatorCoercionMember"/> creation service guid for structs.
                /// </summary>
                /// <remarks>Guid = { 0xb6077b82, 0xeee0, 0x4156, { 0xb1, 0x81, 0xa9, 0x9e, 0xc3, 0x9e, 0x4, 0x6b} }</remarks>
                public static readonly Guid UnaryOperatorCreatorService = new Guid(0xb6077b82, 0xeee0, 0x4156, 0xb1, 0x81, 0xa9, 0x9e, 0xc3, 0x9e, 0x4, 0x6b);

            }
            /// <summary>
            /// The <see cref="IIntermediateDelegateType"/> creation service guid.
            /// </summary>
            /// <remarks>Guid = { 0xef710ca1, 0x316d, 0x4719, { 0xab, 0x86, 0xd3, 0xbd, 0x8e, 0x3f, 0x58, 0xd1 } }</remarks>
            public static readonly Guid IntermediateDelegateCreatorService  = new Guid(0xef710ca1, 0x316d, 0x4719, 0xab, 0x86, 0xd3, 0xbd, 0x8e, 0x3f, 0x58, 0xd1);
            /// <summary>
            /// The <see cref="IIntermediateEnumType"/> creation service guid.
            /// </summary>
            /// <remarks>Guid = { 0xb154963f, 0x2ac, 0x4ae7, { 0x80, 0xf, 0xfd, 0x37, 0xdf, 0x69, 0x72, 0x27 } }</remarks>
            public static readonly Guid IntermediateEnumCreatorService      = new Guid(0xb154963f, 0x2ac, 0x4ae7, 0x80, 0xf, 0xfd, 0x37, 0xdf, 0x69, 0x72, 0x27);

            public static class InterfaceServices
            {
                /// <summary>
                /// The <see cref="IIntermediateInterfaceType"/> creation service guid.
                /// </summary>
                /// <remarks>Guid = { 0x191763b, 0xee19, 0x4ef9, { 0x9f, 0x2f, 0xed, 0x11, 0x4b, 0xf3, 0x88, 0xaa } }</remarks>
                public static readonly Guid InterfaceCreatorService = new Guid(0x191763b, 0xee19, 0x4ef9, 0x9f, 0x2f, 0xed, 0x11, 0x4b, 0xf3, 0x88, 0xaa);
                /// <summary>
                /// The <see cref="IIntermediateInterfaceEventMember"/> creation service guid.
                /// </summary>
                /// <remarks>Guid = { 0x2287ab04, 0xc660, 0x497b, { 0x8c, 0x6c, 0x58, 0x5, 0x1e, 0xc6, 0xf2, 0xe6 } }</remarks>
                public static readonly Guid EventCreatorService = new Guid(0x2287ab04, 0xc660, 0x497b, 0x8c, 0x6c, 0x58, 0x5, 0x1e, 0xc6, 0xf2, 0xe6);
                /// <summary>
                /// The <see cref="IIntermediateInterfaceIndexerMember"/> creation service guid.
                /// </summary>
                /// <remarks>Guid = { 0x2d24e51e, 0xd62f, 0x42e6, { 0x8b, 0x41, 0xd1, 0xbd, 0xf9, 0x60, 0x1b, 0x28 } }</remarks>
                public static readonly Guid IndexerCreatorService = new Guid(0x2d24e51e, 0xd62f, 0x42e6, 0x8b, 0x41, 0xd1, 0xbd, 0xf9, 0x60, 0x1b, 0x28);
                /// <summary>
                /// The <see cref="IIntermediateInterfaceMethodMember"/> creation service guid.
                /// </summary>
                /// <remarks>Guid = { 0x701cd79f, 0xdc23, 0x4c4d, { 0x99, 0xb8, 0x97, 0x9a, 0xf3, 0xea, 0xe6, 0xc } }</remarks>
                public static readonly Guid MethodCreatorService = new Guid(0x701cd79f, 0xdc23, 0x4c4d, 0x99, 0xb8, 0x97, 0x9a, 0xf3, 0xea, 0xe6, 0xc);
                /// <summary>
                /// The <see cref="IIntermediateInterfacePropertyMember"/> creation service guid.
                /// </summary>
                /// <remarks>Guid = { 0xc23aba27, 0x4627, 0x4891, { 0xa0, 0x41, 0x6, 0xeb, 0xa6, 0x79, 0x1f, 0x27 } }</remarks>
                public static readonly Guid PropertyCreatorService = new Guid(0xc23aba27, 0x4627, 0x4891, 0xa0, 0x41, 0x6, 0xeb, 0xa6, 0x79, 0x1f, 0x27);
            }
            // {35A208DB-30B7-40C1-9840-60106B1879C8}
            /// <summary>
            /// The <see cref="IIntermediateAssembly"/> creation service guid.
            /// </summary>
            /// <remarks>Guid = { 0xcfa52a0e, 0x3c49, 0x4f99, { 0x8d, 0x40, 0xdc, 0x24, 0x9c, 0xe9, 0x56, 0x7c } }</remarks>
            public static readonly Guid IntermediateAssemblyCreatorService  = new Guid(0xcfa52a0e, 0x3c49, 0x4f99, 0x8d, 0x40, 0xdc, 0x24, 0x9c, 0xe9, 0x56, 0x7c);

            /// <summary>The <see cref="IParameterParent.LastIsParams"/> determination service.</summary>
            public static readonly Guid ParameterArrayDeterminationService  = new Guid(0x7BB8ECF6, 0xFCF0, 0x471C, 0x90, 0x59, 0x71, 0xB4, 0x63, 0x91, 0x89, 0x15);
            
            /// <summary>The <see cref="IIntermediateNamespaceDeclaration"/> creation service guid.</summary>
            public static readonly Guid IntermediateNamespaceCreatorService = new Guid(0x8E8B8549, 0x3F1F, 0x4568, 0xB5, 0x24, 0xBE, 0x08, 0x83, 0xBA, 0xF7, 0x26);
            public static readonly Guid UniqueIdentifierService = new Guid("F4E029F6-8C93-4712-A66E-2E08491270B9");
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
