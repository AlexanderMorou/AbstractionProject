 /* -----------------------------------------------------------\
 |  This code was generated by Oilexer.                        |
 |  Version: 1.0.0.0                                           |
 |-------------------------------------------------------------|
 |  To ensure the code works properly,                         |
 |  please do not make any changes to the file.                |
 |-------------------------------------------------------------|
 |  The specific language is C# (Runtime version: v4.0.30319)  |
 |  Sub-tool Name: Oilexer.CSharpCodeTranslator                |
 |  Sub-tool Version: 1.0.0.0                                  |
 \----------------------------------------------------------- */
using System;
using AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser.Tokens;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser.Tokens
{
    
    // Module: RootModule
    [FlagsAttribute()]
    internal enum AssemblyTerminalCases
    {
        /// <summary>
        /// Represents a case where no elements in the current set are selected.
        /// </summary>
        None = 0,

        /// <summary>
        /// <para>Defines an assemblyterminal in the TypeIdentity lexer that is '7' characters long.</para>
        /// <para>Actual value: Version</para>
        /// </summary>
        /// <remarks>
        /// <para>Original definition: @"Version":Version;</para>
        /// <para>Defined in "tid.oilexer" on line 28.</para>
        /// </remarks>
        Version = 1,

        /// <summary>
        /// <para>Defines an assemblyterminal in the TypeIdentity lexer that is '7' characters long.</para>
        /// <para>Actual value: Culture</para>
        /// </summary>
        /// <remarks>
        /// <para>Original definition: @"Culture":Culture;</para>
        /// <para>Defined in "tid.oilexer" on line 29.</para>
        /// </remarks>
        Culture = 2,

        /// <summary>
        /// <para>Defines an assemblyterminal in the TypeIdentity lexer that is '14' characters long.</para>
        /// <para>Actual value: PublicKeyToken</para>
        /// </summary>
        /// <remarks>
        /// <para>Original definition: @"PublicKeyToken":KeyToken;</para>
        /// <para>Defined in "tid.oilexer" on line 30.</para>
        /// </remarks>
        KeyToken = 4,

        /// <summary>
        /// <para>Defines an assemblyterminal in the TypeIdentity lexer that is '4' characters long.</para>
        /// <para>Actual value: null</para>
        /// </summary>
        /// <remarks>
        /// <para>Original definition: @"null":NullPublicKeyToken;</para>
        /// <para>Defined in "tid.oilexer" on line 31.</para>
        /// </remarks>
        NullPublicKeyToken = 8,

        /// <summary>
        /// Represents all the members of the current set of values for AssemblyTerminals.
        /// </summary>
        All = 15
    }
}
 /* ----------------------------------------------\
 |  This file took 00:00:00.0024395 to generate.  |
 |  Date generated: 4/8/2013 7:25:03 PM           |
 |  There were 2 types used by this file          |
 |  FlagsAttribute, AssemblyTerminalCases         |
 |------------------------------------------------|
 |  There were 1 assemblies referenced:           |
 |  mscorlib                                      |
 \---------------------------------------------- */