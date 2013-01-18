using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Languages.Cil;
using AllenCopeland.Abstraction.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.SupplementaryProjects.TestCli
{
    internal class Program
    {
        public static void Main()
        {
            TimeTest();
            TimeTest();
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
            var warnErrors = CliMetadataValidator.ValidateMetadata(assembly, assembly.MetadataRoot, identityManager);
            if (warnErrors.HasErrors)
                Console.WriteLine(warnErrors.Count);
        }

    }
}
