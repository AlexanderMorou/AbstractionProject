using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Oil;
/*---------------------------------------------------------------------\
| Copyright © 2008-2011 Allen Copeland Jr.                             |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Provides a base class for compiler options.
    /// </summary>
    public class CompilationContext :
        ICompilationContext
    {
        #region ICompilationContext Members

        /// <summary>
        /// Returns/sets whether the code should be optimized.
        /// </summary>
        public bool Optimize { get; set; }

        /// <summary>
        /// Returns/sets whether the resulted assembly is com visible.
        /// </summary>
        public bool COMVisible { get; set; }

        /// <summary>
        /// Returns/sets whether the assembly can have unsafe code.
        /// </summary>
        public bool AllowUnsafeCode { get; set; }

        /// <summary>
        /// Returns/sets whether the compiler should generate an accompanying XML documentation set.
        /// </summary>
        public bool GenerateXMLDocs { get; set; }

        /// <summary>
        /// Returns/sets the level of support given to debug output.
        /// </summary>
        public DebugSupport DebugSupport { get; set; }

        /// <summary>
        /// Returns/sets the type of assembly that will result from the compile operation.
        /// </summary>
        public AssemblyOutputType OutputType { get; set; }

        /// <summary>
        /// Returns/sets the level of warnings displayed by the compiler.
        /// </summary>
        public WarningLevel WarnLevel { get; set; }

        /// <summary>
        /// Returns/sets whether arithmetic overflow checks are on by default.
        /// </summary>
        public bool ArithmeticOverflowChecks { get; set; }

        #endregion

        /// <summary>
        /// Creates a new <see cref="CompilationContext"/> initialized to its default state.
        /// </summary>
        public CompilationContext()
        {
        }
    }
}
