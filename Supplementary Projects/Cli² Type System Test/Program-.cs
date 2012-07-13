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
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Security;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using System.Data;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using System.Reflection.Emit;

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
        public class TA : Attribute {
            public TA(object o) { }
        }
        [TA(new[]{"test", "test2"})]
        private abstract class Voe : CliAssembly.test3.test4{ }
        private static ICliManager DoTest()
        {
            string assemblyLocation = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            _ICliManager clim = (_ICliManager) CliGateway.CreateIdentityManager(CliGateway.CurrentPlatform, CliGateway.CurrentVersion, true, true, true, assemblyLocation);
            var assemblies = (from f in (ObtainFrameworkAssemblies(clim.RuntimeEnvironment).Concat(new string[] { }))
                              let assembly = AttemptGetAssembly(clim, f)
                              where assembly != null
                              select assembly).ToList();

            foreach (var t in from assembly in assemblies
                              from t in assembly.MetadataRoot.TableStream.Values
                              select t)
                t.Read();
            var mSet = (from a in assemblies
                        where a.MetadataRoot.TableStream.StandAloneSigTable != null
                        from sSig in a.MetadataRoot.TableStream.StandAloneSigTable
                        where sSig.Signature is ICliMetadataFieldSignature
                        let len = sSig.SignatureSize
                        let fSig = ((ICliMetadataFieldSignature) (sSig.Signature))
                        let data = a.MetadataRoot.BlobHeap[sSig.SignatureIndex]
                        //where fSig.IsPinned
                        orderby len descending
                        select new { FieldSignature = fSig.Type, SignatureSize = len, Assembly = a, StandaloneSignature = sSig, Data = data }).ToArray();
            //foreach (var s in mSet)
            //    Console.WriteLine("{0}, {1}, {2}", s.Assembly.Name, s.SignatureSize, s.StandaloneSignature.Index, s.FieldSignature);
            var curDir = Directory.GetCurrentDirectory().ToLower();
            var delea = (from mS in mSet
                         let localeFolder = Path.GetDirectoryName(mS.StandaloneSignature.MetadataRoot.SourceImage.Filename)
                         where !clim.RuntimeEnvironment.ResolutionPaths.Any(dirInfo => localeFolder.Contains(dirInfo.FullName.ToLower())) ||
                               localeFolder == assemblyLocation.ToLower() || localeFolder == curDir
                         where mS.Assembly.Name != "ModOptReqOutput"
                         orderby mS.Assembly.Name,
                                 mS.SignatureSize descending
                         select new { mS.FieldSignature, Size = mS.SignatureSize, Data = mS.Data, Assembly = mS.Assembly }).ToArray();
            //Console.WriteLine("{0} fields in the Sigtables", mSet.Length);

            
            Console.WriteLine("{0} fields in the Sigtables", delea.Length);
            Console.WriteLine();

            //var u = typeof(DataSet).Assembly.ManifestModule.ResolveSignature(0x0045 | 0x11 << 24);
            //var tParamIdentifiers = (from assembly in assemblies
            //                         from ICliModule module in assembly.Modules.Values
            //                         from type in module.Metadata.MetadataRoot.TableStream.TypeDefinitionTable
            //                         where type.TypeParameters != null && type.TypeParameters.Count > 0
            //                         //where (((type.TypeAttributes & TypeAttributes.VisibilityMask)!= TypeAttributes.Public && ((type.TypeAttributes & TypeAttributes.VisibilityMask) != TypeAttributes.NotPublic))) && type.NamespaceIndex != 0
            //                         let identifier = CliCommon.GetUniqueIdentifier(type, assembly, clim)
            //                         where identifier is IGeneralGenericTypeUniqueIdentifier
            //                         let gIdentifier = (IGeneralGenericTypeUniqueIdentifier) identifier
            //                         where gIdentifier.TypeParameters > 0
            //                         orderby gIdentifier.ToString()
            //                         select gIdentifier).ToArray();
            //foreach (var identifier in tParamIdentifiers)
            //    Console.WriteLine(identifier);
            return clim;
        }

        private static _ICliAssembly AttemptGetAssembly(_ICliManager clim, string f)
        {
            try
            {
                return (_ICliAssembly) clim.ObtainAssemblyReference(f);
            }
            catch (BadImageFormatException)
            {
                return null;
            }
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
