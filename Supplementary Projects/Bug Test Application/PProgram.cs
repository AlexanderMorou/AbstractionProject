using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
/*----------------------------------------\
| Copyright © 2009 Allen Copeland Jr.     |
|-----------------------------------------|
| The Abstraction Project's code is prov- |
| -ided under a contract-release basis.   |
| DO NOT DISTRIBUTE and do not use beyond |
| the contract terms.                     |
\--------------------------------------- */

namespace AllenCopeland.Abstraction.SupplimentaryProjects.BugTestApplication
{
    internal static class Program
    {
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new ObjectBrowserDialog());
        }
    }
}
