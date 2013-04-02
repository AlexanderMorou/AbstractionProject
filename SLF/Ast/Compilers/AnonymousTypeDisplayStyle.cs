using System;
using System.Diagnostics.CodeAnalysis;
using AllenCopeland.Abstraction.Slf.Abstract;
/*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /* *
     * Suppressed CA2217 due to the nature of the flags.  They're
     * intended to be used together, but only for certain combinations.
     * */
    /// <summary>
    /// The display style of anonymous types.
    /// </summary>
    [SuppressMessage("Microsoft.Usage", "CA2217:DoNotMarkEnumsWithFlags")]
    [Flags]
    public enum AnonymousTypeDisplayStyles
    {
        /* *
         * So that Clean + Visual Basic and Clean + C#
         * can be used; however these combinations
         * cannot be used with '|'
         * Visual Basic + C#
         * Visual Basic + Other
         * C# + Other
         * 000011 - Visual Basic
         * 000110 - C#
         * 011000 - Clean
         * 110010 - Other
         * */
        /// <summary>
        /// The <see cref="IAnonymousType"/> and its
        /// members display in Visual Basic mode.
        /// </summary>
        VisualBasic = 0x3,
        /// <summary>
        /// The <see cref="IAnonymousType"/> and its
        /// members display in C&#9839; mode.
        /// </summary>
        CSharp = 0x6,
        /// <summary>
        /// The <see cref="IAnonymousTypePatternAid"/>
        /// displays in a clean format free of any
        /// characters that might cause
        /// compile errors in the language.
        /// </summary>
        /// <remarks>Does not apply
        /// in the case of Common Intermediate Language
        /// (CIL).</remarks>
        Clean = 0x18,
        /// <summary>
        /// The <see cref="IAnonymousType"/>s
        /// use another source for displaying 
        /// the members.
        /// </summary>
        Other = 0x32,
    }
}
