using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Languages.Cil;
using AllenCopeland.Abstraction.Slf.Languages.CSharp;
using AllenCopeland.Abstraction.Utilities;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.SupplementaryProjects.TestCli
{
    internal class Program
    {
        public static void Main()
        {
            ICliType test = null;
            var identityManager = AstExtensions.CreateIdentityManager(CliFrameworkPlatform.x86Platform);
            var assemblies = new[] { (ICliAssembly)identityManager.ObtainAssemblyReference(typeof(int).Assembly), (ICliAssembly)identityManager.ObtainAssemblyReference(typeof(Uri).Assembly), (ICliAssembly)identityManager.ObtainAssemblyReference(typeof(DataRow).Assembly) };
            
            //var firstMethod = test.MetadataEntry.Methods.First(m => m.Name == "Dispose");
            var enumerateMethodsTime = MiscHelperMethods.CreateActionOfTime<IEnumerable<ICliAssembly>>(EnumerateMethods);
            Console.WriteLine("Took {0} ms to enumerate methods.", enumerateMethodsTime(assemblies).TotalMilliseconds);
            Console.WriteLine("Took {0} ms to enumerate methods after loading.", enumerateMethodsTime(assemblies).TotalMilliseconds);
        }

        private static void EnumerateMethods(IEnumerable<ICliAssembly> targets)
        {
            int totalManagedCount = 0;
            int totalUnmanagedCount = 0;
            foreach (var assembly in targets)
            {
                ICliMetadataMethodDefinitionTable mdt = assembly.MetadataRoot.TableStream.MethodDefinitionTable;
                if (mdt == null)
                    continue;
                foreach (var methodDef in mdt)
                {
                    var body = methodDef.Body;
                    if (body != null)
                    {
                        body.Header.ToString();
                        totalManagedCount++;
                    }
                    else
                    {
                        if (methodDef.RVA == 0)
                            methodDef.ToString();
                        totalUnmanagedCount++;
                    }
                }
            }
            Console.WriteLine("Total methods with bodies: {0}", totalManagedCount);
            Console.WriteLine("Total methods without bodies: {0}", totalUnmanagedCount);
        }

        private static void FlowControl(int i)
        {
            Console.WriteLine("{1} {2}");
            Console.WriteLine("{0} {1} {2}", 32.9, 32.2F, new Nullable<long>(0x3200000000L));
            if (i < 32)
            {
                for (int k = 2; k < 6; k++)
                    Console.WriteLine("Condition1");
            }
            else if (i > 40)
            {
                int o = 9;
                do
                {
                    Console.WriteLine("Condition2");
                } while (o++ < 33);
            }
            switch (i)
            {
                case 0:
                    Console.WriteLine("Switch Case 0");
                    break;
                case 21:
                    Console.WriteLine("Switch Case 21");
                    break;
            }
            Console.WriteLine("End");
        }

        private static void IntermediateForm()
        {
            var csProvider = LanguageVendors.Microsoft.GetCSharpLanguage().GetProvider(CSharpLanguageVersion.Version4);
            var csAssembly = csProvider.CreateAssembly("TestAssembly");
            var testNamespace = csAssembly.Namespaces.Add("AllenCopeland.Abstraction.Slf.SupplementaryProjects.TestIntermediate001");
            var testClass = testNamespace.Classes.Add("TestClassAttribute");
            testClass.BaseType = (IClassType)csProvider.IdentityManager.ObtainTypeReference(csProvider.IdentityManager.RuntimeEnvironment.GetCoreIdentifier(CliRuntimeCoreType.RootMetadatum));

            testClass.Metadata.Add(new MetadatumDefinitionParameterValueCollection(typeof(AttributeUsageAttribute).GetTypeReference((ICliManager)csAssembly.IdentityManager)));
            var dc = typeof(IIntermediateMember<,,>).GetTypeReference(csProvider.IdentityManager);
            foreach (var implInter in dc.ImplementedInterfaces)
                Console.WriteLine(implInter);
            Console.WriteLine();
            foreach (var implInter in dc.GetDirectlyImplementedInterfaces())
                Console.WriteLine(implInter);
            Console.WriteLine();
            /* *
             * ToDo: Ensure typing model properly maintains a nesting hierarchy.
             * */
            var testClassMethod = testClass.Methods.Add("Test", new TypedNameSeries() { { "test", csProvider.IdentityManager.ObtainTypeReference(RuntimeCoreType.Int32) } });
            testClassMethod.TypeParameters.Add("T1");
            var testClassMethodClass = testClassMethod.Classes.Add("TestMethodClass");
            testClassMethodClass.TypeParameters.Add("T2");
            var testMethod = testClassMethodClass.Methods.Add("TestMethod2", new TypedNameSeries() { { "test2", csProvider.IdentityManager.ObtainTypeReference(RuntimeCoreType.UInt64) } });
            testMethod.TypeParameters.Add("T3");
            var testMethodClass = testMethod.Classes.Add("TestMethod2Class");
            testMethodClass.TypeParameters.Add("T4");
            Console.WriteLine(testMethodClass.FullName);
            TimeTestWith((_ICliManager)csProvider.IdentityManager);
        }


        public class ExampleAttribute :
            Attribute
        {
            public ExampleAttribute(Type example) { }
        }
        private static void TimeTest()
        {
            var identityManager = (_ICliManager)CliGateway.CreateIdentityManager(CliGateway.CurrentPlatform, CliGateway.CurrentVersion, true, true, true);
            TimeTestWith(identityManager);
        }

        private static void TimeTestWith(_ICliManager identityManager)
        {
            var mscorValidate = MiscHelperMethods.CreateActionOfTime<_ICliManager>(GetAndValidateMSCorLib);
            var validateTime1 = mscorValidate(identityManager);
            var validateTime2 = mscorValidate(identityManager);
            Console.WriteLine("Primary validation took: {0}ms.", validateTime1.TotalMilliseconds);
            Console.WriteLine("Secondary validation took: {0}ms.", validateTime2.TotalMilliseconds);
            identityManager.Dispose();
        }

        private static void GetAndValidateMSCorLib(_ICliManager identityManager)
        {
            ICliAssembly assembly = (ICliAssembly)identityManager.ObtainAssemblyReference(typeof(int).Assembly);
            //Console.WriteLine(assembly.MetadataRoot.TableStream.TypeDefinitionTable.Count);
            var warnErrors = CliMetadataValidator.ValidateMetadata(assembly, assembly.MetadataRoot, identityManager);
            if (warnErrors.HasErrors)
                Console.WriteLine(warnErrors.Count);
        }

    }
}
