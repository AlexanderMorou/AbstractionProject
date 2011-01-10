using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public interface ICompilationContext
    {
        /// <summary>
        /// Returns/sets whether the code should be optimized.
        /// </summary>
        bool Optimize { get; set; }
        /// <summary>
        /// Returns/sets whether the resulted assembly is com visible.
        /// </summary>
        bool COMVisible { get; set; }
        /// <summary>
        /// Returns/sets whether the assembly can have unsafe code.
        /// </summary>
        bool AllowUnsafeCode { get; set; }
        /// <summary>
        /// Returns/sets whether the compiler should generate an accompanying 
        /// XML documentation set.
        /// </summary>
        bool GenerateXMLDocs { get; set; }
        /// <summary>
        /// Returns/sets the level of support given to debug output.
        /// </summary>
        DebugSupport DebugSupport { get; set; }
        /// <summary>
        /// Returns/sets the type of assembly that will result from the compile operation.
        /// </summary>
        AssemblyOutputType OutputType { get; set; }
        /// <summary>
        /// Returns/sets the level of warnings displayed by the compiler.
        /// </summary>
        WarningLevel WarnLevel { get; set; }
        /// <summary>
        /// Returns/sets whether arithmetic overflow checks are on by default.
        /// </summary>
        bool ArithmeticOverflowChecks { get; set; }
    }
}
