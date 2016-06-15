using AllenCopeland.Abstraction.IO;
using AllenCopeland.Abstraction;
using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Cli;
using AllenCopeland.Abstraction.Slf.Ast.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Languages.CSharp;
using AllenCopeland.Abstraction.Slf.Languages.VisualBasic;
using AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions;
using AllenCopeland.Abstraction.Slf.Languages.CSharp.Translation;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;
using AllenCopeland.Abstraction.Slf.Translation;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
namespace CliMetadataReader
{
    using HasConstant           = CliMetadataHasConstantTag;
    using TypeDefOrMethodDef    = CliMetadataTypeOrMethodDef;
    using TypeDefOrRef          = CliMetadataTypeDefOrRefTag;
    using System.Diagnostics;
    using AllenCopeland.Abstraction.Utilities.Security;
    using System.Threading.Tasks;
    class Program
    {

        static void MainRunTest()
        {
            //var myLocation = typeof(Program).Assembly.Location;
            //var isAssembly = CliGateway.IsCliLibrary(myLocation);
            while (true)
            {
                Console.WriteLine("Took {0,9:0.0000} ms to run the test...", RunTest("TestAssembly1").TotalMilliseconds);
                var keyDetail = Console.ReadKey(true);
                if (keyDetail.Key == ConsoleKey.Q)
                    return;
            }
            
            //Console.WriteLine(isAssembly);
        }

        private static TimeSpan RunTest(string assemblyName)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var csLang = LanguageVendors.Microsoft.GetCSharpLanguage();
            sw.Stop();
            var csLangAssign = sw.Elapsed;
            sw.Restart();
            var csProv = csLang.GetProvider(/* /VisualBasicVersion.CurrentVersion, /* */CSharpLanguageVersion.CurrentVersion, /* */IntermediateCliGateway.GlobalManager);
            sw.Stop();
            var csProvAssign = sw.Elapsed;
            sw.Restart();
            var mu = csProv.CreateAssembly(assemblyName);
            sw.Stop();
            var muAssign = sw.Elapsed;
            sw.Restart();
            var tc = mu.Namespaces.Add("TestNamespace").Classes.Add("TestClass");
            sw.Stop();
            var tcAssign = sw.Elapsed;
            sw.Restart();
            var testMeth = tc.Methods.Add(typeof(void).WithName("TestMethod"), new TypedNameSeries(typeof(int).GetTypeReference().MakeArray().WithName("testParams")));
            sw.Stop();
            var testMethAssign = sw.Elapsed;
            sw.Restart();
            testMeth.LastIsParams = true;
            sw.Stop();
            var testMethodLIPAssign = sw.Elapsed;
            sw.Restart();
            var console = typeof(Console).GetTypeReference<IClassType>();
            var writeLine = console.Methods[TypeSystemIdentifiers.GetGenericSignatureIdentifier("WriteLine", typeof(string).GetTypeReference(), typeof(object[]).GetTypeReference())];
            var iType = typeof(int).GetTypeReference<IStructType>();
            var vType = typeof(void).GetTypeReference();
            var mued = tc.Properties.Add(vType.WithName("FarGea"), true, true);
            var valueParam = mued.SetMethod.ValueParameter;
            var classes = 0.RangeTo(1000)/**/.AsParallel()/* [0->n-1] */.Select(i =>
            {
                var @class = mu.Classes.Add("Test{0}Class", i);
                @class.SuspendDualLayout();
                //Parallel.ForEach(0.RangeTo(100), j =>
                for (int j = 0; j < 100; j++)
                {
                    var meth = @class.Methods.Add(vType.WithName(string.Format("TestMethod{0}_{1}", i, j)), new TypedNameSeries(0.RangeTo(20).Select(k => iType.WithName(string.Format("arg{0}", k)))));
                    meth.Call(writeLine.GetReference().Invoke(new IExpression[1] { string.Join(", ", 0.RangeTo(20).Select(k => string.Format("arg{0}: {{{0}}}", k))).ToPrimitive() }.Add(0.RangeTo(20).Select(k => meth.Parameters[TypeSystemIdentifiers.GetMemberIdentifier(string.Format("arg{0}", k))].GetReference()).ToArray())));
                }//*/);
                @class.ResumeDualLayout();
                return @class;
            }).ToArray();
            sw.Stop();
            var create1KClasses = sw.Elapsed;
            sw.Restart();
            mu.Dispose();
            sw.Stop();
            var disposeTime = sw.Elapsed;
            Console.WriteLine(@"
Time Taken to Get CSharpLanguage    : {0,9:0.0000} ms
Time Taken to Get Provider          : {1,9:0.0000} ms
Time Taken to Create Assembly       : {2,9:0.0000} ms
Time Taken to Create Class          : {3,9:0.0000} ms
Time Taken to Create Method         : {4,9:0.0000} ms
Time Taken to Assign Last Is Params : {5,9:0.0000} ms
Time Taken to Create 100K Methods
    With 2mil parameters total      : {7,9:0.0000} ms
Time Taken to Dispose               : {6,9:0.0000} ms",
                            csLangAssign.TotalMilliseconds,
                            csProvAssign.TotalMilliseconds,
                            muAssign.TotalMilliseconds,
                            tcAssign.TotalMilliseconds,
                            testMethAssign.TotalMilliseconds,
                            testMethodLIPAssign.TotalMilliseconds,
                            disposeTime.TotalMilliseconds,
                            create1KClasses.TotalMilliseconds);
            return csLangAssign + csProvAssign + muAssign + tcAssign + testMethAssign + testMethodLIPAssign + disposeTime + create1KClasses;
        }
        static void Main3(string[] args)
        {
            var d = typeof(Program).GetTypeReference<IClassType>();
            var methods = 
                d.Methods.Values
                .Cast<ICliDeclaration>()
                .Select(k => 
                    new 
                    { 
                        Method = (IMethodSignatureMember)k,
                        MethodMetadata = (ICliMetadataMethodDefinitionTableRow)k.MetadataEntry 
                    })
                .ToArray();
            foreach (var m in methods)
            {
                var method    = m.MethodMetadata;
                var methodDef = m.Method;
                var header    = method.Body.Header;
                var locals    = header.LocalVariables;
                if (locals   != null)
                {
                    var localsByType =
                        (from l in locals
                         let t  = l as ICliMetadataLocalVarFullEntrySignature
                         let t2 =
                             l != null
                             ? ((_ICliManager)(IntermediateCliGateway.GlobalManager)).ObtainTypeReference(t.LocalType, d, methodDef, d.Assembly)
                             : typeof(TypedReference).GetTypeReference()
                         group l by t2).ToDictionary(k => k.Key, v => v.ToArray());
                }
                var inst = method.Body.Instructions.ToArray();
                Console.WriteLine(methodDef.Name);
                foreach (var instruction in inst)
                    Console.WriteLine(instruction);
                Console.WriteLine();
            }
        }

        private static void Main231() {
            var time = TimeIsCliLibraryCheck();
            var time2 = TimeIsCliLibraryCheck();
            Console.WriteLine("{0} ms to check if the file was an assembly (1).", time.TotalMilliseconds);
            Console.WriteLine("{0} ms to check if the file was an assembly (2).", time2.TotalMilliseconds);
        }

        private static TimeSpan TimeIsCliLibraryCheck()
        {
            Stopwatch sw = Stopwatch.StartNew();
            CliGateway.IsFullAssembly(typeof(IStrongNamePrivateKeyInfo).Assembly.Location);
            sw.Stop();
            var time = sw.Elapsed;
            return time;
        }

        //private static void Main()
        //{
        //    var idMan = IntermediateCliGateway.GlobalManager;
        //    var type = idMan.ObtainTypeReference(typeof(IIntermediateCliManager).AssemblyQualifiedName);
        //    idMan.Dispose();
        //}

