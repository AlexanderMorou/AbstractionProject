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
using System.Collections.Generic;
namespace AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser.Rules
{
    
    // Module: RootModule
    internal interface ITITypeIdentityRule
    {
        string Namespace { get; }
        IEnumerable<string> Names { get; }
        int TypeParameterCount { get; }
        bool HasTypeReplacements { get; }
        IEnumerable<ITITypeParameterIdentityRule> TypeReplacements { get; }
        bool HasElementClassifications { get; }
        IEnumerable<ITIElementClassificationRule> ElementClassifications { get; }
    }
}
 /* ----------------------------------------------\
 |  This file took 00:00:00.0001129 to generate.  |
 |  Date generated: 4/8/2013 2:00:48 AM           |
 |  There were 0 types used by this file          |
 |                                                |
 \---------------------------------------------- */
