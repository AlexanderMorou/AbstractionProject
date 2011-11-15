using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Languages.Cil;
using AllenCopeland.Abstraction.Slf.Languages.CSharp;
using AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions;
using AllenCopeland.Abstraction.Slf.Languages.VisualBasic;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.SupplementaryProjects.BugTestApplication.Examples;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Common;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.SupplementaryProjects.BugTestApplication
{
    using linqExample = ExampleHandler.LanguageIntegratedQuery;
    using winformExample = ExampleHandler.WindowsFormsApplication;
    using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
    using AllenCopeland.Abstraction.Globalization;
    using AllenCopeland.Abstraction.Utilities.Collections;
    using AllenCopeland.Abstraction.Slf.Oil.Members;

    internal class TestClass {
        public virtual event EventHandler Test;
    }

    internal static class Program
    {
        private static void Main()
        {
            RunExamples();
        }

        private static void RunExamples()
        {
            var msLangVendor = LanguageVendors.Microsoft;
            IMyVisualBasicAssembly vbAssem = null;
            ICSharpAssembly csAssem = null;

            var winFormsVB = MiscHelperMethods.TimeResultFunc(winformExample.CreateStructureVB, () => vbAssem = msLangVendor.GetVisualBasicLanguage().GetMyProvider().CreateAssembly("VB.Net examples"));
            var winFormsCS = MiscHelperMethods.TimeResultFunc(winformExample.CreateStructureCSharp, () => csAssem = msLangVendor.GetCSharpLanguage().CreateAssembly("CSharp examples"));

            var linqVB = MiscHelperMethods.TimeResultFunc(linqExample.CreateStructureVB, () => vbAssem);
            var linqCS = MiscHelperMethods.TimeResultFunc(linqExample.CreateStructureCSharp, () => csAssem);

            var vbTestFunc = MiscHelperMethods.TimeResultFunc(() =>
            {
                var winForms = winFormsVB();
                var linq = linqVB();
                return Tuple.Create(winForms.Item1, linq.Item1, winForms.Item2, linq.Item2);
            });
            var csTestFunc = MiscHelperMethods.TimeResultFunc(() =>
            {
                var winForms = winFormsCS();
                var linq = linqCS();
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
                var item = vbTestFunc();
                vbAssem.Dispose();
                vbAssem = null;
            });
            //CLIGateway.ClearCache();
            vbAssem = null;
            Console.WriteLine("Initial tests took {0}", jitTest);
            var vbTest = vbTestFunc();
            var csTest = csTestFunc();
            Console.WriteLine("VB.NET test took: {0}", vbTest.Item1);
            Console.WriteLine("\tWinForms: {0}", vbTest.Item2.Item1);
            Console.WriteLine("\tLinq: {0}", vbTest.Item2.Item2);
            Console.WriteLine("C# test took: {0}", csTest.Item1);
            Console.WriteLine("\tWinForms: {0}", csTest.Item2.Item1);
            Console.WriteLine("\tLinq: {0}", csTest.Item2.Item2);
            vbAssem.Dispose();
            csAssem.Dispose();
            CLIGateway.ClearCache();
        }

    }
}