        private static void tMain()
        {
            var csharpProvider = LanguageVendors.Microsoft.GetCSharpLanguage().GetProvider(IntermediateCliGateway.GlobalManager);
            var resultedProject = csharpProvider.CreateAssembly("CliMetadataReader");//new IntermediateProject("CliMetadataReader", defaultNamespaceName);
            var defaultNamespace = resultedProject.DefaultNamespace = resultedProject.Namespaces.Add("A.B");
            var testClass = defaultNamespace.Classes.Add("Test");
            var testInterface = defaultNamespace.Interfaces.Add("Test");
            var tName = typeof(int).GetTypeReference().WithName("Test");
            var tNameSeries = new TypedNameSeries(new TypedName("Test", typeof(double).GetTypeReference()));
            var tTestParams = 0.RangeTo(5).Select(k => testClass.TypeParameters.Add(string.Format("TTest{0}", k + 1))).ToArray();
            var tTestParam3 = tTestParams[2];
            IType[] types = new IType[] { typeof(sbyte).GetTypeReference<IStructType>(), typeof(ushort).GetTypeReference(), typeof(byte).GetTypeReference(), typeof(short).GetTypeReference(), typeof(uint).GetTypeReference(), typeof(int).GetTypeReference(), typeof(ulong).GetTypeReference(), typeof(long).GetTypeReference(), typeof(double).GetTypeReference() };
            var testGeneric = testClass.MakeGenericClosure(tTestParams.Select((k, i) => types[i % types.Length]).ToArray());
            Random r = new Random();
            var testGenericSet = 0.RangeTo(5).Select(a => testClass.MakeGenericClosure((tTestParams.Select((k, i) => types[r.Next(0, types.Length)]).ToArray()))).ToArray();
            var method1 = testClass.Methods.Add(tTestParams[tTestParams.Length - 1].WithName("Method1"), new TypedNameSeries(testClass.TypeParameters.Values.Reverse().Skip(1).Reverse().Select((k, i) => k.WithName(string.Format("p{0}", i)))));
            var method2 = testInterface.Methods.Add(tName, tNameSeries);
            var ind = testClass.Indexers.Add(typeof(int).WithName("TKK"), new TypedNameSeries(typeof(byte).WithName("index")), false, true);
            var indSMParams = ind.SetMethod.Parameters.Values.Cast<IMethodParameterMember>().ToArray();
            var mParams = method1.Parameters.Values.ToArray();
            var fGenMeth = testGeneric.Methods.Values.FirstOrDefault();
            tTestParam3.Position = 2;
            Stopwatch sw = Stopwatch.StartNew();
            const int times = 100000;
            for (int i = 0; i < times; i++)
                tTestParam3.Position = i % testGeneric.GenericParameters.Count;
            sw.Stop();
            Console.WriteLine("Time to reposition generic parameter {1} times: {0}ms", sw.Elapsed.TotalMilliseconds, times);
        }
        private static void Main()
        {
            /* *
             * Classic example of 'Get it done'.  I hate this style of programming
             * but it's very effective for program generators.
             * */
            var uintDataType                          = new MetadataTableTypeDataType(typeof(uint).GetTypeReference());
            var ushortDataType                        = new MetadataTableTypeDataType(typeof(ushort).GetTypeReference());
            var typeAttributes                        = new MetadataTableTypeDataType(typeof(TypeAttributes).GetTypeReference(), typeof(uint).GetTypeReference());
            var fieldAttributes                       = new MetadataTableTypeDataType(typeof(FieldAttributes).GetTypeReference(), typeof(ushort).GetTypeReference());
            var methodImplementationDetails           = new MetadataTableTypeDataType(typeof(MethodImplementationDetails).GetTypeReference(), typeof(ushort).GetTypeReference());
            var methodUsage                           = new MetadataTableTypeDataType(typeof(MethodUseDetails).GetTypeReference(), typeof(ushort).GetTypeReference());
            var parameterAttributes                   = new MetadataTableTypeDataType(typeof(ParameterAttributes).GetTypeReference(), typeof(ushort).GetTypeReference());
            var nativeTypes                           = new MetadataTableTypeDataType(typeof(CliMetadataNativeTypes).GetTypeReference(), typeof(ushort).GetTypeReference());
            var securityAction                        = new MetadataTableTypeDataType(typeof(SecurityAction).GetTypeReference(), typeof(ushort).GetTypeReference());
            var eventAttributes                       = new MetadataTableTypeDataType(typeof(EventAttributes).GetTypeReference(), typeof(ushort).GetTypeReference());
            var propertyAttributes                    = new MetadataTableTypeDataType(typeof(PropertyAttributes).GetTypeReference(), typeof(ushort).GetTypeReference());
            var methodSemanticsAttributes             = new MetadataTableTypeDataType(typeof(MethodSemanticsAttributes).GetTypeReference(), typeof(ushort).GetTypeReference());
            var platformInvokeCharacterSet            = new MetadataTableTypeDataType(typeof(PlatformInvokeCharacterSet).GetTypeReference(), typeof(byte).GetTypeReference());
            var platformInvokeCallingConvention       = new MetadataTableTypeDataType(typeof(PlatformInvokeCallingConvention).GetTypeReference(), typeof(byte).GetTypeReference());
            var assemblyHashAlgorithm                 = new MetadataTableTypeDataType(typeof(AssemblyHashAlgorithm).GetTypeReference(), typeof(uint).GetTypeReference());
            var assemblyFlags                         = new MetadataTableTypeDataType(typeof(CliMetadataAssemblyFlags).GetTypeReference(), typeof(uint).GetTypeReference());
            var qwordVersion                          = new MetadataTableTypeDataType(typeof(QWordLongVersion).GetTypeReference(), true);
            var fileAttributes                        = new MetadataTableTypeDataType(typeof(CliMetadataFileAttributes).GetTypeReference(), typeof(uint).GetTypeReference());
            var genericParameterAttributes            = new MetadataTableTypeDataType(typeof(GenericParameterAttributes).GetTypeReference(), typeof(ushort).GetTypeReference());

            var signatureKindsType                    = typeof(SignatureKinds).GetTypeExpression();

            var stringsHeap                           = new MetadataTableFieldHeapDataType(MetadataHeapTarget.StringsHeap);
            var blobHeap                              = new MetadataTableFieldHeapDataType(MetadataHeapTarget.BlobHeap);
            var guidHeap                              = new MetadataTableFieldHeapDataType(MetadataHeapTarget.GuidHeap);
            var userString                            = new MetadataTableFieldHeapDataType(MetadataHeapTarget.UserStringsHeap);
            var fieldSignatureBlobType                = new MetadataTableBlobHeapDataType(signatureKindsType.GetField("FieldSig"), typeof(ICliMetadataFieldSignature).GetTypeReference());
            var methodSignatureBlobType               = new MetadataTableBlobHeapDataType(signatureKindsType.GetField("MethodDefSig"), typeof(ICliMetadataMethodSignature).GetTypeReference());
            var methodRefSignatureBlobType            = new MetadataTableBlobHeapDataType(signatureKindsType.GetField("MethodRefSig"), typeof(ICliMetadataMethodSignature).GetTypeReference());
            var memberRefSignatureBlobType            = new MetadataTableBlobHeapDataType(signatureKindsType.GetField("MemberRefSig"), typeof(ICliMetadataMemberRefSignature).GetTypeReference());
            var standAloneSignatureBlobType           = new MetadataTableBlobHeapDataType(signatureKindsType.GetField("StandaloneSignature"), typeof(ICliMetadataStandAloneSignature).GetTypeReference());
            var propertySignatureBlobType             = new MetadataTableBlobHeapDataType(signatureKindsType.GetField("PropertySig"), typeof(ICliMetadataPropertySignature).GetTypeReference());
            var typeSpecBlobType                      = new MetadataTableBlobHeapDataType(signatureKindsType.GetField("TypeSpec"), typeof(ICliMetadataTypeSpecSignature).GetTypeReference());
            var methodSpecBlobType                    = new MetadataTableBlobHeapDataType(signatureKindsType.GetField("MethodSpec"), typeof(ICliMetadataMethodSpecSignature).GetTypeReference());
            var typeDefinitionOrReferenceEncoding     = new MetadataTableEncoding<TypeDefOrRef>("TypeDefOrRef");
            var hasConstantEncoding                   = new MetadataTableEncoding<HasConstant>("HasConstant");

            var hasCustomAttributeEncoding            = new MetadataTableEncoding<CliMetadataHasCustomAttributeTag>("HasCustomAttribute");
            var hasFieldMarshalEncoding               = new MetadataTableEncoding<CliMetadataHasFieldMarshalTag>("HasFieldMarshal");
            var hasDeclaredSecurityEncoding           = new MetadataTableEncoding<CliMetadataHasDeclSecurityTag>("HasDeclSecurity");
            var memberReferenceParentEncoding         = new MetadataTableEncoding<CliMetadataMemberRefParentTag>("MemberRefParent");
            var hasSemanticsEncoding                  = new MetadataTableEncoding<CliMetadataHasSemanticsTag>("HasSemantics");
            var methodDefinitionOrReferenceEncoding   = new MetadataTableEncoding<CliMetadataMethodDefOrRefTag>("MethodDefOrRef");
            var memberForwardedEncoding               = new MetadataTableEncoding<CliMetadataMemberForwardedTag>("MemberForwarded");
            var implementationEncoding                = new MetadataTableEncoding<CliMetadataImplementationTag>("Implementation");
            var customAttributeTypeEncoding           = new MetadataTableEncoding<CliMetadataCustomAttributeTypeTag>("CustomAttributeType");
            var resolutionScopeEncoding               = new MetadataTableEncoding<CliMetadataResolutionScopeTag>("ResolutionScope");
            var typeOrMethodDefinitionEncoding        = new MetadataTableEncoding<CliMetadataTypeOrMethodDef>("TypeOrMethodDef");

            const string defaultNamespaceName         = "AllenCopeland.Abstraction.Slf.Cli.Metadata";
            const string defaultInternalNamespaceName = "AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata";
            const string depreciatedMetadata          = "This record should not be emitted into any PE image; however if present, it should be treated as if all its fields were zero. Supported to ensure proper reading of the metadata.";
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
                    new MetadataTableField("FieldStart",      () => tableLookup[CliMetadataTableKinds.Field], "The first field in the type.", "The set of fields defined by the type can be determined by the next row's first field."),
                    new MetadataTableField("MethodStart",     () => tableLookup[CliMetadataTableKinds.MethodDefinition], "The first method in the type.", "The set of methods defined by the type can be determined by the next row's first method."),
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
                    new MetadataTableField("ParameterStart",  () => tableLookup[CliMetadataTableKinds.Parameter], "The parameters of the method."),
                },
                new MetadataTable("Parameter",                      0x08, "Defines information about the parameters defined within the image.")
                {
                    new MetadataTableField("Flags",                 parameterAttributes, "Conditional information about the parameter, whether it's optional, has marshaling applied to it, and/or the direction in which the parameter's data is coerced."),
                    new MetadataTableField("Sequence",              ushortDataType, "The ordinal index of the parameter within the sequence of parameters for a given method.", "Gaps in Sequence are allowed; however, the value of sequence from one parameter to the next must be in increasing value.  Parameter with sequence index zero refers to the method's return type."),
                    new MetadataTableField("Name",                  stringsHeap, "The name of the method.", "The name of a parameter can be null or non-null."),
                },
                new MetadataTable("InterfaceImpl",                  0x09, "Defines information about the interfaces implemented by the defined types of the image.")
                {
                    new MetadataTableField("Class",           () => tableLookup[CliMetadataTableKinds.TypeDefinition], "The class which implements @s:Interface;."),
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
                    new MetadataTableField("Parent",          () => tableLookup[CliMetadataTableKinds.TypeDefinition])
                },
                new MetadataTable("FieldLayout",                    0x10, "Defines the layout of the fields on an @s:LayoutKind.Explicit; layout type.")
                {
                    new MetadataTableField("Offset",                uintDataType),
                    new MetadataTableField("Field",           () => tableLookup[CliMetadataTableKinds.Field]) { IndexSummary = "Returns the @s:UInt32; value which represents the field for which the layout exists." } 
                },
                new MetadataTable("StandAloneSig",                  0x11, "Defines the offset to a standalone signature.", "Used when a method is called by address, the standalone signature is pushed onto the stack and the call is made.")
                {
                    new MetadataTableField("Signature",             standAloneSignatureBlobType),
                },
                new MetadataTable("EventMap",                       0x12, "Defines the event mapping of a type defined within the module.")
                {
                    new MetadataTableField("Parent",          () => tableLookup[CliMetadataTableKinds.TypeDefinition], "The target type definition which contains the series of events."),
                    new MetadataTableField("EventList",       () => tableLookup[CliMetadataTableKinds.Event], "The first event of the @s:Parent;.", "The full list of events can be obtained by comparing the next entry's @s:EventList;.")
                },
                new MetadataTable("Event",                          0x14, "Defines information about an event.")
                {
                    new MetadataTableField("Flags",                 eventAttributes, "The @s:EventAttributes; which denote how the event is handled."),
                    new MetadataTableField("Name",                  stringsHeap,     "The name of the event."),
                    new MetadataTableEncodedField<TypeDefOrRef>("SignatureType", typeDefinitionOrReferenceEncoding, "SignatureSource", "The @s:ITypeDefOrRefRow; which is the delegate that acts as the event's signature."),
                },
                new MetadataTable("PropertyMap",                    0x15, "Defines the property mapping of a type defined within the module.")
                {
                    new MetadataTableField("Parent",          () => tableLookup[CliMetadataTableKinds.TypeDefinition], "The target type definition which contains the series of properties."),
                    new MetadataTableField("PropertyList",    () => tableLookup[CliMetadataTableKinds.Property], "The properties defined on the current type.")
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
                    new MetadataTableField("Method",          () => tableLookup[CliMetadataTableKinds.MethodDefinition], "The target method of the semantics."),
                    new MetadataTableFilteredEncodedField<CliMetadataHasSemanticsTag>("Association", hasSemanticsEncoding, "AssociationSource", "Semantics"),
                },
                new MetadataTable("MethodImpl",                     0x19)
                {
                    new MetadataTableField("Class",           () => tableLookup[CliMetadataTableKinds.TypeDefinition]),
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
                    new MetadataTableField("ImportScope",     () => tableLookup[CliMetadataTableKinds.ModuleReference]),

                },
                new MetadataTable("FieldRelativeVirtualAddress",    0x1D, "Defines information about a field's relative virtual address.")
                {
                    new MetadataTableField("RVA",                   uintDataType, "Returns the @s:UInt32; value representing the relative virtual address for the field."),
                    new MetadataTableField("Field",           () => tableLookup[CliMetadataTableKinds.Field]) { IndexSummary = "Returns the @s:UInt32; value which represents the field for which the rva exists." },
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
                    new MetadataTableField("AssemblyRef",     () => tableLookup[CliMetadataTableKinds.AssemblyReference]),
                },
                new MetadataTable("AssemblyReferenceOS",            0x25, "Defines the target operating system of the assembly.", depreciatedMetadata)
                {
                    new MetadataTableField("PlatformId",            uintDataType),
                    new MetadataTableField("MajorVersion",          uintDataType),
                    new MetadataTableField("MinorVersion",          uintDataType),
                    new MetadataTableField("AssemblyRef",     () => tableLookup[CliMetadataTableKinds.AssemblyReference]),
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
                    new MetadataTableField("NestedClass",     () => tableLookup[CliMetadataTableKinds.TypeDefinition]),
                    new MetadataTableField("EnclosingClass",  () => tableLookup[CliMetadataTableKinds.TypeDefinition]),
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
                    new MetadataTableField("Owner",           () => tableLookup[CliMetadataTableKinds.GenericParameter]),
                    new MetadataTableEncodedField<TypeDefOrRef>("Constraint", typeDefinitionOrReferenceEncoding, "ConstraintSource"),
                },
            };

            foreach (var table in tables)
                tableLookup.Add(table.TableKind, table);

            var nestedClassEnclosingField             = tableLookup[CliMetadataTableKinds.NestedClass]["EnclosingClass"];
            var nestedClassNestedField                = tableLookup[CliMetadataTableKinds.NestedClass]["NestedClass"];

            nestedClassEnclosingField.ImportType      = MetadataTableFieldImportKind.ManyToOneImport;
            nestedClassEnclosingField.SourceKind      = MetadataTableFieldListSource.FieldRef;
            nestedClassEnclosingField.TargetField     = nestedClassNestedField;
            nestedClassEnclosingField.TargetListTable = tableLookup[CliMetadataTableKinds.TypeDefinition];
            nestedClassEnclosingField.ImportName      = "NestedClasses";
            nestedClassEnclosingField.ImportSummary   = "Returns the nested types for the current type.";

            nestedClassNestedField.ImportType         = MetadataTableFieldImportKind.TableReference;
            nestedClassNestedField.ImportName         = "DeclaringType";
            nestedClassNestedField.TargetField        = nestedClassEnclosingField;
            nestedClassNestedField.ImportSummary      = "Returns the type which declares the current type.";
            nestedClassNestedField.ImportRemarks      = "Can be null.";

            var methodEntriesField                    = tableLookup[CliMetadataTableKinds.TypeDefinition]["MethodStart"];
            methodEntriesField.ImportType             = MetadataTableFieldImportKind.OneToSequentialMany;
            methodEntriesField.TargetListTable        = tableLookup[CliMetadataTableKinds.MethodDefinition];
            methodEntriesField.ImportName             = "Methods";
            methodEntriesField.ImportSummary          = "Returns the methods for the current type.";

            var fieldEntriesField                     = tableLookup[CliMetadataTableKinds.TypeDefinition]["FieldStart"];
            fieldEntriesField.ImportType              = MetadataTableFieldImportKind.OneToSequentialMany;
            fieldEntriesField.TargetListTable         = tableLookup[CliMetadataTableKinds.Field];
            fieldEntriesField.ImportName              = "Fields";
            fieldEntriesField.ImportSummary           = "Returns the fields for the current type.";

            var parametersField                       = tableLookup[CliMetadataTableKinds.MethodDefinition]["ParameterStart"];
            parametersField.ImportType                = MetadataTableFieldImportKind.OneToSequentialMany;
            parametersField.TargetListTable           = tableLookup[CliMetadataTableKinds.Parameter];
            parametersField.ImportName                = "Parameters";
            parametersField.ImportSummary             = "Returns the parameters for the current method.";

            var eventListField                        = tableLookup[CliMetadataTableKinds.EventMap]["Parent"];
            eventListField.ImportType                 = MetadataTableFieldImportKind.OneToSequentialManyImported;
            eventListField.TargetListTable            = tableLookup[CliMetadataTableKinds.EventMap];
            eventListField.TargetField                = tableLookup[CliMetadataTableKinds.EventMap]["EventList"];
            eventListField.TargetListTable            = tableLookup[CliMetadataTableKinds.Event];
            eventListField.ImportName                 = "Events";
            eventListField.ResultedListElementName    = "Event";
            eventListField.ImportSummary              = "Returns the events for the current type definition.";

            var propertyListField                     = tableLookup[CliMetadataTableKinds.PropertyMap]["Parent"];
            propertyListField.ImportType              = MetadataTableFieldImportKind.OneToSequentialManyImported;
            propertyListField.TargetListTable         = tableLookup[CliMetadataTableKinds.PropertyMap];
            propertyListField.TargetField             = tableLookup[CliMetadataTableKinds.PropertyMap]["PropertyList"];
            propertyListField.TargetListTable         = tableLookup[CliMetadataTableKinds.Property];
            propertyListField.ImportName              = "Properties";
            propertyListField.ResultedListElementName = "Property";
            propertyListField.ImportSummary           = "Returns the properties for the current type definition.";

            
            var genericParameterOwner                 = tableLookup[CliMetadataTableKinds.GenericParameter]["Owner"];
            genericParameterOwner.ImportType          = MetadataTableFieldImportKind.ManyToOneImport;
            genericParameterOwner.SourceKind          = MetadataTableFieldListSource.SourceTableRow;
            genericParameterOwner.ImportName          = "TypeParameters";
            genericParameterOwner.ImportSummary       = "Returns the type-parameters relative to the current row.";

            var customAttributeOwner                  = tableLookup[CliMetadataTableKinds.CustomAttribute]["Parent"];
            customAttributeOwner.ImportType           = MetadataTableFieldImportKind.ManyToOneImport;
            customAttributeOwner.SourceKind           = MetadataTableFieldListSource.SourceTableRow;
            customAttributeOwner.ImportName           = "CustomAttributes";
            customAttributeOwner.ImportSummary        = "Returns the set of custom metadata elements applied to the member.";


            var genericParameterConstraint           = tableLookup[CliMetadataTableKinds.GenericParamConstraint]["Owner"];
            genericParameterConstraint.ImportType    = MetadataTableFieldImportKind.ManyToOneImport;
            genericParameterConstraint.SourceKind    = MetadataTableFieldListSource.SourceTableRow;
            genericParameterConstraint.ImportName    = "Constraints";
            genericParameterConstraint.ImportSummary = "Returns the constraints relative to the current generic parameter.";
            
            var fieldLayout                          = tableLookup[CliMetadataTableKinds.FieldLayout]["Field"];
            fieldLayout.ImportType                   = MetadataTableFieldImportKind.TableReference;
            fieldLayout.ImportName                   = "Layout";
            fieldLayout.ImportSummary                = "Returns the layout of the field which determines the byte offset of the field relative to the structure which contains it.";
            fieldLayout.ImportRemarks                = "Can be null.";
            
            var fieldRVA                             = tableLookup[CliMetadataTableKinds.FieldRelativeVirtualAddress]["Field"];
            fieldRVA.ImportType                      = MetadataTableFieldImportKind.TableReference;
            fieldRVA.ImportName                      = "RVA";
            fieldRVA.ImportSummary                   = "Returns the relative virtual address for the field.";
            fieldRVA.ImportRemarks                   = "Usually null except for initialized and uninitialized '.data' fields which store sequential bytes of data within the application's memory space.  The data-types of such fields must have no private fields of their own and contain no reference type fields as they point into the GC Heap.";
            
            var classLayout                          = tableLookup[CliMetadataTableKinds.ClassLayout]["Parent"];
            classLayout.ImportType                   = MetadataTableFieldImportKind.TableReference;
            classLayout.ImportName                   = "Layout";
            classLayout.ImportSummary                = "Returns the class layout information which determines the data and packing size of the type.";
            classLayout.ImportRemarks                = "Can be null.";

            var methodSemanticsAssociation           = tableLookup[CliMetadataTableKinds.MethodSemantics]["Association"];
            methodSemanticsAssociation.ImportType    = MetadataTableFieldImportKind.ManyToOneImport;
            methodSemanticsAssociation.SourceKind    = MetadataTableFieldListSource.SourceTableRow;
            methodSemanticsAssociation.TargetField   = tableLookup[CliMetadataTableKinds.MethodSemantics]["Method"];
            methodSemanticsAssociation.ImportName    = "Methods";
            methodSemanticsAssociation.ImportSummary = "Returns the methods with semantics relative to the current row.";

            tableLookup[CliMetadataTableKinds.FieldRelativeVirtualAddress].ShortName = "FieldRVA";
            tableLookup[CliMetadataTableKinds.AssemblyReference].ShortName           = "AssemblyRef";
            tableLookup[CliMetadataTableKinds.AssemblyReferenceOS].ShortName         = "AssemblyRefOS";
            tableLookup[CliMetadataTableKinds.AssemblyReferenceProcessor].ShortName  = "AssemblyRefProcessor";

            var interfaceImpl                        = tableLookup[CliMetadataTableKinds.InterfaceImpl]["Class"];
            interfaceImpl.ImportType                 = MetadataTableFieldImportKind.ManyToOneImport;
            interfaceImpl.SourceKind                 = MetadataTableFieldListSource.SourceTableRow;
            interfaceImpl.TargetField                = tableLookup[CliMetadataTableKinds.InterfaceImpl]["Interface"];
            interfaceImpl.SummaryText                = "Returns the set of interfaces implemented by the class.";
            interfaceImpl.ImportName                 = "ImplementedInterfaces";

            var implMap                              = tableLookup[CliMetadataTableKinds.MethodImpl]["Class"];
            implMap.ImportName                       = "ImplementationMap";
            implMap.SourceKind                       = MetadataTableFieldListSource.SourceTableRow;
            implMap.ImportType                       = MetadataTableFieldImportKind.ManyToOneImport;
            implMap.ImportSummary                    = "Returns the set of implementation mappings related to a class' implemented interfaces.";


            tableLookup[CliMetadataTableKinds.Property].NeedsIndex               = true;
            tableLookup[CliMetadataTableKinds.Event].NeedsIndex                  = true;
            tableLookup[CliMetadataTableKinds.EventMap].NeedsIndex               = true;
            tableLookup[CliMetadataTableKinds.PropertyMap].NeedsIndex            = true;
            tableLookup[CliMetadataTableKinds.MethodSemantics].NeedsIndex        = true;
            tableLookup[CliMetadataTableKinds.MethodImpl].NeedsIndex             = true;
            tableLookup[CliMetadataTableKinds.CustomAttribute].NeedsIndex        = true;
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
            memberReferenceParentEncoding.Add(CliMetadataMemberRefParentTag.TypeSpecification, tableLookup[CliMetadataTableKinds.TypeSpecification]);

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
            var csharpProvider = LanguageVendors.Microsoft.GetCSharpLanguage().GetProvider(IntermediateCliGateway.GlobalManager);

            var resultedProject          = csharpProvider.CreateAssembly("CliMetadataReader");//new IntermediateProject("CliMetadataReader", defaultNamespaceName);
            var defaultNamespace         = resultedProject.DefaultNamespace = resultedProject.Namespaces.Add(defaultNamespaceName);
            var defaultInternalNamespace = resultedProject.Namespaces.Add(defaultInternalNamespaceName);
            /* *
             * var constructOverrides = 
             *      CreateDualInterfaces(resultedProject, 
             *      typeof(CliMetadataRoot), typeof(CliMetadataTableStreamAndHeader), typeof(CliMetadataStringsHeaderAndHeap),
             *      typeof(CliMetadataBlobHeaderAndHeap), typeof(CliMetadataUserStringsHeaderAndHeap), typeof(CliMetadataGuidHeaderAndHeap));
             * */
            var metadataRoot           = typeof(ICliMetadataRoot).GetTypeReference<IInterfaceType>();
            var mutableMetadataRoot    = typeof(ICliMetadataMutableRoot).GetTypeReference<IInterfaceType>();
            var tablesStream           = defaultInternalNamespace.Parts.Add().Classes.Add("CliMetadataTableStreamAndHeader");
            var mutableTablesStream    = defaultInternalNamespace.Parts.Add().Classes.Add("CliMutableMetadataTableStreamAndHeader");

            mutableTablesStream.ForcedPartial = 
                tablesStream.ForcedPartial  = true;

            tablesStream.BaseType           = typeof(ControlledDictionary<CliMetadataTableKinds, ICliMetadataTable>).GetTypeReference<IClassType>();
            mutableTablesStream.BaseType    = typeof(ControlledDictionary<CliMetadataTableKinds, ICliMetadataTable>).GetTypeReference<IClassType>();

            tablesStream.ImplementedInterfaces.ImplementInterfaceQuick(typeof(ICliMetadataTableStreamAndHeader).GetTypeReference<IInterfaceType>());
            mutableTablesStream.ImplementedInterfaces.ImplementInterfaceQuick(typeof(ICliMetadataMutableTableStreamAndHeader).GetTypeReference<IInterfaceType>());
            var reservedA                   = tablesStream.Fields.Add(typeof(uint).WithName("reservedA"));
            var schemataVersion             = tablesStream.Fields.Add(typeof(WordVersion).WithName("schemataVersion"));
            var heapSizes                   = tablesStream.Fields.Add(typeof(CliMetadataHeapSizes).WithName("heapSizes"));
            var reservedB                   = tablesStream.Fields.Add(typeof(byte).WithName("reservedB"));
            var tablesPresent               = tablesStream.Fields.Add(typeof(CliMetadataTableKinds).WithName("tablesPresent"));
            var sortedTables                = tablesStream.Fields.Add(typeof(CliMetadataTableKinds).WithName("sortedTables"));

            var reservedAMutable            = mutableTablesStream.Fields.Add(typeof(uint).WithName("reservedA"), 0.ToPrimitive());
            var schemaVersionMutable        = mutableTablesStream.Fields.Add(typeof(WordVersion).WithName("schemaVersion"), typeof(WordVersion).GetNewExpression(2.ToPrimitive(), 0.ToPrimitive()));
            var reservedBMutable            = mutableTablesStream.Fields.Add(typeof(byte).WithName("reservedB"), 0.ToPrimitive());

            //var heapSizesMutable            = mutableTablesStream.Fields.Add(typeof(CliMetadataHeapSizes).WithName("heapSizes"));

            reservedAMutable.AccessLevel  = AccessLevelModifiers.Private;

            
            reservedA.AccessLevel           =
                schemataVersion.AccessLevel =
                heapSizes.AccessLevel       =
                reservedB.AccessLevel       =
                tablesPresent.AccessLevel   =
                sortedTables.AccessLevel    =
                AccessLevelModifiers.Private;

            var binaryReader = typeof(BinaryReader).GetTypeReference<IClassType>();

            var tablesStreamConstructor               = CreateTablesStreamCtor(tablesStream);
            var tablesStreamReadMethod                = tablesStream.Methods.Add(typeof(void).WithName("Read"));
            tablesStreamReadMethod.AccessLevel        = AccessLevelModifiers.Internal;
            var tablesStreamReadMethod_reader         = tablesStreamReadMethod.Parameters.Add(typeof(EndianAwareBinaryReader).WithName("reader"));
            var tablesStreamReadMethod_metadataRoot   = tablesStreamReadMethod.Parameters.Add(metadataRoot.WithName("metadataRoot"));
            //var tableSubstream                      = tablesStreamReadMethod.Locals.Add(typeof(Substream).WithName("tableSubstream"));
            //tableSubstream.InitializationExpression = new CreateNewObjectExpression(tableSubstream.LocalType, tablesStreamReadMethod_reader.GetReference().GetProperty("BaseStream"), IntermediateGateway.NumberZero, IntermediateGateway.NumberZero, IntermediateGateway.FalseValue);

            //var readNewReader = tablesStreamReadMethod.Locals.Add(typeof(EndianAwareBinaryReader).WithName("tableSubstreamReader"));
            var readByte = binaryReader.Methods[TypeSystemIdentifiers.GetGenericSignatureIdentifier("ReadByte")];
            //readNewReader.InitializationExpression = new CreateNewObjectExpression(readNewReader.LocalType, tableSubstream.GetReference(), typeof(Endianness).GetTypeExpression().GetField("LittleEndian"), IntermediateGateway.FalseValue);
            tablesStreamReadMethod.Comment("Programs are best suited to this kind of code generation.  Lots of interconnected relationships, and lots of room for human error.  Thus, why this generator was created.");
            tablesStreamReadMethod.Comment("Reserved, always 0.");
            tablesStreamReadMethod.Assign(reservedA.GetReference(), binaryReader.Methods[TypeSystemIdentifiers.GetGenericSignatureIdentifier("ReadUInt32")].GetReference(tablesStreamReadMethod_reader.GetReference()).Invoke());
            tablesStreamReadMethod.Comment("Shall be 2.0.");
            tablesStreamReadMethod.Call(schemataVersion.GetReference().GetMethod("Read").Invoke(tablesStreamReadMethod_reader.GetReference()));
            tablesStreamReadMethod.Comment("Bit vector for heap sizes.");
            tablesStreamReadMethod.Assign(heapSizes.GetReference(), readByte.GetReference(tablesStreamReadMethod_reader.GetReference()).Invoke().Cast(typeof(CliMetadataHeapSizes)));
            tablesStreamReadMethod.Comment("Reserved, always 1.");
            tablesStreamReadMethod.Assign(reservedB.GetReference(), readByte.GetReference(tablesStreamReadMethod_reader.GetReference()).Invoke());

            var stringHeapSize = tablesStreamReadMethod.Locals.Add(typeof(CliMetadataReferenceIndexSize).WithName("stringHeapSize"));
            var stringHeapExpression = heapSizes.BitwiseAnd(typeof(CliMetadataHeapSizes).GetTypeExpression().GetField("StringStream")).EqualTo(typeof(CliMetadataHeapSizes).GetTypeExpression().GetField("StringStream"));
            //new BinaryOperationExpression(
            //    new BinaryOperationExpression(
            //        heapSizes.GetReference(), 
            //        CodeBinaryOperatorType.BitwiseAnd,
            //        typeof(CliMetadataHeapSizes).GetTypeExpression()
            //            .GetField("StringStream")),
            //    CodeBinaryOperatorType.IdentityEquality, 
            //    typeof(CliMetadataHeapSizes).GetTypeExpression().GetField("StringStream"));
            stringHeapSize.AutoDeclare = false;
            tablesStreamReadMethod.Add(stringHeapSize.GetDeclarationStatement());
            var stringHeapCondition    = tablesStreamReadMethod.If(stringHeapExpression);
            stringHeapCondition.Assign(stringHeapSize.GetReference(), typeof(CliMetadataReferenceIndexSize).GetTypeExpression().GetField("DWord"));
            stringHeapCondition.Next.Assign(stringHeapSize.GetReference(), typeof(CliMetadataReferenceIndexSize).GetTypeExpression().GetField("Word"));
            var blobHeapSize           = tablesStreamReadMethod.Locals.Add(typeof(CliMetadataReferenceIndexSize).WithName("blobHeapSize"));
            blobHeapSize.AutoDeclare   = false;
            tablesStreamReadMethod.Add(blobHeapSize.GetDeclarationStatement());
            var blobHeapExpression     = heapSizes.BitwiseAnd(typeof(CliMetadataHeapSizes).GetTypeExpression().GetField("BlobStream")).EqualTo(typeof(CliMetadataHeapSizes).GetTypeExpression().GetField("BlobStream"));
            //new BinaryOperationExpression(
            //    new BinaryOperationExpression(
            //        heapSizes.GetReference(), 
            //        CodeBinaryOperatorType.BitwiseAnd, 
            //        typeof(CliMetadataHeapSizes).GetTypeExpression().GetField("BlobStream")),
            //    CodeBinaryOperatorType.IdentityEquality, 
            //    typeof(CliMetadataHeapSizes).GetTypeExpression().GetField("BlobStream"));
            var blobHeapCondition = tablesStreamReadMethod.If(blobHeapExpression);
            blobHeapCondition.Assign(blobHeapSize.GetReference(), typeof(CliMetadataReferenceIndexSize).GetTypeExpression().GetField("DWord"));
            blobHeapCondition.Next.Assign(blobHeapSize.GetReference(), typeof(CliMetadataReferenceIndexSize).GetTypeExpression().GetField("Word"));
            var guidHeapSize       = tablesStreamReadMethod.Locals.Add(typeof(CliMetadataReferenceIndexSize).WithName("guidHeapSize"));
            var guidHeapExpression = heapSizes.BitwiseAnd(typeof(CliMetadataHeapSizes).GetTypeExpression().GetField("GuidStream")).EqualTo(typeof(CliMetadataHeapSizes).GetTypeExpression().GetField("GuidStream"));
            //new BinaryOperationExpression(
            //    new BinaryOperationExpression(
            //        heapSizes.GetReference(),
            //        CodeBinaryOperatorType.BitwiseAnd,
            //        typeof(CliMetadataHeapSizes).GetTypeExpression().GetField("GuidStream")),
            //    CodeBinaryOperatorType.IdentityEquality,
            //    typeof(CliMetadataHeapSizes).GetTypeExpression().GetField("GuidStream"));
            guidHeapSize.AutoDeclare = false;
            tablesStreamReadMethod.Add(guidHeapSize.GetDeclarationStatement());
            var guidHeapCondition = tablesStreamReadMethod.If(guidHeapExpression);
            guidHeapCondition.Assign(guidHeapSize.GetReference(), typeof(CliMetadataReferenceIndexSize).GetTypeExpression().GetField("DWord"));
            guidHeapCondition.Next.Assign(guidHeapSize.GetReference(), typeof(CliMetadataReferenceIndexSize).GetTypeExpression().GetField("Word"));

            tablesStreamReadMethod.Assign(tablesPresent.GetReference(), tablesStreamReadMethod_reader.GetReference().GetMethod("ReadUInt64").Invoke().Cast(typeof(CliMetadataTableKinds)));
            tablesStreamReadMethod.Assign(sortedTables.GetReference(), tablesStreamReadMethod_reader.GetReference().GetMethod("ReadUInt64").Invoke().Cast(typeof(CliMetadataTableKinds)));
            var metadataValidationCheck =
                tablesStreamReadMethod.If(
                    tablesPresent
                    .BitwiseAnd(
                        typeof(CliMetadataTableKinds)
                        .GetTypeExpression()
                        .GetField("SupportedMask")
                        .Invert())
                        .Cast(typeof(ulong))
                    .InequalTo(IntermediateGateway.NumberZero));

            //tablesStreamReadMethod.If(
            //    new BinaryOperationExpression(
            //        new BinaryOperationExpression(
            //            tablesPresent.GetReference(),
            //            CodeBinaryOperatorType.BitwiseAnd, 
            //            typeof(CliMetadataTableKinds).GetTypeExpression().GetField("SupportedMask").Invert()).Cast(typeof(ulong)), 
            //        CodeBinaryOperatorType.IdentityInequality,
            //        IntermediateGateway.NumberZero));
            /*
             metadataValidationCheck.Add(
             * new ThrowStatement(
             *    new CreateNewObjectExpression(
             *      typeof(BadImageFormatException).GetTypeReference(), 
             *      new ExpressionCollection(
             *        new PrimitiveExpression("Unsupported metadata type.")))));
             */
            //metadataValidationCheck.Add(new ThrowStatement(new CreateNewObjectExpression(typeof(BadImageFormatException).GetTypeReference(), new ExpressionCollection(new PrimitiveExpression("Unsupported metadata type.")))));
            metadataValidationCheck.Throw(typeof(BadImageFormatException).GetNewExpression("Unsupported metadata type.".ToPrimitive()));

            Dictionary<MetadataHeapTarget, IType> heapTypeReferences = new Dictionary<MetadataHeapTarget, IType>()
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

            Dictionary<MetadataHeapTarget, IMemberParentReferenceExpression> heapReferenceExpressions = new Dictionary<MetadataHeapTarget, IMemberParentReferenceExpression>()
            {
                {
                    MetadataHeapTarget.BlobHeap,
                    new SpecialReferenceExpression(SpecialReferenceKind.This).GetField("metadataRoot").GetProperty("BlobHeap")
                },
                {
                    MetadataHeapTarget.GuidHeap,
                    new SpecialReferenceExpression(SpecialReferenceKind.This).GetField("metadataRoot").GetProperty("GuidHeap")
                },
                {
                    MetadataHeapTarget.StringsHeap,
                    new SpecialReferenceExpression(SpecialReferenceKind.This).GetField("metadataRoot").GetProperty("StringsHeap")
                },
                {
                    MetadataHeapTarget.UserStringsHeap,
                    new SpecialReferenceExpression(SpecialReferenceKind.This).GetField("metadataRoot").GetProperty("UserStringsHeap")
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
            var tableNamespace         = defaultNamespace.Namespaces.Add("Tables");
            var internalTableNamespace = defaultInternalNamespace.Namespaces.Add("Tables");
            int maxTableCount          = tables[tables.Count - 1].Offset + 1;

            for (int i = 0; i < encodings.Count; i++)
            {
                var currentSizeLocal                        = encodings[i].WordSizeLocal = tablesStreamReadMethod.Locals.Add(typeof(CliMetadataReferenceIndexSize).WithName(string.Format("enc{0}", encodings[i].Name)));
                currentSizeLocal.AutoDeclare                = false;
                currentSizeLocal.InitializationExpression   = typeof(CliMetadataReferenceIndexSize).GetTypeExpression().GetField("Word");
                var currentEncodingInterface                = encodings[i].EncodingCommonType = tableNamespace.Parts.Add().Interfaces.Add(string.Format("ICliMetadata{0}Row", encodings[i].Name));
                currentEncodingInterface.SummaryText        = string.Format("Defines the umbrella interface for reference indexes encoded with @s:{0};.", encodings[i].EncodingType.Name /*((IExternTypeReference)encodings[i].EncodingType).TypeInstance.Type.Name*/);
                currentEncodingInterface.AccessLevel        = AccessLevelModifiers.Public;
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

            List<ILocalMember> encodingLocalsDefined = new List<ILocalMember>();
            var offsetLocal = tablesStreamReadMethod.Locals.Add(typeof(long).WithName("currentOffset"));

            foreach (var table in orderedTables)
            {

                var staticReference = (from r in referencesTo
                                       where r.Key == table
                                       where r.Value.Length > 0
                                       select r).Count() > 0;
                var encodingReference = (from e in encodings
                                         where e.Contains(table)
                                         select e).Count() > 0;
                var currentTableClass = internalTableNamespace.Parts.Add().Classes.Add(string.Format("CliMetadata{0}TableReader", table.ShortName));
                var currentMutableTableClass = internalTableNamespace.Parts.Add().Classes.Add(string.Format("CliMetadata{0}MutableTable", table.ShortName));

                currentMutableTableClass.AccessLevel            = 
                    currentTableClass.AccessLevel               = AccessLevelModifiers.Internal;

                currentTableClass.RemarksText                   = 
                    currentMutableTableClass.RemarksText        = table.RemarksText;

                var currentTableLockedInterface                 = tableNamespace.Parts.Add().Interfaces.Add(string.Format("ICliMetadata{0}Table", table.ShortName));
                var currentTableMutableInterface                = tableNamespace.Parts.Add().Interfaces.Add(string.Format("ICliMetadata{0}MutableTable", table.ShortName));

                currentTableMutableInterface.AccessLevel =
                    currentTableLockedInterface.AccessLevel = AccessLevelModifiers.Public;
                if (!string.IsNullOrEmpty(table.SummaryText))
                {
                    currentTableClass.SummaryText = string.Format("Provides a table which {0}", lowerFirst(table.SummaryText));
                    currentTableLockedInterface.SummaryText = string.Format("Defines properties and methods for a table which {0}", lowerFirst(table.SummaryText));
                }
                var currentTableClassCtor                       = currentTableClass.Constructors.Add();
                currentTableClassCtor.AccessLevel               = AccessLevelModifiers.Public;
                var kindProperty                                = currentTableClass.Properties.Add(typeof(CliMetadataTableKinds).WithName("Kind"), true, false);
                table.TableKindExpression                       = typeof(CliMetadataTableKinds).GetTypeExpression().GetField(table.Name);
                kindProperty.IsOverride                         = true;
                kindProperty.GetMethod.Return(table.TableKindExpression);
                kindProperty.AccessLevel                        = AccessLevelModifiers.Public;
                var currentLockedMetadataRoot                   = currentTableClass.Fields.Add(metadataRoot.WithName("metadataRoot"));
                var currentMutableMetadataRoot                  = currentMutableTableClass.Fields.Add(mutableMetadataRoot.WithName("metadataRoot"));
                var currentReader                               = currentTableClass.Fields.Add(typeof(EndianAwareBinaryReader).WithName("reader"));
                var currentSync                                 = currentTableClass.Fields.Add(typeof(object).WithName("syncObject"));
                var currentStream                               = currentTableClass.Fields.Add(typeof(FileStream).WithName("fStream"));
                var currentRowCount                             = currentTableClass.Fields.Add(typeof(uint).WithName("rowCount"));
                currentLockedMetadataRoot.AccessLevel           =
                    currentMutableMetadataRoot.AccessLevel      =
                    currentReader.AccessLevel                   =
                    currentSync.AccessLevel                     =
                    currentStream.AccessLevel                   =
                    currentRowCount.AccessLevel                 = AccessLevelModifiers.Private;

                table.RowCountField                             = currentRowCount;
                var currentTableClassCtor_metadataRoot          = currentTableClassCtor.Parameters.Add(metadataRoot.WithName("metadataRoot"));
                var currentTableClassCtor_readerTriplet         = currentTableClassCtor.Parameters.Add(typeof(Tuple<object, FileStream, EndianAwareBinaryReader>).WithName("readerInfo"));
                //var currentTableClassCtor_reader              = currentTableClassCtor.Parameters.Add(typeof(EndianAwareBinaryReader).WithName("reader"));
                var currentTableClassCtor_rowCount              = currentTableClassCtor.Parameters.Add(typeof(uint).WithName("rowCount"));
                currentTableClassCtor.CascadeTarget             = ConstructorCascadeTarget.Base;
                currentTableClassCtor.CascadeMembers.Add(currentTableClassCtor_metadataRoot.GetReference());
                currentTableClassCtor.CascadeMembers.Add(currentTableClassCtor_rowCount.GetReference());
                currentTableClassCtor.Assign(currentLockedMetadataRoot.GetReference(), currentTableClassCtor_metadataRoot.GetReference());
                currentTableClassCtor.Assign(currentSync.GetReference(), currentTableClassCtor_readerTriplet.GetReference().GetProperty("Item1"));
                currentTableClassCtor.Assign(currentStream.GetReference(), currentTableClassCtor_readerTriplet.GetReference().GetProperty("Item2"));
                currentTableClassCtor.Assign(currentReader.GetReference(), currentTableClassCtor_readerTriplet.GetReference().GetProperty("Item3"));
                currentTableClassCtor.Assign(currentRowCount.GetReference(), currentTableClassCtor_rowCount.GetReference());
                table.SyncField = currentSync;
                table.StreamField = currentStream;
                table.ReaderField = currentReader;

                table.LockedMetadataRootField                   = currentLockedMetadataRoot;
                table.MutableMetadataRootField                  = currentMutableMetadataRoot;
                var currentLockedTableRowClass                  = internalTableNamespace.Parts.Add().Classes.Add(string.Format("CliMetadata{0}LockedTableRow", table.ShortName));
                var currentMutableTableRowClass                 = internalTableNamespace.Parts.Add().Classes.Add(string.Format("CliMetadata{0}MutableTableRow", table.ShortName));
                var currentTableLockedRowInterface              = tableNamespace.Parts.Add().Interfaces.Add(string.Format("{0}Row", currentTableLockedInterface.Name));
                var currentTableMutableRowInterface             = tableNamespace.Parts.Add().Interfaces.Add(string.Format("{0}Row", currentTableMutableInterface.Name));
                currentTableMutableRowInterface.ImplementedInterfaces.Add(currentTableLockedRowInterface);

                currentTableMutableRowInterface.AccessLevel     = 
                    currentTableLockedRowInterface.AccessLevel  = AccessLevelModifiers.Public;
                table.DeclaredLockedTableRowInterface           = currentTableLockedRowInterface;
                table.DeclaredMutableTableRowInterface          = currentTableMutableRowInterface;

                table.DeclaredLockedTableInterface              = currentTableLockedInterface;
                table.DeclaredMutableTableInterface             = currentTableMutableInterface;

                table.DeclaredLockedTableRowClass               = currentLockedTableRowClass;
                table.DeclaredMutableTableRowClass              = currentMutableTableRowClass;

                table.DeclaredLockedTableRowClass.ImplementedInterfaces.ImplementInterfaceQuick(table.DeclaredLockedTableRowInterface);
                table.DeclaredLockedTableInterface.ImplementedInterfaces.Add(typeof(IControlledCollection<>).GetTypeReference<IInterfaceType>().MakeGenericClosure(table.DeclaredLockedTableRowInterface));

                table.DeclaredMutableTableRowClass.ImplementedInterfaces.ImplementInterfaceQuick(table.DeclaredMutableTableRowInterface);
                table.DeclaredMutableTableInterface.ImplementedInterfaces.Add(typeof(IControlledCollection<>).GetTypeReference<IInterfaceType>().MakeGenericClosure(table.DeclaredMutableTableRowInterface));

                var mutableTableAddMethod                       = table.DeclaredMutableTableInterface.Methods.Add(typeof(void).WithName("Add"), new TypedNameSeries(new TypedName(string.Format("{0}ToAdd", lowerFirst(table.ShortName)), table.DeclaredMutableTableRowInterface)));
                var mutableTableRemoveMethod                    = table.DeclaredMutableTableInterface.Methods.Add(typeof(void).WithName("Remove"), new TypedNameSeries(new TypedName(string.Format("{0}ToRemove", lowerFirst(table.ShortName)), table.DeclaredMutableTableRowInterface)));


                table.DeclaredLockedTableRowInterface.ImplementedInterfaces.Add(typeof(ICliMetadataTableRow).GetTypeReference());
                currentTableClass.BaseType = typeof(CliMetadataLazyTable<>).GetTypeReference<IClassType>(currentTableLockedRowInterface);//new TypeReferenceCollection());

                currentMutableTableRowClass.AccessLevel         =
                    currentLockedTableRowClass.AccessLevel      =
                    AccessLevelModifiers.Internal               ;

                if (!string.IsNullOrEmpty(table.SummaryText))
                {
                    currentLockedTableRowClass.SummaryText = string.Format("Provides a locked row class for a locked table which {0}", lowerFirst(table.SummaryText));
                    currentTableLockedRowInterface.SummaryText = string.Format("Defines properties and methods for a locked row in a table which {0}", lowerFirst(table.SummaryText));

                    currentMutableTableRowClass.SummaryText = string.Format("Provides a mutable row class for a mutable table which {0}", lowerFirst(table.SummaryText));
                    currentTableMutableRowInterface.SummaryText = string.Format("Defines properties and methods for a mutable row in a table which {0}", lowerFirst(table.SummaryText));
                }
                var currentTableRowCtor                         = currentLockedTableRowClass.Constructors.Add();
                table.DeclaredTableRowCtor                      = currentTableRowCtor;
                table.DeclaredTableRowCtor.AccessLevel          = AccessLevelModifiers.Internal;
                table.DeclaredTableClassCtor                    = currentTableClassCtor;
                table.DeclaredTableClass                        = currentTableClass;
                table.DeclaredMutableTableClass                 = currentMutableTableClass;

                table.DeclaredTableClass.ImplementedInterfaces.ImplementInterfaceQuick(typeof(ICliMetadataTable).GetTypeReference<IInterfaceType>());
                table.DeclaredTableClass.ImplementedInterfaces.ImplementInterfaceQuick(table.DeclaredLockedTableInterface);

                table.DeclaredMutableTableClass.ImplementedInterfaces.ImplementInterfaceQuick(typeof(ICliMetadataTable).GetTypeReference<IInterfaceType>());
                table.DeclaredMutableTableClass.ImplementedInterfaces.ImplementInterfaceQuick(table.DeclaredMutableTableInterface);

                if (staticReference || encodingReference || table.NeedsIndex)
                {
                    if (staticReference)
                    {
                        var currentWordSizeLocal                        = table.WordSizeLocal = tablesStreamReadMethod.Locals.Add(typeof(CliMetadataReferenceIndexSize).WithName(string.Format("{0}Size", lowerFirst(table.ShortName))));
                        currentWordSizeLocal.AutoDeclare                = false;
                        currentWordSizeLocal.InitializationExpression   = typeof(CliMetadataReferenceIndexSize).GetTypeExpression().GetField("Word");
                    }
                    var currentLockedTableIndexField                    = table.DeclaredLockedTableRowClass.Fields.Add(typeof(uint).WithName("_index"));
                    var currentLockedTableIndexProperty                 = table.DeclaredLockedTableRowClass.Properties.Add(typeof(uint).WithName("Index"), true, false);

                    var currentMutableTableIndexField                   = table.DeclaredMutableTableRowClass.Fields.Add(typeof(uint).WithName("_index"));
                    var currentMutableTableIndexProperty                = table.DeclaredMutableTableRowClass.Properties.Add(typeof(uint).WithName("Index"), true, true);

                    currentMutableTableIndexField.AccessLevel           =
                        currentLockedTableIndexField.AccessLevel        = AccessLevelModifiers.Private;

                    currentMutableTableIndexProperty.AccessLevel        =
                        currentLockedTableIndexProperty.AccessLevel     = AccessLevelModifiers.Public;

                    currentLockedTableIndexProperty.SummaryText         = string.Format("Returns the index of the row within the @s:{0}; since the rows from the containing table are referenced by other tables.", table.DeclaredTableClass.Name);
                    currentMutableTableIndexProperty.SummaryText        = string.Format("Returns/sets the index of the row within the @s:{0}; since the rows from the containing table are referenced by other tables.", table.DeclaredMutableTableClass.Name);

                    currentMutableTableIndexProperty.GetMethod.Return(currentMutableTableIndexField.GetReference());

                    //table.MetadataProperty

                    currentLockedTableIndexField.SummaryText            = 
                        currentMutableTableIndexField.SummaryText       = string.Format("Data member for @s:{0};.", currentLockedTableIndexProperty.Name);


                    currentLockedTableIndexProperty.GetMethod.Return(currentLockedTableIndexField.GetReference());
                    var indexParam = table.DeclaredTableRowCtor.Parameters.Add(typeof(uint).WithName("index"));
                    table.DeclaredTableRowCtor.Assign(currentLockedTableIndexField.GetReference(), indexParam.GetReference());
                }
                //else if (!encodingReference)
                //    Console.WriteLine("No reference to: {0}", table.ShortName);
            }
            foreach (var table in tableLookup.Values)
            {
                var currentClass = table.DeclaredTableClass;
                var currentInterface = table.DeclaredLockedTableInterface;
                var tableRefField = tablesStream.Fields.Add(new TypedName(string.Format("{0}Table", lowerFirst(table.ShortName)), currentClass));
                var tableRefProperty = tablesStream.Properties.Add(new TypedName(string.Format("{0}Table", table.ShortName), currentInterface), true, false);

                //var tableRefMutableField = tableStream

                tableRefField.AccessLevel = AccessLevelModifiers.Private;
                var tableRefNullCheck =
                    tableRefProperty
                    .GetMethod
                    .If(tableRefField.EqualTo(IntermediateGateway.NullValue));
                //new BinaryOperationExpression(
                //    tableRefField.GetReference(),
                //    CodeBinaryOperatorType.IdentityEquality,
                //    IntermediateGateway.NullValue));
                var tableRefLocal = tableRefNullCheck.Locals.Add(typeof(ICliMetadataTable).WithName(tableRefField.Name));
                var ifCondition = tableRefNullCheck.If(new SpecialReferenceExpression(SpecialReferenceKind.This).GetMethod("TryGetValue").Invoke(table.TableKindExpression, tableRefLocal.GetReference().Direct(ParameterCoercionDirection.Out)));
                ifCondition.Assign(tableRefField.GetReference(), tableRefLocal.GetReference().Cast(currentClass));
                tableRefField.SummaryText = string.Format("Data member for @s:{0};.", tableRefProperty.Name);
                tableRefProperty.SummaryText = string.Format("Returns the @s:{0}; for the module.", table.DeclaredTableClass.Name);
                tableRefProperty.RemarksText = "May return null if the metadata is not present in the module.";
                tableRefProperty.GetMethod.Return(tableRefField.GetReference());
                tableRefProperty.AccessLevel = AccessLevelModifiers.Public;
                table.MetadataProperty = tableRefProperty;



                var lockedMetadataRootField = table.DeclaredLockedTableRowClass.Fields.Add(metadataRoot.WithName("metadataRoot"));
                var mutableMetadataRootField = table.DeclaredMutableTableRowClass.Fields.Add(mutableMetadataRoot.WithName("metadataRoot"));
                lockedMetadataRootField.AccessLevel =
                    mutableMetadataRootField.AccessLevel =
                    AccessLevelModifiers.Private;
                table.RowLockedMetadataRootField = lockedMetadataRootField;
                table.RowMutableMetadataRootField = mutableMetadataRootField;

                lockedMetadataRootField.AccessLevel = AccessLevelModifiers.Private;
                var lockedMetadataRootProperty = table.DeclaredLockedTableRowClass.Properties.Add(metadataRoot.WithName("MetadataRoot"), true, false);
                lockedMetadataRootProperty.AccessLevel = AccessLevelModifiers.Public;
                lockedMetadataRootProperty.SummaryText = string.Format("Returns the root of the metadata from which the current @s:{0}; was derived.", table.DeclaredLockedTableRowClass.Name);
                lockedMetadataRootProperty.GetMethod.Return(lockedMetadataRootField.GetReference());
                var mutableMetadataRootProperty = table.DeclaredMutableTableRowClass.Properties.Add(mutableMetadataRoot.WithName("MetadataRoot"), true, false);
                mutableMetadataRootProperty.AccessLevel = AccessLevelModifiers.Public;
                mutableMetadataRootProperty.SummaryText = string.Format("Returns the root of the metadata from which the current @s:{0}; was derived.", table.DeclaredMutableTableRowClass.Name);
                mutableMetadataRootProperty.GetMethod.Return(mutableMetadataRootField.GetReference());
                table.RowLockedMetadataRootProperty = lockedMetadataRootProperty;
                table.RowMutableMetadataRootProperty = mutableMetadataRootProperty;
            }

            foreach (var table in tableLookup.Values)
            {
                var toStringMethod = table.DeclaredLockedTableRowClass.Methods.Add(typeof(string).WithName("ToString"));
                toStringMethod.AccessLevel = AccessLevelModifiers.Public;
                toStringMethod.IsOverride = true;
                toStringMethod.IsFinal = false;
                StringBuilder formatStringBuilder = new StringBuilder();
                formatStringBuilder.AppendFormat("{0}: ", table.ShortName);
                IMalleableExpressionCollection formatArgs = new MalleableExpressionCollection();
                int formatIndex = 0;
                var currentTableRowClass = table.DeclaredLockedTableRowClass;
                var primitiveFormat = string.Empty.ToPrimitive();
                formatArgs.Add(primitiveFormat);
                foreach (var field in table.Values)
                {
                    switch (field.DataType.DataKind)
                    {
                        case FieldDataKind.Encoding:
                            var encodingType                          = field.DataType as IMetadataTableFieldEncodingDataType;
                            var encodedField                          = (field as IMetadataTableEncodedField);
                            field.FieldReference                      = currentTableRowClass.Fields.Add(typeof(uint).WithName(string.Format("{0}Index", lowerFirst(field.FieldName))));
                            field.FieldReference.AccessLevel          = AccessLevelModifiers.Private;
                            field.PropertyReference                   = currentTableRowClass.Properties.Add(encodingType.EncodingCommonType.WithName(field.FieldName), true, false);
                            field.PropertyIndexReference              = currentTableRowClass.Properties.Add(typeof(uint).WithName(string.Format("{0}Index", field.FieldName)), true, false);
                            field.PropertyIndexReference.SummaryText  = string.Format("Returns the decoded index of the @s:{0}; relative to the appropriate table.", field.PropertyReference.Name);
                            encodedField.EncodedField                 = currentTableRowClass.Fields.Add(new TypedName(lowerFirst(encodedField.EncodingIdName), encodingType.EncodingType));
                            encodedField.EncodingProperty             = currentTableRowClass.Properties.Add(new TypedName(encodedField.EncodingIdName, encodingType.EncodingType), true, false);
                            field.PropertyIndexReference.RemarksText  = string.Format("Refer to @s:{0}; to discern the proper table for @s:{1};", encodedField.EncodingProperty.Name, field.PropertyIndexReference.Name);
                            encodedField.EncodedField.AccessLevel     = AccessLevelModifiers.Private;
                            field.PropertyIndexReference.AccessLevel  = AccessLevelModifiers.Public;
                            encodedField.EncodingProperty.AccessLevel = AccessLevelModifiers.Public;
                            encodedField.EncodingProperty.GetMethod.Return(encodedField.EncodedField.GetReference());
                            var ifZeroSwitch =
                                field
                                .PropertyReference
                                .GetMethod
                                .If(
                                field.FieldReference.EqualTo(IntermediateGateway.NumberZero));
                            ifZeroSwitch.Return(IntermediateGateway.NullValue);
                            var kindSwitch = field.PropertyReference.GetMethod.Switch(encodedField.EncodedField.GetReference());

                            foreach (var target in encodingType.Values)
                            {
                                if (target.Item2 == null)
                                    continue;
                                var currentCase = kindSwitch.Case(target.Item1);

                                currentCase.Return(target.Item2.MetadataProperty.GetReference(table.RowLockedMetadataRootField.GetReference().GetProperty("TableStream")).GetIndexer(field.FieldReference.GetReference().Cast(typeof(int).GetTypeReference())));
                            }
                            string encodingTypeString = encodingType.EncodingType.BuildTypeName(typeParameterDisplayMode: TypeParameterDisplayMode.CommentStandard);//new IntermediateCodeTranslatorOptions(true));
                            encodedField.EncodingProperty.SummaryText = string.Format("Returns the @s:{0}; which determines the table that @s:{1}; refers to.", encodingTypeString, field.PropertyIndexReference.Name);
                            StringBuilder encodingPropertyRemarksBuilder = new StringBuilder();
                            encodingPropertyRemarksBuilder.AppendFormat("@s:{0}; encoding @s:{1}; tables:", encodingTypeString, tablesStream.Name);
                            encodingPropertyRemarksBuilder.Append("@table;");
                            encodingPropertyRemarksBuilder.Append("|:-Encoding-|-TableStream Property-|");
                            foreach (var tableKind in encodingType.Values)
                                if (tableKind.Item2 != null)
                                    encodingPropertyRemarksBuilder.AppendFormat("|-@s:{0};-|-@s:{1};-| ", tableKind.Item1, typeof(CliMetadataTableStreamAndHeader).GetTypeExpression().GetProperty(tableKind.Item2.MetadataProperty.Name));
                            encodingPropertyRemarksBuilder.Append("@/table;");
                            encodedField.EncodingProperty.RemarksText = encodingPropertyRemarksBuilder.ToString();
                            field.FieldReference.AccessLevel = AccessLevelModifiers.Private;
                            field.PropertyReference.AccessLevel = AccessLevelModifiers.Public;
                            field.PropertyReference.GetMethod.Return(IntermediateGateway.NullValue);
                            break;
                        case FieldDataKind.HeapIndex:
                            field.FieldReference = currentTableRowClass.Fields.Add(new TypedName(string.Format("{0}Index", lowerFirst(field.FieldName)), typeof(uint).GetTypeReference()));
                            field.FieldReference.AccessLevel = AccessLevelModifiers.Private;
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
                                var signatureBlobType               = field.DataType as IMetadataTableBlobHeapDataType;
                                field.PropertyReference             = currentTableRowClass.Properties.Add(new TypedName(field.FieldName, signatureBlobType.SignatureType), true, false);
                                field.PropertyReference.AccessLevel = AccessLevelModifiers.Public;
                                field.PropertyReference.GetMethod.Return(heapReferenceExpressions[heapTarget].GetMethod("GetSignature", signatureBlobType.SignatureType).Invoke(signatureBlobType.SignatureKind, field.FieldReference.GetReference()));
                                field.FieldReference.AccessLevel    = AccessLevelModifiers.Private;
                                field.PropertyIndexReference        = currentTableRowClass.Properties.Add(typeof(uint).WithName(string.Format("{0}Index", field.FieldName)), true, false);
                            }
                            else
                            {
                                field.PropertyReference             = currentTableRowClass.Properties.Add(heapTypeReferences[heapTarget].WithName(field.FieldName), true, false);
                                field.PropertyReference.AccessLevel = AccessLevelModifiers.Public;
                                field.PropertyReference.GetMethod.Return(heapReferenceExpressions[heapTarget].GetIndexer(field.FieldReference.GetReference()));
                                field.FieldReference.AccessLevel    = AccessLevelModifiers.Private;
                                field.PropertyIndexReference        = currentTableRowClass.Properties.Add(typeof(uint).WithName(string.Format("{0}Index", field.FieldName)), true, false);
                            }
                            field.PropertyIndexReference.SummaryText = string.Format("Returns the index onto the @s:{0}; from which @s:{1}; is derived.", heapPropertyName, field.PropertyReference.Name);
                            if (formatIndex > 0)
                                formatStringBuilder.Append(", ");
                            formatStringBuilder.AppendFormat("{0} = {{{1}}}", field.PropertyReference.Name, formatIndex++);
                            formatArgs.Add(field.PropertyReference.GetReference());
                            break;
                        case FieldDataKind.DataType:
                        case FieldDataKind.CastDataType:
                        case FieldDataKind.SelfsufficientDataType:
                            field.FieldReference = currentTableRowClass.Fields.Add(new TypedName(lowerFirst(field.FieldName), ((MetadataTableTypeDataType)field.DataType).DataType));
                            field.PropertyReference = currentTableRowClass.Properties.Add(new TypedName(field.FieldName, ((MetadataTableTypeDataType)field.DataType).DataType), true, false);
                            field.PropertyReference.AccessLevel = AccessLevelModifiers.Public;
                            field.PropertyReference.GetMethod.Return(field.FieldReference.GetReference());
                            field.FieldReference.AccessLevel = AccessLevelModifiers.Private;
                            if (field.DataType.DataKind != FieldDataKind.CastDataType)
                            {
                                if (formatIndex > 0)
                                    formatStringBuilder.Append(", ");
                                formatStringBuilder.AppendFormat("{0} = {{{1}}}", field.PropertyReference.Name, formatIndex++);
                                formatArgs.Add(field.PropertyReference.GetReference());
                            }
                            break;
                        case FieldDataKind.TableReference:
                            var fieldTable                           = ((MetadataTable)field.DataType);
                            var fieldType                            = tableLookup[fieldTable.TableKind].DeclaredLockedTableRowInterface;
                            field.FieldReference                     = currentTableRowClass.Fields.Add(new TypedName(string.Format("{0}Index", lowerFirst(field.FieldName)), typeof(uint).GetTypeReference()));
                            field.PropertyIndexReference             = currentTableRowClass.Properties.Add(typeof(uint).WithName(string.Format("{0}Index", field.FieldName)), true, false);
                            field.PropertyIndexReference.SummaryText = field.IndexSummary;
                            field.PropertyReference                  = currentTableRowClass.Properties.Add(new TypedName(field.FieldName, fieldType), true, false);
                            field.PropertyReference.AccessLevel      = AccessLevelModifiers.Public;

                            field.PropertyReference.GetMethod.Return(fieldTable.MetadataProperty.GetReference(table.RowLockedMetadataRootField.GetReference().GetProperty("TableStream")).GetIndexer(field.FieldReference.GetReference().Cast(typeof(int))));
                            field.FieldReference.AccessLevel         = AccessLevelModifiers.Private;
                            break;
                        default:
                            return;
                    }
                    if (field.PropertyIndexReference != null)
                    {
                        field.PropertyIndexReference.GetMethod.Return(field.FieldReference.GetReference());
                        field.PropertyIndexReference.AccessLevel     = AccessLevelModifiers.Public;
                    }
                    if (field.SummaryText != null)
                        field.PropertyReference.SummaryText          = string.Format("Returns {0}", lowerFirst(field.SummaryText));
                    field.PropertyReference.RemarksText              = field.RemarksText;
                    field.FieldReference.SummaryText                 = string.Format("Data member for @s:{0};.", field.PropertyReference.Name);
                }
                if (formatArgs.Count > 1)
                {
                    primitiveFormat.Value = formatStringBuilder.ToString();
                    toStringMethod.Return(typeof(string).GetTypeExpression().GetMethod("Format").Invoke(formatArgs.ToArray()));
                }
                else
                    currentTableRowClass.Methods.Remove(toStringMethod.UniqueIdentifier, false);

            }


            var addQuery =
                from t in tables
                orderby t.TableKind
                let encodingQuery =
                    from e in encodings
                    where e.Contains(t)
                    select e
                select new { Table = t, TableKind = t.TableKindExpression, StateMachine = t.StateMachine, ShortName = t.ShortName, WordSizeLocal = t.WordSizeLocal, Encodings = encodingQuery.ToArray(), RowClass = t.DeclaredLockedTableRowClass, RowInterface = t.DeclaredLockedTableRowInterface, Constructor = (CreateConstructorDelegate)((e) => t.DeclaredTableClass.GetNewExpression(e)) };

            //tablesStreamReadMethod.Assign(readCounter.GetReference(), IntermediateGateway.NumberZero);
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
                        tablesStreamReadMethod.Add(tableInfo.WordSizeLocal.GetDeclarationStatement());
                    if (staticReferences.Length > 0)
                        if (encodedReferences.Length > 0)
                            tablesStreamReadMethod.Comment(string.Format("{0} is referenced by the following fields: {1} The following encodings reference it as well:\r\n{2}", tableInfo.ShortName, string.Join(", ", from s in staticReferences
                                                                                                                                                                                                                       select string.Format("{0}.{1}", s.Table.ShortName, s.Field.FieldName)), string.Join(", ", from e in encodedReferences
                                                                                                                                                                                                                                                                                                                 select e.Name)));
                        else
                            tablesStreamReadMethod.Comment(string.Format("{0} is referenced by the following fields: {1}", tableInfo.ShortName, string.Join(", ", from s in staticReferences
                                                                                                                                                                  select string.Format("{0}.{1}", s.Table.ShortName, s.Field.FieldName))));
                    else
                        tablesStreamReadMethod.Comment(string.Format("The following encodings reference {0}: {1}", tableInfo.ShortName, string.Join(", ", from e in encodedReferences
                                                                                                                                                          select e.Name)));
                }
                else
                {
                    if (firstNoEncode)
                    {
                        firstNoEncode = false;
                        tablesStreamReadMethod.Comment(string.Format("{0} is not referenced by anything, so setup is much simpler.", tableInfo.ShortName));
                    }
                    else
                        tablesStreamReadMethod.Comment(string.Format("{0} is not referenced by anything.", tableInfo.ShortName));
                }


                tableInfo.Table.PresenceCheckCondition =
                    tablesPresent.BitwiseAnd(tableInfo.TableKind).EqualTo(tableInfo.TableKind);
                //new BinaryOperationExpression(
                //    new BinaryOperationExpression(
                //        tablesPresent.GetReference(),
                //        CodeBinaryOperatorType.BitwiseAnd,
                //        tableInfo.TableKind),
                //    CodeBinaryOperatorType.IdentityEquality, 
                //    tableInfo.TableKind);
                var tablePresenceCheck = tablesStreamReadMethod.If(tableInfo.Table.PresenceCheckCondition);
                if (encodedReferences.Length > 0 ||
                    staticReferences.Length > 0)
                {
                    var countLocal = tablePresenceCheck.Locals.Add(typeof(uint).WithName(string.Format("{0}Count", lowerFirst(tableInfo.ShortName))));
                    countLocal.InitializationExpression = tablesStreamReadMethod_reader.GetReference().GetMethod("ReadUInt32").Invoke();
                    if (tableInfo.WordSizeLocal != null)
                    {
                        var tableSizeCheck =
                            tablePresenceCheck.If(
                                countLocal.GreaterThan(
                                    typeof(ushort)
                                    .GetTypeExpression()
                                    .GetField("MaxValue")));
                        //tablePresenceCheck.If(
                        //new BinaryOperationExpression(
                        //    countLocal.GetReference(),
                        //    CodeBinaryOperatorType.GreaterThan, 
                        //    typeof(ushort)
                        //.GetTypeExpression()
                        //.GetField("MaxValue")));

                        tableSizeCheck.Assign(tableInfo.WordSizeLocal.GetReference(), typeof(CliMetadataReferenceIndexSize).GetTypeExpression().GetField("DWord"));
                    }
                    foreach (var encoding in tableInfo.Encodings)
                    {
                        tableInfo.RowInterface.ImplementedInterfaces.Add(encoding.EncodingCommonType);

                        bool encodingAdded = true;
                        if (!encodingLocalsDefined.Contains(encoding.WordSizeLocal))
                        {
                            tablesStreamReadMethod.AddBefore(tablePresenceCheck, encoding.WordSizeLocal.GetDeclarationStatement());
                            encodingLocalsDefined.Add(encoding.WordSizeLocal);
                            encodingAdded = false;
                        }
                        //var encodingSizeCheck = tablePresenceCheck.If(new BinaryOperationExpression(tableInfo.CountLocal.GetReference(), CodeBinaryOperatorType.GreaterThan, encoding.WordSizeLocal.GetReference()));
                        var aPart =
                            encoding.WordSizeLocal
                            .EqualTo(
                                typeof(CliMetadataReferenceIndexSize)
                                .GetTypeExpression()
                                .GetField("Word"));
                        //new BinaryOperationExpression(
                        //    encoding.WordSizeLocal.GetReference(),
                        //    CodeBinaryOperatorType.IdentityEquality, 
                        //    typeof(CliMetadataReferenceIndexSize)
                        //    .GetTypeExpression()
                        //    .GetField("Word"));
                        var bPart =
                            countLocal.GreaterThan(
                                (ushort.MaxValue >> encoding.BitEncodingSize)
                                .ToPrimitive());
                        //new BinaryOperationExpression(
                        //    countLocal.GetReference(),
                        //    CodeBinaryOperatorType.GreaterThan, 
                        //    (ushort.MaxValue >> encoding.BitEncodingSize)
                        //    .ToPrimitive());
                        if (!encodingAdded)
                        {
                            if (firstTagDescription)
                            {
                                firstTagDescription = false;
                                tablePresenceCheck.Comment(string.Format("It takes {0} bit{3} to encode indices with the {1} tag, so if the count for any target exceeds 2^{2}, use a DWord.", encoding.BitEncodingSize, encoding.Name, 16 - encoding.BitEncodingSize, encoding.BitEncodingSize == 1 ? string.Empty : "s"));
                            }
                            else
                                tablePresenceCheck.Comment(string.Format("{1} tags take {0} bit{3} to encode, so if count exceeds 2^{2}, use a DWord.", encoding.BitEncodingSize, encoding.Name, 16 - encoding.BitEncodingSize, encoding.BitEncodingSize == 1 ? string.Empty : "s"));

                        }
                        var encodingSizeCheck = tablePresenceCheck.If(aPart.LogicalAnd(bPart));
                        //tablePresenceCheck.IfThen(
                        //    new BinaryOperationExpression(
                        //        aPart, 
                        //        CodeBinaryOperatorType.BooleanAnd, 
                        //        bPart));
                        encodingSizeCheck.Assign(encoding.WordSizeLocal.GetReference(), typeof(CliMetadataReferenceIndexSize).GetTypeExpression().GetField("DWord"));
                    }
                    tablePresenceCheck.Call(new SpecialReferenceExpression(SpecialReferenceKind.This).GetMethod("_Add").Invoke(tableInfo.TableKind, tableInfo.Constructor(tablesStreamReadMethod_metadataRoot.GetReference(), tablesStreamReadMethod_metadataRoot.GetReference().GetProperty("SourceImage").GetMethod("SecureReader").Invoke(), countLocal.GetReference())));
                }
                else
                    tablePresenceCheck.Call(new SpecialReferenceExpression(SpecialReferenceKind.This).GetMethod("_Add").Invoke(tableInfo.TableKind, tableInfo.Constructor(tablesStreamReadMethod_metadataRoot.GetReference(), tablesStreamReadMethod_metadataRoot.GetReference().GetProperty("SourceImage").GetMethod("SecureReader").Invoke(), tablesStreamReadMethod_reader.GetReference().GetMethod("ReadUInt32").Invoke())));

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


            offsetLocal.InitializationExpression = tablesStreamReadMethod_reader.GetReference().GetProperty("BaseStream").GetProperty("Position");//IntermediateGateway.NumberZero;//tablesStreamReadMethod_reader.GetReference().GetProperty("BaseStream").GetProperty("Position");
            offsetLocal.AutoDeclare = false;
            tablesStreamReadMethod.Add(offsetLocal.GetDeclarationStatement());
            foreach (var table in tableLookup.Values)
            {
                var presenceCondition                    = table.PresenceCheckCondition;
                var presenceCheck                        = tablesStreamReadMethod.If(presenceCondition);
                var currentTableRef                      = presenceCheck.Locals.Add(new TypedName(string.Format("current{0}", table.ShortName), table.DeclaredTableClass));
                
                currentTableRef.InitializationExpression = table.MetadataProperty.GetReference().Cast(table.DeclaredTableClass);
                //new SpecialReferenceExpression(SpecialReferenceKind.This).GetIndex(table.TableKindExpression).Cast(table.DeclaredTableClass);

                
                var initializeMethodRef                  = table.InitializeMethod.GetReference(currentTableRef.GetReference()).Invoke(offsetLocal.GetReference());
                foreach (var kind in table.StateMachine.Values)
                {
                    switch (kind.DataType.DataKind)
                    {
                        case FieldDataKind.Encoding:
                            var encoding = kind.DataType as IMetadataTableFieldEncodingDataType;
                            initializeMethodRef.Arguments.Add(encoding.WordSizeLocal.GetReference());
                            break;
                        case FieldDataKind.HeapIndex:
                            var heapType = kind.DataType as IMetadataTableFieldHeapDataType;
                            switch (heapType.Heap)
                            {
                                case MetadataHeapTarget.StringsHeap:
                                    initializeMethodRef.Arguments.Add(stringHeapSize);
                                    break;
                                case MetadataHeapTarget.BlobHeap:
                                    initializeMethodRef.Arguments.Add(blobHeapSize);
                                    break;
                                case MetadataHeapTarget.GuidHeap:
                                    initializeMethodRef.Arguments.Add(guidHeapSize);
                                    break;
                            }
                            break;
                        case FieldDataKind.TableReference:
                            var dataTypeTable = kind.DataType as MetadataTable;
                            initializeMethodRef.Arguments.Add(dataTypeTable.WordSizeLocal);
                            break;
                        default:
                            break;
                    }
                }
                //foreach (var encoding in encodings)
                //    if (encoding.Contains(table))
                //        initializeMethodRef.Arguments.Add(encoding.WordSizeLocal.GetReference());
                presenceCheck.Call(initializeMethodRef);
                /*  
presenceCheck.Assign(
    offsetLocal.GetReference(), 
        new BinaryOperationExpression(
            offsetLocal.GetReference(), 
            CodeBinaryOperatorType.Add, 
            table.LengthProperty.GetReference(currentTableRef.GetReference())));
                 */
                presenceCheck.Assign(
                    offsetLocal.GetReference(),
                    AssignmentOperation.AddAssign,
                    table.LengthProperty.GetReference(currentTableRef.GetReference()));
                //presenceCheck.Assign(offsetLocal.GetReference(), new BinaryOperationExpression(offsetLocal.GetReference(), CodeBinaryOperatorType.Add, table.LengthProperty.GetReference(currentTableRef.GetReference())));
            }

            //tablesStreamReadMethod.Assign(tableSubstream.GetReference().GetProperty("Offset"), new BinaryOperationExpression(tableSubstream.GetReference().GetProperty("Offset"), CodeBinaryOperatorType.Add, tablesStreamReadMethod_reader.GetReference().GetProperty("BaseStream").GetProperty("Position")));
            //tablesStreamReadMethod.Call(tableSubstream.GetReference().GetMethod("SetLength").Invoke(offsetLocal.GetReference()));

            ITypeReferenceExpression semanticsExpression = typeof(MethodSemanticsAttributes).GetTypeExpression();
            IExpression propertySemanticsMask =
                semanticsExpression
                .GetField("Getter")
                .BitwiseOr(
                    semanticsExpression
                    .GetField("Setter"))
                .BitwiseOr(
                    semanticsExpression
                    .GetField("Other"));
            //new BinaryOperationExpression(
            //    new BinaryOperationExpression(
            //        semanticsExpression.GetField("Getter"),
            //        CodeBinaryOperatorType.BitwiseOr, 
            //        semanticsExpression.GetField("Setter")), 
            //    CodeBinaryOperatorType.BitwiseOr, 
            //    semanticsExpression.GetField("Other"));
            IExpression eventSemanticsMask =
                semanticsExpression.GetField("AddOn")
                .BitwiseOr(
                    semanticsExpression.GetField("RemoveOn"))
                .BitwiseOr(
                    semanticsExpression.GetField("Fire"))
                .BitwiseOr(
                    semanticsExpression.GetField("Other"));
            //new BinaryOperationExpression(
            //    new BinaryOperationExpression(
            //        new BinaryOperationExpression(
            //            semanticsExpression.GetField("AddOn"),
            //            CodeBinaryOperatorType.BitwiseOr, 
            //            semanticsExpression.GetField("RemoveOn")),
            //        CodeBinaryOperatorType.BitwiseOr, 
            //        semanticsExpression.GetField("Fire")),
            //    CodeBinaryOperatorType.BitwiseOr, 
            //    semanticsExpression.GetField("Other"));
            IExpression semanticsMask = propertySemanticsMask;
            IExpression semanticsTarget = semanticsExpression.GetField("Getter");
            var methodSemanticsInterface = tableLookup[CliMetadataTableKinds.MethodSemantics].DeclaredLockedTableRowInterface;
            var methodRowInterface = tableLookup[CliMetadataTableKinds.MethodDefinition].DeclaredLockedTableRowInterface;
            var propertyClass = tableLookup[CliMetadataTableKinds.Property].DeclaredLockedTableRowClass;
            var eventClass = tableLookup[CliMetadataTableKinds.Event].DeclaredLockedTableRowClass;
            CreateSemanticsMethodProperty(propertyClass, methodRowInterface, methodSemanticsInterface, "GetMethod", propertySemanticsMask, semanticsExpression.GetField("Getter"), tableLookup[CliMetadataTableKinds.Property].DeclaredLockedTableRowClass.Properties[TypeSystemIdentifiers.GetMemberIdentifier("Methods")].GetReference());
            CreateSemanticsMethodProperty(propertyClass, methodRowInterface, methodSemanticsInterface, "SetMethod", propertySemanticsMask, semanticsExpression.GetField("Setter"), tableLookup[CliMetadataTableKinds.Property].DeclaredLockedTableRowClass.Properties[TypeSystemIdentifiers.GetMemberIdentifier("Methods")].GetReference());
            CreateSemanticsMethodProperty(eventClass, methodRowInterface, methodSemanticsInterface, "OnAdd", eventSemanticsMask, semanticsExpression.GetField("AddOn"), tableLookup[CliMetadataTableKinds.Event].DeclaredLockedTableRowClass.Properties[TypeSystemIdentifiers.GetMemberIdentifier("Methods")].GetReference());
            CreateSemanticsMethodProperty(eventClass, methodRowInterface, methodSemanticsInterface, "OnRemove", eventSemanticsMask, semanticsExpression.GetField("RemoveOn"), tableLookup[CliMetadataTableKinds.Property].DeclaredLockedTableRowClass.Properties[TypeSystemIdentifiers.GetMemberIdentifier("Methods")].GetReference());
            CreateSemanticsMethodProperty(eventClass, methodRowInterface, methodSemanticsInterface, "OnFire", eventSemanticsMask, semanticsExpression.GetField("Fire"), tableLookup[CliMetadataTableKinds.Property].DeclaredLockedTableRowClass.Properties[TypeSystemIdentifiers.GetMemberIdentifier("Methods")].GetReference());

            var methodDefinition = tableLookup[CliMetadataTableKinds.MethodDefinition];
            var rvaField         = methodDefinition["RVA"].FieldReference;

            var bodyProp         = methodDefinition.DeclaredLockedTableRowClass.Properties.Add(typeof(ICliMetadataMethodBody).WithName("Body"), true, false);
            var bodyPropField    = methodDefinition.DeclaredLockedTableRowClass.Fields.Add(typeof(ICliMetadataMethodBody).WithName("body"));
            var bodyPropCheck    = methodDefinition.DeclaredLockedTableRowClass.Fields.Add(typeof(bool).WithName("checkedBody"));
            var typeDefRow       = tableLookup[CliMetadataTableKinds.TypeDefinition].DeclaredLockedTableRowClass;

            typeDefRow.ImplementedInterfaces.ImplementInterfaceQuick(typeof(_ICliTypeParent).GetTypeReference<IInterfaceType>());
            bodyProp.AccessLevel     = AccessLevelModifiers.Public;
            var bodyCheckedCondition = bodyProp.GetMethod.If(bodyPropCheck.GetReference().Not());
            var rvaValidCondition    = bodyCheckedCondition.If(rvaField.InequalTo(IntermediateGateway.NumberZero));
            //new BinaryOperationExpression(rvaField.GetReference(), CodeBinaryOperatorType.IdentityInequality, IntermediateGateway.NumberZero));
            rvaValidCondition.Assign(bodyPropField.GetReference(), typeof(CliMetadataMethodBody).GetNewExpression(methodDefinition.RowLockedMetadataRootField.GetReference(), rvaField.GetReference()));
            bodyCheckedCondition.Assign(bodyPropCheck.GetReference(), IntermediateGateway.TrueValue);
            bodyProp.GetMethod.Return(bodyPropField.GetReference());

            foreach (var table in tables)
            {
                DuplicateMembers(table.DeclaredLockedTableRowClass, table.DeclaredLockedTableRowInterface, table.DeclaredTableClass, table.DeclaredLockedTableInterface);
                if (table.DeclaredLockedTableRowInterface.Properties.Keys.Any(k => k.Name == "Index"))
                {
                    table.DeclaredLockedTableRowInterface.Properties.Remove(TypeSystemIdentifiers.GetMemberIdentifier("Index"), false);
                    table.DeclaredLockedTableRowInterface.ImplementedInterfaces.Add(typeof(ICliMetadataIndexedRow).GetTypeReference());
                }
                DuplicateMembers(table.DeclaredTableClass, table.DeclaredLockedTableInterface, table.DeclaredLockedTableRowClass, table.DeclaredLockedTableRowInterface);
                table.DeclaredLockedTableRowInterface.Properties.Remove(table.DeclaredLockedTableRowInterface.Properties[TypeSystemIdentifiers.GetMemberIdentifier("Size")], false);
                table.DeclaredLockedTableInterface.Properties.Remove(table.DeclaredLockedTableInterface.Properties[TypeSystemIdentifiers.GetMemberIdentifier("Kind")], false);
            }
            //var oldTableStreamAndHeader = (IInterfaceType) constructOverrides[typeof(CliMetadataTableStreamAndHeader)];
            //oldTableStreamAndHeader.ParentTarget.Interfaces.Remove(oldTableStreamAndHeader);
            //var newTableStreamAndHeader = oldTableStreamAndHeader.ParentTarget.Interfaces.Add(oldTableStreamAndHeader.Name);
            //DuplicateMembers(tablesStream, newTableStreamAndHeader, tablesStream, newTableStreamAndHeader);
            //tablesStream.ImplementsList.Add(newTableStreamAndHeader);
            //newTableStreamAndHeader.AccessLevel = AccessLevelModifiers.Public;
            var genericCollectionRef = typeof(IControlledCollection<>).GetTypeReference<IInterfaceType>();
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
                var commonProperty  = currentEncoding.EncodingCommonType.Properties.Add(new TypedName(currentProperty.Name, currentProperty.PropertyType), true, false);

                bool isList = currentProperty.PropertyType.ElementClassification == TypeElementClassification.GenericTypeDefinition && currentProperty.PropertyType.ElementType == genericCollectionRef;
                commonProperty.SummaryText = string.Format("Returns the @s:{0};{3} {2} associated to the row encoded with @s:{1};.", currentProperty.PropertyType.BuildTypeName(typeParameterDisplayMode: TypeParameterDisplayMode.CommentStandard, shortFormGeneric: true), currentEncoding.EncodingType.BuildTypeName(typeParameterDisplayMode: TypeParameterDisplayMode.CommentStandard, shortFormGeneric: true), lowerFirst(currentProperty.Name), isList ? " of" : string.Empty);
                foreach (var item in encodingAndProperty.Set.Concat(new[] { encodingAndProperty.Property }))
                {
                    /* *
                     * A little 'Ambiguity' check, if multiple encodings lifted
                     * the same property out, we re-add it to rows which had it removed
                     * to avoid an ambiguity when the property is accessed in code.
                     * */
                    if (!removedSet.Contains(item))
                    {
                        item.Parent.Properties.Remove(item.UniqueIdentifier, false);
                        removedSet.Add(item);
                    }
                    else if (!item.Parent.Properties.Keys.Contains(item.UniqueIdentifier))
                    {
                        item.HideByName = true;
                        item.Parent.Properties.Add(item.PropertyType.WithName(item.Name), item.CanRead, item.CanWrite);
                    }
                }
            }

            foreach (var encoding in encodings)
            {
                var currentEncodingProperty = encoding.EncodingCommonType.Properties.Add(new TypedName(string.Format("{0}Encoding", encoding.Name), encoding.EncodingType), true, false);
                currentEncodingProperty.SummaryText = string.Format("Returns the source table from which the current @s:{0}; is derived.", encoding.EncodingCommonType.Name);
                foreach (var expTable in encoding.Values)
                {
                    if (expTable.Item2 == null)
                        continue;
                    var currentTableProperty = expTable.Item2.DeclaredLockedTableRowClass.Properties.Add(new TypedName(currentEncodingProperty.Name, currentEncodingProperty.PropertyType), true, false);
                    currentTableProperty.GetMethod.Return(expTable.Item1);
                    currentTableProperty.Implementations.Add(encoding.EncodingCommonType);// PrivateImplementationTarget = encoding.EncodingCommonType;
                }
            }
            /* *
             * Hard-coded requirement.
             * */
            var _typesProp =
                typeDefRow.Properties
                .Add(
                    typeof(IControlledCollection<>)
                    .GetTypeReference<IInterfaceType>(
                        tableLookup[CliMetadataTableKinds.TypeDefinition]
                        .DeclaredLockedTableRowInterface)
                    .WithName("_Types"),
                    true,
                    false);
            _typesProp.GetMethod.Return(new SpecialReferenceExpression(SpecialReferenceKind.This).GetProperty("NestedClasses"));
            _typesProp.Implementations.Add(typeof(_ICliTypeParent).GetTypeReference());//PrivateImplementationTarget = ;
            resultedProject.GetRoot().DefaultNamespace = (IIntermediateNamespaceDeclaration)resultedProject.Namespaces.GetThis("AllenCopeland.Abstraction.Slf");

            /* *
             * In debug mode, write to HTML, otherwise write to CSharp files.
             * *
             * HTML made my development easier so I didn't have to copy the files into the project
             * to notice errors or browse internal member definitions.
             * */
            Console.WriteLine("Writing project...");
            Stopwatch writeTimer = Stopwatch.StartNew();
