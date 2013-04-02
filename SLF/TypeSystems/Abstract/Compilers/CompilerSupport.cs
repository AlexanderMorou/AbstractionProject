using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
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
        XMLDocumentation            = 0x0001,
        /// <summary>
        /// Compiler supports generic parameter based structural typing.
        /// </summary>
        StructuralTyping            = 0x0002,
        /// <summary>
        /// Compiler supports optimization.
        /// </summary>
        Optimization                = 0x0004,
        /// <summary>
        /// Compiler supports unsafe code which is not CLSCompliant.
        /// </summary>
        Unsafe                      = 0x0008,
        /// <summary>
        /// Compiler supports COM Interop.
        /// </summary>
        COMInterop                  = 0x0010,
        /// <summary>
        /// Compiler supports automated variations in the the output
        /// assembly based off of conditionals defined for a given
        /// compilation.
        /// </summary>
        ConditionalCompilation      = 0x0020,
        /// <summary>
        /// Compiler supports .NET Resources.
        /// </summary>
        Resources                   = 0x0040,
        /// <summary>
        /// Compiler supports win32 resources.
        /// </summary>
        Win32Resources              = 0x0080,
        /// <summary>
        /// Compiler supports signing the assembly.
        /// </summary>
        Signing                     = 0x0100,
        /// <summary>
        /// Compiler supports creation of multi-file assemblies.
        /// </summary>
        MultiFileAssemblies         = 0x0200,
        /// <summary>
        /// Compiler supports libraries with full debugger support.
        /// </summary>
        DebuggerSupport             = 0x0400,
        /// <summary>
        /// Compiler supports reading more commands from a response file.
        /// </summary>
        /// <remarks>Applies to compilers that are accessed through a command-line
        /// only.</remarks>
        ResponseFile                = 0x0800,
        /// <summary>
        /// Compiler supports embedding primary interop assembly meta-data pertinent
        /// to scope of the assembly to compile.
        /// </summary>
        PrimaryInteropEmbedding     = 0x1000,
        /// <summary>
        /// The compiler supports all functionality.
        /// </summary>
        FullSupport                 = XMLDocumentation           | 
                                      StructuralTyping           | 
                                      Optimization               | 
                                      DebuggerSupport            | 
                                      Unsafe                     | 
                                      COMInterop                 | 
                                      ConditionalCompilation     | 
                                      Resources | Win32Resources |
                                      Signing                    | 
                                      MultiFileAssemblies        | 
                                      ResponseFile               | 
                                      PrimaryInteropEmbedding
    }
}
