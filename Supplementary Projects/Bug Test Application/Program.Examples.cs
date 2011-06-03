using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.SupplementaryProjects.BugTestApplication.Examples;
using AllenCopeland.Abstraction.Utilities.Common;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Diagnostics;

namespace AllenCopeland.Abstraction.SupplementaryProjects.BugTestApplication
{
    internal class Program
    {
        private static void Main()
        {
            var winFormsVB = MiscHelperMethods.TimeResultFunc(ExampleHandler.WindowsFormsApplication.CreateProjectVB);
            var winFormsCS = MiscHelperMethods.TimeResultFunc(ExampleHandler.WindowsFormsApplication.CreateProjectCSharp);
            Console.WriteLine("Running initial test...");
            Console.WriteLine("* * * * * * * * * * * * * * * * * * * * * * * * *");
            Console.WriteLine("* If the native image has been generated for    *");
            Console.WriteLine("* this project and its dependencies, then this  *");
            Console.WriteLine("* caches commonly used types; otherwise, this   *");
            Console.WriteLine("* also accounts for JIT overhead.               *");
            Console.WriteLine("* * * * * * * * * * * * * * * * * * * * * * * * *");
            var jitTest = MiscHelperMethods.TimeAction(() => winFormsVB().Item2.Item1.Dispose());
            CLIGateway.ClearCache();
            Console.WriteLine("Initial tests took {0}", jitTest);
            var vbWinFormsTest = winFormsVB();
            var csWinFormsTest = winFormsCS();
            Console.WriteLine("C# test took: {0}", csWinFormsTest.Item1);
            Console.WriteLine("VB.NET test took: {0}", vbWinFormsTest.Item1);
            vbWinFormsTest.Item2.Item1.Dispose();
            csWinFormsTest.Item2.Item1.Dispose();
            CLIGateway.ClearCache();
            //arr1();
        }

        private static void FullName()
        {
            var Test = LanguageVendors.Microsoft.GetCSharpLanguage().CreateAssembly("TestAssembly");
            var class1 = Test.Classes.Add("Class1", new GenericParameterData("T1"));
            var method1 = class1.Methods.Add("Method1", TypedNameSeries.Empty, new GenericParameterData("T2"));
            var class2 = method1.Classes.Add("Class2", new GenericParameterData("T3"));
            Console.WriteLine(class2.FullName);
        }

        private static void arr1()
        {
            Random  r= new Random();
            int[][] ds = new int[800][];
            int value = 0;
            for (int i = 0; i < ds.Length; i++)
            {
                var dsCurrent = new int[r.Next(10, 80)];
                for (int j = 0; j < dsCurrent.Length; j++)
                    dsCurrent[j] = ++value;
                ds[i] = dsCurrent;
                value++;
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            const int testCount = 10000;
            for (int i = 0; i < testCount; i++)
            {
                var ag = ds.AsParallel().Aggregate((a3, a4) =>
                {
                    var a5 = new int[a3.Length + a4.Length];
                    a3.CopyTo(a5, 0);
                    a4.CopyTo(a5, a3.Length);
                    return a5;
                });
                ag.ToString();
            }
            sw.Stop();
            var chunkCopy = sw.Elapsed;
            sw.Reset();
            sw.Start();
            for (int i = 0; i < testCount; i++)
            {
                var ag = Tweaks.MergeArrays(ds);
                ag.ToString();
            }
            sw.Stop();
            var fullCopy = sw.Elapsed;
            Console.WriteLine("Chunk copy took {0} for {1} iterations on {3} items whereas\r\nInline group copy took {2}.", chunkCopy, testCount, fullCopy, (from a in ds
                                                                                                                                                             select a.Length).Sum());
        }
    }
}
