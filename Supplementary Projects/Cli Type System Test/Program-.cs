using AllenCopeland.Abstraction.Globalization;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli.Modules;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Languages.CSharp;
using AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions;
using AllenCopeland.Abstraction.Utilities;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Arrays;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Languages.Cil;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata;

namespace AllenCopeland.Abstraction.Slf.SupplementaryProjects.TestCli
{
    internal class Program
    {
        static void Main()
        {
            //IntermediateTestMain();
            Test();
            //Console.ReadKey(true);
        }

        private static void IntermediateTestMain()
        {
            var timedIntermediateTest = MiscHelperMethods.CreateActionOfTime(IntermediateTest);
            var first = timedIntermediateTest();
            var second = timedIntermediateTest();
            Console.WriteLine(
@"PreJit : {0}
PostJit: {1}", first, second);
        }

        private static void IntermediateTest()
        {
            var provider = LanguageVendors.Microsoft.GetCSharpLanguage().GetProvider(CSharpLanguageVersion.Version2);

            var csAssembly = provider.CreateAssembly("TestAssembly");
            var csAssemblyRef2 = provider.IdentityManager.ObtainAssemblyReference(AstIdentifier.GetAssemblyIdentifier("TestAssembly", new IntermediateVersion(1, 0) { AutoIncrementBuild = true, AutoIncrementRevision = true }, CultureIdentifiers.None));
            var coreLib = (ICliAssembly)provider.IdentityManager.ObtainAssemblyReference(provider.IdentityManager.RuntimeEnvironment.CoreLibraryIdentifier);
            var coreLibTableStream = coreLib.MetadataRoot.TableStream;
            /*
            Parallel.For(0, 10000, i =>
                TestLinq(csAssembly, baseIndex: i + 1));
            //*/
            ///*
            for (int i = 0; i < 40; i++)
                TestLinq(csAssembly.Parts.Add(), baseIndex: i + 1);
            var thirdPart = csAssembly.Parts[3];
            var thirdMethod = thirdPart.Namespaces["LinqExample"].Methods[3];
            thirdPart.Dispose();
            //*/
        }
        private static byte[] ecmaKey = new byte[] { 0xb7, 0x7a, 0x5c, 0x56, 0x19, 0x34, 0xe0, 0x89 };
#if DEBUG
        private static byte[] abstractionKey = null;
#else
        private static byte[] abstractionKey = new byte[] { 0x9f, 0x6e, 0xc7, 0xb8, 0xea, 0x17, 0x8, 0x9 };
#endif
        private static byte[] microsoftKey = new byte[] { 0x31, 0xbf, 0x38, 0x56, 0xad, 0x36, 0x4e, 0x35 };
        private static byte[] microsoftAltKey = new byte[] { 0xb0, 0x3f, 0x5f, 0x7f, 0x11, 0xd5, 0xa, 0x3a };
        private static byte[] xnaKey = new byte[] { 0x84, 0x2c, 0xf8, 0xbe, 0x1d, 0xe5, 0x05, 0x53 };
        private static IVersion v10 = AstIdentifier.GetVersion(1, 0, 0, 0);
        private static IVersion v20 = AstIdentifier.GetVersion(2, 0, 0, 0);
        private static IVersion v40 = AstIdentifier.GetVersion(4, 0, 0, 0);
        private static IVersion v80 = AstIdentifier.GetVersion(8, 0, 0, 0);
        private static IVersion v100 = AstIdentifier.GetVersion(10, 0, 0, 0);
        private static ICultureIdentifier cultureCommon = CultureIdentifiers.None;
        private static IAssemblyUniqueIdentifier[] identifiers = new IAssemblyUniqueIdentifier[] {
                AstIdentifier.GetAssemblyIdentifier("_abs.slf.compilerservices", v10, cultureCommon, abstractionKey),
                AstIdentifier.GetAssemblyIdentifier("_abs.slf.metadata", v10, cultureCommon, abstractionKey),
                AstIdentifier.GetAssemblyIdentifier("_abs.slf.typesystems.abstract", v10, cultureCommon, abstractionKey),
                AstIdentifier.GetAssemblyIdentifier("_abs.slf.typesystems.cli", v20, cultureCommon, abstractionKey),
                AstIdentifier.GetAssemblyIdentifier("_abs.supplementary.cts.test", v10, cultureCommon, abstractionKey),
                AstIdentifier.GetAssemblyIdentifier("_abs.util.common", v10, cultureCommon, abstractionKey),
                AstIdentifier.GetAssemblyIdentifier("Accessibility", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("AddInProcess", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("AddInProcess32", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("AddInUtil", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("aspnet_compiler", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("aspnet_regbrowsers", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("aspnet_regsql", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("AspNetMMCExt", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("caspol", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("ComSvcConfig", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("CustomMarshalers", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("DataSvcUtil", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("dfsvc", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("EdmGen", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("InstallUtil", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("ISymWrapper", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("jsc", v100, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Activities.Build", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Build.Conversion.v4.0", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Build", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Build.Engine", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Build.Framework", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Build.Tasks.v4.0", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Build.Utilities.v4.0", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.CSharp", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Data.Entity.Build.Tasks", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Internal.Tasks.Dataflow", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.JScript", v100, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Transactions.Bridge", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Transactions.Bridge.Dtc", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.VisualBasic.Activities.Compiler", v100, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.VisualBasic.Compatibility.Data", v100, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.VisualBasic.Compatibility", v100, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.VisualBasic", v100, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.VisualBasic.Vsa", v80, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.VisualC", v100, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.VisualC.STLCLR", v20, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Vsa", v80, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Windows.ApplicationServer.Applications", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Workflow.Compiler", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft_VsaVb", v80, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("MSBuild", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("mscorlib", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("RegAsm", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("RegSvcs", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("SMDiagnostics", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("SMSvcHost", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("sysglobl", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Activities.Core.Presentation", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Activities", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Activities.DurableInstancing", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Activities.Presentation", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.AddIn.Contract", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.AddIn", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Collections.Concurrent", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Collections", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.ComponentModel.Annotations", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.ComponentModel.Composition", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.ComponentModel.Composition.Registration", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.ComponentModel.DataAnnotations", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.ComponentModel", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.ComponentModel.EventBasedAsync", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Configuration", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Configuration.Install", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Core", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Data.DataSetExtensions", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Data", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Data.Entity.Design", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Data.Entity", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Data.Linq", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Data.OracleClient", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Data.Services.Client", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Data.Services.Design", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Data.Services", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Data.SqlXml", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Deployment", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Design", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Device", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Diagnostics.Contracts", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Diagnostics.Debug", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Diagnostics.Tools", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Diagnostics.Tracing", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.DirectoryServices.AccountManagement", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.DirectoryServices", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.DirectoryServices.Protocols", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Drawing.Design", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Drawing", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Dynamic", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Dynamic.Runtime", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.EnterpriseServices", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Globalization", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.IdentityModel", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.IdentityModel.Selectors", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.IdentityModel.Services", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.IO.Compression", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.IO.Compression.FileSystem", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.IO", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.IO.Log", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Linq", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Linq.Expressions", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Linq.Parallel", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Linq.Queryable", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Management", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Management.Instrumentation", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Messaging", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Net", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Net.Http", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Net.Http.Rtc", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Net.Http.WebRequest", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Net.NetworkInformation", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Net.Primitives", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Net.Requests", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Numerics", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.ObjectModel", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Reflection.Context", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Reflection", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Reflection.Emit", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Reflection.Emit.ILGeneration", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Reflection.Emit.Lightweight", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Reflection.Extensions", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Reflection.Primitives", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Resources.ResourceManager", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Runtime.Caching", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Runtime", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Runtime.DurableInstancing", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Runtime.Extensions", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Runtime.InteropServices", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Runtime.InteropServices.WindowsRuntime", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Runtime.Numerics", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Runtime.Remoting", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Runtime.Serialization", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Runtime.Serialization.Formatters.Soap", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Runtime.Serialization.Json", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Runtime.Serialization.Primitives", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Runtime.Serialization.Xml", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Security", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Security.Principal", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.Activation", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.Activities", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.Channels", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.Discovery", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.Duplex", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.Http", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.Internals", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.NetTcp", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.Primitives", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.Routing", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.Security", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.ServiceMoniker40", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.WasHosting", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.Web", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceProcess", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Text.Encoding", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Text.Encoding.Extensions", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Text.RegularExpressions", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Threading", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Threading.Tasks", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Threading.Tasks.Parallel", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Transactions", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Web.Abstractions", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Web.ApplicationServices", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Web.DataVisualization.Design", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Web.DataVisualization", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Web", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Web.DynamicData.Design", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Web.DynamicData", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Web.Entity.Design", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Web.Entity", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Web.Extensions.Design", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Web.Extensions", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Web.Mobile", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Web.RegularExpressions", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Web.Routing", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Web.Services", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Windows", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Windows.Forms.DataVisualization.Design", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Windows.Forms.DataVisualization", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Windows.Forms", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Workflow.Activities", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Workflow.ComponentModel", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Workflow.Runtime", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.WorkflowServices", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Xaml", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Xaml.Hosting", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Xml", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Xml.Linq", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Xml.ReaderWriter", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Xml.Serialization", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Xml.XDocument", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("System.Xml.XmlSerializer", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("PresentationBuildTasks", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("PresentationCore", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("PresentationFramework.Aero", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("PresentationFramework.AeroLite", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("PresentationFramework.Classic", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("PresentationFramework", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("PresentationFramework.Luna", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("PresentationFramework.Royale", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("PresentationFramework-SystemCore", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("PresentationFramework-SystemData", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("PresentationFramework-SystemDrawing", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("PresentationFramework-SystemXml", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("PresentationFramework-SystemXmlLinq", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("PresentationUI", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("ReachFramework", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Printing", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Speech", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Windows.Controls.Ribbon", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Windows.Input.Manipulations", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("System.Windows.Presentation", v40, cultureCommon, ecmaKey),
                AstIdentifier.GetAssemblyIdentifier("UIAutomationClient", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("UIAutomationClientsideProviders", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("UIAutomationProvider", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("UIAutomationTypes", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("WindowsBase", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("WindowsFormsIntegration", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("WsatConfig", v40, cultureCommon, microsoftAltKey),
                AstIdentifier.GetAssemblyIdentifier("XamlBuildTask", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("XsdBuildTask", v40, cultureCommon, microsoftKey) };

        private static void Test()
        {
            string assemblyLocation = @"C:\Program Files (x86)\Microsoft XNA\XNA Game Studio\v4.0\References\Windows\x86";// @"C:\Users\Allen Copeland\Documents\Visual Studio 2010\Projects\Test Member Kinds\bin\Release";//Path.GetDirectoryName(typeof(Program).Assembly.Location);
            var files = ObtainFrameworkAssemblies(CliGateway.GetRuntimeEnvironmentInfo(CliGateway.CurrentPlatform, CliGateway.CurrentVersion, true, true, true, assemblyLocation)).ToArray();
            var timeFunc = MiscHelperMethods.CreateActionOfTime<string[]>(ReflectionVersion);
            var rv1 = timeFunc(files);
            var rv2 = timeFunc(files);
            Console.WriteLine("From reflection (first): {0}\nSecond pass: {1}", rv1, rv2);
            //Console.WriteLine("From Reflection: {0}", sw.Elapsed);
            //Console.WriteLine(assemblyLocation);
            var timedProcess = Process1(assemblyLocation);
            Console.WriteLine(timedProcess);
            timedProcess = Process1(assemblyLocation);
            Console.WriteLine(timedProcess);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private static void ReflectionVersion(string[] files)
        {
            //var assemblyNames = (from id in identifiers
            //                     select GetAssemblyNameFromId(id)).ToArray();
            var assemblies = (from fileName in files.AsParallel()
                              let assembly = TryLoadAssembly(fileName)
                              where assembly != null
                              select assembly).ToArray();
            var query = (from assembly in assemblies.AsParallel()
                         from type in typeof(int).Assembly.GetTypes().AsParallel()
                         from member in type.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
                         select member).ToArray();
        }

        private static Assembly TryLoadAssembly(string fileName)
        {
            try
            {
                return Assembly.LoadFrom(fileName);
            }
            catch
            {
                return null;
            }
        }

        //private static Assembly TryLoadAssembly(AssemblyName name)
        //{
        //    try
        //    {
        //        return Assembly.Load(name);
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        private static AssemblyName GetAssemblyNameFromId(IAssemblyUniqueIdentifier id)
        {
            AssemblyName result = new AssemblyName(id.Name);
            if (id.Culture != CultureIdentifiers.None)
                result.CultureInfo = CultureInfo.GetCultureInfo((int)id.Culture.Culture);
            if (id.PublicKeyToken != null)
                result.SetPublicKeyToken(id.PublicKeyToken);
            result.Version = new Version(id.Version.Major, id.Version.Minor, id.Version.Build, id.Version.Revision);
            return result;
        }

        private static Tuple<TimeSpan, TimeSpan> Process1(string assemblyLocation)
        {
            _ICliManager clim = (_ICliManager)CliGateway.CreateIdentityManager(CliGateway.CurrentPlatform, CliGateway.CurrentVersion, true, true, true, assemblyLocation);
            var timedProcess = DoProcess(clim);
            //Console.ReadKey(true);
            clim.Dispose();
            return timedProcess;
        }

        //private static void ObtainAssemblyFilenames(_ICliManager clim)
        //{
        //    files = (from f in ObtainFrameworkAssemblies(clim.RuntimeEnvironment)
        //             orderby f
        //             select f).ToArray();
        //}

        public class TA : Attribute
        {
            public TA(object o) { }
        }

        private static void TestLinq(IIntermediateAssembly assembly, string baseName = "LinqTest", int baseIndex = 0)
        {
            //using System;
            assembly.ScopeCoercions.Add(typeof(Console).Namespace);
            //using System.Linq;
            assembly.ScopeCoercions.Add(typeof(Queryable).Namespace);
            assembly.ScopeCoercions.Add(typeof(CultureInfo).Namespace);
            IIntermediateNamespaceDeclaration @namespace = null;
            IIntermediateTopLevelMethodMember topLevelMethod = null;
            lock (assembly)
            {
                if (assembly.Namespaces.ContainsName("LinqExample"))
                    @namespace = assembly.Namespaces["LinqExample"];
                else
                    @namespace = assembly.Namespaces.Add("LinqExample");
            }
            if (baseIndex == 0)
                topLevelMethod = @namespace.Methods.Add(baseName);
            else
                topLevelMethod = @namespace.Methods.Add(string.Format("{0}{1:x4}", baseName, baseIndex));
            //var digits = new String[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" }; 
            //var digits = topLevelMethod.Locals.Add(
            //        "digits",
            //        ,
            //        LocalTypingKind.Implicit);
            var digitSymbol = (Symbol)"digit";
            /* *
             * If you know you're dealing with properties and methods,
             * use the appropriate Get* method on the expression builders
             * otherwise...
             * */

            /* *
             * var sortedDigits = from digit in digits
             *                    orderby digit.Length descending,
             *                            digit[0]
             *                    select digit;
             * */
            var sortedDigits = topLevelMethod.Locals.Add("sortedDigits",
                    LinqHelper
                    .From("digit", /* in */ (new [,] { {"one", "two", "three" }, { "four", "five", "six" }, { "seven", "eight", "nine" } }).ToExpression((ICliManager)assembly.IdentityManager))
                        .OrderBy(digitSymbol.GetProperty("Length"), LinqOrderByDirection.Descending)
                        .ThenBy(digitSymbol.GetIndexer(0.ToPrimitive()))
                    .Select(digitSymbol).Build(), LocalTypingKind.Implicit);
            /* *
             * from c in sigSource.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
             * where c.GetParameters().Length > 0
             * select c;
             * */

            /* *
             *      (from runtimeDirectory in environment.ResolutionPaths
             *       from file in runtimeDirectory.GetFiles()
             *       let extension = Path.GetExtension(file.FullName).ToLower()
             *       where extension == ".exe" || extension == ".dll"
             *       where CliGateway.IsFullAssembly(file.FullName)
             *       orderby file.Length descending
             *       select file.FullName).Distinct()
             * */
            var runtimeResolutionPaths = LinqHelper.From("runtimeDirectory", /* in */ "environment".Fuse("ResolutionPaths"))
                                                   .From("file", /* in */ "runtimeDirectory".Fuse("GetFiles").Fuse(new IExpression[0]))
                                                   .Let("extension", /* = */ "Path".Fuse("GetExtension").Fuse("file".Fuse("FullName")).Fuse("ToLower").Fuse(new IExpression[0]))
                                                   .Where(new SymbolExpression("extension").EqualTo(".exe".ToPrimitive()).LogicalOr(new SymbolExpression("extension").EqualTo(".dll".ToPrimitive())))
                                                   .Where("CliGateway".Fuse("IsFullAssembly").Fuse("file".Fuse("FullName")))
                                                   .OrderBy("file".Fuse("Length"), LinqOrderByDirection.Descending)
                                                   .Select("file".Fuse("FullName")).Build();
            //topLevelMethod.DefineLocal(digits);
            topLevelMethod.DefineLocal(sortedDigits);

            /* *
             * Construction of expressions is pretty simple; just fuse strings together, 
             * differentiation takes place on whether the string is an expression or not.
             * */

            /* *
             * Console.WriteLine(CultureInfo.CurrentCulture.TextInfo.ToTitleCase("sorted digits"));
             * */
            topLevelMethod.Call(
                "Console".Fuse("WriteLine").Fuse(
                    "CultureInfo".Fuse("CurrentCulture").Fuse("TextInfo").Fuse("ToTitleCase").Fuse(
                        "sorted digits".ToPrimitive())));
            //Console.WriteLine(runtimeResolutionPaths.From.RangeVariable);
            /* *
             * foreach (digit in sortedDigits)
             *     Console.WriteLine(digit);
             * */
            //var iteratorLocal = topLevelMethod.Locals.Add("digit", null, LocalTypingKind.Implicit);
            //iteratorLocal.AutoDeclare = false;
            var enumerationBlock = topLevelMethod.Enumerate("digit", sortedDigits.GetReference());
            enumerationBlock.Call("Console".Fuse("WriteLine").Fuse(enumerationBlock.Local.GetReference()));
            enumerationBlock.Local.Name = "test";
            //var deq = assembly.Classes.Add("Test");
            //var deqM1 = deq.Methods.Add("test");
            //var deqT1 = deq.TypeParameters.Add("TTestParam");
            //var deqM1P1 = deqM1.Parameters.Add("p1", deqT1);
            //var deqInst = deq.MakeGenericClosure(assembly.IdentityManager.ObtainTypeReference(CliRuntimeCoreType.CompilerGeneratedMetadatum));
            //var deqInstM1 = deqInst.Methods.Values[0];
            //var deqInstM1P1 = deqInstM1.Parameters.Values[0];
            //Console.WriteLine(deqInstM1P1.ParameterType.UniqueIdentifier);
        }

        private static Tuple<TimeSpan, TimeSpan> DoProcess(_ICliManager clim)
        {
            var timedObtainTypeMembers = MiscHelperMethods.CreateFunctionOfTime<_ICliManager, Tuple<CliMemberType, ICliMetadataTableRow>[]>(Program.ObtainTypeMembers);
            var result1 = timedObtainTypeMembers(clim);
            var timedCheckMembers = MiscHelperMethods.CreateActionOfTime<_ICliManager>(CheckMember);

            //var timedCheckReflectionMembers = MiscHelperMethods.CreateActionOfTime(CheckMemberReflection);
            //var checkMembersTime = timedCheckMembers(clim);
            //var checkMembersReflectionTime = timedCheckReflectionMembers();
            //var checkMembersTime2 = timedCheckMembers(clim);
            //var checkMembersReflectionTime2 = timedCheckReflectionMembers();
            //Console.WriteLine("Check members took {0} - reflection {1}.", checkMembersTime, checkMembersReflectionTime);
            //Console.WriteLine("Second members check took {0} - reflection {1}.", checkMembersTime2, checkMembersReflectionTime2);
            var propertySets1 = result1.Item1;
            var result2 = timedObtainTypeMembers(clim);
            var propertySets2 = result2.Item1;
            return new Tuple<TimeSpan, TimeSpan>(propertySets1, propertySets2);
        }
        private delegate void PD(params int[] data);

        private struct testS
        {
            private event PD Test;
        }

        private static void CheckMember(_ICliManager clim)
        {
            //var dedc = (IDelegateType)typeof(PD).GetTypeReference(clim);
            //var deae = typeof(Action<int, double, decimal>).GetTypeReference(clim);
            //Console.WriteLine(deae);
            //var pdType = (IDelegateType)typeof(PD).GetTypeReference(clim);
            //Console.WriteLine(pdType.LastIsParams);

            var med = (IStructType)typeof(testS).GetTypeReference(clim);
            var m = (ICliType)typeof(CliMetadataReturnTypeSignature).GetTypeReference(clim);
            var deam = m.MetadataEntry;
            Console.WriteLine(med.Events[0].Value.OnAddMethod.ReturnType);
            //var dedcGI = dedc.MakeGenericClosure(typeof(int).GetTypeReference(clim), typeof(long).GetTypeReference(clim), typeof(double).GetTypeReference(clim));
            //var giParam3 = dedcGI.Parameters[2].Value;
            //var meda = dedcGI.UniqueIdentifier.ToString();
            //var giParam3T = giParam3.ParameterType;
            //Console.WriteLine(giParam3T);
            //var md = (IStructType)typeof(TimeSpan).GetTypeReference(clim);
            //foreach (var field in md.Fields.Values)
            //    field.ToString();
            //Console.WriteLine(mdeee.FieldType == md);
            //Console.WriteLine(mdeee.UniqueIdentifier);
        }

        private static void CheckMemberReflection()
        {
            var md = typeof(TimeSpan);
            foreach (var field in md.GetFields())
                field.ToString();
        }

        private static Tuple<CliMemberType, ICliMetadataTableRow>[] ObtainTypeMembers(_ICliManager clim)
        {
            var assemblyQuery =
                from aId in identifiers.AsParallel()
                select (ICliAssembly)clim.ObtainAssemblyReference(aId);
            var eQuery = (from assembly in assemblyQuery
                          from typeDef in assembly.MetadataRoot.TableStream.TypeDefinitionTable
                          select typeDef.GetMemberData().ToArray()).ToArray().ConcatinateSeries();
            foreach (var assembly in assemblyQuery)
                CliMetadataValidator.ValidateMetadata(assembly, assembly.MetadataRoot);
            //var nQuery = (from t in eQuery
            //              select t.GetMemberData().ToArray()).ToArray().ConcatinateSeries();
            return eQuery;
        }

        public static ICliAssembly ObtainRef(_ICliManager manager, IAssemblyUniqueIdentifier id)
        {
            var result = (CliAssembly)manager.ObtainAssemblyReference(id);
            var tableStream = result.MetadataRoot.TableStream;
            foreach (var t in tableStream.Values)//(new ICliMetadataTable[] { (ICliMetadataTable)tableStream.TypeDefinitionTable, (ICliMetadataTable)tableStream.MethodSemanticsTable, (ICliMetadataTable)tableStream.PropertyTable, (ICliMetadataTable)tableStream.MethodDefinitionTable, (ICliMetadataTable)tableStream.EventTable, (ICliMetadataTable)tableStream.EventMapTable, (ICliMetadataTable)tableStream.PropertyMapTable }).AsParallel())
                //if (t != null)
                t.Read();
            //if (tableStream.TypeDefinitionTable != null)
            //    tableStream.TypeDefinitionTable.Read();
            //if (tableStream.MethodSemanticsTable != null)
            //    tableStream.MethodSemanticsTable.Read();
            //if (tableStream.MethodDefinitionTable != null)
            //    tableStream.MethodDefinitionTable.Read();
            //if (tableStream.PropertyTable != null)
            //    tableStream.PropertyTable.Read();
            //if (tableStream.EventTable != null)
            //    tableStream.EventTable.Read();
            //if (tableStream.EventMapTable != null)
            //    tableStream.EventMapTable.Read();
            //if (tableStream.PropertyMapTable != null)
            //    tableStream.PropertyMapTable.Read();
            return result;
        }

        private static void Junk()
        {
            //StringBuilder sb = new StringBuilder();
            //sb.Append("new string[] { ");
            //bool first = true;
            //foreach (var assem in assemblies)
            //{
            //    if (first)
            //        first = false;
            //    else
            //        sb.Append(", ");
            //    sb.AppendFormat("\"{0}\"", assem.UniqueIdentifier.Name);
            //}
            //sb.Append(" }");
            //var de = sb.ToString();
            //sw.Restart();
            Console.WriteLine("starting filter");
        }

        private static _ICliAssembly AttemptGetAssembly(_ICliManager clim, string f)
        {
            try
            {
                return (_ICliAssembly)clim.ObtainAssemblyReference(f);
            }
            catch (BadImageFormatException)
            {
                return null;
            }
        }

        private static IEnumerable<string> ObtainFrameworkAssemblies(ICliRuntimeEnvironmentInfo environment)
        {
            return (from runtimeDirectory in environment.ResolutionPaths
                    from file in runtimeDirectory.GetFiles()
                    let extension = Path.GetExtension(file.FullName).ToLower()
                    where extension == ".exe" || extension == ".dll"
                    where CliGateway.IsFullAssembly(file.FullName)
                    orderby file.Length descending
                    select file.FullName).Distinct();
        }
    }
}