#if !CS
            var files = WriteProject(Path.GetDirectoryName(GetTypeAssemblyCodeBase(typeof(Program))), resultedProject).ToArray();
#else
            var files = WriteProject(Path.GetDirectoryName(GetTypeAssemblyCodeBase(typeof(Program))), resultedProject).ToArray();
            //WriteProject(resultedProject, Path.GetDirectoryName(GetTypeAssemblyCodeBase(typeof(Program))), ".html", "&nbsp;&nbsp;&nbsp;&nbsp;", true);
#endif
            writeTimer.Stop();
            Console.WriteLine("Writing the project took {0} ms to write {1} files.", writeTimer.Elapsed.TotalMilliseconds, files.Length);
        }

#if false
        private static IDictionary<Type, IType> CreateDualInterfaces(IIntermediateAssembly project, params Type[] targets)
        {
            var result = new Dictionary<Type, IType>();
            //Start by creating the replacement interfaces.
            foreach (var originType in targets)
            {
                IIntermediateNamespaceDeclaration @namespace = null;

                if (!project.Namespaces.ContainsName(originType.Namespace))
                    @namespace = project.Namespaces.Add(originType.Namespace);
                else
                {
                    @namespace = (IIntermediateNamespaceDeclaration)project.Namespaces.GetThis(originType.Namespace);
                    @namespace = @namespace.Parts.Add();
                }
                IIntermediateInterfaceType rInterface;
                result.Add(originType, rInterface = @namespace.Interfaces.Add(string.Format("I{0}", originType.Name)));
                rInterface.AccessLevel = AccessLevelModifiers.Public;
            }

            IDictionary<Type, IType> gPLookup = new Dictionary<Type, IType>();
            foreach (var kvOI in result.ToArray())
            {
                var originType = kvOI.Key;
                var @interface = (IIntermediateInterfaceType)kvOI.Value;
                var properties = originType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                var methods    = (from m in originType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                               where (m.Attributes & MethodAttributes.RTSpecialName) != MethodAttributes.RTSpecialName &&
                                     (m.Attributes & MethodAttributes.SpecialName) != MethodAttributes.SpecialName
                               select m).ToArray();
                foreach (var propertyInfo in properties)
                {
                    var parameterInfos = propertyInfo.GetIndexParameters();
                    if (parameterInfos.Length > 0)
                    {
                        var parameterTypedNames    = new TypedName[parameterInfos.Length];
                        for (int i = 0; i < parameterInfos.Length; i++)
                            parameterTypedNames[i] = ConstructLookup(parameterInfos[i].ParameterType, result).WithName(parameterInfos[i].Name);
                        @interface.Indexers.Add(ConstructLookup(propertyInfo.PropertyType, result).WithName(propertyInfo.Name), new TypedNameSeries(parameterTypedNames), propertyInfo.CanRead, propertyInfo.CanWrite);
                    }
                    else
                        @interface.Properties.Add(ConstructLookup(propertyInfo.PropertyType, result).WithName(propertyInfo.Name), propertyInfo.CanRead, propertyInfo.CanWrite);
                }
                foreach (var methodInfo in methods)
                {
                    var parameterInfos                   = methodInfo.GetParameters();
                    var parameterTypedNames              = new TypedName[parameterInfos.Length];
                    var genericParameterInfos            = methodInfo.GetGenericArguments();
                    var genericParameterConstrainedNames = new GenericParameterData[genericParameterInfos.Length];
                    for (int i = 0; i < genericParameterInfos.Length; i++)
                    {
                        var constraintInfos = genericParameterInfos[i].GetGenericParameterConstraints();
                        var constraintReferences = new IType[constraintInfos.Length];

                        for (int j = 0; j < constraintInfos.Length; j++)
                        {
                            if (constraintInfos[j] == typeof(ValueType))
                                continue;
                            constraintReferences[j] = ConstructLookup(constraintInfos[j], result);
                        }

                        genericParameterConstrainedNames[i] = new GenericParameterData(genericParameterInfos[i].Name, (from c in constraintReferences
                                                                                                                      where c != null
                                                                                                                      select c).ToArray());
                    }
                    for (int i = 0; i < parameterInfos.Length; i++)
                        parameterTypedNames[i] = new TypedName(parameterInfos[i].Name, ConstructLookup(parameterInfos[i].ParameterType, result));
                    var rMethod = @interface.Methods.Add(ConstructLookup(methodInfo.ReturnType, result).WithName(methodInfo.Name), new TypedNameSeries(parameterTypedNames), genericParameterConstrainedNames);
                    for (int i = 0; i < genericParameterInfos.Length; i++)
                        gPLookup.Add(genericParameterInfos[i], rMethod.TypeParameters.Values[i]);
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

        private static IType GPReplacement(IDictionary<Type, IType> gPLookup, IType pType)
        {
            //IType tInst = pType.TypeInstance;
            if (pType is ICliType && pType is IGenericParameter)
            {
                ICliType cType = (ICliType)pType;
                if (cType.Parent is IMethodMember)
                    throw new NotSupportedException();
                else
                {
                    var assem = Assembly.Load(new AssemblyName(cType.Assembly.ToString()));
                    assem.GetType(cType.FullName);
                }
            }
            //if (tInst is IExternType)
            //{
            //    var extType = (IExternType)tInst;
            //    var underlyingType = extType.Type;
            //    if (underlyingType.IsGenericParameter)
            //        pType = ConstructLookup(underlyingType, gPLookup);
            //}
            return pType;
        }
#endif
        private static IType ConstructLookup(Type sourceType, IDictionary<Type, IType> replacements)
        {
            return replacements.ContainsKey(sourceType) ? replacements[sourceType] : sourceType.GetTypeReference();
        }

        private static IInterfaceType CreateDualInterface(IIntermediateAssembly project, Type originType, string overriddenName = null)
        {
            if (overriddenName == null)
                overriddenName = string.Format("I{0}", originType.Name);
            var @namespace     = project.Namespaces.Add(originType.Namespace);
            var @interface     = @namespace.Interfaces.Add(overriddenName);
            var properties     = originType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            var methods        = originType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (var propertyInfo in properties)
                @interface.Properties.Add(new TypedName());
            return @interface;
        }

        private static void CreateSemanticsMethodProperty(IIntermediateClassType target, IIntermediateInterfaceType methodRowInterface, IInterfaceType methodSemanticsRowInterface, string memberName, IExpression semanticsMask, IExpression semanticsTarget, IMemberParentReferenceExpression methodsSource)
        {
            var semanticsMethodProperty      = target.Properties.Add(methodRowInterface.WithName(memberName), true, false);
            var semanticsMethodCheckField    = target.Fields.Add(typeof(bool).WithName(string.Format("checked{0}", memberName)));
            var semanticsMethodField         = target.Fields.Add(semanticsMethodProperty.PropertyType.WithName(lowerFirst(memberName)));
            var semanticsMethodCheck         = semanticsMethodProperty.GetMethod.If(semanticsMethodCheckField.GetReference().Not());
            var semanticsMethodCheckIterator = semanticsMethodCheck.Enumerate("semanticsMethod", methodsSource);
            semanticsMethodCheck.Assign(semanticsMethodCheckField.GetReference(), IntermediateGateway.TrueValue);
            
            /* *
             * new BinaryOperationExpression(
             *   new BinaryOperationExpression(
             *     semanticsMethodCheckIterator.Local.GetReference().GetProperty("Semantics"), 
             *     CodeBinaryOperatorType.BitwiseAnd, 
             *     semanticsMask), 
             *   CodeBinaryOperatorType.IdentityEquality,
             *   semanticsTarget)
             * */
            var getterMethodIteratorCheck = 
                semanticsMethodCheckIterator.If(
                    semanticsMethodCheckIterator
                    .Local.GetReference().GetProperty("Semantics")
                    .LogicalAnd(semanticsMask)
                    .EqualTo(semanticsTarget));
            getterMethodIteratorCheck.Assign(semanticsMethodField, semanticsMethodCheckIterator.Local.GetReference().GetProperty("Method"));
            semanticsMethodProperty.GetMethod.Return(semanticsMethodField.GetReference());
            semanticsMethodProperty.AccessLevel = AccessLevelModifiers.Public;
        }

        private static void DuplicateMembers(IIntermediateClassType source, IIntermediateInterfaceType destination, IClassType tableClass, IInterfaceType tableInterface)
        {
            if (string.IsNullOrEmpty(destination.SummaryText))
                destination.SummaryText = source.SummaryText;
            if (string.IsNullOrEmpty(destination.RemarksText))
                destination.RemarksText = source.RemarksText;
            foreach (var method in source.Methods.Values)
                if (method.AccessLevel != AccessLevelModifiers.Public ||
                    method.Name == "ToString")
                    continue;
                else
                    DuplicateComments(destination.Methods.Add(new TypedName(method.Name, method.ReturnType), (from p in method.Parameters.Values
                                                                                                                 select new TypedName(p.Name, p.ParameterType)).ToArray()), method, tableClass, tableInterface);
            foreach (var indexer in source.Indexers.Values)
                if (indexer.AccessLevel != AccessLevelModifiers.Public)
                    continue;
                else
                    DuplicateComments(destination.Indexers.Add(indexer.PropertyType.WithName(indexer.Name), new TypedNameSeries((from p in indexer.Parameters.Values
                                                                                                                                 select p.ParameterType.WithName(p.Name)).ToArray()), indexer.CanRead, indexer.CanWrite), indexer, tableClass, tableInterface);

            foreach (var property in source.Properties.Values)
                if (property.AccessLevel != AccessLevelModifiers.Public)
                    continue;
                else
                    DuplicateComments(destination.Properties.Add(new TypedName(property.Name, property.PropertyType), property.CanRead, property.CanWrite), property, tableClass, tableInterface);

        }

        public static void DuplicateComments(IIntermediateMember dest, IIntermediateMember source, IClassType tableClass, IInterfaceType tableInterface)
        {
            if (source.SummaryText != null)
                dest.SummaryText    = source.SummaryText.Replace(string.Format("@s:{0};", ((IDeclaration)source.Parent).Name), string.Format("@s:{0};", ((IDeclaration)dest.Parent).Name)).Replace(string.Format("@s:{0};", tableClass.Name), string.Format("@s:{0};", tableInterface.Name));
            if (source.RemarksText != null)
                dest.RemarksText    = source.RemarksText.Replace(string.Format("@s:{0};", ((IDeclaration)source.Parent).Name), string.Format("@s:{0};", ((IDeclaration)dest.Parent).Name)).Replace(string.Format("@s:{0};", tableClass.Name), string.Format("@s:{0};", tableInterface.Name));
        }

        public static ICSharpShiftExpression MakeLeftShiftCall(IExpression value, IExpression shift)
        {
            return value.Shift(CSharpShiftOperation.LeftShift, shift);
        }

        public static ICSharpShiftExpression MakeRightShiftCall(IExpression value, IExpression shift)
        {
            return value.Shift(CSharpShiftOperation.RightShift, shift);
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

        delegate ICreateInstanceExpression CreateConstructorDelegate(params IExpression[] parameters);

        private static IIntermediateConstructorMember CreateTablesStreamCtor(IIntermediateClassType tablesStream)
        {
            
            var ctor                      = tablesStream.Constructors.Add();
            var originalHeader            = ctor.Parameters.Add(typeof(CliMetadataStreamHeader).WithName("originalHeader"));
            var data                      = ctor.Locals.Add(typeof(Tuple<uint, uint, string>).WithName("data"));
            ctor.AccessLevel              = AccessLevelModifiers.Internal;
            data.AutoDeclare              = true;
            data.InitializationExpression = originalHeader.GetReference().GetMethod("GetData").Invoke();
            ctor.Assign(tablesStream.GetThis().GetProperty("Offset"), data.GetReference().GetProperty("Item1"));
            ctor.Assign(tablesStream.GetThis().GetProperty("Size"), data.GetReference().GetProperty("Item2"));
            ctor.Assign(tablesStream.GetThis().GetProperty("Name"), data.GetReference().GetProperty("Item3"));
            return ctor;
        }

        private static string[] WriteProject(string resultPath, IIntermediateAssembly newAssembly, string extension = "*.cs", IIntermediateCodeTranslatorFormatter formatter = null)
        {
            var opts                     = new IntermediateCodeTranslatorOptions(new DefaultCodeTranslatorFormatterProvider());

            /* Below controls the emission order of the elements.  The default would be verbatim order (the order they were inserted.) */
            CSharpProjectTranslator cspt = new CSharpProjectTranslator(opts);
            opts.AllowPartials           = true;

            opts.TranslationOrder.Add(DeclarationTranslationOrder.Fields);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.Constructors);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.UnaryOperatorCoercions);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.BinaryOperatorCoercions);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.TypeCoercions);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.Properties);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.Methods);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.Classes);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.Delegates);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.Enums);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.Interfaces);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.Structs);

            opts.ElementOrderingMethod   = TranslationOrderKind.Specific;
            opts.ShortenFilenames        = false;
            return cspt.WriteProject(newAssembly, resultPath);
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
