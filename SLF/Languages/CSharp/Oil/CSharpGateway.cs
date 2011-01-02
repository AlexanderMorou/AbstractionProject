using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public static class CSharpGateway
    {
        public static class Language
        {
            public static readonly ICSharpLanguage Version2 = new CSharpLanguage(CSharpLanguageVersion.CSharp_v2);
            public static readonly ICSharpLanguage Version3 = new CSharpLanguage(CSharpLanguageVersion.CSharp_v3);
            public static readonly ICSharpLanguage Version3_5 = new CSharpLanguage(CSharpLanguageVersion.CSharp_v3_5);
            public static readonly ICSharpLanguage Version4 = new CSharpLanguage(CSharpLanguageVersion.CSharp_v4);
            public static readonly ICSharpLanguage Version5 = new CSharpLanguage(CSharpLanguageVersion.CSharp_v5);
            
        }
    }
}
