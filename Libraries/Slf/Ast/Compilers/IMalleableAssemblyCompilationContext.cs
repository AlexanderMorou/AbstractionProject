using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Defines properties and methods for working with an assembly's compilation context
    /// setup in a malleable state.
    /// </summary>
    public interface IMalleableAssemblyCompilationContext :
        IAssemblyCompilationContext
    {
        /// <summary>
        /// Returns/sets whether the code should be optimized.
        /// </summary>
        new bool Optimize { get; set; }
        /// <summary>
        /// Returns/sets whether the resulted assembly is com visible.
        /// </summary>
        new bool COMVisible { get; set; }
        /// <summary>
        /// Returns/sets whether the assembly can have unsafe code.
        /// </summary>
        new bool AllowUnsafeCode { get; set; }
        /// <summary>
        /// Returns/sets whether the compiler should generate an
        /// accompanying XML documentation set.
        /// </summary>
        new bool GenerateXMLDocs { get; set; }
        /// <summary>
        /// Returns/sets the level of support given to debug output.
        /// </summary>
        new DebugSupport DebugSupport { get; set; }
        /// <summary>
        /// Returns/sets the type of assembly that will result from
        /// the compile operation.
        /// </summary>
        new AssemblyOutputType OutputType { get; set; }
        /// <summary>
        /// Returns/sets the level of warnings displayed by the compiler.
        /// </summary>
        new WarningLevel WarnLevel { get; set; }
        /// <summary>
        /// Returns/sets whether arithmetic overflow checks are on by
        /// default.
        /// </summary>
        new bool ArithmeticOverflowChecks { get; set; }
        /// <summary>
        /// Returns the immutable <see cref="IAssemblyCompilationContext"/>
        /// associated to the current
        /// <see cref="IMalleableAssemblyCompilationContext"/>.
        /// </summary>
        /// <remarks>Typically used by a language's compiler to fix
        /// the details of the resultant assembly such that changes
        /// to the original <see cref="IMalleableAssemblyCompilationContext"/>
        /// are ignored.</remarks>
        /// <returns>A <see cref="IAssemblyCompilationContext"/> whose members
        /// cannot be changed.</returns>
        IAssemblyCompilationContext GetImmutableContext();
    }
}
