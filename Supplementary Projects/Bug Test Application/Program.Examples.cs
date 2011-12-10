using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Languages.CSharp;
using AllenCopeland.Abstraction.Slf.Languages.VisualBasic;
using AllenCopeland.Abstraction.Utilities.Miscellaneous;
using linqExample = AllenCopeland.Abstraction.SupplementaryProjects.BugTestApplication.Examples.ExampleHandler.LanguageIntegratedQuery;
using winformExample = AllenCopeland.Abstraction.SupplementaryProjects.BugTestApplication.Examples.ExampleHandler.WindowsFormsApplication;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.SupplementaryProjects.BugTestApplication
{

    internal static class Program
    {
        private static void Main()
        {
            //var msLangVendor = LanguageVendors.Microsoft;
            //IIntermediateAssembly iia = msLangVendor.GetVisualBasicLanguage().GetMyProvider().CreateAssembly("TestAssembly");
            //var uas = iia.Methods.Add(new TypedName("TestMethod1", typeof(void).GetTypeReference()), new TypedNameSeries(new TypedName("arg1", "IList".GetSymbolType("T1".GetSymbolType()))), new GenericParameterData("T1"));
            //var uasTC1 = uas.Classes.Add("TestClass1");
            //var uas2 = uasTC1.Methods.Add(new TypedName("TestMethod2", typeof(void).GetTypeReference()));
            //var m = typeof(Predicate<string>).GetTypeReference();
            //foreach (var item in m.Members)
            //    Console.WriteLine(item);
            RunExamples();
        }

        private static void RunExamples()
        {
            var msLangVendor = LanguageVendors.Microsoft;
            IMyVisualBasicAssembly vbAssem = null;
            ICSharpAssembly csAssem = null;

            var linqVB = MiscHelperMethods.TimeResultFunc(linqExample.CreateStructureVB, () => vbAssem = msLangVendor.GetVisualBasicLanguage().GetMyProvider().CreateAssembly("VB.Net examples"));
            var linqCS = MiscHelperMethods.TimeResultFunc(linqExample.CreateStructureCSharp, () => csAssem = msLangVendor.GetCSharpLanguage().CreateAssembly("CSharp examples"));

            var winFormsVB = MiscHelperMethods.TimeResultFunc(winformExample.CreateStructureVB, () => vbAssem);
            var winFormsCS = MiscHelperMethods.TimeResultFunc(winformExample.CreateStructureCSharp, () => csAssem);

            var vbTestFunc = MiscHelperMethods.TimeResultFunc(() =>
            {
                var linq = linqVB();
                var winForms = winFormsVB();
                return Tuple.Create(winForms.Item1, linq.Item1, winForms.Item2, linq.Item2);
            });
            var csTestFunc = MiscHelperMethods.TimeResultFunc(() =>
            {
                var linq = linqCS();
                var winForms = winFormsCS();
                return Tuple.Create(winForms.Item1, linq.Item1, winForms.Item2, linq.Item2);
            });
            Console.WriteLine("Running initial test...");
            Console.WriteLine("* * * * * * * * * * * * * * * * * * * * * * * * *");
            Console.WriteLine("* If the native image has been generated for    *");
            Console.WriteLine("* this project and its dependencies, then this  *");
            Console.WriteLine("* caches commonly used types; otherwise, this   *");
            Console.WriteLine("* also accounts for JIT overhead.               *");
            Console.WriteLine("* * * * * * * * * * * * * * * * * * * * * * * * *");
            var jitTest = MiscHelperMethods.TimeAction(() =>
            {
                using (vbTestFunc().Item2.Item3.Item1) { vbAssem = null; }
                using (csTestFunc().Item2.Item3.Item1) { csAssem = null; }
                //CLIGateway.ClearCache();
            });
            Console.WriteLine("Initial tests took {0}", jitTest);
            CLIGateway.ClearCache();
            var vbTest = vbTestFunc();
            CLIGateway.ClearCache();
            var csTest = csTestFunc();
            Console.WriteLine("VB.NET test took: {0}", vbTest.Item1);
            Console.WriteLine("\tWinForms: {0}", vbTest.Item2.Item1);
            Console.WriteLine("\tLinq: {0}", vbTest.Item2.Item2);
            Console.WriteLine();
            Console.WriteLine("C# test took: {0}", csTest.Item1);
            Console.WriteLine("\tWinForms: {0}", csTest.Item2.Item1);
            Console.WriteLine("\tLinq: {0}", csTest.Item2.Item2);
            Console.WriteLine();
            vbAssem.Dispose();
            csAssem.Dispose();
            var cacheEnum = CLIGateway.CacheEnumerator.ToArray();
            var cacheClear = MiscHelperMethods.TimeAction(CLIGateway.ClearCache);
            Console.WriteLine("It took {0} to clear the type-reference cache.", cacheClear);
        }

    }
}
