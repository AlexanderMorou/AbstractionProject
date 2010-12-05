using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// The kinds of functionality the compiler supports.
    /// </summary>
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
        /// Compiler supports generic parameter based duck typing.
        /// </summary>
        DuckTyping,
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
        /// <summary>
        /// Compiler supports embedding primary interop assembly meta-data pertinent
        /// to scope of the assembly to compile.
        /// </summary>
        PrimaryInteropEmbedding = 2048,
        /// <summary>
        /// The compiler supports all functionality.
        /// </summary>
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
                    | PrimaryInteropEmbedding
    }
}
