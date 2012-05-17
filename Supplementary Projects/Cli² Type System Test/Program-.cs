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
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Modules;
using System.Reflection;

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
            _ICliManager clim = (_ICliManager) CliGateway.CreateIdentityManager(FrameworkPlatform.x86Platform, FrameworkVersion.v4_0_30319, true, true, true, typeof(Program).Assembly.Location);
            var assemblies = (from f in ObtainFrameworkAssemblies(clim.RuntimeEnvironment)
                              let assembly = (_ICliAssembly)clim.ObtainAssemblyReference(f)
                              select assembly).ToList();

            foreach (var t in (from assembly in assemblies
                               from t in assembly.MetadataRoot.TableStream.Values
                               select t))
                t.Read();
            var tParamIdentifiers = (from assembly in assemblies
                                     from ICliModule module in assembly.Modules.Values
                                     from type in module.Metadata.MetadataRoot.TableStream.TypeDefinitionTable
                                     where (((type.TypeAttributes & TypeAttributes.VisibilityMask)!= TypeAttributes.Public && ((type.TypeAttributes & TypeAttributes.VisibilityMask) != TypeAttributes.NotPublic))) && type.NamespaceIndex != 0
                                     let identifier = CliCommon.GetUniqueIdentifier(type, assembly, clim)
                                     //where identifier is IGeneralGenericTypeUniqueIdentifier
                                     //let gIdentifier = (IGeneralGenericTypeUniqueIdentifier) identifier
                                     //where gIdentifier.TypeParameters > 0
                                     select identifier).ToArray();
            foreach (var identifier in tParamIdentifiers)
                Console.WriteLine(identifier);
            
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
