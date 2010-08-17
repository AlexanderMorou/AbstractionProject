using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Provides a base class for compiler options.
    /// </summary>
    public class CompilerOptions :
        ICompilerOptions
    {
        #region ICompilerOptions Members

        public bool Optimize { get; set; }

        public bool COMVisible { get; set; }

        public bool AllowUnsafeCode { get; set; }

        public bool GenerateXMLDocs { get; set; }

        public string Target { get; set; }

        public DebugSupport DebugSupport { get; set; }

        public AssemblyOutputType OutputType { get; set; }

        public WarningLevel WarnLevel { get; set; }

        #endregion
    }
}
