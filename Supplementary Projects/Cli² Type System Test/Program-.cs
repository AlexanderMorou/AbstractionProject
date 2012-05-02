using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Globalization;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf.SupplementaryProjects.CliTest
{
    internal class Program
    {
        static void Main()
        {
            ICliManager clim = CliGateway.CreateIdentityManager(FrameworkPlatform.x86Platform, FrameworkVersion.v1_1_4322, true, true, true);
            var mscorlib = clim.ObtainAssemblyReference(clim.RuntimeEnvironment.CoreLibraryIdentifier);
            var system = clim.ObtainAssemblyReference(AstIdentifier.GetAssemblyIdentifier("System", mscorlib.AssemblyInformation.AssemblyVersion, mscorlib.AssemblyInformation.Culture, mscorlib.PublicKeyInfo.PublicToken.Token));
            var winForms = clim.ObtainAssemblyReference(AstIdentifier.GetAssemblyIdentifier("System.Windows.Forms", mscorlib.AssemblyInformation.AssemblyVersion, mscorlib.AssemblyInformation.Culture, mscorlib.PublicKeyInfo.PublicToken.Token));
            //var cli = clim.ObtainAssemblyReference(AstIdentifier.GetAssemblyIdentifier("_abs.slf.typesystems.cli", AstIdentifier.GetVersion(1,0), CultureIdentifiers.None, typeof(ICliManager).Assembly.GetName().GetPublicKeyToken()));
            //const string cliManagerTypeNamespace = "AllenCopeland.Abstraction.Slf._Internal.Cli";
            //const string cliManagerTypeName = "CliManager";
            //var cliManagerType = cli.FindType(cliManagerTypeNamespace, cliManagerTypeName);

            //var internalType = cliManagerType.NestedClasses[1];
            //Stack<ICliMetadataTypeDefinitionTableRow> inheritanceChain = new Stack<ICliMetadataTypeDefinitionTableRow>();
            //var current = internalType;
            //while (current != null)
            //{
            //    inheritanceChain.Push(current);
            //    current = clim.ResolveScope(current.Extends);
            //}
            //Console.WriteLine(inheritanceChain);
            foreach (var @ref in (from @ref in system.References.Values.Concat(winForms.References.Values)
                                  orderby @ref.Name
                                  select @ref).Concat(new[] { winForms }).Distinct())
            {
                Console.WriteLine("{1}\n\t{0}\n", @ref.MetadataRoot.SourceImage.Filename, @ref);
            }
            Console.ReadKey(true);
            clim.Dispose();
        }
    }
}
