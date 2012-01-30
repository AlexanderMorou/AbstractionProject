using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Languages.CSharp;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Languages.VisualBasic;
using AllenCopeland.Abstraction.Utilities.Miscellaneous;
using linqExample = AllenCopeland.Abstraction.SupplementaryProjects.BugTestApplication.Examples.ExampleHandler.LanguageIntegratedQuery;
using winformExample = AllenCopeland.Abstraction.SupplementaryProjects.BugTestApplication.Examples.ExampleHandler.WindowsFormsApplication;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System.Diagnostics;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using AllenCopeland.Abstraction.Slf.Compilers;
using System.Runtime.InteropServices;
using System.Diagnostics.SymbolStore;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using System.Security.Cryptography;
using AllenCopeland.Abstraction.OwnerDrawnControls;
using System.Windows.Forms;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.SupplementaryProjects.BugTestApplication
{

    internal static class Program
    {
        static IType[] intrinsicSet = new[] { typeof(sbyte), typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal), typeof(char) }.ToCollection().ToArray();
        private static void Main()
        {
            IntermediateCompilerBase icb = new IntermediateCompilerBase();
            /* *
             * Easy is the goal.
             * */
            bool first = true;
            for (int i = 0; i < intrinsicSet.Length; i++)
            {
                if (first)
                    first = false;
                else
                    Console.WriteLine();
                var outterType = intrinsicSet[i];
                for (int j = 0; j < intrinsicSet.Length; j++)
                {
                    if (j == i)
                        continue;
                    var innerType = intrinsicSet[j];
                    Console.WriteLine("{0} can{1} implicitly convert to {2}", GetIntrinsicTypeShortName(outterType), outterType.CanConvertTo(innerType) ? string.Empty : "not", GetIntrinsicTypeShortName(innerType));
                }
            }
            //StrongNameTest();
            //AssemblyBuilderTest();
            //CompressionTest();
            //ExtensionsTest();
            //TimedMirrorTest();
            //CreationTest();
            //SeriesCreationTest();
            //var msLangVendor = LanguageVendors.Microsoft;
            //IIntermediateAssembly iia = msLangVendor.GetVisualBasicLanguage().GetMyProvider().CreateAssembly("TestAssembly");
            //var uas = iia.Methods.Add(new TypedName("TestMethod1", typeof(void).GetTypeReference()), new TypedNameSeries(new TypedName("arg1", "IList".GetSymbolType("T1".GetSymbolType()))), new GenericParameterData("T1"));
            //var uasTC1 = uas.Classes.Add("TestClass1");
            //var uas2 = uasTC1.Methods.Add(new TypedName("TestMethod2", typeof(void).GetTypeReference()));
            //var m = typeof(Predicate<string>).GetTypeReference();
            //foreach (var item in m.Members)
            //    Console.WriteLine(item);
            //RunExamples();
        }

        public static string GetIntrinsicTypeShortName(IType intrinsicType)
        {
            switch (intrinsicSet.GetIndexOf(intrinsicType))
            {
                case 0:
                    return "sbyte";
                case 1:
                    return "byte";
                case 2:
                    return "short";
                case 3:
                    return "ushort";
                case 4:
                    return "int";
                case 5:
                    return "uint";
                case 6:
                    return "long";
                case 7:
                    return "ulong";
                case 8:
                    return "float";
                case 9:
                    return "double";
                case 10:
                    return "decimal";
                case 11:
                    return "char";
                default:
                    return null;
            }
        }

        private static void StrongNameTest()
        {
            const int firstBitCount = 400;
            const int lastBitCount = 2048;
            for (int i = firstBitCount; i <= lastBitCount; i += 16)
                WriteStrongName(i);
        }

        private static void WriteStrongName(int bitCount)
        {
            Stopwatch sw = Stopwatch.StartNew();
            IStrongNamePrivateKeyInfo keyPair = new StrongNamePrivateKeyInfo(bitCount);
            sw.Stop();
            var keyPairCreation = sw.Elapsed;
            sw.Restart();
            var privateFile = string.Format("KeyData [{0}, Private].snk", bitCount);
            var publicFile = string.Format("KeyData [{0}, Public].snk", bitCount);
            keyPair.WriteTo(privateFile);
            sw.Stop();
            var privateKeyWrite = sw.Elapsed;
            sw.Restart();
            keyPair.PublicKey.WriteTo(publicFile);
            sw.Stop();
            var publicKeyWrite = sw.Elapsed;
            Console.WriteLine("It took {0} to generate a {1} bit key.", keyPairCreation, bitCount);
            Console.WriteLine("Writing the public key took {0}, the private key took {1}.", publicKeyWrite, privateKeyWrite);
        }

        private static void AssemblyBuilderTest()
        {
            AssemblyName an = new AssemblyName("TestAssembly") { KeyPair = new StrongNamePrivateKeyInfo(384).CreateStrongName() };
            AssemblyBuilder ab = AppDomain.CurrentDomain.DefineDynamicAssembly(an, AssemblyBuilderAccess.Save);

            var mod = ab.DefineDynamicModule("TestAssembly.dll", true);
            var testILDocument = mod.DefineDocument("TestAssembly.il", LanguageGuids.CommonIntermediateLanguage, LanguageGuids.Vendors.Microsoft, SymDocumentType.Text);
            var byteStream = ArrayToExpressionToByteArrayTest();
            var compressedByteStream = StandardCompilerAids.CompressByteStream(byteStream);
            var sizeType = mod.DefineType(string.Format("$ArrayType${0}", compressedByteStream.Length), TypeAttributes.AnsiClass | TypeAttributes.Sealed | TypeAttributes.ExplicitLayout | TypeAttributes.NotPublic, typeof(ValueType), PackingSize.Size1, compressedByteStream.Length);
            
            var type = mod.DefineType("AllenCopeland.Abstraction.SupplementaryProjects.TestAssembly.TestType", TypeAttributes.AnsiClass | TypeAttributes.Abstract | TypeAttributes.Sealed | TypeAttributes.Public);
            var field = type.DefineField("testField", typeof(int), new Type[0], new[] { typeof(IsLong) }, FieldAttributes.Public | FieldAttributes.Static);
            var prop = type.DefineProperty("TestProperty", PropertyAttributes.None, CallingConventions.Standard, typeof(short[ , , , , ]), new Type[ 0 ], new Type[ 0 ], null, null, null);
            var propGetMethod = type.DefineMethod("get_TestProperty", MethodAttributes.Public | MethodAttributes.Static, CallingConventions.Standard, typeof(short[, , , ,]), new Type[0]);
            var propGetILGen = propGetMethod.GetILGenerator();
            propGetILGen.MarkSequencePoint(testILDocument, 49, 3, 49, 4);
            
            var glField = type.DefineInitializedData("TestData", compressedByteStream, FieldAttributes.PrivateScope);//$PST04
            sizeType.CreateType();
            propGetILGen.BeginScope();
            propGetILGen.Emit(OpCodes.Nop);
            propGetILGen.MarkSequencePoint(testILDocument, 52, 15, 52, 31);
            propGetILGen.Emit(OpCodes.Ldc_I4, compressedByteStream.Length);
            propGetILGen.MarkSequencePoint(testILDocument, 53, 15, 53, 29);
            propGetILGen.Emit(OpCodes.Ldc_I4, 5);
            propGetILGen.MarkSequencePoint(testILDocument, 54, 15, 54, 29);
            propGetILGen.Emit(OpCodes.Ldc_I4, 6);
            propGetILGen.MarkSequencePoint(testILDocument, 55, 15, 55, 29);
            propGetILGen.Emit(OpCodes.Ldc_I4, 7);
            propGetILGen.MarkSequencePoint(testILDocument, 56, 15, 56, 29);
            propGetILGen.Emit(OpCodes.Ldc_I4, 8);
            propGetILGen.MarkSequencePoint(testILDocument, 57, 15, 57, 29);
            propGetILGen.Emit(OpCodes.Ldc_I4, 9);
            propGetILGen.MarkSequencePoint(testILDocument, 58, 15, 58, 156);
            propGetILGen.Emit(OpCodes.Ldtoken, glField);
            propGetILGen.EndScope();
            var targetType = typeof(StandardCompilerAids);
            var targetMethod = targetType.GetMethod("InitializeCompressedInt16Array", BindingFlags.Static | BindingFlags.Public, Type.DefaultBinder, CallingConventions.Standard, new Type[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(RuntimeFieldHandle) }, null);
            propGetILGen.MarkSequencePoint(testILDocument, 59, 15, 65, 230);
            propGetILGen.Emit(OpCodes.Call, targetMethod);
            propGetILGen.MarkSequencePoint(testILDocument, 66, 15, 66, 18);
            propGetILGen.Emit(OpCodes.Ret);
            propGetILGen.MarkSequencePoint(testILDocument, 67, 3, 67, 4);
            prop.SetGetMethod(propGetMethod);
            type.CreateType();
            mod.CreateGlobalFunctions();
            ab.Save("TestAssembly.dll");
        }

        private static void ImplicitIntrinsicDataTypeConversions()
        {
            ImplicitIntrisicDataTypeConversions_Add(0, 0, 0, 0, 0, 0, '\0', 0, 0, 0, 0, 0);
        }



        private static void ImplicitIntrisicDataTypeConversions_Add(
            byte vUInt8, ushort vUInt16, uint vUInt32, ulong vUInt64, float vSingle, double vDouble, char vChar,
            sbyte vInt8, short vInt16, int vInt32, long vInt64, decimal vDecimal)
        {
            var addUInt8_1 = vUInt8 + vUInt8;
            var addUInt8_2 = vUInt8 + vUInt16;
            var addUInt8_3 = vUInt8 + vUInt32;
            var addUInt8_4 = vUInt8 + vUInt64;
            var addUInt8_5 = vUInt8 + vSingle;
            var addUInt8_6 = vUInt8 + vDouble;
            var addUInt8_7 = vUInt8 + vChar;
            var addUInt8_8 = vUInt8 + vInt8;
            var addUInt8_9 = vUInt8 + vInt16;
            var addUInt8_10 = vUInt8 + vInt32;
            var addUInt8_11 = vUInt8 + vInt64;
            var addUInt8_12 = vUInt8 + vDecimal;

            var addUInt16_1 = vUInt16 + vUInt16;
            var addUInt16_2 = vUInt16 + vUInt32;
            var addUInt16_3 = vUInt16 + vUInt64;
            var addUInt16_4 = vUInt16 + vSingle;
            var addUInt16_5 = vUInt16 + vDouble;
            var addUInt16_6 = vUInt16 + vChar;
            var addUInt16_7 = vUInt16 + vInt8;
            var addUInt16_8 = vUInt16 + vInt16;
            var addUInt16_9 = vUInt16 + vInt32;
            var addUInt16_10 = vUInt16 + vInt64;
            var addUInt16_11 = vUInt16 + vDecimal;

            var addUInt32_1 = vUInt32 + vUInt32;
            var addUInt32_2 = vUInt32 + vUInt64;
            var addUInt32_3 = vUInt32 + vSingle;
            var addUInt32_4 = vUInt32 + vDouble;
            var addUInt32_5 = vUInt32 + vChar;
            var addUInt32_6 = vUInt32 + vInt8;
            var addUInt32_7 = vUInt32 + vInt16;
            var addUInt32_8 = vUInt32 + vInt32;
            var addUInt32_9 = vUInt32 + vInt64;
            var addUInt32_10 = vUInt32 + vDecimal;

            var addUInt64_1 = vUInt64 + vUInt64;
            var addUInt64_2 = vUInt64 + vSingle;
            var addUInt64_3 = vUInt64 + vDouble;
            var addUInt64_4 = vUInt64 + vChar;
            var addUInt64_5 = vUInt64 + vDecimal;

            var addSingle_1 = vSingle + vSingle;
            var addSingle_2 = vSingle + vDouble;
            var addSingle_3 = vSingle + vChar;
            var addSingle_4 = vSingle + vInt8;
            var addSingle_5 = vSingle + vInt16;
            var addSingle_6 = vSingle + vInt32;
            var addSingle_7 = vSingle + vInt64;

            var addDouble_1 = vDouble + vDouble;
            var addDouble_2 = vDouble + vChar;
            var addDouble_3 = vDouble + vInt8;
            var addDouble_4 = vDouble + vInt16;
            var addDouble_5 = vDouble + vInt32;
            var addDouble_6 = vDouble + vInt64;

            var addChar_1 = vChar + vChar;
            var addChar_2 = vChar + vInt8;
            var addChar_3 = vChar + vInt16;
            var addChar_4 = vChar + vInt32;
            var addChar_5 = vChar + vInt64;
            var addChar_6 = vChar + vDecimal;


            var addInt8_1 = vInt8 + vInt8;
            var addInt8_2 = vInt8 + vInt16;
            var addInt8_3 = vInt8 + vInt32;
            var addInt8_4 = vInt8 + vInt64;
            var addInt8_5 = vInt8 + vDecimal;

            var addInt16_1 = vInt16 + vInt16;
            var addInt16_2 = vInt16 + vInt32;
            var addInt16_3 = vInt16 + vInt64;
            var addInt16_4 = vInt16 + vDecimal;

            var addInt32_1 = vInt32 + vInt32;
            var addInt32_2 = vInt32 + vInt64;
            var addInt32_3 = vInt32 + vDecimal;

            var addInt64_1 = vInt64 + vInt64;
            var addInt64_2 = vInt64 + vDecimal;

            var addDecimal = vDecimal + vDecimal;

        }

        private static byte[] ArrayToExpressionToByteArrayTest()
        {
            var result = new short[5, 6, 7, 8, 9];
            short i = 0;
            foreach (var indices in result.Iterate())
                result.SetValue((short) ((++i) % 254), indices);
            return result.ToExpression<short>(ExpressionExtensions.ToPrimitive).ConvertToByteArray();
        }

        private static void TimedMirrorTest()
        {
            var timerFunc = MiscHelperMethods.TimeActionFunc(MirrorTest);
            var time1 = timerFunc();
            var time2 = timerFunc();
            Console.WriteLine("Took {0} to process first, and {1} to process secondarily.", time1, time2);
        }

        private static void MirrorTest()
        {
            Stopwatch sw = Stopwatch.StartNew();
            var targetType = typeof(CSharpExpressionExtensions);
            var assembly = LanguageVendors.Microsoft.GetCSharpLanguage().CreateAssembly("Mirror Test");
            assembly.AssemblyInformation.AssemblyVersion = new IntermediateVersion(1, 2, 345);
            assembly.DefaultNamespace = assembly.Namespaces.Add("AllenCopeland.Abstraction.SupplementaryProjects.MirrorTest");
            var classCopy = assembly.DefaultNamespace.Classes.Add(targetType.Name);
            sw.Stop();
            var dk = sw.Elapsed;
            sw.Reset();
            sw.Start();
            classCopy.SpecialModifier = SpecialClassModifier.Static;
            var targetTypeRef = targetType.GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IClassType>();
            /* *
             * Parallel manipulation
             * */
            sw.Stop();
            var dk2 = sw.Elapsed;
            sw.Restart();
            classCopy.SuspendDualLayout();
            sw.Stop();
            var dk3 = sw.Elapsed;
            sw.Restart();
            //(from m in targetTypeRef.Methods.Values.AsParallel()
            // let method = classCopy.Methods.Add(new TypedName(m.Name, m.ReturnType),
            //                              new TypedNameSeries(from parameter in m.Parameters.Values
            //                                                  select new TypedName(parameter.Name, parameter.ParameterType, parameter.Direction)))
            // let call = method.Call(m.GetReference(), (from parameter in method.Parameters.Values
            //                                           select parameter.GetReference()).ToArray())
            // select method).ToArray();
            Parallel.ForEach(targetTypeRef.Methods.Values,
                originalMethod =>
                {
                    var originalParameterValues = originalMethod.Parameters.Values;
                    int paramCount = originalMethod.Parameters.Count;
                    TypedName[] parameters = new TypedName[paramCount];
                    IParameterReferenceExpression[] parameterReferences = new IParameterReferenceExpression[paramCount];
                    for (int i = 0; i < paramCount; i++)
                    {
                        var originalParameter = originalParameterValues[i];
                        parameters[i] = new TypedName(originalParameter.Name, originalParameter.ParameterType, originalParameter.Direction);
                    }
                    var newMethod = classCopy.Methods.Add(new TypedName(originalMethod.Name, originalMethod.ReturnType), new TypedNameSeries(parameters));
                    var newParameterValues = newMethod.Parameters.Values;

                    for (int i = 0; i < paramCount; i++)
                        parameterReferences[i] = newParameterValues[i].GetReference();
                    //newMethod.Call(originalMethod.GetReference(), parameterReferences);
                    
                });
            sw.Stop();
            var dk4 = sw.Elapsed;
            sw.Restart();
            classCopy.ResumeDualLayout();
            sw.Stop();
            var dk5 = sw.Elapsed;
            Console.WriteLine("{0} - {1} - {2} - {3} - {4}", dk, dk2, dk3, dk4, dk5);
        }

        private static void ExtensionsTest()
        {
            var firstStep = MiscHelperMethods.TimeAction(() => DiagnoseTimeForExtensions());
            var secondStep = MiscHelperMethods.TimeResult(DiagnoseTimeForExtensions);
            Console.WriteLine("It took {0} to iterate the members the first time.\r\n" +
                              "It took {1} to iterate the members the second time.\r\n" +
                              "There were a total of {2} methods and {3} parameters", firstStep, secondStep.Item1, secondStep.Item2.Item1, secondStep.Item2.Item2);
        }

        private static Tuple<int, int> DiagnoseTimeForExtensions()
        {
            var type = typeof(CSharpExpressionExtensions).GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IClassType>();
            int parameterCount = 0;
            object locker = new object();
            var set = (from m in type.Methods.Values
                       where m.IsExtensionMethod
                       orderby m.Name ascending
                       select m).ToArray();
            Parallel.ForEach(set, method =>
            {
                int pc = method.Parameters.Count;
                lock (locker)
                    parameterCount += pc;
            });
            return Tuple.Create(set.Length, parameterCount);
            //Console.WriteLine("There are {0} methods with {1} parameters in total.", type.Methods.Count, parameterCount);
        }

        private static void CreationTest()
        {
            Console.WriteLine("Took {0} primarily", MiscHelperMethods.TimeAction(Execute));
            Console.WriteLine("Press any key to continue...");
            CLIGateway.ClearCache();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.ReadKey(true);
            Console.WriteLine("Took {0} secondarily", MiscHelperMethods.TimeAction(Execute));
            Console.WriteLine("Press any key to continue...");
            CLIGateway.ClearCache();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.ReadKey(true);
            Console.WriteLine("Took {0} tertiarily", MiscHelperMethods.TimeAction(Execute));
            Console.WriteLine("Press any key to exit...");
            CLIGateway.ClearCache();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.ReadKey(true);
        }

        private static void Execute()
        {
            //SeriesCreationTest();
            var assembly = LanguageVendors.Microsoft.GetVisualBasicLanguage().GetProvider().CreateAssembly("TestAssembly");
            var testNamespace = assembly.Namespaces.Add("TestNamespace");
            var testMethod = assembly.Methods.Add(new TypedName("TestMethod", typeof(int).GetTypeReference()));
            testMethod.ReturnTypeMetadata.OptionalModifiers.Add(typeof(IsLong).GetTypeReference());
            var testField = assembly.Fields.Add(new TypedName("testField", typeof(double).GetTypeReference()));
            var testClass = assembly.Classes.Add("TestClass");
            var testInterface = assembly.Interfaces.Add("ITestInterface");
            var testDelegate = assembly.Delegates.Add("TestDelegate");
            var testEnum = assembly.Enums.Add("TestEnum");
            var testStruct = assembly.Structs.Add("TestStruct");
            var testClassMethod = testClass.Methods.Add(new TypedName("TestClassMethod", CommonTypeRefs.Void));
            var testClassProperty = testClass.Properties.Add(new TypedName("TestClassProperty", CommonTypeRefs.Object));
            var testClassBinaryCoercion = testClass.BinaryOperatorCoercions.Add(Slf.Abstract.Members.CoercibleBinaryOperators.Add, typeof(int).GetTypeReference());
            var testClassConstructor = testClass.Constructors.Add(new TypedName("testParam", typeof(double).GetTypeReference()));
            var testClassEvent = testClass.Events.Add(new TypedName("testClassEvent", typeof(Action).GetTypeReference()));
            var testClassField = testClass.Fields.Add(new TypedName("testClassField", typeof(float).GetTypeReference()));
            var testClassIndexer = testClass.Indexers.Add(new TypedName("TestClassIndexer", typeof(Decimal).GetTypeReference()), new TypedNameSeries() { { "index", typeof(int).GetTypeReference() } }, true, true);
            var testClassTypeCoercion = testClass.TypeCoercions.Add(TypeConversionRequirement.Explicit, TypeConversionDirection.FromContainingType, typeof(int).GetTypeReference());
            var testClassUnaryOperator = testClass.UnaryOperatorCoercions.Add(CoercibleUnaryOperators.Complement);
            var testClassIndexerSetMethod = testClassIndexer.SetMethod;
            testClassIndexerSetMethod.Assign(testClassField.GetReference(), 0F.ToPrimitive());
            var testStructMethod = testStruct.Methods.Add(new TypedName("TestStructMethod", CommonTypeRefs.Void));
            var testStructProperty = testStruct.Properties.Add(new TypedName("TestStructProperty", CommonTypeRefs.Object));
            var testStructBinaryCoercion = testStruct.BinaryOperatorCoercions.Add(Slf.Abstract.Members.CoercibleBinaryOperators.Add, typeof(int).GetTypeReference());
            var testStructConstructor = testStruct.Constructors.Add(new TypedName("testParam", typeof(double).GetTypeReference()));
            var testStructEvent = testStruct.Events.Add(new TypedName("testStructEvent", typeof(Action).GetTypeReference()));
            var testStructField = testStruct.Fields.Add(new TypedName("testStructField", typeof(float).GetTypeReference()));
            var testStructIndexer = testStruct.Indexers.Add(new TypedName("TestStructIndexer", typeof(Decimal).GetTypeReference()), new TypedNameSeries() { { "index", typeof(int).GetTypeReference() } }, true, true);
            var testStructTypeCoercion = testStruct.TypeCoercions.Add(TypeConversionRequirement.Explicit, TypeConversionDirection.FromContainingType, typeof(int).GetTypeReference());
            var testStructUnaryOperator = testStruct.UnaryOperatorCoercions.Add(CoercibleUnaryOperators.Complement);
            var testStructIndexerSetMethod = (IIntermediateStructMethodMember)testStructIndexer.SetMethod;
            testStructIndexerSetMethod.Assign(testStructField.GetReference(testStruct.GetThis()), 0F.ToPrimitive());
            var testInterfaceMethod = testInterface.Methods.Add(new TypedName("TestInterfaceMethod", CommonTypeRefs.Void));
            var testInterfaceProperty = testInterface.Properties.Add(new TypedName("TestInterfaceProperty", CommonTypeRefs.TaskOfT.MakeGenericClosure(typeof(int))));
            var testInterfacePropertySetMethod = (IIntermediateInterfaceMethodMember)testInterfaceProperty.SetMethod;
            var testInterfaceIndexer = testInterface.Indexers.Add(new TypedName("TestInterfaceIndexer", typeof(Decimal).GetTypeReference()), new TypedNameSeries() { { "index", typeof(int).GetTypeReference() } }, true, true);
            var testInterfaceIndexerSetMethod = (IIntermediateInterfaceMethodMember)testInterfaceIndexer.SetMethod;
            var valueParam = testInterfacePropertySetMethod.Parameters[AstIdentifier.Member("value")];
            valueParam.Metadata.OptionalModifiers.Add(typeof(int).GetTypeReference());
            assembly.Dispose();
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            
        }

        private static void SeriesCreationTest()
        {
            Console.WriteLine("Took {0} primarily", MiscHelperMethods.TimeAction(CreateSeries));
            Console.WriteLine("Press any key to continue...");
            CLIGateway.ClearCache();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.ReadKey(true);
            Console.WriteLine("Took {0} secondarily", MiscHelperMethods.TimeAction(CreateSeries));
            Console.WriteLine("Press any key to continue...");
            CLIGateway.ClearCache();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.ReadKey(true);
            Console.WriteLine("Took {0} tertiarily", MiscHelperMethods.TimeAction(CreateSeries));
            Console.WriteLine("Press any key to exit...");
            CLIGateway.ClearCache();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.ReadKey(true);
        }

        private class TestLockContext
        {
            public int TestIndex { get; private set; }
            public TestLockContext(int testIndex)
            {
                this.TestIndex = testIndex;
            }
            public override string ToString()
            {
                return string.Format("Lock {0}", this.TestIndex);
            }
        }

        private static void CreateSeries()
        {
#if DEBUG
            const int testCount = 60;
            const int paramCount = 60;
#else
            const int testCount = 203;
            const int paramCount = 100;
#endif
            int pC = 0;
            object pcLock = new object();
            var assembly = LanguageVendors.Microsoft.GetVisualBasicLanguage().GetMyProvider().CreateAssembly("TestAssembly");
            //Parallel.For(1, testCount + 1, new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount / 3 }, i =>
            var dataSet =
                (from i in 1.RangeTo(testCount + 1)
                 let lockObject = new TestLockContext(i)
                 let genericParameters = (from k in 1.RangeTo(i)
                                          select new GenericParameterData(string.Format("T{0}", k))).ToArray()
                 let Identifier = AstIdentifier.Type("TestType", i - 1)
                 from j in 0.RangeTo(paramCount)
                 select new
                 {
                     CurrentI = i,
                     CurrentJ = j,
                     GenericParameterData = genericParameters,
                     Identifier = Identifier,
                     LockContext = lockObject
                 }).ToArray();
            Parallel.ForEach(dataSet, dataElement =>
            {
                IIntermediateStructType currentStruct;
                IStructType currentStructRef;
                lock (dataElement.LockContext)
                {
                    if (!assembly.Structs.TryGetValue(dataElement.Identifier, out currentStructRef))
                        currentStruct = assembly.Structs.Add("TestType", dataElement.GenericParameterData);
                    else
                        currentStruct = (IIntermediateStructType)currentStructRef;

                }
                var parameters = (from k in 1.RangeTo(dataElement.CurrentJ + 1)
                                  select new TypedName(string.Format("param{0}", k), (k < dataElement.CurrentI) ? currentStruct.TypeParameters.Values[k - 1] : typeof(int).GetTypeReference()));
                currentStruct.Indexers.Add(new TypedName(string.Format("TestIndexer{0}", dataElement.CurrentJ + 1), CommonTypeRefs.ObjectArray), new TypedNameSeries(parameters.ToArray()));
            });
            //for (int i = 1; i <= testCount; i++)
            //{
            //    var currentStruct = (IIntermediateStructType)assembly.Structs.Add("TestType", (from k in 1.RangeTo(i)
            //                                                                                   select new GenericParameterData(string.Format("T{0}", k))).ToArray());
            //    currentStruct.SuspendDualLayout();
            //    Parallel.For(0, paramCount, new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount / 2 }, j =>
            //    //for (int j = 0; j < paramCount; j++)
            //    {
            //        var parameters = (from k in 1.RangeTo(j + 1)
            //                          select new TypedName(string.Format("param{0}", k), (k < i) ? currentStruct.TypeParameters.Values[k - 1] : typeof(int).GetTypeReference()));
            //        currentStruct.Indexers.Add(new TypedName(string.Format("TestIndexer{0}", j + 1), CommonTypeRefs.ObjectArray), new TypedNameSeries(parameters.ToArray()));
            //    });
            //    lock (pcLock)
            //        pC += (int)((double)paramCount * (((double)paramCount - 1) / 2));
            //    currentStruct.ResumeDualLayout();
            //}//);

            var m = (from currentStruct in assembly.Structs.Values
                     select new
                     {
                         Struct = currentStruct,
                         Indexers = (from indexer in currentStruct.Indexers.Values
                                     let Get = indexer.GetMethod as IIntermediateStructMethodMember
                                     let Set = indexer.SetMethod as IIntermediateStructMethodMember
                                     select new { Indexer = indexer, Get = new { Method = Get, Parameters = Get.Parameters.Values.ToArray() }, Set = new { Method = Set, Parameters = Set.Parameters.Values.ToArray() } }).ToArray()
                     }).ToArray();
            assembly.Dispose();
            //CLIGateway.ClearCache();
        }

        private static void RunExamples()
        {
            var msLangVendor = LanguageVendors.Microsoft;
            ICoreVisualBasicAssembly vbAssem = null;
            ICSharpAssembly csAssem = null;

            var linqVB = MiscHelperMethods.TimeResultFunc(linqExample.CreateStructureVB, () => vbAssem = msLangVendor.GetVisualBasicLanguage().GetProvider().CreateAssembly("VB.Net examples"));
            var linqCS = MiscHelperMethods.TimeResultFunc(linqExample.CreateStructureCSharp, () => csAssem = msLangVendor.GetCSharpLanguage().CreateAssembly("CSharp examples"));

            var winFormsVB = MiscHelperMethods.TimeResultFunc(winformExample.CreateStructureVB, () => vbAssem);
            var winFormsCS = MiscHelperMethods.TimeResultFunc(winformExample.CreateStructureCSharp, () => csAssem);

            var vbTestFunc = MiscHelperMethods.TimeResultFunc(() =>
            {
                var linq = linqVB();
                var winForms = winFormsVB();
                return Tuple.Create(winForms.Item1, linq.Item1, winForms.Item2, linq.Item2);
            });
            var csTestFunc = MiscHelperMethods.TimeResultFunc(() =>
            {
                var linq = linqCS();
                var winForms = winFormsCS();
                return Tuple.Create(winForms.Item1, linq.Item1, winForms.Item2, linq.Item2);
            });
            
            Console.WriteLine("Running initial test...");
            Console.WriteLine("* * * * * * * * * * * * * * * * * * * * * * * * *");
            Console.WriteLine("* If the native image has been generated for    *");
            Console.WriteLine("* this project and its dependencies, then this  *");
            Console.WriteLine("* caches commonly used types; otherwise, this   *");
            Console.WriteLine("* also accounts for JIT overhead.               *");
            Console.WriteLine("* * * * * * * * * * * * * * * * * * * * * * * * *");
            var jitTest = MiscHelperMethods.TimeAction(() =>
            {
                using (vbTestFunc().Item2.Item3.Item1) { vbAssem = null; }
                using (csTestFunc().Item2.Item3.Item1) { csAssem = null; }
                //CLIGateway.ClearCache();
            });
            Console.WriteLine("Initial tests took {0}", jitTest);
            CLIGateway.ClearCache();
            var vbTest = vbTestFunc();
            CLIGateway.ClearCache();
            var csTest = csTestFunc();
            Console.WriteLine("VB.NET test took: {0}", vbTest.Item1);
            Console.WriteLine("\tWinForms: {0}", vbTest.Item2.Item1);
            Console.WriteLine("\tLinq: {0}", vbTest.Item2.Item2);
            Console.WriteLine();
            Console.WriteLine("C# test took: {0}", csTest.Item1);
            Console.WriteLine("\tWinForms: {0}", csTest.Item2.Item1);
            Console.WriteLine("\tLinq: {0}", csTest.Item2.Item2);
            Console.WriteLine();
            vbAssem.Dispose();
            csAssem.Dispose();
            var dict = typeof(IDictionary<double,float>).GetTypeReference();
            var prop = (IInterfacePropertyMember)dict.Members[AstIdentifier.Member("Keys")].Entry;
            var propGet = prop.GetMethod;
            var pgrt = propGet.ReturnType;
            var cacheCount = CLIGateway.CacheEnumerator.Count();
            var cacheClear = MiscHelperMethods.TimeAction(CLIGateway.ClearCache);
            Console.WriteLine("It took {0} to clear the type-reference cache of {1} types.", cacheClear, cacheCount);
        }

    }
}
