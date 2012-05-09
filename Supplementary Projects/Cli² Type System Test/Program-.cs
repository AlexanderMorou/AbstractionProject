using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Globalization;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System.Diagnostics;
using System.IO;
using AllenCopeland.Abstraction.Utilities;

namespace AllenCopeland.Abstraction.Slf.SupplementaryProjects.CliTest
{
    internal class Program
    {
        static void Main()
        {
            Test();
            Console.ReadKey(true);
        }

        private static void Test()
        {
            var testResult = MiscHelperMethods.TimeResult(DoTest);
            ICliManager clim = testResult.Item2;
            Console.WriteLine(testResult.Item1.TotalMilliseconds);
            Console.ReadKey(true);
            clim.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private static ICliManager DoTest()
        {
            ICliManager clim = CliGateway.CreateIdentityManager(FrameworkPlatform.x86Platform, FrameworkVersion.v4_0_30319, true, true, true, typeof(Program).Assembly.Location);
            foreach (var t in (from f in ObtainFrameworkAssemblies(clim.RuntimeEnvironment)
                               let assem = clim.ObtainAssemblyReference(f)
                               from t in assem.MetadataRoot.TableStream.Values
                               select t))
                t.Read();
            return clim;
        }


        private static IEnumerable<string> ObtainFrameworkAssemblies(ICliRuntimeEnvironmentInfo environment)
        {
            return from runtimeDirectory in environment.ResolutionPaths
                   from file in runtimeDirectory.GetFiles()
                   let extension = Path.GetExtension(file.FullName).ToLower()
                   where extension == ".exe" || extension == ".dll"
                   where CliGateway.IsFullAssembly(file.FullName)
                   orderby file.Length descending
                   select file.FullName;
        }
    }
}
