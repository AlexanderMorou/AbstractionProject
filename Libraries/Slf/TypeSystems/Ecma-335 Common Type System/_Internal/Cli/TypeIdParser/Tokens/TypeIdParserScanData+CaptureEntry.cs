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
using AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser.Tokens;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser.Tokens
{
    // Module: RootModule
    partial class TypeIdParserScanData
    {
        #region TypeIdParserScanData nested types
        public class CaptureEntry :
            Entry
        {
            #region CaptureEntry data members
            private string capture;
            
            private TokenTransition? transition;
            #endregion // CaptureEntry data members
            #region CaptureEntry properties
            public string Capture
            {
                get
                {
                    return this.capture;
                }
            }
            #endregion // CaptureEntry properties
            #region CaptureEntry methods
            internal override TokenTransition GetTransition()
            {
                if (this.transition == null)
                    this.transition = new TokenTransition(((TypeIdParserTokens)(base.SubsetIndex)));
                return this.transition.Value;
            }
            #endregion // CaptureEntry methods
            #region CaptureEntry .ctors
            public CaptureEntry(TypeIdParserTokens tokenId, string capture)
                 : base(((int)(tokenId)))
            {
                this.capture = capture;
            }
            #endregion // CaptureEntry .ctors
        }
        #endregion // TypeIdParserScanData nested types
    }
}
 /* ------------------------------------------------------\
 |  This file took 00:00:00.0003334 to generate.          |
 |  Date generated: 4/8/2013 7:25:03 PM                   |
 |  There were 5 types used by this file                  |
 |  System.String, TokenTransition, TypeIdParserTokens,   |
 |  System.Int32, TypeIdParserScanData+Entry              |
 |--------------------------------------------------------|
 |  There were 1 assemblies referenced:                   |
 |  mscorlib                                              |
 \------------------------------------------------------ */