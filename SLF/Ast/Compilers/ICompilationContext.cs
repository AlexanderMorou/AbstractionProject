using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Ast;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public interface ICompilationContext
    {
        /// <summary>
        /// Returns whether the code should be optimized.
        /// </summary>
        bool Optimize { get; }
        /// <summary>
        /// Returns whether the resulted assembly is com visible.
        /// </summary>
        bool COMVisible { get; }
        /// <summary>
        /// Returns whether the assembly can have unsafe code.
        /// </summary>
        bool AllowUnsafeCode { get; }
        /// <summary>
        /// Returns whether the compiler should generate an accompanying 
        /// XML documentation set.
        /// </summary>
        bool GenerateXMLDocs { get; }
        /// <summary>
        /// Returns the level of support given to debug output.
        /// </summary>
        DebugSupport DebugSupport { get; }
        /// <summary>
        /// Returns the type of assembly that will result from the compile operation.
        /// </summary>
        AssemblyOutputType OutputType { get; }
        /// <summary>
        /// Returns the level of warnings displayed by the compiler.
        /// </summary>
        WarningLevel WarnLevel { get; }
        /// <summary>
        /// Returns whether arithmetic overflow checks are on by default.
        /// </summary>
        bool ArithmeticOverflowChecks { get; }
    }
}
