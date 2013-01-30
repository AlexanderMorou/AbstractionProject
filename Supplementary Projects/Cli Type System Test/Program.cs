using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Languages.Cil;
using AllenCopeland.Abstraction.Slf.Languages.CSharp;
using AllenCopeland.Abstraction.Utilities;
using System;
using System.Collections.Generic;
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
            var csProvider = LanguageVendors.Microsoft.GetCSharpLanguage().GetProvider(CSharpLanguageVersion.Version2);
            var csAssembly = csProvider.CreateAssembly("TestAssembly");
            var testClass = csAssembly.Classes.Add("TestClassAttribute");

            testClass.BaseType = (IClassType)csProvider.IdentityManager.ObtainTypeReference(csProvider.IdentityManager.RuntimeEnvironment.GetCoreIdentifier(CliRuntimeCoreType.RootMetadatum));

            //testClass.Metadata.Add(new MetadatumDefinitionParameterValueCollection(typeof(AttributeUsageAttribute).GetTypeReference((ICliManager)csAssembly.IdentityManager)));
            var dc = typeof(Dictionary<string, Dictionary<int, long>>).GetTypeReference(csProvider.IdentityManager);
            foreach (var implInter in dc.ImplementedInterfaces)
                Console.WriteLine(implInter);
            Console.WriteLine();
            foreach (var implInter in dc.GetDirectImplementedInterfaces())
                Console.WriteLine(implInter);
            IType d = testClass;
            foreach (var metadata in d.Metadata)
                Console.WriteLine(metadata);
        }
        public class ExampleAttribute :
            Attribute
        {
            public ExampleAttribute(Type example) { }
        }
        private static void TimeTest()
        {
            var identityManager = (_ICliManager)CliGateway.CreateIdentityManager(CliGateway.CurrentPlatform, CliGateway.CurrentVersion, true, true, true);
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
