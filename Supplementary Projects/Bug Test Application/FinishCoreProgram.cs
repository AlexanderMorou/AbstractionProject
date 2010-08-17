using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.SupplimentaryProjects.BugTestApplication
{
    static class FinishCoreProgram
    {
        static void Main()
        {
            /* *
             * TODO:
             * *
             * Intermediate Core:
             * Finish interface events, indexers, methods and properties.
             * Finish class/struct fields, indexers, and properties
             * Implement struct type.
             * Finish generic parameter constructors, events, indexers, 
             *                          methods, and properties
             * *
             * Compiled Core:
             * Finish class/struct events, indexers, type coercions, unary operators.
             * Finish interface events, indexers, and properties
             * Finish generic parameter events and indexers.
             * *
             * Generic Instance Core:
             * Finish class/struct members, pretty much all of them
             * Finish interface members
             * This area requires the most work.
             * */
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainDialog());
        }
    }
}
