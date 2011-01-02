using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// The type of assembly that results from the
    /// compile operation.
    /// </summary>
    public enum AssemblyOutputType
    {
        /// <summary>
        /// The manifest module is a class library.
        /// </summary>
        ClassLibrary,
        /// <summary>
        /// The manifest module is a console application.
        /// </summary>
        ConsoleApplication,
        /// <summary>
        /// The manifest module is a windows application.
        /// </summary>
        WindowsApplication,
    }
}
