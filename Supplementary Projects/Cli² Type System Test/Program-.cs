using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Globalization;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System.Diagnostics;

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
            ICliManager clim = CliGateway.CreateIdentityManager(FrameworkPlatform.x86Platform, FrameworkVersion.v4_0_30319, true, true, true);
            var mscorlib = clim.ObtainAssemblyReference(clim.RuntimeEnvironment.CoreLibraryIdentifier);
            var system = clim.ObtainAssemblyReference(AstIdentifier.GetAssemblyIdentifier("System", mscorlib.AssemblyInformation.AssemblyVersion, mscorlib.AssemblyInformation.Culture, mscorlib.PublicKeyInfo.PublicToken.Token));
            var winForms = clim.ObtainAssemblyReference(AstIdentifier.GetAssemblyIdentifier("System.Windows.Forms", mscorlib.AssemblyInformation.AssemblyVersion, mscorlib.AssemblyInformation.Culture, mscorlib.PublicKeyInfo.PublicToken.Token));
            var cli = clim.ObtainAssemblyReference(AstIdentifier.GetAssemblyIdentifier("_abs.slf.typesystems.cli", AstIdentifier.GetVersion(1, 0), CultureIdentifiers.None, typeof(ICliManager).Assembly.GetName().GetPublicKeyToken()));
            const string cliManagerTypeNamespace = "AllenCopeland.Abstraction.Slf._Internal.Cli";
            const string cliManagerTypeName = "CliManager";
            var cliManagerType = cli.FindType(cliManagerTypeNamespace, cliManagerTypeName);

            var internalType = cliManagerType.NestedClasses[0];
            Stack<ICliMetadataTypeDefinitionTableRow> inheritanceChain = new Stack<ICliMetadataTypeDefinitionTableRow>();
            var current = internalType;
            while (current != null)
            {
                Console.WriteLine(current);
                inheritanceChain.Push(current);
                current = clim.ResolveScope(current.Extends);
            }
            Console.WriteLine(inheritanceChain);
            foreach (var @ref in (from @ref in system.References.Values.Concat(winForms.References.Values)
                                  select @ref).Concat(new[] { winForms }).Distinct())
            {
                Stopwatch sw = Stopwatch.StartNew();
                ICliMetadataTypeDefinitionTable typeTable;
                foreach (var table in @ref.MetadataRoot.TableStream.Values)
                    table.Read();
                if ((typeTable = @ref.MetadataRoot.TableStream.TypeDefinitionTable) != null)
                {
                    var first = typeTable.Skip(1).FirstOrDefault();
                    var fMethod = first.Methods.FirstOrDefault();
                    if (fMethod != null)
                        Console.WriteLine(fMethod.Body);
                    var firstTypeRef = clim.ObtainTypeReference(first);
                    Console.WriteLine(firstTypeRef.FullName);
                }
                sw.Stop();
                Console.WriteLine("{1}\n\t{0}\n\t{2}ms\n", @ref.MetadataRoot.SourceImage.Filename, @ref, sw.Elapsed.TotalMilliseconds);
            }
            Console.ReadKey(true);
            clim.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
