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
        private static byte[] ecmaKey = new byte[] { 0xb7, 0x7a, 0x5c, 0x56, 0x19, 0x34, 0xe0, 0x89 };
#if DEBUG
        private static byte[] abstractionKey = null;
#else
        private static byte[] abstractionKey = new byte[] { 0x9f, 0x6e, 0xc7, 0xb8, 0xea, 0x17, 0x8, 0x9 };
#endif
        private static byte[] microsoftKey = new byte[] { 0x31, 0xbf, 0x38, 0x56, 0xad, 0x36, 0x4e, 0x35 };
        private static byte[] microsoftAltKey = new byte[] { 0xb0, 0x3f, 0x5f, 0x7f, 0x11, 0xd5, 0xa, 0x3a };
        private static IVersion v10 = AstIdentifier.GetVersion(1, 0, 0, 0);
        private static IVersion v20 = AstIdentifier.GetVersion(2, 0, 0, 0);
        private static IVersion v40 = AstIdentifier.GetVersion(4, 0, 0, 0);
        private static IVersion v80 = AstIdentifier.GetVersion(8, 0, 0, 0);
        private static IVersion v100 = AstIdentifier.GetVersion(10, 0, 0, 0);
        private static ICultureIdentifier cultureCommon = CultureIdentifiers.GetIdentifierById(CultureIdentifiers.NumericIdentifiers.None);
        private static IAssemblyUniqueIdentifier[] identifiers = new IAssemblyUniqueIdentifier[] {
                AstIdentifier.GetAssemblyIdentifier("_abs.slf.compilerservices", v10, cultureCommon, abstractionKey),
                AstIdentifier.GetAssemblyIdentifier("_abs.slf.metadata", v10, cultureCommon, abstractionKey),
                AstIdentifier.GetAssemblyIdentifier("_abs.slf.typesystems.abstract", v10, cultureCommon, abstractionKey),
                AstIdentifier.GetAssemblyIdentifier("_abs.slf.typesystems.cli", v20, cultureCommon, abstractionKey),
                AstIdentifier.GetAssemblyIdentifier("_abs.supplementary.cts.test", v10, cultureCommon, abstractionKey),
                AstIdentifier.GetAssemblyIdentifier("_abs.util.common", v10, cultureCommon, abstractionKey),
                AstIdentifier.GetAssemblyIdentifier("Accessibility", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("AddInProcess", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("AddInProcess32", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("AddInUtil", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("aspnet_compiler", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("aspnet_regbrowsers", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("aspnet_regsql", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("AspNetMMCExt", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("caspol", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("ComSvcConfig", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("CustomMarshalers", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("DataSvcUtil", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("dfsvc", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("EdmGen", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("InstallUtil", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("ISymWrapper", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("jsc", v100, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Activities.Build", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Build.Conversion.v4.0", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Build", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Build.Engine", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Build.Framework", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Build.Tasks.v4.0", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Build.Utilities.v4.0", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.CSharp", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Data.Entity.Build.Tasks", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Internal.Tasks.Dataflow", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.JScript", v100, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Transactions.Bridge", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Transactions.Bridge.Dtc", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.VisualBasic.Activities.Compiler", v100, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.VisualBasic.Compatibility.Data", v100, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.VisualBasic.Compatibility", v100, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.VisualBasic", v100, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.VisualBasic.Vsa", v80, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.VisualC", v100, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.VisualC.STLCLR", v20, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Vsa", v80, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Windows.ApplicationServer.Applications", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft.Workflow.Compiler", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("Microsoft_VsaVb", v80, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("MSBuild", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("mscorlib", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("RegAsm", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("RegSvcs", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("SMDiagnostics", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("SMSvcHost", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("sysglobl", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Activities.Core.Presentation", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Activities", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Activities.DurableInstancing", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Activities.Presentation", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.AddIn.Contract", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.AddIn", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Collections.Concurrent", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Collections", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.ComponentModel.Annotations", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.ComponentModel.Composition", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.ComponentModel.Composition.Registration", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.ComponentModel.DataAnnotations", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.ComponentModel", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.ComponentModel.EventBasedAsync", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Configuration", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Configuration.Install", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Core", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Data.DataSetExtensions", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Data", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Data.Entity.Design", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Data.Entity", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Data.Linq", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Data.OracleClient", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Data.Services.Client", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Data.Services.Design", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Data.Services", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Data.SqlXml", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Deployment", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Design", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Device", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Diagnostics.Contracts", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Diagnostics.Debug", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Diagnostics.Tools", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Diagnostics.Tracing", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.DirectoryServices.AccountManagement", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.DirectoryServices", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.DirectoryServices.Protocols", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Drawing.Design", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Drawing", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Dynamic", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Dynamic.Runtime", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.EnterpriseServices", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Globalization", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.IdentityModel", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.IdentityModel.Selectors", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.IdentityModel.Services", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.IO.Compression", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.IO.Compression.FileSystem", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.IO", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.IO.Log", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Linq", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Linq.Expressions", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Linq.Parallel", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Linq.Queryable", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Management", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Management.Instrumentation", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Messaging", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Net", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Net.Http", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Net.Http.Rtc", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Net.Http.WebRequest", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Net.NetworkInformation", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Net.Primitives", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Net.Requests", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Numerics", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.ObjectModel", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Reflection.Context", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Reflection", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Reflection.Emit", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Reflection.Emit.ILGeneration", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Reflection.Emit.Lightweight", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Reflection.Extensions", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Reflection.Primitives", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Resources.ResourceManager", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Runtime.Caching", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Runtime", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Runtime.DurableInstancing", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Runtime.Extensions", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Runtime.InteropServices", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Runtime.InteropServices.WindowsRuntime", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Runtime.Numerics", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Runtime.Remoting", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Runtime.Serialization", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Runtime.Serialization.Formatters.Soap", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Runtime.Serialization.Json", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Runtime.Serialization.Primitives", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Runtime.Serialization.Xml", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Security", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Security.Principal", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.Activation", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.Activities", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.Channels", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.Discovery", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.Duplex", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.Http", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.Internals", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.NetTcp", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.Primitives", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.Routing", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.Security", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.ServiceMoniker40", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.WasHosting", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceModel.Web", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.ServiceProcess", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Text.Encoding", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Text.Encoding.Extensions", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Text.RegularExpressions", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Threading", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Threading.Tasks", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Threading.Tasks.Parallel", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Transactions", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Web.Abstractions", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Web.ApplicationServices", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Web.DataVisualization.Design", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Web.DataVisualization", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Web", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Web.DynamicData.Design", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Web.DynamicData", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Web.Entity.Design", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Web.Entity", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Web.Extensions.Design", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Web.Extensions", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Web.Mobile", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Web.RegularExpressions", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Web.Routing", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Web.Services", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Windows", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Windows.Forms.DataVisualization.Design", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Windows.Forms.DataVisualization", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Windows.Forms", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Workflow.Activities", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Workflow.ComponentModel", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Workflow.Runtime", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.WorkflowServices", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Xaml", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Xaml.Hosting", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Xml", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Xml.Linq", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Xml.ReaderWriter", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Xml.Serialization", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Xml.XDocument", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Xml.XmlSerializer", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("PresentationBuildTasks", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("PresentationCore", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("PresentationFramework.Aero", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("PresentationFramework.AeroLite", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("PresentationFramework.Classic", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("PresentationFramework", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("PresentationFramework.Luna", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("PresentationFramework.Royale", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("PresentationFramework-SystemCore", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("PresentationFramework-SystemData", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("PresentationFramework-SystemDrawing", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("PresentationFramework-SystemXml", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("PresentationFramework-SystemXmlLinq", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("PresentationUI", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("ReachFramework", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Printing", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Speech", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("System.Windows.Controls.Ribbon", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Windows.Input.Manipulations", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("System.Windows.Presentation", v40, cultureCommon, ecmaKey ),
                AstIdentifier.GetAssemblyIdentifier("UIAutomationClient", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("UIAutomationClientsideProviders", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("UIAutomationProvider", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("UIAutomationTypes", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("WindowsBase", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("WindowsFormsIntegration", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("WsatConfig", v40, cultureCommon, microsoftAltKey ),
                AstIdentifier.GetAssemblyIdentifier("XamlBuildTask", v40, cultureCommon, microsoftKey),
                AstIdentifier.GetAssemblyIdentifier("XsdBuildTask", v40, cultureCommon, microsoftKey) };

        private static void Test()
        {

            string assemblyLocation = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            Console.WriteLine(assemblyLocation);
            var timedProcess = Process1(assemblyLocation);
            Console.WriteLine(timedProcess);
            timedProcess = Process1(assemblyLocation);
            Console.WriteLine(timedProcess);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private static Tuple<TimeSpan, TimeSpan> Process1(string assemblyLocation)
        {
            _ICliManager clim = (_ICliManager) CliGateway.CreateIdentityManager(CliGateway.CurrentPlatform, CliGateway.CurrentVersion, true, true, true, assemblyLocation);
            var timedProcess = DoProcess(clim);
            Console.ReadKey(true);
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
        private static Tuple<TimeSpan, TimeSpan> DoProcess(_ICliManager clim)
        {
            Stopwatch sw = Stopwatch.StartNew();
            //var assemblies = (from f in identifiers
            //                  let assembly = clim.ObtainAssemblyReference(f)
            //                  select assembly).ToList();
            //sw.Stop();
            //TimeSpan t1 = sw.Elapsed;
            //sw.Restart();
            //foreach (var t in from assembly in assemblies
            //                  from t in assembly.MetadataRoot.TableStream.Values
            //                  select t)
            //    t.Read();
            //sw.Stop();
            //var t2 = sw.Elapsed;
            TimeSpan t1 = TimeSpan.Zero, t2 = TimeSpan.Zero;
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
            var assem = clim.ObtainAssemblyReference(CliCommon.mscorlibIdentifierv2);
            var clId = AstIdentifier.GetAssemblyIdentifier("System.EnterpriseServices", v40, cultureCommon, microsoftAltKey);
            var doubleModuleLib = clim.ObtainAssemblyReference(clId);
            Console.WriteLine(doubleModuleLib.FrameworkVersion);
            var nsId = AstIdentifier.GetDeclarationIdentifier("System.EnterpriseServices");
            var de = clim.ObtainAssemblyReference(clim.RuntimeEnvironment.CoreLibraryIdentifier).FindType(clim.RuntimeEnvironment.CoreLibraryIdentifier.GetTypeIdentifier("System.Collections.Generic", "Dictionary", 2));
            var filteredTypes = doubleModuleLib.Namespaces[nsId].Classes.Keys;
            foreach (var m in doubleModuleLib.Namespaces[nsId].Types.Keys)
                m.GetHashCode();
            foreach (var filteredType in filteredTypes)
                filteredType.GetHashCode();
            sw.Stop();
            var t3 = sw.Elapsed;
            Console.WriteLine(t3);
            //Console.WriteLine(type);
            return new Tuple<TimeSpan, TimeSpan>(t1, t2);
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

        //private static IEnumerable<string> ObtainFrameworkAssemblies(ICliRuntimeEnvironmentInfo environment)
        //{
        //    return (from runtimeDirectory in environment.ResolutionPaths
        //            from file in runtimeDirectory.GetFiles()
        //            let extension = Path.GetExtension(file.FullName).ToLower()
        //            where extension == ".exe" || extension == ".dll"
        //            where CliGateway.IsFullAssembly(file.FullName)
        //            orderby file.Length descending
        //            select file.FullName).Distinct();
        //}
    }
}
