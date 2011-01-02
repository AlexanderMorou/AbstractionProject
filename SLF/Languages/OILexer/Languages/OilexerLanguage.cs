using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Parsers.Oilexer;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public class OilexerLanguage :
        IHighLevelLanguage<IGDFile>
    {
        internal static OilexerLanguage LanguageInstance = new OilexerLanguage();
        #region IHighLevelLanguage<IGDFile> Members

        public IHighLevelLanguageProvider<IGDFile> GetProvider()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ILanguage Members

        public string Name
        {
            get { return "OILexer Grammar Description Language"; }
        }

        ILanguageProvider ILanguage.GetProvider()
        {
            return this.GetProvider();
        }

        #endregion

        #region ILanguage Members

        public Compilers.CompilerSupport CompilerSupport
        {
            get { return Compilers.CompilerSupport.FullSupport ^ (Compilers.CompilerSupport.DebuggerSupport | Compilers.CompilerSupport.COMInterop | Compilers.CompilerSupport.Unsafe | Compilers.CompilerSupport.Win32Resources); }
        }

        #endregion
    }
}
