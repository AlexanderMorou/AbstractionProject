 /* -----------------------------------------------------------\
 |  This code was generated by Oilexer.                        |
 |  Version: 1.0.0.0                                           |
 |-------------------------------------------------------------|
 |  To ensure the code works properly,                         |
 |  please do not make any changes to the file.                |
 |-------------------------------------------------------------|
 |  The specific language is C# (Runtime version: v2.0.50727)  |
 |  Sub-tool Name: Oilexer.CSharpCodeTranslator                |
 |  Sub-tool Version: 1.0.0.0                                  |
 \----------------------------------------------------------- */
using System;
using AllenCopeland.Abstraction.Slf.Languages.Oilexer.Rules;
 /*---------------------------------------------------------------------\
 | Copyright � 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer.Tokens
{
    // Module: RootModule
    partial class RegularLanguageScanData
    {
        #region RegularLanguageScanData nested types
        public class CaptureEntry :
            Entry
        {
            #region CaptureEntry data members
            private string capture;
            #endregion // CaptureEntry data members
            #region CaptureEntry properties
            public string Capture
            {
                get
                {
                    return this.capture;
                }
            }
            
            public override int Length
            {
                get
                {
                    return this.capture.Length;
                }
            }
            #endregion // CaptureEntry properties
            #region CaptureEntry .ctors
            public CaptureEntry(GrammarVocabulary tokenId, string capture)
                 : base(tokenId)
            {
                this.capture = capture;
            }
            #endregion // CaptureEntry .ctors

            public override string ToString()
            {
                return string.Format("{0} ({1})", base.ToString(), this.Capture);
            }
        }
        #endregion // RegularLanguageScanData nested types
    }
}
 /* ------------------------------------------------------------\
 |  This file took 00:00:00.0007006 to generate.                |
 |  Date generated: 8/26/2011 4:13:06 PM                        |
 |  There were 4 types used by this file                        |
 |  System.String, System.Int32, RegularLanguageReaderTokens,   |
 |  RegularLanguageScanData+Entry                         |
 |--------------------------------------------------------------|
 |  There were 1 assemblies referenced:                         |
 |  mscorlib                                                    |
 \------------------------------------------------------------ */