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
    public class MalleableCompilationContext :
        IMalleableCompilationContext
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
        /// Returns the immutable <see cref="ICompilationContext"/> associated to the
        /// current <see cref="MalleableCompilationContext"/>.
        /// </summary>
        /// <remarks>Typically used by a language's compiler to fix
        /// the details of the resultant assembly such that changes 
        /// to the original 
        /// <see cref="<see cref="MalleableCompilationContext"/>"/>
        /// are ignored.</remarks>
        /// <returns>A <see cref="ICompilationContext"/>
        /// whose members cannot be changed.</returns>
        public ICompilationContext GetImmutableContext()
        {
            return new FixedContext(this);
        }

        /// <summary>
        /// Creates a new <see cref="MalleableCompilationContext"/> initialized to its default state.
        /// </summary>
        public MalleableCompilationContext()
        {
        }

        private class FixedContext :
            ICompilationContext
        {

            internal FixedContext(MalleableCompilationContext context)
            {
                this.Optimize = context.Optimize;
                this.COMVisible = context.COMVisible;
                this.AllowUnsafeCode = context.AllowUnsafeCode;
                this.GenerateXMLDocs = context.AllowUnsafeCode;
                this.DebugSupport = context.DebugSupport;
                this.OutputType = context.OutputType;
                this.WarnLevel = context.WarnLevel;
                this.ArithmeticOverflowChecks = context.ArithmeticOverflowChecks;
            }

            #region ICompilationContext Members

            public bool Optimize { get; private set; }

            public bool COMVisible { get; private set; }

            public bool AllowUnsafeCode { get; private set; }

            public bool GenerateXMLDocs { get; private set; }

            public DebugSupport DebugSupport { get; private set; }

            public AssemblyOutputType OutputType { get; private set; }

            public WarningLevel WarnLevel { get; private set; }

            public bool ArithmeticOverflowChecks { get; private set; }

            #endregion
        }
    }
}
