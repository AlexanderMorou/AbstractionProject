using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    [Flags]
    public enum CompilerSupport :
        short
    {
        /// <summary>
        /// Compiler supports generation of a documentation file 
        /// from documentation comments.
        /// </summary>
        XMLDocumentation = 1,
        /// <summary>
        /// Compiler supports optimization.
        /// </summary>
        Optimization = 2,
        /// <summary>
        /// Compiler supports unsafe code which is not CLSCompliant.
        /// </summary>
        Unsafe = 4,
        /// <summary>
        /// Compiler supports COM Interop.
        /// </summary>
        COMInterop = 8,
        /// <summary>
        /// Compiler supports the define directive.
        /// </summary>
        Define = 16,
        /// <summary>
        /// Compiler supports .NET Resources.
        /// </summary>
        Resources = 32,
        /// <summary>
        /// Compiler supports win32 resources.
        /// </summary>
        Win32Resources = 64,
        /// <summary>
        /// Compiler supports signing the assembly.
        /// </summary>
        Signing = 128,
        /// <summary>
        /// Compiler supports creation of multi-file assemblies.
        /// </summary>
        MultiFileAssemblies = 256,
        /// <summary>
        /// Compiler supports libraries with full debugger support.
        /// </summary>
        DebuggerSupport = 512,
        /// <summary>
        /// Compiler supports reading more commands from a response file.
        /// </summary>
        /// <remarks>Applies to compilers that are accessed through a command-line
        /// only.</remarks>
        ResponseFile = 1024,
        FullSupport = XMLDocumentation
                    | Optimization
                    | DebuggerSupport
                    | Unsafe
                    | COMInterop
                    | Define
                    | Resources | Win32Resources
                    | Signing
                    | MultiFileAssemblies
                    | ResponseFile
    }
}
