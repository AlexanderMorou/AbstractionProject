using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using AllenCopeland.Abstraction.Globalization;
using AllenCopeland.Abstraction.IO;
using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli.Modules;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Platforms.DOS;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;
using AllenCopeland.Abstraction.Utilities;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.SupplementaryProjects.CliTest
{
    static class Program
    {
        private class TestDict :
            Dictionary<int, string> {
            private class Test0 { }
            private class TestA {
                private interface ITest4 { }
            }
            private class TestC { private interface ITest9 { } }
            private class Test34 { }
        }

        private static void Main()
        {
            FrameworkPlatform platform;
#if x86
            platform = FrameworkPlatform.x86Platform;
#elif x64
            platform = FrameworkPlatform.x64Platform;
#endif

            ICliRuntimeEnvironmentInfo runtimeEnvironment = CliGateway.GetRuntimeEnvironmentInfo(platform, useCoreLibrary: false);
            //TestExtractModuleInfo(runtimeEnvironment);
            //BCLTest(runtimeEnvironment);
            //RunTestOn(runtimeEnvironment, @"C:\Users\Allen Copeland\Desktop\Research\Stringer.exe");
            //RunTestOn(runtimeEnvironment, @"C:\windows\microsoft.net\framework\v4.0.30319\System.EnterpriseServices.dll");
            //RunTestOn(runtimeEnvironment, @"C:\Users\Allen Copeland\AppData\Local\Temporary Projects\ExplicitInterfaceStateMachine\bin\Debug\ExplicitInterfaceStateMachine.exe");
            //RunTestOn(runtimeEnvironment, typeof(TestDict).Assembly.Location);
            RunTestOn(runtimeEnvironment, ObtainFrameworkAssemblies(runtimeEnvironment).ToArray());
            //TestModOptReq(runtimeEnvironment);

        }

        private static void BCLTest(ICliRuntimeEnvironmentInfo runtimeEnvironment)
        {
#if x64
            RunTestOn(runtimeEnvironment, @"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\mscorlib.dll");
#elif x86
#if MONO
            RunTestOn(runtimeEnvironment, @"C:\Program Files (x86)\Mono-2.10.8\lib\mono\4.0\mscorlib.dll");
#else
            RunTestOn(runtimeEnvironment, @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll");
#endif
#endif
        }

        private static void TestUsingBCL(ICliRuntimeEnvironmentInfo runtimeEnvironment)
        {
            RunTestOn(runtimeEnvironment, ObtainFrameworkAssemblies(runtimeEnvironment));
        }

        private static void RunTestOn(ICliRuntimeEnvironmentInfo runtimeEnvironment, string filename)
        {
            RunTestOn(runtimeEnvironment, new[] { filename });
        }

        private static void RunTestOn(ICliRuntimeEnvironmentInfo runtimeEnvironment, IEnumerable<string> filenames)
        {
            Console.WriteLine("To begin test, press any key. . . ");
            Console.ReadKey(true);

            var timedScanFrom = TimeAction<ICliRuntimeEnvironmentInfo, IEnumerable<string>>(ScanFrameworkAssemblies, runtimeEnvironment, filenames);
            Console.ReadKey(true);
#if THREETIME
            var timedScanFrom2 = TimeAction<ICliRuntimeEnvironmentInfo, IEnumerable<string>>(ScanFrameworkAssemblies, runtimeEnvironment, filenames);
            Console.ReadKey(true);

            var timedScanFrom3 = TimeAction<ICliRuntimeEnvironmentInfo, IEnumerable<string>>(ScanFrameworkAssemblies, runtimeEnvironment, filenames);
            Console.ReadKey(true);
#endif

            var timedScanFrom4 = MiscHelperMethods.TimeAction<IEnumerable<string>>(ScanFrameworkAssemblies, filenames);
            Console.ReadKey(true);

            Console.WriteLine("Initial test took {0}ms", timedScanFrom.TotalMilliseconds);
#if THREETIME
            Console.WriteLine("Second test took {0}ms", timedScanFrom2.TotalMilliseconds);
            Console.WriteLine("Third test took {0}ms", timedScanFrom3.TotalMilliseconds);
#endif
            Console.WriteLine("Initial native test took {0}ms", timedScanFrom4.TotalMilliseconds);
        }

        private static TimeSpan TimeAction<T1, T2>(Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            Stopwatch sw = Stopwatch.StartNew();
            action(arg1, arg2);
            sw.Stop();
            return sw.Elapsed;
        }

        private static void ScanFrameworkAssemblies(IEnumerable<string> frameworkFilenames)
        {
            IList<Assembly> assemblies = new List<Assembly>();
            foreach (var filename in frameworkFilenames)
            {
                try
                {
                    assemblies.Add(Assembly.LoadFile(filename));
                }
                catch (BadImageFormatException)
                {
                }
            }
            Stopwatch sw = Stopwatch.StartNew();
            var fieldsQuery = (from a in assemblies
                               from m in a.GetModules()
                               from t in TryGetTypes(m)
                               where (t.Attributes & TypeAttributes.VisibilityMask) == TypeAttributes.NotPublic || (t.Attributes & TypeAttributes.VisibilityMask) == TypeAttributes.Public
                               let q = t.BaseType
                               let fieldQuery = from f in t.GetFields()
                                                select f.FieldType
                               let methodQuery = from meth in t.GetMethods()
                                                 let paramQuery = TryGetParameters(meth)
                                                 select new { Method = meth, ParameterTypes = paramQuery.ToArray() }
                               let propertyQuery = from prop in t.GetProperties()
                                                   let paramQuery = from p in prop.GetIndexParameters()
                                                                    select p.ParameterType
                                                   select new { Prop = prop, ParameterTypes = paramQuery.ToArray() }
                               select new { Type = t, Fields = fieldQuery.ToArray(), Methods = methodQuery.ToArray(), Properties = propertyQuery.ToArray() }).ToArray();
            sw.Stop();
            Console.WriteLine("All field, method, and property retrieval took {0}ms", sw.Elapsed.TotalMilliseconds);
            assemblies.ToString();
        }

        private static Type[] TryGetTypes(Module m)
        {
            try
            {
                return m.GetTypes();
            }
            catch (TypeLoadException e)
            {
                Console.WriteLine("{0} - {1}", e.Message, m.FullyQualifiedName);
                return new Type[0];
            }
            catch (ReflectionTypeLoadException e)
            {
                Console.WriteLine("{0} - {1}", e.Message, m.FullyQualifiedName);
                return new Type[0];
            }
        }

        private static IEnumerable<Type> TryGetParameters(MethodInfo meth)
        {
            try
            {
                return from p in meth.GetParameters()
                       select p.ParameterType;
            }
            catch (TypeLoadException e)
            {
                Console.WriteLine("{0} - {1}::{2}", e.Message, meth.DeclaringType.GetGenericTypeDefinition().FullName, meth.Name);
                return new Type[0];
            }
        }

        private static void ScanFrameworkAssemblies(ICliRuntimeEnvironmentInfo environmentInfo, IEnumerable<string> frameworkFilenames)
        {
            Stopwatch timer = Stopwatch.StartNew();
            ICliManager identityManager = CliGateway.CreateIdentityManager(environmentInfo);
            IList<ICliAssembly> assemblies = new List<ICliAssembly>();
            ScanFrameworkAssemblies(frameworkFilenames, identityManager, assemblies);
            timer.Stop();
            foreach (var assembly in assemblies)
                foreach (var reference in assembly.References.Values.ToArray())
                    ;
            Console.Write("To load base metadata took {0}ms, ", timer.Elapsed.TotalMilliseconds);
            var fieldMethodTime = MiscHelperMethods.TimeAction(GetFieldsAndMethods, assemblies);
            Console.Write("to read all the field and method signatures took {0}ms, ", fieldMethodTime.TotalMilliseconds);

            //Console.ReadKey(true);
            assemblies.Clear();
            var disposeTime = MiscHelperMethods.TimeAction(DisposeIdentityManager, identityManager);
            Console.WriteLine("disposal took {0}ms.", disposeTime.TotalMilliseconds);
        }

        private static void GetFieldsAndMethods(IList<ICliAssembly> assemblies)
        {
            //Stopwatch sw = new Stopwatch();
            //int typeReferenceCount = 0;
            foreach (var assembly in assemblies)
            {
                foreach (ICliModule module in assembly.Modules.Values)
                {
                    if (module.Metadata.MetadataRoot.TableStream.FieldTable != null)
                        module.Metadata.MetadataRoot.TableStream.FieldTable.Read();
                    if (module.Metadata.MetadataRoot.TableStream.MethodDefinitionTable != null)
                        module.Metadata.MetadataRoot.TableStream.MethodDefinitionTable.Read();
                    if (module.Metadata.MetadataRoot.TableStream.TypeDefinitionTable != null)
                        module.Metadata.MetadataRoot.TableStream.TypeDefinitionTable.Read();
                    if (module.Metadata.MetadataRoot.TableStream.PropertyMapTable != null)
                        module.Metadata.MetadataRoot.TableStream.PropertyMapTable.Read();
                    if (module.Metadata.MetadataRoot.TableStream.PropertyTable != null)
                        module.Metadata.MetadataRoot.TableStream.PropertyTable.Read();
                    if (module.Metadata.MetadataRoot.TableStream.TypeSpecificationTable != null)
                        module.Metadata.MetadataRoot.TableStream.TypeSpecificationTable.Read();
                    if (module.Metadata.MetadataRoot.TableStream.StandAloneSigTable != null)
                        module.Metadata.MetadataRoot.TableStream.StandAloneSigTable.Read();
                    if (module.Metadata.MetadataRoot.TableStream.TypeRefTable != null)
                    {
                        //sw.Start();
                        //var typeReferences = (from m in assembly.MetadataRoot.TableStream.TypeRefTable
                        //                      where m.ResolutionScope == CliMetadataResolutionScopeTag.AssemblyReference
                        //                      let typeRefSource = (ICliMetadataAssemblyRefTableRow) m.Source
                        //                      where typeRefSource != null
                        //                      let typeRefAssembly = TryGetReference(assembly, typeRefSource)
                        //                      where typeRefAssembly != null
                        //                      select typeRefAssembly.FindType(m.Namespace, m.Name)).ToArray();
                        //typeReferenceCount += typeReferences.Length;
                        //sw.Stop();
                    }
                }
            }
            var junk = (from assembly in assemblies
                        from ICliModule mod in assembly.Modules.Values
                        let typeTable = mod.Metadata.MetadataRoot.TableStream.TypeDefinitionTable
                        let typeQuery = from t in typeTable
                                        let q = t.Extends
                                        let fieldQuery = from f in t.Fields
                                                         let fs = f.FieldType
                                                         select f
                                        let methodQuery = from m in t.Methods
                                                          select m.Signature
                                        let propertyQuery = GetProperties(t, GetPropertyMap(t, assembly.MetadataRoot), assembly.MetadataRoot)
                                        //orderby mod.Name ascending,
                                        //        t.Name ascending
                                        select new { t = assembly.IdentityManager.ObtainTypeReference(t), fq = fieldQuery.ToArray(), mq = methodQuery.ToArray(), pq = propertyQuery == null ? null : propertyQuery.ToArray() }                                
                        group new { a = assembly, Mod = mod, types = typeQuery.ToArray() } by assembly).ToArray();
            var junk3 = (from at in junk
                         from mod in at
                         from t in mod.types
                         select 1 + t.fq.Length + t.mq.Length + (t.pq == null ? 0 : t.pq.Length) + 1).Sum();
            var junk2 = (from assembly in assemblies
                         let sast = assembly.MetadataRoot.TableStream.StandAloneSigTable
                         where sast != null
                         from sas in sast
                         select sas.Signature).Count();
            //, firstref lookup: {1}ms for {2} types
            Console.Write("(member count: {0}) ", junk3/*, sw.ElapsedMilliseconds, typeReferenceCount*/);
        }

        private static ICliAssembly TryGetReference(ICliAssembly assembly, ICliMetadataAssemblyRefTableRow typeRefSource)
        {
            try
            {
                return assembly.IdentityManager.ObtainAssemblyReference(typeRefSource);
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        private static ICliMetadataPropertyMapTableRow GetPropertyMap(ICliMetadataTypeDefinitionTableRow typeRow, CliMetadataRoot root)
        {
            if (root.TableStream.PropertyMapTable == null)
                return null;
            foreach (var map in root.TableStream.PropertyMapTable)
                if (map.ParentIndex == typeRow.Index)
                    return map;
            return null;
        }

        private static IEnumerable<ICliMetadataPropertyTableRow> GetProperties(ICliMetadataTypeDefinitionTableRow typeRow, ICliMetadataPropertyMapTableRow propertyMap, CliMetadataRoot root)
        {
            if (propertyMap == null)
                yield break;
            int currentItemIndex = root.TableStream.PropertyMapTable.IndexOf(propertyMap);
            int lastItemIndex = 0;
            ICliMetadataPropertyMapTableRow nextItem = root.TableStream.PropertyMapTable[currentItemIndex + 1];
            if (nextItem == null)
                lastItemIndex = root.TableStream.PropertyTable.Count;
            else
                lastItemIndex = (int) nextItem.PropertyListIndex;
            for (int i = (int) propertyMap.PropertyListIndex; i < lastItemIndex; i++)
            {
                var unnecessary = root.TableStream.PropertyTable[i].PropertyType;
                yield return root.TableStream.PropertyTable[i];
            }
        }

        private static ICliMetadataMethodDefSignature TryGetSignature(ICliMetadataMethodDefinitionTableRow method)
        {
            try
            {
                return (ICliMetadataMethodDefSignature) method.Signature;
            }
            catch (NotImplementedException) { }
            return null;
        }

        private static void DisposeIdentityManager(ICliManager manager)
        {
            manager.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private static ICliMetadataFieldSignature TryGetSignature(ICliMetadataFieldTableRow field)
        {
            try
            {
                return field.FieldType;
            }
            catch (NotImplementedException) { }
            return null;
        }

        private static ICliAssembly TryGetAssembly(ICliManager identityManager, string s)
        {
            try
            {
                return identityManager.ObtainAssemblyReference(s);
            }
            catch (BadImageFormatException) { }
            return null;
        }

        private static void ScanFrameworkAssemblies(IEnumerable<string> frameworkFilenames, ICliManager identityManager, IList<ICliAssembly> assemblies)
        {
            foreach (var filename in frameworkFilenames)
                try
                {
                    assemblies.Add(identityManager.ObtainAssemblyReference(filename));
                }
                catch (BadImageFormatException) { }
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
