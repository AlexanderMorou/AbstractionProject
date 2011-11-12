using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Languages.Cil;
using AllenCopeland.Abstraction.Slf.Languages.CSharp;
using AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions;
using AllenCopeland.Abstraction.Slf.Languages.VisualBasic;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.SupplementaryProjects.BugTestApplication.Examples;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Common;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.SupplementaryProjects.BugTestApplication
{
    using linqExample = ExampleHandler.LanguageIntegratedQuery;
    using winformExample = ExampleHandler.WindowsFormsApplication;

    internal static class Program
    {
        private static void Main()
        {
            //UnitTest_GenericCache();
            //UnitTest_Direction();
            //UnitTestForCompiledGenericConstraints();
            //CheckDisambiguation(); return;
            //FullName(); return;
            //arr1(); return;
            RunExamples();
            //Fix001();
        }

        private static void UnitTest_Direction()
        {
            var typeRef = typeof(ExpressionExtensions).GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IClassType>();
            var methods =
                (from m in typeRef.Methods.Values
                 let parameters = (from param in m.Parameters.Values
                                   select new { Name = param.Name, Direction = param.Direction, Type = param.ParameterType }).ToList()
                 orderby m.Name ascending, parameters.Count ascending
                 select new { Name = m.Name, Parameters = parameters, ReturnType = m.ReturnType }).ToList();

        }

        private static void UnitTest_GenericCache()
        {
            var testType = typeof(Dictionary<string, IClassType>).GetTypeReference();
            var testType2 = typeof(Dictionary<,>).GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IClassType>().MakeGenericClosure(typeof(string), typeof(IClassType));

            testType.ElementType.Dispose();
        }

        private static void UnitTestForCompiledGenericConstraints()
        {
            var targetType = typeof(ISignatureMember<,,,>);
            var targetTypeRef = targetType.GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IInterfaceType>();
            var targetTypeGenericParameters =
                    (from genericParameter in targetType.GetGenericArguments()
                     select new { Parameter = genericParameter, UnderlyingType = genericParameter, ParameterConstraints = genericParameter.GetGenericParameterConstraints(), Constructors = genericParameter.GetConstructors() }).ToArray();
            var targetTypeRefGenericParameters =
                    (from genericParameter in targetTypeRef.TypeParameters.Values
                     let compiledParameter = genericParameter as ICompiledGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, IInterfaceType>
                     where compiledParameter != null
                     select new { Parameter = genericParameter, UnderlyingType = compiledParameter.UnderlyingSystemType, ParameterConstraints = genericParameter.Constraints.ToArray(), Constructors = genericParameter.Constructors.Values.ToArray() }).ToArray();
            Console.WriteLine("{0},{1}", targetTypeGenericParameters, targetTypeRefGenericParameters);
            Console.WriteLine(targetTypeRefGenericParameters[1].Parameter.BaseType);
        }

        private static void CheckDisambiguation()
        {
            var testAssembly = LanguageVendors.Microsoft.GetVisualBasicLanguage().CreateAssembly("TestAssembly");
            var testClass = testAssembly.Classes.Add("TestGenericClass", new GenericParameterData("TA", SignaturesData.DefaultConstructorSet), new GenericParameterData("TB", new IType[] { typeof(Tuple<>).GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IClassType>().MakeGenericClosure("TA".GetSymbolType().MakeArray()) }));
            var testMethod = testClass.Methods.Add("TA".GetSymbolType().GetTypedName("TestMethod"), new TypedNameSeries { { "p1", "TC".GetSymbolType() }, { "p2", "TD".GetSymbolType() }, { "p3", "TB".GetSymbolType() } }, new GenericParameterData("TC"), new GenericParameterData("TD", new IType[] { "TC".GetSymbolType() }));
            testMethod.IsVirtual = true;
            testMethod.AccessLevel = AccessLevelModifiers.ProtectedOrInternal;
            var testDerivedClass = testAssembly.Classes.Add("TestDerivedClass", new GenericParameterData("TE", SignaturesData.DefaultConstructorSet));
            var typeParamTE = testDerivedClass.TypeParameters[AstIdentifier.GenericParameter(0)];
            var tbReplacement = typeof(Tuple<>).GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IClassType>().MakeGenericClosure(typeParamTE.MakeArray());
            testDerivedClass.BaseType = testClass.MakeGenericClosure(typeParamTE, tbReplacement);
            var testDerivedMethod = testDerivedClass.Methods.Add("TE".GetSymbolType().GetTypedName("TestMethod"), new TypedNameSeries { { "p1", "TF".GetSymbolType() }, { "p2", "TG".GetSymbolType() }, { "p3", tbReplacement } }, new GenericParameterData("TF"), new GenericParameterData("TG", new IType[] { "TF".GetSymbolType() }));
            testDerivedMethod.AccessLevel = AccessLevelModifiers.ProtectedOrInternal;
            testDerivedMethod.IsOverride = true;
            var testMapping = testDerivedClass.ImplementedInterfaces.ImplementInterface((IInterfaceType)typeof(IDisposable).GetTypeReference(), true);
            var methods = testMapping.IntermediateMethods.ToArray();
            
            var testDerivedClosure = testDerivedClass.MakeGenericClosure(typeof(int));
            var testDerivedClosureMethod = testDerivedClosure.Methods[testDerivedClass.Methods.Values.IndexOf(testDerivedMethod)].Value;
            var testDerivedClosureMethodClosure = testDerivedClosureMethod.MakeGenericClosure(typeof(IClassType).GetTypeReference(), typeof(IIntermediateClassType).GetTypeReference());
            var testDerivedClosureMethodClosureBaseDefinition = testDerivedClosureMethodClosure.BaseDefinition;
            var testEnum = testDerivedClass.Enums.Add("TestEnumeration");
            var testEnumField = testEnum.Fields.Add("TestField", 0);
            //var testEnumClosureField = testDerivedClosure.Enums.Values[0].Fields.Values[0].GetReference();
            var testEnumClosureField = ((IEnumType)((IGenericType)(testEnum)).MakeGenericClosure(typeof(int).GetTypeReference())).Fields.Values[0].GetReference();
            var testEnumFieldReference = testEnumField.GetReference();
            Console.WriteLine(testEnumFieldReference.Fuse("ToString").Fuse(new IExpression[0]));
            Console.WriteLine(testDerivedMethod.BaseDefinition);
        }

        private static void Fix001()
        {
            var testAssembly = LanguageVendors.Microsoft.GetVisualBasicLanguage().CreateAssembly("TestAssembly");
            var testClass = testAssembly.Classes.Add("TestClass");
            /* *
             * Fixed type initializers, so they can't have parameters.
             * */
            //var ti = testClass.TypeInitializer;
            /* *
             * Fixed post-creation parameter injection, ensures
             * it doesn't throw an exception due to an event being
             * called prior to the parameter actually being added.
             * *
             * This caused the signature to be incorrect when it was
             * rekeying the dictionary
             * */
            var ti = testClass.Constructors.Add();
            var p1 = ti.Parameters.Add("Test", typeof(int).GetTypeReference());
            /* *
             * ToDo: Fix parameters dictionary to have Remove method.
             * */
            ti.Parameters.Remove(p1);
        }

        private static void RunExamples()
        {
            var msLangVendor = LanguageVendors.Microsoft;
            IMyVisualBasicAssembly vbAssem = null;
            ICSharpAssembly csAssem = null;

            var winFormsVB = MiscHelperMethods.TimeResultFunc(winformExample.CreateStructureVB, () => vbAssem = msLangVendor.GetVisualBasicLanguage().GetMyProvider().CreateAssembly("VB.Net examples"));
            var winFormsCS = MiscHelperMethods.TimeResultFunc(winformExample.CreateStructureCSharp, () => csAssem = msLangVendor.GetCSharpLanguage().CreateAssembly("CSharp examples"));

            var linqVB = MiscHelperMethods.TimeResultFunc(linqExample.CreateStructureVB, () => vbAssem);
            var linqCS = MiscHelperMethods.TimeResultFunc(linqExample.CreateStructureCSharp, () => csAssem);

            var vbTestFunc = MiscHelperMethods.TimeResultFunc(() =>
            {
                var winForms = winFormsVB();
                var linq = linqVB();
                return Tuple.Create(winForms.Item1, linq.Item1, winForms.Item2, linq.Item2);
            });
            var csTestFunc = MiscHelperMethods.TimeResultFunc(() =>
            {
                var winForms = winFormsCS();
                var linq = linqCS();
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
                var item = vbTestFunc();
                vbAssem.Dispose();
                vbAssem = null;
            });
            //CLIGateway.ClearCache();
            vbAssem = null;
            Console.WriteLine("Initial tests took {0}", jitTest);
            var vbTest = vbTestFunc();
            var csTest = csTestFunc();
            Console.WriteLine("VB.NET test took: {0}", vbTest.Item1);
            Console.WriteLine("\tWinForms: {0}", vbTest.Item2.Item1);
            Console.WriteLine("\tLinq: {0}", vbTest.Item2.Item2);
            Console.WriteLine("C# test took: {0}", csTest.Item1);
            Console.WriteLine("\tWinForms: {0}", csTest.Item2.Item1);
            Console.WriteLine("\tLinq: {0}", csTest.Item2.Item2);
            vbAssem.Dispose();
            csAssem.Dispose();
            CLIGateway.ClearCache();
        }

        private static void FullName()
        {
            var Test = LanguageVendors.Microsoft.GetCSharpLanguage().CreateAssembly("TestAssembly");
            var class1 = Test.Classes.Add("Class1", new GenericParameterData("T1"));
            var method1 = class1.Methods.Add("Method1", new TypedNameSeries() { { "param1", "T1" }, { "param2", "T2" } }, new GenericParameterData("T2"));
            method1.Name = "What'cha?";
            Console.WriteLine(method1.UniqueIdentifier);
            var class2 = method1.Classes.Add("Class2", new GenericParameterData("T3"));
            Console.WriteLine(class2.FullName);
        }

        private static void arr1()
        {
            Console.WriteLine("Chunk copy.");
            Random r = new Random();
            int[][] ds = new int[800][];
            int value = 0;
            for (int i = 0; i < ds.Length; i++)
            {
                var dsCurrent = new int[80];
                for (int j = 0; j < dsCurrent.Length; j++)
                    dsCurrent[j] = ++value;
                ds[i] = dsCurrent;
                value++;
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            const int testCount = 0x10000;
            for (int i = 0; i < testCount; i++)
            {
                var ag = ds.AsParallel().Aggregate((a3, a4) =>
                {
                    if (i == 1)
                        Console.Write("{0}-{1} ",a3.Length, a4.Length);
                    var a5 = new int[a3.Length + a4.Length];
                    a3.CopyTo(a5, 0);
                    a4.CopyTo(a5, a3.Length);
                    return a5;
                });
                ag.ToString();
            }
            sw.Stop();
            var chunkCopy = sw.Elapsed;
            Console.WriteLine("Press any key to start alternative copy.");
            Console.ReadKey(true);
            Console.WriteLine("Alternative copy.");
            sw.Restart();
            for (int i = 0; i < testCount; i++)
            {
                var ag = ArrayExtensions.MergeArrays(ds);
                ag.ToString();
            }
            sw.Stop();
            var fullCopy = sw.Elapsed;
            Console.WriteLine("Chunk copy took {0} for {1} iterations on {3} items whereas\r\nInline group copy took {2}.", chunkCopy, testCount, fullCopy, (from a in ds
                                                                                                                                                             select a.Length).Sum());
        }
    }
}
