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
    /// <summary>
    /// What level of warning messages should be displayed by the compiler.
    /// </summary>
    /// <remarks>Descriptions sourced from C# Compiler options in the MSDN library.
    /// <seealso cref="http://msdn.microsoft.com/en-us/library/13b90fz7.aspx"/></remarks>
    [Flags]
    public enum WarningLevel
    {
        /// <summary>
        /// No warning levels are emitted.
        /// </summary>
        None   = 0x0,
        /// <summary>
        /// Displays severe warning messages.
        /// </summary>
        Level1 = 0x1,
        /// <summary>
        /// Displays certain less-severe warnings, such as warnings about hiding class members.
        /// </summary>
        Level2 = 0x2,
        /// <summary>
        /// Displays level one, and those less severe, such as warnings about hiding class
        /// members, warnings.
        /// </summary>
        Level2Full = Level2 | Level1,
        /// <summary>
        /// Displays certain less-severe warnings, such as warnings about expressions
        /// that always evaluate to true or false.
        /// </summary>
        Level3 = 0x4,
        /// <summary>
        /// Displays level one, two, those less severe, such as warnings about expressions
        /// that always evaluate to true or false, warnings.
        /// </summary>
        Level3Full = Level3 | Level2Full,
        /// <summary>
        /// Displays all informational warnings.
        /// </summary>
        Level4 = 0x8,
        /// <summary>
        /// Displays level one, two, three, and all informational warnings.
        /// </summary>
        Level4Full = Level4 | Level3Full,
    }
}
