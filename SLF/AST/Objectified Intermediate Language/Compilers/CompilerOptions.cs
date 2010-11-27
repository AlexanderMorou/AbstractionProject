using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Provides a base class for compiler options.
    /// </summary>
    public class CompilerOptions :
        ICompilerOptions
    {
        #region ICompilerOptions Members

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
        /// Returns/sets the assembly target file name.
        /// </summary>
        /// <remarks>If <see cref="InMemory"/> is true, returns null; otherwise the target file.</remarks>
        public string Target { get; set; }

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
        /// Returns the <see cref="ILanguage"/> associated to the compile process.
        /// </summary>
        public ILanguage Language { get; private set; }

        /// <summary>
        /// Returns/sets whether arithmetic overflow checks are on by default.
        /// </summary>
        public bool ArithmeticOverflowChecks { get; set; }

        #endregion

        /// <summary>
        /// Creates a new <see cref="CompilerOptions"/> for the
        /// <paramref name="language"/> provided.
        /// </summary>
        /// <param name="language">The <see cref="ILanguage"/> which represents
        /// details about the langauge the intermediate code is being compiled
        /// in.</param>
        public CompilerOptions(ILanguage language)
        {
            this.Language = language;
        }
    }
}
