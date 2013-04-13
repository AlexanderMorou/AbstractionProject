using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Languages.Cil;
using AllenCopeland.Abstraction.Slf.Languages.CSharp;
using AllenCopeland.Abstraction.Slf.Languages.VisualBasic;
using AllenCopeland.Abstraction.Utilities;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AllenCopeland.Abstraction.Slf.SupplementaryProjects.TestCli
{
    static class Program
    {
        static void Main() {
            var timedLanguageTest = MiscHelperMethods.CreateFunctionOfTime<Tuple<TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, Tuple<TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, Tuple<IIntermediateClassType>>, Tuple<Tuple<TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, Tuple<IIntermediateClassType>>, Tuple<TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, Tuple<IIntermediateClassType>>>>>(LangaugeTests);
            var elapsedFirst = timedLanguageTest();
            Console.Clear();
            var elapsedSecond = timedLanguageTest();
            Console.WriteLine("Took {0}ms to process first time around.\nTook {1}ms to process second time around.", elapsedFirst.Item1.TotalMilliseconds, elapsedSecond.Item1.TotalMilliseconds);
            Console.WriteLine(@"Breakdown:
VB Core Provider: {0}
VB Core Structure: {1}
VB My Provider: {2}
VB My Structure: {3}
C# Provider: {4}
C# Structure: {5}", elapsedSecond.Item2.Item1, elapsedSecond.Item2.Item2, elapsedSecond.Item2.Item3, elapsedSecond.Item2.Item4, elapsedSecond.Item2.Item5, elapsedSecond.Item2.Item6);
            Console.WriteLine(@"C# Structure Breakdown:
Assembly Creation: {0}
Namespace Creation: {1}
Program Creation: {2}
Scope Modification: {3}
Main Procedure Creation: {4}
Primitive Creation: {5}
Console.WriteLine: {6}", elapsedSecond.Item2.Item7.Item1, elapsedSecond.Item2.Item7.Item2, elapsedSecond.Item2.Item7.Item3, elapsedSecond.Item2.Item7.Item4, elapsedSecond.Item2.Item7.Item5, elapsedSecond.Item2.Item7.Item6, elapsedSecond.Item2.Item7.Item7);
            Console.WriteLine(@"VB Core Program Type: {0}
VB My Program Type  : {1}
C# Program Type     : {2}", elapsedSecond.Item2.Item7.Rest.Item1.GetType(), elapsedSecond.Item2.Rest.Item1.Rest.Item1.GetType(), elapsedSecond.Item2.Rest.Item2.Rest.Item1.GetType());
        }

        private static Tuple<TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, Tuple<TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, Tuple<IIntermediateClassType>>, Tuple<Tuple<TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, Tuple<IIntermediateClassType>>, Tuple<TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, Tuple<IIntermediateClassType>>>> LangaugeTests()
        {
            /* *
             * 
             * */
            var intermediateManager = IntermediateGateway.CreateIdentityManager(CliGateway.CurrentPlatform, CliGateway.CurrentVersion);
            Stopwatch sw = Stopwatch.StartNew();
            var vbProvider = LanguageVendors.Microsoft.GetVisualBasicLanguage().GetProvider(VisualBasicVersion.CurrentVersion, intermediateManager);
            sw.Stop();
            var first = sw.Elapsed;
            sw.Restart();
            var vbCoreBodyTime = CreateBody<IVisualBasicLanguage, ICoreVisualBasicProvider>(vbProvider);
            sw.Stop();
            var second = sw.Elapsed;
            sw.Restart();
            var vbMyProvider = LanguageVendors.Microsoft.GetVisualBasicLanguage().GetMyProvider(VisualBasicVersion.CurrentVersion, intermediateManager);
            sw.Stop();
            var third = sw.Elapsed;
            sw.Restart();
            var vbMyBodyTime = CreateBody<IVisualBasicLanguage, IMyVisualBasicProvider>(vbMyProvider);
            sw.Stop();
            var fourth = sw.Elapsed;
            sw.Restart();
            var cSharpProvider = LanguageVendors.Microsoft.GetCSharpLanguage().GetProvider(CSharpLanguageVersion.CurrentVersion, intermediateManager);
            sw.Stop();
            var fifth = sw.Elapsed;
            sw.Restart();
            var csharpBodyTime = CreateBody<ICSharpLanguage, ICSharpProvider>(cSharpProvider);
            sw.Stop();
            var sixth = sw.Elapsed;
            return TupleHelper.Create(first, second, third, fourth, fifth, sixth, vbCoreBodyTime, vbMyBodyTime, csharpBodyTime);
        }

        private static Tuple<TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, Tuple<IIntermediateClassType>> CreateBody<TLanguage, TProvider>(TProvider provider)
            where TLanguage :
                ILanguage<TLanguage, TProvider>
            where TProvider :
                ILanguageProvider<TLanguage, TProvider>
        {
            Stopwatch sw = Stopwatch.StartNew();
            var assembly = (IIntermediateAssembly)provider.CreateAssembly("TestVBAssembly");
            sw.Stop();
            var createAssembly = sw.Elapsed;
            sw.Restart();
            assembly.DefaultNamespace = assembly.Namespaces.Add("AllenCopeland.Abstraction.Slf.SupplementaryProjects.VBTest");
            sw.Stop();
            var namespaceCreation = sw.Elapsed;
            sw.Restart();
            var program = assembly.DefaultNamespace.Parts.Add().Classes.Add("Program");
            sw.Stop();
            var programTime = sw.Elapsed;
            sw.Restart();
            assembly.ScopeCoercions.Add("System.Console".GetSymbolType(), false);
            sw.Stop();
            var scopeCoercionInsertion = sw.Elapsed;
            sw.Restart();
            var main = program.Methods.Add("Main");
            sw.Stop();
            var mainTime = sw.Elapsed;
            sw.Restart();
            main.IsStatic = true;
            string versionString = string.Empty;
            var helloWorldPrimitive = string.Format("Hello World from {0}!", provider.Language.Name).ToPrimitive();
            sw.Stop();
            var primitiveTime = sw.Elapsed;
            sw.Restart();
            main.Call("Console".Fuse("WriteLine").Fuse(helloWorldPrimitive));
            sw.Stop();
            var writeTime = sw.Elapsed;
            return Tuple.Create(createAssembly, namespaceCreation, programTime, scopeCoercionInsertion, mainTime, primitiveTime, writeTime, program);
        }
    }
}
