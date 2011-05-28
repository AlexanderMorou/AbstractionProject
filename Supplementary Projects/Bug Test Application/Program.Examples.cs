using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.SupplementaryProjects.BugTestApplication.Examples;
using AllenCopeland.Abstraction.Utilities.Common;

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
            var jitTest = MiscHelperMethods.TimeAction(() =>
            {
                winFormsVB().Item2.Item1.Dispose();
                winFormsCS().Item2.Item1.Dispose();
            });
            Console.WriteLine("Initial tests took {0}", jitTest);
            var vbTest = winFormsVB();
            var csTest = winFormsCS();
            Console.WriteLine("C# test took: {0}", csTest.Item1);
            Console.WriteLine("VB.NET test took: {0}", vbTest.Item1);
            vbTest.Item2.Item1.Dispose();
            csTest.Item2.Item1.Dispose();
        }
    }
}
